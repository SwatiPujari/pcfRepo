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
using System.Globalization;

namespace Xpedeon.ProfitCashflow
{
    public partial class fAccPeriod : DevExpress.XtraEditors.XtraForm
    {
        sAccountingPeriod oAccountPeriod = new sAccountingPeriod();
        DataTable dtAccountPeriod = new DataTable();
        DataRow drParent;

        string vPeriodType = "";
        private DateTime vOldEndDate;
        private DateTime vNewEndDate;
        private string vOldName;
        private string vNewName;

        private Boolean vIns;
        bool bHasChanges = false;

        public fAccPeriod()
        {
            InitializeComponent();
        }

        private void fAccPeriod_Load(object sender, EventArgs e)
        {
            repLookUpEditCompany.DataSource = oAccountPeriod.RetrieveCompany(ProfitCashflow.oPcfDM.UserName);

            dtAccountPeriod = oAccountPeriod.RetrieveAccountingPeriod(ProfitCashflow.oPcfDM.UserName);
            cxDBTreeList1.DataSource = dtAccountPeriod;

            dxBarBtnsave.Enabled = false;
            dxBarBtnCancel.Enabled = false;
        }

        private bool Validation(DevExpress.XtraTreeList.Nodes.TreeListNode tlNode)
        {
            if (tlNode.Id < 0) return true;

            var currDr = (DataRow)dtAccountPeriod.Rows[tlNode.Id];
            if (currDr != null && currDr.RowState == DataRowState.Added)
            {
                vIns = true;
            }
            else
            {
                vIns = false;
            }
            // mandatory messages
            if (currDr != null && (currDr.RowState == DataRowState.Added || currDr.RowState == DataRowState.Modified))
            {
                //if (currDr["PERIOD_TYPE"] != DBNull.Value)
                //    vPeriodType = currDr["PERIOD_TYPE"].ToString();

                

                if (currDr["COMPANY_ID"] == DBNull.Value || string.IsNullOrEmpty(currDr["COMPANY_ID"].ToString().Trim()))
                {
                    XtraMessageBox.Show("Company is mandatory.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                if (currDr["PERIOD_CODE"] == DBNull.Value || string.IsNullOrEmpty(currDr["PERIOD_CODE"].ToString().Trim()))
                {
                    XtraMessageBox.Show("Financial Year Code is mandatory.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                if (currDr["PERIOD_NAME"] == DBNull.Value || string.IsNullOrEmpty(currDr["PERIOD_NAME"].ToString().Trim()))
                {
                    XtraMessageBox.Show("Financial Year Name is mandatory.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (currDr["START_DATE"] == DBNull.Value || string.IsNullOrEmpty(currDr["START_DATE"].ToString().Trim()))
                {
                    XtraMessageBox.Show("Start Date is mandatory.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                if (currDr["END_DATE"] == DBNull.Value || string.IsNullOrEmpty(currDr["END_DATE"].ToString().Trim()))
                {
                    XtraMessageBox.Show("End Date is mandatory.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                if ((Convert.ToDateTime(currDr["START_DATE"]).Day) != 1)
                {
                    XtraMessageBox.Show("Start Date must be the 1st day of the month.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                var lastDayofMonth = DateTime.DaysInMonth(Convert.ToDateTime(currDr["END_DATE"]).Year, Convert.ToDateTime(currDr["END_DATE"]).Month);
                if (Convert.ToDateTime(currDr["END_DATE"]).Day != lastDayofMonth)
                {
                    XtraMessageBox.Show("End Date must be the last day (" + (lastDayofMonth) + ") of the month.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                if (currDr.RowState == DataRowState.Added)
                {
                    int qChkCodeCNT = oAccountPeriod.CheckAccountingCode(Convert.ToDecimal(currDr["COMPANY_ID"]), currDr["PERIOD_CODE"].ToString());
                    if (qChkCodeCNT > 0)
                    {
                        XtraMessageBox.Show("Financial Year Code cannot be duplicate.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }

                if ((currDr["START_DATE"] != DBNull.Value || !string.IsNullOrEmpty(currDr["START_DATE"].ToString().Trim())) &&
                   (currDr["END_DATE"] != DBNull.Value || !string.IsNullOrEmpty(currDr["END_DATE"].ToString().Trim())) &&
                    Convert.ToDateTime(currDr["START_DATE"]) > Convert.ToDateTime(currDr["END_DATE"]))
                {
                    XtraMessageBox.Show("Start Date cannot be greater than End Date.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                if (currDr["PERIOD_TYPE"].ToString().Equals("M"))
                {
                    var drStartDtOverlapping = dtAccountPeriod.Select("COMPANY_ID = " + currDr["COMPANY_ID"] +
                                                        " AND START_DATE <= #" + Convert.ToDateTime(currDr["START_DATE"]).ToString(CultureInfo.InvariantCulture) +
                                                        "# AND END_DATE >= #" + Convert.ToDateTime(currDr["START_DATE"]).ToString(CultureInfo.InvariantCulture) +
                                                        "# AND PERIOD_CODE <> '" + currDr["PERIOD_CODE"].ToString() +
                                                        "' AND PERIOD_TYPE = 'M'");
                    if (drStartDtOverlapping.Length > 0)
                    {
                        XtraMessageBox.Show("Start Date is overlapping with an existing date.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    var drEndDtOverlapping = dtAccountPeriod.Select("COMPANY_ID = " + currDr["COMPANY_ID"] +
                                                        " AND START_DATE <= #" + Convert.ToDateTime(currDr["END_DATE"]).ToString(CultureInfo.InvariantCulture) +
                                                        "# AND END_DATE >= #" + Convert.ToDateTime(currDr["END_DATE"]).ToString(CultureInfo.InvariantCulture) +
                                                        "# AND PERIOD_CODE <> '" + currDr["PERIOD_CODE"].ToString() +
                                                        "' AND PERIOD_TYPE = 'M'");
                    if (drEndDtOverlapping.Length > 0)
                    {
                        XtraMessageBox.Show("End Date is overlapping with an existing date.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    var drBothOverlapping = dtAccountPeriod.Select("COMPANY_ID = " + currDr["COMPANY_ID"] +
                                                    " AND START_DATE >= #" + Convert.ToDateTime(currDr["START_DATE"]).ToString(CultureInfo.InvariantCulture) +
                                                    "# AND  END_DATE <= #" + Convert.ToDateTime(currDr["END_DATE"]).ToString(CultureInfo.InvariantCulture) +
                                                    "# AND PERIOD_CODE <> '" + currDr["PERIOD_CODE"].ToString() +
                                                    "' AND PERIOD_TYPE = 'M'");
                    if (drBothOverlapping.Length > 0)
                    {
                        XtraMessageBox.Show("Start and End Dates are overlapping with an existing date.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }

                //if (currDr.RowState == DataRowState.Added)
                //{
                //    vIns = true;

                //    //currDr["COMP_PERIOD_CODE"] = currDr["COMPANY_ID"].ToString() + " - " + currDr["PERIOD_CODE"].ToString();
                //    //currDr["COMP_MAJ_PERIOD_CODE"] = DBNull.Value;

                //    //if (currDr["PERIOD_TYPE"].ToString().Equals("M"))
                //    //{
                //    //    if (DateTime.Now <= Convert.ToDateTime(currDr["END_DATE"]))
                //    //    {
                //    //        currDr["CODA_SEC1"] = 1;
                //    //        currDr["CODA_ACCESS_LEVEL"] = 1;
                //    //    }
                //    //    else if (DateTime.Now > Convert.ToDateTime(currDr["END_DATE"]))
                //    //    {
                //    //        currDr["CODA_SEC1"] = 7;
                //    //        currDr["CODA_ACCESS_LEVEL"] = 7;
                //    //    }
                //    //}
                //}
                //else
                //{
                //    vIns = false;
                //}
            }

            if (currDr.RowState != DataRowState.Deleted)
            {
                if (currDr["PERIOD_TYPE"] != DBNull.Value)
                    vPeriodType = currDr["PERIOD_TYPE"].ToString();
            }
            return true;
        }

        private void SaveAccountingPeriod(DataRow currDr)
        {
            int i;
            DateTime lSTART_DATE;
            DateTime ldate_to;
            bool lproceed = true;
            string vMonth = string.Empty;
            dxBarBtnsave.Enabled = false;
            dxBarBtnCancel.Enabled = false;
            dxBarBtnAddNode.Enabled = true;
            dxBarBtnDelNode.Enabled = true;
            dxBarBtnExp.Enabled = true;
            dxBarBtnCollp.Enabled = true;

            cxDBTreeList1.EndCurrentEdit();
            cxDBTreeList1.BeginUpdate();

            try
            {
                if (vPeriodType.Equals("M"))
                {
                    if (vIns)
                    {
                        currDr["COMP_PERIOD_CODE"] = currDr["COMPANY_ID"].ToString() + " - " + currDr["PERIOD_CODE"].ToString();
                        currDr["COMP_MAJ_PERIOD_CODE"] = DBNull.Value;

                        if (currDr["PERIOD_TYPE"].ToString().Equals("M"))
                        {
                            if (DateTime.Now <= Convert.ToDateTime(currDr["END_DATE"]))
                            {
                                currDr["CODA_SEC1"] = 1;
                                currDr["CODA_ACCESS_LEVEL"] = 1;
                            }
                            else if (DateTime.Now > Convert.ToDateTime(currDr["END_DATE"]))
                            {
                                currDr["CODA_SEC1"] = 7;
                                currDr["CODA_ACCESS_LEVEL"] = 7;
                            }
                        }

                        lSTART_DATE = Convert.ToDateTime(currDr["START_DATE"]);

                        i = 1;
                        while (lproceed)
                        {
                            DateTime firstOfNextMonth = new DateTime(lSTART_DATE.Year, lSTART_DATE.Month, 1).AddMonths(1).AddDays(-1);
                            if (Convert.ToDateTime(currDr["END_DATE"]) <= firstOfNextMonth)
                            {
                                ldate_to = Convert.ToDateTime(currDr["END_DATE"]);
                                lproceed = false;
                            }
                            else
                            {
                                ldate_to = firstOfNextMonth;
                            }

                            DataRow dr = dtAccountPeriod.NewRow();
                            dr["COMPANY_ID"] = currDr["COMPANY_ID"];
                            dr["PERIOD_CODE"] = i.ToString() + '/' + currDr["PERIOD_CODE"];
                            vMonth = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(lSTART_DATE.Month);
                            dr["PERIOD_NAME"] = vMonth + ' ' + lSTART_DATE.Year.ToString();
                            dr["START_DATE"] = lSTART_DATE;
                            dr["END_DATE"] = ldate_to;
                            dr["MAJOR_PERIOD_CODE"] = currDr["PERIOD_CODE"];
                            dr["COMP_MAJ_PERIOD_CODE"] = dr["COMPANY_ID"].ToString() + " - " + dr["MAJOR_PERIOD_CODE"].ToString();
                            dr["COMP_PERIOD_CODE"] = dr["COMPANY_ID"].ToString() + " - " + dr["PERIOD_CODE"].ToString();
                            dr["PERIOD_TYPE"] = 'S';
                            dr["CREATED_BY"] = ProfitCashflow.oPcfDM.UserName;
                            dr["CREATED_ON"] = DateTime.Now;
                            dr["LAST_UPDATED"] = DateTime.Now;
                            dr["CURRENT_PERIOD"] = 'N';
                            if (DateTime.Now <= ldate_to)
                            {
                                dr["CODA_SEC1"] = 1;
                                dr["CODA_ACCESS_LEVEL"] = 1;
                            }
                            else if (DateTime.Now > ldate_to)
                            {
                                dr["CODA_SEC1"] = 6;
                                dr["CODA_ACCESS_LEVEL"] = 6;
                            }

                            dtAccountPeriod.Rows.Add(dr);
                            lSTART_DATE = ldate_to.AddDays(1);
                            i = i + 1;
                        }

                        if (dtAccountPeriod.GetChanges() != null)
                        {
                            oAccountPeriod.UpdateAccountingPeriod(dtAccountPeriod.GetChanges());
                            dtAccountPeriod.AcceptChanges();
                        }
                    }
                    else if (!(vIns))
                    /// Edit
                    {
                        if (vNewEndDate > vOldEndDate)
                        {
                            int qAccMthsCNT = oAccountPeriod.CheckAccountingMonths(Convert.ToDecimal(currDr["COMPANY_ID"]), currDr["PERIOD_CODE"].ToString());
                            lSTART_DATE = vOldEndDate.AddDays(1);
                            lproceed = true;
                            i = qAccMthsCNT + 1;

                            while (lproceed)
                            {
                                DateTime firstOfNextMonth = new DateTime(lSTART_DATE.Year, lSTART_DATE.Month, 1).AddMonths(1).AddDays(-1);
                                if (Convert.ToDateTime(currDr["END_DATE"]) <= firstOfNextMonth)
                                {
                                    ldate_to = Convert.ToDateTime(currDr["END_DATE"]);
                                    lproceed = false;
                                }
                                else
                                {
                                    ldate_to = firstOfNextMonth;
                                }

                                DataRow dr = dtAccountPeriod.NewRow();
                                dr["COMPANY_ID"] = currDr["COMPANY_ID"];
                                dr["PERIOD_CODE"] = i.ToString() + '/' + currDr["PERIOD_CODE"];
                                vMonth = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(lSTART_DATE.Month);
                                dr["PERIOD_NAME"] = vMonth + ' ' + lSTART_DATE.Year;
                                dr["START_DATE"] = lSTART_DATE;
                                dr["END_DATE"] = ldate_to;
                                dr["MAJOR_PERIOD_CODE"] = currDr["PERIOD_CODE"];
                                dr["COMP_MAJ_PERIOD_CODE"] = dr["COMPANY_ID"].ToString() + " - " + dr["MAJOR_PERIOD_CODE"].ToString();
                                dr["COMP_PERIOD_CODE"] = dr["COMPANY_ID"].ToString() + " - " + dr["PERIOD_CODE"].ToString();
                                dr["PERIOD_TYPE"] = 'S';
                                dr["CREATED_BY"] = ProfitCashflow.oPcfDM.UserName;
                                dr["CREATED_ON"] = DateTime.Now;
                                dr["LAST_UPDATED"] = DateTime.Now;
                                dr["CURRENT_PERIOD"] = 'N';
                                if (DateTime.Now <= ldate_to)
                                {
                                    dr["CODA_SEC1"] = 1;
                                    dr["CODA_ACCESS_LEVEL"] = 1;
                                }
                                else if (DateTime.Now > ldate_to)
                                {
                                    dr["CODA_SEC1"] = 6;
                                    dr["CODA_ACCESS_LEVEL"] = 6;
                                }
                                dtAccountPeriod.Rows.Add(dr);

                                lSTART_DATE = ldate_to.AddDays(1);
                                i = i + 1;
                            }

                            //oAccountPeriod.UpdateAccountingPeriod(dtAccountPeriod.GetChanges(DataRowState.Added));
                        }
                        if (vNewEndDate < vOldEndDate)
                        {
                            // Delete Sub nodes 
                            oAccountPeriod.CheckDelEndDateSubPeriod(currDr);
                        }

                        // Parent node's financial year name got modified
                        if (vNewName != null && vOldName != null && vNewName != vOldName)
                        {
                            if (dtAccountPeriod.GetChanges() != null)
                            {
                                oAccountPeriod.UpdateAccountingPeriod(dtAccountPeriod.GetChanges());
                                dtAccountPeriod.AcceptChanges();
                            }
                        }

                        // Enddate is updated, sub nodes will be modified automatically
                        if (vNewEndDate != vOldEndDate)
                        {
                            if (dtAccountPeriod.GetChanges() != null)
                                oAccountPeriod.UpdateAccountingPeriod(dtAccountPeriod.GetChanges());

                            //TODO : Remove nodes from treelist when accounting period is decreasing
                            // Deleting child nodes from treelist
                            var deleteRows = dtAccountPeriod.Select("COMPANY_ID = " + currDr["COMPANY_ID"] +
                                                " AND MAJOR_PERIOD_CODE = '" + currDr["PERIOD_CODE"] +
                                                "' AND PERIOD_TYPE = 'S' AND (START_DATE > #" + Convert.ToDateTime(currDr["START_DATE"]).ToString(CultureInfo.InvariantCulture) +
                                                "# AND END_DATE > #" + Convert.ToDateTime(currDr["END_DATE"]).ToString(CultureInfo.InvariantCulture) + "#)");
                            foreach (var item in deleteRows)
                                item.Delete();
                            dtAccountPeriod.AcceptChanges();
                        }

                        dxBarBtnsave.Enabled = false;
                        dxBarBtnCancel.Enabled = false;
                        dxBarBtnAddNode.Enabled = true;
                        dxBarBtnDelNode.Enabled = true;
                        dxBarBtnExp.Enabled = true;
                        dxBarBtnCollp.Enabled = true;
                    }
                }
                else
                {
                    if (vNewName != vOldName)
                    {
                        if (dtAccountPeriod.GetChanges() != null)
                        {
                            oAccountPeriod.UpdateAccountingPeriod(dtAccountPeriod.GetChanges());
                            dtAccountPeriod.AcceptChanges();
                            cxDBTreeList1.EndUpdate();

                            return;
                        }
                    }

                    //XtraMessageBox.Show("Cannot insert sub periods.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //return;
                }

                //dtAccountPeriod.AcceptChanges();
                cxDBTreeList1.EndUpdate();
                cxDBTreeList1.LayoutChanged();
            }
            catch (Exception ex)
            {
                cxDBTreeList1.EndUpdate();
                XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK);
                return;
            }

            bHasChanges = false;
            dxBarBtnsave.Enabled = false;
            dxBarBtnCancel.Enabled = false;
            cxDBTreeList1cxDBTreeListCOMPANY_ID.OptionsColumn.AllowEdit = false;
            cxDBTreeList1cxDBTreeListCODE.OptionsColumn.AllowEdit = false;
            cxDBTreeList1cxDBTreeListFROM.OptionsColumn.AllowEdit = false;
            drParent = null;
        }

        private void dxBarBtnAddNode_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                cxDBTreeList1.FilterConditions.Clear();
                cxDBTreeList1.ActiveFilterString = string.Empty;

                cxDBTreeList1.OptionsBehavior.ReadOnly = false;
                repLookUpEditCompany.ReadOnly = false;

                drParent = null;
                drParent = dtAccountPeriod.NewRow();
                if (ProfitCashflow.oPcfDM.company_id > 0)
                    drParent["COMPANY_ID"] = ProfitCashflow.oPcfDM.company_id;

                drParent["PERIOD_CODE"] = "";
                drParent["PERIOD_TYPE"] = "M";
                drParent["CURRENT_PERIOD"] = "N";
                drParent["CREATED_BY"] = ProfitCashflow.oPcfDM.UserName;
                drParent["CREATED_ON"] = DateTime.Now;
                drParent["LAST_UPDATED"] = DateTime.Now;
                drParent["CODA_SEC1"] = 1;

                //Insert empty row
                cxDBTreeList1.FocusedNode = cxDBTreeList1.AppendNode(drParent, null);
                cxDBTreeList1.FocusedColumn = cxDBTreeList1cxDBTreeListCOMPANY_ID;

                vIns = true;
                dxBarBtnsave.Enabled = true;
                dxBarBtnCancel.Enabled = true;

                dxBarBtnAddNode.Enabled = false;
                dxBarBtnDelNode.Enabled = false;
                dxBarBtnExp.Enabled = false;
                dxBarBtnCollp.Enabled = false;

                cxDBTreeList1cxDBTreeListCOMPANY_ID.OptionsColumn.AllowEdit = true;
                cxDBTreeList1cxDBTreeListCODE.OptionsColumn.AllowEdit = true;
                cxDBTreeList1cxDBTreeListFROM.OptionsColumn.AllowEdit = true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void dxBarBtnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            cxDBTreeList1.CloseEditor();
            cxDBTreeList1.EndCurrentEdit();

            var currDr = (DataRow)dtAccountPeriod.Rows[cxDBTreeList1.FocusedNode.Id];

            if (currDr == null) return;

            //if (currDr.RowState == DataRowState.Added)
            //{
            //    currDr["COMP_PERIOD_CODE"] = currDr["COMPANY_ID"].ToString() + " - " + currDr["PERIOD_CODE"].ToString();
            //    currDr["COMP_MAJ_PERIOD_CODE"] = DBNull.Value;
            //}

            this.Cursor = Cursors.WaitCursor;
            /*splashScreenManager1.ShowWaitForm();
            splashScreenManager1.SetWaitFormDescription("");*/

            if (currDr.RowState == DataRowState.Added || currDr.RowState == DataRowState.Modified)
            {
                try
                {
                    if (Validation(cxDBTreeList1.FocusedNode))
                    {
                        DevExpress.XtraTreeList.Nodes.TreeListNode tlNode = cxDBTreeList1.FocusedNode;
                        if (tlNode != null)
                        {
                            var currDr1 = ((DataRowView)cxDBTreeList1.GetDataRecordByNode(tlNode)).Row;
                            SaveAccountingPeriod(currDr1);
                        }
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //splashScreenManager1.CloseWaitForm();
                    //return;
                }
            }

            //splashScreenManager1.CloseWaitForm();
            this.Cursor = Cursors.Default;
        }

        private void dxBarBtnDelNodeClick_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (cxDBTreeList1.FocusedNode != null)
            {
                try
                {
                    DevExpress.XtraTreeList.Nodes.TreeListNode tlNode = cxDBTreeList1.FocusedNode;
                    if (tlNode != null)
                    {
                        var drCurr = ((DataRowView)cxDBTreeList1.GetDataRecordByNode(tlNode));

                        if (drCurr != null)
                        {
                            if (drCurr["PERIOD_CODE"] == DBNull.Value || string.IsNullOrEmpty(drCurr["PERIOD_CODE"].ToString()))
                            {
                                MessageBox.Show("No records exist for deletion.");
                                return;
                            }
                            if (drCurr["PERIOD_TYPE"].ToString().Equals("S"))
                            {
                                XtraMessageBox.Show("Cannot delete Sub Periods.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            int cnt = oAccountPeriod.CheckAccountingPeriod(Convert.ToDecimal(drCurr["COMPANY_ID"]), drCurr["PERIOD_CODE"].ToString());
                            if (cnt > 0)
                            {
                                XtraMessageBox.Show("Cannot delete, as the Financial Year Code is selected in Adjustment / Overhead / Interest.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            else
                            {
                                cxDBTreeList1.BeginUpdate();
                                DialogResult oTempResult = XtraMessageBox.Show("Do you want to delete ?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (oTempResult == DialogResult.Yes)
                                {
                                    this.Cursor = Cursors.WaitCursor;
                                    //splashScreenManager1.ShowWaitForm();
                                    //splashScreenManager1.SetWaitFormDescription("");
                                    try
                                    {
                                        // Deleting parent node from treelist
                                        drCurr.Delete();

                                        oAccountPeriod.UpdateAccountingPeriod(dtAccountPeriod.GetChanges(DataRowState.Deleted));

                                        // Deleting child nodes from treelist
                                        var deleteRows = dtAccountPeriod.Select("MAJOR_PERIOD_CODE = '" + drCurr["PERIOD_CODE"] + "' AND COMPANY_ID = " + drCurr["COMPANY_ID"]);
                                        foreach (var item in deleteRows)
                                            item.Delete();

                                        dtAccountPeriod.AcceptChanges();
                                    }
                                    catch (Exception ex)
                                    {
                                        cxDBTreeList1.EndUpdate();
                                        dtAccountPeriod.RejectChanges();
                                        XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK);
                                        //splashScreenManager1.CloseWaitForm();
                                        this.Cursor = Cursors.Default;
                                        return;
                                    }

                                    cxDBTreeList1.EndUpdate();

                                    if (dtAccountPeriod.Rows.Count == 0)
                                    {
                                        dxBarBtnsave.Enabled = false;
                                        dxBarBtnCancel.Enabled = false;
                                        dxBarBtnAddNode.Enabled = true;
                                        dxBarBtnDelNode.Enabled = false;
                                        dxBarBtnExp.Enabled = false;
                                        dxBarBtnCollp.Enabled = false;
                                    }
                                    else
                                    {
                                        dxBarBtnsave.Enabled = false;
                                        dxBarBtnCancel.Enabled = false;
                                        dxBarBtnAddNode.Enabled = true;
                                        dxBarBtnDelNode.Enabled = true;
                                        dxBarBtnExp.Enabled = true;
                                        dxBarBtnCollp.Enabled = true;
                                    }
                                }

                                //splashScreenManager1.CloseWaitForm();
                                this.Cursor = Cursors.Default;
                            }
                        }
                    }
                }

                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void dxBarBtnCancelClick_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //cxDBTreeList1.DataSource = null;

            //if (dtAccountPeriod != null)
            //{
            //    dtAccountPeriod.RejectChanges();
            //    cxDBTreeList1cxDBTreeListCOMPANY_ID.OptionsColumn.AllowEdit = false;
            //    cxDBTreeList1cxDBTreeListCODE.OptionsColumn.AllowEdit = false;
            //    cxDBTreeList1cxDBTreeListFROM.OptionsColumn.AllowEdit = false;
            //}

            //if (dtAccountPeriod.Rows.Count == 0)
            //{
            //    dxBarBtnsave.Enabled = false;
            //    dxBarBtnCancel.Enabled = false;
            //    dxBarBtnAddNode.Enabled = true;
            //    dxBarBtnDelNode.Enabled = false;
            //    dxBarBtnExp.Enabled = false;
            //    dxBarBtnCollp.Enabled = false;
            //}
            //else
            //{
            //    cxDBTreeList1.DataSource = dtAccountPeriod;
            //    dxBarBtnsave.Enabled = false;
            //    dxBarBtnCancel.Enabled = false;
            //    dxBarBtnAddNode.Enabled = true;
            //    dxBarBtnDelNode.Enabled = true;
            //    dxBarBtnExp.Enabled = true;
            //    dxBarBtnCollp.Enabled = true;
            //}

            //dxBarBtnsave.Enabled = false;
            //dxBarBtnCancel.Enabled = false;
            //cxDBTreeList1.FilterConditions.Clear();
            //cxDBTreeList1.ActiveFilterString = string.Empty;

            if (drParent != null)
            {
                DevExpress.XtraTreeList.Nodes.TreeListNode tlNode = cxDBTreeList1.FocusedNode;
                if (vIns)
                    cxDBTreeList1.DeleteNode(tlNode);

                dtAccountPeriod.RejectChanges();
                if (dtAccountPeriod != null)
                {
                    dtAccountPeriod.RejectChanges();

                    cxDBTreeList1cxDBTreeListCOMPANY_ID.OptionsColumn.AllowEdit = false;
                    cxDBTreeList1cxDBTreeListCODE.OptionsColumn.AllowEdit = false;
                    cxDBTreeList1cxDBTreeListFROM.OptionsColumn.AllowEdit = false;
                }

                if (dtAccountPeriod.Rows.Count == 0)
                {
                    dxBarBtnsave.Enabled = false;
                    dxBarBtnCancel.Enabled = false;
                    dxBarBtnAddNode.Enabled = true;
                    dxBarBtnDelNode.Enabled = false;
                    dxBarBtnExp.Enabled = false;
                    dxBarBtnCollp.Enabled = false;
                }
                else
                {
                    cxDBTreeList1.DataSource = dtAccountPeriod;
                    dxBarBtnsave.Enabled = false;
                    dxBarBtnCancel.Enabled = false;
                    dxBarBtnAddNode.Enabled = true;
                    dxBarBtnDelNode.Enabled = true;
                    dxBarBtnExp.Enabled = true;
                    dxBarBtnCollp.Enabled = true;
                }
                drParent = null;
            }
        }

        private void dxBarBtnExpClick_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (cxDBTreeList1.AllNodesCount > 0)
            {
                cxDBTreeList1.ForceInitialize();
                cxDBTreeList1.ExpandAll();
            }
        }

        private void dxBarBtnCollpClick_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            cxDBTreeList1.CollapseAll();
        }

        private void cxDBTreeList1_InvalidNodeException(object sender, DevExpress.XtraTreeList.InvalidNodeExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void cxDBTreeList1_ValidateNode(object sender, DevExpress.XtraTreeList.ValidateNodeEventArgs e)
        {
            cxDBTreeList1.EndCurrentEdit();
            e.Valid = Validation(e.Node);

            if (e.Valid)
            {
                //DevExpress.XtraTreeList.Nodes.TreeListNode tlNode = e.Node;
                DevExpress.XtraTreeList.Nodes.TreeListNode tlNode = cxDBTreeList1.FocusedNode;

                if (tlNode != null && tlNode.Id > 0)
                {
                    var currDr1 = ((DataRowView)cxDBTreeList1.GetDataRecordByNode(tlNode)).Row;
                    if (currDr1.RowState == DataRowState.Added)
                    {
                        currDr1["COMP_PERIOD_CODE"] = currDr1["COMPANY_ID"].ToString() + " - " + currDr1["PERIOD_CODE"].ToString();
                        currDr1["COMP_MAJ_PERIOD_CODE"] = DBNull.Value;
                    }

                    this.Cursor = Cursors.WaitCursor;
                    //splashScreenManager1.ShowWaitForm();
                    SaveAccountingPeriod(currDr1);
                    //splashScreenManager1.CloseWaitForm();
                    this.Cursor = Cursors.Default;
                }
            }
        }

        private void cxDBTreeList1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            try
            {
                if (e.Node == cxDBTreeList1.Nodes.AutoFilterNode)
                    return;

                DevExpress.XtraTreeList.Nodes.TreeListNode tlNode = e.Node;
                if (tlNode != null)
                {
                    var currDr = ((DataRowView)cxDBTreeList1.GetDataRecordByNode(tlNode)).Row;
                    if (currDr["PERIOD_TYPE"].ToString().Equals("S"))
                    {
                        cxDBTreeList1cxDBTreeListTO.OptionsColumn.AllowEdit = false;
                    }
                    else
                    {
                        drParent = ((DataRowView)cxDBTreeList1.GetDataRecordByNode(tlNode)).Row;
                        cxDBTreeList1cxDBTreeListTO.OptionsColumn.AllowEdit = true;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void cxDBTreeList1_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            if (e.Node == cxDBTreeList1.Nodes.AutoFilterNode)
                return;

            if (e.Column == cxDBTreeList1cxDBTreeListNAME)
            {
                DevExpress.XtraTreeList.Nodes.TreeListNode tlNode = e.Node;
                if (tlNode != null && e.Node.Id > 0)
                {
                    DataRow drCurr = ((DataRowView)cxDBTreeList1.GetDataRecordByNode(tlNode)).Row;
                    if (drCurr != null)
                    {
                        if (drCurr.RowState == DataRowState.Modified || drCurr.RowState == DataRowState.Unchanged)
                        {
                            vOldName = drCurr["PERIOD_NAME", DataRowVersion.Original].ToString();
                            vNewName = e.Value.ToString();
                        }
                    }
                }
            }
            if (e.Column == cxDBTreeList1cxDBTreeListTO)
            {
                DevExpress.XtraTreeList.Nodes.TreeListNode tlNode = e.Node;
                if (tlNode != null && e.Node.Id > 0)
                {
                    DataRow drCurr = ((DataRowView)cxDBTreeList1.GetDataRecordByNode(tlNode)).Row;
                    if (drCurr != null)
                    {
                        if (drCurr.RowState == DataRowState.Modified || drCurr.RowState == DataRowState.Unchanged)
                        {
                            vOldEndDate = Convert.ToDateTime(drCurr["END_DATE", DataRowVersion.Original]);
                            vNewEndDate = Convert.ToDateTime(e.Value);
                        }
                    }
                }
            }
        }

        private void cxDBTreeList1_CellValueChanging(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            if (e.Node == cxDBTreeList1.Nodes.AutoFilterNode)
                return;

            bHasChanges = true;
            if (e.Column == cxDBTreeList1cxDBTreeListNAME || e.Column == cxDBTreeList1cxDBTreeListTO)
            {
                dxBarBtnsave.Enabled = true;
                dxBarBtnCancel.Enabled = true;
                dxBarBtnAddNode.Enabled = false;
                dxBarBtnDelNode.Enabled = false;
                dxBarBtnExp.Enabled = false;
                dxBarBtnCollp.Enabled = false;
            }
        }

        private void cxDBTreeList1_MouseUp(object sender, MouseEventArgs e)
        {
            if (cxDBTreeList1.FocusedNode == cxDBTreeList1.Nodes.AutoFilterNode)
                return;

            if (e.Button == MouseButtons.Right)
            {
                popupMenu1.ShowPopup(Control.MousePosition);
            }
        }

        private void cxDBTreeList1_BeforeFocusNode(object sender, DevExpress.XtraTreeList.BeforeFocusNodeEventArgs e)
        {
            if (e.Node == e.OldNode || e.OldNode == null)
                return;

            //if (e.Node == cxDBTreeList1.Nodes.AutoFilterNode)
            //    return;

            if (e.OldNode != null)
            {
                //if (!Validation(e.OldNode))
                //    e.CanFocus = false;

                if (!Validation(e.OldNode))
                    e.CanFocus = false;
            }
        }

        private void cxDBTreeList1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (drParent != null)
                {
                    DevExpress.XtraTreeList.Nodes.TreeListNode tlNode = cxDBTreeList1.FocusedNode;
                    if (vIns)
                        cxDBTreeList1.DeleteNode(tlNode);

                    dtAccountPeriod.RejectChanges();
                    if (dtAccountPeriod != null)
                    {
                        dtAccountPeriod.RejectChanges();

                        cxDBTreeList1cxDBTreeListCOMPANY_ID.OptionsColumn.AllowEdit = false;
                        cxDBTreeList1cxDBTreeListCODE.OptionsColumn.AllowEdit = false;
                        cxDBTreeList1cxDBTreeListFROM.OptionsColumn.AllowEdit = false;
                    }

                    if (dtAccountPeriod.Rows.Count == 0)
                    {
                        dxBarBtnsave.Enabled = false;
                        dxBarBtnCancel.Enabled = false;
                        dxBarBtnAddNode.Enabled = true;
                        dxBarBtnDelNode.Enabled = false;
                        dxBarBtnExp.Enabled = false;
                        dxBarBtnCollp.Enabled = false;
                    }
                    else
                    {
                        cxDBTreeList1.DataSource = dtAccountPeriod;
                        dxBarBtnsave.Enabled = false;
                        dxBarBtnCancel.Enabled = false;
                        dxBarBtnAddNode.Enabled = true;
                        dxBarBtnDelNode.Enabled = true;
                        dxBarBtnExp.Enabled = true;
                        dxBarBtnCollp.Enabled = true;
                    }
                    drParent = null;
                }
            }
            else if (e.KeyCode == Keys.Insert)
                vIns = true;
        }

        private void fAccPeriod_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (bHasChanges)
            {
                DialogResult oTempResult = XtraMessageBox.Show("Data has been changed, do you want to save the changes?", Application.ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (oTempResult == DialogResult.Yes)
                {
                    if (Validation(cxDBTreeList1.FocusedNode))
                    {
                        DevExpress.XtraTreeList.Nodes.TreeListNode tlNode = cxDBTreeList1.FocusedNode;
                        if (tlNode != null && tlNode.Id > 0)
                        {
                            var currDr1 = ((DataRowView)cxDBTreeList1.GetDataRecordByNode(tlNode)).Row;
                            if (currDr1.RowState == DataRowState.Added)
                            {
                                currDr1["COMP_PERIOD_CODE"] = currDr1["COMPANY_ID"].ToString() + " - " + currDr1["PERIOD_CODE"].ToString();
                                currDr1["COMP_MAJ_PERIOD_CODE"] = DBNull.Value;
                            }

                            this.Cursor = Cursors.WaitCursor;
                            //splashScreenManager1.ShowWaitForm();
                            SaveAccountingPeriod(currDr1);
                            //splashScreenManager1.CloseWaitForm();
                            this.Cursor = Cursors.Default;
                            e.Cancel = false;
                        }
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
                else if (oTempResult == DialogResult.No)
                {
                    cxDBTreeList1.CancelUpdate();
                    e.Cancel = false;
                    //dtOpCompMast.RejectChanges();
                }
                else
                    e.Cancel = true;
            }

            if (e.Cancel) return;
        }

        private void fAccPeriod_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                bHasChanges = false;
                cxDBTreeList1_KeyDown(sender, e);
            }
        }
    }
}