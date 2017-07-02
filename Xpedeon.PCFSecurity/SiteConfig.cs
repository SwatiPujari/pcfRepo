using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Transactions;
using DevExpress.XtraEditors;

namespace Xpedeon.PCFSecurity
{
    public partial class SiteConfig : DevExpress.XtraEditors.XtraForm
    {
        DataTable dtSiteConfig = new DataTable();
        int lastRowIndex = 0;

        public SiteConfig()
        {
            InitializeComponent();
        }

        private void SiteConfig_Load(object sender, EventArgs e)
        {
            RetrieveSiteConfiguration();
            cxGrid1.DataSource = dtSiteConfig;
            lastRowIndex = cxGrid1DBTableView1.DataRowCount - 1;
        }

        private void SiteConfig_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!cxGrid1DBTableView1.PostEditor())
                e.Cancel = true;
            if (!cxGrid1DBTableView1.UpdateCurrentRow())
                e.Cancel = true;
        }

        private void cxGrid1DBTableView1_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            DataRow drSiteConfig = cxGrid1DBTableView1.GetDataRow(e.RowHandle);
            if (drSiteConfig == null) return;

            if(e.Column == cxGrid1DBTableView1VALUE && Convert.ToDecimal(drSiteConfig["SRNO"]) > 6 && Convert.ToDecimal(drSiteConfig["SRNO"]) != 8)
                e.RepositoryItem = repChkValue;
        }

        private void cxGrid1DBTableView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow drSiteConfig = cxGrid1DBTableView1.GetDataRow(cxGrid1DBTableView1.FocusedRowHandle);
            if (drSiteConfig != null)
            {
                if(drSiteConfig["NAME"].ToString() == "PROFIT_CASHFLOW_EXE" || drSiteConfig["NAME"].ToString() == "PCF_SECURITY_EXE")
                    cxGrid1DBTableView1VALUE.OptionsColumn.AllowEdit = false;
                else
                    cxGrid1DBTableView1VALUE.OptionsColumn.AllowEdit = true;
            }
        }

        private void cxGrid1DBTableView1_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            DataRow drSiteConfig = cxGrid1DBTableView1.GetDataRow(cxGrid1DBTableView1.FocusedRowHandle);
            if (drSiteConfig == null) return;

            try
            {
                if (drSiteConfig.RowState == DataRowState.Modified)
                    UpdateSiteConfiguration(drSiteConfig);
                drSiteConfig.AcceptChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void cxGrid1DBTableView1_CustomColumnSort(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnSortEventArgs e)
        {
            e.Handled = true;

            if (e.ListSourceRowIndex1 == lastRowIndex)
                e.Result = (e.SortOrder == DevExpress.Data.ColumnSortOrder.Ascending ? 1 : -1);
            else if (e.ListSourceRowIndex2 == lastRowIndex)
                e.Result = (e.SortOrder == DevExpress.Data.ColumnSortOrder.Ascending ? -1 : 1);
            else
                e.Handled = false;
        }

        private void RetrieveSiteConfiguration()
        {
            SqlDataAdapter da = null;

            try
            {
                da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("SELECT SRNO, NAME, DESCRIPTION, VALUE FROM SITE_CONFIG_TABLE ORDER BY CASE SRNO WHEN 7 THEN 8 WHEN 8 THEN 7 ELSE SRNO END");
                da.SelectCommand.CommandType = CommandType.Text;
                da.SelectCommand.Connection = PCFSecurity.oSecDM.conn;

                da.Fill(dtSiteConfig);
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

        private void UpdateSiteConfiguration(DataRow drSiteConfig)
        {
            TransactionOptions to = new TransactionOptions();
            to.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew, to, EnterpriseServicesInteropOption.Full))
            {
                SqlCommand cmd = null;
                try
                {
                    cmd = new SqlCommand("UPDATE SITE_CONFIG_TABLE SET VALUE=@IV_VALUE WHERE NAME=@IV_NAME", PCFSecurity.oSecDM.conn);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@IV_NAME", drSiteConfig["NAME"]);
                    cmd.Parameters.AddWithValue("@IV_VALUE", drSiteConfig["VALUE"]);

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
                    if (cmd != null) cmd.Dispose();
                }
            }
        }
    }
}