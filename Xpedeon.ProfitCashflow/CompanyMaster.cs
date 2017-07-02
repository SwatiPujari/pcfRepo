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
    public partial class CompanyMaster : DevExpress.XtraEditors.XtraForm
    {
        fPcfMain frmParent;
        sCompanyMaster oCompMast = new sCompanyMaster();
        DataTable dtCompany, dtOpCompanyIns;
        DataRow drCompany;

        string sDefaultCountry, sIsValided = "N";

        bool b_edit, b_JV_Comp, b_OP_Comp;
        string vSeed_Change;

        public CompanyMaster()
        {
            InitializeComponent();
        }

        private void CompanyMaster_Load(object sender, EventArgs e)
        {
            try
            {
                frmParent = (fPcfMain)this.MdiParent;

                DataTable dtCountryMast = oCompMast.RetrieveCountryMaster();
                repLookUpCountry.DataSource = dtCountryMast; 
                DataRow[] drDefaultCountry = dtCountryMast.Select("DEFAULT_ON_INSERT='Y'");
                if (drDefaultCountry.Length > 0)
                    sDefaultCountry = drDefaultCountry[0]["COUNTRY_CODE"].ToString();

                dtOpCompanyIns = oCompMast.RetrieveNonJVCompanys(ProfitCashflow.oPcfDM.UserName);
                repLookUpJvOpComp.DataSource = dtOpCompanyIns;

                btnNewCompany.Enabled = true;
                btnEditCompany.Enabled = false;
                btnSaveCompany.Enabled = false;
                btnCancelCompany.Enabled = false;

                frmParent.barStatusTxt.Caption = "Opening Company Master...";
                dtCompany = oCompMast.RetrieveCompanyForUser(ProfitCashflow.oPcfDM.UserName);

                cxGrid_Company.DataSource = dtCompany;
                cxDBVerticalGrid_Company.DataSource = dtCompany;

                if (dtCompany.Rows.Count > 0)
                    btnEditCompany.Enabled = true;

                //cxGrid_CompanydbtvDiv.RestoreFromIniFile(fPcfMain.GridLayoutDir,False,False,[],'Company Layout');

                cxDBVerticalGrid_Company.OptionsBehavior.Editable = false;
                frmParent.barStatusTxt.Caption = "Done.";
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CompanyMaster_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (drCompany.RowState == DataRowState.Added || drCompany.RowState == DataRowState.Modified ||
                btnSaveCompany.Enabled || dtCompany.GetChanges() != null)
            {
                DialogResult oResult = XtraMessageBox.Show("Data has been changed, do you want to save the changes?", Application.ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (oResult == DialogResult.Yes)
                    btnSaveCompany_Click(sender, new EventArgs());
                else if (oResult == DialogResult.No)
                    btnCancelCompany_Click(sender, new EventArgs());
                else
                    e.Cancel = true;
            }
        }

        private void CompanyMaster_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                switch (e.KeyCode.ToString().ToUpper())
                {
                    case "N":
                        if (btnNewCompany.Enabled)
                            btnNewCompany_Click(sender, new EventArgs());
                        break;
                    case "E":
                        if (btnEditCompany.Enabled)
                            btnEditCompany_Click(sender, new EventArgs());
                        break;
                    case "S":
                        if (btnSaveCompany.Enabled)
                            btnSaveCompany_Click(sender, new EventArgs());
                        break;
                    case "L":
                        if (btnCancelCompany.Enabled)
                            btnCancelCompany_Click(sender, new EventArgs());
                        break;
                }
            }

            if (e.Alt && e.KeyCode.ToString() == "1")
                if (cxGrid_Company.Enabled)
                    cxGrid_CompanydbtvDiv.Focus();
                else
                    e.Handled = true;
        }

        private void btnNewCompany_Click(object sender, EventArgs e)
        {
            cxGrid_Company.Enabled = false;
            cxDBVerticalGrid_Company.OptionsBehavior.Editable = true;

            cxDBVerticalGrid_Company.Focus();
            cxDBVerticalGrid_Company.FocusedRow = cxDBVerticalGrid_CompanyCOMPANY_CODE;

            btnNewCompany.Enabled = false;
            btnEditCompany.Enabled = false;
            btnSaveCompany.Enabled = true;
            btnCancelCompany.Enabled = true;

            dtCompany.Columns["COMPANY_CODE"].AllowDBNull = true;
            DataRow drNewComp = dtCompany.NewRow();

            if (!string.IsNullOrWhiteSpace(sDefaultCountry))
                drNewComp["COUNTRY_CODE"] = sDefaultCountry;

            drNewComp["IS_ACTIVE"] = "Y";
            drNewComp["DEFAULT_LOGO"] = "N";
            drNewComp["CREATED_BY"] = ProfitCashflow.oPcfDM.UserName;
            drNewComp["CREATED_ON"] = DateTime.Now.Date;
            drNewComp["LAST_UPDATED"] = DateTime.Now;

            drNewComp["JV_COMPANY"] = "N";
            drNewComp["OP_COMPANY_ID"] = DBNull.Value;
            drNewComp["JV_COMPANY_BG_PCT"] = 0;

            dtCompany.Rows.Add(drNewComp);
            cxGrid_CompanydbtvDiv.FocusedRowHandle = cxGrid_CompanydbtvDiv.GetRowHandle(dtCompany.Rows.IndexOf(drNewComp));

            cxDBVerticalGrid_CompanyCOMPANY_CODE.Properties.AllowEdit = true;
            cxDBVerticalGrid_CompanyOP_COMPANY_ID.Properties.AllowEdit = false;
            cxDBVerticalGrid_CompanyJV_COMPANY_BG_PCT.Properties.AllowEdit = false;

            b_edit = false; sIsValided = "N";
        }

        private void btnEditCompany_Click(object sender, EventArgs e)
        {
            if (drCompany == null) return;

            cxGrid_Company.Enabled = false;
            cxDBVerticalGrid_Company.OptionsBehavior.Editable = true;

            btnNewCompany.Enabled = false;
            btnEditCompany.Enabled = false;
            btnSaveCompany.Enabled = true;
            btnCancelCompany.Enabled = true;

            if (drCompany.RowState == DataRowState.Added)
                cxDBVerticalGrid_CompanyCOMPANY_CODE.Properties.AllowEdit = true;
            else
                cxDBVerticalGrid_CompanyCOMPANY_CODE.Properties.AllowEdit = false;

            if (drCompany["JV_COMPANY"] != DBNull.Value && drCompany["JV_COMPANY"].ToString() == "Y")
            {
                cxDBVerticalGrid_CompanyOP_COMPANY_ID.Properties.AllowEdit = true;
                cxDBVerticalGrid_CompanyJV_COMPANY_BG_PCT.Properties.AllowEdit = true;
            }
            else
            {
                cxDBVerticalGrid_CompanyOP_COMPANY_ID.Properties.AllowEdit = false;
                cxDBVerticalGrid_CompanyJV_COMPANY_BG_PCT.Properties.AllowEdit = false;
            }

            b_edit = true; sIsValided = "N";
        }

        private void btnSaveCompany_Click(object sender, EventArgs e)
        {
            frmParent.barStatusTxt.Caption = "Saving Company Master...";

            if (!cxDBVerticalGrid_Company.PostEditor()) return;
            if (!cxDBVerticalGrid_Company.UpdateFocusedRecord()) return;

            if (sIsValided == "N" && (cxDBVerticalGrid_Company.GetCellValue(cxDBVerticalGrid_CompanyCOMPANY_CODE, cxDBVerticalGrid_Company.FocusedRecord) == null ||
                cxDBVerticalGrid_Company.GetCellValue(cxDBVerticalGrid_CompanyCOMPANY_CODE, cxDBVerticalGrid_Company.FocusedRecord).ToString().Trim() == ""))
                cxDBVerticalGrid_Company.SetCellValue(cxDBVerticalGrid_CompanyCOMPANY_CODE, cxDBVerticalGrid_Company.FocusedRecord, "");

            if (sIsValided == "E") return;
            cxDBVerticalGrid_Company.CloseEditor();

            try
            {
                if (dtCompany.GetChanges() != null)
                {
                    dtCompany.Constraints.Clear();
                    dtCompany.Constraints.Add("PK_COMP", dtCompany.Columns["COMPANY_CODE"], true);

                    DataTable dtSaveComp = oCompMast.UpdateCompanyMaster(dtCompany.GetChanges());
                    dtCompany.Merge(dtSaveComp);

                    dtCompany.Constraints.Clear();
                    dtCompany.AcceptChanges();

                    if (vSeed_Change == "Y")
                    {
                        oCompMast.UpdateCompanyCurrentSeed(Convert.ToDecimal(drCompany["COMPANY_ID"]));
                        vSeed_Change = "N";
                    }
                    if (b_JV_Comp)
                        oCompMast.UpdateJVOperatingCompany(Convert.ToDecimal(drCompany["COMPANY_ID"]));
                    if (b_OP_Comp)
                        oCompMast.UpdateJVOperatingCompany(Convert.ToDecimal(drCompany["COMPANY_ID"]), Convert.ToDecimal(drCompany["OP_COMPANY_ID"]));
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            cxGrid_Company.Enabled = true;
            cxDBVerticalGrid_Company.OptionsBehavior.Editable = false;

            btnNewCompany.Enabled = true;
            btnEditCompany.Enabled = true;
            btnSaveCompany.Enabled = false;
            btnCancelCompany.Enabled = false;

            frmParent.barStatusTxt.Caption = "Done.";
        }

        private void btnCancelCompany_Click(object sender, EventArgs e)
        {
            cxDBVerticalGrid_Company.CloseEditor();
            cxDBVerticalGrid_Company.CancelUpdateFocusedRecord();
            dtCompany.RejectChanges();

            cxGrid_Company.Enabled = true;
            cxDBVerticalGrid_Company.OptionsBehavior.Editable = false;

            btnNewCompany.Enabled = true;
            btnEditCompany.Enabled = dtCompany.Rows.Count > 0 ? true : false;
            btnSaveCompany.Enabled = false;
            btnCancelCompany.Enabled = false;

            b_edit = false; sIsValided = "N";
        }

        private void cxDBVerticalGrid_Company_KeyDown(object sender, KeyEventArgs e)
        {
            if(cxDBVerticalGrid_Company.OptionsBehavior.Editable)
                if (e.Control && e.KeyCode == Keys.Delete && cxDBVerticalGrid_Company.RecordCount > 0)
                {
                    DialogResult oResult = XtraMessageBox.Show("Do you want to delete the record?", Application.ProductName, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (oResult == System.Windows.Forms.DialogResult.OK)
                    {
                        int iRecordIndex = cxDBVerticalGrid_Company.FocusedRecord;
                        try
                        {
                            object oChkComp = oCompMast.RetrieveCheckCompanyUsage(Convert.ToDecimal(drCompany["COMPANY_ID"]));
                            if (oChkComp != null && Convert.ToDecimal(oChkComp) > 0)
                            {
                                XtraMessageBox.Show("Cannot delete, as data has been entered for the selected Company.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            drCompany.Delete();
                            oCompMast.UpdateCompanyMaster(dtCompany.GetChanges());
                            dtCompany.AcceptChanges();

                            cxGrid_Company.Enabled = true;
                            cxDBVerticalGrid_Company.OptionsBehavior.Editable = false;

                            btnNewCompany.Enabled = true;
                            btnEditCompany.Enabled = dtCompany.Rows.Count > 0 ? true : false;
                            btnSaveCompany.Enabled = false;
                            btnCancelCompany.Enabled = false;
                        }
                        catch (Exception ex)
                        {
                            XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);

                            dtCompany.RejectChanges();
                            cxDBVerticalGrid_Company.FocusedRecord = iRecordIndex;
                            return;
                        }
                    }
                }
        }

        private void cxDBVerticalGrid_Company_ValidateRecord(object sender, DevExpress.XtraVerticalGrid.Events.ValidateRecordEventArgs e)
        {
            if (drCompany == null) return;

            if (drCompany["COMPANY_CODE"] == DBNull.Value || string.IsNullOrWhiteSpace(drCompany["COMPANY_CODE"].ToString().Trim()))
            {
                XtraMessageBox.Show("Company Code is mandatory.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Valid = false; sIsValided = "E";
                return;
            }
            if (drCompany["NAME"] == DBNull.Value || string.IsNullOrWhiteSpace(drCompany["NAME"].ToString().Trim()))
            {
                XtraMessageBox.Show("Company Name is mandatory.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Valid = false; sIsValided = "E";
                return;
            }

            if (drCompany["SITE_CODE_PREFIX"] == DBNull.Value || string.IsNullOrWhiteSpace(drCompany["SITE_CODE_PREFIX"].ToString().Trim()) ||
                Convert.ToString(drCompany["SITE_CODE_PREFIX"]).Trim().Length != 2)
            {
                XtraMessageBox.Show("Site Code Prefix should comprise of 2 characters.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Valid = false; sIsValided = "E";
                return;
            }

            if (drCompany["INITIAL_SEED_VALUE"] != DBNull.Value && !string.IsNullOrWhiteSpace(drCompany["INITIAL_SEED_VALUE"].ToString().Trim()) &&
                Convert.ToString(drCompany["INITIAL_SEED_VALUE"]).Trim().Length > 4)
            {
                XtraMessageBox.Show("Initial Seed Value should comprise of 4 numbers.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Valid = false; sIsValided = "E";
                return;
            }

            if (drCompany.RowState == DataRowState.Added || (drCompany.RowState == DataRowState.Modified &&
                Convert.ToString(drCompany["COMPANY_CODE", DataRowVersion.Original]) != Convert.ToString(drCompany["COMPANY_CODE", DataRowVersion.Current])))
            {
                object oResult = oCompMast.RetrieveDuplicateCompanyCount(Convert.ToString(drCompany["COMPANY_CODE"]));
                if (oResult != null && Convert.ToDecimal(oResult) > 0)
                {
                    XtraMessageBox.Show("Company code cannot be duplicate.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Valid = false; sIsValided = "E";
                    return;
                }
            }

            if (drCompany["JV_COMPANY"] != DBNull.Value && Convert.ToString(drCompany["JV_COMPANY"]) == "Y")
            {
                if (drCompany["OP_COMPANY_ID"] == DBNull.Value || string.IsNullOrWhiteSpace(drCompany["OP_COMPANY_ID"].ToString()) || Convert.ToDecimal(drCompany["OP_COMPANY_ID"]) == 0)
                {
                    XtraMessageBox.Show("Please select Operating Company as the Company is a JV Company.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Valid = false; sIsValided = "E";
                    return;
                }
            }
            else if (drCompany.RowState == DataRowState.Modified && !drCompany["JV_COMPANY", DataRowVersion.Original].Equals(drCompany["JV_COMPANY", DataRowVersion.Current]) &&
                (drCompany["JV_COMPANY"] == DBNull.Value || string.IsNullOrWhiteSpace(drCompany["JV_COMPANY"].ToString()) || drCompany["JV_COMPANY"].ToString() == "N"))
            {
                drCompany["OP_COMPANY_ID"] = DBNull.Value;
                b_JV_Comp = true;
            }

            if (drCompany.RowState == DataRowState.Modified && !drCompany["OP_COMPANY_ID", DataRowVersion.Original].Equals(drCompany["OP_COMPANY_ID", DataRowVersion.Current]) &&
                drCompany["OP_COMPANY_ID"] != DBNull.Value && !string.IsNullOrWhiteSpace(drCompany["OP_COMPANY_ID"].ToString()))
                b_OP_Comp = true;

            drCompany["CREATED_BY"] = ProfitCashflow.oPcfDM.UserName;
            drCompany["LAST_UPDATED"] = DateTime.Now;
            sIsValided = "Y";
        }

        private void cxDBVerticalGrid_Company_InvalidValueException(object sender, DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void cxDBVerticalGrid_Company_InvalidRecordException(object sender, DevExpress.XtraVerticalGrid.Events.InvalidRecordExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void cxGrid_CompanydbtvDiv_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (cxGrid_CompanydbtvDiv.IsFilterRow(e.FocusedRowHandle)) 
                return;
            drCompany = cxGrid_CompanydbtvDiv.GetDataRow(e.FocusedRowHandle);
        }

        private void repChkJvComp_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckEdit)sender).Checked)
            {
                cxDBVerticalGrid_CompanyOP_COMPANY_ID.Properties.AllowEdit = true;
                cxDBVerticalGrid_CompanyJV_COMPANY_BG_PCT.Properties.AllowEdit = true;
            }
            else
            {
                drCompany["OP_COMPANY_ID"] = DBNull.Value;
                cxDBVerticalGrid_CompanyOP_COMPANY_ID.Properties.AllowEdit = false;
                cxDBVerticalGrid_CompanyJV_COMPANY_BG_PCT.Properties.AllowEdit = false;
            }
        }

        private void repTxtIntSeedValue_Validating(object sender, CancelEventArgs e)
        {
            object oEditValue = ((TextEdit)sender).EditValue;
            if (oEditValue != null && !string.IsNullOrWhiteSpace(oEditValue.ToString()))
                if (drCompany["COMPANY_ID"] != DBNull.Value && !string.IsNullOrWhiteSpace(drCompany["COMPANY_ID"].ToString()))
                {
                    try
                    {
                        decimal dCompanyId = Convert.ToDecimal(drCompany["COMPANY_ID"]);
                        string sPrefix = cxDBVerticalGrid_Company.GetCellValue(cxDBVerticalGrid_CompanySITE_CODE_PREFIX, cxDBVerticalGrid_Company.FocusedRecord).ToString();
                        decimal dSeedVal = Convert.ToDecimal(oEditValue);

                        object oResult = oCompMast.RetrieveCompanyInitialSeed(dCompanyId, sPrefix, dSeedVal);
                        if (oResult != null && Convert.ToDecimal(oResult) > 0)
                        {
                            XtraMessageBox.Show("Select another Initial Seed Value, as " + Convert.ToString(oEditValue) + " already exists in Site Setup.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            e.Cancel = true;
                            return;
                        }
                        else
                            vSeed_Change = "Y";
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        e.Cancel = true;
                        return;
                    }
                }
            e.Cancel = false;
        }

        private void repLookUpJvOpComp_Enter(object sender, EventArgs e)
        {
            if (drCompany != null && b_edit)
            {
                try
                {
                    object oResult = oCompMast.RetrieveSiteSetupCount4Company(Convert.ToDecimal(drCompany["COMPANY_ID"]));
                    if (oResult != null && Convert.ToDecimal(oResult) > 0)
                        repLookUpJvOpComp.DataSource = dtOpCompanyIns;
                    else
                        repLookUpJvOpComp.DataSource = oCompMast.RetrieveJVOperatingCompany(Convert.ToDecimal(drCompany["COMPANY_ID"]), ProfitCashflow.oPcfDM.UserName);
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}