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
    public partial class fGarageParkMaster : DevExpress.XtraEditors.XtraForm
    {
        sGarageParkMaster oGarage = new sGarageParkMaster();
        int iCount = 0;
        bool bHasChanges = false;

        public fGarageParkMaster()
        {
            InitializeComponent();
        }

        private void fGarageMaster_Load(object sender, EventArgs e)
        {
            cxImageComboBoxShowCompanies.SelectedIndex = 1;

            if (ProfitCashflow.oPcfDM.company_id > 0)
            {
                cxLookupComboBoxCompany.Properties.DataSource = oGarage.RetrieveCompany(ProfitCashflow.oPcfDM.UserName, cxImageComboBoxShowCompanies.EditValue.ToString());
                cxLookupComboBoxCompany.EditValue = ProfitCashflow.oPcfDM.company_id;
            }
        }

        private void fGarageMaster_FormClosing(object sender, FormClosingEventArgs e)
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

        private void cxImageComboBoxShowCompanies_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                cxGrid1DBTableView1.FocusedRowChanged -= cxGrid1DBTableView1_FocusedRowChanged;
                cxLookupComboBoxCompany.Properties.DataSource = oGarage.RetrieveCompany(ProfitCashflow.oPcfDM.UserName, cxImageComboBoxShowCompanies.EditValue.ToString());
                cxLookupComboBoxCompany.EditValue = null;
                cxGrid1.DataSource = null;
                cxGrid1DBTableView1.FocusedRowChanged += cxGrid1DBTableView1_FocusedRowChanged;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void cxLookupComboBoxCompany_EditValueChanged(object sender, EventArgs e)
        {
            if (cxLookupComboBoxCompany.EditValue != null)
            {
                var dtGarage = oGarage.RetrieveGarageMaster(Convert.ToDecimal(cxLookupComboBoxCompany.EditValue));
                cxGrid1.DataSource = dtGarage;
            }
        }

        private void cxGrid1DBTableView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            cxImageComboBoxShowCompanies.Enabled = false;
            cxLookupComboBoxCompany.Enabled = false;
            int maxId;

            try
            {
                maxId = oGarage.RetrieveMaxGarageId(Convert.ToDecimal(cxLookupComboBoxCompany.EditValue));
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DataRow drGarage = cxGrid1DBTableView1.GetDataRow(e.RowHandle);
            if (maxId > -1)
                drGarage["GARAGE_PARKING_ID"] = Convert.ToDecimal(maxId) + 1;

            drGarage["COMPANY_ID"] = cxLookupComboBoxCompany.EditValue;
            drGarage["CREATED_BY"] = ProfitCashflow.oPcfDM.UserName;
            drGarage["CREATED_ON"] = DateTime.Now.Date;
        }

        private void cxGrid1DBTableView1_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            DataRow drGarage = cxGrid1DBTableView1.GetDataRow(e.RowHandle);
            if (drGarage == null) return;

            if (drGarage["GARAGE_PARKING_NAME"] == DBNull.Value || string.IsNullOrWhiteSpace(Convert.ToString(drGarage["GARAGE_PARKING_NAME"])))
            {
                XtraMessageBox.Show("Garage / Parking Name is mandatory.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Valid = false;
                return;
            }
            else
            {
                drGarage["UPPER_GARAGE_PARKING_NAME"] = Convert.ToString(drGarage["GARAGE_PARKING_NAME"]).ToUpper();
            }

            if (cxGrid1DBTableView1.IsNewItemRow(e.RowHandle) || (!(drGarage["GARAGE_PARKING_NAME", DataRowVersion.Original]).Equals(drGarage["GARAGE_PARKING_NAME"])))
            {
                if (drGarage["GARAGE_PARKING_NAME"] != DBNull.Value && !string.IsNullOrWhiteSpace(Convert.ToString(drGarage["GARAGE_PARKING_NAME"])))
                {
                    int cnt = oGarage.CheckGarageMaster(Convert.ToDecimal(cxLookupComboBoxCompany.EditValue), drGarage["GARAGE_PARKING_NAME"].ToString());
                    if (cnt > 0)
                    {
                        XtraMessageBox.Show("Duplicate Garage / Parking for a company is not allowed.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        e.Valid = false;
                        return;
                    }
                }
            }

            drGarage["LAST_UPDATED"] = DateTime.Now.Date;
        }

        private void cxGrid1DBTableView1_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            DataRow drGarage = ((DataRowView)e.Row).Row;
            if (drGarage == null) return;

            string sType = "";
            if (drGarage.RowState == DataRowState.Added)
                sType = "I";
            else if (drGarage.RowState == DataRowState.Modified)
                sType = "U";
            else if (drGarage.RowState == DataRowState.Deleted)
                sType = "D";

            try
            {
                oGarage.UpdateGarageMaster(sType, drGarage);
                drGarage.AcceptChanges();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bHasChanges = false;
        }

        private void cxGrid1DBTableView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle < 0 && cxGrid1DBTableView1.IsFilterRow(e.FocusedRowHandle)) return;

            if (e.FocusedRowHandle < 0)
            {
                if (cxGrid1DBTableView1.RowCount > 0)
                {
                    cxImageComboBoxShowCompanies.Enabled = false;
                    cxLookupComboBoxCompany.Enabled = false;
                }

                if (cxLookupComboBoxCompany.EditValue == null)
                {
                    XtraMessageBox.Show("Please select a Company.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                 object oDrTemp = cxLookupComboBoxCompany.Properties.GetDataSourceRowByKeyValue(cxLookupComboBoxCompany.EditValue);
                 if (oDrTemp != null)
                 {
                     DataRow drCompList = (DataRow)((DataRowView)oDrTemp).Row;
                     if (drCompList != null)
                     {
                         if (drCompList["IS_ACTIVE"] != DBNull.Value && drCompList["IS_ACTIVE"].ToString() == "N")
                         {
                             XtraMessageBox.Show("Cannot create an Garage / Parking for an inactive company.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                             cxGrid1DBTableView1GARAGE_PARKING_NAME.OptionsColumn.AllowEdit = false;
                             cxImageComboBoxShowCompanies.Enabled = true;
                             cxLookupComboBoxCompany.Enabled = true;
                             return;
                         }
                         else
                         {
                             cxGrid1DBTableView1GARAGE_PARKING_NAME.OptionsColumn.AllowEdit = true;
                         }
                     }
                 }
            }
            else
            {
                cxGrid1DBTableView1GARAGE_PARKING_NAME.OptionsColumn.AllowEdit = true;
                cxImageComboBoxShowCompanies.Enabled = true;
                cxLookupComboBoxCompany.Enabled = true;
            }
        }

        private void cxGrid1DBTableView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            bHasChanges = true;

            if (e.RowHandle < 0) return;

            cxImageComboBoxShowCompanies.Enabled = false;
            cxLookupComboBoxCompany.Enabled = false;
        }

        private void cxGrid1DBTableView1_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void cxGrid1DBTableView1_KeyDown(object sender, KeyEventArgs e)
        {
            var drGarage = cxGrid1DBTableView1.GetDataRow(cxGrid1DBTableView1.FocusedRowHandle);
            if (drGarage == null) return;

            if (e.KeyData == Keys.Delete)
            {
                try
                {
                    DialogResult oTempResult = XtraMessageBox.Show("Delete record?", Application.ProductName, MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (oTempResult == DialogResult.OK)
                    {
                        int cnt = oGarage.CheckDelGarageMaster(Convert.ToDecimal(drGarage["COMPANY_ID"]), Convert.ToDecimal(drGarage["GARAGE_PARKING_ID"]));
                        if (cnt > 0)
                        {
                            XtraMessageBox.Show("'Cannot delete, as the Garage / Parking is selected in Profit Forecast.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        drGarage.Delete();
                        oGarage.UpdateGarageMaster("D", drGarage);
                        drGarage.AcceptChanges();
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
            if (e.DragObject == cxGrid1DBTableView1GARAGE_PARKING_NAME)
                e.DropInfo.Valid = !(e.DropInfo.Index < 0);
        }

        private void cxGrid1_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                iCount = iCount + 1;
                if (iCount == 2)
                {
                    cxImageComboBoxShowCompanies.Enabled = true;
                    cxLookupComboBoxCompany.Enabled = true;
                    iCount = 0;
                }
            }
        }

        private void fGarageParkMaster_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                bHasChanges = false;
        }
    }
}