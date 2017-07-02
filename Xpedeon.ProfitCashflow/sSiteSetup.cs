using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Transactions;

namespace Xpedeon.ProfitCashflow
{
    class sSiteSetup
    {
        public DataTable RetrieveSites(decimal? dCompanyId)
        {
            DataTable dt = new DataTable();
            using (SqlDataAdapter da = new SqlDataAdapter())
            {
                da.SelectCommand = new SqlCommand("SPN_XP_GET_ALL_SITES");
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                if (dCompanyId != null)
                    da.SelectCommand.Parameters.Add("@IN_COMPANY_ID", SqlDbType.Decimal).Value = dCompanyId;

                da.SelectCommand.Connection = ProfitCashflow.oPcfDM.conn;
                da.Fill(dt);
            }

            return dt;
        }

        public DataSet RetrievePrefixSuffixSites(decimal dCompanyId)
        {
            DataSet ds = new DataSet();
            using (SqlDataAdapter da = new SqlDataAdapter())
            {
                da.SelectCommand = new SqlCommand("SPN_XP_GET_PREFIX_SUFFIX_SITE_CODE");
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@IN_COMPANY_ID", SqlDbType.Decimal).Value = dCompanyId;
                da.SelectCommand.Connection = ProfitCashflow.oPcfDM.conn;
                da.Fill(ds);
            }

            return ds;
        }

        public DataTable RetrieveSiteConfigValue()
        {
            DataTable dt = new DataTable();
            using (SqlDataAdapter da = new SqlDataAdapter())
            {
                da.SelectCommand = new SqlCommand("SPN_XP_GET_SITE_CONFIG_VALUE");
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Connection = ProfitCashflow.oPcfDM.conn;
                da.Fill(dt);
            }

            return dt;
        }

        public int ExistsSiteCode(decimal dCompanyId, string sSiteCode)
        {
            int cnt = 0;
            DataTable dt = new DataTable();
            using (SqlDataAdapter da = new SqlDataAdapter())
            {
                da.SelectCommand = new SqlCommand("SPN_XP_GET_SITE_CODE");
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@IN_COMPANY_ID", SqlDbType.Decimal).Value = dCompanyId;
                da.SelectCommand.Parameters.Add("@IV_SITE_CODE", SqlDbType.VarChar).Value = sSiteCode;
                da.SelectCommand.Connection = ProfitCashflow.oPcfDM.conn;
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    cnt = Convert.ToInt32(dt.Rows[0]["CNT"]);
                }
            }

            return cnt;
        }

        public DataTable RetrieveJVCompany(decimal dCompanyId)
        {
            DataTable dt = new DataTable();
            using (SqlDataAdapter da = new SqlDataAdapter())
            {
                da.SelectCommand = new SqlCommand("SPN_XP_GET_JV_COMPANY");
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@IN_COMPANY_ID", SqlDbType.Decimal).Value = dCompanyId;
                da.SelectCommand.Connection = ProfitCashflow.oPcfDM.conn;
                da.Fill(dt);
            }

            return dt;
        }

        public DataTable RetrieveSearchSites(decimal dCompanyId, string sSite, decimal? development, string sShortName, string sMarketingName)
        {
            DataTable dt = new DataTable();
            using (SqlDataAdapter da = new SqlDataAdapter())
            {
                da.SelectCommand = new SqlCommand("SPN_XP_GET_SITE_SEARCH");
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@IN_COMPANY_ID", SqlDbType.Decimal).Value = dCompanyId;
                da.SelectCommand.Parameters.Add("@IV_SITE_CODE", SqlDbType.VarChar).Value = sSite;
                da.SelectCommand.Parameters.Add("@IN_DEVELOPMENT_ID", SqlDbType.Decimal).Value = development;
                da.SelectCommand.Parameters.Add("@IV_SITE_SHORT_NAME", SqlDbType.VarChar).Value = sShortName;
                da.SelectCommand.Parameters.Add("@IV_MKTG_NAME", SqlDbType.VarChar).Value = sMarketingName;
                da.SelectCommand.Connection = ProfitCashflow.oPcfDM.conn;
                da.Fill(dt);
            }

            return dt;
        }

