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
    public partial class fAspectMaster : DevExpress.XtraEditors.XtraForm
    {
        sAspectMas oAspectMas = new sAspectMas();

        // Handled Escape key
        int iCount = 0;

        bool bHasChanges = false;

        public fAspectMaster()
        {
            InitializeComponent();
        }

        private void fAspectMaster_Load(object sender, EventArgs e)
        {
            cxImageComboBoxShowCompanies.SelectedIndex = 1;

            try
            {
                if (ProfitCashflow.oPcfDM.company_id > 0)
                {
                    cxLookupComboBoxCompany.Properties.DataSource = oAspectMas.RetrieveCompany(ProfitCashflow.oPcfDM.UserName, cxImageComboBoxShowCompanies.EditValue.ToString());
                    cxLookupComboBoxCompany.EditValue = ProfitCashflow.oPcfDM.company_id;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void fAspectMaster_FormClosing(object sender, FormClosingEventArgs e)
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
                    //dtOpCompMast.RejectChanges();
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
                cxLookupComboBoxCompany.Properties.DataSource = oAspectMas.RetrieveCompany(ProfitCashflow.oPcfDM.UserName, cxImageComboBoxShowCompanies.EditValue.ToString());
                cxLookupComboBoxCompany.EditValue = null;
                cxGrid1.DataSource = null;
                cxGrid1DBTableView1.FocusedRowChanged += cxGrid1DBTableView1_FocusedRowChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cxLookupComboBoxCompany_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (cxLookupComboBoxCompany.EditValue != null)
                {
                    var dtAspect = oAspectMas.RetrieveAspectMaster(Convert.ToDecimal(cxLookupComboBoxCompany.EditValue));
                    cxGrid1.DataSource = dtAspect;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cxGrid1DBTableView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            cxImageComboBoxShowCompanies.Enabled = false;
            cxLookupComboBoxCompany.Enabled = false;
            int maxId;

            try
            {
                maxId = oAspectMas.RetrieveMaxAspectId(Convert.ToDecimal(cxLookupComboBoxCompany.EditValue));
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DataRow drAspect = cxGrid1DBTableView1.GetDataRow(e.RowHandle);
            if (maxId > -1)
                drAspect["ASPECT_ID"] = Convert.ToDecimal(maxId) + 1;

            drAspect["COMPANY_ID"] = cxLookupComboBoxCompany.EditValue;
            drAspect["CREATED_BY"] = ProfitCashflow.oPcfDM.UserName;
            drAspect["CREATED_ON"] = DateTime.Now.Date;
        }

        private void cxGrid1DBTableView1_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            DataRow drAspect = cxGrid1DBTableView1.GetDataRow(e.RowHandle);
            if (drAspect == null) return;

            if (drAspect["ASPECT_NAME"] == DBNull.Value || string.IsNullOrWhiteSpace(Convert.ToString(drAspect["ASPECT_NAME"])))
            {
                XtraMessageBox.Show("Aspect Name is mandatory.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Valid = false;
                return;
            }
            else
            {
                drAspect["UPPER_ASPECT_NAME"] = Convert.ToString(drAspect["ASPECT_NAME"]).ToUpper();
            }

            if (cxGrid1DBTableView1.IsNewItemRow(e.RowHandle) || (!(drAspect["ASPECT_NAME", DataRowVersion.Original]).Equals(drAspect["ASPECT_NAME"])))
            {
                if (drAspect["ASPECT_NAME"] != DBNull.Value && !string.IsNullOrWhiteSpace(Convert.ToString(drAspect["ASPECT_NAME"])))
                {
                    int cnt = oAspectMas.CheckAspectMaster(Convert.ToDecimal(cxLookupComboBoxCompany.EditValue), drAspect["ASPECT_NAME"].ToString());
                    if (cnt > 0)
                    {
                        XtraMessageBox.Show("Duplicate Aspect for a company is not allowed.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        e.Valid = false;
                        return;
                    }
                }
            }

            drAspect["LAST_UPDATED"] = DateTime.Now.Date;
        }

        private void cxGrid1DBTableView1_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            DataRow drAspect = ((DataRowView)e.Row).Row;
            if (drAspect == null) return;

            string sType = "";
            if (drAspect.RowState == DataRowState.Added)
                sType = "I";
            else if (drAspect.RowState == DataRowState.Modified)
                sType = "U";
            else if (drAspect.RowState == DataRowState.Deleted)
                sType = "D";

            try
            {
                oAspectMas.UpdateAspectMaster(sType, drAspect);
                drAspect.AcceptChanges();
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
                if (cxGrid1DBTableView1.RowCount > 0 && cxGrid1DBTableView1.IsNewItemRow(e.FocusedRowHandle))
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
                            XtraMessageBox.Show("Cannot create an Aspect for an inactive company.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            cxGrid1DBTableView1ASPECT_NAME.OptionsColumn.AllowEdit = false;
                            cxImageComboBoxShowCompanies.Enabled = true;
                            cxLookupComboBoxCompany.Enabled = true;
                            return;
                        }
                        else
                        {
                            cxGrid1DBTableView1ASPECT_NAME.OptionsColumn.AllowEdit = true;
                        }
                    }
                }
            }
            else
            {
                cxGrid1DBTableView1ASPECT_NAME.OptionsColumn.AllowEdit = true;
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

            iCount = 0;
        }

        private void cxGrid1DBTableView1_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void cxGrid1DBTableView1_KeyDown(object sender, KeyEventArgs e)
        {
            var drAspect = cxGrid1DBTableView1.GetDataRow(cxGrid1DBTableView1.FocusedRowHandle);
            if (drAspect == null) return;

            if (e.KeyData == Keys.Delete)
            {
                try
                {
                    DialogResult oTempResult = XtraMessageBox.Show("Delete record?", Application.ProductName, MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (oTempResult == DialogResult.OK)
                    {
                        int cnt = oAspectMas.CheckDelAspectMaster(Convert.ToDecimal(drAspect["COMPANY_ID"]), Convert.ToDecimal(drAspect["ASPECT_ID"]));
                        if (cnt > 0)
                        {
                            XtraMessageBox.Show("Cannot delete, as the Location is selected in Site Setup.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        drAspect.Delete();
                        oAspectMas.UpdateAspectMaster("D", drAspect);
                        drAspect.AcceptChanges();
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
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

        private void fAspectMaster_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                bHasChanges = false;
        }
    }
}