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
    public partial class OperatingCompanyMaster : DevExpress.XtraEditors.XtraForm
    {
        sOperatingCompMast oOpCompMast = new sOperatingCompMast();
        sCommonMethods oComm = new sCommonMethods();

        DataTable dtOpCompMast, dtCompList;
        DataRow drOpComp;
        string sOpCompName = "";
        bool bHasChanges = false, bIsFrmClosing = false;

        public OperatingCompanyMaster()
        {
            InitializeComponent();
        }

        private void OperatingCompanyMaster_Load(object sender, EventArgs e)
        {
            try
            {
                dtOpCompMast = oOpCompMast.RetrieveOperatingCompanyMaster();
                dtOpCompMast.Constraints.Add("PK_OP_COMP", dtOpCompMast.Columns["OPERATING_COMPANY_CODE"], true);

                dtCompList = oOpCompMast.RetrieveCompanyList4OpComp(ProfitCashflow.oPcfDM.UserName);
                if (dtCompList != null)
                {
                    DataColumn dcolSelect = new DataColumn("Select", typeof(string));
                    dcolSelect.DefaultValue = "N";
                    dtCompList.Columns.Add(dcolSelect);
                }
                dtCompList.Constraints.Add("PK_COMP", dtCompList.Columns["COMPANY_ID"], true);

                gvOperatingComp.BeginSort();
                gvOperatingComp.EndSort();
                gvIncludedComp.BeginSort();
                gvIncludedComp.EndSort();
                gvAddComp.BeginSort();
                gvAddComp.EndSort();

                gvOperatingComp.Focus();
                gcOperatingComp.DataSource = dtOpCompMast;

                btnLink.Enabled = false;
                btnUnlink.Enabled = false;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OperatingCompanyMaster_FormClosing(object sender, FormClosingEventArgs e)
        {
            bIsFrmClosing = true;
            e.Cancel = false;
            
            if (bHasChanges)
            {
                DialogResult oTempResult = XtraMessageBox.Show("Data has been changed, do you want to save the changes?", Application.ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (oTempResult == DialogResult.Yes)
                {
                    if (!gvOperatingComp.PostEditor())
                        e.Cancel = true;
                    if (!gvOperatingComp.UpdateCurrentRow())
                        e.Cancel = true;
                }
                else if (oTempResult == DialogResult.No)
                {
                    gvOperatingComp.CancelUpdateCurrentRow();
                    dtOpCompMast.RejectChanges();
                    bHasChanges = false;
                }
                else
                    e.Cancel = true;
            }

            bIsFrmClosing = false;
            if (e.Cancel) return;

            gvIncludedComp.PostEditor();
            gvAddComp.PostEditor();
            e.Cancel = DoBeforeClose();
        }

        private void OperatingCompanyMaster_KeyUp(object sender, KeyEventArgs e)
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
                if (drOpComp != null && dtCompList.Select("Select='Y' AND OPERATING_COMPANY_ID IS NOT NULL").Length > 0)
                {
                    DialogResult oTempResult = XtraMessageBox.Show("Some of the Included Companies are selected for removal. Do you want to save it?", Application.ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (oTempResult == DialogResult.Yes)
                        btnUnlink_Click(new object(), new EventArgs());
                    else if (oTempResult == DialogResult.No)
                        dtCompList.RejectChanges();
                    else
                        return true;
                }
                if (drOpComp != null && dtCompList.Select("Select='Y' AND OPERATING_COMPANY_ID IS NULL").Length > 0)
                {
                    DialogResult oTempResult = XtraMessageBox.Show("Some of the Companies are selected for inclusion. Do you want to save it?", Application.ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (oTempResult == DialogResult.Yes)
                        btnLink_Click(new object(), new EventArgs());
                    else if (oTempResult == DialogResult.No)
                        dtCompList.RejectChanges();
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

        private void gvOperatingComp_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            bHasChanges = true;
        }

        private void gvOperatingComp_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            gvOperatingCompFocusedRowChanged(e.FocusedRowHandle);
        }

        private void gvOperatingCompFocusedRowChanged(int iFocusedRowHandle)
        {
            drOpComp = gvOperatingComp.GetDataRow(iFocusedRowHandle);
            if (drOpComp != null)
            {
                sOpCompName = Convert.ToString(drOpComp["OPERATING_COMPANY_NAME"]);
                if (drOpComp["OPERATING_COMPANY_ID"] != DBNull.Value && !string.IsNullOrWhiteSpace(drOpComp["OPERATING_COMPANY_ID"].ToString()))
                    gcIncludedComp.DataSource = new DataView(dtCompList, "OPERATING_COMPANY_ID IS NOT NULL AND OPERATING_COMPANY_ID=" + drOpComp["OPERATING_COMPANY_ID"], "", DataViewRowState.CurrentRows);
                else
                    gcIncludedComp.DataSource = null;
                gcAddComp.DataSource = new DataView(dtCompList, "OPERATING_COMPANY_ID IS NULL", "", DataViewRowState.CurrentRows);

                if (drOpComp.RowState == DataRowState.Added)
                    gcolCode.OptionsColumn.AllowEdit = true;
                else
                    gcolCode.OptionsColumn.AllowEdit = false;

                MakeControlsEnableDisable();
            }
            else
            {
                gcolCode.OptionsColumn.AllowEdit = true;
                gcIncludedComp.DataSource = null;
                gcAddComp.DataSource = null;

                sOpCompName = "";
                btnLink.Enabled = false;
                btnUnlink.Enabled = false;
            }
        }

        private void gvOperatingComp_BeforeLeaveRow(object sender, DevExpress.XtraGrid.Views.Base.RowAllowEventArgs e)
        {
            e.Allow = !DoBeforeClose();
        }

        private void gvOperatingComp_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            drOpComp = gvOperatingComp.GetDataRow(e.RowHandle);
            drOpComp["IS_ACTIVE"] = "Y";
            drOpComp["CREATED_BY"] = ProfitCashflow.oPcfDM.UserName;
            drOpComp["CREATED_ON"] = DateTime.Now;
        }

        private void gvOperatingComp_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void gvOperatingComp_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
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

            drOpComp = gvOperatingComp.GetDataRow(e.RowHandle);
            if (drOpComp == null) return;

            if (drOpComp["OPERATING_COMPANY_CODE"] == DBNull.Value || drOpComp["OPERATING_COMPANY_CODE"].ToString().Trim() == string.Empty)
            {
                XtraMessageBox.Show("Operating Company Code is mandatory.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Valid = false;
                return;
            }
            else if (drOpComp["OPERATING_COMPANY_CODE"] != DBNull.Value && !string.IsNullOrWhiteSpace(drOpComp["OPERATING_COMPANY_CODE"].ToString()))
            {
                DataRow[] drRows = dtOpCompMast.Select("OPERATING_COMPANY_CODE = '" + drOpComp["OPERATING_COMPANY_CODE"] + "'");
                foreach (DataRow drCode in drRows)
                    if (dtOpCompMast.Rows.IndexOf(drCode) != dtOpCompMast.Rows.IndexOf(drOpComp) && drCode.RowState != DataRowState.Deleted)
                        if (Convert.ToString(drCode["OPERATING_COMPANY_CODE"]) == Convert.ToString(drOpComp["OPERATING_COMPANY_CODE"]))
                        {
                            XtraMessageBox.Show("Duplicate Operating Company Code is not allowed.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            e.Valid = false;
                            return;
                        }
            }
            else
                drOpComp["OPERATING_COMPANY_CODE"] = drOpComp["OPERATING_COMPANY_CODE"].ToString().ToUpper();

            if (gvOperatingComp.IsNewItemRow(e.RowHandle) || (drOpComp.RowState == DataRowState.Modified &&
                Convert.ToString(drOpComp["OPERATING_COMPANY_CODE", DataRowVersion.Original]) != Convert.ToString(drOpComp["OPERATING_COMPANY_CODE", DataRowVersion.Current])))
            {
                object oResult = oOpCompMast.RetrieveDuplicateOpCompCount(Convert.ToString(drOpComp["OPERATING_COMPANY_CODE"]));
                if (oResult != null && Convert.ToDecimal(oResult) > 0)
                {
                    XtraMessageBox.Show("Duplicate Operating Company Code is not allowed.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Valid = false;
                    return;
                }
            }

            if (drOpComp["OPERATING_COMPANY_NAME"] == DBNull.Value || drOpComp["OPERATING_COMPANY_NAME"].ToString().Trim() == string.Empty)
            {
                XtraMessageBox.Show("Operating Company Name is mandatory.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Valid = false;
                return;
            }

            if (Convert.ToString(drOpComp["IS_ACTIVE"]) == "N" && dtCompList.Select("OPERATING_COMPANY_ID IS NOT NULL AND OPERATING_COMPANY_ID=" + drOpComp["OPERATING_COMPANY_ID"]).Length > 0)
            {
                XtraMessageBox.Show("An Operating Company with Included Companies cannot be deactivated. Please remove Included Companies and then deactivate.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Valid = false;
                return;
            }

            if (Convert.ToString(drOpComp["IS_ACTIVE"]) == "N" && dtOpCompMast.Select("IS_ACTIVE='Y' AND OPERATING_COMPANY_CODE <> '" + drOpComp["OPERATING_COMPANY_CODE"] + "'").Length == 0)
            {
                XtraMessageBox.Show("At least one Operating Company must remain Active.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Valid = false;
                return;
            }

            if (!string.IsNullOrEmpty(sOpCompName) && sOpCompName != Convert.ToString(drOpComp["OPERATING_COMPANY_NAME"]))
            {
                DialogResult oResult = XtraMessageBox.Show("Are you sure, you want to update Operating Company name?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (oResult == System.Windows.Forms.DialogResult.No)
                {
                    e.Valid = false;
                    return;
                }
            }

            drOpComp["CREATED_BY"] = ProfitCashflow.oPcfDM.UserName;
            drOpComp["LAST_UPDATED"] = DateTime.Now;
        }

        private void gvOperatingComp_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            try
            {
                if (dtOpCompMast.GetChanges() != null)
                {
                    oOpCompMast.UpdateOperatingCompMaster(dtOpCompMast.GetChanges());
                    dtOpCompMast.AcceptChanges();

                    int iPrevHandle = gvOperatingComp.FocusedRowHandle;
                    dtOpCompMast = oOpCompMast.RetrieveOperatingCompanyMaster();
                    gcOperatingComp.DataSource = dtOpCompMast;

                    if (iPrevHandle == gvOperatingComp.FocusedRowHandle)
                        gvOperatingCompFocusedRowChanged(gvOperatingComp.FocusedRowHandle);
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
            if (drOpComp == null) return;

            DataRow[] drLinks = dtCompList.Select("Select='Y' AND OPERATING_COMPANY_ID IS NULL");
            if (drLinks.Length > 0)
            {
                gvIncludedComp.BeginUpdate();
                foreach (DataRow drLink in drLinks)
                    drLink["OPERATING_COMPANY_ID"] = drOpComp["OPERATING_COMPANY_ID"];

                try
                {
                    if (dtCompList.GetChanges() != null)
                    {
                        oOpCompMast.UpdateOperatingCompAndCompLinkage(dtCompList.GetChanges());
                        foreach (DataRow drTemp in dtCompList.Select("Select='Y'"))
                            drTemp["Select"] = "N";
                        dtCompList.AcceptChanges();
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dtCompList.RejectChanges();
                }

                gvIncludedComp.EndUpdate();

                gcIncludedComp.DataSource = new DataView(dtCompList, "OPERATING_COMPANY_ID IS NOT NULL AND OPERATING_COMPANY_ID=" + drOpComp["OPERATING_COMPANY_ID"], "", DataViewRowState.CurrentRows);
                gcAddComp.DataSource = new DataView(dtCompList, "OPERATING_COMPANY_ID IS NULL", "", DataViewRowState.CurrentRows);
                gvOperatingComp.Focus();

                MakeControlsEnableDisable();
            }
        }

        private void btnUnlink_Click(object sender, EventArgs e)
        {
            if (drOpComp == null) return;

            DataRow[] drUnlinks = dtCompList.Select("Select='Y' AND OPERATING_COMPANY_ID IS NOT NULL");
            if (drUnlinks.Length > 0)
            {
                foreach (DataRow drUnlink in drUnlinks)
                    drUnlink["OPERATING_COMPANY_ID"] = DBNull.Value;

                try
                {
                    if (dtCompList.GetChanges() != null)
                    {
                        oOpCompMast.UpdateOperatingCompAndCompLinkage(dtCompList.GetChanges());
                        foreach (DataRow drTemp in dtCompList.Select("Select='Y'"))
                            drTemp["Select"] = "N";
                        dtCompList.AcceptChanges();

                        gcIncludedComp.DataSource = new DataView(dtCompList, "OPERATING_COMPANY_ID IS NOT NULL AND OPERATING_COMPANY_ID=" + drOpComp["OPERATING_COMPANY_ID"], "", DataViewRowState.CurrentRows);
                        gcAddComp.DataSource = new DataView(dtCompList, "OPERATING_COMPANY_ID IS NULL", "", DataViewRowState.CurrentRows);
                        gvOperatingComp.Focus();

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
            if (dtCompList.Select("Select='Y' AND OPERATING_COMPANY_ID IS NOT NULL").Length > 0)
            {
                gvIncludedComp.OptionsBehavior.Editable = true;
                gvAddComp.OptionsBehavior.Editable = false;
                btnLink.Enabled = false;
                btnUnlink.Enabled = true;
            }
            else if(dtCompList.Select("Select='Y' AND OPERATING_COMPANY_ID IS NULL").Length > 0)
            {
                gvIncludedComp.OptionsBehavior.Editable = false;
                gvAddComp.OptionsBehavior.Editable = true;
                btnLink.Enabled = true;
                btnUnlink.Enabled = false;
            }
            else
            {
                gvIncludedComp.OptionsBehavior.Editable = true;
                gvAddComp.OptionsBehavior.Editable = true;
                btnLink.Enabled = false;
                btnUnlink.Enabled = false;
            }
        }

        private void gvCompLinkage_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.RowHandle < 0) return;

            if (e.Column == gcolASelect && e.Value != null && e.Value.ToString() == "Y")
            {
                if (drOpComp != null && drOpComp["IS_ACTIVE"].ToString() != "Y")
                {
                    XtraMessageBox.Show("Companies cannot be linked for an inactive Operating Company.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    gvAddComp.SetFocusedRowCellValue(gcolASelect, gvAddComp.GetDataRow(e.RowHandle)["Select"]);
                    return;
                }

                try
                {
                    oComm.ValidateRelationCompRegOpComp(gvAddComp.GetDataRow(e.RowHandle)["COMPANY_ID"], null, drOpComp["OPERATING_COMPANY_ID"]);
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    gvAddComp.SetFocusedRowCellValue(gcolASelect, gvAddComp.GetDataRow(e.RowHandle)["Select"]);
                    return;
                }
            }

            if (e.Column == gcolASelect)
                gvAddComp.GetDataRow(e.RowHandle)["Select"] = (e.Value != null && e.Value.ToString() == "Y") ? "Y" : "N";
            if (e.Column == gcolISelect)
                gvIncludedComp.GetDataRow(e.RowHandle)["Select"] = (e.Value != null && e.Value.ToString() == "Y") ? "Y" : "N";

            MakeControlsEnableDisable();
        }
    }
}