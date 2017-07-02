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
    public partial class ReleaseDoc : DevExpress.XtraEditors.XtraForm
    {
        DataSet dsAllLockDoc = new DataSet();

        public ReleaseDoc()
        {
            InitializeComponent();
        }

        private void ReleaseDoc_Load(object sender, EventArgs e)
        {
            RetrieveAllLockedDocs();
            if (dsAllLockDoc != null && dsAllLockDoc.Tables.Count > 0)
            {
                cxGrid1.DataSource = dsAllLockDoc.Tables[0];
                cxGrid2.DataSource = dsAllLockDoc.Tables[1];
            }
        }

        private void GridView_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle == DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
                e.Appearance.BackColor = System.Drawing.Color.Ivory;
        }

        private void cxGrid1DBTableView1_KeyDown(object sender, KeyEventArgs e)
        {
            int[] iRowHandle = cxGrid1DBTableView1.GetSelectedRows();
            if (iRowHandle.Length <= 0) return;

            if (e.KeyData == Keys.Delete)
            {
                try
                {
                    DialogResult oTempResult;
                    if (iRowHandle.Length > 1)
                        oTempResult = XtraMessageBox.Show("Delete all selected record?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    else
                        oTempResult = XtraMessageBox.Show("Delete record?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (oTempResult == DialogResult.Yes)
                    {
                        for (int i = 0; i < iRowHandle.Length; i++)
                            cxGrid1DBTableView1.GetDataRow(iRowHandle[i]).Delete();

                        ReleaseLockedDocs("PCF", dsAllLockDoc.Tables[0].GetChanges());
                        dsAllLockDoc.Tables[0].AcceptChanges();
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void cxGridDBTableViewCFL_KeyDown(object sender, KeyEventArgs e)
        {
            int[] iRowHandle = cxGridDBTableViewCFL.GetSelectedRows();
            if (iRowHandle.Length <= 0) return;

            if (e.KeyData == Keys.Delete)
            {
                try
                {
                    DialogResult oTempResult;
                    if (iRowHandle.Length > 1)
                        oTempResult = XtraMessageBox.Show("Delete all selected record?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    else
                        oTempResult = XtraMessageBox.Show("Delete record?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (oTempResult == DialogResult.Yes)
                    {
                        for (int i = 0; i < iRowHandle.Length; i++)
                            cxGridDBTableViewCFL.GetDataRow(iRowHandle[i]).Delete();

                        ReleaseLockedDocs("CF", dsAllLockDoc.Tables[1].GetChanges());
                        dsAllLockDoc.Tables[1].AcceptChanges();
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void RetrieveAllLockedDocs()
        {
            SqlDataAdapter da = null;
            try
            {
                da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("SPN_XS_GET_LOCKED_DOCUMENTS", PCFSecurity.oSecDM.conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;

                da.Fill(dsAllLockDoc);
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

        private void ReleaseLockedDocs(string sType, DataTable dtLockDoc)
        {
            if (dtLockDoc == null) return;

            TransactionOptions to = new TransactionOptions();
            to.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew, to, EnterpriseServicesInteropOption.Full))
            {
                SqlDataAdapter da = null;
                try
                {
                    da = new SqlDataAdapter();
                    if (sType == "PCF")
                    {
                        da.DeleteCommand = new SqlCommand("SPN_XS_DEL_PCF_LOCKED_DOC", PCFSecurity.oSecDM.conn);
                        DeletePCFLockDocCommand(da.DeleteCommand);
                    }
                    else if (sType == "CF")
                    {
                        da.DeleteCommand = new SqlCommand("SPN_XS_DEL_CF_LOCKED_DOC", PCFSecurity.oSecDM.conn);
                        DeleteCFLockDocCommand(da.DeleteCommand);
                    }
                    da.DeleteCommand.CommandType = CommandType.StoredProcedure;

                    da.Update(dtLockDoc);
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

        private void DeletePCFLockDocCommand(SqlCommand cmd)
        {
            cmd.Parameters.Add(CreateSqlParameter("@IN_PCF_DOC_GUID", SqlDbType.Decimal, 20, "PCF_DOC_GUID", System.Data.ParameterDirection.Input, DataRowVersion.Current));
        }

        private void DeleteCFLockDocCommand(SqlCommand cmd)
        {
            cmd.Parameters.Add(CreateSqlParameter("@IV_USERNAME", SqlDbType.VarChar, 30, "USERNAME", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_SESSIONID", SqlDbType.VarChar, 150, "SESSIONID", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_FORM_CODE", SqlDbType.VarChar, 30, "FORM_CODE", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IN_COMPANY_ID", SqlDbType.Decimal, 10, "COMPANY_ID", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IN_STACK_ID", SqlDbType.Decimal, 10, "STACK_ID", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IN_PHASE_ID", SqlDbType.Decimal, 10, "PHASE_ID", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IV_PERIOD_CODE", SqlDbType.VarChar, 30, "PERIOD_CODE", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@IN_START_MONTH", SqlDbType.Decimal, 2, "START_MONTH", System.Data.ParameterDirection.Input, DataRowVersion.Current));
            cmd.Parameters.Add(CreateSqlParameter("@ID_CREATED_ON", SqlDbType.DateTime, -1, "CREATED_ON", System.Data.ParameterDirection.Input, DataRowVersion.Current));
        }

        private SqlParameter CreateSqlParameter(string parameterName, SqlDbType dataType, int size,
                string srcColumn, ParameterDirection direction, DataRowVersion srcVersion)
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
    }
}