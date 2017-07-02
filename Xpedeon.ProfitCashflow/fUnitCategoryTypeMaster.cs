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
    public partial class fUnitCategoryTypeMaster : DevExpress.XtraEditors.XtraForm
    {
        sUnitCategoryTypeMaster dal = new sUnitCategoryTypeMaster();
        bool bHasChanges = false;

        public fUnitCategoryTypeMaster()
        {
            InitializeComponent();
        }

        private void fMaster_Load(object sender, EventArgs e)
        {
            repItemLookUpEditPlot.DataSource = dal.RetrievePlotMaster();
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
            object oResType = new object();
            try
            {
                oResType = dal.RetrieveMaxId();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DataRow dr = cxGrid1DBTableView1.GetDataRow(e.RowHandle);
            if (oResType != null && Convert.ToDecimal(oResType) > -1)
                dr["RESIDENCE_TYPE_ID"] = Convert.ToDecimal(oResType);
        
            dr["CREATED_BY"] = ProfitCashflow.oPcfDM.UserName;
            dr["CREATED_ON"] = DateTime.Now.Date;
        }

        private void cxGrid1DBTableView1_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            DataRow dr = cxGrid1DBTableView1.GetDataRow(e.RowHandle);
            if (dr == null) return;

            if (dr["PLOT_TYPE_ID"] == DBNull.Value || string.IsNullOrWhiteSpace(Convert.ToString(dr["PLOT_TYPE_ID"])))
            {
                XtraMessageBox.Show("Plot Type is mandatory.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Valid = false;
                return;
            }

            if (dr["RESIDENCE_CATEGORY_ID"] != DBNull.Value && (cxGrid1DBTableView1.IsNewItemRow(e.RowHandle) ||  !(dr["RESIDENCE_CATEGORY_ID", DataRowVersion.Original]).Equals(dr["RESIDENCE_CATEGORY_ID"])))
            {
                if (dr["RESIDENCE_CATEGORY_ID"].ToString().Length > 2)
                {
                    XtraMessageBox.Show("Number is out of range.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Valid = false;
                    return;
                }
                
                int cnt = dal.CheckMaster("C", Convert.ToDecimal(dr["RESIDENCE_CATEGORY_ID"].ToString()), 
                                             null, 
                                             string.Empty);
                if (cnt > 0)
                {
                    XtraMessageBox.Show("Duplicate Category Code is not allowed.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Valid = false;
                    return;
                }
            }

            if (dr["DESCRIPTION"] == DBNull.Value || string.IsNullOrWhiteSpace(Convert.ToString(dr["DESCRIPTION"])))
            {
                XtraMessageBox.Show("Unit Category is mandatory.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Valid = false;
                return;
            }

            if (dr["PLOT_TYPE_ID"] != DBNull.Value && dr["DESCRIPTION"] != DBNull.Value &&
                (cxGrid1DBTableView1.IsNewItemRow(e.RowHandle) || ((!(dr["PLOT_TYPE_ID", DataRowVersion.Original]).Equals(dr["PLOT_TYPE_ID"])) &&
                ((!(dr["DESCRIPTION", DataRowVersion.Original]).Equals(dr["DESCRIPTION"]))))))
            {
                int cnt = dal.CheckMaster("P", null,
                                             Convert.ToDecimal(dr["PLOT_TYPE_ID"].ToString()),
                                             dr["DESCRIPTION"].ToString());
                if (cnt > 0)
                {
                    XtraMessageBox.Show("Duplicate House Type for a Plot Type is not allowed.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Valid = false;
                    return;
                }
            }

            dr["CREATED_BY"] = ProfitCashflow.oPcfDM.UserName;
            dr["LAST_UPDATED"] = DateTime.Now;
        }

        private void cxGrid1DBTableView1_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            DataRow drLoc = ((DataRowView)e.Row).Row;
            if (drLoc == null) return;

            string sType = "";
            if (drLoc.RowState == DataRowState.Added)
                sType = "I";
            else if (drLoc.RowState == DataRowState.Modified)
                sType = "U";
            else if (drLoc.RowState == DataRowState.Deleted)
                sType = "D";

            try
            {
                dal.UpdateMaster(sType, drLoc);
                drLoc.AcceptChanges();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("PK_UNIT_CATEGORY") || ex.Message.Contains("FK_STACKPLOTDET_2_UNIT_CAT"))
                {
                    XtraMessageBox.Show("Key violation.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    drLoc.RejectChanges();
                }
                else
                {
                    XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
            var drLoc = cxGrid1DBTableView1.GetDataRow(cxGrid1DBTableView1.FocusedRowHandle);
            if (drLoc == null) return;

            if (e.KeyData == Keys.Delete)
            {
                try
                {
                    DialogResult oTempResult = XtraMessageBox.Show("Delete record?", Application.ProductName, MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (oTempResult == DialogResult.OK)
                    {
                        int cnt = dal.CheckDeleteMaster(Convert.ToDecimal(drLoc["PLOT_TYPE_ID"]), Convert.ToDecimal(drLoc["RESIDENCE_TYPE_ID"]));
                        if (cnt > 0)
                        {
                            XtraMessageBox.Show("Cannot delete, as the Unit Category is selected either in Profit Forecast -> Plot Details, Stack Plot Setup or B4 Plot Setup.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        drLoc.Delete();
                        dal.UpdateMaster("D", drLoc);
                        drLoc.AcceptChanges();
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
            if (e.DragObject == cxGrid1DBTableView1RESIDENCE_CATEGORY_ID)
                e.DropInfo.Valid = !(e.DropInfo.Index < 0);
        }

        private void cxGrid1DBTableView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            bHasChanges = true;
        }

        private void fUnitCategoryTypeMaster_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                bHasChanges = false;
        }
    }
}