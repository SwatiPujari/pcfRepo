using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace Xpedeon.PCFSecurity
{
    public class sUserSetup
    {
        public DataTable RetrieveAllSiteUsers()
        {
            SqlDataAdapter da = null;
            DataTable dtAllSiteUsers = new DataTable();

            try
            {
                da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("SPN_XS_GET_ALL_SITE_USERS", PCFSecurity.oSecDM.conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;

                da.Fill(dtAllSiteUsers);
                dtAllSiteUsers.Constraints.Add("PK_USER", dtAllSiteUsers.Columns["USERNAME"], true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (da != null) da.Dispose();
            }

            return dtAllSiteUsers;
        }

        public DataTable RetrieveUserCompanies(string sPCFUser)
        {
            SqlDataAdapter da = null;
            DataTable dtUserComp = new DataTable();

            try
            {
                da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("SPN_XS_GET_COMP_FOR_USER", PCFSecurity.oSecDM.conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@IV_PCF_USER", sPCFUser);

                da.Fill(dtUserComp);
                dtUserComp.Constraints.Add("PK_COMP", dtUserComp.Columns["COMPANY_ID"], true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (da != null) da.Dispose();
            }

            return dtUserComp;
        }

        public void UpdateUserCompanies(string sType, string sPCFUser, decimal dCompId)
        {
            TransactionOptions to = new TransactionOptions();
            to.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew, to, EnterpriseServicesInteropOption.Full))
            {
                SqlCommand cmd = null;
                try
                {
                    if (sType == "I")
                        cmd = new SqlCommand("INSERT INTO SECURITY_COMPANY_USERS (COMPANY_ID, PCF_USER) VALUES (@IN_COMPANY_ID, @IV_PCF_USER)", PCFSecurity.oSecDM.conn);
                    else if (sType == "D")
                        cmd = new SqlCommand("DELETE SECURITY_COMPANY_USERS WHERE COMPANY_ID=@IN_COMPANY_ID AND PCF_USER=@IV_PCF_USER", PCFSecurity.oSecDM.conn);

                    cmd.Parameters.AddWithValue("@IV_PCF_USER", sPCFUser);
                    cmd.Parameters.AddWithValue("@IN_COMPANY_ID", dCompId);

                    cmd.CommandType = CommandType.Text;
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

        public DataTable RetrieveUserSetupInfo(string sUserName)
        {
            SqlDataAdapter da = null;
            DataTable dtUserSetupInfo = new DataTable();

            try
            {
                da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("SPN_XS_GET_SITE_USER_INFO", PCFSecurity.oSecDM.conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@IV_USERNAME", sUserName);

                da.Fill(dtUserSetupInfo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (da != null) da.Dispose();
            }

            return dtUserSetupInfo;
        }

        public object CheckUserNameExist(string sUserName)
        {
            object oResult = new object();
            SqlCommand cmd = null;

            try
            {
                cmd = new SqlCommand("SELECT COUNT(1) KOUNT FROM SITE_USERS WHERE USERNAME=@IV_USERNAME", PCFSecurity.oSecDM.conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@IV_USERNAME", sUserName);

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

        public void UpdateUserSetup(string sType, DataRow drUserSetup)
        {
            TransactionOptions to = new TransactionOptions();
            to.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew, to, EnterpriseServicesInteropOption.Full))
            {
                SqlCommand cmd = null;
                try
                {
                    if (sType == "I")
                        cmd = new SqlCommand("SPN_XS_INS_SITE_USER", PCFSecurity.oSecDM.conn);
                    else if (sType == "U")
                        cmd = new SqlCommand("SPN_XS_UPD_SITE_USER", PCFSecurity.oSecDM.conn);

                    cmd.Parameters.AddWithValue("@IV_USERNAME", drUserSetup["USERNAME"]);
                    cmd.Parameters.AddWithValue("@IV_LAST_NAME", drUserSetup["LAST_NAME"]);
                    cmd.Parameters.AddWithValue("@IV_FIRST_NAME", drUserSetup["FIRST_NAME"]);
                    cmd.Parameters.AddWithValue("@IV_MIDDLE_INITIAL", drUserSetup["MIDDLE_INITIAL"]);
                    cmd.Parameters.AddWithValue("@IV_SALUTATION", drUserSetup["SALUTATION"]);
                    cmd.Parameters.AddWithValue("@IV_EMAIL", drUserSetup["EMAIL"]);
                    cmd.Parameters.AddWithValue("@IV_TEL", drUserSetup["TEL"]);
                    cmd.Parameters.AddWithValue("@IV_FAX", drUserSetup["FAX"]);
                    cmd.Parameters.AddWithValue("@IV_MOBILE", drUserSetup["MOBILE"]);
                    cmd.Parameters.AddWithValue("@IN_FUNCTION_FLAG", drUserSetup["FUNCTION_FLAG"]);
                    cmd.Parameters.AddWithValue("@IV_USER_PASSWORD", drUserSetup["USER_PASSWORD"]);
                    cmd.Parameters.AddWithValue("@IV_DESIGNATION", drUserSetup["DESIGNATION"]);
                    cmd.Parameters.AddWithValue("@IV_DISPLAYNAME", drUserSetup["DISPLAYNAME"]);
                    cmd.Parameters.AddWithValue("@IV_TEL_NO1", drUserSetup["TEL_NO1"]);
                    cmd.Parameters.AddWithValue("@IV_TEL_NO2", drUserSetup["TEL_NO2"]);
                    cmd.Parameters.AddWithValue("@IV_TEL_NO3", drUserSetup["TEL_NO3"]);
                    cmd.Parameters.AddWithValue("@IV_ENABLE_DISABLE", drUserSetup["ENABLE_DISABLE"]);
                    cmd.Parameters.AddWithValue("@ID_PASSWORD_UPDATED_ON", drUserSetup["PASSWORD_UPDATED_ON"]);
                    cmd.Parameters.AddWithValue("@IV_CODA_USERNAME", drUserSetup["CODA_USERNAME"]);
                    cmd.Parameters.AddWithValue("@IV_CODA_USER_PASSWORD", drUserSetup["CODA_USER_PASSWORD"]);
                    cmd.Parameters.AddWithValue("@IN_CODA_SEC1", drUserSetup["CODA_SEC1"]);
                    cmd.Parameters.AddWithValue("@IC_COMPANY_MASTER_VIEW", drUserSetup["COMPANY_MASTER_VIEW"]);
                    cmd.Parameters.AddWithValue("@IC_PCF_PLOT_MODELLING", drUserSetup["PCF_PLOT_MODELLING"]);
                    cmd.Parameters.AddWithValue("@IC_PCF_PURCHASE_TYPE", drUserSetup["PCF_PURCHASE_TYPE"]);
                    cmd.Parameters.AddWithValue("@IC_PCF_INTEREST_RATE", drUserSetup["PCF_INTEREST_RATE"]);
                    cmd.Parameters.AddWithValue("@IC_PCF_B4_SITE_CODES", drUserSetup["PCF_B4_SITE_CODES"]);
                    cmd.Parameters.AddWithValue("@IC_PCF_CASHFLOW_CATEGORIES", drUserSetup["PCF_CASHFLOW_CATEGORIES"]);
                    cmd.Parameters.AddWithValue("@IC_PCF_SITE_SETUP", drUserSetup["PCF_SITE_SETUP"]);
                    cmd.Parameters.AddWithValue("@IC_PCF_B4_STACK", drUserSetup["PCF_B4_STACK"]);
                    cmd.Parameters.AddWithValue("@IC_PCF_STACKS", drUserSetup["PCF_STACKS"]);
                    cmd.Parameters.AddWithValue("@IC_PCF_PROFIT_FORECAST", drUserSetup["PCF_PROFIT_FORECAST"]);
                    cmd.Parameters.AddWithValue("@IC_PCF_ADJUSTMENTS", drUserSetup["PCF_ADJUSTMENTS"]);
                    cmd.Parameters.AddWithValue("@IC_PCF_KEY_DATES", drUserSetup["PCF_KEY_DATES"]);
                    cmd.Parameters.AddWithValue("@IC_PCF_TAKE_TO_SALES", drUserSetup["PCF_TAKE_TO_SALES"]);
                    cmd.Parameters.AddWithValue("@IC_PCF_CASHFLOW", drUserSetup["PCF_CASHFLOW"]);
                    cmd.Parameters.AddWithValue("@IC_PCF_LOCATION_MASTER", drUserSetup["PCF_LOCATION_MASTER"]);
                    cmd.Parameters.AddWithValue("@IC_PCF_PLOT_TYPE_MASTER", drUserSetup["PCF_PLOT_TYPE_MASTER"]);
                    cmd.Parameters.AddWithValue("@IC_PCF_RESIDENCE_TYPE_MASTER", drUserSetup["PCF_RESIDENCE_TYPE_MASTER"]);
                    cmd.Parameters.AddWithValue("@IC_PCF_UNIT_CATEGORY_MASTER", drUserSetup["PCF_UNIT_CATEGORY_MASTER"]);
                    cmd.Parameters.AddWithValue("@IC_PCF_BLOCK_MASTER", drUserSetup["PCF_BLOCK_MASTER"]);
                    cmd.Parameters.AddWithValue("@IC_PCF_CASHFLOW_OTHER_INCOME", drUserSetup["PCF_CASHFLOW_OTHER_INCOME"]);
                    cmd.Parameters.AddWithValue("@IC_PCF_CASHFLOW_OTHER_EXPENSE", drUserSetup["PCF_CASHFLOW_OTHER_EXPENSE"]);
                    cmd.Parameters.AddWithValue("@IC_PCF_ACC_PERIOD", drUserSetup["PCF_ACC_PERIOD"]);
                    cmd.Parameters.AddWithValue("@IC_PCF_EDIT_FROZEN_STACK", drUserSetup["PCF_EDIT_FROZEN_STACK"]);
                    cmd.Parameters.AddWithValue("@IC_PCF_EDIT_LOCKED_STACK", drUserSetup["PCF_EDIT_LOCKED_STACK"]);
                    cmd.Parameters.AddWithValue("@IC_PCF_B4_SITE_SETUP", drUserSetup["PCF_B4_SITE_SETUP"]);
                    cmd.Parameters.AddWithValue("@IC_PCF_DELETE_STACK", drUserSetup["PCF_DELETE_STACK"]);
                    cmd.Parameters.AddWithValue("@IC_PCF_RTS_PLOT", drUserSetup["PCF_RTS_PLOT"]);
                    cmd.Parameters.AddWithValue("@IC_PCF_RTS_PARKING", drUserSetup["PCF_RTS_PARKING"]);
                    cmd.Parameters.AddWithValue("@IC_PCF_RTS_GR", drUserSetup["PCF_RTS_GR"]);
                    cmd.Parameters.AddWithValue("@IC_PCF_EDIT_PROG_DATE", drUserSetup["PCF_EDIT_PROG_DATE"]);
                    cmd.Parameters.AddWithValue("@IC_PCF_REVERSE_TTS_PLOT", drUserSetup["PCF_REVERSE_TTS_PLOT"]);
                    cmd.Parameters.AddWithValue("@IC_PCF_REVERSE_TTS_PARK", drUserSetup["PCF_REVERSE_TTS_PARK"]);
                    cmd.Parameters.AddWithValue("@IC_PCF_REVERSE_TTS_GR", drUserSetup["PCF_REVERSE_TTS_GR"]);
                    cmd.Parameters.AddWithValue("@IC_PCF_INCL_EXCL_PLOTS", drUserSetup["PCF_INCL_EXCL_PLOTS"]);
                    cmd.Parameters.AddWithValue("@IC_PCF_USE_STACKS_IN_REPORT", drUserSetup["PCF_USE_STACKS_IN_REPORT"]);
                    cmd.Parameters.AddWithValue("@IC_PCF_PROCESS_TO_PREV_STACK", drUserSetup["PCF_PROCESS_TO_PREV_STACK"]);
                    cmd.Parameters.AddWithValue("@IC_PCF_RESTACK", drUserSetup["PCF_RESTACK"]);
                    cmd.Parameters.AddWithValue("@IC_PCF_ZERO_WIP", drUserSetup["PCF_ZERO_WIP"]);
                    cmd.Parameters.AddWithValue("@IC_PCF_UNFINALISE_PLOTS", drUserSetup["PCF_UNFINALISE_PLOTS"]);
                    cmd.Parameters.AddWithValue("@IC_PCF_PF_VIEW_MASTER", drUserSetup["PCF_PF_VIEW_MASTER"]);
                    cmd.Parameters.AddWithValue("@IC_PCF_PF_ASPECT_MASTER", drUserSetup["PCF_PF_ASPECT_MASTER"]);
                    cmd.Parameters.AddWithValue("@IC_PCF_PF_GARAGE_PARKING_MASTER", drUserSetup["PCF_PF_GARAGE_PARKING_MASTER"]);
                    cmd.Parameters.AddWithValue("@IC_PCF_PF_BALCONY_TERRACE_MASTER", drUserSetup["PCF_PF_BALCONY_TERRACE_MASTER"]);
                    cmd.Parameters.AddWithValue("@IC_PCF_SS_LAND_TYPE", drUserSetup["PCF_SS_LAND_TYPE"]);
                    cmd.Parameters.AddWithValue("@IC_PCF_SS_BUILD_TYPE", drUserSetup["PCF_SS_BUILD_TYPE"]);
                    cmd.Parameters.AddWithValue("@IC_PCF_DIFF_STACKS_PF_REP", drUserSetup["PCF_DIFF_STACKS_PF_REP"]);
                    cmd.Parameters.AddWithValue("@IC_PCF_PFU_STACK", drUserSetup["PCF_PFU_STACK"]);
                    cmd.Parameters.AddWithValue("@IC_PCF_PF_SUMMARY", drUserSetup["PCF_PF_SUMMARY"]);
                    cmd.Parameters.AddWithValue("@IC_PCF_DELETE_PF", drUserSetup["PCF_DELETE_PF"]);
                    cmd.Parameters.AddWithValue("@IC_PCF_IMP_PF", drUserSetup["PCF_IMP_PF"]);
                    cmd.Parameters.AddWithValue("@IC_PCF_CORPORATE_USER", drUserSetup["PCF_CORPORATE_USER"]);
                    cmd.Parameters.AddWithValue("@IC_PCF_MAINTAIN_BLACKOUT_PERIOD", drUserSetup["PCF_MAINTAIN_BLACKOUT_PERIOD"]);
                    cmd.Parameters.AddWithValue("@IC_PCF_GROUP_MASTER", drUserSetup["PCF_GROUP_MASTER"]);
                    cmd.Parameters.AddWithValue("@IC_PCF_DIVISION_MASTER", drUserSetup["PCF_DIVISION_MASTER"]);
                    cmd.Parameters.AddWithValue("@IC_PCF_REGION_MASTER", drUserSetup["PCF_REGION_MASTER"]);
                    cmd.Parameters.AddWithValue("@IC_PCF_OPERATING_COMPANY_MASTER", drUserSetup["PCF_OPERATING_COMPANY_MASTER"]);
                    cmd.Parameters.AddWithValue("@IC_PCF_DEVELOPMENT_MASTER", drUserSetup["PCF_DEVELOPMENT_MASTER"]);

                    cmd.CommandType = CommandType.StoredProcedure;
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
