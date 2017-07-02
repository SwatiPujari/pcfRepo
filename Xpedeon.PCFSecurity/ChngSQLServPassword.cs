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
    public partial class ChngSQLServPassword : DevExpress.XtraEditors.XtraForm
    {
        SecurityMainfrm oSecMainFrm = new SecurityMainfrm();

        public ChngSQLServPassword()
        {
            InitializeComponent();
        }

        private void ChngSQLServPassword_Load(object sender, EventArgs e)
        {
            cxTextEditUserName.EditValue = PCFSecurity.oSecDM.sSuperUserName;
        }

        private void ChngSQLServPassword_Shown(object sender, EventArgs e)
        {
            cxTextEditOldPwd.Focus();
        }

        private void cxBtnOk_Click(object sender, EventArgs e)
        {
            string sSQLServOldPwd, sSQLServNewPwd;

            if (cxTextEditOldPwd.EditValue == null || Convert.ToString(cxTextEditOldPwd.EditValue).Trim() == "")
            {
                oSecMainFrm.MessageDlg("Old Password cannot be blank...", "mtWarning", "mbOk", 0);
                return;
            }
            if (cxTextEditNewPwd.EditValue == null || Convert.ToString(cxTextEditNewPwd.EditValue).Trim() == "")
            {
                oSecMainFrm.MessageDlg("Please enter the new password.", "mtWarning", "mbOk", 0);
                return;
            }
            if (cxTextEditConfirmPwd.EditValue == null || Convert.ToString(cxTextEditConfirmPwd.EditValue).Trim() == "")
            {
                oSecMainFrm.MessageDlg("Please enter the confirm password.", "mtWarning", "mbOk", 0);
                return;
            }

            try
            {
                sSQLServOldPwd = XpedeonCrypto.XpedeonServerDecrypt(PCFSecurity.oSecDM.sSuperUserPassword);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (!Convert.ToString(cxTextEditOldPwd.EditValue).Equals(sSQLServOldPwd, StringComparison.InvariantCulture))
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
                sSQLServNewPwd = XpedeonCrypto.XpedeonServerEncrypt(Convert.ToString(cxTextEditNewPwd.EditValue));
                UpdateSQLServPassword(Convert.ToString(cxTextEditUserName.EditValue), Convert.ToString(cxTextEditNewPwd.EditValue), Convert.ToString(cxTextEditOldPwd.EditValue));

                string sAppPath = Application.StartupPath.ToString();
                System.Xml.XmlDocument xdDataBaseConnection = new System.Xml.XmlDocument();
                xdDataBaseConnection.Load(@sAppPath + "\\PCFSecurityAccessInfo.xml");
                if (xdDataBaseConnection.GetElementsByTagName("PASSWORD").Count > 0)
                {
                    // Get the target node using XPath
                    System.Xml.XmlNode xnOldPwd = xdDataBaseConnection.SelectSingleNode("//PASSWORD");
                    // Create a new comment node with XML content of the target node
                    System.Xml.XmlComment xcOldPwd = xdDataBaseConnection.CreateComment(xnOldPwd.OuterXml);
                    // Replace the target node with the comment
                    xdDataBaseConnection.DocumentElement.ReplaceChild(xcOldPwd, xnOldPwd);

                    // Create a new node
                    System.Xml.XmlElement xeNewPwd = xdDataBaseConnection.CreateElement("PASSWORD");
                    xeNewPwd.InnerText = sSQLServNewPwd;
                    // Add the node to the document
                    xdDataBaseConnection.DocumentElement.AppendChild(xeNewPwd);
                }
                xdDataBaseConnection.Save(@sAppPath + "\\PCFSecurityAccessInfo.xml");

                PCFSecurity.oSecDM.DataModuleCreate();

                oSecMainFrm.MessageDlg("Password Changed Successfully.", "mtConfirmation", "mbOk", 0);
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void UpdateSQLServPassword(string sUserName, string sPassword, string sOldPassword)
        {
            TransactionOptions to = new TransactionOptions();
            to.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew, to, EnterpriseServicesInteropOption.Full))
            {
                SqlCommand cmd = null;
                try
                {
                    cmd = new SqlCommand("SPN_CHANGE_PASSWORD", PCFSecurity.oSecDM.conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IC_USER", sUserName);
                    cmd.Parameters.AddWithValue("@IC_PASSWORD", sPassword);
                    cmd.Parameters.AddWithValue("@IC_OLD_PASSWORD", sOldPassword);

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