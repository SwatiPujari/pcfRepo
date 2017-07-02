using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Xpedeon.ProfitCashflow
{
    public partial class fPlotTypeMaster : DevExpress.XtraEditors.XtraForm
    {
        sPlotTypeMaster dal = new sPlotTypeMaster();
        bool bHasChanges = false;

        public fPlotTypeMaster()
        {
            InitializeComponent();
        }

        private void fMaster_Load(object sender, EventArgs e)
        {
            cxGrid1.DataSource = dal.RetrieveMaster();
        }

        private void fMaster_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (bHasChanges)
            {
                DialogResult oTempResult = XtraMessageBox.Show("Data has been changed, do you want to save the changes?", Application.ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (oTempResult == DialogResult.Yes)
                {
                    if (!cxGrid1DBTableView1.PostEditor())
                        e.Cancel = true;
                    if (!cxGrid1DBTableView1.UpdateCurrentRow()) // will call rowupdated event
                        e.Cancel = true;
                }
                else if (oTempResult == DialogResult.No)
                {
                    cxGrid1DBTableView1.CancelUpdateCurrentRow();
                }
                else
                    e.Cancel = true;
            }

            if (e.Cancel) return;
        }

        private void cxGrid1DBTableView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            object oResult = new object();
            try
            {
                oResult = dal.RetrieveMaxId();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DataRow dr = cxGrid1DBTableView1.GetDataRow(e.RowHandle);
            if (oResult != null && Convert.ToDecimal(oResult) > -1)
                dr["PLOT_TYPE_ID"] = Convert.ToDecimal(oResult);
            dr["SQFT_MANDATORY"] = "Y";
            dr["CREATED_BY"] = ProfitCashflow.oPcfDM.UserName;
            dr["CREATED_ON"] = DateTime.Now.Date;
        }

        private void cxGrid1DBTableView1_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            DataRow dr = cxGrid1DBTableView1.GetDataRow(e.RowHandle);
            if (dr == null) return;

            if (dr["PLOT_TYPE"] == DBNull.Value || string.IsNullOrWhiteSpace(Convert.ToString(dr["PLOT_TYPE"])))
            {
                XtraMessageBox.Show("Plot Type is mandatory.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Valid = false;
                return;
            }
            else
                dr["UPPER_PLOT_TYPE_NAME"] = Convert.ToString(dr["PLOT_TYPE"]).ToUpper();

            if (cxGrid1DBTableView1.IsNewItemRow(e.RowHandle) || (!(dr["PLOT_TYPE", DataRowVersion.Original]).Equals(dr["PLOT_TYPE"])))
            {
                int cnt = dal.CheckMaster(dr["PLOT_TYPE"].ToString());
                if (cnt > 0)
                {
                    XtraMessageBox.Show("Duplicate Plot Type is not allowed.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Valid = false;
                    return;
                }
            }
            dr["CREATED_BY"] = ProfitCashflow.oPcfDM.UserName;
            dr["LAST_UPDATED"] = DateTime.Now;
        }

        private void cxGrid1DBTableView1_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            DataRow dr = ((DataRowView)e.Row).Row;
            if (dr == null) return;

            string sType = "";
            if (dr.RowState == DataRowState.Added)
                sType = "I";
            else if (dr.RowState == DataRowState.Modified)
                sType = "U";
            else if (dr.RowState == DataRowState.Deleted)
                sType = "D";

            try
            {
                dal.UpdateMaster(sType, dr);
                dr.AcceptChanges();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bHasChanges = false;
        }

        private void cxGrid1DBTableView1_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void cxGrid1DBTableView1_KeyDown(object sender, KeyEventArgs e)
        {
            var dr = cxGrid1DBTableView1.GetDataRow(cxGrid1DBTableView1.FocusedRowHandle);
            if (dr == null) return;

            if (e.KeyData == Keys.Delete)
            {
                try
                {
                    DialogResult oTempResult = XtraMessageBox.Show("Delete record?", Application.ProductName, MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (oTempResult == DialogResult.OK)
                    {
                        if (dr["SYS_DEF_TYPE"] != DBNull.Value)
                        {
                            if (dr["SYS_DEF_TYPE"].ToString() == "P" || dr["SYS_DEF_TYPE"].ToString() == "G" || dr["SYS_DEF_TYPE"].ToString() == "H")
                            {
                                XtraMessageBox.Show("Cannot delete a system created Plot Type.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }

                        int cnt = dal.CheckDeleteMaster(Convert.ToDecimal(dr["PLOT_TYPE_ID"]));
                        if (cnt > 0)
                        {
                            XtraMessageBox.Show("Cannot delete, as the Plot Type is selected either in Residence Type Master, Stack Plot Setup, B4 Plot Setup, Profit Forecast, Take Plot To Sales or Retake Plot to Sales.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        dr.Delete();
                        dal.UpdateMaster("D", dr);
                        dr.AcceptChanges();
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void cxGrid1DBTableView1_DragObjectOver(object sender, DevExpress.XtraGrid.Views.Base.DragObjectOverEventArgs e)
        {
            if (e.DragObject == cxGrid1DBTableView1PLOT_TYPE || e.DragObject == cxGrid1DBTableView1SQFT_MANDATORY)
                e.DropInfo.Valid = !(e.DropInfo.Index < 0);
        }

        private void fPlotTypeMaster_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                bHasChanges = false;
        }

        private void cxGrid1DBTableView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            bHasChanges = true;
        }
    }
}