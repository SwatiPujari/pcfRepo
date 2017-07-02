using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Runtime.InteropServices;

namespace Xpedeon.PCFSecurity
{
    public class SecurityDataModule
    {
        public string PCF_VERSION, PCF_DB_NAME, PCF_HOST_NAME, PCF_WORK_DIR;
        public string RegUserName, pUserName, sSuperUserName, sSuperUserPassword;
        public SqlConnection conn;

        SecurityMainfrm oSecMainFrm = new SecurityMainfrm();

        public SecurityDataModule()
        {
            DataModuleCreate();
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
                        oSecMainFrm.MessageDlg("Unable to identify the database connection...", "mtError", "mbOk", 0);
                        Application.ExitThread();
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
                oSecMainFrm.MessageDlg(ex.Message, "mtError", "mbOk", 0);
                Application.ExitThread();
            }
        }

        public void checkVersion()
        {
            //string sVersion = GetFileVersionNameInfo(Application.ExecutablePath);

            System.Diagnostics.FileVersionInfo myFileVersionInfo = System.Diagnostics.FileVersionInfo.GetVersionInfo(Application.ExecutablePath);
            string sVersion = myFileVersionInfo.FileVersion;
            if (string.IsNullOrWhiteSpace(sVersion))
            {
                oSecMainFrm.MessageDlg("No Version information found.", "mtError", "mbOk", 0);
                Application.Exit();
            }

            object oExeVer = RetrieveExeVersion();
            if (oExeVer != null && Convert.ToString(oExeVer) != sVersion)
            {
                oSecMainFrm.MessageDlg("Version number in exe file and database mismatch.", "mtError", "mbOk", 0);
                Application.Exit();
            }

            PCF_VERSION = sVersion;
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
                /*if (ex is System.IO.FileNotFoundException)
                    throw ex;
                else*/
                if (ex is System.Xml.XmlException)
                {
                    oSecMainFrm.MessageDlg("PCFSecurityAccessInfo.xml : " + ex.Message, "mtError", "mbOk", 0);
                    return oResult;
                }
                else
                    throw ex;
            }

            if (xdDataBaseConnection.GetElementsByTagName("DB").Count == 0)
            {
                oSecMainFrm.MessageDlg("PCFSecurityAccessInfo.xml does not contain DB tag.", "mtError", "mbOk", 0);
                return oResult;
            }
            if (xdDataBaseConnection.GetElementsByTagName("HOSTNAME").Count == 0)
            {
                oSecMainFrm.MessageDlg("PCFSecurityAccessInfo.xml does not contain HOSTNAME tag.", "mtError", "mbOk", 0);
                return oResult;
            }
            if (xdDataBaseConnection.GetElementsByTagName("DB_NAME").Count == 0)
            {
                oSecMainFrm.MessageDlg("PCFSecurityAccessInfo.xml does not contain DB_NAME tag.", "mtError", "mbOk", 0);
                return oResult;
            }
            if (xdDataBaseConnection.GetElementsByTagName("USERNAME").Count == 0)
            {
                oSecMainFrm.MessageDlg("PCFSecurityAccessInfo.xml does not contain USERNAME tag.", "mtError", "mbOk", 0);
                return oResult;
            }

            if (xdDataBaseConnection.GetElementsByTagName("USERID").Count == 0)
            {
                oSecMainFrm.MessageDlg("PCFSecurityAccessInfo.xml does not contain USERID tag.", "mtError", "mbOk", 0);
                return oResult;
            }
            if (xdDataBaseConnection.GetElementsByTagName("PASSWORD").Count == 0)
            {
                oSecMainFrm.MessageDlg("PCFSecurityAccessInfo.xml does not contain PASSWORD tag.", "mtError", "mbOk", 0);
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

            if (xdDataBaseConnection.GetElementsByTagName("WORKING_DIRECTORY").Count > 0)
            {
                string sWorkDir = (xdDataBaseConnection.GetElementsByTagName("WORKING_DIRECTORY"))[0].InnerText;
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
            string connString = "Data Source=" + sDataSource + ";Initial Catalog=" + sInitialCatalog;
            if (sDB == "S" && !string.IsNullOrWhiteSpace(sRegUserName))
                connString += ";User ID=" + sRegUserName.ToLower() + ";Password=site";
            else
                connString += ";User ID=" + sUserID + ";Password=" + sPassword;
            oResult.Add("CONN", connString);

            return oResult;
        }

        public object RetrieveExeVersion()
        {
            object oResult = new object();
            SqlCommand cmd = new SqlCommand();

            try
            {
                cmd = new SqlCommand("SELECT VALUE FROM SITE_CONFIG_TABLE WHERE NAME='PCF_SECURITY_EXE'", PCFSecurity.oSecDM.conn);
                cmd.CommandType = CommandType.Text;

                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                oResult = cmd.ExecuteScalar();
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
