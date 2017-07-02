namespace Xpedeon.ProfitCashflow
{
    partial class fPurchaseTypeMaster
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fPurchaseTypeMaster));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.cxGrid1 = new DevExpress.XtraGrid.GridControl();
            this.cxGrid1DBTableView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.cxGrid1DBTableView1DESCRIPTION = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cxGrid1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cxGrid1DBTableView1)).BeginInit();
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
            this.cxGrid1.Size = new System.Drawing.Size(416, 344);
            this.cxGrid1.TabIndex = 6;
            this.cxGrid1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.cxGrid1DBTableView1});
            // 
            // cxGrid1DBTableView1
            // 
            this.cxGrid1DBTableView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
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
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.cxGrid1DBTableView1DESCRIPTION, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.cxGrid1DBTableView1.DragObjectOver += new DevExpress.XtraGrid.Views.Base.DragObjectOverEventHandler(this.cxGrid1DBTableView1_DragObjectOver);
            this.cxGrid1DBTableView1.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.cxGrid1DBTableView1_InitNewRow);
            this.cxGrid1DBTableView1.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.cxGrid1DBTableView1_CellValueChanging);
            this.cxGrid1DBTableView1.InvalidRowException += new DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventHandler(this.cxGrid1DBTableView1_InvalidRowException);
            this.cxGrid1DBTableView1.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(this.cxGrid1DBTableView1_ValidateRow);
            this.cxGrid1DBTableView1.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.cxGrid1DBTableView1_RowUpdated);
            this.cxGrid1DBTableView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cxGrid1DBTableView1_KeyDown);
            // 
            // cxGrid1DBTableView1DESCRIPTION
            // 
            this.cxGrid1DBTableView1DESCRIPTION.Caption = "Purchase Type";
            this.cxGrid1DBTableView1DESCRIPTION.FieldName = "DESCRIPTION";
            this.cxGrid1DBTableView1DESCRIPTION.Name = "cxGrid1DBTableView1DESCRIPTION";
            this.cxGrid1DBTableView1DESCRIPTION.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.cxGrid1DBTableView1DESCRIPTION.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.cxGrid1DBTableView1DESCRIPTION.Visible = true;
            this.cxGrid1DBTableView1DESCRIPTION.VisibleIndex = 0;
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
            // fPurchaseTypeMaster
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
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fPurchaseTypeMaster";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Purchase Type Master";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.fLocationMaster_FormClosing);
            this.Load += new System.EventHandler(this.fLocationMaster_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.fPurchaseTypeMaster_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cxGrid1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cxGrid1DBTableView1)).EndInit();
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
        private DevExpress.XtraGrid.Columns.GridColumn cxGrid1DBTableView1DESCRIPTION;


    }
}