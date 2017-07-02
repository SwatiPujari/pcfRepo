using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Transactions;

namespace Xpedeon.ProfitCashflow
{
    class sCFOtherIncomeMaster
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

        public DataTable RetrieveMaster(decimal dCompanyId)
        {
            DataTable dt = new DataTable();

            using (SqlDataAdapter da = new SqlDataAdapter())
            {
                da.SelectCommand = new SqlCommand("SPN_XP_GET_CF_INCOME", ProfitCashflow.oPcfDM.conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@IN_COMPANY_ID", SqlDbType.Decimal).Value = dCompanyId;
                da.Fill(dt);
            }

            return dt;
        }

        public int CheckMaster(decimal dCompanyId, string dName)
        {
            int oResult;

            using (SqlCommand cmd = new SqlCommand("SPN_XP_CHECK_CF_INCOME",ProfitCashflow.oPcfDM.conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@IN_COMPANY_ID", SqlDbType.Decimal).Value = dCompanyId;
                cmd.Parameters.Add("@IV_CF_CATEGORY_NAME", SqlDbType.VarChar).Value = dName;

                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                oResult = Convert.ToInt32(cmd.ExecuteScalar()); 
            }
         
            return oResult;
        }

        public int RetrieveMaxId(decimal dCompanyId)
        {
            int oResult;
            using (SqlCommand cmd = new SqlCommand("SPN_XP_MAX_CF_INCOME_ID", ProfitCashflow.oPcfDM.conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@IN_COMPANY_ID", SqlDbType.Decimal).Value = dCompanyId;

                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                oResult = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return oResult;
        }

        public void UpdateMaster(string sType, DataRow drMas)
        {
            TransactionOptions to = new TransactionOptions();
            to.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;

            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew, to, EnterpriseServicesInteropOption.Full))
            {
                SqlCommand cmd = new SqlCommand();
                try
                {
                    SqlConnection conn = ProfitCashflow.oPcfDM.conn;

                    if (sType == "I")
                        cmd = new SqlCommand("SPN_XP_INS_CF_INCOME_MASTER", ProfitCashflow.oPcfDM.conn);
                    else if (sType == "U")
                        cmd = new SqlCommand("SPN_XP_UPD_CF_INCOME_MASTER", ProfitCashflow.oPcfDM.conn);
                    else if (sType == "D")
                    {
                        cmd = new SqlCommand("SPN_XP_DEL_CF_INCOME_MASTER", ProfitCashflow.oPcfDM.conn);
                        cmd.Parameters.AddWithValue("@IN_COMPANY_ID", drMas["COMPANY_ID", DataRowVersion.Original]);
                        cmd.Parameters.AddWithValue("@IN_CF_CATEGORY_ID", drMas["CF_CATEGORY_ID", DataRowVersion.Original]);

                        goto L1;
                    }

                    cmd.Parameters.AddWithValue("@IN_COMPANY_ID", drMas["COMPANY_ID"]);
                    cmd.Parameters.AddWithValue("@IN_CF_CATEGORY_ID", drMas["CF_CATEGORY_ID"]);
                    cmd.Parameters.AddWithValue("@IV_CF_CATEGORY_NAME", drMas["CF_CATEGORY_NAME"]);
                    cmd.Parameters.AddWithValue("@IV_CREATED_BY", drMas["CREATED_BY"]);
                    cmd.Parameters.AddWithValue("@ID_CREATED_ON", drMas["CREATED_ON"]);
                    cmd.Parameters.AddWithValue("@ID_LAST_UPDATED", drMas["LAST_UPDATED"]);

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
                    if (cmd.Connection != null && cmd.Connection.State != ConnectionState.Closed)
                        cmd.Connection.Close();
                    cmd.Dispose();
                }
            }
        }
    }
}
