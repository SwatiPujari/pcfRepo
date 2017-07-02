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
using DevExpress.XtraEditors.Repository;

namespace Xpedeon.ProfitCashflow
{
    public partial class fInterestRateHistory : DevExpress.XtraEditors.XtraForm
    {
        sInterestRateMaster dal;
        decimal company;

        public fInterestRateHistory()
        {
            InitializeComponent();
        }

        public void SetParameters(sInterestRateMaster d, decimal sCompany)
        {
            dal = d;
            company = sCompany;
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
                        dal.DeleteHistory(dr);

                        dr.Delete();
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

        private void fInterestRateHistory_Load(object sender, EventArgs e)
        {
            cxGrid1.DataSource = dal.RetrieveMaster("H", company);
        }

        private void cxGrid1DBTableView1_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.Column == cxGrid1DBTableView1PREV_INTEREST_RATE)
            {
                if (e.CellValue != null && e.CellValue.ToString() != string.Empty)
                {
                    //RepositoryItemTextEdit rpTxt = new RepositoryItemTextEdit();
                    //rpTxt.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                    //string a = string.Format("{0:C0}", e.CellValue.ToString());
                    //cxGrid1DBTableView1PREV_INTEREST_RATE.SummaryItem.DisplayFormat = "G29";
                }
            }
            //    else
            //        e.RepositoryItem = repTextEditIntRate;
            //}
        }

        private void cxGrid1DBTableView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column == cxGrid1DBTableView1PREV_INTEREST_RATE)
            {
                if (e.Value != null)
                    e.DisplayText = Convert.ToDecimal(e.Value).ToString("G29");
            }
        }
    }
}