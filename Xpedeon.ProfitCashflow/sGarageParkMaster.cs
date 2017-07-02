using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Transactions;

namespace Xpedeon.ProfitCashflow
{
    class sGarageParkMaster
    {
        public DataTable RetrieveCompany(string sUser, string isActive)
        {
            DataTable dt = new DataTable();
            using (SqlDataAdapter da = new SqlDataAdapter())
            {
                da.SelectCommand = new SqlCommand("SPN_XP_GET_COMPANY", ProfitCashflow.oPcfDM.conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@IV_PCF_USER", SqlDbType.VarChar).Value = sUser;
                da.SelectCommand.Parameters.Add("@IC_IS_ACTIVE", SqlDbType.Char).Value = isActive;

                da.Fill(dt);
            }

            return dt;
        }

        public DataTable RetrieveGarageMaster(decimal dCompanyId)
        {
            DataTable dt = new DataTable();
            using (SqlDataAdapter da = new SqlDataAdapter())
            {
                da.SelectCommand = new SqlCommand("SPN_XP_GET_GARAGE_PARKING", ProfitCashflow.oPcfDM.conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@IN_COMPANY_ID", SqlDbType.Decimal).Value = dCompanyId;

                da.Fill(dt);
            }

            return dt;
        }

        public int CheckGarageMaster(decimal dCompanyId, string dGarageName)
        {
            int iResult;
            using (SqlCommand cmd = new SqlCommand("SPN_XP_CHECK_GARAGE_PARKING", ProfitCashflow.oPcfDM.conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@IN_COMPANY_ID", SqlDbType.Decimal).Value = dCompanyId;
                cmd.Parameters.Add("@IV_GARAGE_PARKING_NAME", SqlDbType.VarChar).Value = dGarageName;

                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                iResult = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return iResult;
        }

        public int RetrieveMaxGarageId(decimal dCompanyId)
        {
            int iResult;
            using (SqlCommand cmd = new SqlCommand("SPN_XP_MAX_GARAGE_PARKING_ID", ProfitCashflow.oPcfDM.conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@IN_COMPANY_ID", SqlDbType.Decimal).Value = dCompanyId;

                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                iResult = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return iResult;
        }

        public int CheckDelGarageMaster(decimal dCompanyId, decimal dGarageId)
        {
            int iResult;
            using (SqlCommand cmd = new SqlCommand("SPN_XP_DEL_CHECK_GARAGE_PARKING", ProfitCashflow.oPcfDM.conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@IN_COMPANY_ID", SqlDbType.Decimal).Value = dCompanyId;
                cmd.Parameters.Add("@IN_GARAGE_PARKING_ID", SqlDbType.VarChar).Value = dGarageId;

                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                iResult = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return iResult;
        }

        public void UpdateGarageMaster(string sType, DataRow drGarageMas)
        {
            TransactionOptions to = new TransactionOptions();
            to.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;

            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew, to, EnterpriseServicesInteropOption.Full))
            {
                SqlCommand cmd = new SqlCommand();
                try
                {
                    if (sType == "I")
                        cmd = new SqlCommand("SPN_XP_INS_GARAGE_PARKING_MASTER", ProfitCashflow.oPcfDM.conn);
                    else if (sType == "U")
                        cmd = new SqlCommand("SPN_XP_UPD_GARAGE_PARKING_MASTER", ProfitCashflow.oPcfDM.conn);
                    else if (sType == "D")
                    {
                        cmd = new SqlCommand("SPN_XP_DEL_GARAGE_PARKING_MASTER", ProfitCashflow.oPcfDM.conn);
                        cmd.Parameters.AddWithValue("@IN_COMPANY_ID", drGarageMas["COMPANY_ID", DataRowVersion.Original]);
                        cmd.Parameters.AddWithValue("@IN_GARAGE_PARKING_ID", drGarageMas["GARAGE_PARKING_ID", DataRowVersion.Original]);

                        goto L1;
                    }

                    cmd.Parameters.AddWithValue("@IN_COMPANY_ID", drGarageMas["COMPANY_ID"]);
                    cmd.Parameters.AddWithValue("@IN_GARAGE_PARKING_ID", drGarageMas["GARAGE_PARKING_ID"]);
                    cmd.Parameters.AddWithValue("@IV_GARAGE_PARKING_NAME", drGarageMas["GARAGE_PARKING_NAME"]);
                    cmd.Parameters.AddWithValue("@IV_CREATED_BY", drGarageMas["CREATED_BY"]);
                    cmd.Parameters.AddWithValue("@ID_CREATED_ON", drGarageMas["CREATED_ON"]);
                    cmd.Parameters.AddWithValue("@ID_LAST_UPDATED", drGarageMas["LAST_UPDATED"]);

                L1: cmd.CommandType = CommandType.StoredProcedure;
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
