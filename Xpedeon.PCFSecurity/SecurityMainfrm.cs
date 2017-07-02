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

namespace Xpedeon.PCFSecurity
{
    public partial class SecurityMainfrm : DevExpress.XtraEditors.XtraForm
    {
        public string WorkDir;
        
        public SecurityMainfrm()
        {
            InitializeComponent();
        }

        private void SecurityMainfrm_Load(object sender, EventArgs e)
        {
            // Add the event handler for handling UI thread exceptions to the event.
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(XpedeonExceptionHandler);

            barMnuBtnLogin.Enabled = true;
            barMnuBtnLogout.Enabled = false;
            barMnuBtnUserSetup.Enabled = false;
            barMnuBtnChngPassword.Enabled = false;
            barMnuBtnChngSQLPassword.Enabled = false;
            barMnuBtnPCFConfig.Enabled = false;
            barMnuBtnReleaseLockedDoc.Enabled = false;

            barTolBtnLogin.Enabled = true;
            barTolBtnLogout.Enabled = false;
            barTolBtnUserSetup.Enabled = false;
            barTolBtnChngPassword.Enabled = false;
            barTolBtnChngSQLPassword.Enabled = false;
            barTolBtnPCFConfig.Enabled = false;
            barTolBtnReleaseLockedDoc.Enabled = false;
        }

        private void SecurityMainfrm_Shown(object sender, EventArgs e)
        {
            PCFSecurity.oSecDM = new SecurityDataModule();
            WorkDir = PCFSecurity.oSecDM.PCF_WORK_DIR;
        }

        private void SecurityMainfrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (PCFSecurity.oSecDM.conn != null && PCFSecurity.oSecDM.conn.State != ConnectionState.Closed)
                PCFSecurity.oSecDM.conn.Close();

            PCFSecurity.oSecDM = null;
        }

        public void XpedeonExceptionHandler(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            MessageDlg(e.Exception.Message, "mtError", "mbOk", 0);
        }

        private void barBtnLogout_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (PCFSecurity.oSecDM.conn.State != ConnectionState.Closed)
                PCFSecurity.oSecDM.conn.Close();

            barMnuBtnLogin.Enabled = true;
            barMnuBtnLogout.Enabled = false;
            barMnuBtnUserSetup.Enabled = false;
            barMnuBtnChngPassword.Enabled = false;
            barMnuBtnChngSQLPassword.Enabled = false;
            barMnuBtnPCFConfig.Enabled = false;
            barMnuBtnReleaseLockedDoc.Enabled = false;

            barTolBtnLogin.Enabled = true;
            barTolBtnLogout.Enabled = false;
            barTolBtnUserSetup.Enabled = false;
            barTolBtnChngPassword.Enabled = false;
            barTolBtnChngSQLPassword.Enabled = false;
            barTolBtnPCFConfig.Enabled = false;
            barTolBtnReleaseLockedDoc.Enabled = false;
        }

        private void barBtnExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        public void MessageDlg(string msg, string DlgType, string Btns, int HelpIndx)
        {
            CustDlgBox oDlg = new CustDlgBox();
            oDlg.MessageDlg(msg, DlgType, Btns, HelpIndx);
            oDlg.Size = new System.Drawing.Size(oDlg.iWidth, oDlg.iHeight);
            oDlg.ShowDialog();
            oDlg.Dispose();
        }

        private void barBtnPCFConfig_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SiteConfig oSiteConfig = new SiteConfig();
            oSiteConfig.ShowDialog();
            oSiteConfig.Dispose();
        }

        private void barBtnAboutPCFSecurity_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AboutSecurity oAboutSec = new AboutSecurity();
            oAboutSec.ShowDialog();
            oAboutSec.Dispose();
        }

        private void barBtnUserSetup_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ErpUsers oUserSetup = new ErpUsers();
            oUserSetup.ShowDialog();
            oUserSetup.Dispose();
        }

        private void barBtnLogin_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (PCFSecurity.oSecDM.conn.State != ConnectionState.Open)
                PCFSecurity.oSecDM.conn.Open();

            SecurityLogin oSecLogin = new SecurityLogin();
            DialogResult oResult = oSecLogin.ShowDialog();
            if (oResult == System.Windows.Forms.DialogResult.OK)
            {
                barMnuBtnLogin.Enabled = false;
                barMnuBtnLogout.Enabled = true;
                barMnuBtnUserSetup.Enabled = true;
                barMnuBtnChngPassword.Enabled = true;
                barMnuBtnChngSQLPassword.Enabled = true;
                barMnuBtnPCFConfig.Enabled = true;
                barMnuBtnReleaseLockedDoc.Enabled = true;

                barTolBtnLogin.Enabled = false;
                barTolBtnLogout.Enabled = true;
                barTolBtnUserSetup.Enabled = true;
                barTolBtnChngPassword.Enabled = true;
                barTolBtnChngSQLPassword.Enabled = true;
                barTolBtnPCFConfig.Enabled = true;
                barTolBtnReleaseLockedDoc.Enabled = true;
            }
            else
            {
                if (PCFSecurity.oSecDM.conn.State != ConnectionState.Closed)
                    PCFSecurity.oSecDM.conn.Close();
            }
            oSecLogin.Dispose();

            PCFSecurity.oSecDM.checkVersion();
        }

        private void barBtnChngPassword_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ChngUserPassword oChngPwd = new ChngUserPassword();
            oChngPwd.ShowDialog();
            oChngPwd.Dispose();
        }

        private void barBtnChngSQLPassword_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ChngSQLServPassword oChngPwd = new ChngSQLServPassword();
            oChngPwd.ShowDialog();
            oChngPwd.Dispose();
        }

        private void barBtnReleaseLockedDoc_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ReleaseDoc oReleaseDoc = new ReleaseDoc();
            oReleaseDoc.ShowDialog();
            oReleaseDoc.Dispose();
        }
    }
}