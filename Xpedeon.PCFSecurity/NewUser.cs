using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Xpedeon.PCFSecurity
{
    public partial class NewUser : DevExpress.XtraEditors.XtraForm
    {
        public string sUserName = "";
        string sPrevPassword, sIsValided = "N";

        sUserSetup oUserSetup = new sUserSetup();
        SecurityMainfrm oSecMainFrm = new SecurityMainfrm();

        DataTable dtUserSetupInfo = null;
        DataRow drUserSetup = null;

        public NewUser()
        {
            InitializeComponent();
        }

        private void NewUser_Load(object sender, EventArgs e)
        {
            dtUserSetupInfo = oUserSetup.RetrieveUserSetupInfo(sUserName);
            cxDBVerGridUserCreation.DataSource = dtUserSetupInfo;
            
            if (dtUserSetupInfo != null && dtUserSetupInfo.Rows.Count > 0)
            {
                cxDBVerGridUserCreationUSERNAME.Properties.AllowEdit = false;
                cxDBVerGridUserCreationUSERNAME.TabStop = false;
                cxDBVerGridUserCreation.FocusedRow = cxDBVerGridUserCreationPASSWORD;

                drUserSetup = dtUserSetupInfo.Rows[0];
                sPrevPassword = Convert.ToString(drUserSetup["USER_PASSWORD"]);
            }
            else
            {
                cxDBVerGridUserCreationUSERNAME.Properties.AllowEdit = true;
                cxDBVerGridUserCreationUSERNAME.TabStop = true;
                cxDBVerGridUserCreation.FocusedRow = cxDBVerGridUserCreationUSERNAME;

                drUserSetup = dtUserSetupInfo.NewRow();

                drUserSetup["ENABLE_DISABLE"] = "Y";
                drUserSetup["FUNCTION_FLAG"] = 0;

                drUserSetup["COMPANY_MASTER_VIEW"] = "N";
                drUserSetup["PCF_PLOT_MODELLING"] = "N";
                drUserSetup["PCF_PURCHASE_TYPE"] = "N";
                drUserSetup["PCF_INTEREST_RATE"] = "N";
                drUserSetup["PCF_B4_SITE_CODES"] = "N";
                drUserSetup["PCF_CASHFLOW_CATEGORIES"] = "N";
                drUserSetup["PCF_SITE_SETUP"] = "N";
                drUserSetup["PCF_B4_STACK"] = "N";
                drUserSetup["PCF_STACKS"] = "N";
                drUserSetup["PCF_PROFIT_FORECAST"] = "N";
                drUserSetup["PCF_ADJUSTMENTS"] = "N";
                drUserSetup["PCF_KEY_DATES"] = "N";
                drUserSetup["PCF_TAKE_TO_SALES"] = "N";
                drUserSetup["PCF_CASHFLOW"] = "N";
                drUserSetup["PCF_LOCATION_MASTER"] = "N";
                drUserSetup["PCF_PLOT_TYPE_MASTER"] = "N";
                drUserSetup["PCF_RESIDENCE_TYPE_MASTER"] = "N";
                drUserSetup["PCF_UNIT_CATEGORY_MASTER"] = "N";
                drUserSetup["PCF_BLOCK_MASTER"] = "N";
                drUserSetup["PCF_CASHFLOW_OTHER_INCOME"] = "N";
                drUserSetup["PCF_CASHFLOW_OTHER_EXPENSE"] = "N";
                drUserSetup["PCF_ACC_PERIOD"] = "N";
                drUserSetup["PCF_EDIT_FROZEN_STACK"] = "N";
                drUserSetup["PCF_EDIT_LOCKED_STACK"] = "N";
                drUserSetup["PCF_B4_SITE_SETUP"] = "N";
                drUserSetup["PCF_DELETE_STACK"] = "N";
                drUserSetup["PCF_RTS_PLOT"] = "N";
                drUserSetup["PCF_RTS_PARKING"] = "N";
                drUserSetup["PCF_RTS_GR"] = "N";
                drUserSetup["PCF_EDIT_PROG_DATE"] = "N";
                drUserSetup["PCF_REVERSE_TTS_PLOT"] = "N";
                drUserSetup["PCF_REVERSE_TTS_PARK"] = "N";
                drUserSetup["PCF_REVERSE_TTS_GR"] = "N";
                drUserSetup["PCF_INCL_EXCL_PLOTS"] = "N";
                drUserSetup["PCF_USE_STACKS_IN_REPORT"] = "N";
                drUserSetup["PCF_PROCESS_TO_PREV_STACK"] = "N";
                drUserSetup["PCF_RESTACK"] = "N";
                drUserSetup["PCF_ZERO_WIP"] = "N";
                drUserSetup["PCF_UNFINALISE_PLOTS"] = "N";
                drUserSetup["PCF_PF_VIEW_MASTER"] = "N";
                drUserSetup["PCF_PF_ASPECT_MASTER"] = "N";
                drUserSetup["PCF_PF_GARAGE_PARKING_MASTER"] = "N";
                drUserSetup["PCF_PF_BALCONY_TERRACE_MASTER"] = "N";
                drUserSetup["PCF_SS_LAND_TYPE"] = "N";
                drUserSetup["PCF_SS_BUILD_TYPE"] = "N";
                drUserSetup["PCF_DIFF_STACKS_PF_REP"] = "N";
                drUserSetup["PCF_PFU_STACK"] = "N";
                drUserSetup["PCF_PF_SUMMARY"] = "N";
                drUserSetup["PCF_DELETE_PF"] = "N";
                drUserSetup["PCF_IMP_PF"] = "N";
                drUserSetup["PCF_CORPORATE_USER"] = "N";
                drUserSetup["PCF_MAINTAIN_BLACKOUT_PERIOD"] = "N";

                drUserSetup["PCF_GROUP_MASTER"] = "N";
                drUserSetup["PCF_DIVISION_MASTER"] = "N";
                drUserSetup["PCF_REGION_MASTER"] = "N";
                drUserSetup["PCF_OPERATING_COMPANY_MASTER"] = "N";
                drUserSetup["PCF_DEVELOPMENT_MASTER"] = "N";

                dtUserSetupInfo.Rows.Add(drUserSetup);
            }
        }

        private void NewUser_FormClosing(object sender, FormClosingEventArgs e)
        {
            cxDBVerGridUserCreation.CancelUpdate();
            drUserSetup.RejectChanges();
            dtUserSetupInfo.RejectChanges();
        }

        private void dxBarBtnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            cxDBVerGridUserCreation.PostEditor();
            cxDBVerGridUserCreation.UpdateFocusedRecord();

            if (sIsValided == "N")
                cxDBVerGridUserCreation.SetCellValue(cxDBVerGridUserCreationUSERNAME, 0, "");

            if (sIsValided == "E") return;
            if (drUserSetup == null) return;

            string sType = "";
            if (drUserSetup.RowState == DataRowState.Added)
                sType = "I";
            else if (drUserSetup.RowState == DataRowState.Modified)
                sType = "U";

            try
            {
                if (!string.IsNullOrWhiteSpace(sType))
                    oUserSetup.UpdateUserSetup(sType, drUserSetup);
                drUserSetup.AcceptChanges();
                dtUserSetupInfo.AcceptChanges();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (sType == "I") sUserName = Convert.ToString(drUserSetup["USERNAME"]);
            this.Close();
        }

        private void dxBarBtnCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            cxDBVerGridUserCreation.CancelUpdate();
            drUserSetup.RejectChanges();
            dtUserSetupInfo.RejectChanges();
            
            this.Close();
        }

        private void cxDBVerGridUserCreation_InvalidRecordException(object sender, DevExpress.XtraVerticalGrid.Events.InvalidRecordExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void cxDBVerGridUserCreation_ValidateRecord(object sender, DevExpress.XtraVerticalGrid.Events.ValidateRecordEventArgs e)
        {
            DevExpress.XtraVerticalGrid.VGridControl vGrid = sender as DevExpress.XtraVerticalGrid.VGridControl;
            string sUsername = Convert.ToString(vGrid.GetCellValue(cxDBVerGridUserCreationUSERNAME, e.RecordIndex));
            if (drUserSetup == null) return;

            //if (sUsername.IndexOf(' ') > 0)
            if (drUserSetup["USERNAME"] != DBNull.Value && Convert.ToString(drUserSetup["USERNAME"]).IndexOf(' ') > 0)
            {
                oSecMainFrm.MessageDlg("Blank Spaces are not allowed in User Name.", "mtError", "mbOk", 0);
                e.Valid = false; sIsValided = "E";
                return;
            }

            //if (sUsername.Trim() == "")
            if (drUserSetup["USERNAME"] == DBNull.Value || string.IsNullOrWhiteSpace(drUserSetup["USERNAME"].ToString().Trim()))
            {
                oSecMainFrm.MessageDlg("User Name cannot be blank.", "mtError", "mbOk", 0);
                e.Valid = false; sIsValided = "E";
                return;
            }
            else if (Convert.ToString(drUserSetup["USERNAME"]).Length > 27)
            {
                oSecMainFrm.MessageDlg("User Name exceeds maximum permissible length of 27.", "mtError", "mbOk", 0);
                e.Valid = false; sIsValided = "E";
                return;
            }
            else
                drUserSetup["USERNAME"] = Convert.ToString(drUserSetup["USERNAME"]).Trim().ToUpper();

            //if(Convert.ToString(vGrid.GetCellValue(cxDBVerGridUserCreationPASSWORD, e.RecordIndex)).Trim() == "")
            if (drUserSetup["USER_PASSWORD"] == DBNull.Value || string.IsNullOrWhiteSpace(drUserSetup["USER_PASSWORD"].ToString().Trim()))
            {
                oSecMainFrm.MessageDlg("Password cannot be blank.", "mtError", "mbOk", 0);
                e.Valid = false; sIsValided = "E";
                return;
            }

            //if (Convert.ToString(vGrid.GetCellValue(cxDBVerGridUserCreationCONfPASSWORD, e.RecordIndex)).Trim() == "")
            if (drUserSetup["CON_PASSWORD"] == DBNull.Value || string.IsNullOrWhiteSpace(drUserSetup["CON_PASSWORD"].ToString().Trim()))
            {
                oSecMainFrm.MessageDlg("Confirm Password cannot be blank.", "mtError", "mbOk", 0);
                e.Valid = false; sIsValided = "E";
                return;
            }

            /*if (Convert.ToString(vGrid.GetCellValue(cxDBVerGridUserCreationMaintainBlackoutPeriod, e.RecordIndex)) == "Y" &&
                Convert.ToString(vGrid.GetCellValue(cxDBVerGridUserCreationCorporateUser, e.RecordIndex)) == "N")*/
            if (drUserSetup["PCF_MAINTAIN_BLACKOUT_PERIOD"] != DBNull.Value && Convert.ToString(drUserSetup["PCF_MAINTAIN_BLACKOUT_PERIOD"]) == "Y" &&
                drUserSetup["PCF_CORPORATE_USER"] != DBNull.Value && Convert.ToString(drUserSetup["PCF_CORPORATE_USER"]) == "N")
            {
                oSecMainFrm.MessageDlg("Only a corporate user can maintain blackout period.", "mtError", "mbOk", 0);
                e.Valid = false; sIsValided = "E";
                return;
            }

            if (drUserSetup.RowState == DataRowState.Added)
            {
                object oResult = oUserSetup.CheckUserNameExist(drUserSetup["USERNAME"].ToString());
                if (oResult != null && Convert.ToInt32(oResult) > 0)
                {
                    oSecMainFrm.MessageDlg("User Name already exits.", "mtError", "mbOk", 0);
                    e.Valid = false; sIsValided = "E";
                    return;
                }

                if (Convert.ToString(drUserSetup["USER_PASSWORD"]) != Convert.ToString(drUserSetup["CON_PASSWORD"]))
                {
                    oSecMainFrm.MessageDlg("Password confirmation is wrong.", "mtError", "mbOk", 0);
                    e.Valid = false; sIsValided = "E";
                    return;
                }

                try
                {
                    drUserSetup["USER_PASSWORD"] = XpedeonCrypto.XpedeonServerEncrypt(drUserSetup["USER_PASSWORD"].ToString());
                }
                catch (Exception ex)
                {
                    e.Valid = false; sIsValided = "E";
                    throw ex;
                }
            }
            else //if (drUserSetup.RowState == DataRowState.Modified)
                if (sPrevPassword != Convert.ToString(drUserSetup["USER_PASSWORD"]))
                {
                    if (Convert.ToString(drUserSetup["USER_PASSWORD"]) != Convert.ToString(drUserSetup["CON_PASSWORD"]))
                    {
                        oSecMainFrm.MessageDlg("Password confirmation is wrong.", "mtError", "mbOk", 0);
                        e.Valid = false; sIsValided = "E";
                        return;
                    }

                    try
                    {
                        drUserSetup["USER_PASSWORD"] = XpedeonCrypto.XpedeonServerEncrypt(drUserSetup["USER_PASSWORD"].ToString());
                    }
                    catch (Exception ex)
                    {
                        e.Valid = false; sIsValided = "E";
                        throw ex;
                    }

                    drUserSetup["PASSWORD_UPDATED_ON"] = DateTime.Now;
                }

            sIsValided = "Y";
        }
    }
}