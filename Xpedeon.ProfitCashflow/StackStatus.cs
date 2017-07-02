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
    public partial class StackStatus : DevExpress.XtraEditors.XtraForm
    {
        public string pLandStatus, pStackStatus;
        public StackStatus()
        {
            InitializeComponent();
        }

        private void cxBtnOK_Click(object sender, EventArgs e)
        {
            if ((rgStackStatus.EditValue == "R") && (pLandStatus != "B1" && pLandStatus != "ATL"))
            {
                XtraMessageBox.Show("Stack Status can be set to Release only when Land Status of the site is ''B1'' or ''ATL''.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.None;
            }
            else
            {
                this.DialogResult = DialogResult.OK;
            }
            
        }

        
    }
}