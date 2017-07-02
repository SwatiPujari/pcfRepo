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
    public partial class RegionMaster : DevExpress.XtraEditors.XtraForm
    {
        sRegionMaster oRegionMaster = new sRegionMaster();
        sCommonMethods oComm = new sCommonMethods();

        DataTable dtRegionMaster, dtCompList4Reg, dtOpCompList4Reg, dtCompanyList, dtLinkComp;
        DataRow drRegion;
        string sRegionName = "";
        bool bHasChanges = false, bIsFrmClosing = false;

        public RegionMaster()
        {
            InitializeComponent();
        }

        private void RegionMaster_Load(object sender, EventArgs e)
        {
            try
            {
                dtRegionMaster = oRegionMaster.RetrieveRegionMaster();
                dtRegionMaster.Constraints.Add("PK_REG", dtRegionMaster.Columns["REGION_CODE"], true);

                dtCompList4Reg = oRegionMaster.RetrieveCompanyListForRegion(ProfitCashflow.oPcfDM.UserName);
                if (dtCompList4Reg != null)
                {
                    DataColumn dcolSelect = new DataColumn("Select", typeof(string));
                    dcolSelect.DefaultValue = "N";
                    dtCompList4Reg.Columns.Add(dcolSelect);
                }

                DataView dvLinkComp = new DataView(dtCompList4Reg, "REGION_ID IS NOT NULL", "", DataViewRowState.CurrentRows);
                dtLinkComp = dvLinkComp.ToTable(true, new string[] { "COMPANY_ID", "REGION_ID" });
                dtLinkComp.Constraints.Add("PK_LINK", new DataColumn[] { dtLinkComp.Columns["COMPANY_ID"], dtLinkComp.Columns["REGION_ID"] }, true);
                dtLinkComp.AcceptChanges();

                dtOpCompList4Reg = oRegionMaster.RetrieveOpCompListForRegion();
                if (dtOpCompList4Reg != null)
                {
                    DataColumn dcolSelect = new DataColumn("Select", typeof(string));
                    dcolSelect.DefaultValue = "N";
                    dtOpCompList4Reg.Columns.Add(dcolSelect);
                }

                dtCompanyList = oComm.RetrieveCompanyList(ProfitCashflow.oPcfDM.UserName);
                dtCompanyList.Constraints.Add("PK_COMPANY", dtCompanyList.Columns["COMPANY_ID"], true);
                repLookUpComp.DataSource = dtCompanyList;
                
                gvRegionMaster.BeginSort();
                gvRegionMaster.EndSort();

                gvIncludedComp.BeginSort();
                gvIncludedComp.EndSort();
                gvAddComp.BeginSort();
                gvAddComp.EndSort();

                gvIncludedOpComp.BeginSort();
                gvIncludedOpComp.EndSort();
                gvAddOpComp.BeginSort();
                gvAddOpComp.EndSort();

                gvRegionMaster.Focus();
                gcRegionMaster.DataSource = dtRegionMaster;

                btnLinkComp.Enabled = false;
                btnUnlinkComp.Enabled = false;
                btnLinkOpComp.Enabled = false;
                btnUnlinkOpComp.Enabled = false;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RegionMaster_FormClosing(object sender, FormClosingEventArgs e)
        {
            bIsFrmClosing = true;
            e.Cancel = false;
            
            if (bHasChanges)
            {
                DialogResult oTempResult = XtraMessageBox.Show("Data has been changed, do you want to save the changes?", Application.ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (oTempResult == DialogResult.Yes)
                {
                    if (!gvRegionMaster.PostEditor())
                        e.Cancel = true;
                    if (!gvRegionMaster.UpdateCurrentRow())
                        e.Cancel = true;
                }
                else if (oTempResult == DialogResult.No)
                {
                    gvRegionMaster.CancelUpdateCurrentRow();
                    dtRegionMaster.RejectChanges();
                    bHasChanges = false;
                }
                else
                    e.Cancel = true;
            }

            bIsFrmClosing = false;
            if (e.Cancel) return;

            gvIncludedComp.PostEditor();
            gvAddComp.PostEditor();
            e.Cancel = DoBeforeClose("C", ref dtCompList4Reg);
            if (e.Cancel) return;

            gvIncludedOpComp.PostEditor();
            gvAddOpComp.PostEditor();
            e.Cancel = DoBeforeClose("O", ref dtOpCompList4Reg);
        }

        private void RegionMaster_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                bHasChanges = false;
                bIsFrmClosing = false;
            }
        }

        private bool DoBeforeClose(string sType, ref DataTable dtCompList)
        {
            string sToUpdate = "";
            if (sType == "C")
                sToUpdate = "Companies";
            else if (sType == "O")
                sToUpdate = "Operating Companies";

            try
            {
                if (drRegion != null && dtCompList.Select("Select='Y' AND REGION_ID IS NOT NULL").Length > 0)
                {
                    DialogResult oTempResult = XtraMessageBox.Show("Some of the Included " + sToUpdate + " are selected for removal. Do you want to save it?", Application.ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (oTempResult == DialogResult.Yes)
                    {
                        if (sType == "C")
                            LinkUnlinkComp("U");
                        else if (sType == "O")
                            LinkUnlinkOpComp("U");
                    }
                    else if (oTempResult == DialogResult.No)
                        dtCompList.RejectChanges();
                    else
                        return true;
                }
                if (drRegion != null && dtCompList.Select("Select='Y' AND REGION_ID IS NULL").Length > 0)
                {
                    DialogResult oTempResult = XtraMessageBox.Show("Some of the " + sToUpdate + " are selected for inclusion. Do you want to save it?", Application.ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (oTempResult == DialogResult.Yes)
                    {
                        if (sType == "C")
                            LinkUnlinkComp("L");
                        else if (sType == "O")
                            LinkUnlinkOpComp("L");
                    }
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

        private void gvRegionMaster_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            bHasChanges = true;
        }

        private void gvRegionMaster_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            gvRegionMasterFocusedRowChanged(e.FocusedRowHandle);
        }

        private void gvRegionMasterFocusedRowChanged(int iFocusedRowHandle)
        {
            drRegion = gvRegionMaster.GetDataRow(iFocusedRowHandle);
            if (drRegion != null)
            {
                sRegionName = Convert.ToString(drRegion["REGION_NAME"]);

                SetCompGridDataSource();
                /*if (drRegion["REGION_ID"] != DBNull.Value && !string.IsNullOrWhiteSpace(drRegion["REGION_ID"].ToString()))
                    gcIncludedComp.DataSource = new DataView(dtCompList4Reg, "REGION_ID IS NOT NULL AND REGION_ID=" + drRegion["REGION_ID"], "", DataViewRowState.CurrentRows);
                else
                    gcIncludedComp.DataSource = null;
                gcAddComp.DataSource = new DataView(dtCompList4Reg, "REGION_ID IS NULL", "", DataViewRowState.CurrentRows);*/

                if (drRegion["REGION_ID"] != DBNull.Value && !string.IsNullOrWhiteSpace(drRegion["REGION_ID"].ToString()))
                    gcIncludedOpComp.DataSource = new DataView(dtOpCompList4Reg, "REGION_ID IS NOT NULL AND REGION_ID=" + drRegion["REGION_ID"], "", DataViewRowState.CurrentRows);
                else
                    gcIncludedOpComp.DataSource = null;
                gcAddOpComp.DataSource = new DataView(dtOpCompList4Reg, "REGION_ID IS NULL", "", DataViewRowState.CurrentRows);

                if (drRegion.RowState == DataRowState.Added)
                    gcolCode.OptionsColumn.AllowEdit = true;
                else
                    gcolCode.OptionsColumn.AllowEdit = false;

                MakeCompControlsEnableDisable();
                MakeOpCompControlsEnableDisable();
            }
            else
            {
                sRegionName = "";
                gcolCode.OptionsColumn.AllowEdit = true;

                gcIncludedComp.DataSource = null;
                gcAddComp.DataSource = null;
                gcIncludedOpComp.DataSource = null;
                gcAddOpComp.DataSource = null;

                btnLinkComp.Enabled = false;
                btnUnlinkComp.Enabled = false;
                btnLinkOpComp.Enabled = false;
                btnUnlinkOpComp.Enabled = false;
            }
        }

        private void gvRegionMaster_BeforeLeaveRow(object sender, DevExpress.XtraGrid.Views.Base.RowAllowEventArgs e)
        {
            bool bComp, bOpComp;
            bComp = DoBeforeClose("C", ref dtCompList4Reg);
            bOpComp = DoBeforeClose("O", ref dtOpCompList4Reg);

            e.Allow = !bComp && !bOpComp;
        }

        private void gvRegionMaster_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            drRegion = gvRegionMaster.GetDataRow(e.RowHandle);
            drRegion["IS_ACTIVE"] = "Y";
            drRegion["CREATED_BY"] = ProfitCashflow.oPcfDM.UserName;
            drRegion["CREATED_ON"] = DateTime.Now;
        }

        private void gvRegionMaster_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void gvRegionMaster_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
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

            drRegion = gvRegionMaster.GetDataRow(e.RowHandle);
            if (drRegion == null) return;

            if (drRegion["REGION_CODE"] == DBNull.Value || drRegion["REGION_CODE"].ToString().Trim() == string.Empty)
            {
                XtraMessageBox.Show("Region Code is mandatory.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Valid = false;
                return;
            }
            else if (drRegion["REGION_CODE"] != DBNull.Value && !string.IsNullOrWhiteSpace(drRegion["REGION_CODE"].ToString()))
            {
                DataRow[] drRows = dtRegionMaster.Select("REGION_CODE = '" + drRegion["REGION_CODE"] + "'");
                foreach (DataRow drCode in drRows)
                    if (dtRegionMaster.Rows.IndexOf(drCode) != dtRegionMaster.Rows.IndexOf(drRegion) && drCode.RowState != DataRowState.Deleted)
                        if (Convert.ToString(drCode["REGION_CODE"]) == Convert.ToString(drRegion["REGION_CODE"]))
                        {
                            XtraMessageBox.Show("Duplicate Region Code is not allowed.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            e.Valid = false;
                            return;
                        }
            }
            else
                drRegion["REGION_CODE"] = drRegion["REGION_CODE"].ToString().ToUpper();

            if (gvRegionMaster.IsNewItemRow(e.RowHandle) || (drRegion.RowState == DataRowState.Modified &&
                Convert.ToString(drRegion["REGION_CODE", DataRowVersion.Original]) != Convert.ToString(drRegion["REGION_CODE", DataRowVersion.Current])))
            {
                object oResult = oRegionMaster.RetrieveDuplicateRegionCount(Convert.ToString(drRegion["REGION_CODE"]));
                if (oResult != null && Convert.ToDecimal(oResult) > 0)
                {
                    XtraMessageBox.Show("Duplicate Region Code is not allowed.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Valid = false;
                    return;
                }
            }

            if (drRegion["REGION_NAME"] == DBNull.Value || drRegion["REGION_NAME"].ToString().Trim() == string.Empty)
            {
                XtraMessageBox.Show("Region Name is mandatory.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Valid = false;
                return;
            }

            if (Convert.ToString(drRegion["IS_ACTIVE"]) == "N" && dtCompList4Reg.Select("REGION_ID IS NOT NULL AND REGION_ID=" + drRegion["REGION_ID"]).Length > 0)
            {
                XtraMessageBox.Show("A Region with Included Companies cannot be deactivated. Please remove Included Companies and then deactivate.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Valid = false;
                return;
            }

            if (Convert.ToString(drRegion["IS_ACTIVE"]) == "N" && dtOpCompList4Reg.Select("REGION_ID IS NOT NULL AND REGION_ID=" + drRegion["REGION_ID"]).Length > 0)
            {
                XtraMessageBox.Show("A Region with Included Operating Companies cannot be deactivated. Please remove Included Operating Companies and then deactivate.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Valid = false;
                return;
            }

            if (Convert.ToString(drRegion["IS_ACTIVE"]) == "N" && dtRegionMaster.Select("IS_ACTIVE='Y' AND REGION_CODE <> '" + drRegion["REGION_CODE"] + "'").Length == 0)
            {
                XtraMessageBox.Show("At least one Region must remain Active.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Valid = false;
                return;
            }

            if (drRegion["COMPANY_ID"] != DBNull.Value && !string.IsNullOrWhiteSpace(drRegion["COMPANY_ID"].ToString()) && dtRegionMaster.Rows.Count > 0)
                if (dtRegionMaster.Select("COMPANY_ID = " + drRegion["COMPANY_ID"] + " AND REGION_CODE <> '" + drRegion["REGION_CODE"] + "'").Length > 0)
                {
                    XtraMessageBox.Show("Company has already been linked to some other Region.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Valid = false;
                    return;
                }

            if (!string.IsNullOrEmpty(sRegionName) && sRegionName != Convert.ToString(drRegion["REGION_NAME"]))
            {
                DialogResult oResult = XtraMessageBox.Show("Are you sure, you want to update Region name?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (oResult == System.Windows.Forms.DialogResult.No)
                {
                    e.Valid = false;
                    return;
                }
            }

            drRegion["CREATED_BY"] = ProfitCashflow.oPcfDM.UserName;
            drRegion["LAST_UPDATED"] = DateTime.Now;
        }

        private void gvRegionMaster_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            try
            {
                if (dtRegionMaster.GetChanges() != null)
                {
                    oRegionMaster.UpdateRegionMaster(dtRegionMaster.GetChanges());
                    dtRegionMaster.AcceptChanges();

                    int iPrevHandle = gvRegionMaster.FocusedRowHandle;
                    dtRegionMaster = oRegionMaster.RetrieveRegionMaster();
                    gcRegionMaster.DataSource = dtRegionMaster;

                    if (iPrevHandle == gvRegionMaster.FocusedRowHandle)
                        gvRegionMasterFocusedRowChanged(gvRegionMaster.FocusedRowHandle);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bHasChanges = false;
        }

        private void btnLinkComp_Click(object sender, EventArgs e)
        {
            LinkUnlinkComp("L");
        }

        private void btnUnlinkComp_Click(object sender, EventArgs e)
        {
            LinkUnlinkComp("U");
        }

        private void LinkUnlinkComp(string sType)
        {
            if (drRegion == null) return;

            DataRow[] drLinkUnlinkComp = null;
            if (sType == "L")
                drLinkUnlinkComp = dtCompList4Reg.Select("Select='Y' AND REGION_ID IS NULL");
            else if (sType == "U")
                drLinkUnlinkComp = dtCompList4Reg.Select("Select='Y' AND REGION_ID IS NOT NULL");

            if (drLinkUnlinkComp != null && drLinkUnlinkComp.Length > 0)
            {
                gvIncludedComp.BeginUpdate();

                foreach (DataRow drTemp in drLinkUnlinkComp)
                {
                    DataRow drLink = dtLinkComp.Rows.Find(new object[] { drTemp["COMPANY_ID"], drRegion["REGION_ID"] });
                    if (sType == "L" && drLink == null)
                    {
                        drLink = dtLinkComp.NewRow();
                        drLink["COMPANY_ID"] = drTemp["COMPANY_ID"];
                        drLink["REGION_ID"] = drRegion["REGION_ID"];
                        dtLinkComp.Rows.Add(drLink);
                    }
                    else if (sType == "U" && drLink != null)
                        drLink.Delete();

                    drTemp["REGION_ID"] = sType == "L" ? drRegion["REGION_ID"] : DBNull.Value;
                }

                try
                {
                    if (dtLinkComp.GetChanges() != null)
                    {
                        oRegionMaster.UpdateRegionAndCompLinkage(dtLinkComp.GetChanges());
                        dtLinkComp.AcceptChanges();

                        foreach (DataRow drTemp in dtCompList4Reg.Select("Select='Y'"))
                        {
                            drTemp["Select"] = "N";
                            if (Convert.ToString(drTemp["JV_COMPANY"]) == "Y")
                                if (sType == "L")
                                {
                                    DataRow drComp = dtCompList4Reg.NewRow();
                                    drComp.ItemArray = drTemp.ItemArray;
                                    drComp["REGION_ID"] = DBNull.Value;
                                    dtCompList4Reg.Rows.Add(drComp);
                                }
                                else if (sType == "U")
                                    drTemp.Delete();
                        }
                        dtCompList4Reg.AcceptChanges();
                    }   
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dtCompList4Reg.RejectChanges();
                }

                gvIncludedComp.EndUpdate();

                SetCompGridDataSource();
                /*gcIncludedComp.DataSource = new DataView(dtCompList4Reg, "REGION_ID IS NOT NULL AND REGION_ID=" + drRegion["REGION_ID"], "", DataViewRowState.CurrentRows);
                gcAddComp.DataSource = new DataView(dtCompList4Reg, "REGION_ID IS NULL", "", DataViewRowState.CurrentRows);*/
                gvRegionMaster.Focus();

                MakeCompControlsEnableDisable();
            }
        }

        private void SetCompGridDataSource()
        {
            string sJvCompFilter = "", sFilter = "";

            if (drRegion != null && drRegion["REGION_ID"] != DBNull.Value && !string.IsNullOrWhiteSpace(drRegion["REGION_ID"].ToString()))
            {
                gcIncludedComp.DataSource = new DataView(dtCompList4Reg, "REGION_ID IS NOT NULL AND REGION_ID=" + drRegion["REGION_ID"], "", DataViewRowState.CurrentRows);

                DataView dvIncludeComp = new DataView(dtCompList4Reg, "JV_COMPANY='Y' AND REGION_ID IS NOT NULL AND REGION_ID=" + drRegion["REGION_ID"], "", DataViewRowState.CurrentRows);
                DataTable dtIncludeComp = dvIncludeComp.ToTable(true, new string[] { "COMPANY_ID" });

                foreach (DataRow drJvComp in dtIncludeComp.Rows)
                    sJvCompFilter = (sJvCompFilter == "") ? drJvComp["COMPANY_ID"].ToString() : ", " + drJvComp["COMPANY_ID"].ToString();
            }
            else
                gcIncludedComp.DataSource = null;

            if (!string.IsNullOrWhiteSpace(sJvCompFilter))
                sFilter = "REGION_ID IS NULL AND COMPANY_ID NOT IN (" + sJvCompFilter + ")";
            else
                sFilter = "REGION_ID IS NULL";
            gcAddComp.DataSource = new DataView(dtCompList4Reg, sFilter, "", DataViewRowState.CurrentRows);
        }

        private void btnLinkOpComp_Click(object sender, EventArgs e)
        {
            LinkUnlinkOpComp("L");
        }

        private void btnUnlinkOpComp_Click(object sender, EventArgs e)
        {
            LinkUnlinkOpComp("U");
        }

        private void LinkUnlinkOpComp(string sType)
        {
            if (drRegion == null) return;

            DataRow[] drLinkUnlinkOpComp = null;
            if (sType == "L")
                drLinkUnlinkOpComp = dtOpCompList4Reg.Select("Select='Y' AND REGION_ID IS NULL");
            else if (sType == "U")
                drLinkUnlinkOpComp = dtOpCompList4Reg.Select("Select='Y' AND REGION_ID IS NOT NULL");

            if (drLinkUnlinkOpComp != null && drLinkUnlinkOpComp.Length > 0)
            {
                gvIncludedOpComp.BeginUpdate();
                foreach (DataRow drTemp in drLinkUnlinkOpComp)
                    drTemp["REGION_ID"] = sType == "L" ? drRegion["REGION_ID"] : DBNull.Value;

                try
                {
                    if (dtOpCompList4Reg.GetChanges() != null)
                    {
                        oRegionMaster.UpdateRegionOpCompLinkage(dtOpCompList4Reg.GetChanges());
                        foreach (DataRow drTemp in dtOpCompList4Reg.Select("Select='Y'"))
                            drTemp["Select"] = "N";
                        dtOpCompList4Reg.AcceptChanges();
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dtOpCompList4Reg.RejectChanges();
                }

                gvIncludedOpComp.EndUpdate();

                gcIncludedOpComp.DataSource = new DataView(dtOpCompList4Reg, "REGION_ID IS NOT NULL AND REGION_ID=" + drRegion["REGION_ID"], "", DataViewRowState.CurrentRows);
                gcAddOpComp.DataSource = new DataView(dtOpCompList4Reg, "REGION_ID IS NULL", "", DataViewRowState.CurrentRows);
                gvRegionMaster.Focus();

                MakeOpCompControlsEnableDisable();
            }
        }

        private void MakeCompControlsEnableDisable()
        {
            if (dtCompList4Reg.Select("Select='Y' AND REGION_ID IS NOT NULL").Length > 0)
            {
                gvIncludedComp.OptionsBehavior.Editable = true;
                gvAddComp.OptionsBehavior.Editable = false;
                btnLinkComp.Enabled = false;
                btnUnlinkComp.Enabled = true;
            }
            else if(dtCompList4Reg.Select("Select='Y' AND REGION_ID IS NULL").Length > 0)
            {
                gvIncludedComp.OptionsBehavior.Editable = false;
                gvAddComp.OptionsBehavior.Editable = true;
                btnLinkComp.Enabled = true;
                btnUnlinkComp.Enabled = false;
            }
            else
            {
                gvIncludedComp.OptionsBehavior.Editable = true;
                gvAddComp.OptionsBehavior.Editable = true;
                btnLinkComp.Enabled = false;
                btnUnlinkComp.Enabled = false;
            }
        }

        private void MakeOpCompControlsEnableDisable()
        {
            if (dtOpCompList4Reg.Select("Select='Y' AND REGION_ID IS NOT NULL").Length > 0)
            {
                gvIncludedOpComp.OptionsBehavior.Editable = true;
                gvAddOpComp.OptionsBehavior.Editable = false;
                btnLinkOpComp.Enabled = false;
                btnUnlinkOpComp.Enabled = true;
            }
            else if (dtOpCompList4Reg.Select("Select='Y' AND REGION_ID IS NULL").Length > 0)
            {
                gvIncludedOpComp.OptionsBehavior.Editable = false;
                gvAddOpComp.OptionsBehavior.Editable = true;
                btnLinkOpComp.Enabled = true;
                btnUnlinkOpComp.Enabled = false;
            }
            else
            {
                gvIncludedOpComp.OptionsBehavior.Editable = true;
                gvAddOpComp.OptionsBehavior.Editable = true;
                btnLinkOpComp.Enabled = false;
                btnUnlinkOpComp.Enabled = false;
            }
        }

        private void gvCompLinkage_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.RowHandle < 0) return;

            if (e.Column == gcolASelect && e.Value != null && e.Value.ToString() == "Y")
            {
                if (drRegion != null && drRegion["IS_ACTIVE"].ToString() != "Y")
                {
                    XtraMessageBox.Show("Companies cannot be linked for an inactive Region.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    gvAddComp.SetFocusedRowCellValue(gcolASelect, gvAddComp.GetDataRow(e.RowHandle)["Select"]);
                    return;
                }

                try
                {
                    oComm.ValidateRelationCompRegOpComp(gvAddComp.GetDataRow(e.RowHandle)["COMPANY_ID"], drRegion["REGION_ID"], null);
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    gvAddComp.SetFocusedRowCellValue(gcolASelect, gvAddComp.GetDataRow(e.RowHandle)["Select"]);
                    return;
                }
            }

            if (e.Column == gcolISelect && e.Value != null && e.Value.ToString() == "Y" && drRegion != null &&
                drRegion["COMPANY_ID"] != DBNull.Value && !string.IsNullOrWhiteSpace(drRegion["COMPANY_ID"].ToString()))
                if (Convert.ToDecimal(drRegion["COMPANY_ID"]) == Convert.ToDecimal(gvIncludedComp.GetDataRow(e.RowHandle)["COMPANY_ID"]))
                {
                    XtraMessageBox.Show("This Company has been linked to the selected Region. Please delink it first.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    gvIncludedComp.SetFocusedRowCellValue(gcolISelect, gvIncludedComp.GetDataRow(e.RowHandle)["Select"]);
                    return;
                }

            if (e.Column == gcolASelect)
                gvAddComp.GetDataRow(e.RowHandle)["Select"] = (e.Value != null && e.Value.ToString() == "Y") ? "Y" : "N";
            if (e.Column == gcolISelect)
                gvIncludedComp.GetDataRow(e.RowHandle)["Select"] = (e.Value != null && e.Value.ToString() == "Y") ? "Y" : "N";

            MakeCompControlsEnableDisable();
        }

        private void gvOpCompLinkage_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.RowHandle < 0) return;

            if (e.Column == gcolAOSelect && e.Value != null && e.Value.ToString() == "Y")
            {
                if (drRegion != null && drRegion["IS_ACTIVE"].ToString() != "Y")
                {
                    XtraMessageBox.Show("Operating Companies cannot be linked for an inactive Region.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    gvAddOpComp.SetFocusedRowCellValue(gcolAOSelect, gvAddOpComp.GetDataRow(e.RowHandle)["Select"]);
                    return;
                }

                try
                {
                    oComm.ValidateRelationCompRegOpComp(null, drRegion["REGION_ID"], gvAddOpComp.GetDataRow(e.RowHandle)["OPERATING_COMPANY_ID"]);
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    gvAddOpComp.SetFocusedRowCellValue(gcolAOSelect, gvAddOpComp.GetDataRow(e.RowHandle)["Select"]);
                    return;
                }
            }

            if (e.Column == gcolAOSelect)
                gvAddOpComp.GetDataRow(e.RowHandle)["Select"] = (e.Value != null && e.Value.ToString() == "Y") ? "Y" : "N";
            if (e.Column == gcolIOSelect)
                gvIncludedOpComp.GetDataRow(e.RowHandle)["Select"] = (e.Value != null && e.Value.ToString() == "Y") ? "Y" : "N";

            MakeOpCompControlsEnableDisable();
        }

        private void repLookUpComp_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
               gvRegionMaster.SetFocusedRowCellValue(gcolComp, DBNull.Value);
        }

        private void LookUp_MouseWheel(object sender, MouseEventArgs e)
        {
            if (!((LookUpEdit)sender).IsPopupOpen)
                DevExpress.Utils.DXMouseEventArgs.GetMouseArgs(e).Handled = true;
        }

        private void repLookUpComp_QueryPopUp(object sender, CancelEventArgs e)
        {
            decimal dRegId = -1;
            if (drRegion != null && drRegion["REGION_ID"] != DBNull.Value && !string.IsNullOrWhiteSpace(drRegion["REGION_ID"].ToString()))
                dRegId = Convert.ToDecimal(drRegion["REGION_ID"]);

            ((LookUpEdit)sender).Properties.DataSource = new DataView(dtCompList4Reg, "REGION_ID IS NOT NULL AND REGION_ID=" + dRegId, "", DataViewRowState.CurrentRows);
            ((LookUpEdit)sender).Properties.DisplayMember = "COMPANY";
            ((LookUpEdit)sender).Properties.ValueMember = "COMPANY_ID";
        }
    }
}