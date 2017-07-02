namespace Xpedeon.ProfitCashflow
{
    partial class fBalconyTerrMaster
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fBalconyTerrMaster));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.cxGrid1 = new DevExpress.XtraGrid.GridControl();
            this.cxGrid1DBTableView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.cxGrid1DBTableView1COMPANY_ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cxGrid1DBTableView1ASPECT_ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cxGrid1DBTableView1BALCONY_TERRACE_NAME = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cxGrid1DBTableView1CREATED_BY = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cxGrid1DBTableView1CREATED_ON = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cxGrid1DBTableView1LAST_UPDATED = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cxImageComboBoxShowCompanies = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.cxLookupComboBoxCompany = new DevExpress.XtraEditors.LookUpEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cxGrid1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cxGrid1DBTableView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cxImageComboBoxShowCompanies.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cxLookupComboBoxCompany.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.cxGrid1);
            this.layoutControl1.Controls.Add(this.cxImageComboBoxShowCompanies);
            this.layoutControl1.Controls.Add(this.cxLookupComboBoxCompany);
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
            this.cxGrid1.Location = new System.Drawing.Point(0, 24);
            this.cxGrid1.MainView = this.cxGrid1DBTableView1;
            this.cxGrid1.Name = "cxGrid1";
            this.cxGrid1.Size = new System.Drawing.Size(416, 320);
            this.cxGrid1.TabIndex = 6;
            this.cxGrid1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.cxGrid1DBTableView1});
            this.cxGrid1.ProcessGridKey += new System.Windows.Forms.KeyEventHandler(this.cxGrid1_ProcessGridKey);
            // 
            // cxGrid1DBTableView1
            // 
            this.cxGrid1DBTableView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.cxGrid1DBTableView1COMPANY_ID,
            this.cxGrid1DBTableView1ASPECT_ID,
            this.cxGrid1DBTableView1BALCONY_TERRACE_NAME,
            this.cxGrid1DBTableView1CREATED_BY,
            this.cxGrid1DBTableView1CREATED_ON,
            this.cxGrid1DBTableView1LAST_UPDATED});
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
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.cxGrid1DBTableView1BALCONY_TERRACE_NAME, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.cxGrid1DBTableView1.DragObjectOver += new DevExpress.XtraGrid.Views.Base.DragObjectOverEventHandler(this.cxGrid1DBTableView1_DragObjectOver);
            this.cxGrid1DBTableView1.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.cxGrid1DBTableView1_InitNewRow);
            this.cxGrid1DBTableView1.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.cxGrid1DBTableView1_FocusedRowChanged);
            this.cxGrid1DBTableView1.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.cxGrid1DBTableView1_CellValueChanging);
            this.cxGrid1DBTableView1.InvalidRowException += new DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventHandler(this.cxGrid1DBTableView1_InvalidRowException);
            this.cxGrid1DBTableView1.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(this.cxGrid1DBTableView1_ValidateRow);
            this.cxGrid1DBTableView1.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.cxGrid1DBTableView1_RowUpdated);
            this.cxGrid1DBTableView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cxGrid1DBTableView1_KeyDown);
            // 
            // cxGrid1DBTableView1COMPANY_ID
            // 
            this.cxGrid1DBTableView1COMPANY_ID.Caption = "COMPANY_ID";
            this.cxGrid1DBTableView1COMPANY_ID.Name = "cxGrid1DBTableView1COMPANY_ID";
            // 
            // cxGrid1DBTableView1ASPECT_ID
            // 
            this.cxGrid1DBTableView1ASPECT_ID.Caption = "ASPECT_ID";
            this.cxGrid1DBTableView1ASPECT_ID.Name = "cxGrid1DBTableView1ASPECT_ID";
            // 
            // cxGrid1DBTableView1BALCONY_TERRACE_NAME
            // 
            this.cxGrid1DBTableView1BALCONY_TERRACE_NAME.Caption = "Balcony / Terrace";
            this.cxGrid1DBTableView1BALCONY_TERRACE_NAME.FieldName = "BALCONY_TERRACE_NAME";
            this.cxGrid1DBTableView1BALCONY_TERRACE_NAME.Name = "cxGrid1DBTableView1BALCONY_TERRACE_NAME";
            this.cxGrid1DBTableView1BALCONY_TERRACE_NAME.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.cxGrid1DBTableView1BALCONY_TERRACE_NAME.Visible = true;
            this.cxGrid1DBTableView1BALCONY_TERRACE_NAME.VisibleIndex = 0;
            // 
            // cxGrid1DBTableView1CREATED_BY
            // 
            this.cxGrid1DBTableView1CREATED_BY.Caption = "CREATED_BY";
            this.cxGrid1DBTableView1CREATED_BY.Name = "cxGrid1DBTableView1CREATED_BY";
            // 
            // cxGrid1DBTableView1CREATED_ON
            // 
            this.cxGrid1DBTableView1CREATED_ON.Caption = "CREATED_ON";
            this.cxGrid1DBTableView1CREATED_ON.Name = "cxGrid1DBTableView1CREATED_ON";
            // 
            // cxGrid1DBTableView1LAST_UPDATED
            // 
            this.cxGrid1DBTableView1LAST_UPDATED.Caption = "LAST_UPDATED";
            this.cxGrid1DBTableView1LAST_UPDATED.Name = "cxGrid1DBTableView1LAST_UPDATED";
            // 
            // cxImageComboBoxShowCompanies
            // 
            this.cxImageComboBoxShowCompanies.Location = new System.Drawing.Point(5, 3);
            this.cxImageComboBoxShowCompanies.Name = "cxImageComboBoxShowCompanies";
            this.cxImageComboBoxShowCompanies.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cxImageComboBoxShowCompanies.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("All", "A", -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Active", "Y", -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Inactive", "N", -1)});
            this.cxImageComboBoxShowCompanies.Size = new System.Drawing.Size(115, 20);
            this.cxImageComboBoxShowCompanies.StyleController = this.layoutControl1;
            this.cxImageComboBoxShowCompanies.TabIndex = 5;
            this.cxImageComboBoxShowCompanies.SelectedValueChanged += new System.EventHandler(this.cxImageComboBoxShowCompanies_SelectedValueChanged);
            // 
            // cxLookupComboBoxCompany
            // 
            this.cxLookupComboBoxCompany.Location = new System.Drawing.Point(178, 3);
            this.cxLookupComboBoxCompany.Name = "cxLookupComboBoxCompany";
            this.cxLookupComboBoxCompany.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cxLookupComboBoxCompany.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("COMPANY_ID", "CompanyId", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NAME", "Company")});
            this.cxLookupComboBoxCompany.Properties.DisplayMember = "NAME";
            this.cxLookupComboBoxCompany.Properties.NullText = "";
            this.cxLookupComboBoxCompany.Properties.ValueMember = "COMPANY_ID";
            this.cxLookupComboBoxCompany.Size = new System.Drawing.Size(233, 20);
            this.cxLookupComboBoxCompany.StyleController = this.layoutControl1;
            this.cxLookupComboBoxCompany.TabIndex = 4;
            this.cxLookupComboBoxCompany.EditValueChanged += new System.EventHandler(this.cxLookupComboBoxCompany_EditValueChanged);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(416, 344);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.cxLookupComboBoxCompany;
            this.layoutControlItem1.Location = new System.Drawing.Point(125, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(291, 24);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(291, 24);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 3, 1);
            this.layoutControlItem1.Size = new System.Drawing.Size(291, 24);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.Text = "Company";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(45, 13);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.cxImageComboBoxShowCompanies;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(125, 24);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(125, 24);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 3, 1);
            this.layoutControlItem2.Size = new System.Drawing.Size(125, 24);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.cxGrid1;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem3.Size = new System.Drawing.Size(416, 320);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // fBalconyTerrMaster
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(416, 344);
            this.Controls.Add(this.layoutControl1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fBalconyTerrMaster";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Balcony / Terrace Master";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.fMaster_FormClosing);
            this.Load += new System.EventHandler(this.fMaster_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.fBalconyTerrMaster_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cxGrid1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cxGrid1DBTableView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cxImageComboBoxShowCompanies.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cxLookupComboBoxCompany.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraGrid.GridControl cxGrid1;
        private DevExpress.XtraGrid.Views.Grid.GridView cxGrid1DBTableView1;
        private DevExpress.XtraEditors.ImageComboBoxEdit cxImageComboBoxShowCompanies;
        private DevExpress.XtraEditors.LookUpEdit cxLookupComboBoxCompany;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraGrid.Columns.GridColumn cxGrid1DBTableView1BALCONY_TERRACE_NAME;
        private DevExpress.XtraGrid.Columns.GridColumn cxGrid1DBTableView1COMPANY_ID;
        private DevExpress.XtraGrid.Columns.GridColumn cxGrid1DBTableView1ASPECT_ID;
        private DevExpress.XtraGrid.Columns.GridColumn cxGrid1DBTableView1CREATED_BY;
        private DevExpress.XtraGrid.Columns.GridColumn cxGrid1DBTableView1CREATED_ON;
        private DevExpress.XtraGrid.Columns.GridColumn cxGrid1DBTableView1LAST_UPDATED;

    }
}