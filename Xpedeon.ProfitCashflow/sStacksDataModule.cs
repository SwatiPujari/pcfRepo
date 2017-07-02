using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Transactions;

namespace Xpedeon.ProfitCashflow
{
    public class sStacksDataModule
    {
        public decimal dNewStackId, dFinalisedPlotCount;
        public DataTable RetrievePlotTypeMaster()
        {
            SqlDataAdapter da = null;
            DataTable dtPlotTypeMast = new DataTable();

            try
            {
                da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("SPN_XP_GET_PLOT_TYPE_MASTER", ProfitCashflow.oPcfDM.conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;

                da.Fill(dtPlotTypeMast);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (da != null) da.Dispose();
            }

            return dtPlotTypeMast;
        }

        public DataTable RetrieveResTypeMaster()
        {
            SqlDataAdapter da = null;
            DataTable dtResTypeMas = new DataTable();

            try
            {
                da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("SPN_XP_GET_RES_TYPE_MASTER", ProfitCashflow.oPcfDM.conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;

                da.Fill(dtResTypeMas);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (da != null) da.Dispose();
            }

            return dtResTypeMas;
        }

        public DataTable RetrieveStackCompanyList(string sUserName)
        {
            SqlDataAdapter da = null;
            DataTable dtCompList = new DataTable();

            try
            {
                da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("SPN_XP_GET_STK_COMPANY", ProfitCashflow.oPcfDM.conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@IV_PCF_USER", SqlDbType.VarChar, 30).Value = sUserName;

                da.Fill(dtCompList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (da != null) da.Dispose();
            }

            return dtCompList;
        }

        public DataTable RetrieveStackSiteList(decimal dCompId)
        {
            SqlDataAdapter da = null;
            DataTable dtSiteList = new DataTable();

            try
            {
                da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("SPN_XP_GET_STK_SITE_BY_COMP", ProfitCashflow.oPcfDM.conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@IN_COMPANY_ID", SqlDbType.Decimal, 10).Value = dCompId;

                da.Fill(dtSiteList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (da != null) da.Dispose();
            }

            return dtSiteList;
        }

        public DataTable RetrieveStackSiteNewList(decimal dCompId)
        {
            SqlDataAdapter da = null;
            DataTable dtSiteNewList = new DataTable();

            try
            {
                da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("SPN_XP_GET_STK_SITE_NEW_BY_COMP", ProfitCashflow.oPcfDM.conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@IN_COMPANY_ID", SqlDbType.Decimal, 10).Value = dCompId;

                da.Fill(dtSiteNewList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (da != null) da.Dispose();
            }

            return dtSiteNewList;
        }

        public DataTable RetrieveAllStacks(decimal dCompId, string sSiteCode)
        {
            SqlDataAdapter da = null;
            DataTable dtAllStacks = new DataTable();

            try
            {
                da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("SPN_XP_GET_STK_ALL_STACKS", ProfitCashflow.oPcfDM.conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;

                da.SelectCommand.Parameters.Add("@IN_COMPANY_ID", SqlDbType.Decimal, 10).Value = dCompId;
                if (!string.IsNullOrWhiteSpace(sSiteCode))
                    da.SelectCommand.Parameters.Add("@IV_SITE_CODE", SqlDbType.VarChar, 30).Value = sSiteCode;

                da.Fill(dtAllStacks);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (da != null) da.Dispose();
            }

            return dtAllStacks;
        }

        public DataTable RetrieveActiveStacks(decimal dCompId, string sSiteCode)
        {
            SqlDataAdapter da = null;
            DataTable dtActiveStacks = new DataTable();

            try
            {
                da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("SPN_XP_GET_STK_ACTIVE_STACK", ProfitCashflow.oPcfDM.conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;

                da.SelectCommand.Parameters.Add("@IN_COMPANY_ID", SqlDbType.Decimal, 10).Value = dCompId;
                da.SelectCommand.Parameters.Add("@IV_SITE_CODE", SqlDbType.VarChar, 30).Value = sSiteCode;

                da.Fill(dtActiveStacks);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (da != null) da.Dispose();
            }

            return dtActiveStacks;
        }

        public DataTable RetrieveStacksById(decimal dStackId)
        {
            SqlDataAdapter da = null;
            DataTable dtStack = new DataTable();

            try
            {
                da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("SPN_XP_GET_STK_STACK_BY_ID", ProfitCashflow.oPcfDM.conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@IN_STACK_ID", SqlDbType.Decimal, 10).Value = dStackId;

                da.Fill(dtStack);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (da != null) da.Dispose();
            }

            return dtStack;
        }

        public DataTable RetrieveStackPlotDet(decimal dStackId, int iDivMulBy)
        {
            SqlDataAdapter da = null;
            DataTable dtStackPlotDet = new DataTable();

            try
            {
                da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("SPN_XP_GET_STK_STACK_PLOT_DET", ProfitCashflow.oPcfDM.conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;

                da.SelectCommand.Parameters.Add("@IN_STACK_ID", SqlDbType.Decimal, 10).Value = dStackId;
                da.SelectCommand.Parameters.Add("@IN_DIV_MUL_BY", SqlDbType.Int).Value = iDivMulBy;

                da.Fill(dtStackPlotDet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (da != null) da.Dispose();
            }

            return dtStackPlotDet;
        }

        public DataTable RetrieveStackPhaseDet(decimal dStackId)
        {
            SqlDataAdapter da = null;
            DataTable dtStackPhaseDet = new DataTable();

            try
            {
                da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("SPN_XP_GET_STK_STACK_PHASE_DET", ProfitCashflow.oPcfDM.conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@IN_STACK_ID", SqlDbType.Decimal, 10).Value = dStackId;

                da.Fill(dtStackPhaseDet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (da != null) da.Dispose();
            }

            return dtStackPhaseDet;
        }

        public DataTable RetrieveStackBlockDet(decimal dStackId)
        {
            SqlDataAdapter da = null;
            DataTable dtStackBlockDet = new DataTable();

            try
            {
                da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("SPN_XP_GET_STK_STACK_BLOCK_DET", ProfitCashflow.oPcfDM.conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@IN_STACK_ID", SqlDbType.Decimal, 10).Value = dStackId;

                da.Fill(dtStackBlockDet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (da != null) da.Dispose();
            }

            return dtStackBlockDet;
        }

        public DataTable RetrieveStackCoreDet(decimal dStackId)
        {
            SqlDataAdapter da = null;
            DataTable dtStackCoreDet = new DataTable();

            try
            {
                da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("SPN_XP_GET_STK_STACK_CORE_DET", ProfitCashflow.oPcfDM.conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@IN_STACK_ID", SqlDbType.Decimal, 10).Value = dStackId;

                da.Fill(dtStackCoreDet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (da != null) da.Dispose();
            }

            return dtStackCoreDet;
        }

        public DataTable RetrieveStackDocuments(decimal dStackId)
        {
            SqlDataAdapter da = null;
            DataTable dtStackDocs = new DataTable();

            try
            {
                da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("SPN_XP_GET_STK_STACK_DOCS", ProfitCashflow.oPcfDM.conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@IN_STACK_ID", SqlDbType.Decimal, 10).Value = dStackId;

                da.Fill(dtStackDocs);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (da != null) da.Dispose();
            }

            return dtStackDocs;
        }

        public DataTable RetrieveStackPFInfo(decimal dStackId)
        {
            SqlDataAdapter da = null;
            DataTable dtStackPFInfo = new DataTable();

            try
            {
                da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("SPN_XP_GET_STK_STACK_PF_INFO", ProfitCashflow.oPcfDM.conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@IN_STACK_ID", SqlDbType.Decimal, 10).Value = dStackId;

                da.Fill(dtStackPFInfo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (da != null) da.Dispose();
            }

            return dtStackPFInfo;
        }

        public DataTable RetrieveStackHistory(decimal dCompId, string sSiteCode)
        {
            SqlDataAdapter da = null;
            DataTable dtStackHistory = new DataTable();

            try
            {
                da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("SPN_XP_GET_STK_STACK_HISTORY", ProfitCashflow.oPcfDM.conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;

                da.SelectCommand.Parameters.Add("@IN_COMPANY_ID", SqlDbType.Decimal, 10).Value = dCompId;
                da.SelectCommand.Parameters.Add("@IV_SITE_CODE", SqlDbType.VarChar, 30).Value = sSiteCode;

                da.Fill(dtStackHistory);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (da != null) da.Dispose();
            }

            return dtStackHistory;
        }

        public DataTable RetrieveStackSummary(decimal dStackId, int iDivMulBy)
        {
            SqlDataAdapter da = null;
            DataTable dtStackSumm = new DataTable();

            try
            {
                da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("SPN_XP_GET_STK_STACK_SUMMARY", ProfitCashflow.oPcfDM.conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;

                da.SelectCommand.Parameters.Add("@IN_STACK_ID", SqlDbType.Decimal, 10).Value = dStackId;
                da.SelectCommand.Parameters.Add("@IN_DIV_MUL_BY", SqlDbType.Int).Value = iDivMulBy;

                da.Fill(dtStackSumm);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (da != null) da.Dispose();
            }

            return dtStackSumm;
        }

        public DataTable RetrieveStackSitePlotRange(decimal dCompId, string sSiteCode)
        {
            SqlDataAdapter da = null;
            DataTable dtPlotRange = new DataTable();

            try
            {
                da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("SPN_XP_GET_STK_SITE_PLOT_RANGE", ProfitCashflow.oPcfDM.conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;

                da.SelectCommand.Parameters.Add("@IN_COMPANY_ID", SqlDbType.Decimal, 10).Value = dCompId;
                da.SelectCommand.Parameters.Add("@IV_SITE_CODE", SqlDbType.VarChar, 30).Value = sSiteCode;

                da.Fill(dtPlotRange);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (da != null) da.Dispose();
            }

            return dtPlotRange;
        }

        public DataRow RetrieveStackSiteDetail(decimal dCompId, string sSiteCode)
        {
            SqlDataAdapter da = null;
            DataRow drStkSiteInfo = null;
            DataTable dtStkSiteInfo = new DataTable();

            try
            {
                da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("SPN_XP_GET_STK_SITE_DET", ProfitCashflow.oPcfDM.conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;

                da.SelectCommand.Parameters.Add("@IN_COMPANY_ID", SqlDbType.Decimal, 10).Value = dCompId;
                da.SelectCommand.Parameters.Add("@IV_SITE_CODE", SqlDbType.VarChar, 30).Value = sSiteCode;

                da.Fill(dtStkSiteInfo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (da != null) da.Dispose();
            }

            if (dtStkSiteInfo.Rows.Count > 0)
                drStkSiteInfo = dtStkSiteInfo.Rows[0];

            return drStkSiteInfo;
        }

        public DataRow RetrieveStackUserRights(string sUserName)
        {
            SqlDataAdapter da = null;
            DataRow drStkUserRights = null;
            DataTable dtStkUserRights = new DataTable();

            try
            {
                da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("SPN_XP_GET_STK_USER_RIGHTS", ProfitCashflow.oPcfDM.conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@IV_USERNAME", SqlDbType.VarChar, 30).Value = sUserName;

                da.Fill(dtStkUserRights);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (da != null) da.Dispose();
            }

            if (dtStkUserRights.Rows.Count > 0)
                drStkUserRights = dtStkUserRights.Rows[0];

            return drStkUserRights;
        }

        public bool Chk_PartExchnage(decimal dCompId, string sSiteCode)
        {
            object oResult = new object();
            SqlCommand cmd = null;

            try
            {
                cmd = new SqlCommand("SELECT PART_EXCHANGE FROM SITE_SETUP WHERE COMPANY_ID=@IN_COMPANY_ID AND SITE_CODE=@IV_SITE_CODE", ProfitCashflow.oPcfDM.conn);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add("@IN_COMPANY_ID", SqlDbType.Decimal, 10).Value = dCompId;
                cmd.Parameters.Add("@IV_SITE_CODE", SqlDbType.VarChar, 30).Value = sSiteCode;

                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                oResult = cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (cmd != null) cmd.Dispose();
            }

            if (oResult != null && !string.IsNullOrWhiteSpace(Convert.ToString(oResult)) && Convert.ToString(oResult) == "Y")
                return true;
            else
                return false;
        }

        public DataTable RetrieveStackSysCreatedPhase(decimal dSysStkId)
        {
            SqlDataAdapter da = null;
            DataTable dtStkSysPhase = new DataTable();

            try
            {
                da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("SPN_XP_GET_STK_SYS_CRE_PHASE", ProfitCashflow.oPcfDM.conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@IN_SYS_CREATED_STACK_ID", SqlDbType.Decimal, 10).Value = dSysStkId;

                da.Fill(dtStkSysPhase);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (da != null) da.Dispose();
            }

            return dtStkSysPhase;
        }

        /*public void DeleteStack(DataRow drStack, string lIsCurrent)
        {
            string sQuery = "";
            List<KeyValuePair<string, object>> oParameter = null;
            string str_oldStack_status, str_newstack_code;

            TransactionOptions to = new TransactionOptions();
            to.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;

            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew, to, EnterpriseServicesInteropOption.Full))
            {
                try
                {
                    sQuery = "UPDATE STACKS SET NEXT_STACK_ID = NULL ";
                    sQuery += "WHERE NEXT_STACK_ID = @IN_STACK_ID";
                    oParameter = new List<KeyValuePair<string, object>>();
                    oParameter.Add(new KeyValuePair<string, object>("@IN_STACK_ID", Convert.ToDecimal(drStack["STACK_ID"])));
                    ProfitCashflow.oPcfDM.SqlQryAllpurpose(sQuery, oParameter);

                    if (lIsCurrent == "Y")
                    {
                        sQuery = "UPDATE SITE_SETUP SET CURRENT_STACK_ID = NULL ";
                        sQuery += "WHERE COMPANY_ID = @IN_COMPANY_ID AND SITE_CODE = @IV_SITE_CODE";
                        oParameter = new List<KeyValuePair<string, object>>();
                        oParameter.Add(new KeyValuePair<string, object>("@IN_COMPANY_ID", Convert.ToDecimal(drStack["COMPANY_ID"])));
                        oParameter.Add(new KeyValuePair<string, object>("@IV_SITE_CODE", Convert.ToString(drStack["SITE_CODE"])));
                        ProfitCashflow.oPcfDM.SqlQryAllpurpose(sQuery, oParameter);
                    }

                    sQuery = "DELETE STACK_DOCUMENTS WHERE STACK_ID=" + drStack["STACK_ID"].ToString();
                    ProfitCashflow.oPcfDM.SqlQryAllpurpose(sQuery, null);

                    //--sQuery = "DELETE STACK_GROUND_RENT_DETAILS WHERE STACK_ID=" + drStack["STACK_ID"].ToString();
                    ProfitCashflow.oPcfDM.SqlQryAllpurpose(sQuery, null);

                    sQuery = "DELETE STACK_PARKING_DETAILS WHERE STACK_ID=" + drStack["STACK_ID"].ToString();
                    ProfitCashflow.oPcfDM.SqlQryAllpurpose(sQuery, null);--//

                    sQuery = "DELETE STACK_PLOT_DETAILS WHERE STACK_ID=" + drStack["STACK_ID"].ToString();
                    ProfitCashflow.oPcfDM.SqlQryAllpurpose(sQuery, null);

                    sQuery = "DELETE STACK_PHASE_DETAILS WHERE STACK_ID=" + drStack["STACK_ID"].ToString();
                    ProfitCashflow.oPcfDM.SqlQryAllpurpose(sQuery, null);

                    sQuery = "DELETE STACK_BLOCK_DETAILS WHERE STACK_ID=" + drStack["STACK_ID"].ToString();
                    ProfitCashflow.oPcfDM.SqlQryAllpurpose(sQuery, null);

                    sQuery = "DELETE STACK_CORE_DETAILS WHERE STACK_ID=" + drStack["STACK_ID"].ToString();
                    ProfitCashflow.oPcfDM.SqlQryAllpurpose(sQuery, null);

                    sQuery = "DELETE STACKS WHERE STACK_ID=" + drStack["STACK_ID"].ToString();
                    ProfitCashflow.oPcfDM.SqlQryAllpurpose(sQuery, null);

                    //-------------------UPDATING NEXT_LANDSTATUS FIELD IN DATABASE-----------------------------------
                    str_oldStack_status = Convert.ToString(drStack["STACK_STATUS"]);

                    sQuery = "SELECT COUNT(1) AS CNT FROM STACKS ";
                    sQuery += "WHERE COMPANY_ID = @IN_COMPANY_ID AND SITE_CODE = @IV_SITE_CODE ";
                    sQuery += "AND STACK_STATUS = @IC_STACK_STATUS";
                    oParameter = new List<KeyValuePair<string, object>>();
                    oParameter.Add(new KeyValuePair<string, object>("@IN_COMPANY_ID", Convert.ToDecimal(drStack["COMPANY_ID"])));
                    oParameter.Add(new KeyValuePair<string, object>("@IV_SITE_CODE", Convert.ToString(drStack["SITE_CODE"])));
                    oParameter.Add(new KeyValuePair<string, object>("@IC_STACK_STATUS", str_oldStack_status));
                    object oQryResult = ProfitCashflow.oPcfDM.SqlQryAllpurpose(sQuery, oParameter, true);

                    if (oQryResult != null && Convert.ToInt16(oQryResult) == 0 && str_oldStack_status != "O")
                    {
                        str_newstack_code = "";
                        if (str_oldStack_status == "R")
                            str_newstack_code = "I";
                        else if (str_oldStack_status == "I")
                            str_newstack_code = "E";
                        else if (str_oldStack_status == "E")
                            str_newstack_code = "P";
                        else if (str_oldStack_status == "P")
                            str_newstack_code = "O";

                        sQuery = "UPDATE STACKS SET NEXT_STACKSTATUS = 'N' ";
                        sQuery += "WHERE COMPANY_ID = @IN_COMPANY_ID AND SITE_CODE = @IV_SITE_CODE ";
                        sQuery += "AND STACK_STATUS = @IC_STACK_STATUS";
                        oParameter = new List<KeyValuePair<string, object>>();
                        oParameter.Add(new KeyValuePair<string, object>("@IN_COMPANY_ID", Convert.ToDecimal(drStack["COMPANY_ID"])));
                        oParameter.Add(new KeyValuePair<string, object>("@IV_SITE_CODE", Convert.ToString(drStack["SITE_CODE"])));
                        oParameter.Add(new KeyValuePair<string, object>("@IC_STACK_STATUS", str_newstack_code));
                        ProfitCashflow.oPcfDM.SqlQryAllpurpose(sQuery, oParameter);
                    }

                    ts.Complete();
                }
                catch (Exception ex)
                {
                    ts.Dispose();
                    throw ex;
                }
            }
        }*/

        public void UpdateStackSystem(DataTable dtStack, DataTable dtStkPhaseDet, DataTable dtStkBlockDet, DataTable dtStkCoreDet, DataTable dtStkPlotDet, DataTable dtStkDocs)
        {
            TransactionOptions to = new TransactionOptions();
            to.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew, to, EnterpriseServicesInteropOption.Full))
            {
                try
                {
                    if (dtStack.GetChanges() != null)
                        UpdateStackDetails(dtStack.GetChanges());
                    if (dtStkPhaseDet.GetChanges() != null)
                        UpdateStackPhaseDetails(dtStkPhaseDet.GetChanges());
                    if (dtStkBlockDet.GetChanges() != null)
                        UpdateStackBlockDetails(dtStkBlockDet.GetChanges());
                    if (dtStkCoreDet.GetChanges() != null)
                        UpdateStackCoreDetails(dtStkCoreDet.GetChanges());
                    if (dtStkPlotDet.GetChanges() != null)
                        UpdateStackPlotDet(dtStkPlotDet.GetChanges());
                    if (dtStkDocs.GetChanges() != null)
                        UpdateStackDocument(dtStkDocs.GetChanges());

                    ts.Complete();
                }
                catch (Exception ex)
                {
                    ts.Dispose();
                    throw ex;
                }
            }
        }
        
        public void UpdateStackDetails(DataTable dtStack)
        {
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                da.InsertCommand = new SqlCommand("SPN_XP_INS_STK_STACK", ProfitCashflow.oPcfDM.conn);
                da.InsertCommand.CommandType = CommandType.StoredProcedure;
                CreateStackCommand(da.InsertCommand);

                da.UpdateCommand = new SqlCommand("SPN_XP_UPD_STK_STACK", ProfitCashflow.oPcfDM.conn);
                da.UpdateCommand.CommandType = CommandType.StoredProcedure;
                CreateStackCommand(da.UpdateCommand);

                da.DeleteCommand = new SqlCommand("SPN_XP_DEL_STK_STACK", ProfitCashflow.oPcfDM.conn);
                da.DeleteCommand.CommandType = CommandType.StoredProcedure;
                da.DeleteCommand.Parameters.Add(CreateSqlParameter("@IN_STACK_ID", SqlDbType.Decimal, 10, "STACK_ID", System.Data.ParameterDirection.Input, DataRowVersion.Current));

                da.Update(dtStack);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (da != null) da.Dispose();
            }
        }

        public void UpdateStack(DataTable dtStack)  //Mainly to Delete stack
        {
            TransactionOptions to = new TransactionOptions();
            to.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew, to, EnterpriseServicesInteropOption.Full))
            {
                SqlDataAdapter da = new SqlDataAdapter();
                try
                {
                    da.InsertCommand = new SqlCommand("SPN_XP_INS_STK_STACK", ProfitCashflow.oPcfDM.conn);
                    da.InsertCommand.CommandType = CommandType.StoredProcedure;
                    CreateStackCommand(da.InsertCommand);

                    da.UpdateCommand = new SqlCommand("SPN_XP_UPD_STK_STACK", ProfitCashflow.oPcfDM.conn);
                    da.UpdateCommand.CommandType = CommandType.StoredProcedure;
                    CreateStackCommand(da.UpdateCommand);

                    da.DeleteCommand = new SqlCommand("SPN_XP_DEL_STK_STACK", ProfitCashflow.oPcfDM.conn);
                    da.DeleteCommand.CommandType = CommandType.StoredProcedure;
                    da.DeleteCommand.Parameters.Add(CreateSqlParameter("@IN_STACK_ID", SqlDbType.Decimal, 10, "STACK_ID", System.Data.ParameterDirection.Input, DataRowVersion.Current));

                    da.Update(dtStack);
                    ts.Complete();
                }
                catch (Exception ex)
                {
                    ts.Dispose();
                    throw ex;
                }
                finally
                {
                    if (da != null) da.Dispose();
                }
            }
        }

        private void CreateStackCommand(SqlCommand cmd)
        {
            cmd.Parameters.Add(CreateSqlParameter("@IN_STACK_ID", SqlDbType.Decimal, 10, "STACK_ID", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IN_COMPANY_ID", SqlDbType.Decimal, 10, "COMPANY_ID", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_SITE_CODE", SqlDbType.VarChar, 30, "SITE_CODE", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IN_VERSION", SqlDbType.Decimal, 2, "VERSION", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IC_IS_CURRENT", SqlDbType.Char, 1, "IS_CURRENT", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_LABEL", SqlDbType.VarChar, 100, "LABEL", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IN_TOTAL_PLOTS", SqlDbType.Decimal, 10, "TOTAL_PLOTS", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IN_TOTAL_PHASES", SqlDbType.Decimal, 10, "TOTAL_PHASES", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IN_TOTAL_BLOCKS", SqlDbType.Decimal, 10, "TOTAL_BLOCKS", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_CREATED_BY", SqlDbType.VarChar, 30, "CREATED_BY", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@ID_CREATED_ON", SqlDbType.DateTime, -1, "CREATED_ON", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@ID_LAST_UPDATED", SqlDbType.DateTime, -1, "LAST_UPDATED", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IN_PCF_DOC_GUID", SqlDbType.Decimal, 20, "PCF_DOC_GUID", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IC_STACK_STATUS", SqlDbType.Char, 1, "STACK_STATUS", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IC_FROZEN", SqlDbType.Char, 1, "FROZEN", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IC_LOCKED", SqlDbType.Char, 1, "LOCKED", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IC_PROCESSED_TO_PREV", SqlDbType.Char, 1, "PROCESSED_TO_PREV", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IN_TOTAL_CORES", SqlDbType.Decimal, 10, "TOTAL_CORES", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_COMMENTS", SqlDbType.VarChar, 255, "COMMENTS", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IC_USE_IN_REPORT", SqlDbType.Char, 1, "USE_IN_REPORT", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IN_MODE", SqlDbType.Decimal, 1, "MODE", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IC_PUSHED_TO_PF", SqlDbType.Char, 1, "PUSHED_TO_PF", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IC_NEXT_STACKSTATUS", SqlDbType.Char, 1, "NEXT_STACKSTATUS", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IC_SYS_CREATED_STACK", SqlDbType.Char, 1, "SYS_CREATED_STACK", System.Data.ParameterDirection.Input, DataRowVersion.Current));
        }

        public void UpdateStackPhaseDetails(DataTable dtStkPhaseDet)
        {
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                da.InsertCommand = new SqlCommand("SPN_XP_INS_STK_PHASE_DET", ProfitCashflow.oPcfDM.conn);
                da.InsertCommand.CommandType = CommandType.StoredProcedure;
                CreateStackPhaseDetCommand(da.InsertCommand);

                da.UpdateCommand = new SqlCommand("SPN_XP_UPD_STK_PHASE_DET", ProfitCashflow.oPcfDM.conn);
                da.UpdateCommand.CommandType = CommandType.StoredProcedure;
                CreateStackPhaseDetCommand(da.UpdateCommand);

                da.DeleteCommand = new SqlCommand("SPN_XP_DEL_STK_PHASE_DET", ProfitCashflow.oPcfDM.conn);
                da.DeleteCommand.CommandType = CommandType.StoredProcedure;
                da.DeleteCommand.Parameters.Add(CreateSqlParameter("@IN_STACK_ID", SqlDbType.Decimal, 10, "STACK_ID", System.Data.ParameterDirection.Input, DataRowVersion.Current));
                da.DeleteCommand.Parameters.Add(CreateSqlParameter("@IN_PHASE_ID", SqlDbType.Decimal, 10, "PHASE_ID", System.Data.ParameterDirection.Input, DataRowVersion.Current));

                da.Update(dtStkPhaseDet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (da != null) da.Dispose();
            }
        }

        private void CreateStackPhaseDetCommand(SqlCommand cmd)
        {
            cmd.Parameters.Add(CreateSqlParameter("@IN_STACK_ID", SqlDbType.Decimal, 10, "STACK_ID", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IN_PHASE_ID", SqlDbType.Decimal, 10, "PHASE_ID", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_PHASE", SqlDbType.VarChar, 100, "PHASE", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IC_BLDG_UNDER_LICENSE", SqlDbType.Char, 1, "BLDG_UNDER_LICENSE", System.Data.ParameterDirection.Input, DataRowVersion.Current));
        }

        public void UpdateStackBlockDetails(DataTable dtStkBlockDet)
        {
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                da.InsertCommand = new SqlCommand("SPN_XP_INS_STK_BLOCK_DET", ProfitCashflow.oPcfDM.conn);
                da.InsertCommand.CommandType = CommandType.StoredProcedure;
                CreateStackBlockDetCommand(da.InsertCommand);

                da.UpdateCommand = new SqlCommand("SPN_XP_UPD_STK_BLOCK_DET", ProfitCashflow.oPcfDM.conn);
                da.UpdateCommand.CommandType = CommandType.StoredProcedure;
                CreateStackBlockDetCommand(da.UpdateCommand);

                da.DeleteCommand = new SqlCommand("SPN_XP_DEL_STK_BLOCK_DET", ProfitCashflow.oPcfDM.conn);
                da.DeleteCommand.CommandType = CommandType.StoredProcedure;
                da.DeleteCommand.Parameters.Add(CreateSqlParameter("@IN_STACK_ID", SqlDbType.Decimal, 10, "STACK_ID", System.Data.ParameterDirection.Input, DataRowVersion.Current));
                da.DeleteCommand.Parameters.Add(CreateSqlParameter("@IN_BLOCK_ID", SqlDbType.Decimal, 10, "BLOCK_ID", System.Data.ParameterDirection.Input, DataRowVersion.Current));

                da.Update(dtStkBlockDet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (da != null) da.Dispose();
            }
        }

        private void CreateStackBlockDetCommand(SqlCommand cmd)
        {
            cmd.Parameters.Add(CreateSqlParameter("@IN_STACK_ID", SqlDbType.Decimal, 10, "STACK_ID", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IN_BLOCK_ID", SqlDbType.Decimal, 10, "BLOCK_ID", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_BLOCK_NAME", SqlDbType.VarChar, 100, "BLOCK_NAME", System.Data.ParameterDirection.Input, DataRowVersion.Current));
        }

        public void UpdateStackCoreDetails(DataTable dtStkCoreDet)
        {
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                da.InsertCommand = new SqlCommand("SPN_XP_INS_STK_CORE_DET", ProfitCashflow.oPcfDM.conn);
                da.InsertCommand.CommandType = CommandType.StoredProcedure;
                CreateStackCoreDetCommand(da.InsertCommand);

                da.UpdateCommand = new SqlCommand("SPN_XP_UPD_STK_CORE_DET", ProfitCashflow.oPcfDM.conn);
                da.UpdateCommand.CommandType = CommandType.StoredProcedure;
                CreateStackCoreDetCommand(da.UpdateCommand);

                da.DeleteCommand = new SqlCommand("SPN_XP_DEL_STK_CORE_DET", ProfitCashflow.oPcfDM.conn);
                da.DeleteCommand.CommandType = CommandType.StoredProcedure;
                da.DeleteCommand.Parameters.Add(CreateSqlParameter("@IN_STACK_ID", SqlDbType.Decimal, 10, "STACK_ID", System.Data.ParameterDirection.Input, DataRowVersion.Current));
                da.DeleteCommand.Parameters.Add(CreateSqlParameter("@IN_CORE_ID", SqlDbType.Decimal, 10, "CORE_ID", System.Data.ParameterDirection.Input, DataRowVersion.Current));

                da.Update(dtStkCoreDet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (da != null) da.Dispose();
            }
        }

        private void CreateStackCoreDetCommand(SqlCommand cmd)
        {
            cmd.Parameters.Add(CreateSqlParameter("@IN_STACK_ID", SqlDbType.Decimal, 10, "STACK_ID", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IN_CORE_ID", SqlDbType.Decimal, 10, "CORE_ID", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_CORE_NAME", SqlDbType.VarChar, 100, "CORE_NAME", System.Data.ParameterDirection.Input, DataRowVersion.Current));
        }

        public void UpdateStackPlotDet(DataTable dtStkPlotDet)
        {
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                da.InsertCommand = new SqlCommand("SPN_XP_INS_STK_PLOT_DET", ProfitCashflow.oPcfDM.conn);
                da.InsertCommand.CommandType = CommandType.StoredProcedure;
                CreateStackPlotDetCommand(da.InsertCommand);

                da.UpdateCommand = new SqlCommand("SPN_XP_UPD_STK_PLOT_DET", ProfitCashflow.oPcfDM.conn);
                da.UpdateCommand.CommandType = CommandType.StoredProcedure;
                CreateStackPlotDetCommand(da.UpdateCommand);

                da.DeleteCommand = new SqlCommand("SPN_XP_DEL_STK_PLOT_DET", ProfitCashflow.oPcfDM.conn);
                da.DeleteCommand.CommandType = CommandType.StoredProcedure;
                da.DeleteCommand.Parameters.Add(CreateSqlParameter("@IN_STACK_ID", SqlDbType.Decimal, 10, "STACK_ID", System.Data.ParameterDirection.Input, DataRowVersion.Current));
                da.DeleteCommand.Parameters.Add(CreateSqlParameter("@IN_DOCUMENT_ID", SqlDbType.Decimal, 10, "DOCUMENT_ID", System.Data.ParameterDirection.Input, DataRowVersion.Current));

                da.Update(dtStkPlotDet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (da != null) da.Dispose();
            }
        }

        private void CreateStackPlotDetCommand(SqlCommand cmd)
        {
            cmd.Parameters.Add(CreateSqlParameter("@IN_STACK_ID", SqlDbType.Decimal, 10, "STACK_ID", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_PLOT_CODE", SqlDbType.VarChar, 20, "PLOT_CODE", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IN_MEMO_PLOTS", SqlDbType.Decimal, 5, "MEMO_PLOTS", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IC_SALEABLE", SqlDbType.Char, 1, "SALEABLE", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IC_COSTING_PLOT", SqlDbType.Char, 1, "COSTING_PLOT", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@ID_TTS_DATE", SqlDbType.DateTime, -1, "TTS_DATE", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IC_TAKEN_TO_SALE", SqlDbType.Char, 1, "TAKEN_TO_SALE", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IN_PHASE_ID", SqlDbType.Decimal, 10, "PHASE_ID", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IN_PLOT_TYPE_ID", SqlDbType.Decimal, 10, "PLOT_TYPE_ID", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IN_BLOCK_ID", SqlDbType.Decimal, 10, "BLOCK_ID", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IN_NIFA", SqlDbType.Decimal, 35, "NIFA", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IN_SALES_REVENUE", SqlDbType.Decimal, 35, "SALES_REVENUE", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IN_LAND_COST", SqlDbType.Decimal, 35, "LAND_COST", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IN_BUILD_COST", SqlDbType.Decimal, 35, "BUILD_COST", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IN_RESIDENCE_TYPE_ID", SqlDbType.Decimal, 10, "RESIDENCE_TYPE_ID", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IC_INCLUDE_IN_REPORT", SqlDbType.Char, 1, "INCLUDE_IN_REPORT", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IN_CORE_ID", SqlDbType.Decimal, 10, "CORE_ID", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IN_MAINTENANCE_RESERVE", SqlDbType.Decimal, 35, "MAINTENANCE_RESERVE", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IN_MARKETING_COST", SqlDbType.Decimal, 35, "MARKETING_COST", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IN_LEGAL_COST", SqlDbType.Decimal, 35, "LEGAL_COST", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IN_SALES_COMMISSION", SqlDbType.Decimal, 35, "SALES_COMMISSION", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IN_OVERAGE", SqlDbType.Decimal, 35, "OVERAGE", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IN_GROSS_MARGIN", SqlDbType.Decimal, 35, "GROSS_MARGIN", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IN_GROSS_MARGIN_PCT", SqlDbType.Decimal, 15, "GROSS_MARGIN_PCT", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IC_PLOT_FINALISED", SqlDbType.Char, 1, "PLOT_FINALISED", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@ID_FORECAST_TTS_DATE", SqlDbType.DateTime, -1, "FORECAST_TTS_DATE", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IC_PLOT_INDICATOR", SqlDbType.Char, 1, "PLOT_INDICATOR", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_PLOT_DESCRIPTION", SqlDbType.VarChar, 255, "PLOT_DESCRIPTION", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IN_MODE", SqlDbType.Decimal, 1, "MODE", System.Data.ParameterDirection.Input, DataRowVersion.Current));
        }

        public void UpdateStackDocument(DataTable dtStkDocs)
        {
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                da.InsertCommand = new SqlCommand("SPN_XP_INS_STK_DOCUMENTS", ProfitCashflow.oPcfDM.conn);
                da.InsertCommand.CommandType = CommandType.StoredProcedure;
                CreateStackDocumentCommand(da.InsertCommand);

                da.UpdateCommand = new SqlCommand("SPN_XP_UPD_STK_DOCUMENTS", ProfitCashflow.oPcfDM.conn);
                da.UpdateCommand.CommandType = CommandType.StoredProcedure;
                CreateStackDocumentCommand(da.UpdateCommand);

                da.DeleteCommand = new SqlCommand("SPN_XP_DEL_STK_DOCUMENTS", ProfitCashflow.oPcfDM.conn);
                da.DeleteCommand.CommandType = CommandType.StoredProcedure;
                da.DeleteCommand.Parameters.Add(CreateSqlParameter("@IN_STACK_ID", SqlDbType.Decimal, 10, "STACK_ID", System.Data.ParameterDirection.Input, DataRowVersion.Current));
                da.DeleteCommand.Parameters.Add(CreateSqlParameter("@IN_DOCUMENT_ID", SqlDbType.Decimal, 10, "DOCUMENT_ID", System.Data.ParameterDirection.Input, DataRowVersion.Current));

                da.Update(dtStkDocs);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (da != null) da.Dispose();
            }
        }

        private void CreateStackDocumentCommand(SqlCommand cmd)
        {
            cmd.Parameters.Add(CreateSqlParameter("@IN_STACK_ID", SqlDbType.Decimal, 10, "STACK_ID", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IN_DOCUMENT_ID", SqlDbType.Decimal, 10, "DOCUMENT_ID", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_DOCUMENT_NAME", SqlDbType.VarChar, 255, "DOCUMENT_NAME", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_DESCRIPTION", SqlDbType.VarChar, 255, "DESCRIPTION", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_CREATED_BY", SqlDbType.VarChar, 30, "CREATED_BY", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@ID_CREATED_ON", SqlDbType.DateTime, -1, "CREATED_ON", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@ID_LAST_UPDATED", SqlDbType.DateTime, -1, "LAST_UPDATED", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_DOC_HYPER_LINK", SqlDbType.VarChar, 255, "DOC_HYPER_LINK", System.Data.ParameterDirection.Input, DataRowVersion.Current));
        }

        private SqlParameter CreateSqlParameter(string parameterName,
            SqlDbType dataType, int size,
            string srcColumn, System.Data.ParameterDirection direction,
            System.Data.DataRowVersion srcVersion)
        {
            SqlParameter myParameter = new SqlParameter();
            myParameter.ParameterName = parameterName;
            myParameter.SqlDbType = dataType;
            myParameter.Size = size;
            myParameter.SourceColumn = srcColumn;
            myParameter.Direction = direction;
            myParameter.SourceVersion = srcVersion;

            return myParameter;
        }

        public DataTable RetrieveStkVerInfoOnActiveChng(DataRow drStack, string sStackCode)
        {
            SqlDataAdapter da = null;
            DataTable dtVerInfo = new DataTable();

            try
            {
                da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("SPN_XP_GET_STK_VER_IS_CURR_CHNG", ProfitCashflow.oPcfDM.conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;

                da.SelectCommand.Parameters.Add("@IN_COMPANY_ID", SqlDbType.Decimal).Value = Convert.ToDecimal(drStack["COMPANY_ID"]);
                da.SelectCommand.Parameters.Add("@IV_SITE_CODE", SqlDbType.VarChar).Value = Convert.ToDecimal(drStack["SITE_CODE"]);
                da.SelectCommand.Parameters.Add("@IV_STACK_CODE", SqlDbType.VarChar).Value = sStackCode;
                da.SelectCommand.Parameters.Add("@IN_STACK_ID", SqlDbType.Decimal).Value = Convert.ToDecimal(drStack["STACK_ID"]);

                da.Fill(dtVerInfo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (da != null) da.Dispose();
            }

            return dtVerInfo;
        }

        public void UpdateStackStatus(decimal dCompId, string sSiteCode, string sStatus, string sOldStatus,
                                                decimal dPrevStackId, string sStackVersion, string sUserName,
                                                string sProcessToPrev,  decimal dNewVersion, string sInsertNewStackes
                                                )
        {
            TransactionOptions to = new TransactionOptions();
            dNewStackId = Convert.ToDecimal(null);
            
            to.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;

            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew, to, EnterpriseServicesInteropOption.Full))
            {
                SqlCommand cmd = new SqlCommand();
                try
                {
                    cmd = new SqlCommand("SPN_XP_UPD_STACK_STATUS", ProfitCashflow.oPcfDM.conn);
                    cmd.Parameters.Add("@IN_COMPANY_ID", SqlDbType.Decimal, 10).Value = dCompId;
                    cmd.Parameters.Add("@IV_SITE_CODE", SqlDbType.VarChar, 30).Value = sSiteCode;
                    cmd.Parameters.Add("@IV_STACK_STATUS", SqlDbType.Char, 1).Value = sStatus;
                    cmd.Parameters.Add("@IV_OLD_STACK_STATUS", SqlDbType.Char, 1).Value = sOldStatus;
                    cmd.Parameters.Add("@IN_PREV_STACK_ID", SqlDbType.Decimal, 10).Value = dPrevStackId;
                    cmd.Parameters.Add("@IC_MOVE_TO_STACK_VERSION", SqlDbType.Char, 1).Value = sStackVersion;
                    cmd.Parameters.Add("@IV_USERNAME", SqlDbType.VarChar, 30).Value = sUserName;
                    cmd.Parameters.Add("@IC_PROCESS_TO_PREV", SqlDbType.Char, 1).Value = sProcessToPrev;
                    cmd.Parameters.Add("@IN_NEW_VERSION", SqlDbType.Decimal, 2).Value = dNewVersion;
                    cmd.Parameters.Add("@IC_INSERT_PREV_STACKS", SqlDbType.Char, 1).Value = sInsertNewStackes;
                    cmd.Parameters.Add("@IN_STACK_ID", SqlDbType.Decimal, 10);
                    cmd.Parameters.Add("@IV_ERR_MSG", SqlDbType.VarChar, 500);
                    cmd.Parameters.Add("@IN_PCF_DOC_GUID", SqlDbType.Decimal, 20);

                    cmd.Parameters["@IN_STACK_ID"].Direction = ParameterDirection.Output;
                    cmd.Parameters["@IV_ERR_MSG"].Direction = ParameterDirection.Output;
                    cmd.Parameters["@IN_PCF_DOC_GUID"].Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (cmd.Connection.State != ConnectionState.Open)
                        cmd.Connection.Open();

                    cmd.ExecuteNonQuery();
                    dNewStackId = Convert.ToDecimal(cmd.Parameters["@IN_STACK_ID"].Value);
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

        public void ChkFinalisedPlotCount(decimal dCompId, string sSiteCode, decimal dStackId, string sType)
        {
            SqlCommand cmd = new SqlCommand();
            dFinalisedPlotCount = 0;
            try
            {  
                cmd = new SqlCommand("SPN_XP_CHK_STACK_PLOT_STATUS", ProfitCashflow.oPcfDM.conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@IN_COMPANY_ID", SqlDbType.Decimal, 10).Value = dCompId;
                cmd.Parameters.Add("@IV_SITE_CODE", SqlDbType.VarChar, 30).Value = sSiteCode;
                cmd.Parameters.Add("@IN_STACK_ID", SqlDbType.Decimal, 10).Value = dStackId;
                cmd.Parameters.Add("@IV_TYPE", SqlDbType.VarChar, 2).Value = sType;
                cmd.Parameters.Add("@IN_CNT", SqlDbType.Decimal, 5);
                cmd.Parameters["@IN_CNT"].Direction = ParameterDirection.Output;
                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                dFinalisedPlotCount = Convert.ToDecimal(cmd.Parameters["@IN_CNT"].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
            }

           
        }

        public void Copy_Create_Stack_Version(string sStackVersion, decimal dPrevStackId,
                                                string sUserName, decimal dNewVersion, string sInsertPrevStackes)     
        {
            TransactionOptions to = new TransactionOptions();
            dNewStackId = Convert.ToDecimal(null);

            to.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;

            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew, to, EnterpriseServicesInteropOption.Full))
            {
                SqlCommand cmd = new SqlCommand();
                try
                {
                    cmd = new SqlCommand("SPN_COPY_STACK_DETAILS", ProfitCashflow.oPcfDM.conn);
                    cmd.Parameters.Add("@LI_PREV_STACK_ID", SqlDbType.Decimal, 10).Value = dPrevStackId;
                    cmd.Parameters.Add("@LC_MOVE_TO_STACK_VERSION", SqlDbType.Char, 1).Value = sStackVersion;
                    cmd.Parameters.Add("@LC_USERNAME", SqlDbType.VarChar, 30).Value = sUserName;
                    cmd.Parameters.Add("@LI_NEW_VERSION", SqlDbType.Decimal, 2).Value = dNewVersion;
                    cmd.Parameters.Add("@LC_INSERT_PREV_STACKS", SqlDbType.Char, 1).Value = sInsertPrevStackes;
                    cmd.Parameters.Add("@LI_STACK_ID", SqlDbType.Decimal, 10);
                    cmd.Parameters.Add("@LC_ERR_MSG", SqlDbType.VarChar, 500);
                    cmd.Parameters.Add("@LI_PCF_DOC_GUID", SqlDbType.Decimal, 20);

                    cmd.Parameters["@LI_STACK_ID"].Direction = ParameterDirection.Output;
                    cmd.Parameters["@LC_ERR_MSG"].Direction = ParameterDirection.Output;
                    cmd.Parameters["@LI_PCF_DOC_GUID"].Direction = ParameterDirection.Output;

                    cmd.CommandType = CommandType.StoredProcedure;
                    if (cmd.Connection.State != ConnectionState.Open)
                        cmd.Connection.Open();

                    cmd.ExecuteNonQuery();
                    dNewStackId = Convert.ToDecimal(cmd.Parameters["@LI_STACK_ID"].Value);
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
