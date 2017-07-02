using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Transactions;

namespace Xpedeon.ProfitCashflow
{
    class sUnitCategoryTypeMaster
    {
        public DataTable RetrieveMaster()
        {
            DataTable dt = new DataTable();
            using (SqlDataAdapter da = new SqlDataAdapter())
            {
                da.SelectCommand = new SqlCommand("SPN_XP_GET_UNIT_CATEGORY", ProfitCashflow.oPcfDM.conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.Fill(dt);
            }

            return dt;
        }

        public DataTable RetrievePlotMaster()
        {
            DataTable dt = new DataTable();
            using (SqlDataAdapter da = new SqlDataAdapter())
            {
                da.SelectCommand = new SqlCommand("SPN_XP_GET_PLOT_TYPE", ProfitCashflow.oPcfDM.conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.Fill(dt);
            }

            return dt;
        }

        public int RetrieveMaxId()
        {
            int iResult;
            using (SqlCommand cmd = new SqlCommand("SPN_XP_MAX_UNIT_CATEGORY_ID", ProfitCashflow.oPcfDM.conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                iResult = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return iResult;
        }

        public int CheckMaster(string sMode, decimal? dResCategoryId, decimal? dPlotId, string sDescription)
        {
            int oResult;
            using (SqlCommand cmd = new SqlCommand("SPN_XP_CHECK_UNIT_CATEGORY", ProfitCashflow.oPcfDM.conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IC_MODE", sMode);
                if (dResCategoryId != null)
                    cmd.Parameters.AddWithValue("@IN_RESIDENCE_CATEGORY_ID", dResCategoryId);
                if (dPlotId != null)
                    cmd.Parameters.AddWithValue("@IN_PLOT_TYPE_ID", dPlotId);
                if (sDescription != string.Empty)
                    cmd.Parameters.AddWithValue("@IV_DESCRIPTION", sDescription);

                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                oResult = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return oResult;
        }

        public int CheckDeleteMaster(decimal dPlotType, decimal dResType)
        {
            int oResult;
            using (SqlCommand cmd = new SqlCommand("SPN_XP_DEL_CHECK_UNIT_CATEGORY", ProfitCashflow.oPcfDM.conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IN_PLOT_TYPE_ID", dPlotType);
                cmd.Parameters.AddWithValue("@IN_RESIDENCE_TYPE_ID", dResType);

                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                oResult = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return oResult;
        }

        public void UpdateMaster(string sType, DataRow dr)
        {
            TransactionOptions to = new TransactionOptions();
            to.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;

            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew, to, EnterpriseServicesInteropOption.Full))
            {
                SqlCommand cmd = new SqlCommand();
                try
                {
                    if (sType == "I")
                        cmd = new SqlCommand("SPN_XP_INS_UNIT_CATEGORY_MASTER", ProfitCashflow.oPcfDM.conn);
                    else if (sType == "U")
                        cmd = new SqlCommand("SPN_XP_UPD_UNIT_CATEGORY_MASTER", ProfitCashflow.oPcfDM.conn);
                    else if (sType == "D")
                    {
                        cmd = new SqlCommand("SPN_XP_DEL_UNIT_CATEGORY_MASTER", ProfitCashflow.oPcfDM.conn);
                        cmd.Parameters.AddWithValue("@IN_RESIDENCE_CATEGORY_ID", dr["RESIDENCE_CATEGORY_ID", DataRowVersion.Original]);
                        goto L1;
                    }

                    if (dr.RowState != DataRowState.Added)
                        cmd.Parameters.AddWithValue("@IN_PREV_PLOT_TYPE_ID", dr["PLOT_TYPE_ID", DataRowVersion.Original]);

                    cmd.Parameters.AddWithValue("@IN_NEW_PLOT_TYPE_ID", dr["PLOT_TYPE_ID", DataRowVersion.Current]);
                    cmd.Parameters.AddWithValue("@IN_RESIDENCE_TYPE_ID", dr["RESIDENCE_TYPE_ID"]);
                    cmd.Parameters.AddWithValue("@IN_RESIDENCE_CATEGORY_ID", dr["RESIDENCE_CATEGORY_ID"]);
                    cmd.Parameters.AddWithValue("@IV_DESCRIPTION", dr["DESCRIPTION"]);
                    cmd.Parameters.AddWithValue("@IV_CREATED_BY", dr["CREATED_BY"]);
                    cmd.Parameters.AddWithValue("@ID_CREATED_ON", dr["CREATED_ON"]);
                    cmd.Parameters.AddWithValue("@ID_LAST_UPDATED", dr["LAST_UPDATED"]);

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
