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
    public partial class fListCompanies : DevExpress.XtraEditors.XtraForm
    {
        sInterestRateMaster dal;

        DataTable dt;
        decimal? pOrgIntRate;
        DateTime? pOrgIntEffDate;
        string pOrgRemarks;
        string str_comp;

        public fListCompanies()
        {
            InitializeComponent();
        }

        public void SetParameters(sInterestRateMaster dLayer)
        {
            dal = dLayer;
        }

        private void fMaster_Load(object sender, EventArgs e)
        {
            cxEffDate.EditValue = DateTime.Now;
            dt = dal.RetrieveCompany(ProfitCashflow.oPcfDM.UserName, "Y");
            cxGrid1.DataSource = dt;
        }

        private void fMaster_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!cxGrid1DBTableView1.PostEditor())
                e.Cancel = true;
            if (!cxGrid1DBTableView1.UpdateCurrentRow())
                e.Cancel = true;
        }

        private void cxGrid1DBTableView1_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void cxButtonOk_Click(object sender, EventArgs e)
        {
            if (cxIntRate.EditValue == null || (cxIntRate.EditValue != null && string.IsNullOrEmpty(cxIntRate.EditValue.ToString())))
            {
                XtraMessageBox.Show("Enter the Interest Rate.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (cxMemo1.EditValue == null || (cxMemo1.EditValue != null && string.IsNullOrEmpty(cxMemo1.EditValue.ToString())))
            {
                XtraMessageBox.Show("Enter the Remarks/Reason for Change.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (cxEffDate.EditValue == null || (cxEffDate.EditValue != null && string.IsNullOrEmpty(cxEffDate.EditValue.ToString())))
            {
                XtraMessageBox.Show("Enter the Effective Date.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DateTime startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            if (cxEffDate.EditValue != null && Convert.ToDateTime(cxEffDate.EditValue).Date < startDate)
            {
                XtraMessageBox.Show("Effective date should be of Current or Later month.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (cxGrid1DBTableView1.SelectedRowsCount == 0)
            {
                XtraMessageBox.Show("Atleast one company must be selected.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            str_comp = string.Empty;

            // This table is for saving interest rate
            DataTable dtRateTable = new DataTable("InterestRate");
            dtRateTable.Columns.Add("COMPANY_ID", typeof(decimal));
            dtRateTable.Columns.Add("INTEREST_RATE", typeof(decimal));
            dtRateTable.Columns.Add("CREATED_BY", typeof(string));
            dtRateTable.Columns.Add("CREATED_ON", typeof(DateTime));
            dtRateTable.Columns.Add("INT_EFFECTIVE_DATE", typeof(DateTime));
            dtRateTable.Columns.Add("REMARKS", typeof(string));

            // This table is for saving history interest rate
            DataTable dtHistory = new DataTable();
            dtHistory.Columns.Add("COMPANY_ID");
            dtHistory.Columns.Add("PREV_INTEREST_RATE");
            dtHistory.Columns.Add("REMARKS");
            dtHistory.Columns.Add("CREATED_BY");
            dtHistory.Columns.Add("CREATED_ON");
            dtHistory.Columns.Add("PREV_INT_EFFECTIVE_DATE");
            dtHistory.Columns.Add("INTEREST_RATE_HISTORY_ID");

            var rows = cxGrid1DBTableView1.GetSelectedRows();

            for (int i = 0; i < rows.Length; i++)
            {
                var drSelected = cxGrid1DBTableView1.GetDataRow(rows[i]);
                
                // If exist in interest rate table
                var dtIntRate = dal.RetrieveMaster("I", Convert.ToDecimal(drSelected["COMPANY_ID"]));

                DataRow dr = null;
                if (dtIntRate != null && dtIntRate.Rows.Count > 0)
                {
                    dtRateTable.ImportRow(dtIntRate.Rows[0]);
                    dr = dtRateTable.Rows[dtRateTable.Rows.Count - 1];
                }
                else
                {
                    dr = dtRateTable.NewRow();
                    dr["COMPANY_ID"] = drSelected["COMPANY_ID"];
                    dr["INTEREST_RATE"] = cxIntRate.EditValue;
                    dr["CREATED_BY"] = ProfitCashflow.oPcfDM.UserName;
                    dr["CREATED_ON"] = DateTime.Now;
                    dr["INT_EFFECTIVE_DATE"] = Convert.ToDateTime(cxEffDate.EditValue).Date;
                    dr["REMARKS"] = cxMemo1.EditValue;
                    dtRateTable.Rows.Add(dr);
                }

                if (dtIntRate.Rows.Count > 0)
                {
                    int notExistInHistory = dal.CheckMaster(drSelected["COMPANY_ID"].ToString(), ((DateTime?)cxEffDate.EditValue).Value.Date);
                    if (notExistInHistory == 0)
                    {
                        pOrgIntRate = Convert.ToDecimal(dtIntRate.Rows[0]["INTEREST_RATE"].ToString());
                        pOrgIntEffDate = Convert.ToDateTime(dtIntRate.Rows[0]["INT_EFFECTIVE_DATE"]).Date;
                        pOrgRemarks = dtIntRate.Rows[0]["REMARKS"] != null ? dtIntRate.Rows[0]["REMARKS"].ToString() : string.Empty;

                        dr["INTEREST_RATE"] = cxIntRate.EditValue;
                        dr["INT_EFFECTIVE_DATE"] = Convert.ToDateTime(cxEffDate.EditValue).Date;
                        dr["CREATED_BY"] = ProfitCashflow.oPcfDM.UserName;
                        dr["REMARKS"] = cxMemo1.EditValue;
                        dr.AcceptChanges();
                        dr.SetModified();

                        if (Convert.ToDateTime(dtIntRate.Rows[0]["INT_EFFECTIVE_DATE"]).Date != Convert.ToDateTime(cxEffDate.EditValue).Date)
                        {
                            var id = dal.RetrieveMaxInterestRateId();

                            DataRow dr1 = dtHistory.NewRow();
                            dr1["COMPANY_ID"] = dtIntRate.Rows[0]["COMPANY_ID"];
                            dr1["PREV_INTEREST_RATE"] = pOrgIntRate;
                            dr1["REMARKS"] = pOrgRemarks;
                            dr1["CREATED_BY"] = ProfitCashflow.oPcfDM.UserName;
                            dr1["CREATED_ON"] = DateTime.Now;
                            dr1["PREV_INT_EFFECTIVE_DATE"] = pOrgIntEffDate;
                            dr1["INTEREST_RATE_HISTORY_ID"] = id;
                            dtHistory.Rows.Add(dr1);
                        }
                    }
                    else
                    {
                        str_comp = str_comp + drSelected[1].ToString() + ",";
                    }
                }
                else
                {
                    dr.AcceptChanges();
                    dr.SetAdded();
                }
            }

            if (str_comp != string.Empty)
            {
                XtraMessageBox.Show("Interest rate has already been defined for company " + str_comp + " for the entered effective date." + System.Environment.NewLine + "Hence the rate cannot be changed.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (dtRateTable.GetChanges() != null)
                dal.UpdateInterestRate("I", dtRateTable);

            if (dtHistory.GetChanges() != null)
                dal.UpdateInterestRate("H", dtHistory.GetChanges());

            this.Close();
        }

        private void cxButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cxIntRate_Enter(object sender, EventArgs e)
        {
            cxIntRate.Properties.Mask.EditMask = "N4";
        }
    }
}