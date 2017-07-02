namespace Xpedeon.ProfitCashflow
{
    partial class fUnitCategoryTypeMaster
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fUnitCategoryTypeMaster));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.cxGrid1 = new DevExpress.XtraGrid.GridControl();
            this.cxGrid1DBTableView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.cxGrid1DBTableView1PLOT_TYPE_ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repItemLookUpEditPlot = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.cxGrid1DBTableView1RESIDENCE_CATEGORY_ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.cxGrid1DBTableView1DESCRIPTION = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cxGrid1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cxGrid1DBTableView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repItemLookUpEditPlot)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
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
            this.layoutControl1.Size = new System.Drawing.Size(416, 344);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // cxGrid1
            // 
            this.cxGrid1.Location = new System.Drawing.Point(0, 0);
            this.cxGrid1.MainView = this.cxGrid1DBTableView1;
            this.cxGrid1.Name = "cxGrid1";
            this.cxGrid1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repItemLookUpEditPlot,
            this.repositoryItemTextEdit1});
            this.cxGrid1.Size = new System.Drawing.Size(416, 344);
            this.cxGrid1.TabIndex = 6;
            this.cxGrid1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.cxGrid1DBTableView1});
            // 
            // cxGrid1DBTableView1
            // 
            this.cxGrid1DBTableView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.cxGrid1DBTableView1PLOT_TYPE_ID,
            this.cxGrid1DBTableView1RESIDENCE_CATEGORY_ID,
            this.cxGrid1DBTableView1DESCRIPTION});
            this.cxGrid1DBTableView1.GridControl = this.cxGrid1;
            this.cxGrid1DBTableView1.Name = "cxGrid1DBTableView1";
            this.cxGrid1DBTableView1.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
            this.cxGrid1DBTableView1.OptionsCustomization.AllowColumnMoving = false;
            this.cxGrid1DBTableView1.OptionsCustomization.AllowQuickHideColumns = false;
            this.cxGrid1DBTableView1.OptionsNavigation.EnterMoveNextColumn = true;
            this.cxGrid1DBTableView1.OptionsView.EnableAppearanceEvenRow = true;
            this.cxGrid1DBTableView1.OptionsView.EnableAppearanceOddRow = true;
            this.cxGrid1DBTableView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            this.cxGrid1DBTableView1.OptionsView.RowAutoHeight = true;
            this.cxGrid1DBTableView1.OptionsView.ShowAutoFilterRow = true;
            this.cxGrid1DBTableView1.OptionsView.ShowGroupPanel = false;
            this.cxGrid1DBTableView1.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.cxGrid1DBTableView1PLOT_TYPE_ID, DevExpress.Data.ColumnSortOrder.Ascending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.cxGrid1DBTableView1DESCRIPTION, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.cxGrid1DBTableView1.DragObjectOver += new DevExpress.XtraGrid.Views.Base.DragObjectOverEventHandler(this.cxGrid1DBTableView1_DragObjectOver);
            this.cxGrid1DBTableView1.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.cxGrid1DBTableView1_InitNewRow);
            this.cxGrid1DBTableView1.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.cxGrid1DBTableView1_CellValueChanging);
            this.cxGrid1DBTableView1.InvalidRowException += new DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventHandler(this.cxGrid1DBTableView1_InvalidRowException);
            this.cxGrid1DBTableView1.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(this.cxGrid1DBTableView1_ValidateRow);
            this.cxGrid1DBTableView1.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.cxGrid1DBTableView1_RowUpdated);
            this.cxGrid1DBTableView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cxGrid1DBTableView1_KeyDown);
            // 
            // cxGrid1DBTableView1PLOT_TYPE_ID
            // 
            this.cxGrid1DBTableView1PLOT_TYPE_ID.Caption = "Plot Type";
            this.cxGrid1DBTableView1PLOT_TYPE_ID.ColumnEdit = this.repItemLookUpEditPlot;
            this.cxGrid1DBTableView1PLOT_TYPE_ID.FieldName = "PLOT_TYPE_ID";
            this.cxGrid1DBTableView1PLOT_TYPE_ID.Name = "cxGrid1DBTableView1PLOT_TYPE_ID";
            this.cxGrid1DBTableView1PLOT_TYPE_ID.Visible = true;
            this.cxGrid1DBTableView1PLOT_TYPE_ID.VisibleIndex = 0;
            // 
            // repItemLookUpEditPlot
            // 
            this.repItemLookUpEditPlot.AutoHeight = false;
            this.repItemLookUpEditPlot.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repItemLookUpEditPlot.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("PLOT_TYPE_ID", "Plot Type Id", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("PLOT_TYPE", "Plot Type")});
            this.repItemLookUpEditPlot.DisplayMember = "PLOT_TYPE";
            this.repItemLookUpEditPlot.Name = "repItemLookUpEditPlot";
            this.repItemLookUpEditPlot.NullText = "";
            this.repItemLookUpEditPlot.ValueMember = "PLOT_TYPE_ID";
            // 
            // cxGrid1DBTableView1RESIDENCE_CATEGORY_ID
            // 
            this.cxGrid1DBTableView1RESIDENCE_CATEGORY_ID.Caption = "Category Code";
            this.cxGrid1DBTableView1RESIDENCE_CATEGORY_ID.ColumnEdit = this.repositoryItemTextEdit1;
            this.cxGrid1DBTableView1RESIDENCE_CATEGORY_ID.FieldName = "RESIDENCE_CATEGORY_ID";
            this.cxGrid1DBTableView1RESIDENCE_CATEGORY_ID.Name = "cxGrid1DBTableView1RESIDENCE_CATEGORY_ID";
            this.cxGrid1DBTableView1RESIDENCE_CATEGORY_ID.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.cxGrid1DBTableView1RESIDENCE_CATEGORY_ID.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.cxGrid1DBTableView1RESIDENCE_CATEGORY_ID.Visible = true;
            this.cxGrid1DBTableView1RESIDENCE_CATEGORY_ID.VisibleIndex = 1;
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Mask.EditMask = "d";
            this.repositoryItemTextEdit1.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // cxGrid1DBTableView1DESCRIPTION
            // 
            this.cxGrid1DBTableView1DESCRIPTION.Caption = "Unit Category";
            this.cxGrid1DBTableView1DESCRIPTION.FieldName = "DESCRIPTION";
            this.cxGrid1DBTableView1DESCRIPTION.Name = "cxGrid1DBTableView1DESCRIPTION";
            this.cxGrid1DBTableView1DESCRIPTION.Visible = true;
            this.cxGrid1DBTableView1DESCRIPTION.VisibleIndex = 2;
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
            this.layoutControlGroup1.Size = new System.Drawing.Size(416, 344);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.cxGrid1;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem3.Size = new System.Drawing.Size(416, 344);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // fUnitCategoryTypeMaster
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.ClientSize = new System.Drawing.Size(416, 344);
            this.Controls.Add(this.layoutControl1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "fUnitCategoryTypeMaster";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Unit Category Master";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.fMaster_FormClosing);
            this.Load += new System.EventHandler(this.fMaster_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.fUnitCategoryTypeMaster_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cxGrid1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cxGrid1DBTableView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repItemLookUpEditPlot)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
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
        private DevExpress.XtraGrid.Columns.GridColumn cxGrid1DBTableView1RESIDENCE_CATEGORY_ID;
        private DevExpress.XtraGrid.Columns.GridColumn cxGrid1DBTableView1PLOT_TYPE_ID;
        private DevExpress.XtraGrid.Columns.GridColumn cxGrid1DBTableView1DESCRIPTION;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repItemLookUpEditPlot;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;


    }
}