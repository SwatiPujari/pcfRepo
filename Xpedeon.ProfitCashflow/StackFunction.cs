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
    public partial class StackFunction : DevExpress.XtraEditors.XtraForm
    {
        public string sFormType;   //AP-Apportion;FP-FinalisePlots;UP-UnfinalisePlots;PF-PushToPF

        public StackFunction()
        {
            InitializeComponent();
        }

        private void StackFunction_Load(object sender, EventArgs e)
        {
            lcgApportion.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            lcgPlotSelection.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            lcgSalesCF.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            lcgBtnPushUpdates.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            lcgBtnFinalisePlot.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            lcgBtnUnfinalisePlot.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            lcgBtnPushToPF.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            if (sFormType == "AP")
            {
                this.Text = "Apportion Across Plots";
                lcgApportion.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
            else if (sFormType == "FP")
            {
                this.Text = "Finalise Plots of Release Stack";
                lcgPlotSelection.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                
                lcgBtnPushUpdates.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lcgBtnFinalisePlot.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

                int iWidth = (int)(layoutControlItem6.Width - lcgBtnFinalisePlot.Width) / 2;
                emptySpaceItem12.Width = iWidth;
            }
            else if (sFormType == "UP")
            {
                this.Text = "Unfinalise Plots of Release Stack";
                lcgPlotSelection.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

                lcgBtnPushUpdates.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lcgBtnUnfinalisePlot.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

                int iWidth = (int)(layoutControlItem6.Width - lcgBtnUnfinalisePlot.Width) / 2;
                emptySpaceItem12.Width = iWidth;
            }
            else if (sFormType == "PF")
            {
                this.Text = "Push Updates from Stack to Profit Forecast";

                lcgPlotSelection.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lcgSalesCF.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

                lcgBtnPushUpdates.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lcgBtnPushToPF.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

                emptySpaceItem12.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                int iWidth = (int)(layoutControlItem6.Width - (layoutControlItem26.Width + layoutControlItem27.Width)) / 2;
                emptySpaceItem13.Width = iWidth;
            }
        }
    }
}