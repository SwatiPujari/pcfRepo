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
using System.Globalization;

namespace Xpedeon.ProfitCashflow
{
    public partial class SiteSetup : DevExpress.XtraEditors.XtraForm
    {
        sSiteSetup dal = new sSiteSetup();
        sCommonMethods commonDal = new sCommonMethods();
        sLocationMas locDal = new sLocationMas();
        sPurchaseTypeMaster purchaseDal = new sPurchaseTypeMaster();
        sLandTypeMaster landDal = new sLandTypeMaster();
        sBuildTypeMaster buildDal = new sBuildTypeMaster();
        sB4SiteCodeMaster b4SideCodeDal = new sB4SiteCodeMaster();

        DataTable dtAllCompany = new DataTable();
        DataTable bindingSiteSearchDt = new DataTable();

        DataTable bindingSiteDt = new DataTable();
        DataTable bindingSiteDocDt = new DataTable();
        DataTable bindingSitePlotDt = new DataTable();

        string upd_CSV_FLAG;
        int upd_CSV, upd_Comp_Id, int_newid, int_newGUID, str_site_id;
        bool pIns, pEdit, b_Insert, b_Phase_ch, b_Update_PlotIndicator;

        FormMode mode;

        public SiteSetup()
        {
            InitializeComponent();
        }

        private void CreateSiteSetupTable()
        {
            bindingSiteDt.Columns.Add("SITE_ID", typeof(decimal));
            bindingSiteDt.Columns.Add("COMPANY_ID", typeof(decimal));
            bindingSiteDt.Columns.Add("SITE_CODE", typeof(string));
            bindingSiteDt.Columns.Add("SITE_NAME", typeof(string));
            bindingSiteDt.Columns.Add("ADDR_L1", typeof(string));
            bindingSiteDt.Columns.Add("ADDR_L2", typeof(string));
            bindingSiteDt.Columns.Add("ADDR_L3", typeof(string));
            bindingSiteDt.Columns.Add("CITY", typeof(string));
            bindingSiteDt.Columns.Add("ZIP_POST_CODE", typeof(string));
            bindingSiteDt.Columns.Add("SITE_SHORT_NAME", typeof(string));
            bindingSiteDt.Columns.Add("MKTG_NAME", typeof(string));
            bindingSiteDt.Columns.Add("STATUS", typeof(string));
            bindingSiteDt.Columns.Add("JV_EXTERNAL", typeof(string));
            bindingSiteDt.Columns.Add("JV_MNGT_FEE_PCT", typeof(decimal));

            bindingSiteDt.Columns.Add("LAND_TYPE", typeof(string));
            bindingSiteDt.Columns.Add("BUILD_TYPE", typeof(string));
            bindingSiteDt.Columns.Add("COUNTRY_CODE", typeof(string));

            bindingSiteDt.Columns.Add("LOCATION_CODE", typeof(string));
            bindingSiteDt.Columns.Add("AREA", typeof(string));
            bindingSiteDt.Columns.Add("BLDG_UNDER_LICENSE", typeof(string));
            bindingSiteDt.Columns.Add("LAND_TITLE_REF", typeof(string));
            bindingSiteDt.Columns.Add("LAND_BUYER", typeof(string));
            bindingSiteDt.Columns.Add("SITE_SIZE", typeof(string));
            bindingSiteDt.Columns.Add("DESCRIPTION", typeof(string));
            bindingSiteDt.Columns.Add("VENDOR", typeof(string));
            bindingSiteDt.Columns.Add("PURCHASE_PRICE", typeof(decimal));
            bindingSiteDt.Columns.Add("PAYMENT_TIMING", typeof(string));
            bindingSiteDt.Columns.Add("SITE_ABORTED", typeof(string));
            bindingSiteDt.Columns.Add("ABORTED_DATE", typeof(DateTime));
            bindingSiteDt.Columns.Add("ABORTED_COMMENTS", typeof(string));

            bindingSiteDt.Columns.Add("SITE_COMPLETED", typeof(string));
            bindingSiteDt.Columns.Add("COMPLETED_DATE", typeof(DateTime));
            bindingSiteDt.Columns.Add("COMPLETED_COMMENTS", typeof(string));

            bindingSiteDt.Columns.Add("CREATED_BY", typeof(string));
            bindingSiteDt.Columns.Add("CREATED_ON", typeof(DateTime));
            bindingSiteDt.Columns.Add("LAST_UPDATED", typeof(DateTime));
            bindingSiteDt.Columns.Add("PURCHASE_TYPE_ID", typeof(decimal));
            bindingSiteDt.Columns.Add("PCF_DOC_GUID", typeof(decimal));
            bindingSiteDt.Columns.Add("PURCHASER", typeof(string));
            bindingSiteDt.Columns.Add("DATE_OF_SALE", typeof(DateTime));
            bindingSiteDt.Columns.Add("STACK_STATUS", typeof(string));
            bindingSiteDt.Columns.Add("JV_COMPANY_ID", typeof(decimal));
            bindingSiteDt.Columns.Add("SALES_RELEASE_STATUS", typeof(string));
            bindingSiteDt.Columns.Add("SALES_RELEASE_DATE", typeof(DateTime));
            bindingSiteDt.Columns.Add("XPEDEON_RELEASE_STATUS", typeof(string));
            bindingSiteDt.Columns.Add("XPEDEON_RELEASE_DATE", typeof(DateTime));
            bindingSiteDt.Columns.Add("WBS_RELEASE_STATUS", typeof(string));
            bindingSiteDt.Columns.Add("WBS_RELEASE_DATE", typeof(DateTime));
            bindingSiteDt.Columns.Add("CODA_RELEASE_STATUS", typeof(string));
            bindingSiteDt.Columns.Add("CODA_RELEASE_DATE", typeof(DateTime));

            bindingSiteDt.Columns.Add("LAND_TYPE_ID", typeof(decimal));
            bindingSiteDt.Columns.Add("BUILD_TYPE_ID", typeof(decimal));

            bindingSiteDt.Columns.Add("MAX_PLOT_NO", typeof(string));

            //MAX_PLOT_NO
            bindingSiteDt.Columns.Add("LAND_STATUS", typeof(string));
            bindingSiteDt.Columns.Add("B4_DATE", typeof(DateTime));
            bindingSiteDt.Columns.Add("B3_DATE", typeof(DateTime));
            bindingSiteDt.Columns.Add("B2_DATE", typeof(DateTime));
            bindingSiteDt.Columns.Add("B1_DATE", typeof(DateTime));
            bindingSiteDt.Columns.Add("ATL_DATE", typeof(DateTime));

            bindingSiteDt.Columns.Add("SYS_CREATED_STACK_ID", typeof(decimal));
            bindingSiteDt.Columns.Add("CURRENT_STACK_ID", typeof(decimal));

            //SYS_CREATED_STACK_ID decimal
            //CURRENT_STACK_ID decimal
            bindingSiteDt.Columns.Add("MODE", typeof(decimal));
            bindingSiteDt.Columns.Add("LAND_SALE", typeof(string));
            bindingSiteDt.Columns.Add("LAND_SALE_COMMENTS", typeof(string));
            //USE_IN_REPORT string
            bindingSiteDt.Columns.Add("USE_IN_REPORT", typeof(string));

            bindingSiteDt.Columns.Add("PART_EXCHANGE", typeof(string));
            bindingSiteDt.Columns.Add("DEVELOPMENT_ID", typeof(decimal));
        }

        private void SiteSetup_Load(object sender, EventArgs e)
        {
            mode = FormMode.Browse;
            cxImageComboBoxShowCompanies.SelectedIndex = 0;
            lcgSelCriteria.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            lcgAllSites.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            dtAllCompany = commonDal.RetrieveCompany(ProfitCashflow.oPcfDM.UserName, "A");

            DataTable regionDt = commonDal.RetrieveRegionMasterList();
            DataView regionDv = new DataView(regionDt, "IS_ACTIVE = 'Y'", string.Empty, DataViewRowState.CurrentRows);
            if (regionDv != null)
                cxLookUpRegion.Properties.DataSource = regionDv.ToTable();

            cxLookupCBSearchComp.Properties.DataSource = commonDal.RetrieveCompanyListByRegion(ProfitCashflow.oPcfDM.UserName, cxImageComboBoxShowCompanies.EditValue.ToString(), cxLookUpRegion.EditValue == null ? -1 : Convert.ToDecimal(cxLookUpRegion.EditValue));
            if (cxLookupCBSearchComp.EditValue != null)
                cxLookupCBSearchSite.Properties.DataSource = dal.RetrieveSites(Convert.ToDecimal(cxLookupCBSearchComp.EditValue.ToString()));

            DataTable dt = commonDal.RetrieveDevelopmentMasterList();

            DataView dv = new DataView(dt, "IS_ACTIVE = 'Y'", string.Empty, DataViewRowState.CurrentRows);
            if (dv != null)
                cxlookUpSearchDevelpmnt.Properties.DataSource = dv.ToTable();

            cxDBLookupCBComp.Properties.DataSource = commonDal.RetrieveCompanyList(ProfitCashflow.oPcfDM.UserName);
            cxDBLandTypeId.Properties.DataSource = landDal.RetrieveLandTypeMaster();
            cxDBBuildTypeId.Properties.DataSource = buildDal.RetrieveBuildTypeMaster();
            cxDBLookupCBLoc.Properties.DataSource = locDal.RetrieveLocationMaster();
            cxDBLookupCBPT.Properties.DataSource = purchaseDal.RetrievePurchaseMaster();
            cxlookUpDevelop.Properties.DataSource = dv.ToTable();
            //cxGrid1DBTableView1.DataController.DataSource = dsSiteDocs;

            if (ProfitCashflow.oPcfDM.company_id != -1)
            {
                cxLookupCBSearchComp.EditValue = ProfitCashflow.oPcfDM.company_id;
            }

            upd_CSV_FLAG = "N";

            MakeButtonEnableDisable(true, false, false, false);
            MakeControlEnable("B", true);
        }

