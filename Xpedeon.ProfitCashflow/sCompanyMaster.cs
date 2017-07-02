using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Transactions;

namespace Xpedeon.ProfitCashflow
{
    class sCompanyMaster
    {
        public DataTable RetrieveCountryMaster()
        {
            SqlDataAdapter da = null;
            DataTable dtCountryMast = new DataTable();

            try
            {
                da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("SPN_XP_GET_CM_COUNTRY_MAST", ProfitCashflow.oPcfDM.conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;

                da.Fill(dtCountryMast);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (da != null) da.Dispose();
            }

            return dtCountryMast;
        }

        public DataTable RetrieveNonJVCompanys(string sUserName)
        {
            SqlDataAdapter da = null;
            DataTable dtNonJVComp = new DataTable();

            try
            {
                da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("SPN_XP_GET_CM_NON_JV_COMP", ProfitCashflow.oPcfDM.conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@IV_PCF_USER", sUserName);

                da.Fill(dtNonJVComp);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (da != null) da.Dispose();
            }

            return dtNonJVComp;
        }

        public DataTable RetrieveJVOperatingCompany(decimal dCompanyId, string sUserName)
        {
            SqlDataAdapter da = null;
            DataTable dtJVOpComp = new DataTable();

            try
            {
                da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("SPN_XP_GET_CM_JV_OPCOMP", ProfitCashflow.oPcfDM.conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
				da.SelectCommand.Parameters.AddWithValue("@IN_COMPANY_ID", dCompanyId);
                da.SelectCommand.Parameters.AddWithValue("@IV_PCF_USER", sUserName);

                da.Fill(dtJVOpComp);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (da != null) da.Dispose();
            }

            return dtJVOpComp;
        }

        public DataTable RetrieveCompanyForUser(string sUserName)
        {
            SqlDataAdapter da = null;
            DataTable dtComp4User = new DataTable();

            try
            {
                da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("SPN_XP_GET_CM_COMP_4_USER", ProfitCashflow.oPcfDM.conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@IV_PCF_USER", sUserName);

                da.Fill(dtComp4User);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (da != null) da.Dispose();
            }

            return dtComp4User;
        }

        public DataTable RetrieveAccPeriodForComp(decimal dCompanyId, DateTime dteStartDate)
        {
            SqlDataAdapter da = null;
            DataTable dtAccPeriod = new DataTable();

            try
            {
                da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("SPN_XP_GET_CM_ACC_PERIOD", ProfitCashflow.oPcfDM.conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@IN_COMPANY_ID", dCompanyId);
				da.SelectCommand.Parameters.AddWithValue("@ID_START_DATE", dteStartDate);

                da.Fill(dtAccPeriod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (da != null) da.Dispose();
            }

            return dtAccPeriod;
        }
		
		public void UpdateCompanyCurrentSeed(decimal dCompanyId)
        {
			TransactionOptions to = new TransactionOptions();
            to.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew, to, EnterpriseServicesInteropOption.Full))
            {
                SqlCommand cmd = null;
				try
				{
					cmd = new SqlCommand("UPDATE COMPANY_MASTER SET CURRENT_SEED_VALUE = NULL WHERE COMPANY_ID = @IN_COMPANY_ID", ProfitCashflow.oPcfDM.conn);
					cmd.CommandType = CommandType.Text;
					cmd.Parameters.AddWithValue("@IN_COMPANY_ID", dCompanyId);

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
		
		public object RetrieveCompanyInitialSeed(decimal dCompanyId, string sPrefix, decimal dSeedVal)
        {
			object oResult = new object();
            SqlCommand cmd = null;
			
			try
			{
				cmd = new SqlCommand("SPN_XP_GET_CM_COMP_INITIAL_SEED", ProfitCashflow.oPcfDM.conn);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@IN_COMPANY_ID", dCompanyId);
				cmd.Parameters.AddWithValue("@IC_PREFIX", sPrefix);
				cmd.Parameters.AddWithValue("@IN_SEED_VALUE", dSeedVal);

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
		
		public object RetrieveCheckCompanyUsage(decimal dCompanyId)
        {
			object oResult = new object();
            SqlCommand cmd = null;
			
			try
			{
				cmd = new SqlCommand("SPN_XP_GET_CM_CHK_COMPANY", ProfitCashflow.oPcfDM.conn);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@IN_COMPANY_ID", dCompanyId);

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
		
		public object RetrieveSequenceIdForCompany(string sSeqName)
        {
			object oResult = new object();
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
                oResult = outVal.Value;
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
    
		public object RetrieveDuplicateCompanyCount(string sCompanyCode)
        {
			object oResult = new object();
            SqlCommand cmd = null;
			
			try
			{
				cmd = new SqlCommand("SELECT COUNT(1) CNT FROM COMPANY_MASTER WHERE COMPANY_CODE = @IV_COMPANY_CODE", ProfitCashflow.oPcfDM.conn);
				cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@IV_COMPANY_CODE", sCompanyCode);

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
		
		public void UpdateJVOperatingCompany(decimal dCompanyId)
        {
			TransactionOptions to = new TransactionOptions();
            to.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew, to, EnterpriseServicesInteropOption.Full))
            {
                SqlCommand cmd = null;
				try
				{
					cmd = new SqlCommand("UPDATE SITE_SETUP SET JV_COMPANY_ID = NULL, JV_MNGT_FEE_PCT = NULL WHERE COMPANY_ID = @IN_COMPANY_ID");
					cmd.Connection = ProfitCashflow.oPcfDM.conn;
					cmd.CommandType = CommandType.Text;
					cmd.Parameters.AddWithValue("@IN_COMPANY_ID", dCompanyId);

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
		
		public void UpdateJVOperatingCompany(decimal dCompanyId, decimal dOpCompanyId)
        {
			TransactionOptions to = new TransactionOptions();
            to.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew, to, EnterpriseServicesInteropOption.Full))
            {
                SqlCommand cmd = null;
				try
				{
					cmd = new SqlCommand("UPDATE SITE_SETUP SET JV_COMPANY_ID = @IN_OP_COMPANY_ID WHERE COMPANY_ID = @IN_COMPANY_ID");
					cmd.Connection = ProfitCashflow.oPcfDM.conn;
					cmd.CommandType = CommandType.Text;
					
					cmd.Parameters.AddWithValue("@IN_COMPANY_ID", dCompanyId);
					cmd.Parameters.AddWithValue("@IN_OP_COMPANY_ID", dOpCompanyId);

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

		public object RetrieveSiteSetupCount4Company(decimal dCompanyId)
        {
			object oResult = new object();
            SqlCommand cmd = null;
			
			try
			{
				cmd = new SqlCommand("SPN_XP_GET_CM_SITE_SETUP_CNT", ProfitCashflow.oPcfDM.conn);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@IN_COMPANY_ID", dCompanyId);

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

        public DataTable UpdateCompanyMaster(DataTable dtCompMast)
        {
            TransactionOptions to = new TransactionOptions();
            to.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew, to, EnterpriseServicesInteropOption.Full))
            {
                SqlDataAdapter da = new SqlDataAdapter();
                try
                {
                    DataRow[] drAdded = dtCompMast.Select("", "", DataViewRowState.Added);
                    if (drAdded.Length > 0)
                    {
                        object oResult = RetrieveSequenceIdForCompany("SEQ_PK_COMPANY_MASTER");
                        if (oResult != null)
                            foreach (DataRow drTemp in drAdded)
                                drTemp["COMPANY_ID"] = Convert.ToDecimal(oResult);
                    }

                    da.InsertCommand = new SqlCommand("SPN_XP_INS_CM_COMPANY_MAST", ProfitCashflow.oPcfDM.conn);
                    da.InsertCommand.CommandType = CommandType.StoredProcedure;
                    CreateCompanyMasterCommand(da.InsertCommand);

                    da.UpdateCommand = new SqlCommand("SPN_XP_UPD_CM_COMPANY_MAST", ProfitCashflow.oPcfDM.conn);
                    da.UpdateCommand.CommandType = CommandType.StoredProcedure;
                    CreateCompanyMasterCommand(da.UpdateCommand);

                    da.DeleteCommand = new SqlCommand("SPN_XP_DEL_CM_COMPANY_MAST", ProfitCashflow.oPcfDM.conn);
                    da.DeleteCommand.CommandType = CommandType.StoredProcedure;
                    da.DeleteCommand.Parameters.Add(CreateSqlParameter("@IN_COMPANY_ID", SqlDbType.Decimal, 10, "COMPANY_ID", System.Data.ParameterDirection.Input, DataRowVersion.Current));

                    da.Update(dtCompMast);
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
            return dtCompMast;
        }

        private void CreateCompanyMasterCommand(SqlCommand cmd)
        {
            cmd.Parameters.Add(CreateSqlParameter("@IN_COMPANY_ID", SqlDbType.Decimal, 10, "COMPANY_ID", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_NAME", SqlDbType.VarChar, 100, "NAME", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IB_LOGO", SqlDbType.VarBinary, -1, "LOGO", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_ADDR_LINE1", SqlDbType.VarChar, 1000, "ADDR_LINE1", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_ADDR_LINE2", SqlDbType.VarChar, 1000, "ADDR_LINE2", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_CITY", SqlDbType.VarChar, 100, "CITY", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_STATE_CODE", SqlDbType.VarChar, 30, "STATE_CODE", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_STATE_PROVINCE", SqlDbType.VarChar, 100, "STATE_PROVINCE", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_ZIP_POST_CODE", SqlDbType.VarChar, 30, "ZIP_POST_CODE", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_COUNTRY_CODE", SqlDbType.VarChar, 30, "COUNTRY_CODE", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_TEL_NO1", SqlDbType.VarChar, 100, "TEL_NO1", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_TEL_NO2", SqlDbType.VarChar, 100, "TEL_NO2", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_TEL_NO3", SqlDbType.VarChar, 100, "TEL_NO3", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_FAX", SqlDbType.VarChar, 100, "FAX", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_EMAIL", SqlDbType.VarChar, 100, "EMAIL", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_MOBILE", SqlDbType.VarChar, 100, "MOBILE", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IC_STATE", SqlDbType.Char, 1, "STATE", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_COMPANY_CODE", SqlDbType.VarChar, 30, "COMPANY_CODE", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IC_DEFAULT_LOGO", SqlDbType.Char, 1, "DEFAULT_LOGO", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_APPR_REGION_CODE", SqlDbType.VarChar, 30, "APPR_REGION_CODE", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_DOC_CURRENCY_CODE", SqlDbType.VarChar, 3, "DOC_CURRENCY_CODE", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_UTR", SqlDbType.VarChar, 50, "UTR", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_ACCOUNTS_OFFICE_REF_NO", SqlDbType.VarChar, 50, "ACCOUNTS_OFFICE_REF_NO", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_CIS_SENDER_ID", SqlDbType.VarChar, 30, "CIS_SENDER_ID", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_CIS_AUTHENTICATION_PASSWORD", SqlDbType.VarChar, 30, "CIS_AUTHENTICATION_PASSWORD", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IN_CIS_TAX_OFFICE_NO", SqlDbType.Decimal, 3, "CIS_TAX_OFFICE_NO", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_CIS_TAX_OFFICE_REFERENCE", SqlDbType.VarChar, 30, "CIS_TAX_OFFICE_REFERENCE", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_CIS_URI", SqlDbType.VarChar, 10, "CIS_URI", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_EMPLOYERS_REFERENCE", SqlDbType.VarChar, 10, "EMPLOYERS_REFERENCE", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IC_IS_ACTIVE", SqlDbType.Char, 1, "IS_ACTIVE", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_CREATED_BY", SqlDbType.VarChar, 30, "CREATED_BY", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@ID_CREATED_ON", SqlDbType.DateTime, -1, "CREATED_ON", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@ID_LAST_UPDATED", SqlDbType.DateTime, -1, "LAST_UPDATED", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IC_JV_COMPANY", SqlDbType.Char, 1, "JV_COMPANY", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IC_SITE_CODE_PREFIX", SqlDbType.Char, 2, "SITE_CODE_PREFIX", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IN_INITIAL_SEED_VALUE", SqlDbType.Decimal, 4, "INITIAL_SEED_VALUE", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IN_CURRENT_SEED_VALUE", SqlDbType.Decimal, 4, "CURRENT_SEED_VALUE", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IN_OP_COMPANY_ID", SqlDbType.Decimal, 10, "OP_COMPANY_ID", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IN_JV_COMPANY_BG_PCT", SqlDbType.Decimal, 15, "JV_COMPANY_BG_PCT", System.Data.ParameterDirection.Input, DataRowVersion.Current));
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
