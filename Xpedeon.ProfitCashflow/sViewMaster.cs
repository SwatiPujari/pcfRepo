using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Transactions;

namespace Xpedeon.ProfitCashflow
{
    class sViewMaster
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

        public DataTable RetrieveViewMaster(decimal dCompanyId)
        {
            DataTable dtViewMas = new DataTable();
            using (SqlDataAdapter da = new SqlDataAdapter())
            {
                da.SelectCommand = new SqlCommand("SPN_XP_GET_VIEW", ProfitCashflow.oPcfDM.conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@IN_COMPANY_ID", SqlDbType.Decimal).Value = dCompanyId;

                da.Fill(dtViewMas);
            }

            return dtViewMas;
        }

        public int CheckViewMaster(decimal dCompanyId, string dViewName)
        {
            int iResult;
            using (SqlCommand cmd = new SqlCommand("SPN_XP_CHECK_VIEW", ProfitCashflow.oPcfDM.conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@IN_COMPANY_ID", SqlDbType.Decimal).Value = dCompanyId;
                cmd.Parameters.Add("@IV_VIEW_NAME", SqlDbType.VarChar).Value = dViewName;

                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                iResult = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return iResult;
        }

        public int RetrieveMaxViewId(decimal dCompanyId)
        {
            int oResult;
            using (SqlCommand cmd = new SqlCommand("SPN_XP_MAX_VIEW_ID", ProfitCashflow.oPcfDM.conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@IN_COMPANY_ID", SqlDbType.Decimal).Value = dCompanyId;

                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                oResult = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return oResult;
        }

        public int CheckDelViewMaster(decimal dCompanyId, decimal dViewId)
        {
            int oResult;
            using (SqlCommand cmd = new SqlCommand("SPN_XP_DEL_CHECK_VIEW", ProfitCashflow.oPcfDM.conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@IN_COMPANY_ID", SqlDbType.Decimal).Value = dCompanyId;
                cmd.Parameters.Add("@IN_VIEW_ID", SqlDbType.VarChar).Value = dViewId;

                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                oResult = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return oResult;
        }

        public void UpdateViewMaster(string sType, DataRow drViewMas)
        {
            TransactionOptions to = new TransactionOptions();
            to.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;

            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew, to, EnterpriseServicesInteropOption.Full))
            {
                SqlCommand cmd = new SqlCommand();
                try
                {
                    if (sType == "I")
                        cmd = new SqlCommand("SPN_XP_INS_VIEW_MASTER", ProfitCashflow.oPcfDM.conn);
                    else if (sType == "U")
                        cmd = new SqlCommand("SPN_XP_UPD_VIEW_MASTER", ProfitCashflow.oPcfDM.conn);
                    else if (sType == "D")
                    {
                        cmd = new SqlCommand("SPN_XP_DEL_VIEW_MASTER", ProfitCashflow.oPcfDM.conn);
                        cmd.Parameters.AddWithValue("@IN_COMPANY_ID", drViewMas["COMPANY_ID", DataRowVersion.Original]);
                        cmd.Parameters.AddWithValue("@IN_VIEW_ID", drViewMas["VIEW_ID", DataRowVersion.Original]);

                        goto L1;
                    }

                    cmd.Parameters.AddWithValue("@IN_COMPANY_ID", drViewMas["COMPANY_ID"]);
                    cmd.Parameters.AddWithValue("@IN_VIEW_ID", drViewMas["VIEW_ID"]);
                    cmd.Parameters.AddWithValue("@IV_VIEW_NAME", drViewMas["VIEW_NAME"]);
                    cmd.Parameters.AddWithValue("@IV_CREATED_BY", drViewMas["CREATED_BY"]);
                    cmd.Parameters.AddWithValue("@ID_CREATED_ON", drViewMas["CREATED_ON"]);
                    cmd.Parameters.AddWithValue("@ID_LAST_UPDATED", drViewMas["LAST_UPDATED"]);

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
