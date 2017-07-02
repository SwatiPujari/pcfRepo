using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Xpedeon.ProfitCashflow
{
    public partial class DivisionMaster : DevExpress.XtraEditors.XtraForm
    {
        sDivisionMaster oDivisionMaster = new sDivisionMaster();
        sCommonMethods oComm = new sCommonMethods();

        DataTable dtDivisionMaster, dtRegionList, dtCompanyList;
        DataRow drDivision;
        string sDivName = "";
        bool bHasChanges = false, bIsFrmClosing = false;

        public DivisionMaster()
        {
            InitializeComponent();
        }

        private void DivisionMaster_Load(object sender, EventArgs e)
        {
            try
            {
                dtDivisionMaster = oDivisionMaster.RetrieveDivisionMaster();
                dtDivisionMaster.Constraints.Add("PK_DIVISION", dtDivisionMaster.Columns["DIVISION_CODE"], true);

                dtRegionList = oDivisionMaster.RetrieveRegionList();
                if (dtRegionList != null)
                {
                    DataColumn dcolSelect = new DataColumn("Select", typeof(string));
                    dcolSelect.DefaultValue = "N";
                    dtRegionList.Columns.Add(dcolSelect);
                }
                dtRegionList.Constraints.Add("PK_REGION", dtRegionList.Columns["REGION_ID"], true);

                dtCompanyList = oComm.RetrieveCompanyList(ProfitCashflow.oPcfDM.UserName);
                dtCompanyList.Constraints.Add("PK_COMPANY", dtCompanyList.Columns["COMPANY_ID"], true);
                repLookUpComp.DataSource = dtCompanyList;

                gvDivisionMaster.BeginSort();
                gvDivisionMaster.EndSort();
                gvIncludedRegion.BeginSort();
                gvIncludedRegion.EndSort();
                gvAddRegion.BeginSort();
                gvAddRegion.EndSort();

                gvDivisionMaster.Focus();
                gcDivisionMaster.DataSource = dtDivisionMaster;

                btnLink.Enabled = false;
                btnUnlink.Enabled = false;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DivisionMaster_FormClosing(object sender, FormClosingEventArgs e)
        {
            bIsFrmClosing = true;
            e.Cancel = false;
            
            if (bHasChanges)
            {
                DialogResult oTempResult = XtraMessageBox.Show("Data has been changed, do you want to save the changes?", Application.ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (oTempResult == DialogResult.Yes)
                {
                    if (!gvDivisionMaster.PostEditor())
                        e.Cancel = true;
                    if (!gvDivisionMaster.UpdateCurrentRow())
                        e.Cancel = true;
                }
                else if (oTempResult == DialogResult.No)
                {
                    gvDivisionMaster.CancelUpdateCurrentRow();
                    dtDivisionMaster.RejectChanges();
                    bHasChanges = false;
                }
                else
                    e.Cancel = true;
            }

            bIsFrmClosing = false;
            if (e.Cancel) return;

            gvIncludedRegion.PostEditor();
            gvAddRegion.PostEditor();
            e.Cancel = DoBeforeClose();
        }

        private void DivisionMaster_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                bHasChanges = false;
                bIsFrmClosing = false;
            }
        }

        private bool DoBeforeClose()
        {
            try
            {
                if (drDivision != null && dtRegionList.Select("Select='Y' AND DIVISION_ID IS NOT NULL").Length > 0)
                {
                    DialogResult oTempResult = XtraMessageBox.Show("Some of the Included Regions are selected for removal. Do you want to save it?", Application.ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (oTempResult == DialogResult.Yes)
                        btnUnlink_Click(new object(), new EventArgs());
                    else if (oTempResult == DialogResult.No)
                        dtRegionList.RejectChanges();
                    else
                        return true;
                }
                if (drDivision != null && dtRegionList.Select("Select='Y' AND DIVISION_ID IS NULL").Length > 0)
                {
                    DialogResult oTempResult = XtraMessageBox.Show("Some of the Regions are selected for inclusion. Do you want to save it?", Application.ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (oTempResult == DialogResult.Yes)
                        btnLink_Click(new object(), new EventArgs());
                    else if (oTempResult == DialogResult.No)
                        dtRegionList.RejectChanges();
                    else
                        return true;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }

            return false;
        }

        private void gvDivisionMaster_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            bHasChanges = true;
        }

        private void gvDivisionMaster_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            gvDivisionMasterFocusedRowChanged(e.FocusedRowHandle);
        }

        private void gvDivisionMasterFocusedRowChanged(int iFocusedRowHandle)
        {
            drDivision = gvDivisionMaster.GetDataRow(iFocusedRowHandle);
            if (drDivision != null)
            {
                sDivName = Convert.ToString(drDivision["DIVISION_NAME"]);
                if (drDivision["DIVISION_ID"] != DBNull.Value && !string.IsNullOrWhiteSpace(drDivision["DIVISION_ID"].ToString()))
                    gcIncludedRegion.DataSource = new DataView(dtRegionList, "DIVISION_ID IS NOT NULL AND DIVISION_ID=" + drDivision["DIVISION_ID"], "", DataViewRowState.CurrentRows);
                else 
                    gcIncludedRegion.DataSource = null;
                gcAddRegion.DataSource = new DataView(dtRegionList, "DIVISION_ID IS NULL", "", DataViewRowState.CurrentRows);

                if (drDivision.RowState == DataRowState.Added)
                    gcolCode.OptionsColumn.AllowEdit = true;
                else
                    gcolCode.OptionsColumn.AllowEdit = false;

                MakeControlsEnableDisable();
            }
            else
            {
                gcolCode.OptionsColumn.AllowEdit = true;
                gcIncludedRegion.DataSource = null;
                gcAddRegion.DataSource = null;

                sDivName = "";
                btnLink.Enabled = false;
                btnUnlink.Enabled = false;
            }
        }

        private void gvDivisionMaster_BeforeLeaveRow(object sender, DevExpress.XtraGrid.Views.Base.RowAllowEventArgs e)
        {
            e.Allow = !DoBeforeClose();
        }

        private void gvDivisionMaster_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            drDivision = gvDivisionMaster.GetDataRow(e.RowHandle);
            drDivision["IS_ACTIVE"] = "Y";
            drDivision["CREATED_BY"] = ProfitCashflow.oPcfDM.UserName;
            drDivision["CREATED_ON"] = DateTime.Now;
        }

        private void gvDivisionMaster_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void gvDivisionMaster_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace();
            System.Diagnostics.StackFrame[] stackFrames = stackTrace.GetFrames();

            var oCallingMethod = stackTrace.GetFrames().AsEnumerable().Where(o => o.GetMethod().Name == "WmClose").ToList();
            if (oCallingMethod.Count > 0)
            {
                if (bIsFrmClosing)
                    bIsFrmClosing = false;
                else
                {
                    e.Valid = false;
                    return;
                }
            }

            drDivision = gvDivisionMaster.GetDataRow(e.RowHandle);
            if (drDivision == null) return;

            if (drDivision["DIVISION_CODE"] == DBNull.Value || drDivision["DIVISION_CODE"].ToString().Trim() == string.Empty)
            {
                XtraMessageBox.Show("Division Code is mandatory.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Valid = false;
                return;
            }
            else if (drDivision["DIVISION_CODE"] != DBNull.Value && !string.IsNullOrWhiteSpace(drDivision["DIVISION_CODE"].ToString()))
            {
                DataRow[] drRows = dtDivisionMaster.Select("DIVISION_CODE = '" + drDivision["DIVISION_CODE"] + "'");
                foreach (DataRow drCode in drRows)
                    if (dtDivisionMaster.Rows.IndexOf(drCode) != dtDivisionMaster.Rows.IndexOf(drDivision) && drCode.RowState != DataRowState.Deleted)
                        if (Convert.ToString(drCode["DIVISION_CODE"]) == Convert.ToString(drDivision["DIVISION_CODE"]))
                        {
                            XtraMessageBox.Show("Duplicate Division Code is not allowed.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            e.Valid = false;
                            return;
                        }
            }
            else
                drDivision["DIVISION_CODE"] = drDivision["DIVISION_CODE"].ToString().ToUpper();

            if (gvDivisionMaster.IsNewItemRow(e.RowHandle) || (drDivision.RowState == DataRowState.Modified &&
                Convert.ToString(drDivision["DIVISION_CODE", DataRowVersion.Original]) != Convert.ToString(drDivision["DIVISION_CODE", DataRowVersion.Current])))
            {
                object oResult = oDivisionMaster.RetrieveDuplicateDivisionCount(Convert.ToString(drDivision["DIVISION_CODE"]));
                if (oResult != null && Convert.ToDecimal(oResult) > 0)
                {
                    XtraMessageBox.Show("Duplicate Division Code is not allowed.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Valid = false;
                    return;
                }
            }

            if (drDivision["DIVISION_NAME"] == DBNull.Value || drDivision["DIVISION_NAME"].ToString().Trim() == string.Empty)
            {
                XtraMessageBox.Show("Division Name is mandatory.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Valid = false;
                return;
            }

            if (Convert.ToString(drDivision["IS_ACTIVE"]) == "N" && dtRegionList.Select("DIVISION_ID IS NOT NULL AND DIVISION_ID=" + drDivision["DIVISION_ID"]).Length > 0)
            {
                XtraMessageBox.Show("A Division with Included Regions cannot be deactivated. Please remove Included Regions and then deactivate.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Valid = false;
                return;
            }

            if (Convert.ToString(drDivision["IS_ACTIVE"]) == "N" && dtDivisionMaster.Select("IS_ACTIVE='Y' AND DIVISION_CODE <> '" + drDivision["DIVISION_CODE"] + "'").Length == 0)
            {
                XtraMessageBox.Show("At least one Division must remain Active.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Valid = false;
                return;
            }

            if (drDivision["COMPANY_ID"] != DBNull.Value && !string.IsNullOrWhiteSpace(drDivision["COMPANY_ID"].ToString()) && dtDivisionMaster.Rows.Count > 0)
                if (dtDivisionMaster.Select("COMPANY_ID = " + drDivision["COMPANY_ID"] + " AND DIVISION_CODE <> '" + drDivision["DIVISION_CODE"] + "'").Length > 0)
                {
                    XtraMessageBox.Show("Company has already been linked to some other Division.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Valid = false;
                    return;
                }

            if (!string.IsNullOrEmpty(sDivName) && sDivName != Convert.ToString(drDivision["DIVISION_NAME"]))
            {
                DialogResult oResult = XtraMessageBox.Show("Are you sure, you want to update Division name?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (oResult == System.Windows.Forms.DialogResult.No)
                {
                    e.Valid = false;
                    return;
                }
            }

            drDivision["CREATED_BY"] = ProfitCashflow.oPcfDM.UserName;
            drDivision["LAST_UPDATED"] = DateTime.Now;
        }

        private void gvDivisionMaster_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            try
            {
                if (dtDivisionMaster.GetChanges() != null)
                {
                    oDivisionMaster.UpdateDivisionMaster(dtDivisionMaster.GetChanges());
                    dtDivisionMaster.AcceptChanges();

                    int iPrevHandle = gvDivisionMaster.FocusedRowHandle;
                    dtDivisionMaster = oDivisionMaster.RetrieveDivisionMaster();
                    gcDivisionMaster.DataSource = dtDivisionMaster;

                    if (iPrevHandle == gvDivisionMaster.FocusedRowHandle)
                        gvDivisionMasterFocusedRowChanged(gvDivisionMaster.FocusedRowHandle);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bHasChanges = false;
        }

        private void btnLink_Click(object sender, EventArgs e)
        {
            if (drDivision == null) return;

            DataRow[] drLinks = dtRegionList.Select("Select='Y' AND DIVISION_ID IS NULL");
            if (drLinks.Length > 0)
            {
                gvIncludedRegion.BeginUpdate();
                foreach (DataRow drLink in drLinks)
                    drLink["DIVISION_ID"] = drDivision["DIVISION_ID"];

                try
                {
                    if (dtRegionList.GetChanges() != null)
                    {
                        oDivisionMaster.UpdateDivisionAndRegionLinkage(dtRegionList.GetChanges());
                        foreach (DataRow drTemp in dtRegionList.Select("Select='Y'"))
                            drTemp["Select"] = "N";
                        dtRegionList.AcceptChanges();
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dtRegionList.RejectChanges();
                }

                gvIncludedRegion.EndUpdate();

                gcIncludedRegion.DataSource = new DataView(dtRegionList, "DIVISION_ID IS NOT NULL AND DIVISION_ID=" + drDivision["DIVISION_ID"], "", DataViewRowState.CurrentRows);
                gcAddRegion.DataSource = new DataView(dtRegionList, "DIVISION_ID IS NULL", "", DataViewRowState.CurrentRows);
                gvDivisionMaster.Focus();

                MakeControlsEnableDisable();
            }
        }

        private void btnUnlink_Click(object sender, EventArgs e)
        {
            if (drDivision == null) return;

            DataRow[] drUnlinks = dtRegionList.Select("Select='Y' AND DIVISION_ID IS NOT NULL");
            if (drUnlinks.Length > 0)
            {
                foreach (DataRow drUnlink in drUnlinks)
                    drUnlink["DIVISION_ID"] = DBNull.Value;

                try
                {
                    if (dtRegionList.GetChanges() != null)
                    {
                        oDivisionMaster.UpdateDivisionAndRegionLinkage(dtRegionList.GetChanges());
                        foreach (DataRow drTemp in dtRegionList.Select("Select='Y'"))
                            drTemp["Select"] = "N";
                        dtRegionList.AcceptChanges();

                        gcIncludedRegion.DataSource = new DataView(dtRegionList, "DIVISION_ID IS NOT NULL AND DIVISION_ID=" + drDivision["DIVISION_ID"], "", DataViewRowState.CurrentRows);
                        gcAddRegion.DataSource = new DataView(dtRegionList, "DIVISION_ID IS NULL", "", DataViewRowState.CurrentRows);
                        gvDivisionMaster.Focus();

                        MakeControlsEnableDisable();
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void MakeControlsEnableDisable()
        {
            if (dtRegionList.Select("Select='Y' AND DIVISION_ID IS NOT NULL").Length > 0)
            {
                gvIncludedRegion.OptionsBehavior.Editable = true;
                gvAddRegion.OptionsBehavior.Editable = false;
                btnLink.Enabled = false;
                btnUnlink.Enabled = true;
            }
            else if(dtRegionList.Select("Select='Y' AND DIVISION_ID IS NULL").Length > 0)
            {
                gvIncludedRegion.OptionsBehavior.Editable = false;
                gvAddRegion.OptionsBehavior.Editable = true;
                btnLink.Enabled = true;
                btnUnlink.Enabled = false;
            }
            else
            {
                gvIncludedRegion.OptionsBehavior.Editable = true;
                gvAddRegion.OptionsBehavior.Editable = true;
                btnLink.Enabled = false;
                btnUnlink.Enabled = false;
            }
        }

        private void gvLinkage_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.RowHandle < 0) return;

            if (e.Column == gcolASelect)
            {
                if (e.Value != null && e.Value.ToString() == "Y" && drDivision != null && drDivision["IS_ACTIVE"].ToString() != "Y")
                {
                    XtraMessageBox.Show("Regions cannot be linked for an inactive Division.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    gvAddRegion.SetFocusedRowCellValue(gcolASelect, gvAddRegion.GetDataRow(e.RowHandle)["Select"]);
                    return;
                }

                gvAddRegion.GetDataRow(e.RowHandle)["Select"] = (e.Value != null && e.Value.ToString() == "Y") ? "Y" : "N";
            }

            if (e.Column == gcolISelect)
            {
                try
                {
                    if (e.Value != null && e.Value.ToString() == "Y")
                        oDivisionMaster.CheckCompRegionLinkage(Convert.ToDecimal(drDivision["DIVISION_ID"]), Convert.ToDecimal(gvIncludedRegion.GetDataRow(e.RowHandle)["REGION_ID"]));
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    gvIncludedRegion.SetFocusedRowCellValue(gcolISelect, "N");
                    return;
                }

                gvIncludedRegion.GetDataRow(e.RowHandle)["Select"] = (e.Value != null && e.Value.ToString() == "Y") ? "Y" : "N";
            }

            MakeControlsEnableDisable();
        }

        private void repLookUpComp_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
                gvDivisionMaster.SetFocusedRowCellValue(gcolComp, DBNull.Value);
        }

        private void LookUp_MouseWheel(object sender, MouseEventArgs e)
        {
            if (!((LookUpEdit)sender).IsPopupOpen)
                DevExpress.Utils.DXMouseEventArgs.GetMouseArgs(e).Handled = true;
        }

        private void repLookUpComp_QueryPopUp(object sender, CancelEventArgs e)
        {
            decimal dDivId = -1;
            if (drDivision != null && drDivision["DIVISION_ID"] != DBNull.Value && !string.IsNullOrWhiteSpace(drDivision["DIVISION_ID"].ToString()))
                dDivId = Convert.ToDecimal(drDivision["DIVISION_ID"]);

            if (dDivId == -1)
                ((LookUpEdit)sender).Properties.DataSource = null;
            else
            {
                try
                {
                    ((LookUpEdit)sender).Properties.DataSource = oDivisionMaster.RetrieveCompForLinkedReg(ProfitCashflow.oPcfDM.UserName, dDivId);
                    ((LookUpEdit)sender).Properties.DisplayMember = "COMPANY";
                    ((LookUpEdit)sender).Properties.ValueMember = "COMPANY_ID";
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}