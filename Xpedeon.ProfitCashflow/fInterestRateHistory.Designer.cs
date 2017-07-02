namespace Xpedeon.ProfitCashflow
{
    partial class fInterestRateHistory
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fInterestRateHistory));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.cxGrid1 = new DevExpress.XtraGrid.GridControl();
            this.cxGrid1DBTableView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.cxGrid1DBTableView1PREV_INTEREST_RATE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cxGrid1DBTableView1PREV_INT_EFFECTIVE_DATE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cxGrid1DBTableView1REMARKS = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cxGrid1DBTableView1BY = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cxGrid1DBTableView1ON = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repTextEditIntRate = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cxGrid1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cxGrid1DBTableView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repTextEditIntRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.cxGrid1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(661, 390);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // cxGrid1
            // 
            this.cxGrid1.Location = new System.Drawing.Point(0, 0);
            this.cxGrid1.MainView = this.cxGrid1DBTableView1;
            this.cxGrid1.Name = "cxGrid1";
            this.cxGrid1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repTextEditIntRate});
            this.cxGrid1.Size = new System.Drawing.Size(661, 390);
            this.cxGrid1.TabIndex = 6;
            this.cxGrid1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.cxGrid1DBTableView1});
            // 
            // cxGrid1DBTableView1
            // 
            this.cxGrid1DBTableView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.cxGrid1DBTableView1PREV_INTEREST_RATE,
            this.cxGrid1DBTableView1PREV_INT_EFFECTIVE_DATE,
            this.cxGrid1DBTableView1REMARKS,
            this.cxGrid1DBTableView1BY,
            this.cxGrid1DBTableView1ON});
            this.cxGrid1DBTableView1.GridControl = this.cxGrid1;
            this.cxGrid1DBTableView1.Name = "cxGrid1DBTableView1";
            this.cxGrid1DBTableView1.OptionsBehavior.Editable = false;
            this.cxGrid1DBTableView1.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
            this.cxGrid1DBTableView1.OptionsCustomization.AllowColumnMoving = false;
            this.cxGrid1DBTableView1.OptionsCustomization.AllowQuickHideColumns = false;
            this.cxGrid1DBTableView1.OptionsNavigation.EnterMoveNextColumn = true;
            this.cxGrid1DBTableView1.OptionsView.EnableAppearanceEvenRow = true;
            this.cxGrid1DBTableView1.OptionsView.EnableAppearanceOddRow = true;
            this.cxGrid1DBTableView1.OptionsView.RowAutoHeight = true;
            this.cxGrid1DBTableView1.OptionsView.ShowAutoFilterRow = true;
            this.cxGrid1DBTableView1.OptionsView.ShowGroupPanel = false;
            this.cxGrid1DBTableView1.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.cxGrid1DBTableView1ON, DevExpress.Data.ColumnSortOrder.Descending)});
            this.cxGrid1DBTableView1.CustomRowCellEdit += new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(this.cxGrid1DBTableView1_CustomRowCellEdit);
            this.cxGrid1DBTableView1.InvalidRowException += new DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventHandler(this.cxGrid1DBTableView1_InvalidRowException);
            this.cxGrid1DBTableView1.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(this.cxGrid1DBTableView1_CustomColumnDisplayText);
            this.cxGrid1DBTableView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cxGrid1DBTableView1_KeyDown);
            // 
            // cxGrid1DBTableView1PREV_INTEREST_RATE
            // 
            this.cxGrid1DBTableView1PREV_INTEREST_RATE.Caption = "Previous Interest Rate";
            this.cxGrid1DBTableView1PREV_INTEREST_RATE.FieldName = "PREV_INTEREST_RATE";
            this.cxGrid1DBTableView1PREV_INTEREST_RATE.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText;
            this.cxGrid1DBTableView1PREV_INTEREST_RATE.Name = "cxGrid1DBTableView1PREV_INTEREST_RATE";
            this.cxGrid1DBTableView1PREV_INTEREST_RATE.OptionsColumn.AllowMove = false;
            this.cxGrid1DBTableView1PREV_INTEREST_RATE.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.cxGrid1DBTableView1PREV_INTEREST_RATE.OptionsColumn.ShowInCustomizationForm = false;
            this.cxGrid1DBTableView1PREV_INTEREST_RATE.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.cxGrid1DBTableView1PREV_INTEREST_RATE.Visible = true;
            this.cxGrid1DBTableView1PREV_INTEREST_RATE.VisibleIndex = 0;
            this.cxGrid1DBTableView1PREV_INTEREST_RATE.Width = 112;
            // 
            // cxGrid1DBTableView1PREV_INT_EFFECTIVE_DATE
            // 
            this.cxGrid1DBTableView1PREV_INT_EFFECTIVE_DATE.Caption = "Previous Effective Date";
            this.cxGrid1DBTableView1PREV_INT_EFFECTIVE_DATE.FieldName = "PREV_INT_EFFECTIVE_DATE";
            this.cxGrid1DBTableView1PREV_INT_EFFECTIVE_DATE.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText;
            this.cxGrid1DBTableView1PREV_INT_EFFECTIVE_DATE.Name = "cxGrid1DBTableView1PREV_INT_EFFECTIVE_DATE";
            this.cxGrid1DBTableView1PREV_INT_EFFECTIVE_DATE.OptionsColumn.AllowMove = false;
            this.cxGrid1DBTableView1PREV_INT_EFFECTIVE_DATE.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.cxGrid1DBTableView1PREV_INT_EFFECTIVE_DATE.OptionsColumn.ShowInCustomizationForm = false;
            this.cxGrid1DBTableView1PREV_INT_EFFECTIVE_DATE.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.cxGrid1DBTableView1PREV_INT_EFFECTIVE_DATE.Visible = true;
            this.cxGrid1DBTableView1PREV_INT_EFFECTIVE_DATE.VisibleIndex = 1;
            this.cxGrid1DBTableView1PREV_INT_EFFECTIVE_DATE.Width = 112;
            // 
            // cxGrid1DBTableView1REMARKS
            // 
            this.cxGrid1DBTableView1REMARKS.Caption = "Remarks";
            this.cxGrid1DBTableView1REMARKS.FieldName = "REMARKS";
            this.cxGrid1DBTableView1REMARKS.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText;
            this.cxGrid1DBTableView1REMARKS.Name = "cxGrid1DBTableView1REMARKS";
            this.cxGrid1DBTableView1REMARKS.OptionsColumn.AllowMove = false;
            this.cxGrid1DBTableView1REMARKS.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.cxGrid1DBTableView1REMARKS.OptionsColumn.ShowInCustomizationForm = false;
            this.cxGrid1DBTableView1REMARKS.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.cxGrid1DBTableView1REMARKS.Visible = true;
            this.cxGrid1DBTableView1REMARKS.VisibleIndex = 2;
            this.cxGrid1DBTableView1REMARKS.Width = 207;
            // 
            // cxGrid1DBTableView1BY
            // 
            this.cxGrid1DBTableView1BY.Caption = "Updated By";
            this.cxGrid1DBTableView1BY.FieldName = "CREATED_BY";
            this.cxGrid1DBTableView1BY.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText;
            this.cxGrid1DBTableView1BY.Name = "cxGrid1DBTableView1BY";
            this.cxGrid1DBTableView1BY.OptionsColumn.AllowMove = false;
            this.cxGrid1DBTableView1BY.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.cxGrid1DBTableView1BY.OptionsColumn.ShowInCustomizationForm = false;
            this.cxGrid1DBTableView1BY.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.cxGrid1DBTableView1BY.Visible = true;
            this.cxGrid1DBTableView1BY.VisibleIndex = 3;
            this.cxGrid1DBTableView1BY.Width = 69;
            // 
            // cxGrid1DBTableView1ON
            // 
            this.cxGrid1DBTableView1ON.Caption = "Updated On";
            this.cxGrid1DBTableView1ON.FieldName = "CREATED_ON";
            this.cxGrid1DBTableView1ON.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText;
            this.cxGrid1DBTableView1ON.Name = "cxGrid1DBTableView1ON";
            this.cxGrid1DBTableView1ON.OptionsColumn.AllowMove = false;
            this.cxGrid1DBTableView1ON.OptionsColumn.ShowInCustomizationForm = false;
            this.cxGrid1DBTableView1ON.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.cxGrid1DBTableView1ON.Visible = true;
            this.cxGrid1DBTableView1ON.VisibleIndex = 4;
            // 
            // repTextEditIntRate
            // 
            this.repTextEditIntRate.AutoHeight = false;
            this.repTextEditIntRate.Mask.EditMask = "F";
            this.repTextEditIntRate.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.repTextEditIntRate.Mask.UseMaskAsDisplayFormat = true;
            this.repTextEditIntRate.Name = "repTextEditIntRate";
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(661, 390);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.cxGrid1;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem3.Size = new System.Drawing.Size(661, 390);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // fInterestRateHistory
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(661, 390);
            this.Controls.Add(this.layoutControl1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "fInterestRateHistory";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Interest Rate History";
            this.Load += new System.EventHandler(this.fInterestRateHistory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cxGrid1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cxGrid1DBTableView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repTextEditIntRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraGrid.GridControl cxGrid1;
        private DevExpress.XtraGrid.Views.Grid.GridView cxGrid1DBTableView1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraGrid.Columns.GridColumn cxGrid1DBTableView1PREV_INTEREST_RATE;
        private DevExpress.XtraGrid.Columns.GridColumn cxGrid1DBTableView1PREV_INT_EFFECTIVE_DATE;
        private DevExpress.XtraGrid.Columns.GridColumn cxGrid1DBTableView1REMARKS;
        private DevExpress.XtraGrid.Columns.GridColumn cxGrid1DBTableView1BY;
        private DevExpress.XtraGrid.Columns.GridColumn cxGrid1DBTableView1ON;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repTextEditIntRate;


    }
}