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
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Controls;

namespace Xpedeon.ProfitCashflow
{
    public partial class Stacks : DevExpress.XtraEditors.XtraForm
    {
        public string Flag, LSettingsDir;

        char lFormMode = 'B';   //'B'=Browse;'N'=New;'E'=Edit
        decimal Site_Doc_Guid;
        bool b_INSUPDEL, pInsStack, Site_lock, pInsAddPlots, pUpdtUseInRep;
        string vLandStatus, vStackStatus, vActive;
        DateTime Forecast_TTS_Date;

        DataRow drStack, drStkSiteDet, drStkUserRights;
        DataTable dtStack, dtStackPhaseDet, dtStackBlockDet, dtStackCoreDet, dtStackHistory, dtStackDocs, dtStackPlotDet;
        DataTable dtStkPFInfo, dtStackSumm, dtStackView, dtActiveStack, dtStkSysPhase, dtPlotRange;
        DataTable dtCompanyLookup, dtSiteLookup, dtRegionLookup, dtDevLookup, dtCompany, dtSites, dtSitesNew, dtPlotTypeMast, dtResTypeMas;

        sCommonMethods oComm = new sCommonMethods();
        sStacksDataModule oStkDM = new sStacksDataModule();

        string sQuery, sOptSiteCode = "";
        List<KeyValuePair<string, object>> oParameter;
        DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repIcbPlotInd;
        decimal dGrupSaleRev = 0, dGrupMargin = 0, dTotSaleRev = 0, dTotMargin = 0, dOptCompId = -1;

        public Stacks()
        {
            InitializeComponent();
        }

