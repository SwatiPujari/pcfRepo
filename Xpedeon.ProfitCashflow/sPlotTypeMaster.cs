using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Transactions;

namespace Xpedeon.ProfitCashflow
{
    class sPlotTypeMaster
    {
        public DataTable RetrieveMaster()
        {
            DataTable dtMas = new DataTable();
            using(SqlDataAdapter da = new SqlDataAdapter())
            {
                da.SelectCommand = new SqlCommand("SPN_XP_GET_PLOT_TYPE", ProfitCashflow.oPcfDM.conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.Fill(dtMas);
            }

            return dtMas;
        }

        public int RetrieveMaxId()
        {
            int oResult;
            using(SqlCommand cmd = new SqlCommand("SPN_GET_SEQ_ID", ProfitCashflow.oPcfDM.conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IC_SEQ", "SEQ_PK_PLOT_TYPE_MAS");

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

        public int CheckMaster(string sType)
        {
            object oResult;
            using(SqlCommand cmd = new SqlCommand("SPN_XP_CHECK_PLOT_TYPE", ProfitCashflow.oPcfDM.conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IV_PLOT_TYPE", sType);

                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                oResult = cmd.ExecuteScalar();
            }

            return (int)oResult;
        }

        public int CheckDeleteMaster(decimal dType)
        {
            object oResult;
            using(SqlCommand cmd = new SqlCommand("SPN_XP_DEL_CHECK_PLOT_TYPE", ProfitCashflow.oPcfDM.conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IN_PLOT_TYPE_ID", dType);

                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                oResult = cmd.ExecuteScalar();
            }

            return (int)oResult;
        }

        public void UpdateMaster(string sType, DataRow drPurTypeMaster)
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
                        cmd = new SqlCommand("SPN_XP_INS_PLOT_TYPE_MASTER", ProfitCashflow.oPcfDM.conn);
                    else if (sType == "U")
                        cmd = new SqlCommand("SPN_XP_UPD_PLOT_TYPE_MASTER", ProfitCashflow.oPcfDM.conn);
                    else if (sType == "D")
                    {
                        cmd = new SqlCommand("SPN_XP_DEL_PLOT_TYPE_MASTER", ProfitCashflow.oPcfDM.conn);
                        cmd.Parameters.AddWithValue("@IN_PLOT_TYPE_ID", drPurTypeMaster["PLOT_TYPE_ID", DataRowVersion.Original]);
                        goto L1;
                    }

                    cmd.Parameters.AddWithValue("@IN_PLOT_TYPE_ID", drPurTypeMaster["PLOT_TYPE_ID"]);
                    cmd.Parameters.AddWithValue("@IV_PLOT_TYPE", drPurTypeMaster["PLOT_TYPE"]);
                    cmd.Parameters.AddWithValue("@IV_CREATED_BY", drPurTypeMaster["CREATED_BY"]);
                    cmd.Parameters.AddWithValue("@ID_CREATED_ON", drPurTypeMaster["CREATED_ON"]);
                    cmd.Parameters.AddWithValue("@ID_LAST_UPDATED", drPurTypeMaster["LAST_UPDATED"]);
                    cmd.Parameters.AddWithValue("@IC_SYS_DEF_TYPE", drPurTypeMaster["SYS_DEF_TYPE"]);
                    cmd.Parameters.AddWithValue("@IC_SQFT_MANDATORY", drPurTypeMaster["SQFT_MANDATORY"]);

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
