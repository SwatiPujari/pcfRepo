using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Transactions;

namespace Xpedeon.ProfitCashflow
{
    class sB4SiteCodeMaster
    {
        public DataTable RetrieveCompany(string sUser, string isActive)
        {
            DataTable dtCompanyMas = new DataTable();
            using(SqlDataAdapter da = new SqlDataAdapter())
            {
                da.SelectCommand = new SqlCommand("SPN_XP_GET_COMPANY");
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@IV_PCF_USER", SqlDbType.VarChar).Value = sUser;
                da.SelectCommand.Parameters.Add("@IC_IS_ACTIVE", SqlDbType.Char).Value = isActive;
                da.SelectCommand.Connection = ProfitCashflow.oPcfDM.conn;

                da.Fill(dtCompanyMas);
            }
            return dtCompanyMas;
        }

        public DataTable RetrieveMaster(decimal dCompanyId)
        {
            DataTable dt = new DataTable();
            using (SqlDataAdapter da = new SqlDataAdapter())
            {
                da.SelectCommand = new SqlCommand("SPN_XP_GET_B4_SITE_CODE");
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@IN_COMPANY_ID", SqlDbType.Decimal).Value = dCompanyId;
                da.SelectCommand.Connection = ProfitCashflow.oPcfDM.conn;
                da.Fill(dt);
            }
            return dt;
        }

        public int CheckMaster(decimal dCompanyId, string dName)
        {
            int oResult;
            using (SqlCommand cmd = new SqlCommand("SPN_XP_CHECK_B4_SITE_CODE", ProfitCashflow.oPcfDM.conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@IN_COMPANY_ID", SqlDbType.Decimal).Value = dCompanyId;
                cmd.Parameters.Add("@IV_B4_SITE_CODE", SqlDbType.VarChar).Value = dName;

                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                oResult = Convert.ToInt32(cmd.ExecuteScalar()); 
            }

            return oResult;
        }

        public int CheckDelMaster(decimal dCompanyId, string sCode)
        {
            int oResult;
            using (SqlCommand cmd = new SqlCommand("SPN_XP_DEL_CHECK_B4_SITE_CODE", ProfitCashflow.oPcfDM.conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@IN_COMPANY_ID", SqlDbType.Decimal).Value = dCompanyId;
                cmd.Parameters.Add("@IV_B4_SITE_CODE", SqlDbType.VarChar).Value = sCode;

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
                    if (sType == "I")
                        cmd = new SqlCommand("SPN_XP_INS_B4_SITE_CODE_MASTER", ProfitCashflow.oPcfDM.conn);
                    else if (sType == "U")
                        cmd = new SqlCommand("SPN_XP_UPD_B4_SITE_CODE_MASTER", ProfitCashflow.oPcfDM.conn);
                    else if (sType == "D")
                    {
                        cmd = new SqlCommand("SPN_XP_DEL_B4_SITE_CODE_MASTER", ProfitCashflow.oPcfDM.conn);
                        cmd.Parameters.AddWithValue("@IN_COMPANY_ID", drMas["COMPANY_ID", DataRowVersion.Original]);
                        cmd.Parameters.AddWithValue("@IV_B4_SITE_CODE", drMas["B4_SITE_CODE", DataRowVersion.Original]);

                        goto L1;
                    }

                    cmd.Parameters.AddWithValue("@IN_COMPANY_ID", drMas["COMPANY_ID"]);
                    if (drMas.RowState == DataRowState.Modified)
                        cmd.Parameters.AddWithValue("@IV_B4_SITE_CODE_OLD", drMas["B4_SITE_CODE", DataRowVersion.Original]);

                    cmd.Parameters.AddWithValue("@IV_B4_SITE_CODE_NEW", drMas["B4_SITE_CODE", DataRowVersion.Current]);
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
                    cmd.Dispose();
                }
            }
        }
    }
}
