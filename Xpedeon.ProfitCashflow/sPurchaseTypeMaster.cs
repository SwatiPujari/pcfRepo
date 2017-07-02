using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Transactions;

namespace Xpedeon.ProfitCashflow
{
    class sPurchaseTypeMaster
    {
        public DataTable RetrievePurchaseMaster()
        {
            DataTable dt = new DataTable();
            using (SqlDataAdapter da = new SqlDataAdapter())
            {
                da.SelectCommand = new SqlCommand("SPN_XP_GET_PURCHASE", ProfitCashflow.oPcfDM.conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.Fill(dt);
            }

            return dt;
        }

        public int RetrieveMaxPurchaseId()
        {
            int oResult;
            using (SqlCommand cmd = new SqlCommand("SPN_GET_SEQ_ID", ProfitCashflow.oPcfDM.conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IC_SEQ", "SEQ_PK_PUR_TYPE_MAS");

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

        public int CheckPurchaseMaster(string sPurchaseType)
        {
            int oResult;
            using (SqlCommand cmd = new SqlCommand("SPN_XP_CHECK_PURCHASE", ProfitCashflow.oPcfDM.conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IV_DESCRIPTION", sPurchaseType);

                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                oResult = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return oResult;
        }

        public int CheckDeletePurchaseMaster(decimal dPurchaseType)
        {
            int iResult;
            using (SqlCommand cmd = new SqlCommand("SPN_XP_DEL_CHECK_PURCHASE", ProfitCashflow.oPcfDM.conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IN_PURCHASE_TYPE_ID", dPurchaseType);

                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                iResult = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return iResult;
        }

        public void UpdatePurchaseMaster(string sType, DataRow drPurTypeMaster)
        {
            TransactionOptions to = new TransactionOptions();
            to.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;

            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew, to, EnterpriseServicesInteropOption.Full))
            {
                SqlCommand cmd = new SqlCommand();
                try
                {
                    if (sType == "I")
                        cmd = new SqlCommand("SPN_XP_INS_PURCHASE_TYPE_MASTER", ProfitCashflow.oPcfDM.conn);
                    else if (sType == "U")
                        cmd = new SqlCommand("SPN_XP_UPD_PURCHASE_TYPE_MASTER", ProfitCashflow.oPcfDM.conn);
                    else if (sType == "D")
                    {
                        cmd = new SqlCommand("SPN_XP_DEL_PURCHASE_TYPE_MASTER", ProfitCashflow.oPcfDM.conn);
                        cmd.Parameters.AddWithValue("@IN_PURCHASE_TYPE_ID", drPurTypeMaster["PURCHASE_TYPE_ID", DataRowVersion.Original]);
                        goto L1;
                    }

                    cmd.Parameters.AddWithValue("@IN_PURCHASE_TYPE_ID", drPurTypeMaster["PURCHASE_TYPE_ID"]);
                    cmd.Parameters.AddWithValue("@IV_DESCRIPTION", drPurTypeMaster["DESCRIPTION"]);
                    cmd.Parameters.AddWithValue("@IV_CREATED_BY", drPurTypeMaster["CREATED_BY"]);
                    cmd.Parameters.AddWithValue("@ID_CREATED_ON", drPurTypeMaster["CREATED_ON"]);
                    cmd.Parameters.AddWithValue("@ID_LAST_UPDATED", drPurTypeMaster["LAST_UPDATED"]);

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
