using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Transactions;

namespace Xpedeon.ProfitCashflow
{
    class sAspectMas
    {
        public DataTable RetrieveCompany(string sUser, string isActive)
        {
            DataTable dtCompanyMas = new DataTable();
            using (SqlDataAdapter da = new SqlDataAdapter())
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

        public DataTable RetrieveAspectMaster(decimal dCompanyId)
        {
            DataTable dtAspectMas = new DataTable();
            using (SqlDataAdapter da = new SqlDataAdapter())
            {
                da.SelectCommand = new SqlCommand("SPN_XP_GET_ASPECT");
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@IN_COMPANY_ID", SqlDbType.Decimal).Value = dCompanyId;
                da.SelectCommand.Connection = ProfitCashflow.oPcfDM.conn;

                da.Fill(dtAspectMas);
            }

            return dtAspectMas;
        }

        public int RetrieveMaxAspectId(decimal dCompanyId)
        {
            int oResult;
            using (SqlCommand cmd = new SqlCommand("SPN_XP_MAX_ASPECT_ID"))
            {
                cmd.Connection = ProfitCashflow.oPcfDM.conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@IN_COMPANY_ID", SqlDbType.Decimal).Value = dCompanyId;

                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                oResult = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return oResult;
        }

        public int CheckAspectMaster(decimal dCompanyId, string dAspectName)
        {
            int oResult;
            using (SqlCommand cmd = new SqlCommand("SPN_XP_CHECK_ASPECT", ProfitCashflow.oPcfDM.conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@IN_COMPANY_ID", SqlDbType.Decimal).Value = dCompanyId;
                cmd.Parameters.Add("@IV_ASPECT_NAME", SqlDbType.VarChar).Value = dAspectName;

                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                oResult = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return oResult;
        }
        
        public int CheckDelAspectMaster(decimal dCompanyId, decimal dAspectId)
        {
            int oResult;
            using (SqlCommand cmd = new SqlCommand("SPN_XP_DEL_CHECK_ASPECT_PROFIT"))
            {
                cmd.Connection = ProfitCashflow.oPcfDM.conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@IN_COMPANY_ID", SqlDbType.Decimal).Value = dCompanyId;
                cmd.Parameters.Add("@IN_ASPECT_ID", SqlDbType.VarChar).Value = dAspectId;

                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                oResult = Convert.ToInt32(cmd.ExecuteScalar());
            }
            return oResult;
        }

        public void UpdateAspectMaster(string sType, DataRow drAspectMas)
        {
            TransactionOptions to = new TransactionOptions();
            to.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;

            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew, to, EnterpriseServicesInteropOption.Full))
            {
                SqlCommand cmd = new SqlCommand();
                try
                {
                    if (sType == "I")
                        cmd = new SqlCommand("SPN_XP_INS_ASPECT_MASTER", ProfitCashflow.oPcfDM.conn);
                    else if (sType == "U")
                        cmd = new SqlCommand("SPN_XP_UPD_ASPECT_MASTER", ProfitCashflow.oPcfDM.conn);
                    else if (sType == "D")
                    {
                        cmd = new SqlCommand("SPN_XP_DEL_ASPECT_MASTER", ProfitCashflow.oPcfDM.conn);
                        cmd.Parameters.AddWithValue("@IN_COMPANY_ID", drAspectMas["COMPANY_ID", DataRowVersion.Original]);
                        cmd.Parameters.AddWithValue("@IN_ASPECT_ID", drAspectMas["ASPECT_ID", DataRowVersion.Original]);

                        goto L1;
                    }

                    cmd.Parameters.AddWithValue("@IN_COMPANY_ID", drAspectMas["COMPANY_ID"]);
                    cmd.Parameters.AddWithValue("@IN_ASPECT_ID", drAspectMas["ASPECT_ID"]);
                    cmd.Parameters.AddWithValue("@IV_ASPECT_NAME", drAspectMas["ASPECT_NAME"]);
                    cmd.Parameters.AddWithValue("@IV_CREATED_BY", drAspectMas["CREATED_BY"]);
                    cmd.Parameters.AddWithValue("@ID_CREATED_ON", drAspectMas["CREATED_ON"]);
                    cmd.Parameters.AddWithValue("@ID_LAST_UPDATED", drAspectMas["LAST_UPDATED"]);

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