        private void SiteSetup_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (mode != FormMode.Browse && (bindingSiteDt.GetChanges() != null || bindingSitePlotDt.GetChanges() != null || bindingSiteDocDt.GetChanges() != null))
            {
                DialogResult oTempResult = XtraMessageBox.Show("Data has been changed, do you want to save the changes?", Application.ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (oTempResult == DialogResult.Yes)
                {
                    if (!SaveSiteSetup())
                        e.Cancel = true;
                }
                else if (oTempResult == DialogResult.No)
                    btnCancel_Click(sender, new EventArgs());
                else
                    e.Cancel = true;
            }
        }

        private void SiteSetup_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (cxLookupCBSearchComp.EditValue == null)
            {
                XtraMessageBox.Show("Company must be selected.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            this.cxGrid2DBTableView1.FocusedRowChanged -= new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.cxGrid2DBTableView1_FocusedRowChanged);
            bindingSiteSearchDt = dal.RetrieveSearchSites(
            Convert.ToDecimal(cxLookupCBSearchComp.EditValue.ToString()),
            (cxLookupCBSearchSite.EditValue == null || (cxLookupCBSearchSite.EditValue != null && string.IsNullOrEmpty(cxLookupCBSearchSite.EditValue.ToString()))) ? null : cxLookupCBSearchSite.EditValue.ToString(),
            (cxlookUpSearchDevelpmnt.EditValue == null || (cxlookUpSearchDevelpmnt.EditValue != null && string.IsNullOrEmpty(cxlookUpSearchDevelpmnt.EditValue.ToString()))) ? null : (decimal?)cxlookUpSearchDevelpmnt.EditValue,
            (cxTextSiteShortName.EditValue == null || (cxTextSiteShortName.EditValue != null && string.IsNullOrEmpty(cxTextSiteShortName.EditValue.ToString()))) ? null : cxTextSiteShortName.EditValue.ToString(),
            (cxTextMktgName.EditValue == null || (cxTextMktgName.EditValue != null && string.IsNullOrEmpty(cxTextMktgName.EditValue.ToString()))) ? null : cxTextMktgName.EditValue.ToString());

            bindingSiteSearchDt.Constraints.Add("PK_SITE_ID", bindingSiteSearchDt.Columns["SITE_ID"], true);

            cxGrid2.DataSource = bindingSiteSearchDt;
            this.cxGrid2DBTableView1.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.cxGrid2DBTableView1_FocusedRowChanged);

            if (cxGrid2DBTableView1.DataRowCount > 0)
            {
                lcgSelCriteria.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lcgAllSites.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

                // Get site setup data
                tabbedControlCriteria.SelectedTabPage = lcgAllSites;
                DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e1 = new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, cxGrid2DBTableView1.FocusedRowHandle);
                cxGrid2DBTableView1_FocusedRowChanged(cxGrid2DBTableView1, e1);
            }
            else
            {
                XtraMessageBox.Show("No rows selected.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (str_site_id > 0)
            {
                LoadSingleSiteSetup(str_site_id);
            }

            MakeButtonEnableDisable(true, cxGrid2DBTableView1.RowCount == 0 ? false : true, false, false);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            mode = FormMode.New;


            cxImgCmboBoxStatus.Properties.AppearanceDisabled.BackColor = Color.FromArgb(191, 191, 255);
            cxDBLookupCBComp.Focus();

            try
            {
                var ds = dal.RetrieveSiteById(-1);
                bindingSiteDt = ds.Tables[0];
                bindingSiteDocDt = ds.Tables[1];
                bindingSitePlotDt = ds.Tables[2];

                gcDocument.DataSource = bindingSiteDocDt;
                gcPlot.DataSource = bindingSitePlotDt;

                bindingSiteDt.TableNewRow -= bindingSiteDt_TableNewRow;
                bindingSiteDt.TableNewRow += bindingSiteDt_TableNewRow;
                bindingSiteDt.Constraints.Add("PK_SITE_ID", bindingSiteDt.Columns["SITE_ID"], true);

                AddBinding();
                try
                {
                    dnSiteSetup.Buttons.DoClick(dnSiteSetup.Buttons.Append);
                }
                catch (Exception exp)
                {
                    throw exp;
                }

                pIns = true;
                pEdit = false;
                b_Insert = true;
                //pSiteSetupDM.int_Old_Pt_No := 0;

                MakeButtonEnableDisable(false, false, true, true);
                MakeControlEnable("N", false);

                if (ProfitCashflow.oPcfDM.company_id != -1)
                {
                    cxDBLookupCBComp.EditValue = ProfitCashflow.oPcfDM.company_id;
                }

                cxDBLookupCBComp.Focus();
                cxImgCmboBoxStatus.BackColor = System.Drawing.Color.FromArgb(193, 193, 255);
                PopulateLandProgressDates();
                PopulateJVCompany();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            decimal pcf_doc_guid = 0;
            decimal lSiteId = 0;
            mode = FormMode.Edit;

            try
            {
                if (drBindingSite["PCF_DOC_GUID"] != DBNull.Value)
                {
                    pcf_doc_guid = Convert.ToDecimal(drBindingSite["PCF_DOC_GUID"]);
                    lSiteId = Convert.ToDecimal(drBindingSite["SITE_ID"]);
                }
                else if (cxGrid2DBTableView1.RowCount != -1)
                {
                    var dr = cxGrid2DBTableView1.GetDataRow(cxGrid2DBTableView1.FocusedRowHandle);
                    if (dr != null)
                    {
                        pcf_doc_guid = Convert.ToDecimal(dr["PCF_DOC_GUID"]);
                        lSiteId = Convert.ToDecimal(dr["SITE_ID"]);
                    }
                }

                if (lSiteId == 0)
                {
                    XtraMessageBox.Show("Select a Site Setup.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                int i = dal.RetrievePcfDocuments(pcf_doc_guid);
                if (i == 0)
                {
                    XtraMessageBox.Show("Selected Site Setup does not exist.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                int cnt = commonDal.RetrievePcfLockedDocuments(pcf_doc_guid, null);
                if (cnt > 0)
                {
                    XtraMessageBox.Show("Selected Site Setup # is locked by other user " + System.Environment.NewLine + "or by Form''s Stack System, ProfitForecast, ProfitForecast Bulk Update " + System.Environment.NewLine + "hence cannot progress further.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                commonDal.InsertPCFLockedDocument(pcf_doc_guid, ProfitCashflow.oPcfDM.UserName, ProfitCashflow.oPcfDM.session_id,
                    "SITE_SETUP", Convert.ToDecimal(drBindingSite["COMPANY_ID"]), drBindingSite["SITE_CODE"].ToString());

                LoadSingleSiteSetup(lSiteId);
                AddBinding();

                pIns = false;
                pEdit = true;
                b_Insert = false;
                MakeButtonEnableDisable(false, false, true, true);
                MakeControlEnable("E", false);

                //pSiteSetupDM.int_Old_Pt_No := StrToIntDef(StringReplace(pSiteSetupDM.cdSiteSetupMAX_PLOT_NO.AsString,'P','',[rfReplaceAll]),0);
                //pOrgJV_CompID = cdSiteSetupJV_COMPANY_ID.AsInteger;
                drBindingSite["CREATED_BY"] = ProfitCashflow.oPcfDM.UserName;
                drBindingSite["LAST_UPDATED"] = DateTime.Now;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveSiteSetup();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            decimal lSiteId = 0;
            bool vSuccess;

            DialogResult oResult = XtraMessageBox.Show("Delete selected Site Setup Record?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (oResult == System.Windows.Forms.DialogResult.No) return;

            lSiteId = Convert.ToDecimal(drBindingSite["SITE_ID"]);

            DataRow dr = cxGrid2DBTableView1.GetDataRow(cxGrid2DBTableView1.FocusedRowHandle);
            if (dr == null) return;

            var cnt = dal.CheckSite(Convert.ToDecimal(dr["COMPANY_ID"]), dr["SITE_CODE"].ToString());
            if (cnt > 0)
            {
                XtraMessageBox.Show("Cannot delete, as the Site is selected either in Stacks, Profit Forecast, Key Dates, Take To Sales or Cashflow.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            vSuccess = true;

            try
            {
                //TODO : Coda code

                if (vSuccess)
                {
                    try
                    {
                        dal.DeleteSiteSetup(Convert.ToDecimal(drBindingSite["COMPANY_ID"]), lSiteId, drBindingSite["SITE_CODE"].ToString(), "Y");
                        dr.Delete();
                        dr.AcceptChanges();
                    }
                    catch (Exception)
                    {
                        XtraMessageBox.Show("Site Setup data has not been deleted from the PCF database successfully.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    XtraMessageBox.Show("Site could not be deleted from the PCF database successfully, as the site did not delete from CODA.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            btnNew.Enabled = true;
            btnEdit.Enabled = true;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
            btnDelete.Enabled = true;
            btnSearch.Enabled = true;
            btnReleaseToSales.Enabled = true;
            btnProgressToNextLand.Enabled = true;

            cxDBLookupCBComp.Enabled = false;
            //cxTxtEditSiteCode.Enabled = false;

            cxBtnPrev.Enabled = true;
            splitContainerControl1.Panel1.Enabled = true;

            MakeControlEnable("C", true);
            cxTxtJVOperatingCompany.ReadOnly = true;
            gvDocument.OptionsBehavior.Editable = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                dnSiteSetup.Buttons.DoClick(dnSiteSetup.Buttons.CancelEdit);

                if (drBindingSite != null && drBindingSite["PCF_DOC_GUID"] != DBNull.Value)
                    commonDal.DeletePCFLockedDocument(Convert.ToDecimal(drBindingSite["PCF_DOC_GUID"]), ProfitCashflow.oPcfDM.session_id);

                if (mode == FormMode.New && cxGrid2DBTableView1.RowCount > 0)
                {
                    DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs eventArg = new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, cxGrid2DBTableView1.FocusedRowHandle);
                    cxGrid2DBTableView1_FocusedRowChanged(cxGrid2DBTableView1, eventArg);
                }
                else
                {
                    if (bindingSiteDt.GetChanges() != null)
                        bindingSiteDt.RejectChanges();
                    if (bindingSiteDocDt.GetChanges() != null)
                        bindingSiteDocDt.RejectChanges();
                }

                if (mode == FormMode.New && cxGrid2DBTableView1.RowCount > 0)
                {
                    DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs eventArg = new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, cxGrid2DBTableView1.FocusedRowHandle);
                    cxGrid2DBTableView1_FocusedRowChanged(cxGrid2DBTableView1, eventArg);
                }
                else if (mode == FormMode.New && bindingSiteDt.Rows.Count == 0)
                {
                    LoadSingleSiteSetup(-1);
                }
                //begin
                //     pSiteSetupDM.cdSiteSetup.Close;
                //     pSiteSetupDM.cdSiteSetup.Params.ParamByName('SITE_ID').AsFloat := -1;
                //     pSiteSetupDM.cdSiteSetup.Open;
                //     pSiteSetupDM.cdSiteSetupAfterScroll(pSiteSetupDM.cdSiteSetup);
                //end;

                mode = FormMode.Cancel;
                MakeButtonEnableDisable(true, cxGrid2DBTableView1.RowCount == 0 ? false : true, false, false);
                MakeControlEnable("C", true);

                cxBtnPrev.Enabled = true;
                splitContainerControl1.Panel1.Enabled = true;

                pIns = false;
                pEdit = false;
                b_Insert = false;

                cxDBLookupCBComp.Enabled = false;
                cxTxtJVOperatingCompany.Enabled = false;
                cxTxtJVOperatingCompany.ReadOnly = false;
                cxImgCmboBoxStatus.BackColor = System.Drawing.Color.FromArgb(227, 239, 255);
                mode = FormMode.Browse;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cxLookUpRegion_EditValueChanged(object sender, EventArgs e)
        {
            cxLookupCBSearchComp.Properties.DataSource = commonDal.RetrieveCompanyListByRegion(ProfitCashflow.oPcfDM.UserName, cxImageComboBoxShowCompanies.EditValue.ToString(), cxLookUpRegion.EditValue == null ? -1 : Convert.ToDecimal(cxLookUpRegion.EditValue));
            cxLookupCBSearchComp.EditValue = null;
        }

        private void cxLookupCBSearchComp_EditValueChanged(object sender, EventArgs e)
        {
            if (cxLookupCBSearchComp.EditValue != null)
            {
                cxLookupCBSearchSite.Properties.DataSource = dal.RetrieveSites(Convert.ToDecimal(cxLookupCBSearchComp.EditValue.ToString()));
            }
        }

        private void cxImageComboBoxShowCompanies_SelectedValueChanged(object sender, EventArgs e)
        {
            cxLookupCBSearchComp.EditValue = null;
            cxLookupCBSearchSite.EditValue = null;
            cxLookupCBSearchComp.Properties.DataSource = commonDal.RetrieveCompany(ProfitCashflow.oPcfDM.UserName, cxImageComboBoxShowCompanies.EditValue.ToString());
        }

        private void cxGrid2DBTableView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                if (e.FocusedRowHandle >= 0)
                {
                    DataRow dr = cxGrid2DBTableView1.GetDataRow(e.FocusedRowHandle);
                    if (dr != null)
                    {
                        decimal siteId = Convert.ToDecimal(dr["SITE_ID"].ToString());
                        LoadSingleSiteSetup(siteId);
                        AddBinding();
                        MakeButtonEnableDisable(true, cxGrid2DBTableView1.RowCount > 0, false, false);
                        SetBackColorOfStatus(drBindingSite);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        DataRow drBindingSite = null;
        void bindingSiteDt_TableNewRow(object sender, DataTableNewRowEventArgs e)
        {
            drBindingSite = e.Row;
            if (drBindingSite == null) return;

            int_newid = -1;
            int_newGUID = -1;

            int_newid = dal.RetrieveMaxSeqId("SEQ_PK_STACKS");
            int_newGUID = dal.RetrieveMaxSeqId("SEQ_PK_PCF_DOC_GUID");

            int seq_Site_Id = dal.RetrieveMaxSeqId("SEQ_PK_SITE_SETUP");
            drBindingSite["SITE_ID"] = seq_Site_Id;

            int seq_SiteDoc_Id = dal.RetrieveMaxSeqId("SEQ_PK_PCF_DOC_GUID");
            drBindingSite["PCF_DOC_GUID"] = seq_SiteDoc_Id;
            drBindingSite["LAND_STATUS"] = "B4";
            drBindingSite["B4_DATE"] = DateTime.Now;
            drBindingSite["STACK_STATUS"] = "O";
            drBindingSite["STATUS"] = "AC";
            drBindingSite["BLDG_UNDER_LICENSE"] = "N";
            drBindingSite["SITE_ABORTED"] = "N";
            drBindingSite["SITE_COMPLETED"] = "N";
            drBindingSite["SALES_RELEASE_STATUS"] = "N";
            drBindingSite["XPEDEON_RELEASE_STATUS"] = "N";
            drBindingSite["WBS_RELEASE_STATUS"] = "N";
            drBindingSite["CODA_RELEASE_STATUS"] = "N";
            drBindingSite["CREATED_BY"] = ProfitCashflow.oPcfDM.UserName;
            drBindingSite["CREATED_ON"] = DateTime.Now;
            drBindingSite["LAST_UPDATED"] = DateTime.Now;
            drBindingSite["MODE"] = 0;
            drBindingSite["LAND_SALE"] = "N";
            drBindingSite["USE_IN_REPORT"] = "N";
            drBindingSite["PART_EXCHANGE"] = "N";
        }

        private void btnReleaseToSales_Click(object sender, EventArgs e)
        {
            decimal pcf_doc_guid, lSiteId;
            pcf_doc_guid = 0;
            lSiteId = 0;

            try
            {
                if (drBindingSite["PCF_DOC_GUID"] != DBNull.Value)
                {
                    pcf_doc_guid = Convert.ToDecimal(drBindingSite["PCF_DOC_GUID"]);
                    lSiteId = Convert.ToDecimal(drBindingSite["SITE_ID"]);
                }
                else if (cxGrid2DBTableView1.RowCount != -1)
                {
                    var dr = cxGrid2DBTableView1.GetDataRow(cxGrid2DBTableView1.FocusedRowHandle);
                    if (dr != null)
                    {
                        pcf_doc_guid = Convert.ToDecimal(dr["PCF_DOC_GUID"]);
                        lSiteId = Convert.ToDecimal(dr["SITE_ID"]);
                    }
                }

                if (lSiteId == 0)
                {
                    XtraMessageBox.Show("Select a Site Setup.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                dal.UpdateSalesReleaseStatus(lSiteId);

                if (drBindingSite != null)
                {
                    drBindingSite["SALES_RELEASE_STATUS"] = "Y";
                    drBindingSite["SALES_RELEASE_DATE"] = DateTime.Now;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnProgressToNextLand_Click(object sender, EventArgs e)
        {
            if (drBindingSite == null) return;

            LandStatus frmLandStatus = new LandStatus();
            frmLandStatus.rgLandStatus.EditValue = Convert.ToString(drBindingSite["LAND_STATUS"]);
            DialogResult oDialogResult = frmLandStatus.ShowDialog();
            if (oDialogResult == DialogResult.OK)
            {
                try
                {
                    commonDal.UpdateSiteLandStatus(Convert.ToDecimal(drBindingSite["COMPANY_ID"]), Convert.ToString(drBindingSite["SITE_CODE"]), frmLandStatus.rgLandStatus.EditValue.ToString());
                    cxTxtLandStatus.EditValue = frmLandStatus.rgLandStatus.EditValue.ToString();
                    DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e1 = new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, cxGrid2DBTableView1.FocusedRowHandle);
                    cxGrid2DBTableView1_FocusedRowChanged(cxGrid2DBTableView1, e1);
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            frmLandStatus.Close();
            frmLandStatus.Dispose();
            frmLandStatus = null;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            splitContainerControl1.Collapsed = false;
        }

        private void cxBtnPrev_Click(object sender, EventArgs e)
        {
            lcgSelCriteria.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            lcgAllSites.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        }

        private void cxDBLookupCBComp_EditValueChanged(object sender, EventArgs e)
        {
            string vPrefix = string.Empty, vSite_Code = string.Empty, vTemp_Suffix = string.Empty;
            int vSuffix = 0;

            try
            {
                if (cxDBLookupCBComp.EditValue != null && cxDBLookupCBComp.EditValue.ToString() != string.Empty)
                {
                    var ds = dal.RetrievePrefixSuffixSites(Convert.ToDecimal(cxDBLookupCBComp.EditValue));

                    // Prefix
                    if (Convert.ToInt32(ds.Tables[0].Rows[0]["CNT"]) > 0)
                    {
                        XtraMessageBox.Show("Please enter the Site Code Prefix in Company Master, as it is required for generating the site code.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //dxBarLargeBtnCancelClick(Self);
                    }

                    //Suffix
                    if (Convert.ToInt32(ds.Tables[1].Rows[0]["CNT"]) > 0)
                    {
                        XtraMessageBox.Show("Please enter the Initial Seed Value in Company Master.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //dxBarLargeBtnCancelClick(Self);
                    }

                    //Prefix and Suffix
                    if (ds.Tables[2].Rows.Count > 0)
                    {
                        vPrefix = ds.Tables[2].Rows[0]["PREFIX"].ToString();
                        vSuffix = Convert.ToInt32(ds.Tables[2].Rows[0]["SUFFIX"].ToString()) + 1;
                    }

                    var dt = dal.RetrieveSiteConfigValue();
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["VALUE"].ToString() == "N")
                        {
                            if (vSuffix.ToString().Length == 1)
                                vTemp_Suffix = "00" + vSuffix.ToString();
                            else if (vSuffix.ToString().Length == 2)
                                vTemp_Suffix = "0" + vSuffix.ToString();
                            else if (vSuffix.ToString().Length == 3)
                                vTemp_Suffix = vSuffix.ToString();
                            else if (vSuffix.ToString().Length > 3)
                                vTemp_Suffix = vSuffix.ToString().Substring(2, vSuffix.ToString().Length);

                            vSite_Code = vPrefix.ToString().Substring(0, 1) + vTemp_Suffix;
                        }
                        else
                        {
                            if (vSuffix.ToString().Length == 1)
                                vTemp_Suffix = "000" + vSuffix.ToString();
                            else if (vSuffix.ToString().Length == 2)
                                vTemp_Suffix = "00" + vSuffix.ToString();
                            else if (vSuffix.ToString().Length == 3)
                                vTemp_Suffix = "0" + vSuffix.ToString();
                            else if (vSuffix.ToString().Length == 4)
                                vTemp_Suffix = vSuffix.ToString();

                            vSite_Code = vPrefix + vTemp_Suffix;
                        }
                    }

                    var cnt = dal.ExistsSiteCode(Convert.ToDecimal(cxDBLookupCBComp.EditValue), vSite_Code);
                    if (cnt > 0)
                    {
                        XtraMessageBox.Show("Site Code " + vSite_Code + " already exists. Please change the Initial Seed Value in Company Master.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        // dxBarLargeBtnCancelClick(Self);
                    }
                    else if (cnt == 0)
                    {
                        if (drBindingSite == null) return;
                        drBindingSite["SITE_CODE"] = vSite_Code;
                        cxTxtEditSiteCode.EditValue = vSite_Code;
                        upd_CSV_FLAG = "Y";
                        upd_CSV = vSuffix;
                        upd_Comp_Id = Convert.ToInt32(cxDBLookupCBComp.EditValue);
                    }

                    PopulateJVCompany();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddBinding()
        {
            ClearBinding();
            cxDBLookupCBComp.DataBindings.Add("EditValue", bindingSiteDt, "COMPANY_ID");
            cxTxtEditSiteCode.DataBindings.Add("EditValue", bindingSiteDt, "SITE_CODE");
            cxlookUpDevelop.DataBindings.Add("EditValue", bindingSiteDt, "DEVELOPMENT_ID");
            cxTxtEditMarket.DataBindings.Add("EditValue", bindingSiteDt, "MKTG_NAME");
            cxTxtEditSiteName.DataBindings.Add("EditValue", bindingSiteDt, "SITE_NAME");
            cxTxtEditAddress.DataBindings.Add("EditValue", bindingSiteDt, "ADDR_L1");
            cxTxtEditAddress2.DataBindings.Add("EditValue", bindingSiteDt, "ADDR_L2");
            cxTxtEditAddress3.DataBindings.Add("EditValue", bindingSiteDt, "ADDR_L3");
            cxTxtEditPostCode.DataBindings.Add("EditValue", bindingSiteDt, "ZIP_POST_CODE");
            cxTxtEditSiteShort.DataBindings.Add("EditValue", bindingSiteDt, "SITE_SHORT_NAME");
            cxDBLandTypeId.DataBindings.Add("EditValue", bindingSiteDt, "LAND_TYPE_ID");
            cxDBBuildTypeId.DataBindings.Add("EditValue", bindingSiteDt, "BUILD_TYPE_ID");
            cxDBLookupCBLoc.DataBindings.Add("EditValue", bindingSiteDt, "LOCATION_CODE");
            cxImgCmbArea.DataBindings.Add("EditValue", bindingSiteDt, "AREA");
            cxChkBuildingLicense.DataBindings.Add("EditValue", bindingSiteDt, "BLDG_UNDER_LICENSE");
            cxTxtLandStatus.DataBindings.Add("EditValue", bindingSiteDt, "LAND_STATUS");
            cxImgCmbStackStatus.DataBindings.Add("EditValue", bindingSiteDt, "STACK_STATUS");

            //Plot range

            imgCmbBoxReleasedSales.DataBindings.Add("EditValue", bindingSiteDt, "SALES_RELEASE_STATUS");
            cxDateEditReleasedSales.DataBindings.Add("EditValue", bindingSiteDt, "SALES_RELEASE_DATE");

            cxTxtTitleRef.DataBindings.Add("EditValue", bindingSiteDt, "LAND_TITLE_REF");
            cxTxtBuyer.DataBindings.Add("EditValue", bindingSiteDt, "LAND_BUYER");
            cxTxtSiteSize.DataBindings.Add("EditValue", bindingSiteDt, "SITE_SIZE");
            cxTxtDescription.DataBindings.Add("EditValue", bindingSiteDt, "DESCRIPTION");
            cxTxtVendor.DataBindings.Add("EditValue", bindingSiteDt, "VENDOR");
            cxTxtPurchasePrice.DataBindings.Add("EditValue", bindingSiteDt, "PURCHASE_PRICE");
            cxTxtPayment.DataBindings.Add("EditValue", bindingSiteDt, "PAYMENT_TIMING");
            cxDBLookupCBPT.DataBindings.Add("EditValue", bindingSiteDt, "PURCHASE_TYPE_ID");

            cxChkPartExchange.DataBindings.Add("EditValue", bindingSiteDt, "PART_EXCHANGE");

            //cxTxtJVOperatingCompany.DataBindings.Add("EditValue", selectedDr, "REMARKS");
            cxTxtJVExternal.DataBindings.Add("EditValue", bindingSiteDt, "JV_EXTERNAL");
            cxTxtJVMgmt.DataBindings.Add("EditValue", bindingSiteDt, "JV_MNGT_FEE_PCT");

            cxTxtCreatedOn.DataBindings.Add("EditValue", bindingSiteDt, "CREATED_ON");
            cxTxtUpdatedOn.DataBindings.Add("EditValue", bindingSiteDt, "LAST_UPDATED");
            cxTxtUpdatedBy.DataBindings.Add("EditValue", bindingSiteDt, "CREATED_BY");

            cxImgCmboBoxStatus.DataBindings.Add("EditValue", bindingSiteDt, "STATUS");
            cxChkLandSale.DataBindings.Add("EditValue", bindingSiteDt, "LAND_SALE");
            cxLandDate.DataBindings.Add("EditValue", bindingSiteDt, "DATE_OF_SALE");
            cxMemoEditPurchaser.DataBindings.Add("EditValue", bindingSiteDt, "PURCHASER");
            cxMemoEditComments.DataBindings.Add("EditValue", bindingSiteDt, "LAND_SALE_COMMENTS");

            cxChkSiteCompleted.DataBindings.Add("EditValue", bindingSiteDt, "SITE_COMPLETED");
            cxSiteDate.DataBindings.Add("EditValue", bindingSiteDt, "COMPLETED_DATE");
            //cxMemoEditSitePurchaser.DataBindings.Add("EditValue", selectedDt, "");
            cxMemoEditSiteComments.DataBindings.Add("EditValue", bindingSiteDt, "COMPLETED_COMMENTS");

            cxChkSiteAborted.DataBindings.Add("EditValue", bindingSiteDt, "SITE_ABORTED");
            cxSiteAbortedDate.DataBindings.Add("EditValue", bindingSiteDt, "ABORTED_DATE");
            //cxMemoEditAbortedPurchaser.DataBindings.Add("EditValue", selectedDt, "");
            cxMemoEditAbortedComments.DataBindings.Add("EditValue", bindingSiteDt, "ABORTED_COMMENTS");

            dnSiteSetup.DataSource = bindingSiteDt;
            //bindingSiteDt.TableNewRow += bindingSiteDt_TableNewRow;

        }

        private void ClearBinding()
        {
            cxDBLookupCBComp.DataBindings.Clear();
            cxTxtEditSiteCode.DataBindings.Clear();
            cxlookUpDevelop.DataBindings.Clear();
            cxTxtEditMarket.DataBindings.Clear();
            cxTxtEditSiteName.DataBindings.Clear();
            cxTxtEditAddress.DataBindings.Clear();
            cxTxtEditAddress2.DataBindings.Clear();
            cxTxtEditAddress3.DataBindings.Clear();
            cxTxtEditPostCode.DataBindings.Clear();
            cxTxtEditSiteShort.DataBindings.Clear();
            cxDBLandTypeId.DataBindings.Clear();
            cxDBBuildTypeId.DataBindings.Clear();
            cxDBLookupCBLoc.DataBindings.Clear();
            cxImgCmbArea.DataBindings.Clear();
            cxChkBuildingLicense.DataBindings.Clear();
            cxTxtLandStatus.DataBindings.Clear();
            cxImgCmbStackStatus.DataBindings.Clear();

            //B4 date b3 date
            //Plot range

            imgCmbBoxReleasedSales.DataBindings.Clear();
            cxDateEditReleasedSales.DataBindings.Clear();

            cxTxtTitleRef.DataBindings.Clear();
            cxTxtBuyer.DataBindings.Clear();
            cxTxtSiteSize.DataBindings.Clear();
            cxTxtDescription.DataBindings.Clear();
            cxTxtVendor.DataBindings.Clear();
            cxTxtPurchasePrice.DataBindings.Clear();
            cxTxtPayment.DataBindings.Clear();
            cxDBLookupCBPT.DataBindings.Clear();

            cxChkPartExchange.DataBindings.Clear();

            cxTxtJVOperatingCompany.DataBindings.Clear();
            cxTxtJVExternal.DataBindings.Clear();
            cxTxtJVMgmt.DataBindings.Clear();

            cxTxtCreatedOn.DataBindings.Clear();
            cxTxtUpdatedOn.DataBindings.Clear();
            cxTxtUpdatedBy.DataBindings.Clear();

            cxImgCmboBoxStatus.DataBindings.Clear();
            cxChkLandSale.DataBindings.Clear();
            cxLandDate.DataBindings.Clear();
            cxMemoEditPurchaser.DataBindings.Clear();
            cxMemoEditComments.DataBindings.Clear();

            cxChkSiteCompleted.DataBindings.Clear();
            cxSiteDate.DataBindings.Clear();
            cxMemoEditSiteComments.DataBindings.Clear();

            cxChkSiteAborted.DataBindings.Clear();
            cxSiteAbortedDate.DataBindings.Clear();
            cxMemoEditAbortedComments.DataBindings.Clear();
            dnSiteSetup.DataBindings.Clear();
        }

        private void PopulateJVCompany()
        {
            if (cxDBLookupCBComp.EditValue == null || (cxDBLookupCBComp.EditValue != null && string.IsNullOrEmpty(cxDBLookupCBComp.EditValue.ToString()))) return;
            var resultJVCompany = dal.RetrieveJVCompany(Convert.ToDecimal(cxDBLookupCBComp.EditValue));
            if (resultJVCompany.Rows.Count > 0)
            {
                var dr = dtAllCompany.Select("COMPANY_ID = " + resultJVCompany.Rows[0]["OP_COMPANY_ID"]);
                if (dr.Length > 0)
                    cxTxtJVOperatingCompany.EditValue = dr[0]["NAME"].ToString();
            }
            else
            {
                cxTxtJVOperatingCompany.EditValue = null;
            }
        }

        private bool Validation()
        {
            if (drBindingSite["COMPANY_ID"] == DBNull.Value)
            {
                XtraMessageBox.Show("Company is mandatory.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (drBindingSite["SITE_CODE"] == DBNull.Value)
            {
                XtraMessageBox.Show("Site Code is mandatory.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (drBindingSite["SITE_NAME"] == DBNull.Value || (drBindingSite["SITE_NAME"] != DBNull.Value && string.IsNullOrEmpty(drBindingSite["SITE_NAME"].ToString())))
            {
                XtraMessageBox.Show("Site Name is mandatory.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (drBindingSite["ADDR_L1"] == DBNull.Value || (drBindingSite["ADDR_L1"] != DBNull.Value && string.IsNullOrEmpty(drBindingSite["ADDR_L1"].ToString())))
            {
                XtraMessageBox.Show("Site Address (Line 1) is mandatory.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            //TODO: ***********************************Plot**************************
            //       if Length(Trim(cdSiteSetupMAX_PLOT_NO.Value)) = 1 then
            //     cdSiteSetupMAX_PLOT_NO.Value := ''
            //else
            //begin
            //    cdSiteSetupMAX_PLOT_NO.Value := StringReplace(Trim(cdSiteSetupMAX_PLOT_NO.Value),' ','',[rfReplaceAll]);
            //end;

            //if (int_Old_Pt_No > StrToFloatDef(StringReplace(cdSiteSetupMAX_PLOT_NO.AsString,'P','',[rfReplaceAll]),0)) then
            ////StrToFloatDef(Copy(cdSiteSetupMAX_PLOT_NO.AsString , 2, length(cdSiteSetupMAX_PLOT_NO.AsString)),0) ) then
            //begin
            //   MessageDlg('The maximum plot number of a site cannot be reduced.',mtError,[mbOK],0);
            //   Abort;
            //end;

            //*******************************************************************

            AddBinding();

            int maxPlot = 0;
            if (mode == FormMode.Edit)
                maxPlot = dal.RetrieveMaxPlot(Convert.ToDecimal(drBindingSite["SITE_ID"]));
            //*************************Edit*************************
            if (mode == FormMode.Edit && cxChkPartExchange.EditValue != null && cxChkPartExchange.EditValue.ToString() == "Y" &&
                (maxPlot - 1) > 1)
            {
                XtraMessageBox.Show("Cannot change to Part Exchange, as more than 1 plot is created for the site.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            //b_Update_PlotIndicator = mode == FormMode.Edit &&
            //                         drBindingSite["PART_EXCHANGE", DataRowVersion.Original].ToString() == "N" &&
            //                         drBindingSite["PART_EXCHANGE", DataRowVersion.Current].ToString() == "Y" ? true : false;

            b_Update_PlotIndicator = mode == FormMode.Edit && cxChkPartExchange.EditValue != null &&
                                      cxChkPartExchange.EditValue.ToString() == "Y" ? true : false;

            //***********************************************************

            if (drBindingSite["SITE_ABORTED"].ToString() == "Y")
            {
                var resultDt = dal.CheckSiteAborted(Convert.ToDecimal(drBindingSite["COMPANY_ID"].ToString()), drBindingSite["SITE_CODE"].ToString());
                if (resultDt.Rows.Count > 0)
                {
                    if (Convert.ToInt32(resultDt.Rows[0]["CNT"]) > 0)
                    {
                        XtraMessageBox.Show("Units have been taken to sales. This site cannot be Aborted.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }

            if (drBindingSite["LAND_TYPE_ID"] == DBNull.Value) //on Additional Site Details Tab,
            {
                XtraMessageBox.Show("Land Type is mandatory.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (drBindingSite["BUILD_TYPE_ID"] == DBNull.Value)
            {
                XtraMessageBox.Show("Build Type is mandatory.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (drBindingSite["LOCATION_CODE"] == DBNull.Value)
            {
                XtraMessageBox.Show("Location is mandatory.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (mode == FormMode.New && bindingSitePlotDt.Rows.Count == 0)
            {
                XtraMessageBox.Show("Plot Range is mandatory.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            //var drFromDtOverlapping = bindingSitePlotDt.Select("PLOT_NO_TO >= PLOT_NO_FROM AND PLOT_NO_FROM < PLOT_NO_TO");

            //if (drFromDtOverlapping.Length > 0)
            //{
            //    XtraMessageBox.Show("From Date is overlapping with an existing date.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return false;
            //}



            if (((drBindingSite["SITE_ABORTED"].ToString() == "Y") && (drBindingSite["SITE_COMPLETED"].ToString() == "Y")) ||
            ((drBindingSite["SITE_ABORTED"].ToString() == "Y") && (drBindingSite["LAND_SALE"].ToString() == "Y")) ||
            ((drBindingSite["LAND_SALE"].ToString() == "Y") && (drBindingSite["SITE_COMPLETED"].ToString() == "Y")) ||
            ((drBindingSite["SITE_ABORTED"].ToString() == "Y") && (drBindingSite["SITE_COMPLETED"].ToString() == "Y") && (drBindingSite["LAND_SALE"].ToString() == "Y")))
            {
                XtraMessageBox.Show("A site can have any one status i.e. either Aborted, Completed or Land Sale.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (mode != FormMode.Edit)
            {
                if (drBindingSite["SITE_CODE"] != null && !string.IsNullOrEmpty(drBindingSite["SITE_CODE"].ToString()))
                {
                    int cnt = dal.ExistsSiteCode(Convert.ToDecimal(drBindingSite["COMPANY_ID"]), drBindingSite["SITE_CODE"].ToString());
                    if (cnt > 0)
                    {
                        XtraMessageBox.Show("Duplicate Site Code is not allowed.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }

            if (mode == FormMode.New && drBindingSite["SITE_CODE"].ToString().Substring(1, 3) == "B4-")
            {
                int cnt = b4SideCodeDal.CheckMaster(Convert.ToDecimal(drBindingSite["COMPANY_ID"]), drBindingSite["SITE_CODE"].ToString());
                if (cnt > 0)
                {
                    XtraMessageBox.Show("User entered Site Code cannot begin with B4-.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            if (drBindingSite["SITE_ABORTED"].ToString() == "Y" && drBindingSite["SITE_COMPLETED"].ToString() == "N" && drBindingSite["LAND_SALE"].ToString() == "N")
                drBindingSite["STATUS"] = "AB";
            else if (drBindingSite["SITE_COMPLETED"].ToString() == "Y" && drBindingSite["SITE_ABORTED"].ToString() == "N" && drBindingSite["LAND_SALE"].ToString() == "N")
                drBindingSite["STATUS"] = "CO";
            else if (drBindingSite["SITE_COMPLETED"].ToString() == "N" && drBindingSite["SITE_ABORTED"].ToString() == "N" && drBindingSite["LAND_SALE"].ToString() == "N")
                drBindingSite["STATUS"] = "AC";
            else if (drBindingSite["LAND_SALE"].ToString() == "Y" && drBindingSite["SITE_ABORTED"].ToString() == "N" && drBindingSite["SITE_COMPLETED"].ToString() == "N")
                drBindingSite["STATUS"] = "LS";

            return true;
        }

        private void cxChkSiteAborted_EditValueChanged(object sender, EventArgs e)
        {
            //if (drBindingSite == null) return;

            //drBindingSite["USE_IN_REPORT"] = cxChkSiteAborted.EditValue != null && cxChkSiteAborted.EditValue.ToString() == "Y" ? "N" : "Y";
        }

        private void gvDocument_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            DataRow dr = gvDocument.GetDataRow(e.RowHandle);
            if (dr == null) return;

            if (dr["DOCUMENT_NAME"] == DBNull.Value || string.IsNullOrWhiteSpace(Convert.ToString(dr["DOCUMENT_NAME"])))
            {
                XtraMessageBox.Show("Document Name is mandatory.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Valid = false;
                return;
            }

            if (dr["DESCRIPTION"] == DBNull.Value || string.IsNullOrWhiteSpace(Convert.ToString(dr["DESCRIPTION"])))
            {
                XtraMessageBox.Show("Document Description is mandatory.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Valid = false;
                return;
            }

            dr["CREATED_BY"] = ProfitCashflow.oPcfDM.UserName;
            dr["LAST_UPDATED"] = DateTime.Now;
        }

        private void gvDocument_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            if (bindingSiteDt != null)
            {
                DataRow dr = gvDocument.GetDataRow(e.RowHandle);
                dr["DOCUMENT_ID"] = GenerateLineNumber();
                dr["SITE_ID"] = drBindingSite["SITE_ID"];
                dr["CREATED_BY"] = ProfitCashflow.oPcfDM.UserName;
                dr["CREATED_ON"] = DateTime.Now;
                dr["LAST_UPDATED"] = DateTime.Now;
            }
        }

        private void gvPlot_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            if (bindingSiteDt != null)
            {
                DataRow dr = gvPlot.GetDataRow(e.RowHandle);
                dr["SITE_ID"] = drBindingSite["SITE_ID"];
                dr["CREATED_BY"] = ProfitCashflow.oPcfDM.UserName;
                dr["CREATED_ON"] = DateTime.Now.Date;
            }
        }

        private void gvPlot_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            if (mode == FormMode.Browse) return;
            DataRow dr = gvPlot.GetDataRow(e.RowHandle);
            if (dr == null) return;

            if (dr["PLOT_NO_FROM"] == DBNull.Value || string.IsNullOrWhiteSpace(Convert.ToString(dr["PLOT_NO_FROM"])))
            {
                XtraMessageBox.Show("From is mandatory.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Valid = false;
                return;
            }

            if (dr["PLOT_NO_TO"] == DBNull.Value || string.IsNullOrWhiteSpace(Convert.ToString(dr["PLOT_NO_TO"])))
            {
                XtraMessageBox.Show("To is mandatory.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Valid = false;
                return;
            }

            if (!string.IsNullOrWhiteSpace(Convert.ToString(dr["PLOT_NO_FROM"])) &&
                !string.IsNullOrWhiteSpace(Convert.ToString(dr["PLOT_NO_TO"])) &&
               Convert.ToInt32(dr["PLOT_NO_FROM"]) > Convert.ToInt32(dr["PLOT_NO_TO"]))
            {
                XtraMessageBox.Show("To Range cannot be less than From Range.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Valid = false;
                return;
            }

            //if (!string.IsNullOrWhiteSpace(Convert.ToString(dr["PLOT_NO_FROM"])) &&
            //    !string.IsNullOrWhiteSpace(Convert.ToString(dr["PLOT_NO_TO"])) &&
            //    Convert.ToInt32(dr["PLOT_NO_FROM", DataRowVersion.Original]) > Convert.ToInt32(dr["PLOT_NO_FROM", DataRowVersion.Current]))
            {
            }

            var drStartDtOverlapping = bindingSitePlotDt.Select("PLOT_NO_FROM <= " + Convert.ToInt32(dr["PLOT_NO_FROM"]).ToString(CultureInfo.InvariantCulture) +
                                                     " AND PLOT_NO_TO >= " + Convert.ToInt32(dr["PLOT_NO_FROM"]).ToString(CultureInfo.InvariantCulture) + "");
            if (drStartDtOverlapping.Length > 0)
            {
                foreach (DataRow drOverlapping in drStartDtOverlapping)
                    if (drOverlapping.Equals(dr))
                        continue;
                    else
                    {
                        XtraMessageBox.Show("From is overlapping with an existing range.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        e.Valid = false;
                        return;
                    }
            }

            var drToDtOverlapping = bindingSitePlotDt.Select("PLOT_NO_FROM <= " + Convert.ToInt32(dr["PLOT_NO_TO"]).ToString(CultureInfo.InvariantCulture) +
                                                      " AND PLOT_NO_TO >= " + Convert.ToInt32(dr["PLOT_NO_TO"]).ToString(CultureInfo.InvariantCulture) + "");
            if (drToDtOverlapping.Length > 0)
            {
                foreach (DataRow drOverlapping in drToDtOverlapping)
                    if (drOverlapping.Equals(dr))
                        continue;
                    else
                    {
                        XtraMessageBox.Show("To is overlapping with an existing range.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        e.Valid = false;
                        return;
                    }
            }

            var drBothOverlapping = bindingSitePlotDt.Select("PLOT_NO_FROM >= " + Convert.ToInt32(dr["PLOT_NO_FROM"]).ToString(CultureInfo.InvariantCulture) +
                                                  " AND PLOT_NO_TO <= " + Convert.ToInt32(dr["PLOT_NO_TO"]).ToString(CultureInfo.InvariantCulture) + "");
            if (drBothOverlapping.Length > 0)
            {
                foreach (DataRow drOverlapping in drBothOverlapping)
                    if (drOverlapping.Equals(dr))
                        continue;
                    else
                    {
                        XtraMessageBox.Show("From and To are overlapping with an existing range.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        e.Valid = false;
                        return;
                    }
            }
        }

        private void gv_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private int GenerateLineNumber()
        {
            int GenlineNo = 0;
            try
            {
                var ds = dal.RetrieveSiteById(Convert.ToDecimal(drBindingSite["SITE_ID"]));
                if (ds.Tables[1].Rows.Count == 0)
                {
                    GenlineNo = 1;
                }
                else
                {
                    DataView dv = new DataView(ds.Tables[1], "", "DOCUMENT_ID DESC", DataViewRowState.CurrentRows);
                    GenlineNo = Convert.ToInt32(dv.ToTable().Rows[0]["DOCUMENT_ID"]) + 1;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return GenlineNo;
        }

        private void SetBackColorOfStatus(DataRow dr)
        {
            if (dr == null) return;

            if (dr["STATUS"].ToString() == "AC")
            {
                cxImgCmboBoxStatus.BackColor = System.Drawing.Color.FromArgb(193, 193, 255);
            }
            else if (dr["STATUS"].ToString() == "AB")
            {
                cxImgCmboBoxStatus.BackColor = System.Drawing.Color.FromArgb(255, 66, 66);
            }
            else if (dr["STATUS"].ToString() == "CO")
            {
                cxImgCmboBoxStatus.BackColor = System.Drawing.Color.FromArgb(0, 183, 0);
            }
            else if (dr["STATUS"].ToString() == "LS")
            {
                cxImgCmboBoxStatus.BackColor = System.Drawing.Color.FromArgb(255, 187, 119);
            }
        }

        private void MakeControlEnable(string mode, bool enable)
        {
            cxDBLookupCBComp.ReadOnly = enable;
            cxlookUpDevelop.ReadOnly = enable;
            cxTxtEditMarket.ReadOnly = enable;
            cxTxtEditSiteName.ReadOnly = enable;
            cxTxtEditAddress.ReadOnly = enable;
            cxTxtEditAddress2.ReadOnly = enable;
            cxTxtEditAddress3.ReadOnly = enable;
            cxTxtEditPostCode.ReadOnly = enable;
            cxTxtEditSiteShort.ReadOnly = enable;
            cxDBLandTypeId.ReadOnly = enable;
            cxDBBuildTypeId.ReadOnly = enable;
            cxDBLookupCBLoc.ReadOnly = enable;
            cxImgCmbArea.ReadOnly = enable;
            cxChkBuildingLicense.ReadOnly = enable;
            //cxImgCmbStackStatus.ReadOnly = enable;
            cxImgCmboBoxStatus.ReadOnly = enable;
            cxTxtJVOperatingCompany.ReadOnly = enable;
            imgCmbBoxReleasedSales.ReadOnly = enable;

            cxTxtCreatedOn.ReadOnly = enable;
            cxTxtUpdatedOn.ReadOnly = enable;
            cxTxtUpdatedBy.ReadOnly = enable;

            cxTxtTitleRef.ReadOnly = enable;
            cxTxtBuyer.ReadOnly = enable;
            cxTxtSiteSize.ReadOnly = enable;
            cxTxtDescription.ReadOnly = enable;
            cxTxtVendor.ReadOnly = enable;
            cxTxtPurchasePrice.ReadOnly = enable;
            cxTxtPayment.ReadOnly = enable;
            cxDBLookupCBPT.ReadOnly = enable;
            cxChkPartExchange.ReadOnly = enable;
            cxTxtJVExternal.ReadOnly = enable;
            cxTxtJVMgmt.ReadOnly = enable;

            cxChkLandSale.ReadOnly = enable;
            cxLandDate.ReadOnly = enable;
            cxMemoEditPurchaser.ReadOnly = enable;
            cxMemoEditComments.ReadOnly = enable;
            cxChkSiteCompleted.ReadOnly = enable;
            cxSiteDate.ReadOnly = enable;
            cxMemoEditSiteComments.ReadOnly = enable;
            cxChkSiteAborted.ReadOnly = enable;
            cxSiteAbortedDate.ReadOnly = enable;
            cxMemoEditAbortedComments.ReadOnly = enable;
            //gvLPD.OptionsBehavior.Editable = enable;
            gvPlot.OptionsBehavior.ReadOnly = enable;
            gvPlot.OptionsBehavior.Editable = !enable;

            gvDocument.OptionsBehavior.ReadOnly = enable;
            gvDocument.OptionsBehavior.Editable = !enable;
            if (mode == "N")
            {
                //cxTxtEditSiteCode.ReadOnly = !enable;
                cxTxtJVOperatingCompany.ReadOnly = !enable;
                imgCmbBoxReleasedSales.ReadOnly = !enable;

                //cxTxtLandStatus.ReadOnly = !enable;
                cxImgCmboBoxStatus.ReadOnly = !enable;
                //cxImgCmbStackStatus.ReadOnly = !enable;
                cxTxtCreatedOn.ReadOnly = !enable;
                cxTxtUpdatedOn.ReadOnly = !enable;
                cxTxtUpdatedBy.ReadOnly = !enable;
                gvPlot.OptionsBehavior.ReadOnly = enable;
                gvPlot.OptionsBehavior.Editable = !enable;
                gvDocument.OptionsBehavior.ReadOnly = enable;
                gvDocument.OptionsBehavior.Editable = !enable;

                cxBtnPrev.Enabled = false;
                splitContainerControl1.Panel1.Enabled = false;
                cxTxtJVOperatingCompany.ReadOnly = true;
                gvDocument.OptionsBehavior.Editable = true;
                cxDBLookupCBComp.Enabled = true;
            }
            if (mode == "E")
            {
                cxDBLookupCBComp.Enabled = false;
                //cxTxtEditSiteCode.Enabled = false;
                cxImgCmboBoxStatus.Enabled = false;
                imgCmbBoxReleasedSales.Enabled = false;
                cxTxtCreatedOn.Enabled = false;
                cxTxtUpdatedOn.Enabled = false;
                cxTxtUpdatedBy.Enabled = false;
                cxTxtJVOperatingCompany.Enabled = false; ////////cxTextEdit1.Enabled = False;
                gvPlot.OptionsBehavior.ReadOnly = enable;
                gvPlot.OptionsBehavior.Editable = !enable;

                gvDocument.OptionsBehavior.ReadOnly = enable;
                gvDocument.OptionsBehavior.Editable = !enable;

                cxBtnPrev.Enabled = false;
                splitContainerControl1.Panel1.Enabled = false;
                cxTxtJVOperatingCompany.ReadOnly = !enable;
            }
        }

        private void MakeButtonEnableDisable(bool bNewEnable, bool bEditEnable, bool bSaveEnable, bool bCancelEnable)
        {
            btnNew.Enabled = bNewEnable;
            btnEdit.Enabled = bEditEnable;
            btnSave.Enabled = bSaveEnable;
            btnCancel.Enabled = bCancelEnable;
            btnDelete.Enabled = bEditEnable;
            btnSearch.Enabled = bNewEnable || bEditEnable;
            btnReleaseToSales.Enabled = cxGrid2DBTableView1.RowCount == 0 ? false : true;
            btnProgressToNextLand.Enabled = cxGrid2DBTableView1.RowCount == 0 ? false : true;
        }

        private void PopulateLandProgressDates()
        {
            try
            {
                DataTable progressDatesDt = new DataTable();
                progressDatesDt.Columns.Add("B4_DATE", typeof(DateTime));
                progressDatesDt.Columns.Add("B3_DATE", typeof(DateTime));
                progressDatesDt.Columns.Add("B2_DATE", typeof(DateTime));
                progressDatesDt.Columns.Add("B1_DATE", typeof(DateTime));
                progressDatesDt.Columns.Add("ATL_DATE", typeof(DateTime));

                drBindingSite["B4_DATE"] = DateTime.Now;
                progressDatesDt.Rows.Add(drBindingSite["B4_DATE"],
                                         drBindingSite["B3_DATE"],
                                         drBindingSite["B2_DATE"],
                                         drBindingSite["B1_DATE"],
                                         drBindingSite["ATL_DATE"]);
                gcLPD.DataSource = progressDatesDt;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadSingleSiteSetup(decimal siteId)
        {
            drBindingSite = null;

            var ds = dal.RetrieveSiteById(siteId);

            if (ds == null) return;
            bindingSiteDt = ds.Tables[0];

            if (bindingSiteDt != null && bindingSiteDt.Rows.Count > 0)
            {
                drBindingSite = ds.Tables[0].Rows[0];
                bindingSiteDocDt = ds.Tables[1];
                bindingSitePlotDt = ds.Tables[2];
            }

            if (drBindingSite == null) return;

            DataTable progressDatesDt = new DataTable();
            progressDatesDt.Columns.Add("B4_DATE", typeof(DateTime));
            progressDatesDt.Columns.Add("B3_DATE", typeof(DateTime));
            progressDatesDt.Columns.Add("B2_DATE", typeof(DateTime));
            progressDatesDt.Columns.Add("B1_DATE", typeof(DateTime));
            progressDatesDt.Columns.Add("ATL_DATE", typeof(DateTime));
            progressDatesDt.Rows.Add(drBindingSite["B4_DATE"],
                                     drBindingSite["B3_DATE"],
                                     drBindingSite["B2_DATE"],
                                     drBindingSite["B1_DATE"],
                                     drBindingSite["ATL_DATE"]);
            gcLPD.DataSource = progressDatesDt;
            gcPlot.DataSource = bindingSitePlotDt;
            gcDocument.DataSource = bindingSiteDocDt;


        }

        private bool SaveSiteSetup()
        {
            try
            {
                bool vSuccess = true;
                string vJV_CompCode;

                cxDBLookupCBComp.DoValidate();
                cxTxtEditSiteCode.DoValidate();
                cxlookUpDevelop.DoValidate();
                cxTxtEditMarket.DoValidate();
                cxTxtEditSiteName.DoValidate();
                cxTxtEditAddress.DoValidate();
                cxTxtEditAddress2.DoValidate();
                cxTxtEditAddress3.DoValidate();
                cxTxtEditPostCode.DoValidate();
                cxTxtEditSiteShort.DoValidate();
                cxDBLandTypeId.DoValidate();
                cxDBBuildTypeId.DoValidate();
                cxDBLookupCBLoc.DoValidate();
                cxImgCmbArea.DoValidate();
                cxChkBuildingLicense.DoValidate();
                cxTxtLandStatus.DoValidate();
                cxImgCmbStackStatus.DoValidate();

                imgCmbBoxReleasedSales.DoValidate();
                cxDateEditReleasedSales.DoValidate();
                cxTxtTitleRef.DoValidate();
                cxTxtBuyer.DoValidate();
                cxTxtSiteSize.DoValidate();
                cxTxtDescription.DoValidate();
                cxTxtVendor.DoValidate();
                cxTxtPurchasePrice.DoValidate();
                cxTxtPayment.DoValidate();
                cxDBLookupCBPT.DoValidate();
                cxChkPartExchange.DoValidate();
                cxTxtJVOperatingCompany.DoValidate();
                cxTxtJVExternal.DoValidate();
                cxTxtJVMgmt.DoValidate();
                cxTxtCreatedOn.DoValidate();
                cxTxtUpdatedOn.DoValidate();
                cxTxtUpdatedBy.DoValidate();
                cxImgCmboBoxStatus.DoValidate();
                cxChkLandSale.DoValidate();
                cxLandDate.DoValidate();
                cxMemoEditPurchaser.DoValidate();
                cxMemoEditComments.DoValidate();
                cxChkSiteCompleted.DoValidate();
                cxSiteDate.DoValidate();
                cxMemoEditSiteComments.DoValidate();
                cxChkSiteAborted.DoValidate();
                cxSiteAbortedDate.DoValidate();
                cxMemoEditAbortedComments.DoValidate();

                gvPlot.PostEditor();
                gvPlot.UpdateCurrentRow();

                gvDocument.PostEditor();
                gvDocument.UpdateCurrentRow();

                if (mode == FormMode.Edit)
                {
                    int cnt = commonDal.RetrievePcfLockedDocuments(Convert.ToDecimal(drBindingSite["PCF_DOC_GUID"]), ProfitCashflow.oPcfDM.session_id);
                    if (cnt == 0)
                    {
                        XtraMessageBox.Show("Lock on selected Site Setup has been released. Contact system administrator.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }

                if (!Validation()) return false;

                dnSiteSetup.Buttons.DoClick(dnSiteSetup.Buttons.EndEdit);

                if ((bindingSiteDt.GetChanges() != null && bindingSiteDt.GetChanges().Rows.Count > 0) ||
                    (bindingSiteDocDt.GetChanges() != null && bindingSiteDocDt.GetChanges().Rows.Count > 0))
                {
                    vSuccess = true;

                    if (drBindingSite["JV_COMPANY_ID"] != DBNull.Value && Convert.ToDecimal(drBindingSite["JV_COMPANY_ID"]) > 0 &&
                    dtAllCompany.Select("COMPANY_ID = " + drBindingSite["JV_COMPANY_ID"]).Length > 0)
                    {
                        var dr = dtAllCompany.Select("COMPANY_ID = " + drBindingSite["COMPANY_ID"]);
                        if (dr.Length > 0)
                        {
                            vJV_CompCode = dr[0]["COMPANY_CODE"].ToString();
                        }
                    }
                }

                if (vSuccess)
                {
                    if (mode == FormMode.New)
                    {
                        var result = dal.UpdateSiteSetup("I", bindingSiteDt, bindingSitePlotDt, bindingSiteDocDt, int_newGUID, int_newid);
                        if (!string.IsNullOrEmpty(result))
                        {
                            XtraMessageBox.Show("Error while creating System Created Stack : " + result);
                            return false;
                        }
                    }
                    if (mode == FormMode.Edit)
                        dal.UpdateSiteSetup("U", bindingSiteDt, bindingSitePlotDt, bindingSiteDocDt, int_newGUID, int_newid);
                    //if (mode == FormMode.Delete)
                    //    dal.UpdateSiteSetup("D", ds);

                    bindingSiteDt.AcceptChanges();
                    bindingSitePlotDt.AcceptChanges();
                    bindingSiteDocDt.AcceptChanges();

                    //bindingSiteDocDt.AcceptChanges();


                    //////Updating the Plot_Indicators==
                    ////if (b_Update_PlotIndicator) 
                    ////   Change_PlotIndicator(pSiteSetupDM.cdSiteSetupCOMPANY_ID.AsFloat,pSiteSetupDM.cdSiteSetupSITE_CODE.AsString);
                    //////===============================

                    //////ShowMessage('Before PcfDM.SQLConnection1.Commit');
                    ////PcfDM.SQLConnection1.Commit(PcfDM.TD);
                    ////      //ShowMessage('After PcfDM.SQLConnection1.Commit');

                    ////if b_Phase_ch then
                    ////      pSiteSetupDM.Update_Stack_phase(pSiteSetupDM.cdSiteSetupCOMPANY_ID.AsInteger,
                    ////                                      pSiteSetupDM.cdSiteSetupSYS_CREATED_STACK_ID.AsInteger,
                    ////                                      pSiteSetupDM.cdSiteSetupSITE_CODE.AsString);

                    ////if not b_Insert then
                    ////begin
                    ////      //ShowMessage('Before pSiteSetupDM.Update_Sys_Plots');
                    ////      pSiteSetupDM.Update_Sys_Plots( pSiteSetupDM.cdSiteSetupSYS_CREATED_STACK_ID.AsInteger ,
                    ////                                     pSiteSetupDM.cdSiteSetupCOMPANY_ID.AsInteger,
                    ////                                     pSiteSetupDM.cdSiteSetupSITE_CODE.AsString);
                    ////      pSiteSetupDM.int_Old_Pt_No := 0;
                    ////end;

                    if (upd_CSV_FLAG == "Y")
                    {
                        dal.UpdateCurrentSeedValue(upd_Comp_Id, upd_CSV);
                    }
                }
                else
                {
                    XtraMessageBox.Show("'Data has not been saved to the PCF database successfully, as Site Setup data did not post to CODA.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                // if any change
                if (bindingSiteDt.GetChanges() == null || bindingSiteDocDt.GetChanges() == null)
                {
                    commonDal.DeletePCFLockedDocument(Convert.ToDecimal(drBindingSite["PCF_DOC_GUID"]), ProfitCashflow.oPcfDM.session_id);

                    if (b_Insert)
                    {
                        str_site_id = 0;

                        if (splitContainerControl1.Panel1.Enabled && cxLookupCBSearchSite.EditValue == null)
                        {
                            str_site_id = Convert.ToInt32(drBindingSite["SITE_ID"]);
                            btnSearch_Click(null, null);
                        }
                        if (splitContainerControl1.Panel1.Enabled && cxLookupCBSearchSite.EditValue != null)
                        {
                            str_site_id = Convert.ToInt32(drBindingSite["SITE_ID"]);
                            cxLookupCBSearchSite.EditValue = str_site_id;
                            btnSearch_Click(null, null);
                        }
                    }

                    pIns = false;
                    pEdit = false;
                    b_Insert = false;
                }

                //cxGrid2DBTableView1.OptionsBehavior.Editable = fal; //((bindingSiteDt.GetChanges() != null && bindingSiteDt.GetChanges().Rows.Count == 0) && (bindingSiteDocDt.GetChanges() != null && bindingSiteDocDt.GetChanges().Rows.Count == 0));
                gvDocument.OptionsBehavior.Editable = bindingSiteDt.GetChanges() == null && bindingSiteDocDt.GetChanges() == null;

                SetBackColorOfStatus(drBindingSite);

                cxDBLookupCBComp.Enabled = true;
                //cxTxtEditSiteCode.Enabled = false;
                cxTxtJVOperatingCompany.Enabled = false; //cxTextEdit1

                cxBtnPrev.Enabled = true;
                splitContainerControl1.Panel1.Enabled = true;

                btnNew.Enabled = bindingSiteDt.GetChanges() == null && bindingSiteDocDt.GetChanges() == null;
                btnEdit.Enabled = bindingSiteDt.GetChanges() == null && bindingSiteDocDt.GetChanges() == null;
                btnSave.Enabled = bindingSiteDt.GetChanges() != null || bindingSiteDocDt.GetChanges() != null;
                btnCancel.Enabled = bindingSiteDt.GetChanges() != null || bindingSiteDocDt.GetChanges() != null;
                btnDelete.Enabled = bindingSiteDt.GetChanges() == null && bindingSiteDocDt.GetChanges() == null;
                btnSearch.Enabled = bindingSiteDt.GetChanges() == null && bindingSiteDocDt.GetChanges() == null;

                btnReleaseToSales.Enabled = bindingSiteDt.GetChanges() == null && bindingSiteDocDt.GetChanges() == null;
                btnProgressToNextLand.Enabled = bindingSiteDt.GetChanges() == null && bindingSiteDocDt.GetChanges() == null;

                MakeControlEnable("B", true);

                //pSiteSetupDM.qStackSummary.Close;
                //pSiteSetupDM.qStackSummary.ParamByName('STACK_ID').AsFloat := pSiteSetupDM.cdSiteSetupSYS_CREATED_STACK_ID.AsFloat; //pSiteSetupDM.qMaxStackSTACK_ID.AsInteger;
                //pSiteSetupDM.qStackSummary.Open;

                DataRow drSiteTemp = bindingSiteSearchDt.Rows.Find(drBindingSite["SITE_ID"]);

                if (drSiteTemp != null)
                {
                    cxGrid2DBTableView1.FocusedRowHandle = cxGrid2DBTableView1.GetRowHandle(bindingSiteSearchDt.Rows.IndexOf(drSiteTemp));
                }
                DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs eventArg = new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, cxGrid2DBTableView1.FocusedRowHandle);

                cxGrid2DBTableView1_FocusedRowChanged(cxGrid2DBTableView1, eventArg);

                //btnRefresh_Click(null, null);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return true;
        }

        private void cdSiteSetupAfterScroll()
        {
            LoadSingleSiteSetup(Convert.ToDecimal(drBindingSite["SITE_ID"]));
        }

        enum FormMode
        {
            New,
            Edit,
            Browse,
            Cancel,
            Delete,
            Save
        };

        private void gvDocument_KeyDown(object sender, KeyEventArgs e)
        {
            if (mode != FormMode.Browse && gvDocument.OptionsBehavior.Editable)
                if (e.KeyCode == Keys.Delete && gvDocument.FocusedRowHandle > -1)
                {
                    DialogResult oTempResult = XtraMessageBox.Show("Delete record?", Application.ProductName, MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (oTempResult == DialogResult.OK)
                    {
                        DataRow drDoc = gvDocument.GetDataRow(gvDocument.FocusedRowHandle);
                        if (drDoc.RowState == DataRowState.Added)
                            drDoc.RejectChanges();
                        else
                            drDoc.Delete();
                    }
                }
        }

        private void gvDocument_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            MakeStackDocColumnEnable();
        }

        private void gvDocument_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            MakeStackDocColumnEnable();
        }

        private void MakeStackDocColumnEnable()
        {
            repBtnEditDoc.ReadOnly = false;
            repHyperLinkDoc.ReadOnly = false;
            if (mode == FormMode.Browse)
                if (gvDocument.FocusedColumn == cxGrid1DBTableView1DOCUMENT_NAME || gvDocument.FocusedColumn == cxGrid1DBTableView1HYPER_LINK)
                {
                    gvDocument.OptionsBehavior.Editable = true;
                    repBtnEditDoc.ReadOnly = gvDocument.FocusedColumn == cxGrid1DBTableView1DOCUMENT_NAME;
                    repHyperLinkDoc.ReadOnly = gvDocument.FocusedColumn == cxGrid1DBTableView1HYPER_LINK;
                }
                else
                    gvDocument.OptionsBehavior.Editable = false;
        }

        private void repBtnEditDoc_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            DataRow drDoc = gvDocument.GetDataRow(gvDocument.FocusedRowHandle);
            if (mode != FormMode.Browse && drDoc == null && gvDocument.IsNewItemRow(gvDocument.FocusedRowHandle))
            {
                gvDocument.AddNewRow();
                drDoc = gvDocument.GetDataRow(gvDocument.FocusedRowHandle);
            }

            if (e.Button.Index == 0)
            {
                if (mode != FormMode.Browse && btnSave.Enabled)
                {
                    OpenDialog1.Title = "Select a Document";
                    if (OpenDialog1.ShowDialog() == DialogResult.OK)
                        drDoc["DOCUMENT_NAME"] = OpenDialog1.FileName;
                }
            }
            else if (e.Button.Index == 1)
            {
                if (drDoc != null && drDoc["DOCUMENT_NAME"] != DBNull.Value && !string.IsNullOrWhiteSpace(drDoc["DOCUMENT_NAME"].ToString()))
                {
                    System.Diagnostics.Process oFileProcess = new System.Diagnostics.Process();
                    oFileProcess.StartInfo = new System.Diagnostics.ProcessStartInfo("IExplore.exe", drDoc["DOCUMENT_NAME"].ToString());
                    oFileProcess.StartInfo.UseShellExecute = true;
                    //oFileProcess.StartInfo.FileName = drStkDoc["DOCUMENT_NAME"].ToString();
                    oFileProcess.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Maximized;
                    oFileProcess.Start();
                }
            }
        }

        private void cxLookUpRegion_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                cxLookUpRegion.EditValue = null;
                cxLookupCBSearchComp.EditValue = null;
            }
        }

        private void cxLookupCBSearchComp_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                cxLookupCBSearchComp.EditValue = null;
            }
        }

        private void cxlookUpSearchDevelpmnt_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                cxlookUpSearchDevelpmnt.EditValue = null;
            }
        }

        private void cxLookupCBSearchSite_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                cxLookupCBSearchSite.EditValue = null;
            }
        }
    }
}