using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Xpedeon.ProfitCashflow
{
    public partial class SelCompany : DevExpress.XtraEditors.XtraForm
    {
        DataTable dtCompany;

        public SelCompany()
        {
            InitializeComponent();
        }

        private void SelCompany_Load(object sender, EventArgs e)
        {
            cxImageComboBoxShowCompanies_SelectedIndexChanged(sender, e);
        }

        private void cxImageComboBoxShowCompanies_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cxImageComboBoxShowCompanies.EditValue != null && !string.IsNullOrWhiteSpace(cxImageComboBoxShowCompanies.EditValue.ToString()))
            {
                string sStatus = GetStatus(Convert.ToString(cxImageComboBoxShowCompanies.EditValue));
                RetrieveCompanies(ProfitCashflow.oPcfDM.UserName, sStatus);

                if (dtCompany != null && dtCompany.Rows.Count > 0)
                    cxLookupComboBoxCompany.Properties.DataSource = dtCompany;
            }
        }

        private void cxBtnOK_Click(object sender, EventArgs e)
        {
            if (cxLookupComboBoxCompany.EditValue == null || string.IsNullOrWhiteSpace(cxLookupComboBoxCompany.EditValue.ToString()))
            {
                XtraMessageBox.Show("Please select a company before proceeding.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = System.Windows.Forms.DialogResult.None;
                return;
            }

            if (cxLookupComboBoxCompany.EditValue != null)
            {
                ProfitCashflow.oPcfDM.company_id = Convert.ToDecimal(cxLookupComboBoxCompany.EditValue);
                ProfitCashflow.oPcfDM.company_name = Convert.ToString(cxLookupComboBoxCompany.Text);
            }
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private string GetStatus(string sIcbStatus)
        {
            if (sIcbStatus == "AC") return "Y";
            else if (sIcbStatus == "IN") return "N";
            else return "A";
        }

        private void RetrieveCompanies(string sUserName, string sStatus)
        {
            SqlDataAdapter da = null;

            try
            {
                da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand("SPN_XP_GET_COMP_4_DEFAULT_SEL", ProfitCashflow.oPcfDM.conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@IV_PCF_USER", sUserName);
                da.SelectCommand.Parameters.AddWithValue("@IC_ISACTIVE", sStatus);

                dtCompany = new DataTable();
                da.Fill(dtCompany);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (da != null) da.Dispose();
            }
        }

        private void cxBtnCancel_Click(object sender, EventArgs e)
        {
            ProfitCashflow.oPcfDM.company_id = -1;
        }
    }
}