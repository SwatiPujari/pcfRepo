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
    public partial class fPurchaseTypeMaster : DevExpress.XtraEditors.XtraForm
    {
        sPurchaseTypeMaster oPurchase = new sPurchaseTypeMaster();
        bool bHasChanges = false;

        public fPurchaseTypeMaster()
        {
            InitializeComponent();
        }

        private void fLocationMaster_Load(object sender, EventArgs e)
        {
            cxGrid1.DataSource = oPurchase.RetrievePurchaseMaster();
        }

        private void fLocationMaster_FormClosing(object sender, FormClosingEventArgs e)
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
                oResult = oPurchase.RetrieveMaxPurchaseId();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DataRow drPurType = cxGrid1DBTableView1.GetDataRow(e.RowHandle);
            if (oResult != null && Convert.ToDecimal(oResult) > -1)
                drPurType["PURCHASE_TYPE_ID"] = Convert.ToDecimal(oResult);
            drPurType["CREATED_BY"] = ProfitCashflow.oPcfDM.UserName;
            drPurType["CREATED_ON"] = DateTime.Now.Date;
        }

        private void cxGrid1DBTableView1_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            DataRow drPurType = cxGrid1DBTableView1.GetDataRow(e.RowHandle);
            if (drPurType == null) return;

            if (drPurType["DESCRIPTION"] == DBNull.Value || string.IsNullOrWhiteSpace(Convert.ToString(drPurType["DESCRIPTION"])))
            {
                XtraMessageBox.Show("Purchase Type is mandatory.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Valid = false;
                return;
            }
            else
                drPurType["UPPER_DESCRIPTION"] = Convert.ToString(drPurType["DESCRIPTION"]).ToUpper();

            int cnt = oPurchase.CheckPurchaseMaster(drPurType["DESCRIPTION"].ToString());
            if (cnt > 0)
            {
                XtraMessageBox.Show("Duplicate Purchase Type is not allowed.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Valid = false;
                return;
            }

            drPurType["CREATED_BY"] = ProfitCashflow.oPcfDM.UserName;
            drPurType["LAST_UPDATED"] = DateTime.Now;
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
                oPurchase.UpdatePurchaseMaster(sType, drLoc);
                drLoc.AcceptChanges();
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
            var drLoc = cxGrid1DBTableView1.GetDataRow(cxGrid1DBTableView1.FocusedRowHandle);
            if (drLoc == null) return;

            if (e.KeyData == Keys.Delete)
            {
                try
                {
                    DialogResult oTempResult = XtraMessageBox.Show("Delete record?", Application.ProductName, MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (oTempResult == DialogResult.OK)
                    {
                        int cnt = oPurchase.CheckDeletePurchaseMaster(Convert.ToDecimal(drLoc["PURCHASE_TYPE_ID"]));
                        if (cnt > 0)
                        {
                            XtraMessageBox.Show("Cannot delete, as the Purchase Type is selected in Site Setup.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        drLoc.Delete();
                        oPurchase.UpdatePurchaseMaster("D", drLoc);
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
            if (e.DragObject == cxGrid1DBTableView1DESCRIPTION)
                e.DropInfo.Valid = !(e.DropInfo.Index < 0);
        }

        private void fPurchaseTypeMaster_KeyUp(object sender, KeyEventArgs e)
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