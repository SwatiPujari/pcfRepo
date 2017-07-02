using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Xpedeon.ProfitCashflow
{
    public partial class fPcfMain : DevExpress.XtraEditors.XtraForm
    {
        string SettingsDir, GridLayoutDir;

        public fPcfMain()
        {
            InitializeComponent();
        }

        private void fPcfMain_Load(object sender, EventArgs e)
        {
            barCapsLk.Enabled = Control.IsKeyLocked(Keys.CapsLock);
            barNumLk.Enabled = Control.IsKeyLocked(Keys.NumLock);
            barScrLk.Enabled = Control.IsKeyLocked(Keys.Scroll);

            string szAppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).ToString();
            //SHGetFolderPath(0, CSIDL_APPDATA, 0, 0, szAppData);   //Replaced with Environment.GetFolderPath
            SettingsDir = szAppData + "\\Algorithms";
            Directory.CreateDirectory(SettingsDir);
            SettingsDir = SettingsDir + "\\PCF";
            Directory.CreateDirectory(SettingsDir);
            SettingsDir = SettingsDir + "\\2.0.0.0";  //Replace 1.0.0.1
            Directory.CreateDirectory(SettingsDir);

            GridLayoutDir = SettingsDir + "\\PCF_Layout.ini";
        }

        private void fPcfMain_Shown(object sender, EventArgs e)
        {
            try
            {
                ProfitCashflow.oPcfDM = new PCFDataModel();
                if (ProfitCashflow.oPcfDM.conn.State != ConnectionState.Open)
                    ProfitCashflow.oPcfDM.conn.Open();

                barBtnLogin.Enabled = false;
                barBtnLogin_ItemClick(null, null);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void fPcfMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult oResult = XtraMessageBox.Show("Do you want to exit the application?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (oResult == System.Windows.Forms.DialogResult.Yes)
            {
                if (this.MdiChildren.Length > 0)
                {
                    XtraMessageBox.Show("Please close all open documents before exiting.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                    return;
                }

                if (ProfitCashflow.oPcfDM.conn.State != ConnectionState.Closed)
                    ProfitCashflow.oPcfDM.conn.Close();
                ProfitCashflow.oPcfDM = null;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void fPcfMain_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.CapsLock:
                    barCapsLk.Enabled = Control.IsKeyLocked(Keys.CapsLock);
                    break;
                case Keys.NumLock:
                    barNumLk.Enabled = Control.IsKeyLocked(Keys.NumLock);
                    break;
                case Keys.Scroll:
                    barScrLk.Enabled = Control.IsKeyLocked(Keys.Scroll);
                    break;
            }
        }

        private void barBtnLogin_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ErpLogin oLogin = new ErpLogin();
            DialogResult oResult = oLogin.ShowDialog();
            bool bLogIn = (oResult == System.Windows.Forms.DialogResult.OK);
            oLogin.Dispose(); oLogin = null;

            if (!bLogIn) return;

            if (ProfitCashflow.oPcfDM.checkVersion("PROFIT_CASHFLOW_EXE"))
            {
                SelCompany oSelComp = new SelCompany();
                oSelComp.ShowDialog();
                oSelComp.Dispose(); oSelComp = null;
            }

            if (ProfitCashflow.oPcfDM.dtUserInfo != null && ProfitCashflow.oPcfDM.dtUserInfo.Rows.Count > 0)
            {
                DataRow drUserInfo = ProfitCashflow.oPcfDM.dtUserInfo.Rows[0];

                dxNavBar.Enabled = true;
                dxNavBarItemCompMas.Visible = Convert.ToString(drUserInfo["COMPANY_MASTER_VIEW"]) == "Y";
                dxNavBarItemPurType.Visible = Convert.ToString(drUserInfo["PCF_PURCHASE_TYPE"]) == "Y";
                dxNavBarItemIntRate.Visible = Convert.ToString(drUserInfo["PCF_INTEREST_RATE"]) == "Y";
                dxNavBarItemB4SC.Visible = Convert.ToString(drUserInfo["PCF_B4_SITE_CODES"]) == "Y";
                dxNavBarItemLocMas.Visible = Convert.ToString(drUserInfo["PCF_LOCATION_MASTER"]) == "Y";
                dxNavBarItemPlotType.Visible = Convert.ToString(drUserInfo["PCF_PLOT_TYPE_MASTER"]) == "Y";
                dxNavBarItemResType.Visible = Convert.ToString(drUserInfo["PCF_UNIT_CATEGORY_MASTER"]) == "Y";
                dxNavBarItemCfOthInc.Visible = Convert.ToString(drUserInfo["PCF_CASHFLOW_OTHER_INCOME"]) == "Y";
                dxNavBarItemCfOthExp.Visible = Convert.ToString(drUserInfo["PCF_CASHFLOW_OTHER_EXPENSE"]) == "Y";
                dxNavBarItemAccPeriod.Visible = Convert.ToString(drUserInfo["PCF_ACC_PERIOD"]) == "Y";
                dxNavBarItemViewMas.Visible = Convert.ToString(drUserInfo["PCF_PF_VIEW_MASTER"]) == "Y";
                dxNavBarItemAspectMas.Visible = Convert.ToString(drUserInfo["PCF_PF_ASPECT_MASTER"]) == "Y";
                dxNavBarItemGarParkMas.Visible = Convert.ToString(drUserInfo["PCF_PF_GARAGE_PARKING_MASTER"]) == "Y";
                dxNavBarItemBalTerrMas.Visible = Convert.ToString(drUserInfo["PCF_PF_BALCONY_TERRACE_MASTER"]) == "Y";
                dxNavBarItemLandTypeMas.Visible = Convert.ToString(drUserInfo["PCF_SS_LAND_TYPE"]) == "Y";
                dxNavBarItemBuildTypeMas.Visible = Convert.ToString(drUserInfo["PCF_SS_BUILD_TYPE"]) == "Y";

                navBarItemOpComp.Visible = Convert.ToString(drUserInfo["PCF_OPERATING_COMPANY_MASTER"]) == "Y";
                navBarItemGrup.Visible = Convert.ToString(drUserInfo["PCF_GROUP_MASTER"]) == "Y";
                navBarItemDiv.Visible = Convert.ToString(drUserInfo["PCF_DIVISION_MASTER"]) == "Y";
                navBarItemReg.Visible = Convert.ToString(drUserInfo["PCF_REGION_MASTER"]) == "Y";
                navBarItemDev.Visible = Convert.ToString(drUserInfo["PCF_DEVELOPMENT_MASTER"]) == "Y";

                /*dxNavBarItemSiteSetup.Visible = Convert.ToString(drUserInfo["PCF_SITE_SETUP"]) == "Y";
                dxNavBarItemStacks.Visible = Convert.ToString(drUserInfo["PCF_STACKS"]) == "Y";
                dxNavBarItemPF.Visible = Convert.ToString(drUserInfo["PCF_PROFIT_FORECAST"]) == "Y";
                dxNavBarItemPFU.Visible = Convert.ToString(drUserInfo["PCF_PROFIT_FORECAST"]) == "Y";
                dxNavBarItemAdj.Visible = Convert.ToString(drUserInfo["PCF_ADJUSTMENTS"]) == "Y";
                dxNavBarItemKD.Visible = Convert.ToString(drUserInfo["PCF_KEY_DATES"]) == "Y";
                dxNavBarItemTTS.Visible = Convert.ToString(drUserInfo["PCF_TAKE_TO_SALES"]) == "Y";
                dxNavBarItemCF.Visible = Convert.ToString(drUserInfo["PCF_CASHFLOW"]) == "Y";
                dxNavBarItemIncSitePlot.Visible = Convert.ToString(drUserInfo["PCF_INCL_EXCL_PLOTS"]) == "Y";
                dxNavBarItemZeroWIP.Visible = Convert.ToString(drUserInfo["PCF_ZERO_WIP"]) == "Y";
                dxNavBarItemDiffStackPF.Visible = Convert.ToString(drUserInfo["PCF_DIFF_STACKS_PF_REP"]) == "Y";
                dxNavBarItemPFSummRPT.Visible = Convert.ToString(drUserInfo["PCF_PF_SUMMARY"]) == "Y";
                dxNavBarItemMaintainBlackoutPeriod.Visible = Convert.ToString(drUserInfo["PCF_MAINTAIN_BLACKOUT_PERIOD"]) == "Y";
                dxNavBarItemMaintainBlackoutExceptions.Visible = Convert.ToString(drUserInfo["PCF_CORPORATE_USER"]) == "Y";*/
            }

            if (ProfitCashflow.oPcfDM.company_id != -1)
                this.Text = "Profit & Cashflow : " + ProfitCashflow.oPcfDM.company_name;
            else
                this.Text = "Profit & Cashflow";

            barBtnLogout.Enabled = (ProfitCashflow.oPcfDM.conn.State == ConnectionState.Open);
            barBtnLogin.Enabled = !barBtnLogout.Enabled;
            dxNavBar.Enabled = true;

            dockPanel1.Text = "User : " + ProfitCashflow.oPcfDM.UserName;
            barStatusTxt.Caption = ProfitCashflow.oPcfDM.UserName;

            /*ProfitCashflow.oPcfDM.qCodaConfig.Close;
            ProfitCashflow.oPcfDM.qCodaConfig.Open;*/
        }

        private void barBtnLogout_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.MdiChildren.Length > 0)
            {
                XtraMessageBox.Show("Please close all open documents before exiting.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            /*if (ProfitCashflow.oPcfDM.conn.State != ConnectionState.Closed)
                ProfitCashflow.oPcfDM.conn.Close();*/   //Commented to keep session active by EXE & not by login

            this.Text = "Profit & Cashflow";
            dockPanel1.Text = "User";
            barStatusTxt.Caption = ProfitCashflow.oPcfDM.UserName;

            dxNavBar.Enabled = false;
            barBtnLogout.Enabled = false;
            barBtnLogin.Enabled = true;
        }

        private void dxNavBarItemLandTypeMas_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            fLandTypeMaster frm = new fLandTypeMaster();
            frm.ShowDialog();
            frm.Dispose(); frm = null;
        }

        private void dxNavBarItemPurType_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            fPurchaseTypeMaster frm = new fPurchaseTypeMaster();
            frm.ShowDialog();
            frm.Dispose(); frm = null;
        }

        private void dxNavBarItemAspectMas_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            fAspectMaster frm = new fAspectMaster();
            frm.ShowDialog();
            frm.Dispose(); frm = null;
        }

        private void dxNavBarItemAccPeriod_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            for (int i = 0; i < this.MdiChildren.Length; i++)
                if (this.MdiChildren[i].Name == "fAccPeriod")
                {
                    this.MdiChildren[i].BringToFront();
                    return;
                }

            fAccPeriod frm = new fAccPeriod();
            frm.MdiParent = this;
            frm.Show();
        }

        private void dxNavBarItemLocMas_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            fLocationMaster frm = new fLocationMaster();
            frm.ShowDialog();
            frm.Dispose(); frm = null;
        }

        private void dxNavBarItemBuildTypeMas_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            fBuildTypeMaster frm = new fBuildTypeMaster();
            frm.ShowDialog();
            frm.Dispose(); frm = null;
        }

        private void dxNavBarItemGarParkMas_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            fGarageParkMaster frm = new fGarageParkMaster();
            frm.ShowDialog();
            frm.Dispose(); frm = null;
        }

        private void dxNavBarItemViewMas_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            fViewMaster frm = new fViewMaster();
            frm.ShowDialog();
            frm.Dispose(); frm = null;
        }

        private void dxNavBarItemBalTerrMas_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            fBalconyTerrMaster frm = new fBalconyTerrMaster();
            frm.ShowDialog();
            frm.Dispose(); frm = null;
        }

        private void dxNavBarItemCfOthInc_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            fCFOtherIncome frm = new fCFOtherIncome();
            frm.ShowDialog();
            frm.Dispose(); frm = null;
        }

        private void dxNavBarItemCfOthExp_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            fCFOtherExpense frm = new fCFOtherExpense();
            frm.ShowDialog();
            frm.Dispose(); frm = null;
        }

        private void dxNavBarItemB4SC_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            fB4SiteCodeMaster frm = new fB4SiteCodeMaster();
            frm.ShowDialog();
            frm.Dispose(); frm = null;
        }

        private void dxNavBarItemPlotType_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            fPlotTypeMaster frm = new fPlotTypeMaster();
            frm.ShowDialog();
            frm.Dispose(); frm = null;
        }

        private void dxNavBarItemCompMas_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            for (int i = 0; i < this.MdiChildren.Length; i++)
                if (this.MdiChildren[i].Name == "CompanyMaster")
                {
                    this.MdiChildren[i].BringToFront();
                    return;
                }

            CompanyMaster frm = new CompanyMaster();
            frm.MdiParent = this;
            frm.Show();
        }

        private void navBarItemOpComp_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            for (int i = 0; i < this.MdiChildren.Length; i++)
                if (this.MdiChildren[i].Name == "OperatingCompanyMaster")
                {
                    this.MdiChildren[i].BringToFront();
                    return;
                }

            OperatingCompanyMaster frm = new OperatingCompanyMaster();
            frm.MdiParent = this;
            frm.Show();
        }

        private void navBarItemGrup_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            for (int i = 0; i < this.MdiChildren.Length; i++)
                if (this.MdiChildren[i].Name == "GroupMaster")
                {
                    this.MdiChildren[i].BringToFront();
                    return;
                }

            GroupMaster frm = new GroupMaster();
            frm.MdiParent = this;
            frm.Show();
        }

        private void navBarItemDiv_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            for (int i = 0; i < this.MdiChildren.Length; i++)
                if (this.MdiChildren[i].Name == "DivisionMaster")
                {
                    this.MdiChildren[i].BringToFront();
                    return;
                }

            DivisionMaster frm = new DivisionMaster();
            frm.MdiParent = this;
            frm.Show();
        }

        private void navBarItemReg_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            for (int i = 0; i < this.MdiChildren.Length; i++)
                if (this.MdiChildren[i].Name == "RegionMaster")
                {
                    this.MdiChildren[i].BringToFront();
                    return;
                }

            RegionMaster frm = new RegionMaster();
            frm.MdiParent = this;
            frm.Show();
        }

        private void navBarItemDev_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            for (int i = 0; i < this.MdiChildren.Length; i++)
                if (this.MdiChildren[i].Name == "DevelopmentMaster")
                {
                    this.MdiChildren[i].BringToFront();
                    return;
                }

            DevelopmentMaster frm = new DevelopmentMaster();
            frm.MdiParent = this;
            frm.Show();
        }

        private void dxNavBarItemResType_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            fUnitCategoryTypeMaster frm = new fUnitCategoryTypeMaster();
            frm.ShowDialog();
            frm.Dispose(); frm = null;
        }

        private void dxNavBarItemIntRate_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            fUnitInterestMaster frm = new fUnitInterestMaster();
            frm.ShowDialog();
            frm.Dispose(); frm = null;
        }

        private void barBtnTileHorizon_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void barBtnTileVertical_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileVertical);
        }

        private void barBtnCascade_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.LayoutMdi(MdiLayout.Cascade);
        }

        private void barBtnAboutPCF_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AboutProfitCashflow frm = new AboutProfitCashflow();
            frm.ShowDialog();
        }

        private void dxNavBarItemSiteSetup_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            for (int i = 0; i < this.MdiChildren.Length; i++)
                if (this.MdiChildren[i].Name == "SiteSetup")
                {
                    this.MdiChildren[i].BringToFront();
                    return;
                }

            SiteSetup frm = new SiteSetup();
            frm.MdiParent = this;
            frm.Show();
        }

        private void dxNavBarItemStacks_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            for (int i = 0; i < this.MdiChildren.Length; i++)
                if (this.MdiChildren[i].Name == "Stacks")
                {
                    this.MdiChildren[i].BringToFront();
                    return;
                }

            Stacks frm = new Stacks();
            frm.LSettingsDir = SettingsDir;
            frm.MdiParent = this;
            frm.Show();
        }
    }
}