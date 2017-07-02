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
    public partial class fUnitInterestMaster : DevExpress.XtraEditors.XtraForm
    {
        sInterestRateMaster dal = new sInterestRateMaster();
        DataTable dt = new DataTable();
        DataRow dr;
        bool isIns;
        decimal? pOrgIntRate;
        DateTime? pOrgIntEffDate;
        string pOrgRemarks;

        public fUnitInterestMaster()
        {
            InitializeComponent();
        }

        private void fMaster_Load(object sender, EventArgs e)
        {
            cxImageComboBoxShowCompanies.SelectedIndex = 1;

            if (ProfitCashflow.oPcfDM.company_id > 0)
                cxLookupComboBoxCompany.EditValue = ProfitCashflow.oPcfDM.company_id;

            cxLookupComboBoxCompany.Properties.DataSource = dal.RetrieveCompany(ProfitCashflow.oPcfDM.UserName, cxImageComboBoxShowCompanies.EditValue.ToString());
        }

        private void fMaster_FormClosing(object sender, FormClosingEventArgs e)
        {
            cxDBCurrencyEdit1.DoValidate();
            cxDBDateEdit1.DoValidate();
            cxDBMemo1.DoValidate();
            dnInterest.Buttons.DoClick(dnInterest.Buttons.EndEdit);

            if (dr != null && dr.RowState == DataRowState.Modified)
            {
                DialogResult oTempResult = XtraMessageBox.Show("Data has been changed, do you want to save the changes?", Application.ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (oTempResult == DialogResult.Yes)
                {
                    try
                    {
                        if (!SaveInterestRate()) e.Cancel = true;
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else if (oTempResult == DialogResult.No)
                {
                    e.Cancel = false; // Form close
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
                cxLookupComboBoxCompany.Properties.DataSource = dal.RetrieveCompany(ProfitCashflow.oPcfDM.UserName, cxImageComboBoxShowCompanies.EditValue.ToString());
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
                dt = dal.RetrieveMaster("I", Convert.ToDecimal(cxLookupComboBoxCompany.EditValue));
                if (dt.Rows.Count > 0)
                {
                    isIns = false;
                    dr = dt.Rows[0];
                    pOrgIntRate = Convert.ToDecimal(dr["INTEREST_RATE"].ToString());
                    pOrgIntEffDate = Convert.ToDateTime(dr["INT_EFFECTIVE_DATE"]);
                    pOrgRemarks = dr["REMARKS"] != null ? dr["REMARKS"].ToString() : string.Empty;
                    //dt.AcceptChanges();

                    cxDBCurrencyEdit1.EditValueChanging -= cxDBEdit_EditValueChanging;
                    cxDBDateEdit1.EditValueChanging -= cxDBEdit_EditValueChanging;
                    cxDBMemo1.EditValueChanging -= cxDBEdit_EditValueChanging;

                    AddBinding();

                    cxDBCurrencyEdit1.EditValueChanging += cxDBEdit_EditValueChanging;
                    cxDBDateEdit1.EditValueChanging += cxDBEdit_EditValueChanging;
                    cxDBMemo1.EditValueChanging += cxDBEdit_EditValueChanging;
                }
                else
                {
                    isIns = true;
                    cxDBCurrencyEdit1.EditValue = null;
                    cxDBDateEdit1.EditValue = null;
                    cxDBMemo1.EditValue = null;

                    AddBinding();
                    try
                    {
                        dnInterest.Buttons.DoClick(dnInterest.Buttons.Append);
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
            }
        }

        private void AddBinding()
        {
            //if (dt.Rows.Count > 0)    //v2
            //{
                ClearBinding();
                cxDBCurrencyEdit1.DataBindings.Add("EditValue", dt, "INTEREST_RATE");
                cxDBDateEdit1.DataBindings.Add("EditValue", dt, "INT_EFFECTIVE_DATE");
                cxDBMemo1.DataBindings.Add("EditValue", dt, "REMARKS");
                dnInterest.DataSource = dt;
            //}
        }

        private void ClearBinding()
        {
            cxDBCurrencyEdit1.DataBindings.Clear();
            cxDBDateEdit1.DataBindings.Clear();
            cxDBMemo1.DataBindings.Clear();
            dnInterest.DataBindings.Clear();
        }

        private void cxBttnOK_Click(object sender, EventArgs e)
        {
            SaveInterestRate();
        }

        private bool SaveInterestRate()
        {
            cxDBCurrencyEdit1.DoValidate();
            cxDBDateEdit1.DoValidate();
            cxDBMemo1.DoValidate();

            if (!Validation()) return false;

            dnInterest.Buttons.DoClick(dnInterest.Buttons.EndEdit);

            if (isIns)
            {
                dr = dt.Rows[0];    //dr = dt.NewRow();
                dr["COMPANY_ID"] = cxLookupComboBoxCompany.EditValue.ToString();
                dr["CREATED_BY"] = ProfitCashflow.oPcfDM.UserName;
                dr["CREATED_ON"] = DateTime.Now;
                //dt.Rows.Add(dr);
                pOrgIntRate = null;
                pOrgIntEffDate = null;
                pOrgRemarks = string.Empty;
            }

            // XE-30494
            //if ((pOrgIntRate == null || (cxDBCurrencyEdit1.EditValue != null && string.IsNullOrWhiteSpace(cxDBCurrencyEdit1.EditValue.ToString()))) &&
            //    (string.IsNullOrWhiteSpace(pOrgIntEffDate.ToString()) ||
            //        (cxDBDateEdit1.EditValue != null && string.IsNullOrWhiteSpace(cxDBDateEdit1.EditValue.ToString()))) &&
            //    (string.IsNullOrWhiteSpace(pOrgRemarks) ||
            //        (cxDBDateEdit1.EditValue != null && string.IsNullOrWhiteSpace(cxDBDateEdit1.EditValue.ToString()))))
            //{
            //    return false;
            //}

            if (dt.GetChanges() == null) return false;

            if (pOrgIntRate != Convert.ToDecimal(cxDBCurrencyEdit1.EditValue))
            {
                dr["CREATED_ON"] = DateTime.Now;
            }

            DataTable dataTab = dal.RetrieveMaster("I", Convert.ToDecimal(cxLookupComboBoxCompany.EditValue));
            if (dataTab.Rows.Count > 0)
            {
                pOrgIntRate = Convert.ToDecimal(dataTab.Rows[0]["INTEREST_RATE"].ToString());
                pOrgIntEffDate = Convert.ToDateTime(dataTab.Rows[0]["INT_EFFECTIVE_DATE"]);
                pOrgRemarks = dataTab.Rows[0]["REMARKS"] != null ? dataTab.Rows[0]["REMARKS"].ToString() : string.Empty;
            }

            // save interest rate
            dal.UpdateInterestRate("I", dt.GetChanges());
            dt.AcceptChanges();

            // save interest rate history
            if (pOrgIntEffDate != null && (pOrgIntEffDate.Value.Date != Convert.ToDateTime(cxDBDateEdit1.EditValue).Date))
            {
                if (!(isIns) && (pOrgIntRate != Convert.ToDecimal(cxDBCurrencyEdit1.EditValue)))
                {  
                    var id = dal.RetrieveMaxInterestRateId();
                    DataTable dtHistory = new DataTable();
                    dtHistory.Columns.Add("COMPANY_ID");
                    dtHistory.Columns.Add("PREV_INTEREST_RATE");
                    dtHistory.Columns.Add("REMARKS");
                    dtHistory.Columns.Add("CREATED_BY");
                    dtHistory.Columns.Add("CREATED_ON");
                    dtHistory.Columns.Add("PREV_INT_EFFECTIVE_DATE");
                    dtHistory.Columns.Add("INTEREST_RATE_HISTORY_ID");

                    DataRow dr = dtHistory.NewRow();
                    dr["COMPANY_ID"] = dt.Rows[0]["COMPANY_ID"];
                    dr["PREV_INTEREST_RATE"] = pOrgIntRate;
                    dr["REMARKS"] = pOrgRemarks;
                    dr["CREATED_BY"] = ProfitCashflow.oPcfDM.UserName;
                    dr["CREATED_ON"] = DateTime.Now;
                    dr["PREV_INT_EFFECTIVE_DATE"] = pOrgIntEffDate;
                    dr["INTEREST_RATE_HISTORY_ID"] = id;

                    dtHistory.Rows.Add(dr);
                    dal.UpdateInterestRate("H", dtHistory.GetChanges());
                }
            }

            isIns = false;
            cxImageComboBoxShowCompanies.Enabled = true;
            cxLookupComboBoxCompany.Enabled = true;

            return true;
        }

        private void cxButton1_Click(object sender, EventArgs e)
        {
            if (cxLookupComboBoxCompany.EditValue == null || (cxLookupComboBoxCompany.EditValue != null && string.IsNullOrWhiteSpace(cxLookupComboBoxCompany.EditValue.ToString())))
            {
                XtraMessageBox.Show("Please select a company.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                cxLookupComboBoxCompany.Focus();
                return;
            }

            // get history
            fInterestRateHistory frm = new fInterestRateHistory();
            frm.SetParameters(dal, Convert.ToDecimal(cxLookupComboBoxCompany.EditValue));
            frm.ShowDialog();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            dnInterest.Buttons.DoClick(dnInterest.Buttons.EndEdit);

            if (dr != null && (dr.RowState == DataRowState.Added || dr.RowState == DataRowState.Modified))
            {
                XtraMessageBox.Show("Please first save the record.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            fListCompanies frm = new fListCompanies();
            frm.SetParameters(dal);
            frm.ShowDialog();

            cxLookupComboBoxCompany_EditValueChanged(sender, e);
        }

        private void cxBttnCancel_Click(object sender, EventArgs e)
        {
            dnInterest.Buttons.DoClick(dnInterest.Buttons.CancelEdit);
            if (dt != null) dt.RejectChanges();

            this.FormClosing -= fMaster_FormClosing;
            this.Close();
            this.FormClosing += fMaster_FormClosing;
        }

        private bool Validation()
        {
            if (cxDBCurrencyEdit1.EditValue == null || (cxDBCurrencyEdit1.EditValue != null && string.IsNullOrWhiteSpace(cxDBCurrencyEdit1.EditValue.ToString())))
            {
                XtraMessageBox.Show("Interest Rate is mandatory.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                decimal number;
                bool result = decimal.TryParse(cxDBCurrencyEdit1.EditValue.ToString(), out number);
                if (!result)
                {
                    XtraMessageBox.Show(cxDBCurrencyEdit1.EditValue.ToString() + " is not a valid BCD value.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            if (cxDBDateEdit1.EditValue == null || (cxDBDateEdit1.EditValue != null && string.IsNullOrWhiteSpace(cxDBDateEdit1.EditValue.ToString())))
            {
                XtraMessageBox.Show("Enter the Effective Date.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!(isIns) && (pOrgIntRate != Convert.ToDecimal(cxDBCurrencyEdit1.EditValue)) &&
               (cxDBMemo1.EditValue == null ||
               (cxDBMemo1.EditValue != null && string.IsNullOrWhiteSpace(cxDBMemo1.EditValue.ToString()))))
            {
                XtraMessageBox.Show("Reason for Change is mandatory, since the Interest Rate has been changed.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            int res = dal.CheckMaster(cxLookupComboBoxCompany.EditValue.ToString(), Convert.ToDateTime(cxDBDateEdit1.EditValue));
            if (res > 0)
            {
                XtraMessageBox.Show("Interest rate has already been defined for the entered effective date.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            DateTime startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            if (Convert.ToDateTime(cxDBDateEdit1.EditValue).Date < (startDate).Date)
            {
                XtraMessageBox.Show("Effective Date should be of Current or Later month.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void cxDBEdit_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (cxLookupComboBoxCompany.EditValue == null || (cxLookupComboBoxCompany.EditValue != null && string.IsNullOrWhiteSpace(cxLookupComboBoxCompany.EditValue.ToString())))
            {
                XtraMessageBox.Show("Please select a company.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                cxLookupComboBoxCompany.Focus();
                e.Cancel = true;
                return;
            }
        }

        private void cxDBCurrencyEdit1_EditValueChanged(object sender, EventArgs e)
        {
            /*cxDBCurrencyEdit1.DoValidate();
            cxDBDateEdit1.DoValidate();
            cxDBMemo1.DoValidate();
            dnInterest.Buttons.DoClick(dnInterest.Buttons.EndEdit);*/

            //XE-30494
            //if (dt.GetChanges() == null) return;

            if (cxDBCurrencyEdit1.EditValue != null && !string.IsNullOrEmpty(cxDBCurrencyEdit1.EditValue.ToString())
                && pOrgIntRate != Convert.ToDecimal(cxDBCurrencyEdit1.EditValue))
            {
                cxImageComboBoxShowCompanies.Enabled = false;
                cxLookupComboBoxCompany.Enabled = false;
                cxDBDateEdit1.EditValue = null;
                cxDBMemo1.EditValue = null;
            }
        }

        private void cxDBCurrencyEdit1_Enter(object sender, EventArgs e)
        {
            cxDBCurrencyEdit1.Properties.Mask.EditMask = "N4";
        }
    }
}