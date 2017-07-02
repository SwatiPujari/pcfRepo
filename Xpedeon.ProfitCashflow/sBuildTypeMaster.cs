using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Transactions;

namespace Xpedeon.ProfitCashflow
{
    class sBuildTypeMaster
    {
        public DataTable RetrieveBuildTypeMaster()
        {
            DataTable dt = new DataTable();
            using (SqlDataAdapter da = new SqlDataAdapter())
            {
                da.SelectCommand = new SqlCommand("SPN_XP_GET_BUILD_TYPE", ProfitCashflow.oPcfDM.conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.Fill(dt);
            }

            return dt;
        }

        public int RetrieveMaxBuildTypeId()
        {
            int iResult;
            using (SqlCommand cmd = new SqlCommand("SPN_XP_MAX_BUILD_ID", ProfitCashflow.oPcfDM.conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                iResult = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return iResult;
        }

        public int CheckBuildTypeMaster(string sBuildType)
        {
            int oResult;
            using (SqlCommand cmd = new SqlCommand("SPN_XP_CHECK_BUILD_TYPE", ProfitCashflow.oPcfDM.conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IV_BUILD_TYPE_NAME", sBuildType);

                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                oResult = Convert.ToInt32(cmd.ExecuteScalar());
            }
          
            return oResult;
        }

        public int CheckDeleteBuildTypeMaster(decimal dBuildType)
        {
            int iResult;
            using (SqlCommand cmd = new SqlCommand("SPN_XP_DEL_CHECK_BUILD_TYPE", ProfitCashflow.oPcfDM.conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IN_BUILD_TYPE_ID", dBuildType);

                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                iResult = Convert.ToInt32(cmd.ExecuteScalar());
            }
            return iResult;
        }

        public void UpdateBuildTypeMaster(string sType, DataRow drPurTypeMaster)
        {
            TransactionOptions to = new TransactionOptions();
            to.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;

            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew, to, EnterpriseServicesInteropOption.Full))
            {
                SqlCommand cmd = new SqlCommand();
                try
                {
                    if (sType == "I")
                        cmd = new SqlCommand("SPN_XP_INS_BUILD_TYPE_MASTER", ProfitCashflow.oPcfDM.conn);
                    else if (sType == "U")
                        cmd = new SqlCommand("SPN_XP_UPD_BUILD_TYPE_MASTER", ProfitCashflow.oPcfDM.conn);
                    else if (sType == "D")
                    {
                        cmd = new SqlCommand("SPN_XP_DEL_BUILD_TYPE_MASTER", ProfitCashflow.oPcfDM.conn);
                        cmd.Parameters.AddWithValue("@IN_BUILD_TYPE_ID", drPurTypeMaster["BUILD_TYPE_ID", DataRowVersion.Original]);
                        goto L1;
                    }

                    cmd.Parameters.AddWithValue("@IN_BUILD_TYPE_ID", drPurTypeMaster["BUILD_TYPE_ID"]);
                    cmd.Parameters.AddWithValue("@IV_BUILD_TYPE_NAME", drPurTypeMaster["BUILD_TYPE_NAME"]);
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
