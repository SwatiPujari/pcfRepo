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
    public partial class ErpUsers : DevExpress.XtraEditors.XtraForm
    {
        sUserSetup oUserSetup = new sUserSetup();
        DataTable dtUserComp, dtAllSiteUsers;

        public ErpUsers()
        {
            InitializeComponent();
        }

        private void ErpUsers_Load(object sender, EventArgs e)
        {
            dtAllSiteUsers = oUserSetup.RetrieveAllSiteUsers();
            cxGridUsers.DataSource = dtAllSiteUsers;

            cxGridUsersDBTV.BeginSort(); cxGridUsersDBTV.EndSort();
            cxGrid1DBTableView1.BeginSort(); cxGrid1DBTableView1.EndSort();
        }

        private void dxBarBtnNewUser_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            NewUser oNewUser = new NewUser();
            oNewUser.Text = "New User";
            oNewUser.ShowDialog();

            if (!string.IsNullOrWhiteSpace(oNewUser.sUserName))
            {
                dtAllSiteUsers = oUserSetup.RetrieveAllSiteUsers();
                cxGridUsers.DataSource = dtAllSiteUsers;
                cxGridUsersDBTV.FocusedRowHandle = cxGridUsersDBTV.GetRowHandle(dtAllSiteUsers.Rows.IndexOf(dtAllSiteUsers.Rows.Find(oNewUser.sUserName)));
            }
        }

        private void dxBarBtnEditUser_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (cxGridUsersDBTV.FocusedRowHandle < 0) return;

            NewUser oNewUser = new NewUser();
            oNewUser.sUserName = Convert.ToString(cxGridUsersDBTV.GetFocusedRowCellValue(cxGridUsersDBTVUSERNAME));
            oNewUser.Text = "Edit User";
            oNewUser.ShowDialog();

            dtAllSiteUsers = oUserSetup.RetrieveAllSiteUsers();
            cxGridUsers.DataSource = dtAllSiteUsers;
            cxGridUsersDBTV.FocusedRowHandle = cxGridUsersDBTV.GetRowHandle(dtAllSiteUsers.Rows.IndexOf(dtAllSiteUsers.Rows.Find(oNewUser.sUserName)));
        }

        private void GridView_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle == DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
                e.Appearance.BackColor = System.Drawing.Color.Ivory;
        }

        private void cxGridUsersDBTV_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (cxGridUsersDBTV.FocusedRowHandle >= 0)
            {
                string sUser = Convert.ToString(cxGridUsersDBTV.GetFocusedRowCellValue(cxGridUsersDBTVUSERNAME));
                dtUserComp = oUserSetup.RetrieveUserCompanies(sUser);
                cxGrid1.DataSource = dtUserComp;
            }
        }

        private void cxGrid1DBTableView1_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            if(e.Row == null) return;

            string sType = "";
            string sUserName = Convert.ToString(cxGridUsersDBTV.GetFocusedRowCellValue(cxGridUsersDBTVUSERNAME));

            DataRow drCompLinkage = ((DataRowView)e.Row).Row;
            if (drCompLinkage.RowState == DataRowState.Modified)
                if (Convert.ToString(drCompLinkage["COMPANY_LINK", DataRowVersion.Original]) == "N" &&
                    Convert.ToString(drCompLinkage["COMPANY_LINK", DataRowVersion.Current]) == "Y")
                    sType = "I";
                else if (Convert.ToString(drCompLinkage["COMPANY_LINK", DataRowVersion.Original]) == "Y" &&
                         Convert.ToString(drCompLinkage["COMPANY_LINK", DataRowVersion.Current]) == "N")
                    sType = "D";

            try
            {
                if (!string.IsNullOrWhiteSpace(sType))
                    oUserSetup.UpdateUserCompanies(sType, sUserName, Convert.ToDecimal(drCompLinkage["COMPANY_ID"]));

                drCompLinkage.AcceptChanges();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void cxGridUsersDBTV_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && cxGridUsersDBTV.FocusedRowHandle >= 0)
                popupMenu1.ShowPopup(Control.MousePosition);
        }
    }
}