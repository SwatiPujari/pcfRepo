using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Transactions;
using DevExpress.XtraEditors;

namespace Xpedeon.PCFSecurity
{
    public partial class ChngUserPassword : DevExpress.XtraEditors.XtraForm
    {
        DataTable dtUserLoginInfo;
        SecurityMainfrm oSecMainFrm = new SecurityMainfrm();

        public ChngUserPassword()
        {
            InitializeComponent();
        }

        private void ChngUserPassword_Load(object sender, EventArgs e)
        {
            cxTextEditUserName.EditValue = PCFSecurity.oSecDM.pUserName.ToUpper();
        }

        private void ChngUserPassword_Shown(object sender, EventArgs e)
        {
            cxTextEditOldPwd.Focus();
        }

        private void cxBtnOk_Click(object sender, EventArgs e)
        {
            if (cxTextEditOldPwd.EditValue == null || Convert.ToString(cxTextEditOldPwd.EditValue).Trim() == "")
            {
                oSecMainFrm.MessageDlg("Old Password cannot be blank...", "mtWarning", "mbOk", 0);
                return;
            }
            if (cxTextEditNewPwd.EditValue == null || Convert.ToString(cxTextEditNewPwd.EditValue).Trim() == "")
            {
                oSecMainFrm.MessageDlg("New Password cannot be blank...", "mtWarning", "mbOk", 0);
                return;
            }
            if (cxTextEditConfirmPwd.EditValue == null || Convert.ToString(cxTextEditConfirmPwd.EditValue).Trim() == "")
            {
                oSecMainFrm.MessageDlg("Confirm Password cannot be blank...", "mtWarning", "mbOk", 0);
                return;
            }

            RetrieveUserLoginInfo(cxTextEditUserName.EditValue.ToString());
            if (dtUserLoginInfo != null && dtUserLoginInfo.Rows.Count > 0)
            {
                string sUserName, sPassword, sOldPassword, sNewPassword;
                sUserName = dtUserLoginInfo.Rows[0]["USERNAME"].ToString();
                sPassword = dtUserLoginInfo.Rows[0]["USER_PASSWORD"].ToString();

                try
                {
                    sOldPassword = XpedeonCrypto.XpedeonServerEncrypt(Convert.ToString(cxTextEditOldPwd.EditValue));
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                if(sPassword != sOldPassword)
                {
                    oSecMainFrm.MessageDlg("Old Password does not match.", "mtWarning", "mbOk", 0);
                    return;
                }
                if (Convert.ToString(cxTextEditNewPwd.EditValue) != Convert.ToString(cxTextEditConfirmPwd.EditValue))
                {
                    oSecMainFrm.MessageDlg("New Password and Confirm Password dont match.", "mtWarning", "mbOk", 0);
                    return;
                }

                try
                {
                    sNewPassword = XpedeonCrypto.XpedeonServerEncrypt(Convert.ToString(cxTextEditNewPwd.EditValue));
                    UpdateUserPassword(Convert.ToString(cxTextEditUserName.EditValue), sNewPassword);
                    oSecMainFrm.MessageDlg("Password Changed Successfully.", "mtConfirmation", "mbOk", 0);
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
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
                da.SelectCommand = new SqlCommand("SELECT USERNAME, USER_PASSWORD, GETDATE() [CURRENT_DATE] FROM SITE_USERS WHERE USERNAME = @IV_USERNAME", PCFSecurity.oSecDM.conn);
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

        private void UpdateUserPassword(string sUserName, string sPassword)
        {
            TransactionOptions to = new TransactionOptions();
            to.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew, to, EnterpriseServicesInteropOption.Full))
            {
                SqlCommand cmd = null;
                try
                {
                    cmd = new SqlCommand("UPDATE SITE_USERS SET USER_PASSWORD=@IV_PASSWORD, PASSWORD_UPDATED_ON=GETDATE() WHERE USERNAME=@IV_USERNAME", PCFSecurity.oSecDM.conn);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@IV_USERNAME", sUserName);
                    cmd.Parameters.AddWithValue("@IV_PASSWORD", sPassword);

                    if (cmd.Connection.State != ConnectionState.Open)
                        cmd.Connection.Open();

                    cmd.ExecuteNonQuery();
                    ts.Complete();
                }
                catch (Exception ex)
                {
                    ts.Dispose();
                    throw ex;
                }
                finally
                {
                    if (cmd != null) cmd.Dispose();
                }
            }
        }
    }
}