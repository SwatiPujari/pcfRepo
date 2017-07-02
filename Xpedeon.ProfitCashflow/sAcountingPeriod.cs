using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Transactions;

namespace Xpedeon.ProfitCashflow
{
    class sAccountingPeriod
    {
        public DataTable RetrieveCompany(string sUser)
        {
            DataTable dtCompanyMas = new DataTable();
            using (SqlDataAdapter da = new SqlDataAdapter())
            {
                da.SelectCommand = new SqlCommand("SPN_XP_GET_ACC_PERIOD_COMPANY", ProfitCashflow.oPcfDM.conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@IV_PCF_USER", SqlDbType.VarChar).Value = sUser;
                da.Fill(dtCompanyMas);
            }

            return dtCompanyMas;
        }

        public DataTable RetrieveAccountingPeriod(string sUser)
        {
            DataTable dtAccPeriod = new DataTable();
            using (SqlDataAdapter da = new SqlDataAdapter())
            {
                da.SelectCommand = new SqlCommand("SPN_XP_GET_ACC_PERIOD", ProfitCashflow.oPcfDM.conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@IV_PCF_USER", SqlDbType.VarChar).Value = sUser;
                da.Fill(dtAccPeriod);
            }

            return dtAccPeriod;
        }

        public int CheckAccountingCode(decimal dCompanyId, string sPeriodCode)
        {
            int oResult;
            using (SqlCommand cmd = new SqlCommand("SPN_XP_CHECK_ACC_CODE", ProfitCashflow.oPcfDM.conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@IN_COMPANY_ID", SqlDbType.Decimal).Value = dCompanyId;
                cmd.Parameters.Add("@IV_PERIOD_CODE", SqlDbType.VarChar).Value = sPeriodCode;

                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                oResult = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return oResult;
        }

        public int CheckAccountingPeriod(decimal dCompanyId, string sPeriodCode)
        {
            int oResult;
            using (SqlCommand cmd = new SqlCommand("SPN_XP_CHECK_ACC_PERIOD", ProfitCashflow.oPcfDM.conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@IN_COMPANY_ID", SqlDbType.Decimal).Value = dCompanyId;
                cmd.Parameters.Add("@IV_PERIOD_CODE", SqlDbType.VarChar).Value = sPeriodCode;

                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                oResult = Convert.ToInt32(cmd.ExecuteScalar());
            }
            return oResult;
        }

        public int CheckAccountingMonths(decimal dCompanyId, string sPeriodCode)
        {
            int oResult;
            using (SqlCommand cmd = new SqlCommand("SPN_XP_CHECK_ACC_MONTHS", ProfitCashflow.oPcfDM.conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@IN_COMPANY_ID", SqlDbType.Decimal).Value = dCompanyId;
                cmd.Parameters.Add("@IV_PERIOD_CODE", SqlDbType.VarChar).Value = sPeriodCode;

                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                oResult = Convert.ToInt32(cmd.ExecuteScalar());
            }
            return oResult;
        }

        public int CheckDelEndDateSubPeriod(DataRow dr)
        {
            int oResult;
            using (SqlCommand cmd = new SqlCommand("SPN_XP_DEL_ACCOUNTING_SUB_PERIOD", ProfitCashflow.oPcfDM.conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@IN_COMPANY_ID", SqlDbType.Decimal).Value = dr["COMPANY_ID"];
                cmd.Parameters.Add("@IV_PERIOD_CODE", SqlDbType.VarChar).Value = dr["PERIOD_CODE"];
                cmd.Parameters.Add("@ID_NEW_START_DATE", SqlDbType.DateTime).Value = dr["START_DATE"];
                cmd.Parameters.Add("@ID_NEW_END_DATE", SqlDbType.DateTime).Value = dr["END_DATE"];

                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                oResult = Convert.ToInt32(cmd.ExecuteScalar());
            }
            return oResult;
        }

        public void UpdateAccountingPeriod(DataTable dt)
        {
            TransactionOptions to = new TransactionOptions();
            to.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;

            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew, to, EnterpriseServicesInteropOption.Full))
            {
                using (SqlDataAdapter da = new SqlDataAdapter())
                {
                    da.InsertCommand = new SqlCommand("SPN_XP_INS_ACCOUNTING_PERIOD", ProfitCashflow.oPcfDM.conn);
                    da.InsertCommand.CommandType = CommandType.StoredProcedure;
                    CreateCommand(da.InsertCommand);

                    da.UpdateCommand = new SqlCommand("SPN_XP_UPD_ACCOUNTING_PERIOD", ProfitCashflow.oPcfDM.conn);
                    da.UpdateCommand.CommandType = CommandType.StoredProcedure;
                    CreateCommand(da.UpdateCommand);

                    da.DeleteCommand = new SqlCommand("SPN_XP_DEL_ACCOUNTING_PERIOD", ProfitCashflow.oPcfDM.conn);
                    da.DeleteCommand.CommandType = CommandType.StoredProcedure;
                    DeletePeriodCommand(da.DeleteCommand);

                    da.Update(dt);
                    ts.Complete();
                }
            }
        }

        private void CreateCommand(SqlCommand cmd)
        {
            cmd.Parameters.Add(CreateSqlParameter("@IV_PERIOD_CODE", SqlDbType.VarChar, 30, "PERIOD_CODE", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_PERIOD_NAME", SqlDbType.VarChar, 100, "PERIOD_NAME", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@ID_START_DATE", SqlDbType.DateTime, -1, "START_DATE", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@ID_END_DATE", SqlDbType.DateTime, -1, "END_DATE", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IC_PERIOD_TYPE", SqlDbType.Char, 1, "PERIOD_TYPE", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_MAJOR_PERIOD_CODE", SqlDbType.VarChar, 30, "MAJOR_PERIOD_CODE", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_CREATED_BY", SqlDbType.VarChar, 30, "CREATED_BY", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@ID_CREATED_ON", SqlDbType.DateTime, -1, "CREATED_ON", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@ID_LAST_UPDATED", SqlDbType.DateTime, -1, "LAST_UPDATED", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IN_COMPANY_ID", SqlDbType.Decimal, 10, "COMPANY_ID", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IC_STATUS", SqlDbType.Char, 1, "STATUS", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IN_CODA_SEC1", SqlDbType.Decimal, 2, "CODA_SEC1", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IC_CURRENT_PERIOD", SqlDbType.Char, 1, "CURRENT_PERIOD", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IN_CODA_ACCESS_LEVEL", SqlDbType.Decimal, 2, "CODA_ACCESS_LEVEL", System.Data.ParameterDirection.Input, DataRowVersion.Current));
        }

        private void DeletePeriodCommand(SqlCommand cmd)
        {
            cmd.Parameters.Add(CreateSqlParameter("@IN_COMPANY_ID", SqlDbType.Decimal, 10, "COMPANY_ID", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_PERIOD_CODE", SqlDbType.VarChar, 30, "PERIOD_CODE", System.Data.ParameterDirection.Input, DataRowVersion.Current));
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
