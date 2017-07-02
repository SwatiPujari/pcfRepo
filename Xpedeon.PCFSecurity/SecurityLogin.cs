using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Xpedeon.PCFSecurity
{
    public partial class SecurityLogin : DevExpress.XtraEditors.XtraForm
    {
        DataTable dtUserLoginInfo;
        SecurityMainfrm oSecMainFrm = new SecurityMainfrm();

        public SecurityLogin()
        {
            InitializeComponent();
        }

        private void cxBttnOK_Click(object sender, EventArgs e)
        {
            if (cxEdiUserName.EditValue == null || Convert.ToString(cxEdiUserName.EditValue).Trim() == "")
            {
                oSecMainFrm.MessageDlg("User Name cannot be blank...", "mtWarning", "mbOk", 0);
                return;
            }
            if (cxEdPassword.EditValue == null || Convert.ToString(cxEdPassword.EditValue).Trim() == "")
            {
                oSecMainFrm.MessageDlg("Password cannot be blank...", "mtWarning", "mbOk", 0);
                return;
            }

            if (cxEdiUserName.EditValue != null && !string.IsNullOrWhiteSpace(cxEdiUserName.EditValue.ToString()))
            {
                if (Convert.ToString(cxEdiUserName.EditValue).Length > 27)
                {
                    oSecMainFrm.MessageDlg("User Name exceeds maximum permissible length of 27.", "mtError", "mbOk", 0);
                    return;
                }
                PCFSecurity.oSecDM.pUserName = cxEdiUserName.EditValue.ToString();

                string sEncryptPassword = "";
                try
                {
                    sEncryptPassword = XpedeonCrypto.XpedeonServerEncrypt(Convert.ToString(cxEdPassword.EditValue));

                    RetrieveUserLoginInfo(Convert.ToString(cxEdiUserName.EditValue));
                    if (dtUserLoginInfo != null && dtUserLoginInfo.Rows.Count > 0)
                    {
                        if (dtUserLoginInfo.Rows[0]["ENABLE_DISABLE"] != DBNull.Value && dtUserLoginInfo.Rows[0]["ENABLE_DISABLE"].ToString() == "N")
                        {
                            oSecMainFrm.MessageDlg("User needs to be enabled to log in.", "mtWarning", "mbOk", 0);
                            return;
                        }

                        if (dtUserLoginInfo.Rows[0]["CURRENT_DATE"] != DBNull.Value && dtUserLoginInfo.Rows[0]["PASSWORD_UPDATED_ON"] != DBNull.Value &&
                            (Convert.ToDateTime(dtUserLoginInfo.Rows[0]["CURRENT_DATE"]) - Convert.ToDateTime(dtUserLoginInfo.Rows[0]["PASSWORD_UPDATED_ON"])).TotalDays > 30)
                        {   
                            oSecMainFrm.MessageDlg("Password has been expired.", "mtWarning", "mbOk", 0);

                            ChngUserPassword oChngPwd = new ChngUserPassword();
                            oChngPwd.ShowDialog();
                            oChngPwd.Dispose();

                            this.Close();
                        }

                        if (dtUserLoginInfo.Rows[0]["USER_PASSWORD"] != DBNull.Value && dtUserLoginInfo.Rows[0]["USER_PASSWORD"].ToString() != sEncryptPassword)
                        {
                            oSecMainFrm.MessageDlg("Password entered is incorrect.", "mtError", "mbOk", 0);
                            return;
                        }

                        this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    }
                    else
                    {
                        oSecMainFrm.MessageDlg("User Name or Password do not match.", "mtError", "mbOk", 0);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        private void RetrieveUserLoginInfo(string sUserName)
        {
            SqlDataAdapter da = null;
            dtUserLoginInfo = new DataTable();

            try
            {
                da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("SELECT USERNAME, USER_PASSWORD, ENABLE_DISABLE, PASSWORD_UPDATED_ON, GETDATE() [CURRENT_DATE] FROM SITE_USERS WHERE USERNAME = @IV_USERNAME", PCFSecurity.oSecDM.conn);
                da.SelectCommand.CommandType = CommandType.Text;
                da.SelectCommand.Parameters.AddWithValue("@IV_USERNAME", sUserName);

                da.Fill(dtUserLoginInfo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (da != null) da.Dispose();
            }
        }
    }
}