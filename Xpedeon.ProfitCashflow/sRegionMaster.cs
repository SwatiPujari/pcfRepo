using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Transactions;

namespace Xpedeon.ProfitCashflow
{
    class sRegionMaster
    {
        public object RetrieveDuplicateRegionCount(string sRegionCode)
        {
            object oResult = new object();
            SqlCommand cmd = null;

            try
            {
                cmd = new SqlCommand("SELECT COUNT(1) CNT FROM REGION_MASTER WHERE REGION_CODE = @IV_REGION_CODE", ProfitCashflow.oPcfDM.conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@IV_REGION_CODE", sRegionCode);

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
		
        public DataTable RetrieveCompanyListForRegion(string sUserName)
        {
            SqlDataAdapter da = null;
            DataTable dtCompList4Reg = new DataTable();

            try
            {
                da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("SPN_XP_GET_COMP_LIST_4_REGION", ProfitCashflow.oPcfDM.conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@IV_PCF_USER", sUserName);

                da.Fill(dtCompList4Reg);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (da != null) da.Dispose();
            }

            return dtCompList4Reg;
        }

        public DataTable RetrieveOpCompListForRegion()
        {
            SqlDataAdapter da = null;
            DataTable dtOpCompList4Reg = new DataTable();

            try
            {
                da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("SPN_XP_GET_OPCOMP_LIST_4_REGION", ProfitCashflow.oPcfDM.conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;

                da.Fill(dtOpCompList4Reg);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (da != null) da.Dispose();
            }

            return dtOpCompList4Reg;
        }

        public DataTable RetrieveRegionMaster()
        {
            SqlDataAdapter da = null;
            DataTable dtRegionMaster = new DataTable();

            try
            {
                da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("SPN_XP_GET_REGION_MASTER", ProfitCashflow.oPcfDM.conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;

                da.Fill(dtRegionMaster);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (da != null) da.Dispose();
            }

            return dtRegionMaster;
        }

        public DataTable UpdateRegionMaster(DataTable dtRegionMaster)
        {
			TransactionOptions to = new TransactionOptions();
            to.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew, to, EnterpriseServicesInteropOption.Full))
            {
				SqlDataAdapter da = new SqlDataAdapter();
				try
				{
					da.InsertCommand = new SqlCommand("SPN_XP_INS_REGION_MASTER", ProfitCashflow.oPcfDM.conn);
					da.InsertCommand.CommandType = CommandType.StoredProcedure;
					CreateRegionMasterCommand(da.InsertCommand);

					da.UpdateCommand = new SqlCommand("SPN_XP_UPD_REGION_MASTER", ProfitCashflow.oPcfDM.conn);
					da.UpdateCommand.CommandType = CommandType.StoredProcedure;
					CreateRegionMasterCommand(da.UpdateCommand);

					da.Update(dtRegionMaster);
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
            return dtRegionMaster;
        }

        private void CreateRegionMasterCommand(SqlCommand cmd)
        {
            cmd.Parameters.Add(CreateSqlParameter("@IN_REGION_ID", SqlDbType.Decimal, 10, "REGION_ID", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_REGION_CODE", SqlDbType.VarChar, 30, "REGION_CODE", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_REGION_NAME", SqlDbType.VarChar, 100, "REGION_NAME", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IC_IS_ACTIVE", SqlDbType.Char, 1, "IS_ACTIVE", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_CREATED_BY", SqlDbType.VarChar, 30, "CREATED_BY", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@ID_CREATED_ON", SqlDbType.DateTime, -1, "CREATED_ON", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@ID_LAST_UPDATED", SqlDbType.DateTime, -1, "LAST_UPDATED", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IN_COMPANY_ID", SqlDbType.Decimal, 10, "COMPANY_ID", System.Data.ParameterDirection.Input, DataRowVersion.Current));
        }

        public void UpdateRegionAndCompLinkage(DataTable dtLinkage)
        {
			TransactionOptions to = new TransactionOptions();
            to.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew, to, EnterpriseServicesInteropOption.Full))
            {
				SqlDataAdapter da = new SqlDataAdapter();
				try
				{
                    da.InsertCommand = new SqlCommand("SPN_XP_INS_REG_COMP_LINKAGE", ProfitCashflow.oPcfDM.conn);
                    da.InsertCommand.CommandType = CommandType.StoredProcedure;
                    CreateRegionCompLinkCommand(da.InsertCommand);

                    da.DeleteCommand = new SqlCommand("SPN_XP_DEL_REG_COMP_LINKAGE", ProfitCashflow.oPcfDM.conn);
                    da.DeleteCommand.CommandType = CommandType.StoredProcedure;
                    CreateRegionCompLinkCommand(da.DeleteCommand);

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

        private void CreateRegionCompLinkCommand(SqlCommand cmd)
        {
            cmd.Parameters.Add(CreateSqlParameter("@IN_REGION_ID", SqlDbType.Decimal, 10, "REGION_ID", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IN_COMPANY_ID", SqlDbType.Decimal, 10, "COMPANY_ID", System.Data.ParameterDirection.Input, DataRowVersion.Current));
        }

        public void UpdateRegionOpCompLinkage(DataTable dtLinkage)
        {
            TransactionOptions to = new TransactionOptions();
            to.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew, to, EnterpriseServicesInteropOption.Full))
            {
                SqlDataAdapter da = new SqlDataAdapter();
                try
                {
                    da.UpdateCommand = new SqlCommand("SPN_XP_UPD_REGION_OPCOMP_LINK", ProfitCashflow.oPcfDM.conn);
                    da.UpdateCommand.CommandType = CommandType.StoredProcedure;
                    da.UpdateCommand.Parameters.Add(CreateSqlParameter("@IN_REGION_ID", SqlDbType.Decimal, 10, "REGION_ID", System.Data.ParameterDirection.Input, DataRowVersion.Current));
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
