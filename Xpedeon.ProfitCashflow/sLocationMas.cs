using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Transactions;

namespace Xpedeon.ProfitCashflow
{
    class sLocationMas
    {
        public DataTable RetrieveLocationMaster()
        {
            DataTable dt = new DataTable();
            using (SqlDataAdapter da = new SqlDataAdapter())
            {
                da.SelectCommand = new SqlCommand("SPN_XP_GET_LOCATION", ProfitCashflow.oPcfDM.conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.Fill(dt);
            }

            return dt;
        }

        public int CheckLocationMaster(string sCountryCode, string sLocationCode)
        {
            object oResult;
            using (SqlCommand cmd = new SqlCommand("SPN_XP_CHECK_LOCATION", ProfitCashflow.oPcfDM.conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IV_COUNTRY_CODE", sCountryCode);
                cmd.Parameters.AddWithValue("@IV_LOCATION_CODE", sLocationCode);

                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                oResult = cmd.ExecuteScalar();
            }

            return (int)oResult;
        }

        public int CheckDeleteLocationMaster(string sCountryCode, string sLocationCode)
        {
            object oResult;
            using (SqlCommand cmd = new SqlCommand("SPN_XP_DEL_CHECK_LOCATION", ProfitCashflow.oPcfDM.conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IV_COUNTRY_CODE", sCountryCode);
                cmd.Parameters.AddWithValue("@IV_LOCATION_CODE", sLocationCode);

                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                oResult = cmd.ExecuteScalar();
            }

            return (int)oResult;
        }

        public void UpdateLocationMaster(string sType, DataRow drLocationMas)
        {
            TransactionOptions to = new TransactionOptions();
            to.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;

            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew, to, EnterpriseServicesInteropOption.Full))
            {
                SqlCommand cmd = new SqlCommand();
                try
                {
                    if (sType == "I")
                    {
                        cmd = new SqlCommand("SPN_XP_INS_LOCATION_MASTER", ProfitCashflow.oPcfDM.conn);
                        cmd.Parameters.AddWithValue("@IV_PREV_LOCATION_CODE", DBNull.Value);
                    }
                    else if (sType == "U")
                    {
                        cmd = new SqlCommand("SPN_XP_UPD_LOCATION_MASTER", ProfitCashflow.oPcfDM.conn);
                        cmd.Parameters.AddWithValue("@IV_PREV_LOCATION_CODE", drLocationMas["LOCATION_CODE", DataRowVersion.Original]);
                    }
                    else if (sType == "D")
                    {
                        cmd = new SqlCommand("SPN_XP_DEL_LOCATION_MASTER", ProfitCashflow.oPcfDM.conn);
                        cmd.Parameters.AddWithValue("@IV_COUNTRY_CODE", drLocationMas["COUNTRY_CODE", DataRowVersion.Original]);
                        cmd.Parameters.AddWithValue("@IV_LOCATION_CODE", drLocationMas["LOCATION_CODE", DataRowVersion.Original]);

                        goto L1;
                    }

                    cmd.Parameters.AddWithValue("@IV_COUNTRY_CODE", drLocationMas["COUNTRY_CODE"]);
                    cmd.Parameters.AddWithValue("@IV_NEW_LOCATION_CODE", drLocationMas["LOCATION_CODE", DataRowVersion.Current]);
                    cmd.Parameters.AddWithValue("@IV_LOCATION_NAME", drLocationMas["LOCATION_NAME"]);
                    cmd.Parameters.AddWithValue("@IV_REMARKS", drLocationMas["REMARKS"]);
                    cmd.Parameters.AddWithValue("@IV_CREATED_BY", drLocationMas["CREATED_BY"]);
                    cmd.Parameters.AddWithValue("@ID_CREATED_ON", drLocationMas["CREATED_ON"]);
                    cmd.Parameters.AddWithValue("@ID_LAST_UPDATED", drLocationMas["LAST_UPDATED"]);

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
