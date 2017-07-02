using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Transactions;

namespace Xpedeon.ProfitCashflow
{
    class sCommonMethods
    {
        public DataTable RetrieveCompanyList(string sUserName)
        {
            SqlDataAdapter da = null;
            DataTable dtCompanyList = new DataTable();

            try
            {
                da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("SPN_XP_GET_COMPANY_FOR_USER", ProfitCashflow.oPcfDM.conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@IV_PCF_USER", sUserName);

                da.Fill(dtCompanyList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (da != null) da.Dispose();
            }

            return dtCompanyList;
        }
        
        public void ValidateRelationCompRegOpComp(object dCompId, object dRegId, object dOpCompId)
        {
            SqlCommand cmd = null;
            try
            {
                cmd = new SqlCommand("SPN_XP_VALIDATE_RELATION", ProfitCashflow.oPcfDM.conn);
                cmd.CommandType = CommandType.StoredProcedure;

                if (dCompId != null && !string.IsNullOrWhiteSpace(dCompId.ToString()))
                    cmd.Parameters.AddWithValue("@IN_COMPANY_ID", Convert.ToDecimal(dCompId));
                if (dRegId != null && !string.IsNullOrWhiteSpace(dRegId.ToString()))
                    cmd.Parameters.AddWithValue("@IN_REGION_ID", Convert.ToDecimal(dRegId));
                if (dOpCompId != null && !string.IsNullOrWhiteSpace(dOpCompId.ToString()))
                    cmd.Parameters.AddWithValue("@IN_OPERATING_COMPANY_ID", Convert.ToDecimal(dOpCompId));

                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

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
        }

        public DataTable RetrieveCompany(string sUser, string isActive)
        {
            DataTable dtCompanyMas = new DataTable();
            using (SqlDataAdapter da = new SqlDataAdapter())
            {
                da.SelectCommand = new SqlCommand("SPN_XP_GET_COMPANY");
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@IV_PCF_USER", SqlDbType.VarChar).Value = sUser;
                da.SelectCommand.Parameters.Add("@IC_IS_ACTIVE", SqlDbType.Char).Value = isActive;
                da.SelectCommand.Connection = ProfitCashflow.oPcfDM.conn;

                da.Fill(dtCompanyMas);
            }
            return dtCompanyMas;
        }

        public DataTable RetrieveRegionMasterList()
        {
            SqlDataAdapter da = null;
            DataTable dtRegMastList = new DataTable();

            try
            {
                da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("SPN_XP_GET_REG_MASTER", ProfitCashflow.oPcfDM.conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;

                da.Fill(dtRegMastList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (da != null) da.Dispose();
            }

            return dtRegMastList;
        }

        public DataTable RetrieveDevelopmentMasterList()
        {
            SqlDataAdapter da = null;
            DataTable dtDevMastList = new DataTable();

            try
            {
                da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("SPN_XP_GET_DEV_MASTER", ProfitCashflow.oPcfDM.conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;

                da.Fill(dtDevMastList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (da != null) da.Dispose();
            }

            return dtDevMastList;
        }

        public DataTable RetrieveCompanyListByRegion(string sUserName, string sIsActive, decimal dRegId)
        {
            SqlDataAdapter da = null;
            DataTable dtCompMastList = new DataTable();

            try
            {
                da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("SPN_XP_GET_COMP_BY_REG", ProfitCashflow.oPcfDM.conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;

                da.SelectCommand.Parameters.Add("@IV_PCF_USER", SqlDbType.VarChar, 30).Value = sUserName;
                da.SelectCommand.Parameters.Add("@IC_IS_ACTIVE", SqlDbType.Char, 1).Value = sIsActive;
                if (dRegId != -1)
                    da.SelectCommand.Parameters.Add("@IN_REGION_ID", SqlDbType.Decimal, 10).Value = dRegId;

                da.Fill(dtCompMastList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (da != null) da.Dispose();
            }

            return dtCompMastList;
        }

        public DataTable RetrieveSiteListByCompDev(decimal dCompId, decimal dDevId)
        {
            SqlDataAdapter da = null;
            DataTable dtSiteList = new DataTable();

            try
            {
                da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("SPN_XP_GET_SITE_BY_COMP_DEV", ProfitCashflow.oPcfDM.conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;

                da.SelectCommand.Parameters.Add("@IN_COMPANY_ID", SqlDbType.Decimal, 10).Value = dCompId;
                if (dDevId != -1)
                    da.SelectCommand.Parameters.Add("@IN_DEVELOPMENT_ID", SqlDbType.Decimal, 10).Value = dDevId;

                da.Fill(dtSiteList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (da != null) da.Dispose();
            }

            return dtSiteList;
        }

        public int RetrievePcfLockedDocuments(decimal dDocGuid, string sSessionId)
        {
            DataTable dt = new DataTable();
            int cnt = 0;
            using (SqlDataAdapter da = new SqlDataAdapter())
            {
                da.SelectCommand = new SqlCommand("SPN_XP_GET_PCF_LOCKED_DOCUMENTS");
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@IN_PCF_DOC_GUID", SqlDbType.Decimal).Value = dDocGuid;
                da.SelectCommand.Parameters.Add("@IV_SESSIONID", SqlDbType.VarChar).Value = sSessionId;
                da.SelectCommand.Connection = ProfitCashflow.oPcfDM.conn;
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    cnt = Convert.ToInt32(dt.Rows[0]["CNT"]);
                }
            }

            return cnt;
        }

        public void InsertPCFLockedDocument(decimal pcfDocGuid, string userName, string sessionId, string formCode, decimal companyId, string siteCode)
        {
            TransactionOptions to = new TransactionOptions();
            to.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;

            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew, to, EnterpriseServicesInteropOption.Full))
            {
                SqlCommand cmd = new SqlCommand();
                try
                {
                    cmd = new SqlCommand("SPN_XP_INS_PCF_LOCKED_DOCUMENTS", ProfitCashflow.oPcfDM.conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IN_PCF_DOC_GUID", pcfDocGuid);
                    cmd.Parameters.AddWithValue("@IV_USERNAME", userName);
                    cmd.Parameters.AddWithValue("@IV_SESSIONID", sessionId);
                    cmd.Parameters.AddWithValue("@IV_FORM_CODE", formCode);
                    cmd.Parameters.AddWithValue("@IN_COMPANY_ID", companyId);
                    cmd.Parameters.AddWithValue("@IV_SITE_CODE", siteCode);

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
                    cmd.Dispose();
                }
            }
        }

        public void DeletePCFLockedDocument(decimal dDocGuid, string sSessionId)
        {
            TransactionOptions to = new TransactionOptions();
            to.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;

            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew, to, EnterpriseServicesInteropOption.Full))
            {
                SqlCommand cmd = new SqlCommand();
                try
                {
                    cmd = new SqlCommand("SPN_XP_DEL_PCF_LOCKED_DOCUMENTS", ProfitCashflow.oPcfDM.conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IN_PCF_DOC_GUID", dDocGuid);
                    cmd.Parameters.AddWithValue("@IV_SESSIONID", sSessionId);

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
                    cmd.Dispose();
                }
            }
        }

        public void UpdateSiteLandStatus(decimal dCompId, string sSiteCode, string sStatus)
        {
            TransactionOptions to = new TransactionOptions();
            to.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;

            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew, to, EnterpriseServicesInteropOption.Full))
            {
                SqlCommand cmd = new SqlCommand();
                try
                {
                    cmd = new SqlCommand("SPN_XP_UPD_SITE_LAND_STATUS", ProfitCashflow.oPcfDM.conn);
                    cmd.Parameters.Add("@IN_COMPANY_ID", SqlDbType.Decimal, 10).Value = dCompId;
                    cmd.Parameters.Add("@IV_SITE_CODE", SqlDbType.VarChar, 30).Value = sSiteCode;
                    cmd.Parameters.Add("@IV_LAND_STATUS", SqlDbType.VarChar, 3).Value = sStatus;
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
                    cmd.Dispose();
                }
            }
        }

    }
}