        public DataSet RetrieveSiteById(decimal dSiteId)
        {
            DataSet ds = new DataSet();

            using (SqlDataAdapter da = new SqlDataAdapter())
            {
                da.SelectCommand = new SqlCommand("SPN_XP_GET_SITE_BY_ID");
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@IN_SITE_ID", SqlDbType.Decimal).Value = dSiteId;
                da.SelectCommand.Connection = ProfitCashflow.oPcfDM.conn;
                da.Fill(ds);
            }

            return ds;
        }

        public int RetrievePcfDocuments(decimal dDocGuid)
        {
            DataTable dt = new DataTable();
            int cnt = 0;
            using (SqlDataAdapter da = new SqlDataAdapter())
            {
                da.SelectCommand = new SqlCommand("SPN_XP_GET_PCF_DOCUMENTS");
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@IN_PCF_DOC_GUID", SqlDbType.Decimal).Value = dDocGuid;
                da.SelectCommand.Connection = ProfitCashflow.oPcfDM.conn;
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    cnt = Convert.ToInt32(dt.Rows[0]["CNT"]);
                }
            }

            return cnt;
        }

        public string RetrieveSystemCreatedStack(string siteCode, decimal companyId, string userName, decimal pcfDocGuid,
                                                decimal stackId, SqlDataAdapter da)
        {
            string errMsg = string.Empty;
            DataTable dt = new DataTable();
            da.SelectCommand = new SqlCommand("SPN_XP_SYSTEM_CREATED_STACK");
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@LC_NEW_SITE_CODE", siteCode);
            da.SelectCommand.Parameters.AddWithValue("@LC_SITE_COMP_ID", companyId);
            da.SelectCommand.Parameters.AddWithValue("@LC_USERNAME", userName);
            da.SelectCommand.Parameters.AddWithValue("@LI_PCF_DOC_GUID", pcfDocGuid);
            da.SelectCommand.Parameters.AddWithValue("@LI_STACK_ID", stackId);
            da.SelectCommand.Connection = ProfitCashflow.oPcfDM.conn;

            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                errMsg = dt.Rows[0][0] != null ? dt.Rows[0][0].ToString() : string.Empty;
            }

            ////Add the output parameter to the command object
            //SqlParameter outPutParameter = new SqlParameter();
            //outPutParameter.ParameterName = "@LC_ERR_MSG";
            //outPutParameter.Size = 500;
            //outPutParameter.SqlDbType = System.Data.SqlDbType.VarChar;
            //outPutParameter.Direction = System.Data.ParameterDirection.Output;
            //da.SelectCommand.Parameters.Add(outPutParameter);
            //da.SelectCommand.Connection = ProfitCashflow.oPcfDM.conn;

            return errMsg;
        }

        public int RetrieveMaxSeqId(string param)
        {
            int oResult = 0;
            using (SqlCommand cmd = new SqlCommand("SPN_GET_SEQ_ID", ProfitCashflow.oPcfDM.conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IC_SEQ", param);

                //Add the output parameter to the command object
                SqlParameter outPutParameter = new SqlParameter();
                outPutParameter.ParameterName = "@OP_VAL";
                outPutParameter.SqlDbType = System.Data.SqlDbType.Int;
                outPutParameter.Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters.Add(outPutParameter);
                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                cmd.ExecuteScalar();
                if (outPutParameter.Value != DBNull.Value)
                    oResult = (int)outPutParameter.Value;
            }

            return oResult;
        }

        public int RetrieveMaxPlot(decimal dSiteId)
        {
            int maxPlot = 0;
            DataTable dt = new DataTable();
            using (SqlDataAdapter da = new SqlDataAdapter())
            {
                da.SelectCommand = new SqlCommand("SPN_XP_GET_MAX_PLOT");
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@IN_SITE_ID", SqlDbType.Decimal).Value = dSiteId;
                da.SelectCommand.Connection = ProfitCashflow.oPcfDM.conn;
                da.Fill(dt);
                if (dt.Rows.Count > 0 && dt.Rows[0]["MAX_PLOT"] != DBNull.Value)
                {
                    maxPlot = Convert.ToInt32(dt.Rows[0]["MAX_PLOT"]);
                }
            }

            return maxPlot;
        }

        public int CheckSite(decimal dCompanyId, string sSiteCode)
        {
            int cnt = 0;
            DataTable dt = new DataTable();
            using (SqlDataAdapter da = new SqlDataAdapter())
            {
                da.SelectCommand = new SqlCommand("SPN_XP_CHECK_SITE");
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@IN_COMPANY_ID", SqlDbType.Decimal).Value = dCompanyId;
                da.SelectCommand.Parameters.Add("@IV_SITE_CODE", SqlDbType.VarChar).Value = sSiteCode;
                da.SelectCommand.Connection = ProfitCashflow.oPcfDM.conn;
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    cnt = Convert.ToInt32(dt.Rows[0]["CNT"]);
                }
            }

            return cnt;
        }

        public DataTable CheckSiteAborted(decimal dCompanyId, string sSiteId)
        {
            DataTable dt = new DataTable();

            using (SqlDataAdapter da = new SqlDataAdapter())
            {
                da.SelectCommand = new SqlCommand("SPN_XP_CHECK_SITE_ABORTED");
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@IN_COMPANY_ID", SqlDbType.Decimal).Value = dCompanyId;
                da.SelectCommand.Parameters.Add("@IV_SITE_CODE", SqlDbType.VarChar).Value = sSiteId;

                da.SelectCommand.Connection = ProfitCashflow.oPcfDM.conn;
                da.Fill(dt);
            }

            return dt;
        }

        public string UpdateSiteSetup(string sType, DataTable bindingSiteDt, DataTable bindingSitePlotDt,
                                        DataTable bindingSiteDocDt, decimal pcfDocGuid, decimal stackId)
        {
            string result = string.Empty;
            TransactionOptions to = new TransactionOptions();
            to.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;

            ProfitCashflow.oPcfDM.conn.Close();
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew, to, EnterpriseServicesInteropOption.Full))
            {
                SqlDataAdapter da = new SqlDataAdapter();

                try
                {
                    if (bindingSiteDt.GetChanges() != null)
                    {
                        if (sType == "I")
                        {
                            da.InsertCommand = new SqlCommand("SPN_XP_INS_SITE_SETUP", ProfitCashflow.oPcfDM.conn);
                            da.InsertCommand.CommandType = CommandType.StoredProcedure;
                            CreateSiteCommand(da.InsertCommand);
                        }
                        else if (sType == "U")
                        {
                            da.UpdateCommand = new SqlCommand("SPN_XP_UPD_SITE_SETUP", ProfitCashflow.oPcfDM.conn);
                            da.UpdateCommand.CommandType = CommandType.StoredProcedure;
                            CreateSiteCommand(da.UpdateCommand);
                        }

                        da.Update(bindingSiteDt.GetChanges());
                    }

                    if (bindingSitePlotDt.GetChanges() != null)
                        UpdateSitePlot(bindingSitePlotDt.GetChanges(), da);

                    if (bindingSiteDocDt.GetChanges() !=null)
                        UpdateSiteDoc(bindingSiteDocDt.GetChanges(), da);

                    if (sType == "I")
                    {
                        result = RetrieveSystemCreatedStack(bindingSiteDt.Rows[0]["SITE_CODE"].ToString(),
                                                              Convert.ToDecimal(bindingSiteDt.Rows[0]["COMPANY_ID"]),
                                                              ProfitCashflow.oPcfDM.UserName,
                                                              pcfDocGuid,
                                                              stackId, da);

                        if (!string.IsNullOrEmpty(result))
                        {
                            ts.Dispose();
                            return result;
                        }
                    }
                    ts.Complete();
                }
                catch (Exception ex)
                {
                    ts.Dispose();
                    throw ex;
                }
                finally
                {
                    if (da != null) da.Dispose();
                }
            }
            ProfitCashflow.oPcfDM.conn.Open();

            return result;
        }

        public void UpdateSiteDoc(DataTable dt, SqlDataAdapter da)
        {
            try
            {
                da.InsertCommand = new SqlCommand("SPN_XP_INS_SITE_DOCUMENT", ProfitCashflow.oPcfDM.conn);
                da.InsertCommand.CommandType = CommandType.StoredProcedure;
                CreateSiteDocCommand(da.InsertCommand);

                da.UpdateCommand = new SqlCommand("SPN_XP_UPD_SITE_DOCUMENT", ProfitCashflow.oPcfDM.conn);
                da.UpdateCommand.CommandType = CommandType.StoredProcedure;
                CreateSiteDocCommand(da.UpdateCommand);

                da.DeleteCommand = new SqlCommand("SPN_XP_DEL_SITE_DOCUMENT", ProfitCashflow.oPcfDM.conn);
                da.DeleteCommand.CommandType = CommandType.StoredProcedure;
                da.DeleteCommand.Parameters.Add(CreateSqlParameter("@IN_SITE_ID", SqlDbType.Decimal, 10, "SITE_ID", System.Data.ParameterDirection.Input, DataRowVersion.Current));
                da.DeleteCommand.Parameters.Add(CreateSqlParameter("@IN_DOCUMENT_ID", SqlDbType.Decimal, 10, "DOCUMENT_ID", System.Data.ParameterDirection.Input, DataRowVersion.Current));

                da.Update(dt);
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

        public void UpdateSitePlot(DataTable dt, SqlDataAdapter da)
        {
            try
            {
                da.InsertCommand = new SqlCommand("SPN_XP_INS_SITE_PLOT", ProfitCashflow.oPcfDM.conn);
                da.InsertCommand.CommandType = CommandType.StoredProcedure;
                CreateSitePlotCommand(da.InsertCommand);

                da.UpdateCommand = new SqlCommand("SPN_XP_UPD_SITE_PLOT", ProfitCashflow.oPcfDM.conn);
                da.UpdateCommand.CommandType = CommandType.StoredProcedure;
                CreateSitePlotCommand(da.UpdateCommand);

                da.DeleteCommand = new SqlCommand("SPN_XP_DEL_SITE_PLOT", ProfitCashflow.oPcfDM.conn);
                da.DeleteCommand.CommandType = CommandType.StoredProcedure;
                CreateSitePlotDelCommand(da.DeleteCommand);

                da.Update(dt);
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

        public void UpdateCurrentSeedValue(decimal companyId, decimal currentValue)
        {
            TransactionOptions to = new TransactionOptions();
            to.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;

            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew, to, EnterpriseServicesInteropOption.Full))
            {
                SqlCommand cmd = new SqlCommand("SPN_XP_UPD_CURRENT_SEED_VALUE", ProfitCashflow.oPcfDM.conn);
                try
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IN_COMPANY_ID", companyId);
                    cmd.Parameters.AddWithValue("@IN_CURR_VAL", currentValue);

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

        public void UpdateSalesReleaseStatus(decimal siteId)
        {
            TransactionOptions to = new TransactionOptions();
            to.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;

            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew, to, EnterpriseServicesInteropOption.Full))
            {
                SqlCommand cmd = new SqlCommand("SPN_XP_UPD_SALES_RELEASE_STATUS", ProfitCashflow.oPcfDM.conn);
                try
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IN_SITE_ID", siteId);

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

        public void DeleteSiteSetup(decimal companyId, decimal siteId, string siteCode, string sysCreatedStack)
        {
            TransactionOptions to = new TransactionOptions();
            to.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew, to, EnterpriseServicesInteropOption.Full))
            {
                SqlCommand cmd = new SqlCommand("SPN_XP_DEL_SITE_SETUP", ProfitCashflow.oPcfDM.conn);
                try
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IN_COMPANY_ID", companyId);
                    cmd.Parameters.AddWithValue("@IN_SITE_ID", siteId);
                    cmd.Parameters.AddWithValue("@IV_SITE_CODE", siteCode);
                    cmd.Parameters.AddWithValue("@IC_SYS_CREATED_STACK", sysCreatedStack);

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

        private void CreateSiteCommand(SqlCommand cmd)
        {
            cmd.Parameters.Add(CreateSqlParameter("@IN_SITE_ID", SqlDbType.Decimal, 10, "SITE_ID", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IN_COMPANY_ID", SqlDbType.Decimal, 10, "COMPANY_ID", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_SITE_CODE", SqlDbType.VarChar, 30, "SITE_CODE", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_SITE_NAME", SqlDbType.VarChar, 100, "SITE_NAME", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_ADDR_L1", SqlDbType.VarChar, 100, "ADDR_L1", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_ADDR_L2", SqlDbType.VarChar, 100, "ADDR_L2", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_ADDR_L3", SqlDbType.VarChar, 100, "ADDR_L3", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_CITY", SqlDbType.VarChar, 100, "CITY", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_ZIP_POST_CODE", SqlDbType.VarChar, 30, "ZIP_POST_CODE", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_SITE_SHORT_NAME", SqlDbType.VarChar, 30, "SITE_SHORT_NAME", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_MKTG_NAME", SqlDbType.VarChar, 100, "MKTG_NAME", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IC_STATUS", SqlDbType.Char, 2, "STATUS", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_JV_EXTERNAL", SqlDbType.VarChar, 100, "JV_EXTERNAL", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IN_JV_MNGT_FEE_PCT", SqlDbType.Decimal, 15, "JV_MNGT_FEE_PCT", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IC_LAND_TYPE", SqlDbType.Char, 1, "LAND_TYPE", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IC_BUILD_TYPE", SqlDbType.Char, 1, "BUILD_TYPE", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_COUNTRY_CODE", SqlDbType.VarChar, 30, "COUNTRY_CODE", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_LOCATION_CODE", SqlDbType.VarChar, 30, "LOCATION_CODE", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_AREA", SqlDbType.VarChar, 2, "AREA", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IC_BLDG_UNDER_LICENSE", SqlDbType.Char, 1, "BLDG_UNDER_LICENSE", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_LAND_TITLE_REF", SqlDbType.VarChar, 100, "LAND_TITLE_REF", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_LAND_BUYER", SqlDbType.VarChar, 100, "LAND_BUYER", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_SITE_SIZE", SqlDbType.VarChar, 100, "SITE_SIZE", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_DESCRIPTION", SqlDbType.VarChar, 255, "DESCRIPTION", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_VENDOR", SqlDbType.VarChar, 100, "VENDOR", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IN_PURCHASE_PRICE", SqlDbType.VarChar, -1, "PURCHASE_PRICE", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_PAYMENT_TIMING", SqlDbType.VarChar, 100, "PAYMENT_TIMING", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IC_SITE_ABORTED", SqlDbType.Char, 1, "SITE_ABORTED", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@ID_ABORTED_DATE", SqlDbType.DateTime, -1, "ABORTED_DATE", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_ABORTED_COMMENTS", SqlDbType.VarChar, 255, "ABORTED_COMMENTS", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IC_SITE_COMPLETED", SqlDbType.Char, 1, "SITE_COMPLETED", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@ID_COMPLETED_DATE", SqlDbType.DateTime, -1, "COMPLETED_DATE", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_COMPLETED_COMMENTS", SqlDbType.VarChar, 255, "COMPLETED_COMMENTS", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_CREATED_BY", SqlDbType.VarChar, 30, "CREATED_BY", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@ID_CREATED_ON", SqlDbType.DateTime, -1, "CREATED_ON", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@ID_LAST_UPDATED", SqlDbType.DateTime, -1, "LAST_UPDATED", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IN_PURCHASE_TYPE_ID", SqlDbType.Decimal, -1, "PURCHASE_TYPE_ID", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IN_PCF_DOC_GUID", SqlDbType.Decimal, 20, "PCF_DOC_GUID", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_PURCHASER", SqlDbType.VarChar, 255, "PURCHASER", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@ID_DATE_OF_SALE", SqlDbType.DateTime, -1, "DATE_OF_SALE", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IC_STACK_STATUS", SqlDbType.Char, 1, "STACK_STATUS", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IN_JV_COMPANY_ID", SqlDbType.Decimal, 10, "JV_COMPANY_ID", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IC_SALES_RELEASE_STATUS", SqlDbType.Char, 1, "SALES_RELEASE_STATUS", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@ID_SALES_RELEASE_DATE", SqlDbType.DateTime, -1, "SALES_RELEASE_DATE", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IC_XPEDEON_RELEASE_STATUS", SqlDbType.Char, 1, "XPEDEON_RELEASE_STATUS", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@ID_XPEDEON_RELEASE_DATE", SqlDbType.DateTime, -1, "XPEDEON_RELEASE_DATE", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IC_CODA_RELEASE_STATUS", SqlDbType.Char, 1, "CODA_RELEASE_STATUS", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@ID_CODA_RELEASE_DATE", SqlDbType.DateTime, -1, "CODA_RELEASE_DATE", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IC_WBS_RELEASE_STATUS", SqlDbType.Char, 1, "WBS_RELEASE_STATUS", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@ID_WBS_RELEASE_DATE", SqlDbType.DateTime, -1, "WBS_RELEASE_DATE", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IN_LAND_TYPE_ID", SqlDbType.Decimal, 10, "LAND_TYPE_ID", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IN_BUILD_TYPE_ID", SqlDbType.Decimal, 10, "BUILD_TYPE_ID", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_MAX_PLOT_NO", SqlDbType.VarChar, 20, "MAX_PLOT_NO", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_LAND_STATUS", SqlDbType.VarChar, 3, "LAND_STATUS", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@ID_B4_DATE", SqlDbType.DateTime, -1, "B4_DATE", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@ID_B3_DATE", SqlDbType.DateTime, -1, "B3_DATE", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@ID_B2_DATE", SqlDbType.DateTime, -1, "B2_DATE", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@ID_B1_DATE", SqlDbType.DateTime, -1, "B1_DATE", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@ID_ATL_DATE", SqlDbType.DateTime, -1, "ATL_DATE", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IN_SYS_CREATED_STACK_ID", SqlDbType.Decimal, 10, "SYS_CREATED_STACK_ID", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IN_CURRENT_STACK_ID", SqlDbType.Decimal, 10, "CURRENT_STACK_ID", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IN_MODE", SqlDbType.Decimal, 1, "MODE", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IC_LAND_SALE", SqlDbType.Char, 1, "LAND_SALE", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_LAND_SALE_COMMENTS", SqlDbType.VarChar, 255, "LAND_SALE_COMMENTS", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IC_USE_IN_REPORT", SqlDbType.Char, 1, "USE_IN_REPORT", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IC_PART_EXCHANGE", SqlDbType.Char, 1, "PART_EXCHANGE", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IN_DEVELOPMENT_ID", SqlDbType.Decimal, 10, "DEVELOPMENT_ID", System.Data.ParameterDirection.Input, DataRowVersion.Current));
        }

        private void CreateSiteDocCommand(SqlCommand cmd)
        {
            cmd.Parameters.Add(CreateSqlParameter("@IN_SITE_ID", SqlDbType.Decimal, 10, "SITE_ID", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IN_DOCUMENT_ID", SqlDbType.Decimal, 10, "DOCUMENT_ID", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_DOCUMENT_NAME", SqlDbType.VarChar, 255, "DOCUMENT_NAME", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_DESCRIPTION", SqlDbType.VarChar, 255, "DESCRIPTION", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_DOC_HYPER_LINK", SqlDbType.VarChar, 255, "DOC_HYPER_LINK", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_CREATED_BY", SqlDbType.VarChar, 30, "CREATED_BY", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@ID_CREATED_ON", SqlDbType.DateTime, -1, "CREATED_ON", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@ID_LAST_UPDATED", SqlDbType.DateTime, -1, "LAST_UPDATED", System.Data.ParameterDirection.Input, DataRowVersion.Current));
        }

        private void CreateSitePlotCommand(SqlCommand cmd)
        {
            cmd.Parameters.Add(CreateSqlParameter("@IN_SITE_ID", SqlDbType.Decimal, 10, "SITE_ID", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@ID_PLOT_NO_FROM", SqlDbType.Int, -1, "PLOT_NO_FROM", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@ID_PLOT_NO_TO", SqlDbType.Int, -1, "PLOT_NO_TO", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_CREATED_BY", SqlDbType.VarChar, 30, "CREATED_BY", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@ID_CREATED_ON", SqlDbType.DateTime, -1, "CREATED_ON", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@ID_LAST_UPDATED", SqlDbType.DateTime, -1, "LAST_UPDATED", System.Data.ParameterDirection.Input, DataRowVersion.Current));
        }

        private void CreateSitePlotDelCommand(SqlCommand cmd)
        {
            cmd.Parameters.Add(CreateSqlParameter("@IN_SITE_ID", SqlDbType.Decimal, 10, "SITE_ID", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@ID_PLOT_NO_FROM", SqlDbType.Int, -1, "PLOT_NO_FROM", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@ID_PLOT_NO_TO", SqlDbType.Int, -1, "PLOT_NO_TO", System.Data.ParameterDirection.Input, DataRowVersion.Current));
        }

        private SqlParameter CreateSqlParameter(string parameterName,
        SqlDbType dataType, int size,
        string srcColumn, System.Data.ParameterDirection direction,
        System.Data.DataRowVersion srcVersion)
        {
            SqlParameter myParameter = new SqlParameter();
            myParameter.ParameterName = parameterName;
            myParameter.SqlDbType = dataType;
            myParameter.Size = size;
            myParameter.SourceColumn = srcColumn;
            myParameter.Direction = direction;
            myParameter.SourceVersion = srcVersion;

            return myParameter;
        }
    }
}
