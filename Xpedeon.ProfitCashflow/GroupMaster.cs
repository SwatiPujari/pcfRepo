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
    public partial class GroupMaster : DevExpress.XtraEditors.XtraForm
    {
        sGroupMaster oGroupMaster = new sGroupMaster();
        sCommonMethods oComm = new sCommonMethods();

        DataTable dtGroupMaster, dtDivisionList, dtCompanyList;
        DataRow drGroup;
        string sGroupName = "";
        bool bHasChanges = false, bIsFrmClosing = false;

        public GroupMaster()
        {
            InitializeComponent();
        }

        private void GroupMaster_Load(object sender, EventArgs e)
        {
            try
            {
                dtGroupMaster = oGroupMaster.RetrieveGroupMaster();
                dtGroupMaster.Constraints.Add("PK_GROUP", dtGroupMaster.Columns["GROUP_CODE"], true);

                dtDivisionList = oGroupMaster.RetrieveDivisionList();
                if (dtDivisionList != null)
                {
                    DataColumn dcolSelect = new DataColumn("Select", typeof(string));
                    dcolSelect.DefaultValue = "N";
                    dtDivisionList.Columns.Add(dcolSelect);
                }
                dtDivisionList.Constraints.Add("PK_DIVISION", dtDivisionList.Columns["DIVISION_ID"], true);

                dtCompanyList = oComm.RetrieveCompanyList(ProfitCashflow.oPcfDM.UserName);
                dtCompanyList.Constraints.Add("PK_COMPANY", dtCompanyList.Columns["COMPANY_ID"], true);
                repLookUpComp.DataSource = dtCompanyList;

                gvGroupMaster.BeginSort();
                gvGroupMaster.EndSort();
                gvIncludedDivision.BeginSort();
                gvIncludedDivision.EndSort();
                gvAddDivision.BeginSort();
                gvAddDivision.EndSort();

                gvGroupMaster.Focus();
                gcGroupMaster.DataSource = dtGroupMaster;

                btnLink.Enabled = false;
                btnUnlink.Enabled = false;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GroupMaster_FormClosing(object sender, FormClosingEventArgs e)
        {
            bIsFrmClosing = true;
            e.Cancel = false;
            
            if (bHasChanges)
            {
                DialogResult oTempResult = XtraMessageBox.Show("Data has been changed, do you want to save the changes?", Application.ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (oTempResult == DialogResult.Yes)
                {
                    if (!gvGroupMaster.PostEditor())
                        e.Cancel = true;
                    if (!gvGroupMaster.UpdateCurrentRow())
                        e.Cancel = true;
                }
                else if (oTempResult == DialogResult.No)
                {
                    gvGroupMaster.CancelUpdateCurrentRow();
                    dtGroupMaster.RejectChanges();
                    bHasChanges = false;
                }
                else
                    e.Cancel = true;
            }

            bIsFrmClosing = false;
            if (e.Cancel) return;

            gvIncludedDivision.PostEditor();
            gvAddDivision.PostEditor();
            e.Cancel = DoBeforeClose();
        }

        private void GroupMaster_KeyUp(object sender, KeyEventArgs e)
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
                if (drGroup != null && dtDivisionList.Select("Select='Y' AND GROUP_ID IS NOT NULL").Length > 0)
                {
                    DialogResult oTempResult = XtraMessageBox.Show("Some of the Included Divisions are selected for removal. Do you want to save it?", Application.ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (oTempResult == DialogResult.Yes)
                        btnUnlink_Click(new object(), new EventArgs());
                    else if (oTempResult == DialogResult.No)
                        dtDivisionList.RejectChanges();
                    else
                        return true;
                }
                if (drGroup != null && dtDivisionList.Select("Select='Y' AND GROUP_ID IS NULL").Length > 0)
                {
                    DialogResult oTempResult = XtraMessageBox.Show("Some of the Divisions are selected for inclusion. Do you want to save it?", Application.ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (oTempResult == DialogResult.Yes)
                        btnLink_Click(new object(), new EventArgs());
                    else if (oTempResult == DialogResult.No)
                        dtDivisionList.RejectChanges();
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

        private void gvGroupMaster_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            bHasChanges = true;
        }

        private void gvGroupMaster_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            gvGroupMasterFocusedRowChanged(e.FocusedRowHandle);
        }

        private void gvGroupMasterFocusedRowChanged(int iFocusedRowHandle)
        {
            drGroup = gvGroupMaster.GetDataRow(iFocusedRowHandle);            
            if (drGroup != null)
            {
                sGroupName = Convert.ToString(drGroup["GROUP_NAME"]);
                if (drGroup["GROUP_ID"] != DBNull.Value && !string.IsNullOrWhiteSpace(drGroup["GROUP_ID"].ToString()))
                    gcIncludedDivision.DataSource = new DataView(dtDivisionList, "GROUP_ID IS NOT NULL AND GROUP_ID=" + drGroup["GROUP_ID"], "", DataViewRowState.CurrentRows);
                else
                    gcIncludedDivision.DataSource = null;
                gcAddDivision.DataSource = new DataView(dtDivisionList, "GROUP_ID IS NULL", "", DataViewRowState.CurrentRows);

                if (drGroup.RowState == DataRowState.Added)
                    gcolCode.OptionsColumn.AllowEdit = true;
                else
                    gcolCode.OptionsColumn.AllowEdit = false;

                MakeControlsEnableDisable();
            }
            else
            {
                gcolCode.OptionsColumn.AllowEdit = true;
                gcIncludedDivision.DataSource = null;
                gcAddDivision.DataSource = null;

                sGroupName = "";
                btnLink.Enabled = false;
                btnUnlink.Enabled = false;
            }
        }

        private void gvGroupMaster_BeforeLeaveRow(object sender, DevExpress.XtraGrid.Views.Base.RowAllowEventArgs e)
        {
            e.Allow = !DoBeforeClose();
        }

        private void gvGroupMaster_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            drGroup = gvGroupMaster.GetDataRow(e.RowHandle);
            drGroup["IS_ACTIVE"] = "Y";
            drGroup["CREATED_BY"] = ProfitCashflow.oPcfDM.UserName;
            drGroup["CREATED_ON"] = DateTime.Now;
        }

        private void gvGroupMaster_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void gvGroupMaster_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
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

            drGroup = gvGroupMaster.GetDataRow(e.RowHandle);
            if (drGroup == null) return;

            if (drGroup["GROUP_CODE"] == DBNull.Value || drGroup["GROUP_CODE"].ToString().Trim() == string.Empty)
            {
                XtraMessageBox.Show("Group Code is mandatory.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Valid = false;
                return;
            }
            else if (drGroup["GROUP_CODE"] != DBNull.Value && !string.IsNullOrWhiteSpace(drGroup["GROUP_CODE"].ToString()))
            {
                DataRow[] drRows = dtGroupMaster.Select("GROUP_CODE = '" + drGroup["GROUP_CODE"] + "'");
                foreach (DataRow drCode in drRows)
                    if (dtGroupMaster.Rows.IndexOf(drCode) != dtGroupMaster.Rows.IndexOf(drGroup) && drCode.RowState != DataRowState.Deleted)
                        if (Convert.ToString(drCode["GROUP_CODE"]) == Convert.ToString(drGroup["GROUP_CODE"]))
                        {
                            XtraMessageBox.Show("Duplicate Group Code is not allowed.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            e.Valid = false;
                            return;
                        }
            }
            else
                drGroup["GROUP_CODE"] = drGroup["GROUP_CODE"].ToString().ToUpper();

            if (gvGroupMaster.IsNewItemRow(e.RowHandle) || (drGroup.RowState == DataRowState.Modified &&
                Convert.ToString(drGroup["GROUP_CODE", DataRowVersion.Original]) != Convert.ToString(drGroup["GROUP_CODE", DataRowVersion.Current])))
            {
                object oResult = oGroupMaster.RetrieveDuplicateGroupCount(Convert.ToString(drGroup["GROUP_CODE"]));
                if (oResult != null && Convert.ToDecimal(oResult) > 0)
                {
                    XtraMessageBox.Show("Duplicate Group Code is not allowed.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Valid = false;
                    return;
                }
            }

            if (drGroup["GROUP_NAME"] == DBNull.Value || drGroup["GROUP_NAME"].ToString().Trim() == string.Empty)
            {
                XtraMessageBox.Show("Group Name is mandatory.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Valid = false;
                return;
            }

            if (Convert.ToString(drGroup["IS_ACTIVE"]) == "N" && dtDivisionList.Select("GROUP_ID IS NOT NULL AND GROUP_ID=" + drGroup["GROUP_ID"]).Length > 0)
            {
                XtraMessageBox.Show("A Group with Included Divisions cannot be deactivated. Please remove Included Divisions and then deactivate.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Valid = false;
                return;
            }

            if (Convert.ToString(drGroup["IS_ACTIVE"]) == "N" && dtGroupMaster.Select("IS_ACTIVE='Y' AND GROUP_CODE <> '" + drGroup["GROUP_CODE"] + "'").Length == 0)
            {
                XtraMessageBox.Show("At least one Group must remain Active.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Valid = false;
                return;
            }

            /*if (dtGroupMaster.Rows.Count > 0)
				if(dtGroupMaster.Select("IS_DEFAULT='Y'").Length == 0)
				{
					XtraMessageBox.Show("Atleast one Group should be default.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
					e.Valid = false;
					return;
				}
				else if(dtGroupMaster.Select("IS_DEFAULT='Y'").Length > 1)
				{
					XtraMessageBox.Show("Only one Group should be default.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
					e.Valid = false;
					return;
				}*/

            if (drGroup["COMPANY_ID"] != DBNull.Value && !string.IsNullOrWhiteSpace(drGroup["COMPANY_ID"].ToString()) && dtGroupMaster.Rows.Count > 0)
                if (dtGroupMaster.Select("COMPANY_ID = " + drGroup["COMPANY_ID"] + " AND GROUP_CODE <> '" + drGroup["GROUP_CODE"] + "'").Length > 0)
                {
                    XtraMessageBox.Show("Company has already been linked to some other Group.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Valid = false;
                    return;
                }

			if (gvGroupMaster.IsNewItemRow(e.RowHandle))
			{
                DialogResult oResult = XtraMessageBox.Show("To add Group, you should get the Authorisation from the CIO. Are you sure, you want to add Group?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (oResult == System.Windows.Forms.DialogResult.No)
                {
                    e.Valid = false;
                    return;
                }
            }

            if (!string.IsNullOrEmpty(sGroupName) && sGroupName != Convert.ToString(drGroup["GROUP_NAME"]))
            {
                DialogResult oResult = XtraMessageBox.Show("To update Group Name, you should get the Authorisation from the CIO. Are you sure, you want to update Group name?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (oResult == System.Windows.Forms.DialogResult.No)
                {
                    e.Valid = false;
                    return;
                }
            }

            drGroup["CREATED_BY"] = ProfitCashflow.oPcfDM.UserName;
            drGroup["LAST_UPDATED"] = DateTime.Now;
        }

        private void gvGroupMaster_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            try
            {
                if (dtGroupMaster.GetChanges() != null)
                {
                    oGroupMaster.UpdateGroupMaster(dtGroupMaster.GetChanges());
                    dtGroupMaster.AcceptChanges();

                    int iPrevHandle = gvGroupMaster.FocusedRowHandle;
                    dtGroupMaster = oGroupMaster.RetrieveGroupMaster();
                    gcGroupMaster.DataSource = dtGroupMaster;

                    if (iPrevHandle == gvGroupMaster.FocusedRowHandle)
                        gvGroupMasterFocusedRowChanged(gvGroupMaster.FocusedRowHandle);
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
            if (drGroup == null) return;

            DataRow[] drLinks = dtDivisionList.Select("Select='Y' AND GROUP_ID IS NULL");
            if (drLinks.Length > 0)
            {
                gvIncludedDivision.BeginUpdate();
                foreach (DataRow drLink in drLinks)
                    drLink["GROUP_ID"] = drGroup["GROUP_ID"];

                try
                {
                    if (dtDivisionList.GetChanges() != null)
                    {
                        oGroupMaster.UpdateGroupAndDivisionLinkage(dtDivisionList.GetChanges());
                        foreach (DataRow drTemp in dtDivisionList.Select("Select='Y'"))
                            drTemp["Select"] = "N";
                        dtDivisionList.AcceptChanges();
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dtDivisionList.RejectChanges();
                }

                gvIncludedDivision.EndUpdate();

                gcIncludedDivision.DataSource = new DataView(dtDivisionList, "GROUP_ID IS NOT NULL AND GROUP_ID=" + drGroup["GROUP_ID"], "", DataViewRowState.CurrentRows);
                gcAddDivision.DataSource = new DataView(dtDivisionList, "GROUP_ID IS NULL", "", DataViewRowState.CurrentRows);
                gvGroupMaster.Focus();

                MakeControlsEnableDisable();
            }
        }

        private void btnUnlink_Click(object sender, EventArgs e)
        {
            if (drGroup == null) return;

            DataRow[] drUnlinks = dtDivisionList.Select("Select='Y' AND GROUP_ID IS NOT NULL");
            if (drUnlinks.Length > 0)
            {
                foreach (DataRow drUnlink in drUnlinks)
                    drUnlink["GROUP_ID"] = DBNull.Value;

                try
                {
                    if (dtDivisionList.GetChanges() != null)
                    {
                        oGroupMaster.UpdateGroupAndDivisionLinkage(dtDivisionList.GetChanges());
                        foreach (DataRow drTemp in dtDivisionList.Select("Select='Y'"))
                            drTemp["Select"] = "N";
                        dtDivisionList.AcceptChanges();

                        gcIncludedDivision.DataSource = new DataView(dtDivisionList, "GROUP_ID IS NOT NULL AND GROUP_ID=" + drGroup["GROUP_ID"], "", DataViewRowState.CurrentRows);
                        gcAddDivision.DataSource = new DataView(dtDivisionList, "GROUP_ID IS NULL", "", DataViewRowState.CurrentRows);
                        gvGroupMaster.Focus();

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
            if (dtDivisionList.Select("Select='Y' AND GROUP_ID IS NOT NULL").Length > 0)
            {
                gvIncludedDivision.OptionsBehavior.Editable = true;
                gvAddDivision.OptionsBehavior.Editable = false;
                btnLink.Enabled = false;
                btnUnlink.Enabled = true;
            }
            else if(dtDivisionList.Select("Select='Y' AND GROUP_ID IS NULL").Length > 0)
            {
                gvIncludedDivision.OptionsBehavior.Editable = false;
                gvAddDivision.OptionsBehavior.Editable = true;
                btnLink.Enabled = true;
                btnUnlink.Enabled = false;
            }
            else
            {
                gvIncludedDivision.OptionsBehavior.Editable = true;
                gvAddDivision.OptionsBehavior.Editable = true;
                btnLink.Enabled = false;
                btnUnlink.Enabled = false;
            }
        }

        private void gvLinkage_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.RowHandle < 0) return;

            if (e.Column == gcolASelect && e.Value != null && e.Value.ToString() == "Y" && drGroup != null && drGroup["IS_ACTIVE"].ToString() != "Y")
            {
                XtraMessageBox.Show("Divisions cannot be linked for an inactive Group.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                gvAddDivision.SetFocusedRowCellValue(gcolASelect, gvAddDivision.GetDataRow(e.RowHandle)["Select"]);
                return;
            }

            if (e.Column == gcolASelect)
                gvAddDivision.GetDataRow(e.RowHandle)["Select"] = (e.Value != null && e.Value.ToString() == "Y") ? "Y" : "N";
            if (e.Column == gcolISelect)
                gvIncludedDivision.GetDataRow(e.RowHandle)["Select"] = (e.Value != null && e.Value.ToString() == "Y") ? "Y" : "N";

            MakeControlsEnableDisable();
        }

        private void repLookUpComp_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
                gvGroupMaster.SetFocusedRowCellValue(gcolComp, DBNull.Value);
        }

        private void LookUp_MouseWheel(object sender, MouseEventArgs e)
        {
            if (!((LookUpEdit)sender).IsPopupOpen)
                DevExpress.Utils.DXMouseEventArgs.GetMouseArgs(e).Handled = true;
        }
    }
}