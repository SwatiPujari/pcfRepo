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
    public partial class CustDlgBox : DevExpress.XtraEditors.XtraForm
    {
        public int iWidth, iHeight;
        int line1, line2, ht;

        public CustDlgBox()
        {
            InitializeComponent();
        }

        public void MessageDlg(string msg, string strDlgType, string strBtns, int HelpIndx)
        {
            line1 = 100;
            line2 = 100;
            ht = 78;

            lblCustDlg.Text = msg;

            string[] sDlgType = strDlgType.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string DlgType in sDlgType)
                switch (DlgType)
                {
                    case "mtWarning":
                        lciImgWarning.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                        this.Text = "Warning";
                        break;
                    case "mtError":
                        lciImgError.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                        this.Text = "Error";
                        break;
                    case "mtInformation":
                        lciImgInfo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                        this.Text = "Information";
                        break;
                    case "mtConfirmation":
                        lciImgConf.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                        this.Text = "Confirmation";
                        break;
                }

            line1 = lblCustDlg.Width + 83;
            if (lblCustDlg.Height > 41)
                ht = ht + lblCustDlg.Height;
            else
            {
                ht = ht + 41;
                lblCustDlg.Height = 45;
            }

            string[] sBtns = strBtns.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string Btns in sBtns)
            {
                if (Btns == "mbYes")
                {
                    line2 = line2 + 80;
                    lciYes.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                }
                if (Btns == "mbNo")
                {
                    line2 = line2 + 80;
                    lciNo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                }
                if (Btns == "mbOk")
                {
                    line2 = line2 + 80;
                    lciOK.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                }
                if (Btns == "mbCancel")
                {
                    line2 = line2 + 80;
                    lciCancel.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                }
                if (Btns == "mbYesToAll")
                {
                    line2 = line2 + 80;
                    lciYesAll.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                }
                if (Btns == "mbNoToAll")
                {
                    line2 = line2 + 80;
                    lciNoAll.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                }
                if (Btns == "mbAbort")
                {
                    line2 = line2 + 80;
                    lciAbort.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                }
                if (Btns == "mbRetry")
                {
                    line2 = line2 + 80;
                    lciRetry.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                }
                if (Btns == "mbIgnore")
                {
                    line2 = line2 + 80;
                    lciIgnore.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                }
            }

            ht = ht + 25;
            this.Height = ht;
            if (line1 > line2)
                this.Width = line1 + 30;
            else
                this.Width = line2 + 30;
            emptySpaceItem3.Width = (this.Width - (32 + (sBtns.Length * 74))) / 2;

            iHeight = this.Height;
            iWidth = this.Width;
        }
    }
}