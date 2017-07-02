using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Transactions;

namespace Xpedeon.ProfitCashflow
{
    class sDivisionMaster
    {
        public object RetrieveDuplicateDivisionCount(string sDivisionCode)
        {
            object oResult = new object();
            SqlCommand cmd = null;

            try
            {
                cmd = new SqlCommand("SELECT COUNT(1) CNT FROM DIVISION_MASTER WHERE DIVISION_CODE = @IV_DIVISION_CODE", ProfitCashflow.oPcfDM.conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@IV_DIVISION_CODE", sDivisionCode);

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
		
        public DataTable RetrieveCompForLinkedReg(string sUserName, decimal dDivisionId)
        {
            SqlDataAdapter da = null;
            DataTable dtCompList = new DataTable();

            try
            {
                da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("SPN_XP_GET_COMPANY_4_DIV", ProfitCashflow.oPcfDM.conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@IV_PCF_USER", sUserName);
                da.SelectCommand.Parameters.AddWithValue("@IV_DIVISION_ID", dDivisionId);

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

        public DataTable RetrieveRegionList()
        {
            SqlDataAdapter da = null;
            DataTable dtRegionList = new DataTable();

            try
            {
                da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("SPN_XP_GET_REGION_LIST_4_DIV_MAST", ProfitCashflow.oPcfDM.conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
				
                da.Fill(dtRegionList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (da != null) da.Dispose();
            }

            return dtRegionList;
        }

        public DataTable RetrieveDivisionMaster()
        {
            SqlDataAdapter da = null;
            DataTable dtDivisionMaster = new DataTable();

            try
            {
                da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("SPN_XP_GET_DIVISION_MASTER", ProfitCashflow.oPcfDM.conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;

                da.Fill(dtDivisionMaster);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (da != null) da.Dispose();
            }

            return dtDivisionMaster;
        }

        public DataTable UpdateDivisionMaster(DataTable dtDivisionMaster)
        {
			TransactionOptions to = new TransactionOptions();
            to.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew, to, EnterpriseServicesInteropOption.Full))
            {
				SqlDataAdapter da = new SqlDataAdapter();
				try
				{
					da.InsertCommand = new SqlCommand("SPN_XP_INS_DIVISION_MASTER", ProfitCashflow.oPcfDM.conn);
					da.InsertCommand.CommandType = CommandType.StoredProcedure;
					CreateDivisionMasterCommand(da.InsertCommand);

					da.UpdateCommand = new SqlCommand("SPN_XP_UPD_DIVISION_MASTER", ProfitCashflow.oPcfDM.conn);
					da.UpdateCommand.CommandType = CommandType.StoredProcedure;
					CreateDivisionMasterCommand(da.UpdateCommand);

					da.Update(dtDivisionMaster);
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
            return dtDivisionMaster;
        }

        private void CreateDivisionMasterCommand(SqlCommand cmd)
        {
            cmd.Parameters.Add(CreateSqlParameter("@IN_DIVISION_ID", SqlDbType.Decimal, 10, "DIVISION_ID", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_DIVISION_CODE", SqlDbType.VarChar, 30, "DIVISION_CODE", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_DIVISION_NAME", SqlDbType.VarChar, 100, "DIVISION_NAME", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IC_IS_ACTIVE", SqlDbType.Char, 1, "IS_ACTIVE", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_CREATED_BY", SqlDbType.VarChar, 30, "CREATED_BY", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@ID_CREATED_ON", SqlDbType.DateTime, -1, "CREATED_ON", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@ID_LAST_UPDATED", SqlDbType.DateTime, -1, "LAST_UPDATED", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IN_COMPANY_ID", SqlDbType.Decimal, 10, "COMPANY_ID", System.Data.ParameterDirection.Input, DataRowVersion.Current));
        }

        public void UpdateDivisionAndRegionLinkage(DataTable dtLinkage)
        {
			TransactionOptions to = new TransactionOptions();
            to.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew, to, EnterpriseServicesInteropOption.Full))
            {
				SqlDataAdapter da = new SqlDataAdapter();
				try
				{
					da.UpdateCommand = new SqlCommand("SPN_XP_UPD_DIV_REGION_LINK", ProfitCashflow.oPcfDM.conn);
					da.UpdateCommand.CommandType = CommandType.StoredProcedure;
					da.UpdateCommand.Parameters.Add(CreateSqlParameter("@IN_DIVISION_ID", SqlDbType.Decimal, 10, "DIVISION_ID", System.Data.ParameterDirection.Input, DataRowVersion.Current));
					da.UpdateCommand.Parameters.Add(CreateSqlParameter("@IN_REGION_ID", SqlDbType.Decimal, 10, "REGION_ID", System.Data.ParameterDirection.Input, DataRowVersion.Current));

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

        public void CheckCompRegionLinkage(decimal dDivisionId, decimal dRegionId)
        {
            SqlCommand cmd = null;
            try
            {
                cmd = new SqlCommand("SPN_XP_VALIDATE_DIV_REG_LINK", ProfitCashflow.oPcfDM.conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IN_DIVISION_ID", dDivisionId);
                cmd.Parameters.AddWithValue("@IN_REGION_ID", dRegionId);

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
