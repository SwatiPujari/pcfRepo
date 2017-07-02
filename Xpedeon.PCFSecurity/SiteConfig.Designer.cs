namespace Xpedeon.PCFSecurity
{
    partial class SiteConfig
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SiteConfig));
            this.cxGrid1 = new DevExpress.XtraGrid.GridControl();
            this.cxGrid1DBTableView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.cxGrid1DBTableView1SRNO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cxGrid1DBTableView1NAME = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cxGrid1DBTableView1DESCRIPTION = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cxGrid1DBTableView1VALUE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repChkValue = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.cxGrid1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cxGrid1DBTableView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repChkValue)).BeginInit();
            this.SuspendLayout();
            // 
            // cxGrid1
            // 
            this.cxGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cxGrid1.Location = new System.Drawing.Point(0, 0);
            this.cxGrid1.MainView = this.cxGrid1DBTableView1;
            this.cxGrid1.Name = "cxGrid1";
            this.cxGrid1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repChkValue});
            this.cxGrid1.Size = new System.Drawing.Size(484, 262);
            this.cxGrid1.TabIndex = 0;
            this.cxGrid1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.cxGrid1DBTableView1});
            // 
            // cxGrid1DBTableView1
            // 
            this.cxGrid1DBTableView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.cxGrid1DBTableView1SRNO,
            this.cxGrid1DBTableView1NAME,
            this.cxGrid1DBTableView1DESCRIPTION,
            this.cxGrid1DBTableView1VALUE});
            this.cxGrid1DBTableView1.GridControl = this.cxGrid1;
            this.cxGrid1DBTableView1.Name = "cxGrid1DBTableView1";
            this.cxGrid1DBTableView1.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.cxGrid1DBTableView1.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.cxGrid1DBTableView1.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
            this.cxGrid1DBTableView1.OptionsView.EnableAppearanceEvenRow = true;
            this.cxGrid1DBTableView1.OptionsView.EnableAppearanceOddRow = true;
            this.cxGrid1DBTableView1.OptionsView.ShowAutoFilterRow = true;
            this.cxGrid1DBTableView1.OptionsView.ShowGroupPanel = false;
            this.cxGrid1DBTableView1.CustomRowCellEdit += new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(this.cxGrid1DBTableView1_CustomRowCellEdit);
            this.cxGrid1DBTableView1.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.cxGrid1DBTableView1_FocusedRowChanged);
            this.cxGrid1DBTableView1.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.cxGrid1DBTableView1_RowUpdated);
            this.cxGrid1DBTableView1.CustomColumnSort += new DevExpress.XtraGrid.Views.Base.CustomColumnSortEventHandler(this.cxGrid1DBTableView1_CustomColumnSort);
            // 
            // cxGrid1DBTableView1SRNO
            // 
            this.cxGrid1DBTableView1SRNO.Caption = "Sr No";
            this.cxGrid1DBTableView1SRNO.FieldName = "SRNO";
            this.cxGrid1DBTableView1SRNO.Name = "cxGrid1DBTableView1SRNO";
            this.cxGrid1DBTableView1SRNO.OptionsColumn.AllowEdit = false;
            this.cxGrid1DBTableView1SRNO.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.cxGrid1DBTableView1SRNO.Width = 60;
            // 
            // cxGrid1DBTableView1NAME
            // 
            this.cxGrid1DBTableView1NAME.Caption = "Name";
            this.cxGrid1DBTableView1NAME.FieldName = "NAME";
            this.cxGrid1DBTableView1NAME.Name = "cxGrid1DBTableView1NAME";
            this.cxGrid1DBTableView1NAME.OptionsColumn.AllowEdit = false;
            this.cxGrid1DBTableView1NAME.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.cxGrid1DBTableView1NAME.OptionsFilter.AllowFilter = false;
            this.cxGrid1DBTableView1NAME.Width = 95;
            // 
            // cxGrid1DBTableView1DESCRIPTION
            // 
            this.cxGrid1DBTableView1DESCRIPTION.Caption = "Description";
            this.cxGrid1DBTableView1DESCRIPTION.FieldName = "DESCRIPTION";
            this.cxGrid1DBTableView1DESCRIPTION.Name = "cxGrid1DBTableView1DESCRIPTION";
            this.cxGrid1DBTableView1DESCRIPTION.OptionsColumn.AllowEdit = false;
            this.cxGrid1DBTableView1DESCRIPTION.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.cxGrid1DBTableView1DESCRIPTION.OptionsFilter.AllowFilter = false;
            this.cxGrid1DBTableView1DESCRIPTION.Visible = true;
            this.cxGrid1DBTableView1DESCRIPTION.VisibleIndex = 0;
            this.cxGrid1DBTableView1DESCRIPTION.Width = 356;
            // 
            // cxGrid1DBTableView1VALUE
            // 
            this.cxGrid1DBTableView1VALUE.Caption = "Value";
            this.cxGrid1DBTableView1VALUE.FieldName = "VALUE";
            this.cxGrid1DBTableView1VALUE.Name = "cxGrid1DBTableView1VALUE";
            this.cxGrid1DBTableView1VALUE.OptionsFilter.AllowFilter = false;
            this.cxGrid1DBTableView1VALUE.SortMode = DevExpress.XtraGrid.ColumnSortMode.Custom;
            this.cxGrid1DBTableView1VALUE.Visible = true;
            this.cxGrid1DBTableView1VALUE.VisibleIndex = 1;
            this.cxGrid1DBTableView1VALUE.Width = 200;
            // 
            // repChkValue
            // 
            this.repChkValue.AutoHeight = false;
            this.repChkValue.Name = "repChkValue";
            this.repChkValue.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.repChkValue.ValueChecked = "Y";
            this.repChkValue.ValueGrayed = "N";
            this.repChkValue.ValueUnchecked = "N";
            // 
            // SiteConfig
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 262);
            this.Controls.Add(this.cxGrid1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SiteConfig";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Berkeley Profit & Cashflow Configuration";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SiteConfig_FormClosing);
            this.Load += new System.EventHandler(this.SiteConfig_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cxGrid1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cxGrid1DBTableView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repChkValue)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl cxGrid1;
        private DevExpress.XtraGrid.Views.Grid.GridView cxGrid1DBTableView1;
        private DevExpress.XtraGrid.Columns.GridColumn cxGrid1DBTableView1SRNO;
        private DevExpress.XtraGrid.Columns.GridColumn cxGrid1DBTableView1NAME;
        private DevExpress.XtraGrid.Columns.GridColumn cxGrid1DBTableView1DESCRIPTION;
        private DevExpress.XtraGrid.Columns.GridColumn cxGrid1DBTableView1VALUE;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repChkValue;
    }
}