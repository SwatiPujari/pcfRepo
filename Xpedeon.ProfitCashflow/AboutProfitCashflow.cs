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
    public partial class AboutProfitCashflow : DevExpress.XtraEditors.XtraForm
    {
        public AboutProfitCashflow()
        {
            InitializeComponent();
        }

        private void AboutSecurity_Load(object sender, EventArgs e)
        {
            cxLabelVersion.Text = "Version : " + ProfitCashflow.oPcfDM.PCF_VERSION;
            cxLabeldbname.Text = "Database : " + ProfitCashflow.oPcfDM.PCF_DB_NAME;
            cxLabelhostname.Text = "Host : " + ProfitCashflow.oPcfDM.PCF_HOST_NAME;
        }
    }
}