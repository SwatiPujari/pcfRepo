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
using System.Globalization;

namespace Xpedeon.ProfitCashflow
{
    public partial class ErpLogin : DevExpress.XtraEditors.XtraForm
    {
        DataTable dtSiteConfig;

        public ErpLogin()
        {
            InitializeComponent();
        }

        private void cxBttnOK_Click(object sender, EventArgs e)
        {
            if (cxEdiUserName.EditValue == null || Convert.ToString(cxEdiUserName.EditValue).Trim() == "")
            {
                XtraMessageBox.Show("User Name cannot be blank...", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cxEdPassword.EditValue == null || Convert.ToString(cxEdPassword.EditValue).Trim() == "")
            {
                XtraMessageBox.Show("Password cannot be blank...", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cxEdiUserName.EditValue != null && !string.IsNullOrWhiteSpace(cxEdiUserName.EditValue.ToString()))
            {
                string sEncryptPassword = "";
                try
                {
                    ProfitCashflow.oPcfDM.RetrieveUserInfo(Convert.ToString(cxEdiUserName.EditValue).ToUpper());
                    RetrieveSiteConfigInfo();

                    if (ProfitCashflow.oPcfDM.dtUserInfo != null && ProfitCashflow.oPcfDM.dtUserInfo.Rows.Count > 0)
                    {
                        DataRow drUserInfo = ProfitCashflow.oPcfDM.dtUserInfo.Rows[0];

                        ProfitCashflow.oPcfDM.UserName = Convert.ToString(drUserInfo["USERNAME"]);
                        ProfitCashflow.oPcfDM.system_date = Convert.ToDateTime(drUserInfo["CURR_DATE_TIME"]);
                        ProfitCashflow.oPcfDM.datewithoutseconds = Convert.ToDateTime(drUserInfo["CURR_DATE"]);

                        if (drUserInfo["ENABLE_DISABLE"] != DBNull.Value && drUserInfo["ENABLE_DISABLE"].ToString() == "N")
                        {
                            XtraMessageBox.Show("User needs to be enabled to log in.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        sEncryptPassword = XpedeonCrypto.XpedeonServerEncrypt(Convert.ToString(cxEdPassword.EditValue));
                        if (drUserInfo["PASSWORD"] != DBNull.Value && drUserInfo["PASSWORD"].ToString() != sEncryptPassword)
                        {
                            XtraMessageBox.Show("Password entered is incorrect.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        double dPwdExpDuratn = 0;
                        if (dtSiteConfig != null && dtSiteConfig.Rows.Count > 0 && dtSiteConfig.Rows.Find("PASSWORD_EXPIRY_DURATION") != null)
                            dPwdExpDuratn = Convert.ToDouble(dtSiteConfig.Rows.Find("PASSWORD_EXPIRY_DURATION")["VALUE"]);

                        if (drUserInfo["PASSWORD_UPDATED_ON"] != DBNull.Value &&
                           (ProfitCashflow.oPcfDM.system_date - Convert.ToDateTime(drUserInfo["PASSWORD_UPDATED_ON"])).TotalDays > dPwdExpDuratn)
                        {
                            XtraMessageBox.Show("Password Expired", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            NewPassword oChngPwd = new NewPassword();
                            oChngPwd.ShowDialog();
                            oChngPwd.Dispose();

                            this.Close(); return;
                        }

                        if (dtSiteConfig != null && dtSiteConfig.Rows.Count > 0 && dtSiteConfig.Rows.Find("BLACKOUT_WARNING_INTERVAL") != null)
                            ProfitCashflow.oPcfDM.blackoutwaringinterval = Convert.ToDouble(dtSiteConfig.Rows.Find("BLACKOUT_WARNING_INTERVAL")["VALUE"]);

                        if (Convert.ToString(drUserInfo["PCF_CORPORATE_USER"]) != "Y")
                        {
                            ProfitCashflow.oPcfDM.RetrieveBlackoutPeriodInfo(ProfitCashflow.oPcfDM.system_date);
                            ProfitCashflow.oPcfDM.RetrieveBlackoutExceptionInfo(drUserInfo["USERNAME"].ToString(), ProfitCashflow.oPcfDM.system_date);

                            DataRow[] drBOPeriod = null;
                            DataRow[] drBOExceptn = ProfitCashflow.oPcfDM.dtBlackoutExceptn.Select("FROMDATE1 <= #" + ProfitCashflow.oPcfDM.datewithoutseconds.ToString(CultureInfo.InvariantCulture) +
                                "# AND TODATE1 >= #" + ProfitCashflow.oPcfDM.datewithoutseconds.ToString(CultureInfo.InvariantCulture) + "#");

                            if (drBOExceptn != null && drBOExceptn.Length == 0)
                            {
                                drBOPeriod = ProfitCashflow.oPcfDM.dtBlackoutPeriod.Select("FROMDATE1 <= #" + ProfitCashflow.oPcfDM.datewithoutseconds.ToString(CultureInfo.InvariantCulture) +
                                    "# AND TODATE1 >= #" + ProfitCashflow.oPcfDM.datewithoutseconds.ToString(CultureInfo.InvariantCulture) + "#");

                                if (drBOPeriod != null && drBOPeriod.Length > 0)
                                {
                                    XtraMessageBox.Show("Cannot login blackout period commenced, logins possible after " + Convert.ToString(drBOPeriod[0]["TODATE"]) + ".");
                                    Application.ExitThread();
                                    return;
                                }

                                ProfitCashflow.oPcfDM.datewithblackoutinterval = ProfitCashflow.oPcfDM.datewithoutseconds.AddMinutes(ProfitCashflow.oPcfDM.blackoutwaringinterval);

                                drBOExceptn = ProfitCashflow.oPcfDM.dtBlackoutExceptn.Select("FROMDATE1 <= #" + ProfitCashflow.oPcfDM.datewithblackoutinterval.ToString(CultureInfo.InvariantCulture) +
                                    "# AND TODATE1 >= #" + ProfitCashflow.oPcfDM.datewithblackoutinterval.ToString(CultureInfo.InvariantCulture) + "#");

                                if (drBOExceptn != null && drBOExceptn.Length == 0)
                                {
                                    drBOPeriod = ProfitCashflow.oPcfDM.dtBlackoutPeriod.Select("FROMDATE1 <= #" + ProfitCashflow.oPcfDM.datewithblackoutinterval.ToString(CultureInfo.InvariantCulture) +
                                        "# AND TODATE1 >= #" + ProfitCashflow.oPcfDM.datewithblackoutinterval.ToString(CultureInfo.InvariantCulture) + "#");

                                    if (drBOPeriod != null && drBOPeriod.Length > 0)
                                    {
                                        TimeSpan span = (Convert.ToDateTime(drBOPeriod[0]["FROMDATE"]) - ProfitCashflow.oPcfDM.system_date);
                                        ProfitCashflow.oPcfDM.iMinsLeft = span.Minutes;
                                        ProfitCashflow.oPcfDM.iSecsLeft = span.Seconds;

                                        if (ProfitCashflow.oPcfDM.iMinsLeft > 0 && ProfitCashflow.oPcfDM.iSecsLeft > 0)
                                            ProfitCashflow.oPcfDM.iSecsLeft = ProfitCashflow.oPcfDM.iSecsLeft - ProfitCashflow.oPcfDM.iMinsLeft * 60;

                                        if (ProfitCashflow.oPcfDM.iMinsLeft + ProfitCashflow.oPcfDM.iSecsLeft > 0)
                                        {
                                            ProfitCashflow.oPcfDM.bAutoShutdown = true;
                                            XtraMessageBox.Show("Blackout period starting in " + ProfitCashflow.oPcfDM.iMinsLeft.ToString() + " minutes and " + ProfitCashflow.oPcfDM.iSecsLeft.ToString() + " seconds.\nAll unsaved transactions will be lost. Please save your work and exit the P.F.C. application.");
                                        }
                                        else
                                        {
                                            XtraMessageBox.Show("Cannot login blackout period commenced, logins possible after " + Convert.ToString(drBOPeriod[0]["TODATE"]) + ".");
                                            Application.ExitThread();
                                            return;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                TimeSpan span = (ProfitCashflow.oPcfDM.datewithoutseconds - Convert.ToDateTime(drBOExceptn[0]["TODATE1"]));
                                ProfitCashflow.oPcfDM.iMinsLeft = span.Minutes;
                                ProfitCashflow.oPcfDM.iSecsLeft = span.Seconds;

                                if (ProfitCashflow.oPcfDM.iMinsLeft > 0 && ProfitCashflow.oPcfDM.iSecsLeft > 0)
                                    ProfitCashflow.oPcfDM.iSecsLeft = ProfitCashflow.oPcfDM.iSecsLeft - ProfitCashflow.oPcfDM.iMinsLeft * 60;

                                if (ProfitCashflow.oPcfDM.iMinsLeft + ProfitCashflow.oPcfDM.iSecsLeft > 0)
                                {
                                    if (ProfitCashflow.oPcfDM.iMinsLeft <= (int)ProfitCashflow.oPcfDM.blackoutwaringinterval)
                                    {
                                        drBOPeriod = ProfitCashflow.oPcfDM.dtBlackoutPeriod.Select("FROMDATE1 <= #" + Convert.ToDateTime(drBOExceptn[0]["TODATE1"]).ToString(CultureInfo.InvariantCulture) +
                                            "# AND TODATE1 > #" + Convert.ToDateTime(drBOExceptn[0]["TODATE1"]).ToString(CultureInfo.InvariantCulture) + "#");

                                        if (drBOPeriod != null && drBOPeriod.Length > 0)
                                        {
                                            ProfitCashflow.oPcfDM.bAutoShutdown = true;
                                            XtraMessageBox.Show("Blackout period starting in " + ProfitCashflow.oPcfDM.iMinsLeft.ToString() + " minutes and " + ProfitCashflow.oPcfDM.iSecsLeft.ToString() + " seconds.\nAll unsaved transactions will be lost. Please save your work and exit the P.F.C. application.");
                                        }
                                    }
                                }
                                else
                                {
                                    XtraMessageBox.Show("Cannot login blackout period commenced, logins possible after " + Convert.ToString(drBOPeriod[0]["TODATE"]) + ".");
                                    Application.ExitThread();
                                    return;
                                }
                            }

                            ProfitCashflow.oPcfDM.Timer1.Enabled = true;
                        }

                        ProfitCashflow.oPcfDM.SetSessionId();

                        ProfitCashflow.oPcfDM.CodaUserName = Convert.ToString(drUserInfo["CODA_USERNAME"]);
                        ProfitCashflow.oPcfDM.CodaUserPassword = Convert.ToString(drUserInfo["CODA_USER_PASSWORD"]);

                        XtraMessageBox.Show(Convert.ToString(cxEdiUserName.EditValue).ToUpper() + " has logged in successfully.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    }
                    else
                    {
                        XtraMessageBox.Show("Invalid User Name or Password.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void RetrieveSiteConfigInfo()
        {
            SqlDataAdapter da = null;
            dtSiteConfig = new DataTable();

            try
            {
                da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("SELECT NAME, VALUE FROM SITE_CONFIG_TABLE", ProfitCashflow.oPcfDM.conn);
                da.SelectCommand.CommandType = CommandType.Text;

                da.Fill(dtSiteConfig);
                dtSiteConfig.Constraints.Add("PK_NAME", dtSiteConfig.Columns["NAME"], true);
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