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
using DevExpress.XtraGrid.Views.Grid;

namespace Xpedeon.ProfitCashflow
{
    public partial class fB4SiteCodeMaster : DevExpress.XtraEditors.XtraForm
    {
        sB4SiteCodeMaster dal = new sB4SiteCodeMaster();
        int iCount = 0;
        bool bHasChanges = false;

        public fB4SiteCodeMaster()
        {
            InitializeComponent();
        }

        private void fMaster_Load(object sender, EventArgs e)
        {
            cxImageComboBoxShowCompanies.SelectedIndex = 1;

            if (ProfitCashflow.oPcfDM.company_id > 0)
            {
                cxLookupComboBoxCompany.Properties.DataSource = dal.RetrieveCompany(ProfitCashflow.oPcfDM.UserName, cxImageComboBoxShowCompanies.EditValue.ToString());
                cxLookupComboBoxCompany.EditValue = ProfitCashflow.oPcfDM.company_id;
            }
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

        private void cxImageComboBoxShowCompanies_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                cxGrid1DBTableView1.FocusedRowChanged -= cxGrid1DBTableView1_FocusedRowChanged;
                cxLookupComboBoxCompany.Properties.DataSource = dal.RetrieveCompany(ProfitCashflow.oPcfDM.UserName, cxImageComboBoxShowCompanies.EditValue.ToString());
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
                var dtView = dal.RetrieveMaster(Convert.ToDecimal(cxLookupComboBoxCompany.EditValue));
                cxGrid1.DataSource = dtView;
            }
        }

        private void cxGrid1DBTableView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            cxImageComboBoxShowCompanies.Enabled = false;
            cxLookupComboBoxCompany.Enabled = false;

            DataRow drView = cxGrid1DBTableView1.GetDataRow(e.RowHandle);
          
            drView["COMPANY_ID"] = cxLookupComboBoxCompany.EditValue;
            drView["CREATED_BY"] = ProfitCashflow.oPcfDM.UserName;
            drView["CREATED_ON"] = DateTime.Now.Date;
        }

        private void cxGrid1DBTableView1_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            DataRow dr = cxGrid1DBTableView1.GetDataRow(e.RowHandle);

            if (dr == null) return;

            if (dr["B4_SITE_CODE"] == DBNull.Value || string.IsNullOrWhiteSpace(Convert.ToString(dr["B4_SITE_CODE"])))
            {
                XtraMessageBox.Show("B4 Site Code is mandatory.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Valid = false;
                return;
            }
            else
            {
                dr["UPPER_B4_SITE_CODE"] = Convert.ToString(dr["B4_SITE_CODE"]).ToUpper();
                if (dr["B4_SITE_CODE"].ToString().Length > 3 && dr["B4_SITE_CODE"].ToString().Substring(0, 3) != "B4-")
                    dr["B4_SITE_CODE"] = "B4-" + dr["B4_SITE_CODE"].ToString();

                if (dr["B4_SITE_CODE"].ToString().Length < 3)
                {
                    dr["B4_SITE_CODE"] = "B4-" + dr["B4_SITE_CODE"].ToString();
                }
            }

            if (cxGrid1DBTableView1.IsNewItemRow(e.RowHandle) || (!(dr["B4_SITE_CODE", DataRowVersion.Original]).Equals(dr["B4_SITE_CODE"])))
            {
                if (dr["B4_SITE_CODE"] != DBNull.Value && !string.IsNullOrWhiteSpace(Convert.ToString(dr["B4_SITE_CODE"])))
                {
                    int cnt = dal.CheckMaster(Convert.ToDecimal(cxLookupComboBoxCompany.EditValue), dr["B4_SITE_CODE"].ToString());
                    if (cnt > 0)
                    {
                        XtraMessageBox.Show("Duplicate B4 Site Code for a company is not allowed.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        e.Valid = false;
                        return;
                    }
                }
            }
            
            dr["LAST_UPDATED"] = DateTime.Now.Date;
        }

        private void cxGrid1DBTableView1_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            if (e.Row == null) return;

            DataRow drView = ((DataRowView)e.Row).Row;
            if (drView == null) return;

            string sType = "";
            if (drView.RowState == DataRowState.Added)
                sType = "I";
            else if (drView.RowState == DataRowState.Modified)
                sType = "U";
            else if (drView.RowState == DataRowState.Deleted)
                sType = "D";

            try
            {
                dal.UpdateMaster(sType, drView);
                drView.AcceptChanges();
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
                            XtraMessageBox.Show("Cannot create a B4 Site Code for an inactive company.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            cxGrid1DBTableView1B4_SITE_CODE.OptionsColumn.AllowEdit = false;
                            cxImageComboBoxShowCompanies.Enabled = true;
                            cxLookupComboBoxCompany.Enabled = true;
                            return;
                        }
                        else
                        {
                            cxGrid1DBTableView1B4_SITE_CODE.OptionsColumn.AllowEdit = true;
                        }
                    }
                }
            }
            else
            {
                cxGrid1DBTableView1B4_SITE_CODE.OptionsColumn.AllowEdit = true;
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
            var drView = cxGrid1DBTableView1.GetDataRow(cxGrid1DBTableView1.FocusedRowHandle);
            if (drView == null) return;

            if (e.KeyData == Keys.Delete)
            {
                try
                {
                    DialogResult oTempResult = XtraMessageBox.Show("Delete record?", Application.ProductName, MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (oTempResult == DialogResult.OK)
                    {
                        int cnt = dal.CheckDelMaster(Convert.ToDecimal(drView["COMPANY_ID"]), drView["B4_SITE_CODE"].ToString());
                        if (cnt > 0)
                        {
                            XtraMessageBox.Show("Cannot delete, as the B4 Site Code is selected in Site Setup.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        drView.Delete();
                        dal.UpdateMaster("D", drView);
                        drView.AcceptChanges();
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void cxGrid1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
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

        private void cxGrid1DBTableView1_ShownEditor(object sender, EventArgs e)
        {
            if (cxGrid1DBTableView1.FocusedColumn == cxGrid1DBTableView1B4_SITE_CODE)
            {
                GridView view = sender as GridView;
                if (view.IsNewItemRow(view.FocusedRowHandle) && view.ActiveEditor.EditValue == null)
                {
                    view.ActiveEditor.EditValue = "B4-";
                    cxGrid1DBTableView1.SetFocusedRowCellValue(cxGrid1DBTableView1B4_SITE_CODE, "B4-");
                }
            }
        }

        private void fB4SiteCodeMaster_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                bHasChanges = false;
        }
    }
}