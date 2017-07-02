using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Transactions;

namespace Xpedeon.ProfitCashflow
{
    class sGroupMaster
    {
        public object RetrieveDuplicateGroupCount(string sGroupCode)
        {
            object oResult = new object();
            SqlCommand cmd = null;

            try
            {
                cmd = new SqlCommand("SELECT COUNT(1) CNT FROM GROUP_MASTER WHERE GROUP_CODE = @IV_GROUP_CODE", ProfitCashflow.oPcfDM.conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@IV_GROUP_CODE", sGroupCode);

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
		
        public DataTable RetrieveDivisionList()
        {
            SqlDataAdapter da = null;
            DataTable dtDivisionList = new DataTable();

            try
            {
                da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("SPN_XP_GET_DIV_LIST_4_GRUP_MAST", ProfitCashflow.oPcfDM.conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
				
                da.Fill(dtDivisionList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (da != null) da.Dispose();
            }

            return dtDivisionList;
        }

        public DataTable RetrieveGroupMaster()
        {
            SqlDataAdapter da = null;
            DataTable dtGroupMaster = new DataTable();

            try
            {
                da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("SPN_XP_GET_GROUP_MASTER", ProfitCashflow.oPcfDM.conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;

                da.Fill(dtGroupMaster);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (da != null) da.Dispose();
            }

            return dtGroupMaster;
        }

        public DataTable UpdateGroupMaster(DataTable dtGroupMaster)
        {
			TransactionOptions to = new TransactionOptions();
            to.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew, to, EnterpriseServicesInteropOption.Full))
            {
				SqlDataAdapter da = new SqlDataAdapter();
				try
				{
					da.InsertCommand = new SqlCommand("SPN_XP_INS_GROUP_MASTER", ProfitCashflow.oPcfDM.conn);
					da.InsertCommand.CommandType = CommandType.StoredProcedure;
					CreateGroupMasterCommand(da.InsertCommand);

					da.UpdateCommand = new SqlCommand("SPN_XP_UPD_GROUP_MASTER", ProfitCashflow.oPcfDM.conn);
					da.UpdateCommand.CommandType = CommandType.StoredProcedure;
					CreateGroupMasterCommand(da.UpdateCommand);

					da.Update(dtGroupMaster);
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
            return dtGroupMaster;
        }

        private void CreateGroupMasterCommand(SqlCommand cmd)
        {
            cmd.Parameters.Add(CreateSqlParameter("@IN_GROUP_ID", SqlDbType.Decimal, 10, "GROUP_ID", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_GROUP_CODE", SqlDbType.VarChar, 30, "GROUP_CODE", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_GROUP_NAME", SqlDbType.VarChar, 100, "GROUP_NAME", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IC_IS_ACTIVE", SqlDbType.Char, 1, "IS_ACTIVE", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_CREATED_BY", SqlDbType.VarChar, 30, "CREATED_BY", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@ID_CREATED_ON", SqlDbType.DateTime, -1, "CREATED_ON", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@ID_LAST_UPDATED", SqlDbType.DateTime, -1, "LAST_UPDATED", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IN_COMPANY_ID", SqlDbType.Decimal, 10, "COMPANY_ID", System.Data.ParameterDirection.Input, DataRowVersion.Current));
        }

        public void UpdateGroupAndDivisionLinkage(DataTable dtLinkage)
        {
			TransactionOptions to = new TransactionOptions();
            to.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew, to, EnterpriseServicesInteropOption.Full))
            {
				SqlDataAdapter da = new SqlDataAdapter();
				try
				{
                    da.UpdateCommand = new SqlCommand("SPN_XP_UPD_GRUP_DIV_LINK", ProfitCashflow.oPcfDM.conn);
					da.UpdateCommand.CommandType = CommandType.StoredProcedure;
					da.UpdateCommand.Parameters.Add(CreateSqlParameter("@IN_GROUP_ID", SqlDbType.Decimal, 10, "GROUP_ID", System.Data.ParameterDirection.Input, DataRowVersion.Current));
					da.UpdateCommand.Parameters.Add(CreateSqlParameter("@IN_DIVISION_ID", SqlDbType.Decimal, 10, "DIVISION_ID", System.Data.ParameterDirection.Input, DataRowVersion.Current));

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
