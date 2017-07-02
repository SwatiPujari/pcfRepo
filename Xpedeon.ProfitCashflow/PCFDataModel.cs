using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Runtime.InteropServices;
using DevExpress.XtraEditors;
using System.Globalization;

namespace Xpedeon.ProfitCashflow
{
    public class PCFDataModel
    {
        public const int CONST_DIV_MUL_BY = 1000;
        public string PCF_VERSION, PCF_DB_NAME, PCF_HOST_NAME, PCF_WORK_DIR;
        public string RegUserName, UserName, sSuperUserName, sSuperUserPassword;
        public SqlConnection conn;

        //public string GridLayoutDir;  //Never used
        public string company_name, session_id, CodaUserName, CodaUserPassword;
        public decimal company_id = -1;
        public double blackoutwaringinterval;
        public int iMinsLeft, iSecsLeft;
        public bool bAutoShutdown;
        public DateTime system_date, datewithoutseconds, datewithblackoutinterval;

        public Timer Timer1;
        public DataTable dtUserInfo, dtBlackoutPeriod, dtBlackoutExceptn;

        DataTable dtCurrDate;

        public PCFDataModel()
        {
            DataModuleCreate();

            Timer1 = new Timer();
            Timer1.Tick += new EventHandler(Timer1_Tick);
            Timer1.Interval = 60000;
            Timer1.Enabled = false;
        }

