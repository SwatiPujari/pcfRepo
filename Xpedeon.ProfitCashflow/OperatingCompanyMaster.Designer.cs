namespace Xpedeon.ProfitCashflow
{
    partial class OperatingCompanyMaster
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OperatingCompanyMaster));
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.gcOperatingComp = new DevExpress.XtraGrid.GridControl();
            this.gvOperatingComp = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcolCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repTxtCode = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gcolName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcolIsActive = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repChkIsActive = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.splitContainerControl2 = new DevExpress.XtraEditors.SplitContainerControl();
            this.gcIncludedComp = new DevExpress.XtraGrid.GridControl();
            this.gvIncludedComp = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcolISelect = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repChkISelect = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.gcolICode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcolIName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnLink = new DevExpress.XtraEditors.SimpleButton();
            this.btnUnlink = new DevExpress.XtraEditors.SimpleButton();
            this.gcAddComp = new DevExpress.XtraGrid.GridControl();
            this.gvAddComp = new DevExpress.XtraGrid.Views.Grid.GridView();
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
            ((System.ComponentModel.ISupportInitialize)(this.gcOperatingComp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvOperatingComp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repTxtCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repChkIsActive)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).BeginInit();
            this.splitContainerControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcIncludedComp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvIncludedComp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repChkISelect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcAddComp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAddComp)).BeginInit();
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
            this.splitContainerControl1.Panel1.Controls.Add(this.gcOperatingComp);
            this.splitContainerControl1.Panel1.ShowCaption = true;
            this.splitContainerControl1.Panel1.Text = "Operating Companies";
            this.splitContainerControl1.Panel2.Controls.Add(this.splitContainerControl2);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(734, 712);
            this.splitContainerControl1.SplitterPosition = 218;
            this.splitContainerControl1.TabIndex = 0;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // gcOperatingComp
            // 
            this.gcOperatingComp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcOperatingComp.Location = new System.Drawing.Point(0, 0);
            this.gcOperatingComp.MainView = this.gvOperatingComp;
            this.gcOperatingComp.Name = "gcOperatingComp";
            this.gcOperatingComp.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repTxtCode,
            this.repChkIsActive});
            this.gcOperatingComp.Size = new System.Drawing.Size(730, 196);
            this.gcOperatingComp.TabIndex = 0;
            this.gcOperatingComp.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvOperatingComp});
            // 
            // gvOperatingComp
            // 
            this.gvOperatingComp.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcolCode,
            this.gcolName,
            this.gcolIsActive});
            this.gvOperatingComp.GridControl = this.gcOperatingComp;
            this.gvOperatingComp.Name = "gvOperatingComp";
            this.gvOperatingComp.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
            this.gvOperatingComp.OptionsCustomization.AllowColumnMoving = false;
            this.gvOperatingComp.OptionsCustomization.AllowQuickHideColumns = false;
            this.gvOperatingComp.OptionsNavigation.EnterMoveNextColumn = true;
            this.gvOperatingComp.OptionsView.EnableAppearanceEvenRow = true;
            this.gvOperatingComp.OptionsView.EnableAppearanceOddRow = true;
            this.gvOperatingComp.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            this.gvOperatingComp.OptionsView.RowAutoHeight = true;
            this.gvOperatingComp.OptionsView.ShowAutoFilterRow = true;
            this.gvOperatingComp.OptionsView.ShowGroupPanel = false;
            this.gvOperatingComp.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gcolCode, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gvOperatingComp.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.gvOperatingComp_InitNewRow);
            this.gvOperatingComp.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvOperatingComp_FocusedRowChanged);
            this.gvOperatingComp.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gvOperatingComp_CellValueChanging);
            this.gvOperatingComp.InvalidRowException += new DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventHandler(this.gvOperatingComp_InvalidRowException);
            this.gvOperatingComp.BeforeLeaveRow += new DevExpress.XtraGrid.Views.Base.RowAllowEventHandler(this.gvOperatingComp_BeforeLeaveRow);
            this.gvOperatingComp.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(this.gvOperatingComp_ValidateRow);
            this.gvOperatingComp.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.gvOperatingComp_RowUpdated);
            // 
            // gcolCode
            // 
            this.gcolCode.Caption = "Code";
            this.gcolCode.ColumnEdit = this.repTxtCode;
            this.gcolCode.FieldName = "OPERATING_COMPANY_CODE";
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
            this.gcolName.FieldName = "OPERATING_COMPANY_NAME";
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
            this.splitContainerControl2.Panel1.Controls.Add(this.gcIncludedComp);
            this.splitContainerControl2.Panel1.ShowCaption = true;
            this.splitContainerControl2.Panel1.Text = "Included Companies";
            this.splitContainerControl2.Panel2.Controls.Add(this.layoutControl1);
            this.splitContainerControl2.Panel2.Text = "Panel2";
            this.splitContainerControl2.Size = new System.Drawing.Size(734, 489);
            this.splitContainerControl2.SplitterPosition = 192;
            this.splitContainerControl2.TabIndex = 0;
            this.splitContainerControl2.Text = "splitContainerControl2";
            // 
            // gcIncludedComp
            // 
            this.gcIncludedComp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcIncludedComp.Location = new System.Drawing.Point(0, 0);
            this.gcIncludedComp.MainView = this.gvIncludedComp;
            this.gcIncludedComp.Name = "gcIncludedComp";
            this.gcIncludedComp.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repChkISelect});
            this.gcIncludedComp.Size = new System.Drawing.Size(730, 170);
            this.gcIncludedComp.TabIndex = 0;
            this.gcIncludedComp.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvIncludedComp});
            // 
            // gvIncludedComp
            // 
            this.gvIncludedComp.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcolISelect,
            this.gcolICode,
            this.gcolIName});
            this.gvIncludedComp.GridControl = this.gcIncludedComp;
            this.gvIncludedComp.Name = "gvIncludedComp";
            this.gvIncludedComp.OptionsView.EnableAppearanceEvenRow = true;
            this.gvIncludedComp.OptionsView.EnableAppearanceOddRow = true;
            this.gvIncludedComp.OptionsView.RowAutoHeight = true;
            this.gvIncludedComp.OptionsView.ShowAutoFilterRow = true;
            this.gvIncludedComp.OptionsView.ShowGroupPanel = false;
            this.gvIncludedComp.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gcolICode, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gvIncludedComp.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gvCompLinkage_CellValueChanging);
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
            this.gcolICode.FieldName = "COMPANY_CODE";
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
            this.gcolIName.FieldName = "NAME";
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
            this.layoutControl1.Controls.Add(this.gcAddComp);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(516, 262, 450, 400);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(734, 292);
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
            this.btnLink.ToolTip = "Include Companies";
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
            this.btnUnlink.ToolTip = "Exclude Companies";
            this.btnUnlink.Click += new System.EventHandler(this.btnUnlink_Click);
            // 
            // gcAddComp
            // 
            this.gcAddComp.Location = new System.Drawing.Point(1, 61);
            this.gcAddComp.MainView = this.gvAddComp;
            this.gcAddComp.Name = "gcAddComp";
            this.gcAddComp.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repChkASelect});
            this.gcAddComp.Size = new System.Drawing.Size(732, 230);
            this.gcAddComp.TabIndex = 0;
            this.gcAddComp.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvAddComp});
            // 
            // gvAddComp
            // 
            this.gvAddComp.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcolASelect,
            this.gcolACode,
            this.gcolAName});
            this.gvAddComp.GridControl = this.gcAddComp;
            this.gvAddComp.Name = "gvAddComp";
            this.gvAddComp.OptionsCustomization.AllowColumnMoving = false;
            this.gvAddComp.OptionsCustomization.AllowQuickHideColumns = false;
            this.gvAddComp.OptionsView.EnableAppearanceEvenRow = true;
            this.gvAddComp.OptionsView.EnableAppearanceOddRow = true;
            this.gvAddComp.OptionsView.RowAutoHeight = true;
            this.gvAddComp.OptionsView.ShowAutoFilterRow = true;
            this.gvAddComp.OptionsView.ShowGroupPanel = false;
            this.gvAddComp.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gcolACode, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gvAddComp.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gvCompLinkage_CellValueChanging);
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
            this.gcolACode.FieldName = "COMPANY_CODE";
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
            this.gcolAName.FieldName = "NAME";
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
            this.layoutControlGroup1.Size = new System.Drawing.Size(734, 292);
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
            this.layoutControlGroup2.Size = new System.Drawing.Size(734, 250);
            this.layoutControlGroup2.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Text = "Add Companies";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcAddComp;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem1.Size = new System.Drawing.Size(732, 230);
            this.layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // OperatingCompanyMaster
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 712);
            this.Controls.Add(this.splitContainerControl1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "OperatingCompanyMaster";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Operating Company Master";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OperatingCompanyMaster_FormClosing);
            this.Load += new System.EventHandler(this.OperatingCompanyMaster_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.OperatingCompanyMaster_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcOperatingComp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvOperatingComp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repTxtCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repChkIsActive)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).EndInit();
            this.splitContainerControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcIncludedComp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvIncludedComp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repChkISelect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcAddComp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAddComp)).EndInit();
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
        private DevExpress.XtraGrid.GridControl gcOperatingComp;
        private DevExpress.XtraGrid.Views.Grid.GridView gvOperatingComp;
        private DevExpress.XtraGrid.Columns.GridColumn gcolCode;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repTxtCode;
        private DevExpress.XtraGrid.Columns.GridColumn gcolName;
        private DevExpress.XtraGrid.Columns.GridColumn gcolIsActive;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repChkIsActive;
        private DevExpress.XtraGrid.GridControl gcIncludedComp;
        private DevExpress.XtraGrid.Views.Grid.GridView gvIncludedComp;
        private DevExpress.XtraGrid.Columns.GridColumn gcolISelect;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repChkISelect;
        private DevExpress.XtraGrid.Columns.GridColumn gcolICode;
        private DevExpress.XtraGrid.Columns.GridColumn gcolIName;
        private DevExpress.XtraGrid.GridControl gcAddComp;
        private DevExpress.XtraGrid.Views.Grid.GridView gvAddComp;
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