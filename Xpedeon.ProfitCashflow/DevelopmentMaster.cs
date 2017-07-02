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
    public partial class DevelopmentMaster : DevExpress.XtraEditors.XtraForm
    {
        sDevelopmentMaster oDevMaster = new sDevelopmentMaster();
        DataTable dtDevMaster, dtSiteList;
        DataRow drDevMast;
        string sDevName = "";
        bool bHasChanges = false, bIsFrmClosing = false;

        public DevelopmentMaster()
        {
            InitializeComponent();
        }

        private void DevelopmentMaster_Load(object sender, EventArgs e)
        {
            try
            {
                dtDevMaster = oDevMaster.RetrieveDevelopmentMaster();
                dtDevMaster.Constraints.Add("PK_DEV", dtDevMaster.Columns["DEVELOPMENT_CODE"], true);

                dtSiteList = oDevMaster.RetrieveSiteList();
                if (dtSiteList != null)
                {
                    DataColumn dcolSelect = new DataColumn("Select", typeof(string));
                    dcolSelect.DefaultValue = "N";
                    dtSiteList.Columns.Add(dcolSelect);
                }
                dtSiteList.Constraints.Add("PK_SITE", dtSiteList.Columns["SITE_ID"], true);

                gvDevMaster.BeginSort();
                gvDevMaster.EndSort();
                gvIncludedSite.BeginSort();
                gvIncludedSite.EndSort();
                gvAddSite.BeginSort();
                gvAddSite.EndSort();

                gvDevMaster.Focus();
                gcDevMaster.DataSource = dtDevMaster;

                btnLink.Enabled = false;
                btnUnlink.Enabled = false;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DevelopmentMaster_FormClosing(object sender, FormClosingEventArgs e)
        {
            bIsFrmClosing = true;
            e.Cancel = false;
            
            if (bHasChanges)
            {
                DialogResult oTempResult = XtraMessageBox.Show("Data has been changed, do you want to save the changes?", Application.ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (oTempResult == DialogResult.Yes)
                {
                    if (!gvDevMaster.PostEditor())
                        e.Cancel = true;
                    if (!gvDevMaster.UpdateCurrentRow())
                        e.Cancel = true;
                }
                else if (oTempResult == DialogResult.No)
                {
                    gvDevMaster.CancelUpdateCurrentRow();
                    dtDevMaster.RejectChanges();
                    bHasChanges = false;
                }
                else
                    e.Cancel = true;
            }

            bIsFrmClosing = false;
            if (e.Cancel) return;

            gvIncludedSite.PostEditor();
            gvAddSite.PostEditor();
            e.Cancel = DoBeforeClose();
        }

        private void DevelopmentMaster_KeyUp(object sender, KeyEventArgs e)
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
                if (drDevMast != null && dtSiteList.Select("Select='Y' AND DEVELOPMENT_ID IS NOT NULL").Length > 0)
                {
                    DialogResult oTempResult = XtraMessageBox.Show("Some of the Included Sites are selected for removal. Do you want to save it?", Application.ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (oTempResult == DialogResult.Yes)
                        btnUnlink_Click(new object(), new EventArgs());
                    else if (oTempResult == DialogResult.No)
                        dtSiteList.RejectChanges();
                    else
                        return true;
                }
                if (drDevMast != null && dtSiteList.Select("Select='Y' AND DEVELOPMENT_ID IS NULL").Length > 0)
                {
                    DialogResult oTempResult = XtraMessageBox.Show("Some of the Sites are selected for inclusion. Do you want to save it?", Application.ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (oTempResult == DialogResult.Yes)
                        btnLink_Click(new object(), new EventArgs());
                    else if (oTempResult == DialogResult.No)
                        dtSiteList.RejectChanges();
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

        private void gvDevMaster_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            bHasChanges = true;
        }

        private void gvDevMaster_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            gvDevMasterFocusedRowChanged(e.FocusedRowHandle);
        }

        private void gvDevMasterFocusedRowChanged(int iFocusedRowHandle)
        {
            drDevMast = gvDevMaster.GetDataRow(iFocusedRowHandle);
            if (drDevMast != null)
            {
                sDevName = Convert.ToString(drDevMast["DEVELOPMENT_NAME"]);
                if (drDevMast["DEVELOPMENT_ID"] != DBNull.Value && !string.IsNullOrWhiteSpace(drDevMast["DEVELOPMENT_ID"].ToString()))
                    gcIncludedSite.DataSource = new DataView(dtSiteList, "DEVELOPMENT_ID IS NOT NULL AND DEVELOPMENT_ID=" + drDevMast["DEVELOPMENT_ID"], "", DataViewRowState.CurrentRows);
                else
                    gcIncludedSite.DataSource = null;
                gcAddSite.DataSource = new DataView(dtSiteList, "DEVELOPMENT_ID IS NULL", "", DataViewRowState.CurrentRows);

                if (drDevMast.RowState == DataRowState.Added)
                    gcolCode.OptionsColumn.AllowEdit = true;
                else
                    gcolCode.OptionsColumn.AllowEdit = false;

                MakeControlsEnableDisable();
            }
            else
            {
                gcolCode.OptionsColumn.AllowEdit = true;
                gcIncludedSite.DataSource = null;
                gcAddSite.DataSource = null;

                sDevName = "";
                btnLink.Enabled = false;
                btnUnlink.Enabled = false;
            }
        }

        private void gvDevMaster_BeforeLeaveRow(object sender, DevExpress.XtraGrid.Views.Base.RowAllowEventArgs e)
        {
            e.Allow = !DoBeforeClose();
        }

        private void gvDevMaster_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            drDevMast = gvDevMaster.GetDataRow(e.RowHandle);
            drDevMast["IS_ACTIVE"] = "Y";
            drDevMast["CREATED_BY"] = ProfitCashflow.oPcfDM.UserName;
            drDevMast["CREATED_ON"] = DateTime.Now;
        }

        private void gvDevMaster_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void gvDevMaster_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
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

            drDevMast = gvDevMaster.GetDataRow(e.RowHandle);
            if (drDevMast == null) return;

            if (drDevMast["DEVELOPMENT_CODE"] == DBNull.Value || drDevMast["DEVELOPMENT_CODE"].ToString().Trim() == string.Empty)
            {
                XtraMessageBox.Show("Development Code is mandatory.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Valid = false;
                return;
            }
            else if (drDevMast["DEVELOPMENT_CODE"] != DBNull.Value && !string.IsNullOrWhiteSpace(drDevMast["DEVELOPMENT_CODE"].ToString()))
            {
                DataRow[] drRows = dtDevMaster.Select("DEVELOPMENT_CODE = '" + drDevMast["DEVELOPMENT_CODE"] + "'");
                foreach (DataRow drCode in drRows)
                    if (dtDevMaster.Rows.IndexOf(drCode) != dtDevMaster.Rows.IndexOf(drDevMast) && drCode.RowState != DataRowState.Deleted)
                        if (Convert.ToString(drCode["DEVELOPMENT_CODE"]) == Convert.ToString(drDevMast["DEVELOPMENT_CODE"]))
                        {
                            XtraMessageBox.Show("Duplicate Development Code is not allowed.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            e.Valid = false;
                            return;
                        }
            }
            else
                drDevMast["DEVELOPMENT_CODE"] = drDevMast["DEVELOPMENT_CODE"].ToString().ToUpper();

            if (gvDevMaster.IsNewItemRow(e.RowHandle) || (drDevMast.RowState == DataRowState.Modified &&
                Convert.ToString(drDevMast["DEVELOPMENT_CODE", DataRowVersion.Original]) != Convert.ToString(drDevMast["DEVELOPMENT_CODE", DataRowVersion.Current])))
            {
                object oResult = oDevMaster.RetrieveDuplicateDevCount(Convert.ToString(drDevMast["DEVELOPMENT_CODE"]));
                if (oResult != null && Convert.ToDecimal(oResult) > 0)
                {
                    XtraMessageBox.Show("Duplicate Development Code is not allowed.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Valid = false;
                    return;
                }
            }

            if (drDevMast["DEVELOPMENT_NAME"] == DBNull.Value || drDevMast["DEVELOPMENT_NAME"].ToString().Trim() == string.Empty)
            {
                XtraMessageBox.Show("Development Name is mandatory.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Valid = false;
                return;
            }

            if (Convert.ToString(drDevMast["IS_ACTIVE"]) == "N" && dtSiteList.Select("DEVELOPMENT_ID IS NOT NULL AND DEVELOPMENT_ID=" + drDevMast["DEVELOPMENT_ID"]).Length > 0)
            {
                XtraMessageBox.Show("A Development with Included Sites cannot be deactivated. Please remove Included Sites and then deactivate.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Valid = false;
                return;
            }

            if (Convert.ToString(drDevMast["IS_ACTIVE"]) == "N" && dtDevMaster.Select("IS_ACTIVE='Y' AND DEVELOPMENT_CODE <> '" + drDevMast["DEVELOPMENT_CODE"] + "'").Length == 0)
            {
                XtraMessageBox.Show("At least one Development must remain Active.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Valid = false;
                return;
            }

            if (!string.IsNullOrEmpty(sDevName) && sDevName != Convert.ToString(drDevMast["DEVELOPMENT_NAME"]))
            {
                DialogResult oResult = XtraMessageBox.Show("Are you sure, you want to update Development name?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (oResult == System.Windows.Forms.DialogResult.No)
                {
                    e.Valid = false;
                    return;
                }
            }

            drDevMast["CREATED_BY"] = ProfitCashflow.oPcfDM.UserName;
            drDevMast["LAST_UPDATED"] = DateTime.Now;
        }

        private void gvDevMaster_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            try
            {
                if (dtDevMaster.GetChanges() != null)
                {
                    oDevMaster.UpdateDevelopmentMaster(dtDevMaster.GetChanges());
                    dtDevMaster.AcceptChanges();

                    int iPrevHandle = gvDevMaster.FocusedRowHandle;
                    dtDevMaster = oDevMaster.RetrieveDevelopmentMaster();
                    gcDevMaster.DataSource = dtDevMaster;

                    if (iPrevHandle == gvDevMaster.FocusedRowHandle)
                        gvDevMasterFocusedRowChanged(gvDevMaster.FocusedRowHandle);
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
            if (drDevMast == null) return;

            DataRow[] drLinks = dtSiteList.Select("Select='Y' AND DEVELOPMENT_ID IS NULL");
            if (drLinks.Length > 0)
            {
                gvIncludedSite.BeginUpdate();
                foreach (DataRow drLink in drLinks)
                    drLink["DEVELOPMENT_ID"] = drDevMast["DEVELOPMENT_ID"];

                try
                {
                    if (dtSiteList.GetChanges() != null)
                    {
                        oDevMaster.UpdateDevelopmentAndSiteLinkage(dtSiteList.GetChanges());
                        foreach (DataRow drTemp in dtSiteList.Select("Select='Y'"))
                            drTemp["Select"] = "N";
                        dtSiteList.AcceptChanges();
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dtSiteList.RejectChanges();
                }

                gvIncludedSite.EndUpdate();

                gcIncludedSite.DataSource = new DataView(dtSiteList, "DEVELOPMENT_ID IS NOT NULL AND DEVELOPMENT_ID=" + drDevMast["DEVELOPMENT_ID"], "", DataViewRowState.CurrentRows);
                gcAddSite.DataSource = new DataView(dtSiteList, "DEVELOPMENT_ID IS NULL", "", DataViewRowState.CurrentRows);
                gvDevMaster.Focus();

                MakeControlsEnableDisable();
            }
        }

        private void btnUnlink_Click(object sender, EventArgs e)
        {
            if (drDevMast == null) return;

            DataRow[] drUnlinks = dtSiteList.Select("Select='Y' AND DEVELOPMENT_ID IS NOT NULL");
            if (drUnlinks.Length > 0)
            {
                foreach (DataRow drUnlink in drUnlinks)
                    drUnlink["DEVELOPMENT_ID"] = DBNull.Value;

                try
                {
                    if (dtSiteList.GetChanges() != null)
                    {
                        oDevMaster.UpdateDevelopmentAndSiteLinkage(dtSiteList.GetChanges());
                        foreach (DataRow drTemp in dtSiteList.Select("Select='Y'"))
                            drTemp["Select"] = "N";
                        dtSiteList.AcceptChanges();

                        gcIncludedSite.DataSource = new DataView(dtSiteList, "DEVELOPMENT_ID IS NOT NULL AND DEVELOPMENT_ID=" + drDevMast["DEVELOPMENT_ID"], "", DataViewRowState.CurrentRows);
                        gcAddSite.DataSource = new DataView(dtSiteList, "DEVELOPMENT_ID IS NULL", "", DataViewRowState.CurrentRows);
                        gvDevMaster.Focus();

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
            if (dtSiteList.Select("Select='Y' AND DEVELOPMENT_ID IS NOT NULL").Length > 0)
            {
                gvIncludedSite.OptionsBehavior.Editable = true;
                gvAddSite.OptionsBehavior.Editable = false;
                btnLink.Enabled = false;
                btnUnlink.Enabled = true;
            }
            else if(dtSiteList.Select("Select='Y' AND DEVELOPMENT_ID IS NULL").Length > 0)
            {
                gvIncludedSite.OptionsBehavior.Editable = false;
                gvAddSite.OptionsBehavior.Editable = true;
                btnLink.Enabled = true;
                btnUnlink.Enabled = false;
            }
            else
            {
                gvIncludedSite.OptionsBehavior.Editable = true;
                gvAddSite.OptionsBehavior.Editable = true;
                btnLink.Enabled = false;
                btnUnlink.Enabled = false;
            }
        }

        private void gvLinkage_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.RowHandle < 0) return;

            if (e.Column == gcolASelect && e.Value != null && e.Value.ToString() == "Y" && drDevMast != null && drDevMast["IS_ACTIVE"].ToString() != "Y")
            {
                XtraMessageBox.Show("Sites cannot be linked for an inactive Development.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                gvAddSite.SetFocusedRowCellValue(gcolASelect, gvAddSite.GetDataRow(e.RowHandle)["Select"]);
                return;
            }

            if (e.Column == gcolASelect)
                gvAddSite.GetDataRow(e.RowHandle)["Select"] = (e.Value != null && e.Value.ToString() == "Y") ? "Y" : "N";
            if (e.Column == gcolISelect)
                gvIncludedSite.GetDataRow(e.RowHandle)["Select"] = (e.Value != null && e.Value.ToString() == "Y") ? "Y" : "N";

            MakeControlsEnableDisable();
        }
    }
}