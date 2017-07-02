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

namespace Xpedeon.PCFSecurity
{
    public partial class AboutSecurity : DevExpress.XtraEditors.XtraForm
    {
        public AboutSecurity()
        {
            InitializeComponent();
        }

        private void AboutSecurity_Load(object sender, EventArgs e)
        {
            cxLabelVersion.Text = "Version : " + PCFSecurity.oSecDM.PCF_VERSION;
            cxLabeldbname.Text = "Database : " + PCFSecurity.oSecDM.PCF_DB_NAME;
            cxLabelhostname.Text = "Host : " + PCFSecurity.oSecDM.PCF_HOST_NAME;
        }
    }
}