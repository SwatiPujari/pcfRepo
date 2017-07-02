namespace Xpedeon.ProfitCashflow
{
    partial class DevelopmentMaster
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DevelopmentMaster));
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.gcDevMaster = new DevExpress.XtraGrid.GridControl();
            this.gvDevMaster = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcolCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repTxtCode = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gcolName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcolIsActive = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repChkIsActive = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.splitContainerControl2 = new DevExpress.XtraEditors.SplitContainerControl();
            this.gcIncludedSite = new DevExpress.XtraGrid.GridControl();
            this.gvIncludedSite = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcolISelect = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repChkISelect = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.gcolICode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcolIName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnLink = new DevExpress.XtraEditors.SimpleButton();
            this.btnUnlink = new DevExpress.XtraEditors.SimpleButton();
            this.gcAddSite = new DevExpress.XtraGrid.GridControl();
            this.gvAddSite = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcolASelect = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repChkASelect = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.gcolACode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcolAName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcDevMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDevMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repTxtCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repChkIsActive)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).BeginInit();
            this.splitContainerControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcIncludedSite)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvIncludedSite)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repChkISelect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcAddSite)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAddSite)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repChkASelect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.CollapsePanel = DevExpress.XtraEditors.SplitCollapsePanel.Panel2;
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.splitContainerControl1.Panel1.Controls.Add(this.gcDevMaster);
            this.splitContainerControl1.Panel1.ShowCaption = true;
            this.splitContainerControl1.Panel1.Text = "Developments";
            this.splitContainerControl1.Panel2.Controls.Add(this.splitContainerControl2);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(734, 712);
            this.splitContainerControl1.SplitterPosition = 218;
            this.splitContainerControl1.TabIndex = 0;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // gcDevMaster
            // 
            this.gcDevMaster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcDevMaster.Location = new System.Drawing.Point(0, 0);
            this.gcDevMaster.MainView = this.gvDevMaster;
            this.gcDevMaster.Name = "gcDevMaster";
            this.gcDevMaster.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repTxtCode,
            this.repChkIsActive});
            this.gcDevMaster.Size = new System.Drawing.Size(730, 194);
            this.gcDevMaster.TabIndex = 0;
            this.gcDevMaster.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvDevMaster});
            // 
            // gvDevMaster
            // 
            this.gvDevMaster.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcolCode,
            this.gcolName,
            this.gcolIsActive});
            this.gvDevMaster.GridControl = this.gcDevMaster;
            this.gvDevMaster.Name = "gvDevMaster";
            this.gvDevMaster.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
            this.gvDevMaster.OptionsCustomization.AllowColumnMoving = false;
            this.gvDevMaster.OptionsCustomization.AllowQuickHideColumns = false;
            this.gvDevMaster.OptionsNavigation.EnterMoveNextColumn = true;
            this.gvDevMaster.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            this.gvDevMaster.OptionsView.RowAutoHeight = true;
            this.gvDevMaster.OptionsView.ShowAutoFilterRow = true;
            this.gvDevMaster.OptionsView.ShowGroupPanel = false;
            this.gvDevMaster.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gcolCode, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gvDevMaster.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.gvDevMaster_InitNewRow);
            this.gvDevMaster.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvDevMaster_FocusedRowChanged);
            this.gvDevMaster.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gvDevMaster_CellValueChanging);
            this.gvDevMaster.InvalidRowException += new DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventHandler(this.gvDevMaster_InvalidRowException);
            this.gvDevMaster.BeforeLeaveRow += new DevExpress.XtraGrid.Views.Base.RowAllowEventHandler(this.gvDevMaster_BeforeLeaveRow);
            this.gvDevMaster.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(this.gvDevMaster_ValidateRow);
            this.gvDevMaster.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.gvDevMaster_RowUpdated);
            // 
            // gcolCode
            // 
            this.gcolCode.Caption = "Code";
            this.gcolCode.ColumnEdit = this.repTxtCode;
            this.gcolCode.FieldName = "DEVELOPMENT_CODE";
            this.gcolCode.Name = "gcolCode";
            this.gcolCode.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gcolCode.Visible = true;
            this.gcolCode.VisibleIndex = 0;
            this.gcolCode.Width = 95;
            // 
            // repTxtCode
            // 
            this.repTxtCode.AutoHeight = false;
            this.repTxtCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.repTxtCode.MaxLength = 30;
            this.repTxtCode.Name = "repTxtCode";
            // 
            // gcolName
            // 
            this.gcolName.Caption = "Name";
            this.gcolName.FieldName = "DEVELOPMENT_NAME";
            this.gcolName.Name = "gcolName";
            this.gcolName.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gcolName.Visible = true;
            this.gcolName.VisibleIndex = 1;
            this.gcolName.Width = 280;
            // 
            // gcolIsActive
            // 
            this.gcolIsActive.Caption = "Active";
            this.gcolIsActive.ColumnEdit = this.repChkIsActive;
            this.gcolIsActive.FieldName = "IS_ACTIVE";
            this.gcolIsActive.Name = "gcolIsActive";
            this.gcolIsActive.Visible = true;
            this.gcolIsActive.VisibleIndex = 2;
            this.gcolIsActive.Width = 60;
            // 
            // repChkIsActive
            // 
            this.repChkIsActive.AutoHeight = false;
            this.repChkIsActive.Name = "repChkIsActive";
            this.repChkIsActive.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.repChkIsActive.ValueChecked = "Y";
            this.repChkIsActive.ValueGrayed = "N";
            this.repChkIsActive.ValueUnchecked = "N";
            // 
            // splitContainerControl2
            // 
            this.splitContainerControl2.CollapsePanel = DevExpress.XtraEditors.SplitCollapsePanel.Panel2;
            this.splitContainerControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl2.Horizontal = false;
            this.splitContainerControl2.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl2.Name = "splitContainerControl2";
            this.splitContainerControl2.Panel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.splitContainerControl2.Panel1.Controls.Add(this.gcIncludedSite);
            this.splitContainerControl2.Panel1.ShowCaption = true;
            this.splitContainerControl2.Panel1.Text = "Included Sites";
            this.splitContainerControl2.Panel2.Controls.Add(this.layoutControl1);
            this.splitContainerControl2.Panel2.Text = "Panel2";
            this.splitContainerControl2.Size = new System.Drawing.Size(734, 490);
            this.splitContainerControl2.SplitterPosition = 192;
            this.splitContainerControl2.TabIndex = 0;
            this.splitContainerControl2.Text = "splitContainerControl2";
            // 
            // gcIncludedSite
            // 
            this.gcIncludedSite.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcIncludedSite.Location = new System.Drawing.Point(0, 0);
            this.gcIncludedSite.MainView = this.gvIncludedSite;
            this.gcIncludedSite.Name = "gcIncludedSite";
            this.gcIncludedSite.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repChkISelect});
            this.gcIncludedSite.Size = new System.Drawing.Size(730, 168);
            this.gcIncludedSite.TabIndex = 0;
            this.gcIncludedSite.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvIncludedSite});
            // 
            // gvIncludedSite
            // 
            this.gvIncludedSite.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcolISelect,
            this.gcolICode,
            this.gcolIName});
            this.gvIncludedSite.GridControl = this.gcIncludedSite;
            this.gvIncludedSite.Name = "gvIncludedSite";
            this.gvIncludedSite.OptionsView.EnableAppearanceEvenRow = true;
            this.gvIncludedSite.OptionsView.EnableAppearanceOddRow = true;
            this.gvIncludedSite.OptionsView.RowAutoHeight = true;
            this.gvIncludedSite.OptionsView.ShowAutoFilterRow = true;
            this.gvIncludedSite.OptionsView.ShowGroupPanel = false;
            this.gvIncludedSite.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gcolICode, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gvIncludedSite.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gvLinkage_CellValueChanging);
            // 
            // gcolISelect
            // 
            this.gcolISelect.Caption = "Select";
            this.gcolISelect.ColumnEdit = this.repChkISelect;
            this.gcolISelect.FieldName = "Select";
            this.gcolISelect.Name = "gcolISelect";
            this.gcolISelect.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gcolISelect.Visible = true;
            this.gcolISelect.VisibleIndex = 0;
            this.gcolISelect.Width = 60;
            // 
            // repChkISelect
            // 
            this.repChkISelect.AutoHeight = false;
            this.repChkISelect.Name = "repChkISelect";
            this.repChkISelect.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.repChkISelect.ValueChecked = "Y";
            this.repChkISelect.ValueGrayed = "N";
            this.repChkISelect.ValueUnchecked = "N";
            // 
            // gcolICode
            // 
            this.gcolICode.Caption = "Code";
            this.gcolICode.FieldName = "SITE_CODE";
            this.gcolICode.Name = "gcolICode";
            this.gcolICode.OptionsColumn.AllowEdit = false;
            this.gcolICode.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gcolICode.Visible = true;
            this.gcolICode.VisibleIndex = 1;
            this.gcolICode.Width = 95;
            // 
            // gcolIName
            // 
            this.gcolIName.Caption = "Name";
            this.gcolIName.FieldName = "SITE_NAME";
            this.gcolIName.Name = "gcolIName";
            this.gcolIName.OptionsColumn.AllowEdit = false;
            this.gcolIName.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gcolIName.Visible = true;
            this.gcolIName.VisibleIndex = 2;
            this.gcolIName.Width = 280;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnLink);
            this.layoutControl1.Controls.Add(this.btnUnlink);
            this.layoutControl1.Controls.Add(this.gcAddSite);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(516, 262, 450, 400);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(734, 294);
            this.layoutControl1.TabIndex = 1;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnLink
            // 
            this.btnLink.Image = ((System.Drawing.Image)(resources.GetObject("btnLink.Image")));
            this.btnLink.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnLink.Location = new System.Drawing.Point(317, 2);
            this.btnLink.Name = "btnLink";
            this.btnLink.Size = new System.Drawing.Size(48, 38);
            this.btnLink.StyleController = this.layoutControl1;
            this.btnLink.TabIndex = 5;
            this.btnLink.ToolTip = "Include Sites";
            this.btnLink.Click += new System.EventHandler(this.btnLink_Click);
            // 
            // btnUnlink
            // 
            this.btnUnlink.Image = ((System.Drawing.Image)(resources.GetObject("btnUnlink.Image")));
            this.btnUnlink.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnUnlink.Location = new System.Drawing.Point(369, 2);
            this.btnUnlink.Name = "btnUnlink";
            this.btnUnlink.Size = new System.Drawing.Size(48, 38);
            this.btnUnlink.StyleController = this.layoutControl1;
            this.btnUnlink.TabIndex = 4;
            this.btnUnlink.ToolTip = "Exclude Sites";
            this.btnUnlink.Click += new System.EventHandler(this.btnUnlink_Click);
            // 
            // gcAddSite
            // 
            this.gcAddSite.Location = new System.Drawing.Point(1, 63);
            this.gcAddSite.MainView = this.gvAddSite;
            this.gcAddSite.Name = "gcAddSite";
            this.gcAddSite.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repChkASelect});
            this.gcAddSite.Size = new System.Drawing.Size(732, 230);
            this.gcAddSite.TabIndex = 0;
            this.gcAddSite.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvAddSite});
            // 
            // gvAddSite
            // 
            this.gvAddSite.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcolASelect,
            this.gcolACode,
            this.gcolAName});
            this.gvAddSite.GridControl = this.gcAddSite;
            this.gvAddSite.Name = "gvAddSite";
            this.gvAddSite.OptionsCustomization.AllowColumnMoving = false;
            this.gvAddSite.OptionsCustomization.AllowQuickHideColumns = false;
            this.gvAddSite.OptionsView.EnableAppearanceEvenRow = true;
            this.gvAddSite.OptionsView.EnableAppearanceOddRow = true;
            this.gvAddSite.OptionsView.RowAutoHeight = true;
            this.gvAddSite.OptionsView.ShowAutoFilterRow = true;
            this.gvAddSite.OptionsView.ShowGroupPanel = false;
            this.gvAddSite.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gcolACode, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gvAddSite.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gvLinkage_CellValueChanging);
            // 
            // gcolASelect
            // 
            this.gcolASelect.Caption = "Select";
            this.gcolASelect.ColumnEdit = this.repChkASelect;
            this.gcolASelect.FieldName = "Select";
            this.gcolASelect.Name = "gcolASelect";
            this.gcolASelect.Visible = true;
            this.gcolASelect.VisibleIndex = 0;
            this.gcolASelect.Width = 60;
            // 
            // repChkASelect
            // 
            this.repChkASelect.AutoHeight = false;
            this.repChkASelect.Name = "repChkASelect";
            this.repChkASelect.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.repChkASelect.ValueChecked = "Y";
            this.repChkASelect.ValueGrayed = "N";
            this.repChkASelect.ValueUnchecked = "N";
            // 
            // gcolACode
            // 
            this.gcolACode.Caption = "Code";
            this.gcolACode.FieldName = "SITE_CODE";
            this.gcolACode.Name = "gcolACode";
            this.gcolACode.OptionsColumn.AllowEdit = false;
            this.gcolACode.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gcolACode.Visible = true;
            this.gcolACode.VisibleIndex = 1;
            this.gcolACode.Width = 95;
            // 
            // gcolAName
            // 
            this.gcolAName.Caption = "Name";
            this.gcolAName.FieldName = "SITE_NAME";
            this.gcolAName.Name = "gcolAName";
            this.gcolAName.OptionsColumn.AllowEdit = false;
            this.gcolAName.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.gcolAName.Visible = true;
            this.gcolAName.VisibleIndex = 2;
            this.gcolAName.Width = 280;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem1,
            this.emptySpaceItem2,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlGroup2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(734, 294);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(419, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(315, 42);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(315, 42);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnUnlink;
            this.layoutControlItem2.Location = new System.Drawing.Point(367, 0);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(52, 42);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(52, 42);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(52, 42);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnLink;
            this.layoutControlItem3.Location = new System.Drawing.Point(315, 0);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(52, 42);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(52, 42);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(52, 42);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 42);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Size = new System.Drawing.Size(734, 252);
            this.layoutControlGroup2.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Text = "Add Sites";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcAddSite;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem1.Size = new System.Drawing.Size(732, 230);
            this.layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // DevelopmentMaster
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 712);
            this.Controls.Add(this.splitContainerControl1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "DevelopmentMaster";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Development Master";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DevelopmentMaster_FormClosing);
            this.Load += new System.EventHandler(this.DevelopmentMaster_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.DevelopmentMaster_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcDevMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDevMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repTxtCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repChkIsActive)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).EndInit();
            this.splitContainerControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcIncludedSite)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvIncludedSite)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repChkISelect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcAddSite)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAddSite)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repChkASelect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl2;
        private DevExpress.XtraGrid.GridControl gcDevMaster;
        private DevExpress.XtraGrid.Views.Grid.GridView gvDevMaster;
        private DevExpress.XtraGrid.Columns.GridColumn gcolCode;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repTxtCode;
        private DevExpress.XtraGrid.Columns.GridColumn gcolName;
        private DevExpress.XtraGrid.Columns.GridColumn gcolIsActive;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repChkIsActive;
        private DevExpress.XtraGrid.GridControl gcIncludedSite;
        private DevExpress.XtraGrid.Views.Grid.GridView gvIncludedSite;
        private DevExpress.XtraGrid.Columns.GridColumn gcolISelect;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repChkISelect;
        private DevExpress.XtraGrid.Columns.GridColumn gcolICode;
        private DevExpress.XtraGrid.Columns.GridColumn gcolIName;
        private DevExpress.XtraGrid.GridControl gcAddSite;
        private DevExpress.XtraGrid.Views.Grid.GridView gvAddSite;
        private DevExpress.XtraGrid.Columns.GridColumn gcolASelect;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repChkASelect;
        private DevExpress.XtraGrid.Columns.GridColumn gcolACode;
        private DevExpress.XtraGrid.Columns.GridColumn gcolAName;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.SimpleButton btnLink;
        private DevExpress.XtraEditors.SimpleButton btnUnlink;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;

    }
}