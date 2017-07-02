using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Transactions;

namespace Xpedeon.ProfitCashflow
{
    public class sInterestRateMaster
    {
        public DataTable RetrieveCompany(string sUser, string isActive)
        {
            DataTable dtCompanyMas = new DataTable();
            using (SqlDataAdapter da = new SqlDataAdapter())
            {
                da.SelectCommand = new SqlCommand("SPN_XP_GET_COMPANY", ProfitCashflow.oPcfDM.conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@IV_PCF_USER", SqlDbType.VarChar).Value = sUser;
                da.SelectCommand.Parameters.Add("@IC_IS_ACTIVE", SqlDbType.Char).Value = isActive;
                da.Fill(dtCompanyMas);
            }

            return dtCompanyMas;
        }

        public DataTable RetrieveMaster(string sMode, decimal sCompany)
        {
            DataTable dt = new DataTable();
            using (SqlDataAdapter da = new SqlDataAdapter())
            {
                da.SelectCommand = new SqlCommand("SPN_XP_GET_INTEREST_RATE", ProfitCashflow.oPcfDM.conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@IV_MODE", sMode);
                da.SelectCommand.Parameters.AddWithValue("@IN_COMPANY_ID", sCompany);
                da.Fill(dt);
            }

            return dt;
        }

        public int RetrieveMaxInterestRateId()
        {
            int oResult;
            using (SqlCommand cmd = new SqlCommand("SPN_GET_SEQ_ID", ProfitCashflow.oPcfDM.conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IC_SEQ", "SEQ_PK_INT_RATE_HISTORY");

                //Add the output parameter to the command object
                SqlParameter outPutParameter = new SqlParameter();
                outPutParameter.ParameterName = "@OP_VAL";
                outPutParameter.SqlDbType = System.Data.SqlDbType.Int;
                outPutParameter.Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters.Add(outPutParameter);
                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                cmd.ExecuteScalar();
                oResult = (int)outPutParameter.Value;
            }

            return oResult;
        }

        public int CheckMaster(string sCompany, DateTime? dEffectiveDt)
        {
            object oResult;

            using (SqlCommand cmd = new SqlCommand("SPN_XP_CHECK_INTEREST_RATE", ProfitCashflow.oPcfDM.conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IN_COMPANY_ID", sCompany);
                cmd.Parameters.AddWithValue("@ID_PREV_INT_EFFECTIVE_DATE", dEffectiveDt);

                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                oResult = cmd.ExecuteScalar();
            }

            return (int)oResult;
        }

        public void UpdateInterestRate(string mode, DataTable dt)
        {
            TransactionOptions to = new TransactionOptions();
            to.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;

            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew, to, EnterpriseServicesInteropOption.Full))
            {
                SqlDataAdapter da = new SqlDataAdapter();
                try
                {
                    if (mode == "I")
                    {
                        da.InsertCommand = new SqlCommand("SPN_XP_INS_INTEREST_RATE_MASTER", ProfitCashflow.oPcfDM.conn);
                        da.InsertCommand.CommandType = CommandType.StoredProcedure;
                        CreateCommand(mode, da.InsertCommand);

                        da.UpdateCommand = new SqlCommand("SPN_XP_UPD_INTEREST_RATE_MASTER", ProfitCashflow.oPcfDM.conn);
                        da.UpdateCommand.CommandType = CommandType.StoredProcedure;
                        CreateCommand(mode, da.UpdateCommand);
                    }
                    else
                    {
                        da.InsertCommand = new SqlCommand("SPN_XP_INS_INTEREST_RATE_HISTORY_MASTER", ProfitCashflow.oPcfDM.conn);
                        da.InsertCommand.CommandType = CommandType.StoredProcedure;
                        CreateCommand(mode, da.InsertCommand);
                    }

                    da.Update(dt);
                    ts.Complete();
                }
                  catch (Exception ex)
                {
                    ts.Dispose();
                    throw ex;
                }
                finally
                {
                    da.Dispose();
                    da = null;
                }
            }
        }

        public void DeleteHistory(DataRow dr)
        {
            TransactionOptions to = new TransactionOptions();
            to.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;

            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew, to, EnterpriseServicesInteropOption.Full))
            {
                SqlCommand cmd = new SqlCommand();
                try
                {
                    cmd = new SqlCommand("SPN_XP_DEL_INTEREST_RATE_HISTORY", ProfitCashflow.oPcfDM.conn);
                    cmd.Parameters.AddWithValue("@IN_INTEREST_RATE_HISTORY_ID", dr["INTEREST_RATE_HISTORY_ID"]);

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

        private void CreateCommand(string mode, SqlCommand cmd)
        {
            if (mode == "I")
            {
                cmd.Parameters.Add(CreateSqlParameter("@IN_COMPANY_ID", SqlDbType.Decimal, 10, "COMPANY_ID", System.Data.ParameterDirection.Input, DataRowVersion.Current));
                cmd.Parameters.Add(CreateSqlParameter("@IN_INTEREST_RATE", SqlDbType.Decimal, 15, "INTEREST_RATE", System.Data.ParameterDirection.Input, DataRowVersion.Current));
                cmd.Parameters.Add(CreateSqlParameter("@IV_CREATED_BY", SqlDbType.VarChar, 30, "CREATED_BY", System.Data.ParameterDirection.Input, DataRowVersion.Current));
                cmd.Parameters.Add(CreateSqlParameter("@ID_CREATED_ON", SqlDbType.DateTime, -1, "CREATED_ON", System.Data.ParameterDirection.Input, DataRowVersion.Current));
                cmd.Parameters.Add(CreateSqlParameter("@ID_INT_EFFECTIVE_DATE", SqlDbType.DateTime, -1, "INT_EFFECTIVE_DATE", System.Data.ParameterDirection.Input, DataRowVersion.Current));
                cmd.Parameters.Add(CreateSqlParameter("@IV_REMARKS", SqlDbType.VarChar, 100, "REMARKS", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            }
            else
            {
                cmd.Parameters.Add(CreateSqlParameter("@IN_COMPANY_ID", SqlDbType.Decimal, 10, "COMPANY_ID", System.Data.ParameterDirection.Input, DataRowVersion.Current));
                cmd.Parameters.Add(CreateSqlParameter("@IN_PREV_INTEREST_RATE", SqlDbType.Decimal, 15, "PREV_INTEREST_RATE", System.Data.ParameterDirection.Input, DataRowVersion.Current));
                cmd.Parameters.Add(CreateSqlParameter("@IV_REMARKS", SqlDbType.VarChar, 100, "REMARKS", System.Data.ParameterDirection.Input, DataRowVersion.Current));
                cmd.Parameters.Add(CreateSqlParameter("@IV_CREATED_BY", SqlDbType.VarChar, 30, "CREATED_BY", System.Data.ParameterDirection.Input, DataRowVersion.Current));
                cmd.Parameters.Add(CreateSqlParameter("@ID_CREATED_ON", SqlDbType.DateTime, -1, "CREATED_ON", System.Data.ParameterDirection.Input, DataRowVersion.Current));
                cmd.Parameters.Add(CreateSqlParameter("@ID_PREV_INT_EFFECTIVE_DATE", SqlDbType.DateTime, -1, "PREV_INT_EFFECTIVE_DATE", System.Data.ParameterDirection.Input, DataRowVersion.Current));
                cmd.Parameters.Add(CreateSqlParameter("@IN_INTEREST_RATE_HISTORY_ID", SqlDbType.Decimal, 10, "INTEREST_RATE_HISTORY_ID", System.Data.ParameterDirection.Input, DataRowVersion.Current));
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