        private void Stacks_Shown(object sender, EventArgs e)
        {
            try
            {
                cxGridPlotSetupTv.BeginSort(); cxGridPlotSetupTv.EndSort();
                cxGridPhaseTv.BeginSort(); cxGridPhaseTv.EndSort();
                cxGridBlockDBTableView1.BeginSort(); cxGridBlockDBTableView1.EndSort();
                cxGridCoreDBTableView1.BeginSort(); cxGridCoreDBTableView1.EndSort();
                cxGridStackDocsTv.BeginSort(); cxGridStackDocsTv.EndSort();
                cxGrid2DBTableView2.BeginSort(); cxGrid2DBTableView2.EndSort();
                CxGridHistorytv.BeginSort(); CxGridHistorytv.EndSort();

                DataModuleCreate();

                cxLookupComboBoxRegion.Properties.DataSource = dtRegionLookup;
                cxLookupComboBoxCompany.Properties.DataSource = dtCompanyLookup;
                cxLookupComboBoxDev.Properties.DataSource = dtDevLookup;
                cxLookupComboBoxSiteCode.Properties.DataSource = dtSiteLookup;

                cxDBLookupComboBoxComp.Properties.DataSource = dtCompany;
                cxDBLookupComboBoxSite.Properties.DataSource = dtSites;
                cxLookupComboBoxPlotTypeForPlot.Properties.DataSource = dtPlotTypeMast;
                cxLookupComboBoxPlotResType.Properties.DataSource = dtResTypeMas;

                repLookUpPlotType.DataSource = dtPlotTypeMast;
                repLookUpUnitCat.DataSource = dtResTypeMas;
                repIcbPlotInd = (DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox)repIcbPlotIndicator.Clone();

                //AddBinding();
                MakeControlsEnable(false);
                MakeButtonEnableDisable(true, false, false, false);
                MakeFunctionBtnEnableDisable(false);

                splitContainerControl1.Collapsed = true;    //cxSplitter1.CloseSplitter;
                cxPageControl1.SelectedTabPage = cxTabSheetStackDet;

                panel3.BackColor = Color.FromArgb(0, 183, 0);
                panel5.BackColor = Color.FromArgb(255, 66, 66);
                panel4.BackColor = Color.FromArgb(255, 157, 60);
                panel6.BackColor = Color.Green;
                panel8.BackColor = Color.FromArgb(255, 130, 255);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DataModuleCreate()
        {
            dtRegionLookup = oComm.RetrieveRegionMasterList();
            dtRegionLookup.Constraints.Add("PK_REG", dtRegionLookup.Columns["REGION_ID"], true);

            dtDevLookup = oComm.RetrieveDevelopmentMasterList();
            dtDevLookup.Constraints.Add("PK_DEV", dtDevLookup.Columns["DEVELOPMENT_ID"], true);

            dtCompany = oStkDM.RetrieveStackCompanyList(ProfitCashflow.oPcfDM.UserName);
            dtCompany.Constraints.Add("PK_COMP", dtCompany.Columns["COMPANY_ID"], true);

            dtPlotTypeMast = oStkDM.RetrievePlotTypeMaster();
            dtPlotTypeMast.Constraints.Add("PK_PLOT", dtPlotTypeMast.Columns["PLOT_TYPE_ID"], true);

            dtResTypeMas = oStkDM.RetrieveResTypeMaster();
            dtResTypeMas.Constraints.Add("PK_RES", new DataColumn[] { dtResTypeMas.Columns["PLOT_TYPE_ID"], dtResTypeMas.Columns["RESIDENCE_TYPE_ID"] }, true);

            LoadSingleStack(-1);
        }

        private void MakeControlsEnable(bool bEnabled)
        {
            cxLookupComboBoxRegion.Properties.ReadOnly = bEnabled;
            cxLookupComboBoxRegion.Properties.Buttons[1].Enabled = !bEnabled;
            cxLookupComboBoxCompany.Properties.ReadOnly = bEnabled;
            cxLookupComboBoxDev.Properties.ReadOnly = bEnabled;
            cxLookupComboBoxDev.Properties.Buttons[1].Enabled = !bEnabled;
            cxLookupComboBoxSiteCode.Properties.ReadOnly = bEnabled;
            cxImageComboBoxShowCompanies.Properties.ReadOnly = bEnabled;
            cxGridStackView.Enabled = !bEnabled;

            cxButtonRefresh.Enabled = lFormMode == 'B' ? true : false;
            cxBtnPrev.Enabled = lFormMode == 'B' ? true : false;

            cxDBLookupComboBoxComp.Properties.ReadOnly = lFormMode == 'N' ? false : true;
            cxDBLookupComboBoxSite.Properties.ReadOnly = lFormMode == 'N' ? false : true;

            cxDBImageComboBox3.Properties.ReadOnly = !bEnabled;
            cxDBMemo1.Properties.ReadOnly = !bEnabled;
            cxDBCurrencyEditTotPlots.Properties.ReadOnly = !bEnabled;
            cxDBCurrencyEditTotPhases.Properties.ReadOnly = !bEnabled;
            cxDBCurrencyEditTotBlocks.Properties.ReadOnly = !bEnabled;
            cxDBCurrencyEditTotCores.Properties.ReadOnly = !bEnabled;

            cxGridHistory.Enabled = lFormMode == 'B' ? true : false;
            cxGridPhaseTv.OptionsBehavior.Editable = bEnabled;
            cxGridBlockDBTableView1.OptionsBehavior.Editable = bEnabled;
            cxGridCoreDBTableView1.OptionsBehavior.Editable = bEnabled;
            cxGridPlotSetupTv.OptionsBehavior.Editable = bEnabled;

            cxGridStackDocsTv.OptionsBehavior.Editable = bEnabled;
            MakeStackDocColumnEnable();
        }

        private void MakeButtonEnableDisable(bool bNewEnable, bool bEditEnable, bool bSaveEnable, bool bCancelEnable)
        {
            dxBarLargeButtonNew.Enabled = bNewEnable;
            dxBarLargeButtonEdit.Enabled = bEditEnable;
            dxBarLargeButtonSave.Enabled = bSaveEnable;
            dxBarLargeButtonCancel.Enabled = bCancelEnable;
            dxBarLargeButtonDelete.Enabled = bEditEnable;
            dxBarLargeButtonSearch.Enabled = bNewEnable || bEditEnable;

            dxBarButtonCopyForecastDate.Enabled = bSaveEnable;
            dxBarButtonPasteForecastDate.Enabled = bSaveEnable;
        }

        private void MakeFunctionBtnEnableDisable(bool bEnable)
        {
            dxBarButtonCreateNewVersion.Enabled = bEnable;
            dxBarButtonPNextStack.Enabled = bEnable;
            dxBarButtonProcesstoNextSS.Enabled = bEnable;
            dxBarButtonProcesstoPrevSS.Enabled = bEnable;
            dxBarButtonCopyToStackorVersion.Enabled = bEnable;
            dxBarButtonMarkAsCurrent.Enabled = bEnable;
            dxBarButtonLabelStack.Enabled = bEnable;
            dxBarBtnProcToPrev.Enabled = bEnable;
            dxBarBtnPush.Enabled = bEnable;
            dxBarBtnUseInRep.Enabled = bEnable;
            dxBarBtnGoTo.Enabled = bEnable;
            dxBarBtnFinaliseRelease.Enabled = bEnable;
            dxBarBtnUnfinalise.Enabled = bEnable;
            dxBarBtnReStack.Enabled = bEnable;
            dxBarBtnChangeB1ToATL.Enabled = bEnable;
            dxBarBtnImptoStack.Enabled = bEnable;
        }

        void AddBinding()
        {
            ClearBinding();
            cxDBLookupComboBoxComp.DataBindings.Add("EditValue", dtStack, "COMPANY_ID");
            cxDBLookupComboBoxSite.DataBindings.Add("EditValue", dtStack, "SITE_CODE");
            cxDBImageComboBox1.DataBindings.Add("EditValue", dtStack, "STACK_STATUS");
            //cxDBCheckBox1.DataBindings.Add("EditValue", dtStack, "USE_IN_REPORT");
            checkEdit2.DataBindings.Add("EditValue", dtStack, "IS_CURRENT");
            cxDBTextEditVersion.DataBindings.Add("EditValue", dtStack, "VERSION");
            cxDBImageComboBox2.DataBindings.Add("EditValue", dtStack, "FROZEN");
            cxDBImageComboBox3.DataBindings.Add("EditValue", dtStack, "LOCKED");
            cxDBMemo1.DataBindings.Add("EditValue", dtStack, "COMMENTS");
            cxDBCurrencyEditTotPlots.DataBindings.Add("EditValue", dtStack, "TOTAL_PLOTS");
            cxDBCurrencyEditTotPhases.DataBindings.Add("EditValue", dtStack, "TOTAL_PHASES");
            cxDBCurrencyEditTotBlocks.DataBindings.Add("EditValue", dtStack, "TOTAL_BLOCKS");
            cxDBCurrencyEditTotCores.DataBindings.Add("EditValue", dtStack, "TOTAL_CORES");
            cxDBTextEdit1.DataBindings.Add("EditValue", dtStack, "CREATED_BY");
            cxDBTextEdit2.DataBindings.Add("EditValue", dtStack, "CREATED_ON");
            cxDBTextEdit3.DataBindings.Add("EditValue", dtStack, "LAST_UPDATED");
            dnStacks.DataSource = dtStack;
        }

        void ClearBinding()
        {
            cxDBLookupComboBoxComp.DataBindings.Clear();
            cxDBLookupComboBoxSite.DataBindings.Clear();
            cxDBImageComboBox1.DataBindings.Clear();        //Stack Status
            //cxDBCheckBox1.DataBindings.Clear();           //Use In Report - ChkBox
            checkEdit2.DataBindings.Clear();                //Active
            cxDBTextEditVersion.DataBindings.Clear();
            cxDBImageComboBox2.DataBindings.Clear();        //Frozen
            cxDBImageComboBox3.DataBindings.Clear();        //Locked
            cxDBMemo1.DataBindings.Clear();                 //Comments
            cxDBCurrencyEditTotPlots.DataBindings.Clear();
            cxDBCurrencyEditTotPhases.DataBindings.Clear();
            cxDBCurrencyEditTotBlocks.DataBindings.Clear();
            cxDBCurrencyEditTotCores.DataBindings.Clear();
            cxDBTextEdit1.DataBindings.Clear();             //Updated By
            cxDBTextEdit2.DataBindings.Clear();             //Created On
            cxDBTextEdit3.DataBindings.Clear();             //Created By
            dnStacks.DataBindings.Clear();
        }

        private void cxBtnPrev_Click(object sender, EventArgs e)
        {
            cxTBSSelCriteria.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            cxTabSheetStacks.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            cxPgCntrlSel.SelectedTabPage = cxTBSSelCriteria;
        }

        private void cxButtonRefresh_Click(object sender, EventArgs e)
        {
            if (cxLookupComboBoxCompany.EditValue == null || string.IsNullOrWhiteSpace(cxLookupComboBoxCompany.EditValue.ToString()))
            {
                XtraMessageBox.Show("Company must be selected.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            RefStackView(-1, true);
        }

        private void cxGridStackViewTv_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            cxGridStackViewtvFocusedRecordChanged(e.FocusedRowHandle);
        }

        private void cxGridStackViewtvFocusedRecordChanged(int iRowHandle)
        {
            try
            {
                if (cxGridStackViewTv.FocusedRowHandle >= 0)
                {
                    DataRow drStackView = cxGridStackViewTv.GetDataRow(iRowHandle);
                    decimal stack_id = Convert.ToDecimal(drStackView["STACK_ID"]);
                    Site_Doc_Guid = Convert.ToDecimal(drStackView["SITE_DOC_GUID"]);

                    LoadSingleStack(stack_id);

                    AddBinding();
                    DataRow drStkTemp = dtStackView.Rows.Find(stack_id);
                    if (drStkTemp != null)
                        cxTextEdit1.EditValue = Convert.ToString(drStkTemp["LAND_STATUS"]);

                    SetColours();
                    MakeButtonEnableDisable(true, dtStack.Rows.Count > 0, false, false);
                    MakeFunctionBtnEnableDisable(dtStack.Rows.Count > 0);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadSingleStack(decimal dStackId)
        {
            try
            {
                cxGridPlotSetupTv.BeginUpdate();
                cxGridPhaseTv.BeginUpdate();
                cxGridBlockDBTableView1.BeginUpdate();
                cxGridCoreDBTableView1.BeginUpdate();
                cxGridStackDocsTv.BeginUpdate();
                cxGrid2DBTableView2.BeginUpdate();
                CxGridHistorytv.BeginUpdate();

                drStack = null;
                dtStack = oStkDM.RetrieveStacksById(dStackId);
                if (dtStack != null && dtStack.Rows.Count > 0)
                {
                    drStack = dtStack.Rows[0];

                    dtStackPlotDet = oStkDM.RetrieveStackPlotDet(Convert.ToDecimal(drStack["STACK_ID"]), PCFDataModel.CONST_DIV_MUL_BY);
                    dtStackPhaseDet = oStkDM.RetrieveStackPhaseDet(Convert.ToDecimal(drStack["STACK_ID"]));
                    dtStackBlockDet = oStkDM.RetrieveStackBlockDet(Convert.ToDecimal(drStack["STACK_ID"]));
                    dtStackCoreDet = oStkDM.RetrieveStackCoreDet(Convert.ToDecimal(drStack["STACK_ID"]));
                    dtStkPFInfo = oStkDM.RetrieveStackPFInfo(Convert.ToDecimal(drStack["STACK_ID"]));
                    dtStackDocs = oStkDM.RetrieveStackDocuments(Convert.ToDecimal(drStack["STACK_ID"]));
                    dtStackSumm = oStkDM.RetrieveStackSummary(Convert.ToDecimal(drStack["STACK_ID"]), PCFDataModel.CONST_DIV_MUL_BY);
                    dtStackHistory = oStkDM.RetrieveStackHistory(Convert.ToDecimal(drStack["COMPANY_ID"]), Convert.ToString(drStack["SITE_CODE"]));
                }
                else
                {
                    dtStackPlotDet = oStkDM.RetrieveStackPlotDet(-1, PCFDataModel.CONST_DIV_MUL_BY);
                    dtStackPhaseDet = oStkDM.RetrieveStackPhaseDet(-1);
                    dtStackBlockDet = oStkDM.RetrieveStackBlockDet(-1);
                    dtStackCoreDet = oStkDM.RetrieveStackCoreDet(-1);
                    dtStkPFInfo = oStkDM.RetrieveStackPFInfo(-1);
                    dtStackDocs = oStkDM.RetrieveStackDocuments(-1);
                    dtStackSumm = oStkDM.RetrieveStackSummary(-1, PCFDataModel.CONST_DIV_MUL_BY);
                    dtStackHistory = oStkDM.RetrieveStackHistory(-1, "");
                }

                dtStackPlotDet.Constraints.Add("PK_PLOT", dtStackPlotDet.Columns["PLOT_CODE"], true);
                dtStackPhaseDet.Constraints.Add("PK_PHASE", dtStackPhaseDet.Columns["PHASE_ID"], true);
                dtStackBlockDet.Constraints.Add("PK_BLOCK", dtStackBlockDet.Columns["BLOCK_ID"], true);
                dtStackCoreDet.Constraints.Add("PK_CORE", dtStackCoreDet.Columns["CORE_ID"], true);
                dtStackDocs.Constraints.Add("PK_DOCS", dtStackDocs.Columns["DOCUMENT_ID"], true);

                cxGridPlotSetup.DataSource = dtStackPlotDet;
                cxGridPhase.DataSource = dtStackPhaseDet;
                cxGridBlock.DataSource = dtStackBlockDet;
                cxGridCore.DataSource = dtStackCoreDet;
                cxGridStackDocs.DataSource = dtStackDocs;
                cxGrid2.DataSource = dtStackSumm;
                cxGridHistory.DataSource = dtStackHistory;

                cxLookupComboBoxPhaseForPlot.Properties.DataSource = dtStackPhaseDet;
                cxLookupComboBoxBlockForPlot.Properties.DataSource = dtStackBlockDet;
                cxLookupComboBoxCoreForPlot.Properties.DataSource = dtStackCoreDet;

                repLookUpPhase.DataSource = dtStackPhaseDet;
                repLookUpBlock.DataSource = dtStackBlockDet;
                repLookUpCore.DataSource = dtStackCoreDet;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cxGridPlotSetupTv.EndUpdate();
                cxGridPhaseTv.EndUpdate();
                cxGridBlockDBTableView1.EndUpdate();
                cxGridCoreDBTableView1.EndUpdate();
                cxGridStackDocsTv.EndUpdate();
                cxGrid2DBTableView2.EndUpdate();
                CxGridHistorytv.EndUpdate();
            }
        }

        private void SetColours(string sFor = "A")
        {
            if (dtStackPlotDet != null && drStack != null && drStack["TOTAL_PLOTS"] != DBNull.Value && (sFor == "A" || sFor == "U"))
                if (dtStackPlotDet.Rows.Count == Convert.ToInt16(drStack["TOTAL_PLOTS"]))
                    cxDBCurrencyEditTotPlots.BackColor = Color.FromArgb(0, 183, 0);             //$0000B700
                else if (dtStackPlotDet.Rows.Count < Convert.ToInt16(drStack["TOTAL_PLOTS"]))
                    cxDBCurrencyEditTotPlots.BackColor = Color.FromArgb(255, 157, 60);          //$003C9DFF
                else if (dtStackPlotDet.Rows.Count > Convert.ToInt16(drStack["TOTAL_PLOTS"]))
                    cxDBCurrencyEditTotPlots.BackColor = Color.FromArgb(255, 66, 66);           //$004242FF

            if (dtStackPhaseDet != null && drStack != null && drStack["TOTAL_PHASES"] != DBNull.Value && (sFor == "A" || sFor == "P"))
                if (dtStackPhaseDet.Rows.Count == Convert.ToInt16(drStack["TOTAL_PHASES"]))
                    cxDBCurrencyEditTotPhases.BackColor = Color.FromArgb(0, 183, 0);
                else if (dtStackPhaseDet.Rows.Count < Convert.ToInt16(drStack["TOTAL_PHASES"]))
                    cxDBCurrencyEditTotPhases.BackColor = Color.FromArgb(255, 157, 60);
                else if (dtStackPhaseDet.Rows.Count > Convert.ToInt16(drStack["TOTAL_PHASES"]))
                    cxDBCurrencyEditTotPhases.BackColor = Color.FromArgb(255, 66, 66);

            if (dtStackBlockDet != null && drStack != null && drStack["TOTAL_BLOCKS"] != DBNull.Value && (sFor == "A" || sFor == "B"))
                if (dtStackBlockDet.Rows.Count == Convert.ToInt16(drStack["TOTAL_BLOCKS"]))
                    cxDBCurrencyEditTotBlocks.BackColor = Color.FromArgb(0, 183, 0);
                else if (dtStackBlockDet.Rows.Count < Convert.ToInt16(drStack["TOTAL_BLOCKS"]))
                    cxDBCurrencyEditTotBlocks.BackColor = Color.FromArgb(255, 157, 60);
                else if (dtStackBlockDet.Rows.Count > Convert.ToInt16(drStack["TOTAL_BLOCKS"]))
                    cxDBCurrencyEditTotBlocks.BackColor = Color.FromArgb(255, 66, 66);

            if (dtStackCoreDet != null && drStack != null && drStack["TOTAL_CORES"] != DBNull.Value && (sFor == "A" || sFor == "C"))
                if (dtStackCoreDet.Rows.Count == Convert.ToInt16(drStack["TOTAL_CORES"]))
                    cxDBCurrencyEditTotCores.BackColor = Color.FromArgb(0, 183, 0);
                else if (dtStackCoreDet.Rows.Count < Convert.ToInt16(drStack["TOTAL_CORES"]))
                    cxDBCurrencyEditTotCores.BackColor = Color.FromArgb(255, 157, 60);
                else if (dtStackCoreDet.Rows.Count > Convert.ToInt16(drStack["TOTAL_CORES"]))
                    cxDBCurrencyEditTotCores.BackColor = Color.FromArgb(255, 66, 66);
        }

        private void cxLookupComboBoxRegion_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                cxLookupComboBoxRegion.EditValue = null;
                cxLookupComboBoxCompany.EditValue = null;
            }
        }

        private void cxLookupComboBoxCompany_EditValueChanged(object sender, EventArgs e)
        {
            if (cxLookupComboBoxCompany.EditValue == null || string.IsNullOrWhiteSpace(cxLookupComboBoxCompany.EditValue.ToString()))
                cxLookupComboBoxSiteCode.EditValue = null;
        }

        private void cxLookupComboBoxDev_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                cxLookupComboBoxDev.EditValue = null;
                cxLookupComboBoxSiteCode.EditValue = null;
            }
        }

        private void cxLookupComboBoxCompany_Enter(object sender, EventArgs e)
        {
            try
            {
                string sIsActive;
                if (cxImageComboBoxShowCompanies.EditValue.ToString() == "AC")
                    sIsActive = "Y";
                else if (cxImageComboBoxShowCompanies.EditValue.ToString() == "IN")
                    sIsActive = "N";
                else
                    sIsActive = "A";

                decimal dRegId = -1;
                if (cxLookupComboBoxRegion.EditValue != null && !string.IsNullOrWhiteSpace(cxLookupComboBoxRegion.EditValue.ToString()))
                    dRegId = Convert.ToDecimal(cxLookupComboBoxRegion.EditValue);

                dtCompanyLookup = oComm.RetrieveCompanyListByRegion(ProfitCashflow.oPcfDM.UserName, sIsActive, dRegId);
                cxLookupComboBoxCompany.Properties.DataSource = dtCompanyLookup;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cxLookupComboBoxSiteCode_QueryPopUp(object sender, CancelEventArgs e)
        {
            if (cxLookupComboBoxCompany.EditValue == null || string.IsNullOrWhiteSpace(cxLookupComboBoxCompany.EditValue.ToString()))
            {
                XtraMessageBox.Show("Select a Company.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
            }
        }

        private void cxLookupComboBoxSiteCode_Enter(object sender, EventArgs e)
        {
            try
            {
                if (cxLookupComboBoxCompany.EditValue != null && !string.IsNullOrWhiteSpace(cxLookupComboBoxCompany.EditValue.ToString()))
                {
                    decimal dDevId = -1;
                    if (cxLookupComboBoxDev.EditValue != null && !string.IsNullOrWhiteSpace(cxLookupComboBoxDev.EditValue.ToString()))
                        dDevId = Convert.ToDecimal(cxLookupComboBoxDev.EditValue);

                    dtSiteLookup = oComm.RetrieveSiteListByCompDev(Convert.ToDecimal(cxLookupComboBoxCompany.EditValue), dDevId);
                    cxLookupComboBoxSiteCode.Properties.DataSource = dtSiteLookup;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RefStackView(decimal dNewStackId, bool bIsBtnClick = false)
        {
            try
            {
                cxGridStackViewTv.FocusedRowChanged -= cxGridStackViewTv_FocusedRowChanged;
                cxGridStackViewTv.BeginUpdate();

                string sSiteCode = "";
                if (cxLookupComboBoxSiteCode.EditValue != null && !string.IsNullOrWhiteSpace(cxLookupComboBoxSiteCode.EditValue.ToString()))
                    sSiteCode = Convert.ToString(cxLookupComboBoxSiteCode.EditValue);

                dtStackView = oStkDM.RetrieveAllStacks(Convert.ToDecimal(cxLookupComboBoxCompany.EditValue), sSiteCode);
                dtStackView.Constraints.Add("PK_STACK", dtStackView.Columns["STACK_ID"], true);
                cxGridStackView.DataSource = dtStackView;

                cxGridStackViewTv.EndUpdate();

                if (!string.IsNullOrWhiteSpace(sSiteCode) || dNewStackId > 0)
                {
                    decimal dTempId = -1;
                    if (bIsBtnClick)
                    {
                        dtActiveStack = oStkDM.RetrieveActiveStacks(Convert.ToDecimal(cxLookupComboBoxCompany.EditValue), sSiteCode);
                        if (dtActiveStack != null && dtActiveStack.Rows.Count > 0)
                            dTempId = Convert.ToDecimal(dtActiveStack.Rows[0]["STACK_ID"]);
                    }
                    else
                        dTempId = dNewStackId;

                    DataRow drStkTemp = dtStackView.Rows.Find(dTempId);
                    if (drStkTemp != null)
                    {
                        cxGridStackViewTv.FocusedRowHandle = cxGridStackViewTv.GetRowHandle(dtStackView.Rows.IndexOf(drStkTemp));
                        if (bIsBtnClick)
                            cxTextEdit1.EditValue = Convert.ToString(drStkTemp["LAND_STATUS"]);
                    }
                }

                cxGridStackViewTv.FocusedRowChanged += cxGridStackViewTv_FocusedRowChanged;
                cxGridStackViewtvFocusedRecordChanged(cxGridStackViewTv.FocusedRowHandle);

                if (cxGridStackViewTv.RowCount > 0)
                {
                    cxTBSSelCriteria.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    cxTabSheetStacks.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    cxPgCntrlSel.SelectedTabPage = cxTabSheetStacks;
                }
                else
                    XtraMessageBox.Show("No rows selected.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Stacks_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (lFormMode != 'B')
            {
                DialogResult oTempResult = XtraMessageBox.Show("Data has been changed, do you want to save the changes?", Application.ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (oTempResult == DialogResult.Yes)
                {
                    if (!SaveStackSystem())
                        e.Cancel = true;
                }
                else if (oTempResult == DialogResult.No)
                    dxBarLargeButtonCancel_Click(sender, new EventArgs());
                else
                    e.Cancel = true;
            }
        }

        private void dxBarLargeButtonSearch_Click(object sender, EventArgs e)
        {
            splitContainerControl1.Collapsed = false;
        }

        private void dxBarLargeButtonNew_Click(object sender, EventArgs e)
        {
            try
            {
                b_INSUPDEL = true;
                Clear_Plot_fld();
                Flag = "N"; lFormMode = 'N';

                if (dtStack != null) dtStack.Clear();
                ClearValue();

                LoadSingleStack(-1);
                dtStack.TableNewRow += new DataTableNewRowEventHandler(dtStack_TableNewRow);
                dtStack.Constraints.Add("PK_STACK", dtStack.Columns["STACK_ID"], true);

                AddBinding();
                try
                {
                    dnStacks.Buttons.DoClick(dnStacks.Buttons.Append);
                }
                catch (Exception exp)
                {
                    throw exp;
                }

                MakeFunctionBtnEnableDisable(false);
                MakeButtonEnableDisable(false, false, true, true);
                MakeControlsEnable(true);

                pInsStack = true;
                cxPageControl1.SelectedTabPage = cxTabSheetStackDet;
                cxDBLookupComboBoxComp.Focus();

                cxCurrencyEditMemoPlots.EditValue = 1;
                cxDBCurrencyEditTotPlots.BackColor = Color.FromArgb(215, 255, 255);
                cxDBCurrencyEditTotPhases.BackColor = Color.FromArgb(215, 255, 255);
                cxDBCurrencyEditTotBlocks.BackColor = Color.FromArgb(215, 255, 255);
                cxDBCurrencyEditTotCores.BackColor = Color.FromArgb(215, 255, 255);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void dtStack_TableNewRow(object sender, DataTableNewRowEventArgs e)
        {
            drStack = e.Row;

            if (ProfitCashflow.oPcfDM.company_id != -1)
            {
                DataRow drTempComp = dtCompany.Rows.Find(ProfitCashflow.oPcfDM.company_id);
                if (drTempComp != null && Convert.ToString(drTempComp["IS_ACTIVE"]) == "Y")
                    drStack["COMPANY_ID"] = ProfitCashflow.oPcfDM.company_id;
            }

            decimal seq_id = ProfitCashflow.oPcfDM.RetrieveSequenceId("SEQ_PK_STACKS");
            drStack["STACK_ID"] = seq_id;

            seq_id = ProfitCashflow.oPcfDM.RetrieveSequenceId("SEQ_PK_PCF_DOC_GUID");
            drStack["PCF_DOC_GUID"] = seq_id;

            drStack["IS_CURRENT"] = "N";
            drStack["FROZEN"] = "N";
            drStack["LOCKED"] = "N";
            drStack["PROCESSED_TO_PREV"] = "N";
            drStack["TOTAL_PLOTS"] = 0;
            drStack["TOTAL_PHASES"] = 0;
            drStack["TOTAL_BLOCKS"] = 0;
            drStack["TOTAL_CORES"] = 0;
            drStack["USE_IN_REPORT"] = "N";
            drStack["CREATED_BY"] = ProfitCashflow.oPcfDM.UserName;
            drStack["CREATED_ON"] = DateTime.Now;
            drStack["LAST_UPDATED"] = DateTime.Now;
            drStack["NEXT_STACKSTATUS"] = "N";
            drStack["MODE"] = 0;
            drStack["SYS_CREATED_STACK"] = "N";
            drStack["PUSHED_TO_PF"] = "N";
            drStack["STACK_STATUS"] = "O";
            drStack["VERSION"] = 1;
            drStack["IS_CURRENT"] = "Y";
            drStack["MODE"] = 0;
        }

        private void dxBarLargeButtonEdit_Click(object sender, EventArgs e)
        {
            try
            {
                decimal pcf_doc_guid = 0, stack_id = 0;
                string vstack_code, vstack_status, lFrozen = "", lLocked = "", lProcToPrev = "";
                Clear_Plot_fld();

                if (oStkDM.Chk_PartExchnage(Convert.ToDecimal(drStack["COMPANY_ID"]), Convert.ToString(drStack["SITE_CODE"])))
                {
                    UpdatePlot_Ind(false);
                    cxGridPlotSetupTvPLOT_INDICATOR.OptionsColumn.AllowEdit = false;
                }
                else
                {
                    UpdatePlot_Ind(true);
                    cxGridPlotSetupTvPLOT_INDICATOR.OptionsColumn.AllowEdit = true;
                }

                dtActiveStack = oStkDM.RetrieveActiveStacks(Convert.ToDecimal(drStack["COMPANY_ID"]), Convert.ToString(drStack["SITE_CODE"]));
                if (dtActiveStack != null && dtActiveStack.Rows.Count > 0)
                    if (Convert.ToString(dtActiveStack.Rows[0]["STACK_STATUS"]) != Convert.ToString(drStack["STACK_STATUS"]))
                    {
                        XtraMessageBox.Show("Only stacks of an Active Stack Status can be edited.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                if (drStack["PCF_DOC_GUID"] != DBNull.Value && !string.IsNullOrWhiteSpace(drStack["PCF_DOC_GUID"].ToString()))
                {
                    pcf_doc_guid = Convert.ToDecimal(drStack["PCF_DOC_GUID"]);
                    stack_id = Convert.ToDecimal(drStack["STACK_ID"]);
                    lFrozen = Convert.ToString(drStack["FROZEN"]);
                    lLocked = Convert.ToString(drStack["LOCKED"]);
                    lProcToPrev = Convert.ToString(drStack["PROCESSED_TO_PREV"]);
                    vstack_code = Convert.ToString(drStkSiteDet["STACK_CODE"]);
                    vstack_status = Convert.ToString(drStack["STACK_STATUS"]);
                }
                else if (cxGridStackViewTv.FocusedRowHandle >= 0)
                {
                    DataRow drSelStk = cxGridStackViewTv.GetDataRow(cxGridStackViewTv.FocusedRowHandle);
                    pcf_doc_guid = Convert.ToDecimal(drSelStk["PCF_DOC_GUID"]);
                    stack_id = Convert.ToDecimal(drSelStk["STACK_ID"]);
                    lFrozen = Convert.ToString(drSelStk["FROZEN"]);
                    lLocked = Convert.ToString(drSelStk["LOCKED"]);
                    vstack_status = Convert.ToString(drSelStk["STACK_STATUS"]);
                }

                if (!Can_Change_Del_Doc(pcf_doc_guid, stack_id))
                    return;

                if (lProcToPrev == "Y")
                {
                    XtraMessageBox.Show("Selected stack is processed to previous Land / Stack Status, hence cannot edit.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (lFrozen == "Y")
                {
                    XtraMessageBox.Show("Cannot edit, as the selected Stack was frozen, while Progressing/Regressing it to the Next/Previous Stack Status.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (lLocked == "Y")
                {
                    drStkUserRights = oStkDM.RetrieveStackUserRights(ProfitCashflow.oPcfDM.UserName);
                    if (drStkUserRights != null && Convert.ToString(drStkUserRights["PCF_EDIT_LOCKED_STACK"]) == "N")
                    {
                        XtraMessageBox.Show("User does not have rights to edit Locked Stacks.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                oComm.InsertPCFLockedDocument(pcf_doc_guid, ProfitCashflow.oPcfDM.UserName, ProfitCashflow.oPcfDM.session_id,
                    "STACKS", Convert.ToDecimal(drStack["COMPANY_ID"]), Convert.ToString(drStack["SITE_CODE"]));

                b_INSUPDEL = true; pInsStack = false;
                Flag = "E"; lFormMode = 'E';

                LoadSingleStack(stack_id);
                AddBinding();
                cxCurrencyEditMemoPlots.EditValue = 1;

                MakeControlsEnable(true);
                MakeButtonEnableDisable(false, false, true, true);
                MakeFunctionBtnEnableDisable(false);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dxBarLargeButtonCancel_Click(object sender, EventArgs e)
        {
            Clear_Plot_fld();
            char cPrevMode = lFormMode;
            Flag = "C"; lFormMode = 'B';

            cxGridPlotSetupTv.CancelUpdateCurrentRow();
            cxGridPhaseTv.CancelUpdateCurrentRow();
            cxGridBlockDBTableView1.CancelUpdateCurrentRow();
            cxGridCoreDBTableView1.CancelUpdateCurrentRow();
            cxGridStackDocsTv.CancelUpdateCurrentRow();
            cxGrid2DBTableView2.CancelUpdateCurrentRow();

            dnStacks.Buttons.DoClick(dnStacks.Buttons.CancelEdit);

            cxGridPlotSetupTv.BeginUpdate();
            cxGridPhaseTv.BeginUpdate();
            cxGridBlockDBTableView1.BeginUpdate();
            cxGridCoreDBTableView1.BeginUpdate();
            cxGridStackDocsTv.BeginUpdate();
            cxGrid2DBTableView2.BeginUpdate();

            try
            {
                DeleteLocks();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                dtStackPlotDet.RejectChanges();
                dtStackPhaseDet.RejectChanges();
                dtStackBlockDet.RejectChanges();
                dtStackCoreDet.RejectChanges();
                dtStackDocs.RejectChanges();
                dtStackSumm.RejectChanges();

                cxGridPlotSetupTv.EndUpdate();
                cxGridPhaseTv.EndUpdate();
                cxGridBlockDBTableView1.EndUpdate();
                cxGridCoreDBTableView1.EndUpdate();
                cxGridStackDocsTv.EndUpdate();
                cxGrid2DBTableView2.EndUpdate();
            }

            if (cPrevMode == 'N' && cxGridStackViewTv.RowCount > 0)
                cxGridStackViewtvFocusedRecordChanged(cxGridStackViewTv.FocusedRowHandle);
            else
            {
                if (dtStack == null || dtStack.Rows.Count == 0) drStack = null;
                SetColours();
            }

            if (drStack == null)
            {
                //InOldToAll -> BackColor = Color.FromArgb(215, 255, 255);     //$00FFFFD7
                cxDBCurrencyEditTotPlots.BackColor = Color.White;
                cxDBCurrencyEditTotPlots.Properties.Appearance.Options.UseBackColor = false;
                cxDBCurrencyEditTotPhases.BackColor = Color.White;
                cxDBCurrencyEditTotPhases.Properties.Appearance.Options.UseBackColor = false;
                cxDBCurrencyEditTotBlocks.BackColor = Color.White;
                cxDBCurrencyEditTotBlocks.Properties.Appearance.Options.UseBackColor = false;
                cxDBCurrencyEditTotCores.BackColor = Color.White;
                cxDBCurrencyEditTotCores.Properties.Appearance.Options.UseBackColor = false;
            }

            pInsStack = false;
            b_INSUPDEL = false;

            MakeControlsEnable(false);
            MakeButtonEnableDisable(true, dtStack.Rows.Count > 0, false, false);
            MakeFunctionBtnEnableDisable(dtStack.Rows.Count > 0);
        }

        private void dxBarLargeButtonDelete_Click(object sender, EventArgs e)
        {
            object oQryResult;
            decimal stack_id = 0, pcf_doc_guid = 0, vcompid = 0;
            string lIsCurrent = "", lFrozen = "", lLocked = "", lProcToPrev, vstack_status = "", vsitecode = "";

            Clear_Plot_fld();

            if (drStack["PCF_DOC_GUID"] != DBNull.Value && !string.IsNullOrWhiteSpace(drStack["PCF_DOC_GUID"].ToString()))
            {
                pcf_doc_guid = Convert.ToDecimal(drStack["PCF_DOC_GUID"]);
                stack_id = Convert.ToDecimal(drStack["STACK_ID"]);
                lFrozen = Convert.ToString(drStack["FROZEN"]);
                lLocked = Convert.ToString(drStack["LOCKED"]);
                lProcToPrev = Convert.ToString(drStack["PROCESSED_TO_PREV"]);
                lIsCurrent = Convert.ToString(drStack["IS_CURRENT"]);
                vcompid = Convert.ToDecimal(drStack["COMPANY_ID"]);
                vsitecode = Convert.ToString(drStack["SITE_CODE"]);
                vstack_status = Convert.ToString(drStack["STACK_STATUS"]);
            }
            else if (cxGridStackViewTv.FocusedRowHandle >= 0)
            {
                DataRow drSelStk = cxGridStackViewTv.GetDataRow(cxGridStackViewTv.FocusedRowHandle);
                pcf_doc_guid = Convert.ToDecimal(drSelStk["PCF_DOC_GUID"]);
                stack_id = Convert.ToDecimal(drSelStk["STACK_ID"]);
                lFrozen = Convert.ToString(drSelStk["FROZEN"]);
                lLocked = Convert.ToString(drSelStk["LOCKED"]);
                lIsCurrent = Convert.ToString(drSelStk["IS_CURRENT"]);
                vcompid = Convert.ToDecimal(drSelStk["COMPANY_ID"]);
                vsitecode = Convert.ToString(drSelStk["SITE_CODE"]);
                vstack_status = Convert.ToString(drSelStk["STACK_STATUS"]);
            }

            try
            {
                if (!Can_Change_Del_Doc(pcf_doc_guid, stack_id)) return;

                if (vstack_status == "R")
                {
                    sQuery = "SELECT COUNT(1) AS CNT FROM STACKS S ";
                    sQuery += "INNER JOIN STACK_PLOT_DETAILS PD ON PD.STACK_ID = S.STACK_ID ";
                    sQuery += "WHERE S.COMPANY_ID = @IN_COMPANY_ID AND S.SITE_CODE = @IV_SITE_CODE ";
                    sQuery += "AND PD.PLOT_FINALISED = @IC_PLOT_FINALISED";

                    oParameter = new List<KeyValuePair<string, object>>();
                    oParameter.Add(new KeyValuePair<string, object>("@IN_COMPANY_ID", Convert.ToDecimal(drStack["COMPANY_ID"])));
                    oParameter.Add(new KeyValuePair<string, object>("@IV_SITE_CODE", Convert.ToString(drStack["SITE_CODE"])));
                    oParameter.Add(new KeyValuePair<string, object>("@IC_PLOT_FINALISED", "Y"));
                    oQryResult = ProfitCashflow.oPcfDM.SqlQryAllpurpose(sQuery, oParameter, true);

                    if (oQryResult != null && Convert.ToInt16(oQryResult) > 0)
                    {
                        XtraMessageBox.Show("Cannot delete the Release Stack, as plots have been finalised.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                dtActiveStack = oStkDM.RetrieveActiveStacks(Convert.ToDecimal(drStack["COMPANY_ID"]), Convert.ToString(drStack["SITE_CODE"]));
                if (dtActiveStack != null && dtActiveStack.Rows.Count > 0)
                    if (Convert.ToString(dtActiveStack.Rows[0]["STACK_STATUS"]) != Convert.ToString(drStack["STACK_STATUS"]))
                    {
                        XtraMessageBox.Show("Only stacks of an Active Stack Status can be deleted.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                // An Active stack can be deleted only if there is 1 stack
                if (lIsCurrent == "Y")
                {
                    sQuery = "SELECT COUNT(1) AS CNT FROM STACKS S ";
                    sQuery += "WHERE S.COMPANY_ID = @IN_COMPANY_ID AND S.SITE_CODE = @IV_SITE_CODE ";
                    sQuery += "AND SYS_CREATED_STACK = @IC_SYS_CREATED_STACK";

                    oParameter = new List<KeyValuePair<string, object>>();
                    oParameter.Add(new KeyValuePair<string, object>("@IN_COMPANY_ID", Convert.ToDecimal(drStack["COMPANY_ID"])));
                    oParameter.Add(new KeyValuePair<string, object>("@IV_SITE_CODE", Convert.ToString(drStack["SITE_CODE"])));
                    oParameter.Add(new KeyValuePair<string, object>("@IC_SYS_CREATED_STACK", "N"));
                    oQryResult = ProfitCashflow.oPcfDM.SqlQryAllpurpose(sQuery, oParameter, true);

                    if (oQryResult != null && Convert.ToInt16(oQryResult) > 1)
                    {
                        XtraMessageBox.Show("An Active Stack Version cannot be deleted. Mark another stack as active and then delete.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                if (lFrozen == "Y")
                {
                    XtraMessageBox.Show("A Frozen Stack cannot be deleted.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (lLocked == "Y")
                {
                    XtraMessageBox.Show("A Locked Stack cannot be deleted.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                sQuery = "SELECT COUNT(1) AS CNT FROM STACKS S ";
                sQuery += "WHERE S.PREVIOUS_STACK_ID = @IN_STACK_ID";
                oParameter = new List<KeyValuePair<string, object>>();
                oParameter.Add(new KeyValuePair<string, object>("@IN_STACK_ID", Convert.ToDecimal(drStack["STACK_ID"])));
                oQryResult = ProfitCashflow.oPcfDM.SqlQryAllpurpose(sQuery, oParameter, true);

                if (oQryResult != null && Convert.ToInt16(oQryResult) > 0)
                {
                    XtraMessageBox.Show("Cannot delete the selected stack, as it is being referenced by another stack.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                drStkUserRights = oStkDM.RetrieveStackUserRights(ProfitCashflow.oPcfDM.UserName);
                if (drStkUserRights != null && Convert.ToString(drStkUserRights["PCF_DELETE_STACK"]) == "N")
                {
                    XtraMessageBox.Show("User does not have rights to delete a Stack.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (XtraMessageBox.Show("Delete selected Stack?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            Flag = "D";
            try
            {
                drStack.Delete();   //ToBeSeen - cxGridStackView's focued row chng
                oStkDM.UpdateStack(dtStack);
                //LoadSingleStack(stack_id);    //ToBeSeen
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                //dtStackHistory = oStkDM.RetrieveStackHistory(vcompid, vsitecode); //ToBeSeen
            }

            if (cxGridStackView.Enabled && CxGridHistorytv.RowCount > 0)
                RefStackView(-1, true);

            MakeButtonEnableDisable(true, dtStack.Rows.Count > 0, false, false);
            MakeFunctionBtnEnableDisable(dtStack.Rows.Count > 0);
        }

        private void dxBarLargeButtonSave_Click(object sender, EventArgs e)
        {
            if (SaveStackSystem())
            {
                lFormMode = 'B';
                pInsAddPlots = false;
                pInsStack = false;
                b_INSUPDEL = false;

                MakeControlsEnable(false);
                MakeButtonEnableDisable(true, true, false, false);
                MakeFunctionBtnEnableDisable(true);

                try
                {
                    if (cxLookupComboBoxSiteCode.EditValue == null || string.IsNullOrWhiteSpace(cxLookupComboBoxSiteCode.EditValue.ToString()))
                        cxLookupComboBoxSiteCode.EditValue = Convert.ToString(drStack["SITE_CODE"]);

                    if (cxGridStackView.Enabled)    //ToBeSeen - call required
                        RefStackView(Convert.ToDecimal(drStack["STACK_ID"]));
                    else
                    {
                        dtStackHistory = oStkDM.RetrieveStackHistory(Convert.ToDecimal(drStack["COMPANY_ID"]), Convert.ToString(drStack["SITE_CODE"]));
                        dtStackSumm = oStkDM.RetrieveStackSummary(Convert.ToDecimal(drStack["STACK_ID"]), PCFDataModel.CONST_DIV_MUL_BY);
                        SetColours();
                    }

                    if (Convert.ToString(drStack["STACK_STATUS"]) == "R" && Convert.ToString(drStack["IS_CURRENT"]) == "Y")
                        if (Convert.ToString(drStkSiteDet["STACK_CODE"]) == "B1" || Convert.ToString(drStkSiteDet["STACK_CODE"]) == "ALT")
                        {
                            if (XtraMessageBox.Show("Do you want to push the selected release stack to PF and finalise it?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                dxBarBtnFinaliseRelease.PerformClick();
                            else
                                return;
                        }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private bool SaveStackSystem()
        {
            string vMaxPlotNo, vCompCode, vJV_CompCode;
            bool vCodaRelease, Data_saved = false, vSuccess = false;
            decimal vGUID;
            object oQryResult;

            try
            {
                Flag = "S";
                DoValidateControls();

                if (lFormMode == 'E')
                {
                    sQuery = "SELECT COUNT(1) AS CNT FROM PCF_LOCKED_DOCUMENTS WHERE PCF_DOC_GUID = @IN_PCF_DOC_GUID AND SESSIONID = @IV_SESSIONID";
                    oParameter = new List<KeyValuePair<string, object>>();
                    oParameter.Add(new KeyValuePair<string, object>("@IN_PCF_DOC_GUID", Convert.ToDecimal(drStack["PCF_DOC_GUID"])));
                    oParameter.Add(new KeyValuePair<string, object>("@IV_SESSIONID", ProfitCashflow.oPcfDM.session_id));
                    oQryResult = ProfitCashflow.oPcfDM.SqlQryAllpurpose(sQuery, oParameter, true);

                    if (oQryResult != null && Convert.ToInt16(oQryResult) == 0)
                    {
                        XtraMessageBox.Show("Lock on selected Document # has been released. Contact system administrator.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        dxBarLargeButtonCancel.PerformClick();
                        return false;
                    }
                }

                if (!cxGridPhaseTv.PostEditor()) return false;
                if (!cxGridPhaseTv.UpdateCurrentRow()) return false;
                if (!cxGridBlockDBTableView1.PostEditor()) return false;
                if (!cxGridBlockDBTableView1.UpdateCurrentRow()) return false;
                if (!cxGridCoreDBTableView1.PostEditor()) return false;
                if (!cxGridCoreDBTableView1.UpdateCurrentRow()) return false;
                if (!cxGridPlotSetupTv.PostEditor()) return false;
                if (!cxGridPlotSetupTv.UpdateCurrentRow()) return false;
                if (!cxGridStackDocsTv.PostEditor()) return false;
                if (!cxGridStackDocsTv.UpdateCurrentRow()) return false;

                /*if (DMStacksL.cdUpdtPFU.State in [dsedit,dsinsert])then
                    DMStacksL.cdUpdtPFU.Post;*/
                //ToBeSeen

                if (!ValidateStacksControls())
                    return false;

                dnStacks.Buttons.DoClick(dnStacks.Buttons.EndEdit);

                if (lFormMode == 'N' && drStack.RowState == DataRowState.Added)
                {
                    //===chk if any Outline stack with verison 1 exits=====
                    if (Convert.ToString(drStack["STACK_STATUS"]) == "O" && Convert.ToDecimal(drStack["VERSION"]) == 1)
                    {
                        sQuery = "SELECT COUNT(1) AS CNT FROM STACKS ";
                        sQuery += "WHERE COMPANY_ID = @IN_COMPANY_ID AND SITE_CODE = @IV_SITE_CODE ";
                        sQuery += "AND STACK_STATUS = 'O' AND VERSION = 1";
                        oParameter = new List<KeyValuePair<string, object>>();
                        oParameter.Add(new KeyValuePair<string, object>("@IN_COMPANY_ID", Convert.ToDecimal(drStack["COMPANY_ID"])));
                        oParameter.Add(new KeyValuePair<string, object>("@IV_SITE_CODE", Convert.ToString(drStack["SITE_CODE"])));
                        oQryResult = ProfitCashflow.oPcfDM.SqlQryAllpurpose(sQuery, oParameter, true);

                        if (oQryResult != null && Convert.ToInt16(oQryResult) > 0)
                        {
                            XtraMessageBox.Show("Cannot insert, as a stack of outline version 1 already exist for the selected site.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);

                            cxLookupComboBoxCompany.EditValue = Convert.ToDecimal(drStack["COMPANY_ID"]);
                            cxLookupComboBoxSiteCode.EditValue = Convert.ToString(drStack["SITE_CODE"]);

                            dxBarLargeButtonCancel.PerformClick();
                            RefStackView(-1, true);

                            return false;
                        }
                    }
                }

                try
                {
                    if (dtStack.GetChanges() != null || dtStackPhaseDet.GetChanges() != null ||
                    dtStackBlockDet.GetChanges() != null || dtStackCoreDet.GetChanges() != null ||
                    dtStackPlotDet.GetChanges() != null || dtStkPFInfo.GetChanges() != null || dtStackDocs.GetChanges() != null)
                    {
                        oStkDM.UpdateStackSystem(dtStack, dtStackPhaseDet, dtStackBlockDet, dtStackCoreDet, dtStackPlotDet, dtStackDocs);
                        /*if (DMStacksL.cdUpdtPFU.ChangeCount >0)then
                            DMStacksL.cdUpdtPFU.ApplyUpdates(-1);*/
                        //ToBeSeen

                        dtStack.AcceptChanges();
                        dtStackPhaseDet.AcceptChanges();
                        dtStackBlockDet.AcceptChanges();
                        dtStackCoreDet.AcceptChanges();
                        dtStackPlotDet.AcceptChanges();
                        dtStackDocs.AcceptChanges();

                        vSuccess = true;
                        Data_saved = true;
                    }
                }
                catch (Exception ex)
                {
                    vSuccess = false;
                    XtraMessageBox.Show("Data has not been saved to database successfully. " + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                if (vSuccess)
                {
                    drStkSiteDet = oStkDM.RetrieveStackSiteDetail(Convert.ToDecimal(cxDBLookupComboBoxComp.EditValue), cxDBLookupComboBoxSite.EditValue.ToString());

                    if (pInsAddPlots)    // Insert additional plots created in this stack, into SysCreatedStack.
                    {
                        sQuery = "SPN_XP_INS_STK_PLOT_SYS_STK";
                        oParameter = new List<KeyValuePair<string, object>>();
                        oParameter.Add(new KeyValuePair<string, object>("@IN_STACK_ID", Convert.ToDecimal(drStack["STACK_ID"])));
                        oParameter.Add(new KeyValuePair<string, object>("@IN_SYS_STACK_ID", Convert.ToDecimal(drStkSiteDet["SYS_CREATED_STACK_ID"])));
                        ProfitCashflow.oPcfDM.SqlQryAllpurpose(sQuery, oParameter, false, true);

                        sQuery = "SPN_XP_UPD_STK_MAX_PLOT_NO";
                        oParameter = new List<KeyValuePair<string, object>>();
                        oParameter.Add(new KeyValuePair<string, object>("@IN_STACK_ID", Convert.ToDecimal(drStack["STACK_ID"])));
                        oParameter.Add(new KeyValuePair<string, object>("@IN_COMPANY_ID", Convert.ToDecimal(cxDBLookupComboBoxComp.EditValue)));
                        oParameter.Add(new KeyValuePair<string, object>("@IV_SITE_CODE", Convert.ToString(cxDBLookupComboBoxSite.EditValue)));
                        oParameter.Add(new KeyValuePair<string, object>("@IN_SYS_STACK_ID", Convert.ToDecimal(drStkSiteDet["SYS_CREATED_STACK_ID"])));
                        ProfitCashflow.oPcfDM.SqlQryAllpurpose(sQuery, oParameter, false, true);

                        if (Convert.ToString(drStkSiteDet["MAX_PLOT_NO"]).Length == 2)
                            vMaxPlotNo = "P000" + Convert.ToString(drStkSiteDet["MAX_PLOT_NO"]).Substring(1, 1);
                        else if (Convert.ToString(drStkSiteDet["MAX_PLOT_NO"]).Length == 3)
                            vMaxPlotNo = "P00" + Convert.ToString(drStkSiteDet["MAX_PLOT_NO"]).Substring(1, 2);
                        else if (Convert.ToString(drStkSiteDet["MAX_PLOT_NO"]).Length == 4)
                            vMaxPlotNo = "P0" + Convert.ToString(drStkSiteDet["MAX_PLOT_NO"]).Substring(1, 3);
                        else
                            vMaxPlotNo = Convert.ToString(drStkSiteDet["MAX_PLOT_NO"]);

                        try
                        {
                            #region CODA    //ToBeSeen

                            //Coda - Create, Setup & Buildup

                            DataRow drTempComp = dtCompany.Rows.Find(drStkSiteDet["COMPANY_ID"]);
                            if (drTempComp != null)
                                vCompCode = Convert.ToString(drTempComp["COMPANY_CODE"]);

                            if (drStkSiteDet["JV_COMPANY_ID"] != DBNull.Value && Convert.ToDecimal(drStkSiteDet["JV_COMPANY_ID"]) > 0)
                            {
                                drTempComp = dtCompany.Rows.Find(Convert.ToDecimal(drStkSiteDet["JV_COMPANY_ID"]));
                                if (drTempComp != null)
                                    vJV_CompCode = Convert.ToString(drTempComp["COMPANY_CODE"]);
                            }

                            vSuccess = true;
                            if (drStkSiteDet["JV_COMPANY_ID"] != DBNull.Value && Convert.ToDecimal(drStkSiteDet["JV_COMPANY_ID"]) > 0)
                            {
                                try
                                {
                                    //ToBeSeen
                                }
                                catch (Exception ex)
                                {
                                    vSuccess = false;
                                    XtraMessageBox.Show("ERROR: Could not update changes to the Joint Venture site in CODA. " + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                try
                                {
                                    //ToBeSeen
                                }
                                catch (Exception ex)
                                {
                                    vSuccess = false;
                                    XtraMessageBox.Show("ERROR: Could not update changes to the operating company site in CODA.  " + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            #endregion
                        }
                        catch (Exception ex)
                        {
                            vSuccess = false;
                            XtraMessageBox.Show("Error posting changed Maximum Plot Number to CODA. " + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    if (drStkSiteDet != null)
                    {
                        oParameter = new List<KeyValuePair<string, object>>();
                        oParameter.Add(new KeyValuePair<string, object>("@IN_STACK_ID", Convert.ToDecimal(drStack["STACK_ID"])));
                        oParameter.Add(new KeyValuePair<string, object>("@IN_SYS_STACK_ID", Convert.ToDecimal(drStkSiteDet["SYS_CREATED_STACK_ID"])));

                        //iNSERT IN SYS PHASE DETAILS
                        ProfitCashflow.oPcfDM.SqlQryAllpurpose("SPN_XP_INS_STK_PHASE_SYS_STK", oParameter, false, true);
                        //iNSERT IN SYS BLOCK DETAILS
                        ProfitCashflow.oPcfDM.SqlQryAllpurpose("SPN_XP_INS_STK_BLOCK_SYS_STK", oParameter, false, true);
                        //iNSERT IN SYS CORE DETAILS
                        ProfitCashflow.oPcfDM.SqlQryAllpurpose("SPN_XP_INS_STK_CORE_SYS_STK", oParameter, false, true);

                        //Update Sys Phase with the current Phase
                        ProfitCashflow.oPcfDM.SqlQryAllpurpose("SPN_XP_UPD_STK_PHASE_SYS_STK", oParameter, false, true);
                        //Update Sys BLock with the current Block
                        ProfitCashflow.oPcfDM.SqlQryAllpurpose("SPN_XP_UPD_STK_BLOCK_SYS_STK", oParameter, false, true);
                        //Update Sys Core with the current Core
                        ProfitCashflow.oPcfDM.SqlQryAllpurpose("SPN_XP_UPD_STK_CORE_SYS_STK", oParameter, false, true);
                    }

                    oParameter = new List<KeyValuePair<string, object>>();
                    oParameter.Add(new KeyValuePair<string, object>("@IN_CURR_STACK_ID", Convert.ToDecimal(drStack["STACK_ID"])));
                    oParameter.Add(new KeyValuePair<string, object>("@IN_COMPANY_ID", Convert.ToDecimal(drStack["COMPANY_ID"])));
                    oParameter.Add(new KeyValuePair<string, object>("@IV_SITE_CODE", Convert.ToString(drStack["SITE_CODE"])));
                    ProfitCashflow.oPcfDM.SqlQryAllpurpose("SPN_XP_UPD_STK_PLOT_SALEABLE", oParameter, false, true);
                }

                if (Data_saved)
                {
                    DeleteLocks();

                    if (pUpdtUseInRep)
                    {
                        sQuery = "UPDATE STACKS SET USE_IN_REPORT = @IC_VALUE ";
                        sQuery += "WHERE COMPANY_ID = @IN_COMPANY_ID AND SITE_CODE = @IV_SITE_CODE AND STACK_ID <> @IN_STACK_ID";
                        oParameter = new List<KeyValuePair<string, object>>();
                        oParameter.Add(new KeyValuePair<string, object>("@IC_VALUE", "N"));
                        oParameter.Add(new KeyValuePair<string, object>("@IN_COMPANY_ID", Convert.ToDecimal(drStack["COMPANY_ID"])));
                        oParameter.Add(new KeyValuePair<string, object>("@IV_SITE_CODE", Convert.ToString(drStack["SITE_CODE"])));
                        oParameter.Add(new KeyValuePair<string, object>("@IN_STACK_ID", Convert.ToDecimal(drStack["STACK_ID"])));
                        ProfitCashflow.oPcfDM.SqlQryAllpurpose(sQuery, oParameter);
                    }
                }

                if (drStack["IS_CURRENT"] != DBNull.Value && Convert.ToString(drStack["IS_CURRENT"]) != "N")
                {
                    //===========================Updating the CURRENT_STACK_ID Field of SITE_SETUP Table===
                    sQuery = "UPDATE SITE_SETUP SET CURRENT_STACK_ID = @IN_STACK_ID, STACK_STATUS = @IC_STACK_STATUS ";
                    sQuery += "WHERE COMPANY_ID = @IN_COMPANY_ID AND SITE_CODE = @IV_SITE_CODE";
                    oParameter = new List<KeyValuePair<string, object>>();
                    oParameter.Add(new KeyValuePair<string, object>("@IN_STACK_ID", Convert.ToDecimal(drStack["STACK_ID"])));
                    oParameter.Add(new KeyValuePair<string, object>("@IN_COMPANY_ID", Convert.ToDecimal(drStack["COMPANY_ID"])));
                    oParameter.Add(new KeyValuePair<string, object>("@IV_SITE_CODE", Convert.ToString(drStack["SITE_CODE"])));
                    oParameter.Add(new KeyValuePair<string, object>("@IC_STACK_STATUS", Convert.ToString(drStack["STACK_STATUS"])));
                    ProfitCashflow.oPcfDM.SqlQryAllpurpose(sQuery, oParameter);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            return true;
        }

        private void DeleteLocks()
        {
            if (Site_lock)
            {
                // Release Lock of Site_setup=======
                sQuery = "DELETE FROM PCF_LOCKED_DOCUMENTS WHERE PCF_DOC_GUID = @IN_PCF_DOC_GUID";
                oParameter = new List<KeyValuePair<string, object>>();
                oParameter.Add(new KeyValuePair<string, object>("@IN_PCF_DOC_GUID", Site_Doc_Guid));
                ProfitCashflow.oPcfDM.SqlQryAllpurpose(sQuery, oParameter);
                //==================================
                Site_lock = false;
            }

            if (drStkSiteDet != null)
            {
                //Delete Phase Lock=================
                sQuery = "DELETE FROM CF_LOCKED_DOCUMENTS WHERE STACK_ID = @IN_STACK_ID AND SESSIONID=@IV_SESSIONID AND FORM_CODE = 'STACK_PHASE'";
                oParameter = new List<KeyValuePair<string, object>>();
                oParameter.Add(new KeyValuePair<string, object>("@IN_STACK_ID", Convert.ToDecimal(drStkSiteDet["SYS_CREATED_STACK_ID"])));
                oParameter.Add(new KeyValuePair<string, object>("@IV_SESSIONID", ProfitCashflow.oPcfDM.session_id));
                ProfitCashflow.oPcfDM.SqlQryAllpurpose(sQuery, oParameter);
                //==================================
                //Delete Block Lock=================
                sQuery = "DELETE FROM CF_LOCKED_DOCUMENTS WHERE STACK_ID = @IN_STACK_ID AND SESSIONID=@IV_SESSIONID AND FORM_CODE = 'STACK_BLOCK'";
                oParameter = new List<KeyValuePair<string, object>>();
                oParameter.Add(new KeyValuePair<string, object>("@IN_STACK_ID", Convert.ToDecimal(drStkSiteDet["SYS_CREATED_STACK_ID"])));
                oParameter.Add(new KeyValuePair<string, object>("@IV_SESSIONID", ProfitCashflow.oPcfDM.session_id));
                ProfitCashflow.oPcfDM.SqlQryAllpurpose(sQuery, oParameter);
                //==================================
                //Delete Core Lock==================
                sQuery = "DELETE FROM CF_LOCKED_DOCUMENTS WHERE STACK_ID = @IN_STACK_ID AND SESSIONID=@IV_SESSIONID AND FORM_CODE = 'STACK_CORE'";
                oParameter = new List<KeyValuePair<string, object>>();
                oParameter.Add(new KeyValuePair<string, object>("@IN_STACK_ID", Convert.ToDecimal(drStkSiteDet["SYS_CREATED_STACK_ID"])));
                oParameter.Add(new KeyValuePair<string, object>("@IV_SESSIONID", ProfitCashflow.oPcfDM.session_id));
                ProfitCashflow.oPcfDM.SqlQryAllpurpose(sQuery, oParameter);
                //==================================
            }

            if (drStack != null && drStack.RowState != DataRowState.Detached)
            {
                sQuery = "DELETE FROM PCF_LOCKED_DOCUMENTS WHERE PCF_DOC_GUID = @IN_DOC_GUID AND SESSIONID=@IV_SESSIONID";
                oParameter = new List<KeyValuePair<string, object>>();
                oParameter.Add(new KeyValuePair<string, object>("@IN_DOC_GUID", Convert.ToDecimal(drStack["PCF_DOC_GUID"])));
                oParameter.Add(new KeyValuePair<string, object>("@IV_SESSIONID", ProfitCashflow.oPcfDM.session_id));
                ProfitCashflow.oPcfDM.SqlQryAllpurpose(sQuery, oParameter);
            }
        }

        private void Clear_Plot_fld()
        {
            cxCurrencyEditStartPlot.EditValue = null;
            cxCurrencyEditNoofPlots.EditValue = null;
            cxTextEditExcludePlots.EditValue = null;
            cxCurrencyEditMemoPlots.EditValue = null;
            cxLookupComboBoxPhaseForPlot.EditValue = null;
            cxLookupComboBoxBlockForPlot.EditValue = null;
            cxLookupComboBoxCoreForPlot.EditValue = null;
            cxLookupComboBoxPlotTypeForPlot.EditValue = null;
            cxLookupComboBoxPlotResType.EditValue = null;
            cxCurrencyEditNIFA.EditValue = null;
            cxCurrencyEditPlotSales.EditValue = null;
            cxCurrencyEditPlotLand.EditValue = null;
            cxCurrencyEditPlotOverage.EditValue = null;
            cxCurrencyEditPlotBuild.EditValue = null;
            cxCurrencyEditPlotMaintRes.EditValue = null;
            cxCurrencyEditPlotMktgCost.EditValue = null;
            cxCurrencyEditPlotLegalCost.EditValue = null;
            cxCurrencyEditPlotSalesCom.EditValue = null;
        }

        private void ClearValue()
        {
            cxGridHistory.DataSource = null;
            cxGridPhase.DataSource = null;
            cxGridBlock.DataSource = null;
            cxGridCore.DataSource = null;
            cxGridPlotSetup.DataSource = null;
            cxGrid2.DataSource = null;
            cxGridStackDocs.DataSource = null;

            cxDBLookupComboBoxComp.EditValue = null;
            cxDBLookupComboBoxSite.EditValue = null;
            cxDBImageComboBox1.EditValue = null;
            //cxDBCheckBox1.EditValue = null;
            checkEdit2.EditValue = null;
            cxDBTextEditVersion.EditValue = null;
            cxDBImageComboBox2.EditValue = null;
            cxDBImageComboBox3.EditValue = null;
            cxDBMemo1.EditValue = null;
            cxDBCurrencyEditTotPlots.EditValue = null;
            cxDBCurrencyEditTotPhases.EditValue = null;
            cxDBCurrencyEditTotBlocks.EditValue = null;
            cxDBCurrencyEditTotCores.EditValue = null;
            cxDBTextEdit1.EditValue = null;
            cxDBTextEdit2.EditValue = null;
            cxDBTextEdit3.EditValue = null;

            //lObjAttach = new List<templateByteCollection>();
        }

        private void UpdatePlot_Ind(bool b_PE)
        {
            if (b_PE)
            {
                //======Filling drop down for plot indicator for Part Exchange
                repIcbPlotIndicator.Items.Clear();
                foreach (DevExpress.XtraEditors.Controls.ImageComboBoxItem oItem in repIcbPlotInd.Items)
                    if (oItem.Value.ToString() == "P")
                    {
                        repIcbPlotIndicator.Items.Add(oItem);
                        //repIcbPlotIndicator.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(oItem.Description, oItem.Value, -1));
                    }
            }
            else
            {
                //======Filling drop down for plot indicator other than Part Exchange
                repIcbPlotIndicator.Items.Clear();
                foreach (DevExpress.XtraEditors.Controls.ImageComboBoxItem oItem in repIcbPlotInd.Items)
                    if (oItem.Value.ToString() != "P")
                    {
                        repIcbPlotIndicator.Items.Add(oItem);
                        //repIcbPlotIndicator.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(oItem.Description, oItem.Value, -1));
                    }
            }
        }

        private bool Can_Change_Del_Doc(decimal pcf_doc_guid, decimal stack_id)
        {
            sQuery = "SELECT COUNT(1) AS CNT FROM STACKS WHERE PCF_DOC_GUID=@IN_PCF_DOC_GUID";
            oParameter = new List<KeyValuePair<string, object>>();
            oParameter.Add(new KeyValuePair<string, object>("@IN_PCF_DOC_GUID", pcf_doc_guid));
            object oResult = ProfitCashflow.oPcfDM.SqlQryAllpurpose(sQuery, oParameter, true);

            if (oResult != null && Convert.ToInt16(oResult) == 0)
            {
                XtraMessageBox.Show("Selected document has been deleted.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            sQuery = "SELECT COUNT(1) AS CNT FROM PCF_LOCKED_DOCUMENTS WHERE PCF_DOC_GUID=@IN_PCF_DOC_GUID";
            oParameter = new List<KeyValuePair<string, object>>();
            oParameter.Add(new KeyValuePair<string, object>("@IN_PCF_DOC_GUID", Convert.ToDecimal(drStack["PCF_DOC_GUID"])));
            oResult = ProfitCashflow.oPcfDM.SqlQryAllpurpose(sQuery, oParameter, true);

            if (oResult != null && Convert.ToInt16(oResult) > 0)
            {
                XtraMessageBox.Show("Selected Document # is locked, hence cannot delete.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            return true;
        }

        private void cxDBLookupComboBoxComp_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (lFormMode != 'B')
                e.Cancel = !(dtStackPhaseDet.Rows.Count == 0 &&
                            dtStackBlockDet.Rows.Count == 0 &&
                            dtStackCoreDet.Rows.Count == 0 &&
                            dtStackPlotDet.Rows.Count == 0 &&
                            dtStackDocs.Rows.Count == 0);
        }

        private void cxDBLookupComboBoxComp_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                cxDBLookupComboBoxSite.EditValue = null;
                if (cxDBLookupComboBoxComp.EditValue != null && !string.IsNullOrWhiteSpace(cxDBLookupComboBoxComp.EditValue.ToString()))
                {
                    dtSites = oStkDM.RetrieveStackSiteList(Convert.ToDecimal(cxDBLookupComboBoxComp.EditValue));
                    dtSites.Constraints.Add("PK_SITE", dtSites.Columns["SITE_CODE"], true);
                    cxDBLookupComboBoxSite.Properties.DataSource = dtSites;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cxDBLookupComboBoxComp_Enter(object sender, EventArgs e)
        {
            if (dtCompany != null)
            {
                DataView dvComp = new DataView(dtCompany, "IS_ACTIVE='Y'", "", DataViewRowState.CurrentRows);
                if (cxDBLookupComboBoxComp.EditValue != null && !string.IsNullOrWhiteSpace(cxDBLookupComboBoxComp.EditValue.ToString()))
                    dvComp.RowFilter += " OR COMPANY_ID=" + cxDBLookupComboBoxComp.EditValue;
                cxDBLookupComboBoxComp.Properties.DataSource = dvComp;
            }
        }

        private void cxDBLookupComboBoxComp_Leave(object sender, EventArgs e)
        {
            cxDBLookupComboBoxComp.Properties.DataSource = dtCompany;
        }

        private void cxDBLookupComboBoxSite_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (lFormMode != 'B')
                e.Cancel = !(cxDBLookupComboBoxComp.EditValue != null && !string.IsNullOrWhiteSpace(cxDBLookupComboBoxComp.EditValue.ToString()) &&
                            dtStackPhaseDet.Rows.Count == 0 &&
                            dtStackBlockDet.Rows.Count == 0 &&
                            dtStackCoreDet.Rows.Count == 0 &&
                            dtStackPlotDet.Rows.Count == 0 &&
                            dtStackDocs.Rows.Count == 0);
        }

        private void cxDBLookupComboBoxSite_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (cxDBLookupComboBoxSite.EditValue != null && !string.IsNullOrWhiteSpace(cxDBLookupComboBoxSite.EditValue.ToString()))
                {
                    decimal dCompId = Convert.ToDecimal(cxDBLookupComboBoxComp.EditValue);
                    string sSiteCode = Convert.ToString(cxDBLookupComboBoxSite.EditValue);
                    drStkSiteDet = oStkDM.RetrieveStackSiteDetail(dCompId, sSiteCode);

                    // Insert Sys_Created_Phase's into the new stack
                    if (lFormMode == 'N' && drStkSiteDet != null && drStkSiteDet["SYS_CREATED_STACK_ID"] != DBNull.Value)
                    {
                        dtStkSysPhase = oStkDM.RetrieveStackSysCreatedPhase(Convert.ToDecimal(drStkSiteDet["SYS_CREATED_STACK_ID"]));
                        if (dtStkSysPhase != null && dtStkSysPhase.Rows.Count > 0)
                            foreach (DataRow drSysPhase in dtStkSysPhase.Rows)
                            {
                                DataRow drPhase = dtStackPhaseDet.NewRow();
                                drPhase["STACK_ID"] = drStack["STACK_ID"];
                                drPhase["PHASE_ID"] = drSysPhase["PHASE_ID"];
                                drPhase["PHASE"] = drSysPhase["PHASE"];
                                drPhase["BLDG_UNDER_LICENSE"] = drSysPhase["BLDG_UNDER_LICENSE"];
                                dtStackPhaseDet.Rows.Add(drPhase);
                            }

                        cxTextEdit1.EditValue = drStkSiteDet["STACK_CODE"].ToString();
                    }

                    //GetSitePlotRange
                    if (dCompId != dOptCompId && sSiteCode != sOptSiteCode)
                    {
                        dtPlotRange = oStkDM.RetrieveStackSitePlotRange(dCompId, sSiteCode);
                        gcPlotRange.DataSource = dtPlotRange;
                        dOptCompId = dCompId;
                        sOptSiteCode = sSiteCode;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cxDBLookupComboBoxSite_Enter(object sender, EventArgs e)
        {
            if (cxDBLookupComboBoxComp.EditValue != null && !string.IsNullOrWhiteSpace(cxDBLookupComboBoxComp.EditValue.ToString()))
            {
                if (lFormMode == 'N')   //Flag == "N"
                {
                    dtSitesNew = oStkDM.RetrieveStackSiteNewList(Convert.ToDecimal(cxDBLookupComboBoxComp.EditValue));
                    cxDBLookupComboBoxSite.Properties.DataSource = dtSitesNew;
                }
                else if (dtSites != null)
                {
                    DataView dvSites = new DataView(dtSites, "STATUS='AC'", "", DataViewRowState.CurrentRows);
                    if (cxDBLookupComboBoxSite.EditValue != null && !string.IsNullOrWhiteSpace(cxDBLookupComboBoxSite.EditValue.ToString()))
                        dvSites.RowFilter += " OR SITE_CODE='" + cxDBLookupComboBoxSite.EditValue + "'";
                    cxDBLookupComboBoxSite.Properties.DataSource = dvSites;
                }
            }
        }

        private void cxDBLookupComboBoxSite_Leave(object sender, EventArgs e)
        {
            cxDBLookupComboBoxSite.Properties.DataSource = dtSites;
        }

        private void DoValidateControls()
        {
            cxDBLookupComboBoxComp.DoValidate();
            cxDBLookupComboBoxSite.DoValidate();
            cxDBImageComboBox1.DoValidate();
            checkEdit2.DoValidate();
            cxDBTextEditVersion.DoValidate();
            cxDBImageComboBox2.DoValidate();
            cxDBImageComboBox3.DoValidate();
            cxDBMemo1.DoValidate();
            cxDBCurrencyEditTotPlots.DoValidate();
            cxDBCurrencyEditTotPhases.DoValidate();
            cxDBCurrencyEditTotBlocks.DoValidate();
            cxDBCurrencyEditTotCores.DoValidate();
            cxDBTextEdit1.DoValidate();
            cxDBTextEdit2.DoValidate();
            cxDBTextEdit3.DoValidate();
        }

        private bool ValidateStacksControls()
        {
            if (cxDBLookupComboBoxComp.EditValue == null || string.IsNullOrWhiteSpace(cxDBLookupComboBoxComp.EditValue.ToString().Trim()))
            {
                XtraMessageBox.Show("Company is mandatory.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                cxDBLookupComboBoxComp.Focus();
                return false;
            }
            if (cxDBLookupComboBoxSite.EditValue == null || string.IsNullOrWhiteSpace(cxDBLookupComboBoxSite.EditValue.ToString().Trim()))
            {
                XtraMessageBox.Show("Site is mandatory.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                cxDBLookupComboBoxSite.Focus();
                return false;
            }
            if (cxDBImageComboBox1.EditValue == null || string.IsNullOrWhiteSpace(cxDBImageComboBox1.EditValue.ToString().Trim()))
            {
                XtraMessageBox.Show("Stack Status is mandatory.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                cxDBImageComboBox1.Focus();
                return false;
            }

            drStack["CREATED_BY"] = ProfitCashflow.oPcfDM.UserName;
            drStack["LAST_UPDATED"] = DateTime.Now;

            vLandStatus = Convert.ToString(drStkSiteDet["STACK_CODE"]);
            vStackStatus = Convert.ToString(drStack["STACK_STATUS"]);
            vActive = Convert.ToString(drStack["IS_CURRENT"]);

            if (((lFormMode == 'N' || drStack.RowState == DataRowState.Added) && Convert.ToString(drStack["USE_IN_REPORT"]) == "Y") || ((lFormMode == 'E' || drStack.RowState == DataRowState.Modified) &&
                Convert.ToString(drStack["USE_IN_REPORT", DataRowVersion.Original]) == "N" && Convert.ToString(drStack["USE_IN_REPORT", DataRowVersion.Current]) == "Y"))
                pUpdtUseInRep = true;
            else
                pUpdtUseInRep = false;

            return true;
        }

        private void dxBarButtonPNextStack_Click(object sender, EventArgs e)
        {
            if (drStack == null) return;

            DataRow[] dr = dtStackView.Select("COMPANY_ID = " + Convert.ToDecimal(drStack["COMPANY_ID"]) + "AND SITE_CODE = '" + Convert.ToString(drStack["SITE_CODE"]) + "'");
            if (dr.Length > 0)
            {
                LandStatus frmLandStatus = new LandStatus();
                frmLandStatus.rgLandStatus.EditValue = Convert.ToString(dr[0]["LAND_STATUS"]);
                DialogResult oDialogResult = frmLandStatus.ShowDialog();
                if (oDialogResult == DialogResult.OK)
                {
                    try
                    {
                        oComm.UpdateSiteLandStatus(Convert.ToDecimal(drStack["COMPANY_ID"]), Convert.ToString(drStack["SITE_CODE"]), frmLandStatus.rgLandStatus.EditValue.ToString());
                        cxTextEdit1.EditValue = frmLandStatus.rgLandStatus.EditValue.ToString();
                        // DataRow[] dr = dtStackView.Select("COMPANY_ID = " + Convert.ToDecimal(drStack["COMPANY_ID"]) + "AND SITE_CODE = '" + Convert.ToString(drStack["SITE_CODE"]) + "'");
                        foreach (DataRow drStatus in dr)
                        {
                            drStatus["LAND_STATUS"] = cxTextEdit1.EditValue.ToString();
                        }
                        dtStackView.AcceptChanges();
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
        }

        private void checkEdit2_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                decimal stack_id = -1;
                int Version = -1;

                if ((lFormMode == 'E' || drStack.RowState == DataRowState.Modified) && checkEdit2.Checked)
                {
                    DataTable dtVerInfo = oStkDM.RetrieveStkVerInfoOnActiveChng(drStack, drStkSiteDet["STACK_CODE"].ToString());
                    if (dtVerInfo != null && dtVerInfo.Rows.Count > 0)
                    {
                        stack_id = Convert.ToDecimal(dtVerInfo.Rows[0]["STACK_ID"]);
                        Version = Convert.ToInt16(dtVerInfo.Rows[0]["VERSION"]);
                    }
                }
                else if ((lFormMode == 'E' || drStack.RowState == DataRowState.Modified) && !checkEdit2.Checked)
                {
                    stack_id = Convert.ToDecimal(drStack["STACK_ID"]);
                    Version = Convert.ToInt16(drStack["VERSION"]);
                }

                if (stack_id != -1)
                    if (check_is_current(stack_id, Version, "C") == 0)
                    {
                        checkEdit2.CheckedChanged -= checkEdit2_CheckedChanged;
                        if (!checkEdit2.Checked || Convert.ToString(drStack["IS_CURRENT"]) == "N")
                            drStack["IS_CURRENT"] = "Y";
                        else
                            drStack["IS_CURRENT"] = "N";
                        checkEdit2.CheckedChanged += checkEdit2_CheckedChanged;
                        return;
                    }
                    else if (checkEdit2.Checked || Convert.ToString(drStack["IS_CURRENT"]) == "Y")
                    {
                        sQuery = "UPDATE STACKS SET IS_CURRENT = 'N' WHERE STACK_ID = @IN_STACK_ID";
                        oParameter = new List<KeyValuePair<string, object>>();
                        oParameter.Add(new KeyValuePair<string, object>("@IN_STACK_ID", stack_id));
                        ProfitCashflow.oPcfDM.SqlQryAllpurpose(sQuery, oParameter);
                    }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int check_is_current(decimal stack_id, int version, string Check_Delete)
        {
            try
            {
                string msg;
                if (Check_Delete == "C")
                    msg = " which is marked as current, hence cannot mark this version as current.";
                else
                    msg = ", hence cannot delete.";

                sQuery = "SELECT COUNT(1) AS CNT FROM PROFIT_FORECAST WHERE STACK_ID = @IN_STACK_ID";
                oParameter = new List<KeyValuePair<string, object>>();
                oParameter.Add(new KeyValuePair<string, object>("@IN_STACK_ID", stack_id));
                object oResult = ProfitCashflow.oPcfDM.SqlQryAllpurpose(sQuery, oParameter, true);

                if (oResult != null && Convert.ToInt16(oResult) > 0)
                {
                    XtraMessageBox.Show("Profit Forecast has been done for the selected record" + msg, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return 0;
                }

                sQuery = "SELECT COUNT(1) AS CNT FROM ORPC_DATES WHERE STACK_ID = @IN_STACK_ID";
                oParameter = new List<KeyValuePair<string, object>>();
                oParameter.Add(new KeyValuePair<string, object>("@IN_STACK_ID", stack_id));
                oResult = ProfitCashflow.oPcfDM.SqlQryAllpurpose(sQuery, oParameter, true);

                if (oResult != null && Convert.ToInt16(oResult) > 0)
                {
                    XtraMessageBox.Show("ORPC dates are entered for the selected record" + msg, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return 0;
                }

                sQuery = "SELECT COUNT(1) AS CNT FROM SPC_PURCHASE WHERE STACK_ID = @IN_STACK_ID";
                oParameter = new List<KeyValuePair<string, object>>();
                oParameter.Add(new KeyValuePair<string, object>("@IN_STACK_ID", stack_id));
                oResult = ProfitCashflow.oPcfDM.SqlQryAllpurpose(sQuery, oParameter, true);

                if (oResult != null && Convert.ToInt16(oResult) > 0)
                {
                    XtraMessageBox.Show("Purchase Key dates are entered for stack " + drStkSiteDet["STACK_CODE"].ToString() + " and Version : " + version.ToString() + msg, 
                        Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return 0;
                }

                sQuery = "SELECT COUNT(1) AS CNT FROM SPC_PLANNING WHERE STACK_ID = @IN_STACK_ID";
                oParameter = new List<KeyValuePair<string, object>>();
                oParameter.Add(new KeyValuePair<string, object>("@IN_STACK_ID", stack_id));
                oResult = ProfitCashflow.oPcfDM.SqlQryAllpurpose(sQuery, oParameter, true);

                if (oResult != null && Convert.ToInt16(oResult) > 0)
                {
                    XtraMessageBox.Show("Planning Key dates are entered for stack " + drStkSiteDet["STACK_CODE"].ToString() + " and Version : " + version.ToString() + msg,
                        Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return 0;
                }

                sQuery = "SELECT COUNT(1) AS CNT FROM SPC_CONSTRUCTION WHERE STACK_ID = @IN_STACK_ID";
                oParameter = new List<KeyValuePair<string, object>>();
                oParameter.Add(new KeyValuePair<string, object>("@IN_STACK_ID", stack_id));
                oResult = ProfitCashflow.oPcfDM.SqlQryAllpurpose(sQuery, oParameter, true);

                if (oResult != null && Convert.ToInt16(oResult) > 0)
                {
                    XtraMessageBox.Show("Construction Key dates are entered for stack " + drStkSiteDet["STACK_CODE"].ToString() + " and Version : " + version.ToString() + msg,
                        Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return 0;
                }

                sQuery = "SELECT COUNT(1) AS CNT FROM SPC_SALES WHERE STACK_ID = @IN_STACK_ID";
                oParameter = new List<KeyValuePair<string, object>>();
                oParameter.Add(new KeyValuePair<string, object>("@IN_STACK_ID", stack_id));
                oResult = ProfitCashflow.oPcfDM.SqlQryAllpurpose(sQuery, oParameter, true);

                if (oResult != null && Convert.ToInt16(oResult) > 0)
                {
                    XtraMessageBox.Show("Sales Key dates are entered for stack " + drStkSiteDet["STACK_CODE"].ToString() + " and Version : " + version.ToString() + msg,
                        Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return 0;
                }

                sQuery = "SELECT COUNT(1) AS CNT FROM SPC_BUDGET_STACK WHERE STACK_ID = @IN_STACK_ID";
                oParameter = new List<KeyValuePair<string, object>>();
                oParameter.Add(new KeyValuePair<string, object>("@IN_STACK_ID", stack_id));
                oResult = ProfitCashflow.oPcfDM.SqlQryAllpurpose(sQuery, oParameter, true);

                if (oResult != null && Convert.ToInt16(oResult) > 0)
                {
                    XtraMessageBox.Show("Budget Stacks Key dates are entered for stack " + drStkSiteDet["STACK_CODE"].ToString() + " and Version : " + version.ToString() + msg,
                        Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return 0;
                }

                sQuery = "SELECT COUNT(1) AS CNT FROM TAKE_TO_SALES_PLOT WHERE STACK_ID = @IN_STACK_ID";
                oParameter = new List<KeyValuePair<string, object>>();
                oParameter.Add(new KeyValuePair<string, object>("@IN_STACK_ID", stack_id));
                oResult = ProfitCashflow.oPcfDM.SqlQryAllpurpose(sQuery, oParameter, true);

                if (oResult != null && Convert.ToInt16(oResult) > 0)
                {
                    XtraMessageBox.Show("Take Plot to Sales Detail are entered for stack " + drStkSiteDet["STACK_CODE"].ToString() + " and Version : " + version.ToString() + msg,
                        Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return 0;
                }

                sQuery = "SELECT COUNT(1) AS CNT FROM TAKE_TO_SALES_PARKING WHERE STACK_ID = @IN_STACK_ID";
                oParameter = new List<KeyValuePair<string, object>>();
                oParameter.Add(new KeyValuePair<string, object>("@IN_STACK_ID", stack_id));
                oResult = ProfitCashflow.oPcfDM.SqlQryAllpurpose(sQuery, oParameter, true);

                if (oResult != null && Convert.ToInt16(oResult) > 0)
                {
                    XtraMessageBox.Show("Take Parking to Sales Detail are entered for stack " + drStkSiteDet["STACK_CODE"].ToString() + " and Version : " + version.ToString() + msg,
                        Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return 0;
                }

                sQuery = "SELECT COUNT(1) AS CNT FROM CASHFLOW_SALES WHERE STACK_ID = @IN_STACK_ID";
                oParameter = new List<KeyValuePair<string, object>>();
                oParameter.Add(new KeyValuePair<string, object>("@IN_STACK_ID", stack_id));
                oResult = ProfitCashflow.oPcfDM.SqlQryAllpurpose(sQuery, oParameter, true);

                if (oResult != null && Convert.ToInt16(oResult) > 0)
                {
                    XtraMessageBox.Show("Sales Cashflow detail are entered for stack " + drStkSiteDet["STACK_CODE"].ToString() + " and Version : " + version.ToString() + msg,
                        Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return 0;
                }

                sQuery = "SELECT COUNT(1) AS CNT FROM CASHFLOW_BUILD WHERE STACK_ID = @IN_STACK_ID";
                oParameter = new List<KeyValuePair<string, object>>();
                oParameter.Add(new KeyValuePair<string, object>("@IN_STACK_ID", stack_id));
                oResult = ProfitCashflow.oPcfDM.SqlQryAllpurpose(sQuery, oParameter, true);

                if (oResult != null && Convert.ToInt16(oResult) > 0)
                {
                    XtraMessageBox.Show("Build Cashflow detail are entered for stack " + drStkSiteDet["STACK_CODE"].ToString() + " and Version : " + version.ToString() + msg,
                        Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return 0;
                }

                sQuery = "SELECT COUNT(1) AS CNT FROM CASHFLOW_LAND WHERE STACK_ID = @IN_STACK_ID";
                oParameter = new List<KeyValuePair<string, object>>();
                oParameter.Add(new KeyValuePair<string, object>("@IN_STACK_ID", stack_id));
                oResult = ProfitCashflow.oPcfDM.SqlQryAllpurpose(sQuery, oParameter, true);

                if (oResult != null && Convert.ToInt16(oResult) > 0)
                {
                    XtraMessageBox.Show("Land Cashflow detail are entered for stack " + drStkSiteDet["STACK_CODE"].ToString() + " and Version : " + version.ToString() + msg,
                        Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return 0;
                }

                sQuery = "SELECT COUNT(1) AS CNT FROM CASHFLOW_OTHER_INC WHERE STACK_ID = @IN_STACK_ID";
                oParameter = new List<KeyValuePair<string, object>>();
                oParameter.Add(new KeyValuePair<string, object>("@IN_STACK_ID", stack_id));
                oResult = ProfitCashflow.oPcfDM.SqlQryAllpurpose(sQuery, oParameter, true);

                if (oResult != null && Convert.ToInt16(oResult) > 0)
                {
                    XtraMessageBox.Show("Other Income Cashflow detail are entered for stack " + drStkSiteDet["STACK_CODE"].ToString() + " and Version : " + version.ToString() + msg,
                        Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return 0;
                }

                sQuery = "SELECT COUNT(1) AS CNT FROM CASHFLOW_OTHER_INC_USERDEF WHERE STACK_ID = @IN_STACK_ID";
                oParameter = new List<KeyValuePair<string, object>>();
                oParameter.Add(new KeyValuePair<string, object>("@IN_STACK_ID", stack_id));
                oResult = ProfitCashflow.oPcfDM.SqlQryAllpurpose(sQuery, oParameter, true);

                if (oResult != null && Convert.ToInt16(oResult) > 0)
                {
                    XtraMessageBox.Show("Other User Defined Income Cashflow detail are entered for stack " + drStkSiteDet["STACK_CODE"].ToString() + " and Version : " + version.ToString() + msg,
                        Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return 0;
                }

                sQuery = "SELECT COUNT(1) AS CNT FROM CASHFLOW_OTHER_EXP WHERE STACK_ID = @IN_STACK_ID";
                oParameter = new List<KeyValuePair<string, object>>();
                oParameter.Add(new KeyValuePair<string, object>("@IN_STACK_ID", stack_id));
                oResult = ProfitCashflow.oPcfDM.SqlQryAllpurpose(sQuery, oParameter, true);

                if (oResult != null && Convert.ToInt16(oResult) > 0)
                {
                    XtraMessageBox.Show("Other Expense Cashflow detail are entered for stack " + drStkSiteDet["STACK_CODE"].ToString() + " and Version : " + version.ToString() + msg,
                        Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return 0;
                }

                sQuery = "SELECT COUNT(1) AS CNT FROM CASHFLOW_OTHER_EXP_USERDEF WHERE STACK_ID = @IN_STACK_ID";
                oParameter = new List<KeyValuePair<string, object>>();
                oParameter.Add(new KeyValuePair<string, object>("@IN_STACK_ID", stack_id));
                oResult = ProfitCashflow.oPcfDM.SqlQryAllpurpose(sQuery, oParameter, true);

                if (oResult != null && Convert.ToInt16(oResult) > 0)
                {
                    XtraMessageBox.Show("Other User Defined Expense Cashflow detail are entered for stack " + drStkSiteDet["STACK_CODE"].ToString() + " and Version : " + version.ToString() + msg,
                        Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return 0;
                }

                return 1;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
        }

        private void ControlFigures_Validated(object sender, EventArgs e)
        {
            string sFor = "A";
            if (((TextEdit)sender) == cxDBCurrencyEditTotPlots)
                sFor = "U";
            else if (((TextEdit)sender) == cxDBCurrencyEditTotPhases)
                sFor = "P";
            else if (((TextEdit)sender) == cxDBCurrencyEditTotBlocks)
                sFor = "B";
            else if (((TextEdit)sender) == cxDBCurrencyEditTotCores)
                sFor = "C";

            SetColours(sFor);
        }

        private int GenLineno(DataTable dtTemp, string sFieldname)
        {
            if (dtTemp == null || dtTemp.Rows.Count == 0)
                return 1;

            object oResult = dtTemp.Compute("MAX(" + sFieldname + ")", "");
            if (oResult != null && !string.IsNullOrWhiteSpace(Convert.ToString(oResult)))
                return Convert.ToInt16(oResult) + 1;

            return 0;
        }

        private int GetMax_PH_BL_CO_ID(decimal stack_id, string str_FLG)
        {
            string str_sql = "";

            if (str_FLG == "PH")
                str_sql = "SELECT MAX(PHASE_ID) FROM STACK_PHASE_DETAILS WHERE STACK_ID = " + Convert.ToString(stack_id);
            else if (str_FLG == "BL")
                str_sql = "SELECT MAX(BLOCK_ID) FROM STACK_BLOCK_DETAILS WHERE STACK_ID = " + Convert.ToString(stack_id);
            else if (str_FLG == "CO")
                str_sql = "SELECT MAX(CORE_ID) FROM STACK_CORE_DETAILS WHERE STACK_ID = " + Convert.ToString(stack_id);

            if (!string.IsNullOrWhiteSpace(str_sql))
            {
                object oResult = ProfitCashflow.oPcfDM.SqlQryAllpurpose(str_sql, null, true);
                if (oResult != null && !string.IsNullOrWhiteSpace(Convert.ToString(oResult)))
                    return Convert.ToInt16(oResult) + 1;
            }

            return 0;
        }

        private bool Chk_Lock_Ph_Bl_Co(decimal Comp_id, decimal Stack_id, string Str_Flag, string Str_FormCode)
        {
            string str_sql, str_inssql, str_code = "", str_code1 = "";

            if (Str_Flag == "PH")
            {
                str_code = "'%_PHASE%'";
                str_code1 = "PF_BULK_PHASE";
            }
            else if (Str_Flag == "BL")
            {
                str_code = "'%_BLOCK%'";
                str_code1 = "PF_BULK_BLOCK";
            }
            else if (Str_Flag == "CO")
            {
                str_code = "'%_CORE%'";
                str_code1 = "PF_BULK_CORE";
            }

            str_sql = "SELECT STACK_ID, FORM_CODE,SESSIONID FROM CF_LOCKED_DOCUMENTS " +
                      "WHERE STACK_ID = " + Convert.ToString(Stack_id) +
                      " AND FORM_CODE LIKE " + str_code;

            DataTable dtInfo = ProfitCashflow.oPcfDM.SqlQryGetInfo(str_sql, null, false);
            if (dtInfo != null)
                if (dtInfo.Select("FORM_CODE = '" + str_code1 + "'").Length > 0)
                    return false;
                else if (dtInfo.Select("FORM_CODE = '" + Str_FormCode + "'").Length > 0)
                {
                    if (dtInfo.Select("FORM_CODE = '" + Str_FormCode + "' AND SESSIONID = '" + ProfitCashflow.oPcfDM.session_id + "'").Length > 0)
                        return false;
                    else
                        return true;
                }
                else
                {
                    try
                    {
                        str_inssql = "INSERT INTO CF_LOCKED_DOCUMENTS(USERNAME, SESSIONID, FORM_CODE, COMPANY_ID, STACK_ID, PHASE_ID, CREATED_ON) " +
                                     "VALUES ('" + ProfitCashflow.oPcfDM.UserName + "', '" + ProfitCashflow.oPcfDM.session_id + "', '" + Str_FormCode +
                                     "', " + Comp_id + ", " + Stack_id + ", -1, '" + DateTime.Now.ToString(CultureInfo.InvariantCulture) + "')";

                        ProfitCashflow.oPcfDM.SqlQryAllpurpose(str_inssql, null);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            return false;
        }

        private void cxGridPhaseTv_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            int int_temp, int_temp1;

            if (drStkSiteDet != null)
            {
                DataRow drStkPhase = cxGridPhaseTv.GetDataRow(e.RowHandle);
                drStkPhase["STACK_ID"] = drStack["STACK_ID"];
                drStkPhase["BLDG_UNDER_LICENSE"] = "N";

                int_temp = GenLineno(dtStackPhaseDet, "PHASE_ID");
                int_temp1 = GetMax_PH_BL_CO_ID(Convert.ToDecimal(drStkSiteDet["SYS_CREATED_STACK_ID"]), "PH");

                if (int_temp >= int_temp1)
                    drStkPhase["PHASE_ID"] = int_temp;
                else if (int_temp < int_temp1)
                    drStkPhase["PHASE_ID"] = int_temp1;
            }
        }

        private void cxGridPhaseTv_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (drStkSiteDet != null)
                if (!Chk_Lock_Ph_Bl_Co(Convert.ToDecimal(drStack["COMPANY_ID"]), Convert.ToDecimal(drStkSiteDet["SYS_CREATED_STACK_ID"]), "PH", "STACK_PHASE"))
                {
                    if (cxGridPhaseTv.FocusedRowHandle < 0)
                        XtraMessageBox.Show("Cannot add a new Phase, as the Phase Details has been locked by Profit Forecast Bulk Update.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                        XtraMessageBox.Show("Cannot Edit as the Phase Document is locked.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                }
        }

        private void cxGridPhaseTv_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            DataRow drStkPhase = cxGridPhaseTv.GetDataRow(e.RowHandle);
            if (drStkPhase == null) return;

            if (drStkPhase["PHASE"] == DBNull.Value || drStkPhase["PHASE"].ToString().Trim() == string.Empty)
            {
                XtraMessageBox.Show("Phase is mandatory.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Valid = false;
                return;
            }
            else
            {
                drStkPhase["UPPER_PHASE"] = drStkPhase["PHASE"];

                DataRow[] drRows = dtStackPhaseDet.Select("UPPER_PHASE = '" + drStkPhase["PHASE"].ToString().ToUpper() + "'");
                foreach (DataRow drCode in drRows)
                    if (dtStackPhaseDet.Rows.IndexOf(drCode) != dtStackPhaseDet.Rows.IndexOf(drStkPhase) && drCode.RowState != DataRowState.Deleted)
                        if (Convert.ToString(drCode["UPPER_PHASE"]) == Convert.ToString(drStkPhase["UPPER_PHASE"]))
                        {
                            XtraMessageBox.Show("Duplicate Phase is not allowed.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            e.Valid = false;
                            return;
                        }
            }
            
            if (drStkSiteDet != null)
            {
                sQuery = "SELECT PHASE_ID FROM STACK_PHASE_DETAILS ";
                sQuery += "WHERE UPPER(PHASE) = @IV_PHASE AND STACK_ID = @IN_STACK_ID";
                oParameter = new List<KeyValuePair<string, object>>();
                oParameter.Add(new KeyValuePair<string, object>("@IN_STACK_ID", drStkSiteDet["SYS_CREATED_STACK_ID"]));
                oParameter.Add(new KeyValuePair<string, object>("@IV_PHASE", drStkPhase["PHASE"].ToString().ToUpper()));

                object oResult = ProfitCashflow.oPcfDM.SqlQryAllpurpose(sQuery, oParameter, true);
                if (oResult != null && Convert.ToDecimal(oResult) > 0)
                    drStkPhase["PHASE_ID"] = Convert.ToDecimal(oResult);
            }
        }

        private void cxGridPhaseTv_KeyDown(object sender, KeyEventArgs e)
        {
            if (cxGridPhaseTv.OptionsBehavior.Editable)
                if (e.Control && e.KeyCode == Keys.Delete && cxGridPhaseTv.FocusedRowHandle > 0)
                {
                    DialogResult oTempResult = XtraMessageBox.Show("Delete record?", Application.ProductName, MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (oTempResult == DialogResult.OK)
                    {
                        try
                        {
                            DataRow drStkPhase = cxGridPhaseTv.GetDataRow(cxGridPhaseTv.FocusedRowHandle);

                            sQuery = "SELECT COUNT(1) AS CNT FROM STACK_PLOT_DETAILS ";
                            sQuery += "WHERE STACK_ID = @IN_STACK_ID AND PHASE_ID = @IN_PHASE_ID";
                            oParameter = new List<KeyValuePair<string, object>>();
                            oParameter.Add(new KeyValuePair<string, object>("@IN_STACK_ID", drStkPhase["STACK_ID"]));
                            oParameter.Add(new KeyValuePair<string, object>("@IN_PHASE_ID", drStkPhase["PHASE_ID"]));

                            object oResult = ProfitCashflow.oPcfDM.SqlQryAllpurpose(sQuery, oParameter, true);
                            if (oResult != null && Convert.ToDecimal(oResult) > 0)
                            {
                                XtraMessageBox.Show("Cannot delete, as the Phase is selected in Plot Setup.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            if (drStkPhase.RowState == DataRowState.Added)
                                drStkPhase.RejectChanges();
                            else
                                drStkPhase.Delete();
                            SetColours("P");
                        }
                        catch (Exception ex)
                        {
                            XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
        }

        private void cxGrid_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void cxGridTotal_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            SetColours("P");
        }

        private void cxGridBlockDBTableView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            int int_temp, int_temp1;

            if (drStkSiteDet != null)
            {
                DataRow drStkBlock = cxGridBlockDBTableView1.GetDataRow(e.RowHandle);
                drStkBlock["STACK_ID"] = drStack["STACK_ID"];

                int_temp = GenLineno(dtStackBlockDet, "BLOCK_ID");
                int_temp1 = GetMax_PH_BL_CO_ID(Convert.ToDecimal(drStkSiteDet["SYS_CREATED_STACK_ID"]), "BL");

                if (int_temp >= int_temp1)
                    drStkBlock["BLOCK_ID"] = int_temp;
                else if (int_temp < int_temp1)
                    drStkBlock["BLOCK_ID"] = int_temp1;
            }
        }

        private void cxGridBlockDBTableView1_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (drStkSiteDet != null)
                if (!Chk_Lock_Ph_Bl_Co(Convert.ToDecimal(drStack["COMPANY_ID"]), Convert.ToDecimal(drStkSiteDet["SYS_CREATED_STACK_ID"]), "BL", "STACK_BLOCK"))
                {
                    if (cxGridBlockDBTableView1.FocusedRowHandle < 0)
                        XtraMessageBox.Show("Cannot add a new Block, as the Block Details has been locked by Profit Forecast Bulk Update.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                        XtraMessageBox.Show("Cannot Edit as the Block Document is locked.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                }
        }

        private void cxGridBlockDBTableView1_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            DataRow drStkBlock = cxGridBlockDBTableView1.GetDataRow(e.RowHandle);
            if (drStkBlock == null) return;

            if (drStkBlock["BLOCK_NAME"] == DBNull.Value || drStkBlock["BLOCK_NAME"].ToString().Trim() == string.Empty)
            {
                XtraMessageBox.Show("Block is mandatory.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Valid = false;
                return;
            }
            else
            {
                drStkBlock["UPPER_BLOCK_NAME"] = drStkBlock["BLOCK_NAME"];

                DataRow[] drRows = dtStackBlockDet.Select("UPPER_BLOCK_NAME = '" + drStkBlock["BLOCK_NAME"].ToString().ToUpper() + "'");
                foreach (DataRow drCode in drRows)
                    if (dtStackBlockDet.Rows.IndexOf(drCode) != dtStackBlockDet.Rows.IndexOf(drStkBlock) && drCode.RowState != DataRowState.Deleted)
                        if (Convert.ToString(drCode["UPPER_BLOCK_NAME"]) == Convert.ToString(drStkBlock["UPPER_BLOCK_NAME"]))
                        {
                            XtraMessageBox.Show("Duplicate Block is not allowed.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            e.Valid = false;
                            return;
                        }
            }

            if (drStkSiteDet != null)
            {
                sQuery = "SELECT BLOCK_ID FROM STACK_BLOCK_DETAILS ";
                sQuery += "WHERE UPPER(BLOCK_NAME) = @IV_BLOCK_NAME AND STACK_ID = @IN_STACK_ID";
                oParameter = new List<KeyValuePair<string, object>>();
                oParameter.Add(new KeyValuePair<string, object>("@IN_STACK_ID", drStkSiteDet["SYS_CREATED_STACK_ID"]));
                oParameter.Add(new KeyValuePair<string, object>("@IV_BLOCK_NAME", drStkBlock["BLOCK_NAME"].ToString().ToUpper()));

                object oResult = ProfitCashflow.oPcfDM.SqlQryAllpurpose(sQuery, oParameter, true);
                if (oResult != null && Convert.ToDecimal(oResult) > 0)
                    drStkBlock["BLOCK_ID"] = Convert.ToDecimal(oResult);
            }
        }

        private void cxGridBlockDBTableView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (cxGridBlockDBTableView1.OptionsBehavior.Editable)
                if (e.Control && e.KeyCode == Keys.Delete && cxGridBlockDBTableView1.FocusedRowHandle > 0)
                {
                    DialogResult oTempResult = XtraMessageBox.Show("Delete record?", Application.ProductName, MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (oTempResult == DialogResult.OK)
                    {
                        try
                        {
                            DataRow drStkBlock = cxGridBlockDBTableView1.GetDataRow(cxGridBlockDBTableView1.FocusedRowHandle);

                            sQuery = "SELECT COUNT(1) AS CNT FROM STACK_PLOT_DETAILS ";
                            sQuery += "WHERE STACK_ID = @IN_STACK_ID AND BLOCK_ID = @IN_BLOCK_ID";
                            oParameter = new List<KeyValuePair<string, object>>();
                            oParameter.Add(new KeyValuePair<string, object>("@IN_STACK_ID", drStkBlock["STACK_ID"]));
                            oParameter.Add(new KeyValuePair<string, object>("@IN_BLOCK_ID", drStkBlock["BLOCK_ID"]));

                            object oResult = ProfitCashflow.oPcfDM.SqlQryAllpurpose(sQuery, oParameter, true);
                            if (oResult != null && Convert.ToDecimal(oResult) > 0)
                            {
                                XtraMessageBox.Show("Cannot delete, as the Block is selected in Plot Setup.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            if (drStkBlock.RowState == DataRowState.Added)
                                drStkBlock.RejectChanges();
                            else
                                drStkBlock.Delete();
                            SetColours("B");
                        }
                        catch (Exception ex)
                        {
                            XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
        }

        private void cxGridCoreDBTableView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            int int_temp, int_temp1;

            if (drStkSiteDet != null)
            {
                DataRow drStkCore = cxGridCoreDBTableView1.GetDataRow(e.RowHandle);
                drStkCore["STACK_ID"] = drStack["STACK_ID"];

                int_temp = GenLineno(dtStackCoreDet, "CORE_ID");
                int_temp1 = GetMax_PH_BL_CO_ID(Convert.ToDecimal(drStkSiteDet["SYS_CREATED_STACK_ID"]), "CO");

                if (int_temp >= int_temp1)
                    drStkCore["CORE_ID"] = int_temp;
                else if (int_temp < int_temp1)
                    drStkCore["CORE_ID"] = int_temp1;
            }
        }

        private void cxGridCoreDBTableView1_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (drStkSiteDet != null)
                if (!Chk_Lock_Ph_Bl_Co(Convert.ToDecimal(drStack["COMPANY_ID"]), Convert.ToDecimal(drStkSiteDet["SYS_CREATED_STACK_ID"]), "CO", "STACK_CORE"))
                {
                    if (cxGridCoreDBTableView1.FocusedRowHandle < 0)
                        XtraMessageBox.Show("Cannot add a new Core, as the Core Details has been locked by Profit Forecast Bulk Update.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                        XtraMessageBox.Show("Cannot Edit as the Core Document is locked.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                }
        }

        private void cxGridCoreDBTableView1_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            DataRow drStkCore = cxGridCoreDBTableView1.GetDataRow(e.RowHandle);
            if (drStkCore == null) return;

            if (drStkCore["CORE_NAME"] == DBNull.Value || drStkCore["CORE_NAME"].ToString().Trim() == string.Empty)
            {
                XtraMessageBox.Show("Core is mandatory.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Valid = false;
                return;
            }
            else
            {
                drStkCore["UPPER_CORE_NAME"] = drStkCore["CORE_NAME"];

                DataRow[] drRows = dtStackCoreDet.Select("UPPER_CORE_NAME = '" + drStkCore["CORE_NAME"].ToString().ToUpper() + "'");
                foreach (DataRow drCode in drRows)
                    if (dtStackCoreDet.Rows.IndexOf(drCode) != dtStackCoreDet.Rows.IndexOf(drStkCore) && drCode.RowState != DataRowState.Deleted)
                        if (Convert.ToString(drCode["UPPER_CORE_NAME"]) == Convert.ToString(drStkCore["UPPER_CORE_NAME"]))
                        {
                            XtraMessageBox.Show("Duplicate Core is not allowed.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            e.Valid = false;
                            return;
                        }
            }
            
            if (drStkSiteDet != null)
            {
                sQuery = "SELECT CORE_ID FROM STACK_CORE_DETAILS ";
                sQuery += "WHERE UPPER(CORE_NAME) = @IV_CORE_NAME AND STACK_ID = @IN_STACK_ID";
                oParameter = new List<KeyValuePair<string, object>>();
                oParameter.Add(new KeyValuePair<string, object>("@IN_STACK_ID", drStkSiteDet["SYS_CREATED_STACK_ID"]));
                oParameter.Add(new KeyValuePair<string, object>("@IV_CORE_NAME", drStkCore["CORE_NAME"].ToString().ToUpper()));

                object oResult = ProfitCashflow.oPcfDM.SqlQryAllpurpose(sQuery, oParameter, true);
                if (oResult != null && Convert.ToDecimal(oResult) > 0)
                    drStkCore["CORE_ID"] = Convert.ToDecimal(oResult);
            }
        }

        private void cxGridCoreDBTableView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (cxGridCoreDBTableView1.OptionsBehavior.Editable)
                if (e.Control && e.KeyCode == Keys.Delete && cxGridCoreDBTableView1.FocusedRowHandle > 0)
                {
                    DialogResult oTempResult = XtraMessageBox.Show("Delete record?", Application.ProductName, MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (oTempResult == DialogResult.OK)
                    {
                        try
                        {
                            DataRow drStkCore = cxGridCoreDBTableView1.GetDataRow(cxGridCoreDBTableView1.FocusedRowHandle);

                            sQuery = "SELECT COUNT(1) AS CNT FROM STACK_PLOT_DETAILS ";
                            sQuery += "WHERE STACK_ID = @IN_STACK_ID AND CORE_ID = @IN_CORE_ID";
                            oParameter = new List<KeyValuePair<string, object>>();
                            oParameter.Add(new KeyValuePair<string, object>("@IN_STACK_ID", drStkCore["STACK_ID"]));
                            oParameter.Add(new KeyValuePair<string, object>("@IN_CORE_ID", drStkCore["CORE_ID"]));

                            object oResult = ProfitCashflow.oPcfDM.SqlQryAllpurpose(sQuery, oParameter, true);
                            if (oResult != null && Convert.ToDecimal(oResult) > 0)
                            {
                                XtraMessageBox.Show("Cannot delete, as the Core is selected in Plot Setup.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            if (drStkCore.RowState == DataRowState.Added)
                                drStkCore.RejectChanges();
                            else
                                drStkCore.Delete();
                            SetColours("C");
                        }
                        catch (Exception ex)
                        {
                            XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
        }

        private void CxGridHistorytv_DoubleClick(object sender, EventArgs e)
        {
            if (CxGridHistorytv.FocusedRowHandle >= 0)
            {
                int iPrevRowHandle = CxGridHistorytv.FocusedRowHandle;
                DataRow drHistory = CxGridHistorytv.GetDataRow(CxGridHistorytv.FocusedRowHandle);
                if (drHistory["STACK_STATUS"].ToString() != "U")
                {
                    DataRow drStkTemp = dtStackView.Rows.Find(drHistory["STACK_ID"]);
                    if (drStkTemp != null)
                        cxGridStackViewtvFocusedRecordChanged(cxGridStackViewTv.GetRowHandle(dtStackView.Rows.IndexOf(drStkTemp)));

                    CxGridHistorytv.FocusedRowHandle = iPrevRowHandle;
                }
                else
                    XtraMessageBox.Show("To access the PF Update stack, go to the 'Profit Forecast Update Stack' form.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);

                Clear_Plot_fld();
            }
        }

        private void CxGridHistorytv_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            Color lColor = CxGridHistorytv.Appearance.Row.ForeColor;
            DataRow drHistory = CxGridHistorytv.GetDataRow(e.RowHandle);
            if (drHistory != null)
            {
                if (drHistory["NEXT_STACKSTATUS"] != DBNull.Value && Convert.ToString(drHistory["NEXT_STACKSTATUS"]) == "Y")
                {
                    e.Appearance.ForeColor = Color.Green;
                    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                }
                if (drHistory["IS_CURRENT"] != DBNull.Value && Convert.ToString(drHistory["IS_CURRENT"]) == "Y")
                {
                    e.Appearance.ForeColor = Color.FromArgb(255, 130, 255);     //$00FF82FF
                    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                }
            }
        }

        private void cxGrid2DBTableView2_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            GridGroupSummaryItem item = e.Item as GridGroupSummaryItem;
            if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Start)
            {
                dGrupSaleRev = 0; dGrupMargin = 0;
                dTotSaleRev = 0; dTotMargin = 0;
            }
            if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Calculate)
            {
                DataRow drSummRow = cxGrid2DBTableView2.GetDataRow(e.RowHandle);
                if (e.IsGroupSummary)
                {
                    dGrupSaleRev += Convert.ToDecimal(drSummRow["SALES_REVENUE"]);
                    dGrupMargin += Convert.ToDecimal(drSummRow["DISP_GROSS_MARGIN"]);
                }
                if (e.IsTotalSummary)
                {
                    dTotSaleRev += Convert.ToDecimal(drSummRow["SALES_REVENUE"]);
                    dTotMargin += Convert.ToDecimal(drSummRow["DISP_GROSS_MARGIN"]);
                }
            }
            if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                if (item != null && Equals("MarginPCT", item.Tag))
                {
                    if (e.IsGroupSummary)
                    {
                        e.TotalValue = dGrupSaleRev != 0 ? (dGrupMargin / dGrupSaleRev) * 100 : 0;
                        dGrupSaleRev = 0;
                        dGrupMargin = 0;
                    }
                    if (e.IsTotalSummary)
                    {
                        e.TotalValue = dTotSaleRev != 0 ? (dTotMargin / dTotSaleRev) * 100 : 0;
                        dTotSaleRev = 0;
                        dTotMargin = 0;
                    }
                }
        }

        private void cxGridStackDocsTv_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            DataRow drStkDoc = cxGridStackDocsTv.GetDataRow(e.RowHandle);
            drStkDoc["STACK_ID"] = drStack["STACK_ID"];
            drStkDoc["CREATED_BY"] = ProfitCashflow.oPcfDM.UserName;
            drStkDoc["CREATED_ON"] = DateTime.Now;
            drStkDoc["DOCUMENT_ID"] = GenLineno(dtStackDocs, "DOCUMENT_ID");
        }

        private void cxGridStackDocsTv_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            DataRow drStkDoc = cxGridStackDocsTv.GetDataRow(e.RowHandle);
            if (drStkDoc == null) return;

            if (drStkDoc["DOCUMENT_NAME"] == DBNull.Value || drStkDoc["DOCUMENT_NAME"].ToString().Trim() == string.Empty)
            {
                XtraMessageBox.Show("Document Name is mandatory.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Valid = false;
                return;
            }
            if (drStkDoc["DESCRIPTION"] == DBNull.Value || drStkDoc["DESCRIPTION"].ToString().Trim() == string.Empty)
            {
                XtraMessageBox.Show("Description is mandatory.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Valid = false;
                return;
            }

            drStkDoc["CREATED_BY"] = ProfitCashflow.oPcfDM.UserName;
            drStkDoc["LAST_UPDATED"] = DateTime.Now;
        }

        private void cxGridStackDocsTv_KeyDown(object sender, KeyEventArgs e)
        {
            if (lFormMode != 'B' && cxGridStackDocsTv.OptionsBehavior.Editable)
                if (e.Control && e.KeyCode == Keys.Delete && cxGridStackDocsTv.FocusedRowHandle > 0)
                {
                    DialogResult oTempResult = XtraMessageBox.Show("Delete record?", Application.ProductName, MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (oTempResult == DialogResult.OK)
                    {
                        DataRow drStkDoc = cxGridStackDocsTv.GetDataRow(cxGridStackDocsTv.FocusedRowHandle);
                        if (drStkDoc.RowState == DataRowState.Added)
                            drStkDoc.RejectChanges();
                        else
                            drStkDoc.Delete();
                    }
                }
        }

        private void cxGridStackDocsTv_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            MakeStackDocColumnEnable();
        }

        private void cxGridStackDocsTv_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            MakeStackDocColumnEnable();
        }

        private void MakeStackDocColumnEnable()
        {
            repBtnEditDoc.ReadOnly = false;
            repHyperLinkDoc.ReadOnly = false;
            if (lFormMode == 'B')
                if (cxGridStackDocsTv.FocusedColumn == cxGridStackDocsTvDOCUMENT_NAME || cxGridStackDocsTv.FocusedColumn == cxGridStackDocsTvDOC_HYPER_LINK)
                {
                    cxGridStackDocsTv.OptionsBehavior.Editable = true;
                    repBtnEditDoc.ReadOnly = cxGridStackDocsTv.FocusedColumn == cxGridStackDocsTvDOCUMENT_NAME;
                    repHyperLinkDoc.ReadOnly = cxGridStackDocsTv.FocusedColumn == cxGridStackDocsTvDOC_HYPER_LINK;
                }
                else
                    cxGridStackDocsTv.OptionsBehavior.Editable = false;
        }

        private void repBtnEditDoc_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            DataRow drStkDoc = cxGridStackDocsTv.GetDataRow(cxGridStackDocsTv.FocusedRowHandle);
            if (lFormMode != 'B' && drStkDoc == null && cxGridStackDocsTv.IsNewItemRow(cxGridStackDocsTv.FocusedRowHandle))
            {
                cxGridStackDocsTv.AddNewRow();
                drStkDoc = cxGridStackDocsTv.GetDataRow(cxGridStackDocsTv.FocusedRowHandle);
            }

            if (e.Button.Index == 0)
            {
                if (lFormMode != 'B' && dxBarLargeButtonSave.Enabled)
                {
                    OpenDialog1.Title = "Select a Document";
                    if (OpenDialog1.ShowDialog() == DialogResult.OK)
                        drStkDoc["DOCUMENT_NAME"] = OpenDialog1.FileName;
                }
            }
            else if (e.Button.Index == 1)
            {
                if (drStkDoc != null && drStkDoc["DOCUMENT_NAME"] != DBNull.Value && !string.IsNullOrWhiteSpace(drStkDoc["DOCUMENT_NAME"].ToString()))
                {
                    System.Diagnostics.Process oFileProcess = new System.Diagnostics.Process();
                    oFileProcess.StartInfo = new System.Diagnostics.ProcessStartInfo("IExplore.exe", drStkDoc["DOCUMENT_NAME"].ToString());
                    oFileProcess.StartInfo.UseShellExecute = true;
                    //oFileProcess.StartInfo.FileName = drStkDoc["DOCUMENT_NAME"].ToString();
                    oFileProcess.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Maximized;
                    oFileProcess.Start();
                }
            }
        }

        private void cxFromTo_Leave(object sender, EventArgs e)
        {
            cxSelectall.CheckedChanged -= cxSelectall_CheckedChanged;
            cxSelectall.Checked = false;
            cxSelectall.CheckedChanged += cxSelectall_CheckedChanged;

            cxGridPlotSetupTv.BeginUpdate();
            cxGridPlotSetupTv.ClearSelection();
            SelectPlot();
            cxGridPlotSetupTv.EndUpdate();
        }

        private void cxSelectall_CheckedChanged(object sender, EventArgs e)
        {
            cxFrom.EditValue = null;
            cxTo.EditValue = null;

            cxGridPlotSetupTv.BeginUpdate();
            if (cxSelectall.Checked)
                cxGridPlotSetupTv.SelectAll();
            else
                cxGridPlotSetupTv.ClearSelection();
            cxGridPlotSetupTv.EndUpdate();
        }

        private void SelectPlot()
        {
            if (cxFrom.EditValue != null && Convert.ToInt16(cxFrom.EditValue) > -1 &&
               (cxTo.EditValue == null || Convert.ToInt16(cxTo.EditValue) <= 0))
                FillPlot(Convert.ToInt16(cxFrom.EditValue), 0);
            else if (cxTo.EditValue != null && Convert.ToInt16(cxTo.EditValue) > -1 &&
               (cxFrom.EditValue == null || Convert.ToInt16(cxFrom.EditValue) <= 0))
                FillPlot(0, Convert.ToInt16(cxTo.EditValue));
            else if (cxFrom.EditValue != null && Convert.ToInt16(cxFrom.EditValue) > 0 &&
               cxTo.EditValue != null && Convert.ToInt16(cxTo.EditValue) > 0)
                FillPlot(Convert.ToInt16(cxFrom.EditValue), Convert.ToInt16(cxTo.EditValue));
            else
                FillPlot(0, 0);
        }

        private string getPlotcode(int int_plot_no)
        {
            if (int_plot_no >= 0 && int_plot_no <= 9)
                return "P000" + int_plot_no.ToString();
            else if (int_plot_no >= 10 && int_plot_no <= 99)
                return "P00" + int_plot_no.ToString();
            else if (int_plot_no >= 100 && int_plot_no <= 999)
                return "P0" + int_plot_no.ToString();
            else if (int_plot_no >= 1000 && int_plot_no <= 9999)
                return "P" + int_plot_no.ToString();
            else
                return "";
        }

        private void FillPlot(int int_from, int int_to)
        {
            if (int_from == 0 && int_to == 0) return;
            if (dtStackPlotDet == null || dtStackPlotDet.Rows.Count == 0) return;

            int int_varf, int_vart;

            DataRow drPlotForm = dtStackPlotDet.Rows.Find(getPlotcode(int_from));
            if (drPlotForm != null)
                int_varf = cxGridPlotSetupTv.GetRowHandle(dtStackPlotDet.Rows.IndexOf(drPlotForm));
            else
                int_varf = 0;

            DataRow drPlotTo = dtStackPlotDet.Rows.Find(getPlotcode(int_to));
            if (drPlotTo != null)
                int_vart = cxGridPlotSetupTv.GetRowHandle(dtStackPlotDet.Rows.IndexOf(drPlotTo));
            else
                int_vart = 0;

            cxGridPlotSetupTv.SelectRows(int_varf, int_vart);
        }

        private void cxLookupComboBoxPlotTypeForPlot_EditValueChanged(object sender, EventArgs e)
        {
            cxLookupComboBoxPlotResType.EditValue = null;
        }

        private void cxLookupComboBoxPlotResType_Enter(object sender, EventArgs e)
        {
            if (cxLookupComboBoxPlotTypeForPlot.EditValue != null && !string.IsNullOrWhiteSpace(cxLookupComboBoxPlotTypeForPlot.EditValue.ToString()))
            {
                DataView dvResType = new DataView(dtResTypeMas, "PLOT_TYPE_ID=" + Convert.ToDecimal(cxLookupComboBoxPlotTypeForPlot.EditValue), "", DataViewRowState.CurrentRows);
                cxLookupComboBoxPlotResType.Properties.DataSource = dvResType;
            }
            else
                cxLookupComboBoxPlotResType.Properties.DataSource = dtResTypeMas;
        }

        private void repLookUpUnitCat_QueryPopUp(object sender, CancelEventArgs e)
        {
            if (lFormMode != 'B' && cxGridPlotSetupTv.FocusedRowHandle >= 0)
            {
                decimal dPlotTypeId = -1;
                DataRow drPlot = cxGridPlotSetupTv.GetDataRow(cxGridPlotSetupTv.FocusedRowHandle);

                if (drPlot != null && drPlot["PLOT_TYPE_ID"] != DBNull.Value && !string.IsNullOrWhiteSpace(drPlot["PLOT_TYPE_ID"].ToString()))
                    dPlotTypeId = Convert.ToDecimal(drPlot["PLOT_TYPE_ID"]);

                ((LookUpEdit)sender).Properties.DataSource = new DataView(dtResTypeMas, "PLOT_TYPE_ID=" + dPlotTypeId, "", DataViewRowState.CurrentRows);
                ((LookUpEdit)sender).Properties.DisplayMember = "DESCRIPTION";
                ((LookUpEdit)sender).Properties.ValueMember = "RESIDENCE_TYPE_ID";
            }
        }

        private void LookUp_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
                ((LookUpEdit)sender).EditValue = null;
        }

        private void LookUp_MouseWheel(object sender, MouseEventArgs e)
        {
            if (!((LookUpEdit)sender).IsPopupOpen)
                DevExpress.Utils.DXMouseEventArgs.GetMouseArgs(e).Handled = true;
        }

        private void control_Spin(object sender, SpinEventArgs e)
        {
            e.Handled = true;
        }

        private void repLookUpPhase_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1 && cxGridPlotSetupTv.FocusedRowHandle >= 0)
                cxGridPlotSetupTv.SetFocusedRowCellValue(cxGridPlotSetupTvPHASE, DBNull.Value);
        }

        private void repLookUpBlock_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1 && cxGridPlotSetupTv.FocusedRowHandle >= 0)
                cxGridPlotSetupTv.SetFocusedRowCellValue(cxGridPlotSetupTvBLOCK_ID, DBNull.Value);
        }

        private void repLookUpCore_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1 && cxGridPlotSetupTv.FocusedRowHandle >= 0)
                cxGridPlotSetupTv.SetFocusedRowCellValue(cxGridPlotSetupTvCORE_ID, DBNull.Value);
        }

        private void repLookUpPlotType_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1 && cxGridPlotSetupTv.FocusedRowHandle >= 0)
                cxGridPlotSetupTv.SetFocusedRowCellValue(cxGridPlotSetupTvPLOT_TYPE, DBNull.Value);
        }

        private void repDate_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1 && cxGridPlotSetupTv.FocusedRowHandle >= 0)
                cxGridPlotSetupTv.SetFocusedRowCellValue(cxGridPlotSetupTvFORECAST_TTS_DATE, DBNull.Value);
        }

        private void dxBarButtonCopyForecastDate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (cxGridPlotSetupTv.FocusedRowHandle >= 0)
            {
                object oDate = cxGridPlotSetupTv.GetRowCellValue(cxGridPlotSetupTv.FocusedRowHandle, cxGridPlotSetupTvFORECAST_TTS_DATE);
                if (oDate != null && !string.IsNullOrWhiteSpace(oDate.ToString()))
                    Forecast_TTS_Date = Convert.ToDateTime(oDate).Date;
            }
        }

        private void dxBarButtonPasteForecastDate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (cxGridPlotSetup.Enabled && cxGridPlotSetupTv.SelectedRowsCount > 0 && Forecast_TTS_Date != null && !string.IsNullOrWhiteSpace(Forecast_TTS_Date.ToString()))
            {
                cxGridPlotSetupTv.BeginUpdate();

                int selCount = cxGridPlotSetupTv.SelectedRowsCount;
                string[] selPlot = new string[selCount];
                for (int i = 0; i < selCount; i++)
                    selPlot[i] = Convert.ToString(cxGridPlotSetupTv.GetRowCellValue(i, cxGridPlotSetupTvPLOT_CODE));

                if (Forecast_TTS_Date < DateTime.Now.Date)
                    XtraMessageBox.Show("Forecast TTS Date cannot be less than the current date.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                {
                    DataRow drPlot;
                    for (int k = 0; k < selCount; k++)
                    {
                        drPlot = dtStackPlotDet.Rows.Find(selPlot[k]);
                        if (drPlot != null && drPlot["TAKEN_TO_SALE"] != null && Convert.ToString(drPlot["TAKEN_TO_SALE"]) != "Y" &&
                           drPlot["PLOT_FINALISED"] != null && Convert.ToString(drPlot["PLOT_FINALISED"]) == "N")
                            drPlot["FORECAST_TTS_DATE"] = Forecast_TTS_Date;
                    }
                }

                cxGridPlotSetupTv.EndUpdate();
            }
        }

        private void cxBtnApportion_Click(object sender, EventArgs e)
        {
            StackFunction oStkFunction = new StackFunction();
            oStkFunction.sFormType = "AP";
            oStkFunction.ShowDialog();
        }

        private void dxBarButtonProcesstoNextSS_Click(object sender, EventArgs e)
        {
            if (drStack == null) return;

            if (drStack["IS_CURRENT"].ToString() == "N")
            {
                XtraMessageBox.Show("Only an Active Stack Version can be progressed to the next stack status.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (drStack["STACK_STATUS"].ToString() == "R")
            {
                XtraMessageBox.Show("A release stack cannot be progressed further.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if ((drStack["STACK_STATUS"].ToString() == "I") && (cxTextEdit1.EditValue.ToString() != "B1" && cxTextEdit1.EditValue.ToString() != "ATL"))
            {
                XtraMessageBox.Show("Cannot progress, as stack status can be set to ''Release'' only for Land Status ''B1'' or ''ATL''.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            StackStatus frmStackStatus = new StackStatus();
            frmStackStatus.rgStackStatus.EditValue = cxDBImageComboBox1.EditValue;
            frmStackStatus.pLandStatus = cxTextEdit1.EditValue.ToString();
            frmStackStatus.pStackStatus = cxDBImageComboBox1.EditValue.ToString();
            DialogResult oDialogResult = frmStackStatus.ShowDialog();


            if (oDialogResult == DialogResult.OK)
            {
                try
                {
                    oStkDM.UpdateStackStatus(Convert.ToDecimal(drStack["COMPANY_ID"]), Convert.ToString(drStack["SITE_CODE"]), frmStackStatus.rgStackStatus.EditValue.ToString(),
                                                cxDBImageComboBox1.EditValue.ToString(),
                                                Convert.ToDecimal(drStack["STACK_ID"]),
                                                "V", ProfitCashflow.oPcfDM.UserName,"N", 0, "N"
                                                );
                   
                    RefStackView(oStkDM.dNewStackId);
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            frmStackStatus.Close();
            frmStackStatus.Dispose();
            frmStackStatus = null;

        }

        private void dxBarButtonProcesstoPrevSS_Click(object sender, EventArgs e)
        {
            if (drStack == null) return;

            if (drStack["IS_CURRENT"].ToString() == "N")
            {
                XtraMessageBox.Show("Only an Active Stack Version can be regressed to the previous stack status.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            oStkDM.ChkFinalisedPlotCount(Convert.ToDecimal(drStack["COMPANY_ID"]),
                                         Convert.ToString(drStack["SITE_CODE"]),
                                         Convert.ToDecimal(drStack["STACK_ID"]), "PD");
            if (oStkDM.dFinalisedPlotCount > 0)
            {
                XtraMessageBox.Show("Cannot regress to previous stack status, as plots have been finalised.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            StackStatus frmStackStatus = new StackStatus();
            frmStackStatus.rgStackStatus.EditValue = cxDBImageComboBox1.EditValue;
            frmStackStatus.pLandStatus = cxTextEdit1.EditValue.ToString();
            frmStackStatus.pStackStatus = cxDBImageComboBox1.EditValue.ToString();
            DialogResult oDialogResult = frmStackStatus.ShowDialog();


            if (oDialogResult == DialogResult.OK)
            {
                try
                {
                    oStkDM.UpdateStackStatus(Convert.ToDecimal(drStack["COMPANY_ID"]), Convert.ToString(drStack["SITE_CODE"]), frmStackStatus.rgStackStatus.EditValue.ToString(),
                                                cxDBImageComboBox1.EditValue.ToString(),
                                                Convert.ToDecimal(drStack["STACK_ID"]),
                                                "V", ProfitCashflow.oPcfDM.UserName, "Y", 0, "N"
                                                );

                    RefStackView(oStkDM.dNewStackId);
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            frmStackStatus.Close();
            frmStackStatus.Dispose();
            frmStackStatus = null;

        }

        private void dxBarButtonCreateNewVersion_Click(object sender, EventArgs e)
        {
            if (drStack == null) return;

            decimal dPrevStackId = Convert.ToDecimal(drStack["STACK_ID"]);

            oStkDM.ChkFinalisedPlotCount(Convert.ToDecimal(drStack["COMPANY_ID"]),
                                        Convert.ToString(drStack["SITE_CODE"]),
                                        Convert.ToDecimal(drStack["STACK_ID"]), "CS");
            if (oStkDM.dFinalisedPlotCount > 0)
            {
                XtraMessageBox.Show("Select an Active Stack Version to create a new version.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (Convert.ToString(drStack["STACK_STATUS"]) == "R")
            {
                oStkDM.ChkFinalisedPlotCount(Convert.ToDecimal(drStack["COMPANY_ID"]),
                                       Convert.ToString(drStack["SITE_CODE"]),
                                       Convert.ToDecimal(drStack["STACK_ID"]), "AP");

                if (oStkDM.dFinalisedPlotCount > 0)
                {
                    XtraMessageBox.Show("Cannot create new version , as plots have been finalised.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

            }

            try
            {
                oStkDM.Copy_Create_Stack_Version("V",
                                            Convert.ToDecimal(drStack["STACK_ID"]),
                                             ProfitCashflow.oPcfDM.UserName, 0, "N"
                                            );

                RefStackView(oStkDM.dNewStackId);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            DialogResult oTempResult = XtraMessageBox.Show("Should the new version be marked as the active version?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (oTempResult == DialogResult.Yes)
            {
                sQuery = "UPDATE STACKS SET IS_CURRENT = 'N' WHERE STACK_ID = @IN_STACK_ID";
                oParameter = new List<KeyValuePair<string, object>>();
                oParameter.Add(new KeyValuePair<string, object>("@IN_STACK_ID", dPrevStackId));
                ProfitCashflow.oPcfDM.SqlQryAllpurpose(sQuery, oParameter);

                sQuery = "UPDATE STACKS SET IS_CURRENT = 'Y' WHERE STACK_ID = @IN_STACK_ID";
                oParameter = new List<KeyValuePair<string, object>>();
                oParameter.Add(new KeyValuePair<string, object>("@IN_STACK_ID", oStkDM.dNewStackId));
                ProfitCashflow.oPcfDM.SqlQryAllpurpose(sQuery, oParameter);

                RefStackView(oStkDM.dNewStackId);
            }

        }

        private void dxBarButtonMarkAsCurrent_Click(object sender, EventArgs e)
        {
            if (drStack == null) return;
            decimal pcf_doc_guid = 0, stack_id = 0;
           
            if (Convert.ToString(drStack["SITE_CODE"]) == "Y") return;

            if (drStack["PCF_DOC_GUID"] != DBNull.Value && !string.IsNullOrWhiteSpace(drStack["PCF_DOC_GUID"].ToString()))
            {
                pcf_doc_guid = Convert.ToDecimal(drStack["PCF_DOC_GUID"]);
                stack_id = Convert.ToDecimal(drStack["STACK_ID"]);  
            }
            else if (cxGridStackViewTv.FocusedRowHandle >= 0)
            {
                DataRow drSelStk = cxGridStackViewTv.GetDataRow(cxGridStackViewTv.FocusedRowHandle);
                pcf_doc_guid = Convert.ToDecimal(drSelStk["PCF_DOC_GUID"]);
                stack_id = Convert.ToDecimal(drSelStk["STACK_ID"]);
            }

            if (!Can_Change_Del_Doc(pcf_doc_guid, stack_id))
                return;

            oStkDM.ChkFinalisedPlotCount(Convert.ToDecimal(drStack["COMPANY_ID"]),
                                        Convert.ToString(drStack["SITE_CODE"]),
                                        Convert.ToDecimal(drStack["STACK_ID"]), "CS");
            if (oStkDM.dFinalisedPlotCount > 0)
            {
                XtraMessageBox.Show("Only a version of the present Stack Status can be marked as Active.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (Convert.ToString(drStack["STACK_STATUS"]) == "R")
            {
                oStkDM.ChkFinalisedPlotCount(Convert.ToDecimal(drStack["COMPANY_ID"]),
                                           Convert.ToString(drStack["SITE_CODE"]),
                                           Convert.ToDecimal(drStack["STACK_ID"]), "RL");

                if (oStkDM.dFinalisedPlotCount > 0)
                {
                    XtraMessageBox.Show("Cannot mark the selected stack as active, as the number of finalised plots in the Active Release Stack and Slected Release Stack are not the same.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

           


            sQuery = "UPDATE STACKS SET IS_CURRENT = 'N' ";
            sQuery += "WHERE COMPANY_ID = @IN_COMPANY_ID AND SITE_CODE = @IV_SITE_CODE AND STACK_ID <> @IN_STACK_ID";
            oParameter = new List<KeyValuePair<string, object>>();
            
            oParameter.Add(new KeyValuePair<string, object>("@IN_COMPANY_ID", Convert.ToDecimal(drStack["COMPANY_ID"])));
            oParameter.Add(new KeyValuePair<string, object>("@IV_SITE_CODE", Convert.ToString(drStack["SITE_CODE"])));
            oParameter.Add(new KeyValuePair<string, object>("@IN_STACK_ID", Convert.ToDecimal(drStack["STACK_ID"])));
            ProfitCashflow.oPcfDM.SqlQryAllpurpose(sQuery, oParameter);

            sQuery = "UPDATE STACKS SET IS_CURRENT = 'Y' WHERE STACK_ID = @IN_STACK_ID";
            oParameter = new List<KeyValuePair<string, object>>();
            oParameter.Add(new KeyValuePair<string, object>("@IN_STACK_ID", Convert.ToDecimal(drStack["STACK_ID"])));
            ProfitCashflow.oPcfDM.SqlQryAllpurpose(sQuery, oParameter);

            sQuery = "UPDATE SITE_SETUP SET CURRENT_STACK_ID = @IN_STACK_ID, STACK_STATUS = @IC_STACK_STATUS ";
            sQuery += "WHERE COMPANY_ID = @IN_COMPANY_ID AND SITE_CODE = @IV_SITE_CODE";
            oParameter = new List<KeyValuePair<string, object>>();
            oParameter.Add(new KeyValuePair<string, object>("@IN_STACK_ID", Convert.ToDecimal(drStack["STACK_ID"])));
            oParameter.Add(new KeyValuePair<string, object>("@IN_COMPANY_ID", Convert.ToDecimal(drStack["COMPANY_ID"])));
            oParameter.Add(new KeyValuePair<string, object>("@IV_SITE_CODE", Convert.ToString(drStack["SITE_CODE"])));
            oParameter.Add(new KeyValuePair<string, object>("@IC_STACK_STATUS", Convert.ToString(drStack["STACK_STATUS"])));
            ProfitCashflow.oPcfDM.SqlQryAllpurpose(sQuery, oParameter);

            RefStackView(oStkDM.dNewStackId);
        }
    }
}