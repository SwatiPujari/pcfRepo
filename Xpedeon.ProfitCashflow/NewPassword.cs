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

namespace Xpedeon.ProfitCashflow
{
    public partial class NewPassword : DevExpress.XtraEditors.XtraForm
    {
        DataTable dtUserLoginInfo;

        public NewPassword()
        {
            InitializeComponent();
        }

        private void NewPassword_Load(object sender, EventArgs e)
        {
            cxTextEditUserName.EditValue = ProfitCashflow.oPcfDM.UserName.ToUpper();
        }

        private void NewPassword_Shown(object sender, EventArgs e)
        {
            cxTextEditOldPwd.Focus();
        }

        private void cxBtnOk_Click(object sender, EventArgs e)
        {
            if (cxTextEditOldPwd.EditValue == null || Convert.ToString(cxTextEditOldPwd.EditValue).Trim() == "")
            {
                XtraMessageBox.Show("Old Password cannot be blank...", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cxTextEditNewPwd.EditValue == null || Convert.ToString(cxTextEditNewPwd.EditValue).Trim() == "")
            {
                XtraMessageBox.Show("New Password cannot be blank...", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cxTextEditConfirmPwd.EditValue == null || Convert.ToString(cxTextEditConfirmPwd.EditValue).Trim() == "")
            {
                XtraMessageBox.Show("Confirm Password cannot be blank...", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                RetrieveUserLoginInfo(cxTextEditUserName.EditValue.ToString());
                if (dtUserLoginInfo != null && dtUserLoginInfo.Rows.Count > 0)
                {
                    string sUserName, sPassword, sOldPassword, sNewPassword;
                    sUserName = dtUserLoginInfo.Rows[0]["USERNAME"].ToString();
                    sPassword = dtUserLoginInfo.Rows[0]["USER_PASSWORD"].ToString();

                    sOldPassword = XpedeonCrypto.XpedeonServerEncrypt(Convert.ToString(cxTextEditOldPwd.EditValue));
                    if (sPassword != sOldPassword)
                    {
                        XtraMessageBox.Show("Old Password does not match.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (Convert.ToString(cxTextEditNewPwd.EditValue) != Convert.ToString(cxTextEditConfirmPwd.EditValue))
                    {
                        XtraMessageBox.Show("New Password and Confirm Password dont match.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    sNewPassword = XpedeonCrypto.XpedeonServerEncrypt(Convert.ToString(cxTextEditNewPwd.EditValue));
                    UpdateUserPassword(Convert.ToString(cxTextEditUserName.EditValue), sNewPassword);
                    XtraMessageBox.Show("Password Changed Successfully.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Question);

                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void RetrieveUserLoginInfo(string sUserName)
        {
            SqlDataAdapter da = null;
            dtUserLoginInfo = new DataTable();

            try
            {
                da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("SELECT USERNAME, USER_PASSWORD FROM SITE_USERS WHERE USERNAME = @IV_USERNAME", ProfitCashflow.oPcfDM.conn);
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
                    cmd = new SqlCommand("UPDATE SITE_USERS SET USER_PASSWORD=@IV_PASSWORD, PASSWORD_UPDATED_ON=GETDATE() WHERE USERNAME=@IV_USERNAME", ProfitCashflow.oPcfDM.conn);
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