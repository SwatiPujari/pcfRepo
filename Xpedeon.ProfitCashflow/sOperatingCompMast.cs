using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Transactions;

namespace Xpedeon.ProfitCashflow
{
    class sOperatingCompMast
    {
        public object RetrieveDuplicateOpCompCount(string sOpCompCode)
        {
            object oResult = new object();
            SqlCommand cmd = null;

            try
            {
                cmd = new SqlCommand("SELECT COUNT(1) CNT FROM OPERATING_COMPANY_MASTER WHERE OPERATING_COMPANY_CODE = @IV_OP_COMP_CODE", ProfitCashflow.oPcfDM.conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@IV_OP_COMP_CODE", sOpCompCode);

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
		
        public DataTable RetrieveCompanyList4OpComp(string sUserName)
        {
            SqlDataAdapter da = null;
            DataTable dtCompList = new DataTable();

            try
            {
                da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("SPN_XP_GET_COMP_LIST_4_OPCOMP", ProfitCashflow.oPcfDM.conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@IV_PCF_USER", sUserName);

                da.Fill(dtCompList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (da != null) da.Dispose();
            }

            return dtCompList;
        }

        public DataTable RetrieveOperatingCompanyMaster()
        {
            SqlDataAdapter da = null;
            DataTable dtOpCompMast = new DataTable();

            try
            {
                da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("SPN_XP_GET_OPERATING_COMP_MAST", ProfitCashflow.oPcfDM.conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;

                da.Fill(dtOpCompMast);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (da != null) da.Dispose();
            }

            return dtOpCompMast;
        }

        public DataTable UpdateOperatingCompMaster(DataTable dtOpCompMast)
        {
			TransactionOptions to = new TransactionOptions();
            to.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew, to, EnterpriseServicesInteropOption.Full))
            {
				SqlDataAdapter da = new SqlDataAdapter();
				try
				{
					da.InsertCommand = new SqlCommand("SPN_XP_INS_OPERATING_COMP_MAST", ProfitCashflow.oPcfDM.conn);
					da.InsertCommand.CommandType = CommandType.StoredProcedure;
					CreateOperatingCompMastCommand(da.InsertCommand);

					da.UpdateCommand = new SqlCommand("SPN_XP_UPD_OPERATING_COMP_MAST", ProfitCashflow.oPcfDM.conn);
					da.UpdateCommand.CommandType = CommandType.StoredProcedure;
					CreateOperatingCompMastCommand(da.UpdateCommand);

					da.Update(dtOpCompMast);
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
            return dtOpCompMast;
        }

        private void CreateOperatingCompMastCommand(SqlCommand cmd)
        {
            cmd.Parameters.Add(CreateSqlParameter("@IN_OPERATING_COMPANY_ID", SqlDbType.Decimal, 10, "OPERATING_COMPANY_ID", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_OPERATING_COMPANY_CODE", SqlDbType.VarChar, 30, "OPERATING_COMPANY_CODE", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_OPERATING_COMPANY_NAME", SqlDbType.VarChar, 100, "OPERATING_COMPANY_NAME", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IC_IS_ACTIVE", SqlDbType.Char, 1, "IS_ACTIVE", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_CREATED_BY", SqlDbType.VarChar, 30, "CREATED_BY", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@ID_CREATED_ON", SqlDbType.DateTime, -1, "CREATED_ON", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@ID_LAST_UPDATED", SqlDbType.DateTime, -1, "LAST_UPDATED", System.Data.ParameterDirection.Input, DataRowVersion.Current));
        }

        public void UpdateOperatingCompAndCompLinkage(DataTable dtLinkage)
        {
			TransactionOptions to = new TransactionOptions();
            to.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew, to, EnterpriseServicesInteropOption.Full))
            {
				SqlDataAdapter da = new SqlDataAdapter();
				try
				{
					da.UpdateCommand = new SqlCommand("SPN_XP_UPD_OPCOMP_COMP_LINK", ProfitCashflow.oPcfDM.conn);
					da.UpdateCommand.CommandType = CommandType.StoredProcedure;
					da.UpdateCommand.Parameters.Add(CreateSqlParameter("@IN_COMPANY_ID", SqlDbType.Decimal, 10, "COMPANY_ID", System.Data.ParameterDirection.Input, DataRowVersion.Current));
					da.UpdateCommand.Parameters.Add(CreateSqlParameter("@IN_OPERATING_COMPANY_ID", SqlDbType.Decimal, 10, "OPERATING_COMPANY_ID", System.Data.ParameterDirection.Input, DataRowVersion.Current));

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