        public void DataModuleCreate()
        {
            try
            {
                Dictionary<string, object> oAccessInfo = GetPCFSecAccessInfo();

                if (oAccessInfo != null && oAccessInfo.Count > 0)
                {
                    object sDB = null, sDBName = null, sHostName = null, sUserName = null, sConnStr = null;
                    object oSuperUserName = null, oSuperUserPassword = null, sWorkDir = null;

                    if (oAccessInfo.ContainsKey("DB"))
                        oAccessInfo.TryGetValue("DB", out sDB);
                    if (sDB == null || string.IsNullOrWhiteSpace(Convert.ToString(sDB)) || Convert.ToString(sDB) != "S")
                    {
                        XtraMessageBox.Show("Unable to identify the database connection...", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Application.ExitThread();
                        return;
                    }

                    if (oAccessInfo.ContainsKey("DB_NAME"))
                        oAccessInfo.TryGetValue("DB_NAME", out sDBName);
                    if (sDBName != null && !string.IsNullOrWhiteSpace(Convert.ToString(sDBName)))
                        PCF_DB_NAME = Convert.ToString(sDBName);

                    if (oAccessInfo.ContainsKey("HOSTNAME"))
                        oAccessInfo.TryGetValue("HOSTNAME", out sHostName);
                    if (sHostName != null && !string.IsNullOrWhiteSpace(Convert.ToString(sHostName)))
                        PCF_HOST_NAME = Convert.ToString(sHostName);

                    if (oAccessInfo.ContainsKey("USERNAME"))
                        oAccessInfo.TryGetValue("USERNAME", out sUserName);
                    if (sUserName != null && !string.IsNullOrWhiteSpace(Convert.ToString(sUserName)))
                        RegUserName = Convert.ToString(sUserName);

                    if (oAccessInfo.ContainsKey("USERID"))
                        oAccessInfo.TryGetValue("USERID", out oSuperUserName);
                    if (oSuperUserName != null && !string.IsNullOrWhiteSpace(Convert.ToString(oSuperUserName)))
                        sSuperUserName = Convert.ToString(oSuperUserName);

                    if (oAccessInfo.ContainsKey("PASSWORD"))
                        oAccessInfo.TryGetValue("PASSWORD", out oSuperUserPassword);
                    if (oSuperUserPassword != null && !string.IsNullOrWhiteSpace(Convert.ToString(oSuperUserPassword)))
                        sSuperUserPassword = Convert.ToString(oSuperUserPassword);

                    if (oAccessInfo.ContainsKey("WORK_DIR"))
                        oAccessInfo.TryGetValue("WORK_DIR", out sWorkDir);
                    if (sWorkDir != null && !string.IsNullOrWhiteSpace(Convert.ToString(sWorkDir)))
                        PCF_WORK_DIR = Convert.ToString(sWorkDir);

                    if (oAccessInfo.ContainsKey("CONN"))
                        oAccessInfo.TryGetValue("CONN", out sConnStr);
                    if (sConnStr != null && !string.IsNullOrWhiteSpace(Convert.ToString(sConnStr)))
                        conn = new SqlConnection(Convert.ToString(sConnStr));
                }
                else
                    Application.ExitThread();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.ExitThread();
            }
        }

        public bool checkVersion(string mode)
        {
            //string sVersion = GetFileVersionNameInfo(Application.ExecutablePath);

            System.Diagnostics.FileVersionInfo myFileVersionInfo = System.Diagnostics.FileVersionInfo.GetVersionInfo(Application.ExecutablePath);
            string sVersion = myFileVersionInfo.FileVersion;
            if (string.IsNullOrWhiteSpace(sVersion))
            {
                XtraMessageBox.Show("No Version information found.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.ExitThread();
                return false;
            }

            object oExeVer = RetrieveExeVersion(mode);
            if (oExeVer != null && Convert.ToString(oExeVer) != sVersion)
            {
                XtraMessageBox.Show("Version number in exe file and database mismatch.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.ExitThread();
                return false;
            }

            PCF_VERSION = sVersion;
            return true;
        }

        private Dictionary<string, object> GetPCFSecAccessInfo()
        {
            string sAppPath = Application.StartupPath.ToString();
            XmlDocument xdDataBaseConnection = new XmlDocument();
            Dictionary<string, object> oResult = null;

            try
            {
                xdDataBaseConnection.Load(@sAppPath + "\\PCFSecurityAccessInfo.xml");
            }
            catch (Exception ex)
            {
                if (ex is System.Xml.XmlException)
                {
                    XtraMessageBox.Show("PCFSecurityAccessInfo.xml : " + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return oResult;
                }
                else   //if (ex is System.IO.FileNotFoundException)
                {
                    XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return oResult;
                }
            }

            if (xdDataBaseConnection.GetElementsByTagName("DB").Count == 0)
            {
                XtraMessageBox.Show("PCFSecurityAccessInfo.xml does not contain DB tag.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return oResult;
            }
            if (xdDataBaseConnection.GetElementsByTagName("HOSTNAME").Count == 0)
            {
                XtraMessageBox.Show("PCFSecurityAccessInfo.xml does not contain HOSTNAME tag.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return oResult;
            }
            if (xdDataBaseConnection.GetElementsByTagName("DB_NAME").Count == 0)
            {
                XtraMessageBox.Show("PCFSecurityAccessInfo.xml does not contain DB_NAME tag.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return oResult;
            }
            if (xdDataBaseConnection.GetElementsByTagName("USERNAME").Count == 0)
            {
                XtraMessageBox.Show("PCFSecurityAccessInfo.xml does not contain USERNAME tag.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return oResult;
            }

            if (xdDataBaseConnection.GetElementsByTagName("USERID").Count == 0)
            {
                XtraMessageBox.Show("PCFSecurityAccessInfo.xml does not contain USERID tag.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return oResult;
            }
            if (xdDataBaseConnection.GetElementsByTagName("PASSWORD").Count == 0)
            {
                XtraMessageBox.Show("PCFSecurityAccessInfo.xml does not contain PASSWORD tag.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return oResult;
            }

            oResult = new Dictionary<string, object>();

            string sDB = (xdDataBaseConnection.GetElementsByTagName("DB"))[0].InnerText;
            oResult.Add("DB", sDB);
            string sDataSource = (xdDataBaseConnection.GetElementsByTagName("HOSTNAME"))[0].InnerText;
            oResult.Add("HOSTNAME", sDataSource);
            string sInitialCatalog = (xdDataBaseConnection.GetElementsByTagName("DB_NAME"))[0].InnerText;
            oResult.Add("DB_NAME", sInitialCatalog);
            string sRegUserName = (xdDataBaseConnection.GetElementsByTagName("USERNAME"))[0].InnerText;
            oResult.Add("USERNAME", sRegUserName);

            if (xdDataBaseConnection.GetElementsByTagName("Working Directory").Count > 0)
            {
                string sWorkDir = (xdDataBaseConnection.GetElementsByTagName("Working Directory"))[0].InnerText;
                oResult.Add("WORK_DIR", sWorkDir);
            }

            string sUserID = (xdDataBaseConnection.GetElementsByTagName("USERID"))[0].InnerText;
            oResult.Add("USERID", sUserID);
            string sPassword = (xdDataBaseConnection.GetElementsByTagName("PASSWORD"))[0].InnerText;
            oResult.Add("PASSWORD", sPassword);
            try
            {
                sPassword = XpedeonCrypto.XpedeonServerDecrypt(sPassword);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            //string connString = "Data Source=" + sDataSource + ";Initial Catalog=" + sInitialCatalog + ";User ID=" + sUserID + ";Password=" + sPassword;
            string connString = "Data Source= USER-PC" + ";Initial Catalog=" + sInitialCatalog + 
                ";Integrated Security=True;";
            //if (sDB == "S" && !string.IsNullOrWhiteSpace(sRegUserName))
            //    connString += ";User ID=" + sRegUserName.ToLower() + ";Password=site";
            //else
            //    connString += ";User ID=" + sUserID + ";Password=" + sPassword;
            oResult.Add("CONN", connString);

            return oResult;
        }
        
        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (conn != null && conn.State == ConnectionState.Open && dtUserInfo != null && dtUserInfo.Rows.Count > 0)
            {
                DataRow drUserInfo = dtUserInfo.Rows[0];

                if (Convert.ToString(drUserInfo["PCF_CORPORATE_USER"]) != "Y")
                {
                    RetrieveCurrentDateTimeInfo();
                    if (dtCurrDate != null && dtCurrDate.Rows.Count > 0)
                    {
                        system_date = Convert.ToDateTime(dtCurrDate.Rows[0]["CurrentDateTime"]);
                        datewithoutseconds = Convert.ToDateTime(dtCurrDate.Rows[0]["CurrentDate"]);

                        RetrieveBlackoutPeriodInfo(system_date);
                        RetrieveBlackoutExceptionInfo(drUserInfo["USERNAME"].ToString(), system_date);

                        DataRow[] drBOPeriod = null;
                        DataRow[] drBOExceptn = dtBlackoutExceptn.Select("FROMDATE1 <= #" + datewithoutseconds.ToString(CultureInfo.InvariantCulture) +
                            "# AND TODATE1 >= #" + datewithoutseconds.ToString(CultureInfo.InvariantCulture) + "#");

                        if (drBOExceptn != null && drBOExceptn.Length == 0)
                        {
                            drBOPeriod = dtBlackoutPeriod.Select("FROMDATE1 <= #" + datewithoutseconds.ToString(CultureInfo.InvariantCulture) +
                                "# AND TODATE1 >= #" + datewithoutseconds.ToString(CultureInfo.InvariantCulture) + "#");

                            if (drBOPeriod != null && drBOPeriod.Length > 0)
                            {
                                XtraMessageBox.Show("Blackout period started, logins possible after " + Convert.ToString(drBOPeriod[0]["TODATE"]) + ".");
                                Application.ExitThread();
                                return;
                            }

                            datewithblackoutinterval = datewithoutseconds.AddMinutes(blackoutwaringinterval);

                            drBOExceptn = dtBlackoutExceptn.Select("FROMDATE1 <= #" + datewithblackoutinterval.ToString(CultureInfo.InvariantCulture) +
                                "# AND TODATE1 >= #" + datewithblackoutinterval.ToString(CultureInfo.InvariantCulture) + "#");

                            if (drBOExceptn != null && drBOExceptn.Length == 0)
                            {
                                drBOPeriod = dtBlackoutPeriod.Select("FROMDATE1 <= #" + datewithblackoutinterval.ToString(CultureInfo.InvariantCulture) +
                                    "# AND TODATE1 >= #" + datewithblackoutinterval.ToString(CultureInfo.InvariantCulture) + "#");

                                if (drBOPeriod != null && drBOPeriod.Length > 0)
                                {
                                    TimeSpan span = (Convert.ToDateTime(drBOPeriod[0]["FROMDATE"]) - system_date);
                                    iMinsLeft = span.Minutes;
                                    iSecsLeft = span.Seconds;

                                    if (iMinsLeft + iSecsLeft > 0)
                                    {
                                        if (bAutoShutdown == false)
                                            XtraMessageBox.Show("Blackout period starting in " + iMinsLeft.ToString() + " minutes and " + iSecsLeft.ToString() + " seconds.\nAll unsaved transactions will be lost. Please save your work and exit the P.F.C. application.");
                                        bAutoShutdown = true;
                                    }
                                    else
                                    {
                                        XtraMessageBox.Show("Blackout period started, logins possible after " + Convert.ToString(drBOPeriod[0]["TODATE"]) + ".");
                                        Application.ExitThread();
                                        return;
                                    }
                                }
                            }
                            else
                            {
                                TimeSpan span = (datewithoutseconds - Convert.ToDateTime(drBOExceptn[0]["TODATE1"]));
                                iMinsLeft = span.Minutes;
                                iSecsLeft = span.Seconds;

                                if (iMinsLeft > 0 && iSecsLeft > 0)
                                    iSecsLeft = iSecsLeft - iMinsLeft * 60;

                                if (iMinsLeft + iSecsLeft > 0)
                                {
                                    drBOPeriod = dtBlackoutPeriod.Select("FROMDATE1 <= #" + Convert.ToDateTime(drBOExceptn[0]["TODATE1"]).ToString(CultureInfo.InvariantCulture) +
                                        "# AND TODATE1 > #" + Convert.ToDateTime(drBOExceptn[0]["TODATE1"]).ToString(CultureInfo.InvariantCulture) + "#");

                                    if (drBOPeriod != null && drBOPeriod.Length > 0)
                                    {
                                        if (bAutoShutdown == false)
                                            XtraMessageBox.Show("Blackout period starting in " + iMinsLeft.ToString() + " minutes and " + iSecsLeft.ToString() + " seconds.\nAll unsaved transactions will be lost. Please save your work and exit the P.F.C. application.");
                                        bAutoShutdown = true;
                                    }
                                }
                            }
                        }

                        else
                        {
                            TimeSpan span = (datewithoutseconds - Convert.ToDateTime(drBOExceptn[0]["TODATE1"]));
                            iMinsLeft = span.Minutes;
                            iSecsLeft = span.Seconds;

                            if (iMinsLeft > 0 && iSecsLeft > 0)
                                iSecsLeft = iSecsLeft - iMinsLeft * 60;

                            if (iMinsLeft + iSecsLeft > 0)
                            {
                                if (iMinsLeft <= (int)blackoutwaringinterval)
                                {
                                    drBOPeriod = dtBlackoutPeriod.Select("FROMDATE1 <= #" + Convert.ToDateTime(drBOExceptn[0]["TODATE1"]).ToString(CultureInfo.InvariantCulture) +
                                        "# AND TODATE1 > #" + Convert.ToDateTime(drBOExceptn[0]["TODATE1"]).ToString(CultureInfo.InvariantCulture) + "#");

                                    if (drBOPeriod != null && drBOPeriod.Length > 0)
                                    {
                                        if (bAutoShutdown == false)
                                            XtraMessageBox.Show("Blackout period starting in " + iMinsLeft.ToString() + " minutes and " + iSecsLeft.ToString() + " seconds.\nAll unsaved transactions will be lost. Please save your work and exit the P.F.C. application.");
                                        bAutoShutdown = true;
                                    }
                                }

                                if (blackoutwaringinterval < iMinsLeft)
                                    bAutoShutdown = false;
                            }
                            else
                            {
                                XtraMessageBox.Show("Blackout period started, logins possible after " + Convert.ToString(drBOPeriod[0]["TODATE"]) + ".");
                                Application.ExitThread();
                                return;
                            }
                        }
                    }
                }
            }
        }

        public object RetrieveExeVersion(string mode)
        {
            object oResult = new object();
            SqlCommand cmd = null;

            try
            {
                // PROFIT_CASHFLOW_EXE
                cmd = new SqlCommand("SELECT VALUE FROM SITE_CONFIG_TABLE WHERE NAME = '" + mode + "'", conn);
                cmd.CommandType = CommandType.Text;

                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                oResult = cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (cmd != null) cmd.Dispose();
            }

            return oResult;
        }

        public string SetSessionId()
        {
            object oResult = new object();
            SqlCommand cmd = null;

            try
            {
                cmd = new SqlCommand("SELECT @@SPID AS SESSION_ID", conn);
                cmd.CommandType = CommandType.Text;

                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                oResult = cmd.ExecuteScalar();
                if (oResult != null && !string.IsNullOrWhiteSpace(Convert.ToString(oResult)))
                    session_id = Convert.ToString(oResult);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (cmd != null) cmd.Dispose();
            }

            return session_id;
        }

        public void RetrieveUserInfo(string sUserName)
        {
            SqlDataAdapter da = null;
            dtUserInfo = new DataTable();

            try
            {
                da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("SPN_XP_GET_SITE_USER_INFO", conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@IV_USERNAME", sUserName);

                da.Fill(dtUserInfo);
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

        public void RetrieveBlackoutPeriodInfo(object oFromDate)
        {
            SqlDataAdapter da = null;
            dtBlackoutPeriod = new DataTable();

            try
            {
                da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("SPN_XP_GET_BLACKOUT_PERIOD", conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@ID_FROMDATE", Convert.ToDateTime(oFromDate));

                da.Fill(dtBlackoutPeriod);
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

        public void RetrieveBlackoutExceptionInfo(string sUserName, object oFromDate)
        {
            SqlDataAdapter da = null;
            dtBlackoutExceptn = new DataTable();

            try
            {
                da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("SPN_XP_GET_BLACKOUT_EXCEPTION", conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@IV_USERNAME", sUserName);
                da.SelectCommand.Parameters.AddWithValue("@ID_FROMDATE", Convert.ToDateTime(oFromDate));

                da.Fill(dtBlackoutExceptn);
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

        public void RetrieveCurrentDateTimeInfo()
        {
            SqlDataAdapter da = null;
            dtCurrDate = new DataTable();

            try
            {
                da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("SELECT GETDATE() AS [CurrentDateTime], CAST(CONVERT(VARCHAR,GETDATE(), 100) AS DATETIME) AS [CurrentDate]", conn);
                da.SelectCommand.CommandType = CommandType.Text;

                da.Fill(dtCurrDate);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (da != null) da.Dispose();
            }
        }

        public decimal RetrieveSequenceId(string sSeqName)
        {
            decimal oResult;
            SqlCommand cmd = null;

            try
            {
                cmd = new SqlCommand("SPN_GET_SEQ_ID", ProfitCashflow.oPcfDM.conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IC_SEQ", sSeqName);
                SqlParameter outVal = new SqlParameter("@OP_VAL", SqlDbType.Decimal, 38) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(outVal);

                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                cmd.ExecuteNonQuery();
                oResult = Convert.ToDecimal(outVal.Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (cmd != null) cmd.Dispose();
            }

            return oResult;
        }

        public object SqlQryAllpurpose(string sQuery, List<KeyValuePair<string, object>> oParameters, bool bIsExeScal = false, bool bIsStoredProc = false)
        {
            object oResult = new object();
            SqlCommand cmd = null;

            try
            {
                cmd = new SqlCommand(sQuery, ProfitCashflow.oPcfDM.conn);
                if (bIsStoredProc)
                    cmd.CommandType = CommandType.StoredProcedure;
                else
                    cmd.CommandType = CommandType.Text;

                if (oParameters != null)
                    foreach (KeyValuePair<string, object> oParam in oParameters)
                        cmd.Parameters.AddWithValue(oParam.Key, oParam.Value);

                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                if (bIsExeScal)
                    oResult = cmd.ExecuteScalar();
                else
                    cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (cmd != null) cmd.Dispose();
            }

            return oResult;
        }

        public DataTable SqlQryGetInfo(string sQuery, List<KeyValuePair<string, object>> oParameters, bool bIsStoredProc)
        {
            SqlDataAdapter da = null;
            DataTable dtResultInfo = new DataTable();

            try
            {
                da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand(sQuery, conn);

                if (bIsStoredProc)
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                else
                    da.SelectCommand.CommandType = CommandType.Text;

                if (oParameters != null)
                    foreach (KeyValuePair<string, object> oParam in oParameters)
                        da.SelectCommand.Parameters.AddWithValue(oParam.Key, oParam.Value);

                da.Fill(dtResultInfo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (da != null) da.Dispose();
            }

            return dtResultInfo;
        }

        #region FileVersionInfo
        /*[DllImport("version.dll")]
        private static extern bool GetFileVersionInfo(string sFileName, int handle, int size, byte[] infoBuffer);
        [DllImport("version.dll")]
        private static extern int GetFileVersionInfoSize(string sFileName, out int handle);

        // The third parameter – “out string pValue” – is automatically marshaled from ANSI to Unicode:
        [DllImport("version.dll", CharSet = CharSet.Ansi)]
        unsafe private static extern bool VerQueryValue(byte[] pBlock, string pSubBlock, out string pValue, out uint len);
        // This VerQueryValue overload is marked with ‘unsafe’ because it uses a short*:
        [DllImport("version.dll", CharSet = CharSet.Ansi)]
        unsafe private static extern bool VerQueryValue(byte[] pBlock, string pSubBlock, out short* pValue, out uint len);

        unsafe public static string GetFileVersionNameInfo(string path)
        {
            string name = null;
            int handle = 0;
            int size = GetFileVersionInfoSize(path, out handle);
            if (size != 0)
            {
                byte[] buffer = new byte[size];

                if (GetFileVersionInfo(path, handle, size, buffer))
                {
                    uint len = 0;
                    short* subBlock = null;
                    if (VerQueryValue(buffer, @"\VarFileInfo\Translation", out subBlock, out len) && len > 2)
                    {
                        string spv = @"\StringFileInfo\" + subBlock[0].ToString("X4") + subBlock[1].ToString("X4") + @"\FileVersion";
                        byte* pVersion = null;
                        string versionInfo;
                        if (VerQueryValue(buffer, spv, out versionInfo, out len))
                            name = versionInfo;
                    }
                }
            }
            return name;
        }*/
        #endregion
    }
}
