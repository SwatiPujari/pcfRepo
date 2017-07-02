using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Transactions;

namespace Xpedeon.ProfitCashflow
{
    class sDevelopmentMaster
    {
        public object RetrieveDuplicateDevCount(string sDevCode)
        {
            object oResult = new object();
            SqlCommand cmd = null;

            try
            {
                cmd = new SqlCommand("SELECT COUNT(1) CNT FROM DEVELOPMENT_MASTER WHERE DEVELOPMENT_CODE = @IV_DEV_CODE", ProfitCashflow.oPcfDM.conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@IV_DEV_CODE", sDevCode);

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
		
        public DataTable RetrieveSiteList()
        {
            SqlDataAdapter da = null;
            DataTable dtSiteList = new DataTable();

            try
            {
                da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("SPN_XP_GET_SITE_LIST_4_DEV_MAST", ProfitCashflow.oPcfDM.conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
				
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

        public DataTable RetrieveDevelopmentMaster()
        {
            SqlDataAdapter da = null;
            DataTable dtDevMaster = new DataTable();

            try
            {
                da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("SPN_XP_GET_DEVELOPMENT_MASTER", ProfitCashflow.oPcfDM.conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;

                da.Fill(dtDevMaster);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (da != null) da.Dispose();
            }

            return dtDevMaster;
        }

        public DataTable UpdateDevelopmentMaster(DataTable dtDevMaster)
        {
			TransactionOptions to = new TransactionOptions();
            to.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew, to, EnterpriseServicesInteropOption.Full))
            {
				SqlDataAdapter da = new SqlDataAdapter();
				try
				{
					da.InsertCommand = new SqlCommand("SPN_XP_INS_DEVELOPMENT_MASTER", ProfitCashflow.oPcfDM.conn);
					da.InsertCommand.CommandType = CommandType.StoredProcedure;
					CreateDevelopmentMasterCommand(da.InsertCommand);

					da.UpdateCommand = new SqlCommand("SPN_XP_UPD_DEVELOPMENT_MASTER", ProfitCashflow.oPcfDM.conn);
					da.UpdateCommand.CommandType = CommandType.StoredProcedure;
					CreateDevelopmentMasterCommand(da.UpdateCommand);

					da.Update(dtDevMaster);
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
            return dtDevMaster;
        }

        private void CreateDevelopmentMasterCommand(SqlCommand cmd)
        {
            cmd.Parameters.Add(CreateSqlParameter("@IN_DEVELOPMENT_ID", SqlDbType.Decimal, 10, "DEVELOPMENT_ID", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_DEVELOPMENT_CODE", SqlDbType.VarChar, 30, "DEVELOPMENT_CODE", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_DEVELOPMENT_NAME", SqlDbType.VarChar, 100, "DEVELOPMENT_NAME", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IC_IS_ACTIVE", SqlDbType.Char, 1, "IS_ACTIVE", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_CREATED_BY", SqlDbType.VarChar, 30, "CREATED_BY", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@ID_CREATED_ON", SqlDbType.DateTime, -1, "CREATED_ON", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@ID_LAST_UPDATED", SqlDbType.DateTime, -1, "LAST_UPDATED", System.Data.ParameterDirection.Input, DataRowVersion.Current));
        }

        public void UpdateDevelopmentAndSiteLinkage(DataTable dtLinkage)
        {
			TransactionOptions to = new TransactionOptions();
            to.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew, to, EnterpriseServicesInteropOption.Full))
            {
				SqlDataAdapter da = new SqlDataAdapter();
				try
				{
					da.UpdateCommand = new SqlCommand("SPN_XP_UPD_DEV_MAST_SITE_LINK", ProfitCashflow.oPcfDM.conn);
					da.UpdateCommand.CommandType = CommandType.StoredProcedure;
					da.UpdateCommand.Parameters.Add(CreateSqlParameter("@IN_DEVELOPMENT_ID", SqlDbType.Decimal, 10, "DEVELOPMENT_ID", System.Data.ParameterDirection.Input, DataRowVersion.Current));
					da.UpdateCommand.Parameters.Add(CreateSqlParameter("@IN_SITE_ID", SqlDbType.Decimal, 10, "SITE_ID", System.Data.ParameterDirection.Input, DataRowVersion.Current));

					da.Update(dtLinkage);
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
