namespace Xpedeon.PCFSecurity
{
    partial class ErpUsers
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ErpUsers));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.cxGrid1 = new DevExpress.XtraGrid.GridControl();
            this.cxGrid1DBTableView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.cxGrid1DBTableView1COMPANY_LINK = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repChkLinkComp = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.cxGrid1DBTableView1COMPANY_ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cxGrid1DBTableView1COMPANY_CODE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cxGrid1DBTableView1NAME = new DevExpress.XtraGrid.Columns.GridColumn();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.dxBarBtnNewUser = new DevExpress.XtraBars.BarButtonItem();
            this.dxBarBtnEditUser = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.cxGridUsers = new DevExpress.XtraGrid.GridControl();
            this.cxGridUsersDBTV = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.cxGridUsersDBTVUSERNAME = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cxGridUsersDBTVDISPLAYNAME = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cxGridUsersDBTVCODA_USERNAME = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cxGridUsersDBTVEMAIL = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repHyperLinkEMAIL = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.popupMenu1 = new DevExpress.XtraBars.PopupMenu(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cxGrid1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cxGrid1DBTableView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repChkLinkComp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cxGridUsers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cxGridUsersDBTV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repHyperLinkEMAIL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.layoutControl1.Controls.Add(this.cxGrid1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(528, 178, 450, 400);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(975, 197);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // cxGrid1
            // 
            this.cxGrid1.Location = new System.Drawing.Point(2, 18);
            this.cxGrid1.MainView = this.cxGrid1DBTableView1;
            this.cxGrid1.MenuManager = this.barManager1;
            this.cxGrid1.Name = "cxGrid1";
            this.cxGrid1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repChkLinkComp});
            this.cxGrid1.Size = new System.Drawing.Size(971, 177);
            this.cxGrid1.TabIndex = 4;
            this.cxGrid1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.cxGrid1DBTableView1});
            // 
            // cxGrid1DBTableView1
            // 
            this.cxGrid1DBTableView1.Appearance.Empty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.cxGrid1DBTableView1.Appearance.Empty.Options.UseBackColor = true;
            this.cxGrid1DBTableView1.Appearance.Row.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.cxGrid1DBTableView1.Appearance.Row.Options.UseBackColor = true;
            this.cxGrid1DBTableView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.cxGrid1DBTableView1COMPANY_LINK,
            this.cxGrid1DBTableView1COMPANY_ID,
            this.cxGrid1DBTableView1COMPANY_CODE,
            this.cxGrid1DBTableView1NAME});
            this.cxGrid1DBTableView1.GridControl = this.cxGrid1;
            this.cxGrid1DBTableView1.Name = "cxGrid1DBTableView1";
            this.cxGrid1DBTableView1.OptionsCustomization.AllowColumnMoving = false;
            this.cxGrid1DBTableView1.OptionsNavigation.EnterMoveNextColumn = true;
            this.cxGrid1DBTableView1.OptionsView.ShowAutoFilterRow = true;
            this.cxGrid1DBTableView1.OptionsView.ShowGroupPanel = false;
            this.cxGrid1DBTableView1.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.cxGrid1DBTableView1NAME, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.cxGrid1DBTableView1.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.GridView_RowStyle);
            this.cxGrid1DBTableView1.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.cxGrid1DBTableView1_RowUpdated);
            // 
            // cxGrid1DBTableView1COMPANY_LINK
            // 
            this.cxGrid1DBTableView1COMPANY_LINK.Caption = "Link ?";
            this.cxGrid1DBTableView1COMPANY_LINK.ColumnEdit = this.repChkLinkComp;
            this.cxGrid1DBTableView1COMPANY_LINK.FieldName = "COMPANY_LINK";
            this.cxGrid1DBTableView1COMPANY_LINK.Name = "cxGrid1DBTableView1COMPANY_LINK";
            this.cxGrid1DBTableView1COMPANY_LINK.Visible = true;
            this.cxGrid1DBTableView1COMPANY_LINK.VisibleIndex = 0;
            this.cxGrid1DBTableView1COMPANY_LINK.Width = 60;
            // 
            // repChkLinkComp
            // 
            this.repChkLinkComp.AutoHeight = false;
            this.repChkLinkComp.Name = "repChkLinkComp";
            this.repChkLinkComp.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.repChkLinkComp.ValueChecked = "Y";
            this.repChkLinkComp.ValueGrayed = "N";
            this.repChkLinkComp.ValueUnchecked = "N";
            // 
            // cxGrid1DBTableView1COMPANY_ID
            // 
            this.cxGrid1DBTableView1COMPANY_ID.Caption = "Company Id";
            this.cxGrid1DBTableView1COMPANY_ID.FieldName = "COMPANY_ID";
            this.cxGrid1DBTableView1COMPANY_ID.Name = "cxGrid1DBTableView1COMPANY_ID";
            this.cxGrid1DBTableView1COMPANY_ID.OptionsColumn.AllowEdit = false;
            // 
            // cxGrid1DBTableView1COMPANY_CODE
            // 
            this.cxGrid1DBTableView1COMPANY_CODE.Caption = "Code";
            this.cxGrid1DBTableView1COMPANY_CODE.FieldName = "COMPANY_CODE";
            this.cxGrid1DBTableView1COMPANY_CODE.Name = "cxGrid1DBTableView1COMPANY_CODE";
            this.cxGrid1DBTableView1COMPANY_CODE.OptionsColumn.AllowEdit = false;
            this.cxGrid1DBTableView1COMPANY_CODE.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.cxGrid1DBTableView1COMPANY_CODE.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.cxGrid1DBTableView1COMPANY_CODE.Visible = true;
            this.cxGrid1DBTableView1COMPANY_CODE.VisibleIndex = 1;
            this.cxGrid1DBTableView1COMPANY_CODE.Width = 95;
            // 
            // cxGrid1DBTableView1NAME
            // 
            this.cxGrid1DBTableView1NAME.Caption = "Name";
            this.cxGrid1DBTableView1NAME.FieldName = "NAME";
            this.cxGrid1DBTableView1NAME.Name = "cxGrid1DBTableView1NAME";
            this.cxGrid1DBTableView1NAME.OptionsColumn.AllowEdit = false;
            this.cxGrid1DBTableView1NAME.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.cxGrid1DBTableView1NAME.Visible = true;
            this.cxGrid1DBTableView1NAME.VisibleIndex = 2;
            this.cxGrid1DBTableView1NAME.Width = 280;
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.dxBarBtnNewUser,
            this.dxBarBtnEditUser});
            this.barManager1.MaxItemId = 2;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.dxBarBtnNewUser),
            new DevExpress.XtraBars.LinkPersistInfo(this.dxBarBtnEditUser)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DisableClose = true;
            this.bar1.OptionsBar.DisableCustomization = true;
            this.bar1.OptionsBar.DrawBorder = false;
            this.bar1.Text = "Tools";
            // 
            // dxBarBtnNewUser
            // 
            this.dxBarBtnNewUser.Caption = "New";
            this.dxBarBtnNewUser.Hint = "New";
            this.dxBarBtnNewUser.Id = 0;
            this.dxBarBtnNewUser.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("dxBarBtnNewUser.ImageOptions.Image")));
            this.dxBarBtnNewUser.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("dxBarBtnNewUser.ImageOptions.LargeImage")));
            this.dxBarBtnNewUser.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N));
            this.dxBarBtnNewUser.Name = "dxBarBtnNewUser";
            this.dxBarBtnNewUser.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.dxBarBtnNewUser_ItemClick);
            // 
            // dxBarBtnEditUser
            // 
            this.dxBarBtnEditUser.Caption = "Edit";
            this.dxBarBtnEditUser.Hint = "Edit";
            this.dxBarBtnEditUser.Id = 1;
            this.dxBarBtnEditUser.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("dxBarBtnEditUser.ImageOptions.Image")));
            this.dxBarBtnEditUser.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("dxBarBtnEditUser.ImageOptions.LargeImage")));
            this.dxBarBtnEditUser.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E));
            this.dxBarBtnEditUser.Name = "dxBarBtnEditUser";
            this.dxBarBtnEditUser.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.dxBarBtnEditUser_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(979, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 486);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(979, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 455);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(979, 31);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 455);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(975, 197);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlItem1.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.layoutControlItem1.Control = this.cxGrid1;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(975, 197);
            this.layoutControlItem1.Text = "Company";
            this.layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(45, 13);
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Appearance.BackColor = System.Drawing.Color.White;
            this.splitContainerControl1.Appearance.Options.UseBackColor = true;
            this.splitContainerControl1.CollapsePanel = DevExpress.XtraEditors.SplitCollapsePanel.Panel2;
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.None;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 31);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.cxGridUsers);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.splitContainerControl1.Panel2.Controls.Add(this.layoutControl1);
            this.splitContainerControl1.Panel2.ShowCaption = true;
            this.splitContainerControl1.Panel2.Text = "Security";
            this.splitContainerControl1.Size = new System.Drawing.Size(979, 455);
            this.splitContainerControl1.SplitterPosition = 231;
            this.splitContainerControl1.TabIndex = 4;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // cxGridUsers
            // 
            this.cxGridUsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cxGridUsers.Location = new System.Drawing.Point(0, 0);
            this.cxGridUsers.MainView = this.cxGridUsersDBTV;
            this.cxGridUsers.MenuManager = this.barManager1;
            this.cxGridUsers.Name = "cxGridUsers";
            this.cxGridUsers.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repHyperLinkEMAIL});
            this.cxGridUsers.Size = new System.Drawing.Size(979, 231);
            this.cxGridUsers.TabIndex = 0;
            this.cxGridUsers.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.cxGridUsersDBTV});
            // 
            // cxGridUsersDBTV
            // 
            this.cxGridUsersDBTV.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.cxGridUsersDBTVUSERNAME,
            this.cxGridUsersDBTVDISPLAYNAME,
            this.cxGridUsersDBTVCODA_USERNAME,
            this.cxGridUsersDBTVEMAIL});
            this.cxGridUsersDBTV.GridControl = this.cxGridUsers;
            this.cxGridUsersDBTV.Name = "cxGridUsersDBTV";
            this.cxGridUsersDBTV.OptionsBehavior.Editable = false;
            this.cxGridUsersDBTV.OptionsCustomization.AllowColumnMoving = false;
            this.cxGridUsersDBTV.OptionsNavigation.EnterMoveNextColumn = true;
            this.cxGridUsersDBTV.OptionsView.EnableAppearanceEvenRow = true;
            this.cxGridUsersDBTV.OptionsView.EnableAppearanceOddRow = true;
            this.cxGridUsersDBTV.OptionsView.ShowAutoFilterRow = true;
            this.cxGridUsersDBTV.OptionsView.ShowGroupPanel = false;
            this.cxGridUsersDBTV.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.cxGridUsersDBTVUSERNAME, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.cxGridUsersDBTV.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.GridView_RowStyle);
            this.cxGridUsersDBTV.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.cxGridUsersDBTV_FocusedRowChanged);
            this.cxGridUsersDBTV.MouseUp += new System.Windows.Forms.MouseEventHandler(this.cxGridUsersDBTV_MouseUp);
            // 
            // cxGridUsersDBTVUSERNAME
            // 
            this.cxGridUsersDBTVUSERNAME.Caption = "User Name";
            this.cxGridUsersDBTVUSERNAME.FieldName = "USERNAME";
            this.cxGridUsersDBTVUSERNAME.Name = "cxGridUsersDBTVUSERNAME";
            this.cxGridUsersDBTVUSERNAME.OptionsColumn.AllowEdit = false;
            this.cxGridUsersDBTVUSERNAME.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.cxGridUsersDBTVUSERNAME.Visible = true;
            this.cxGridUsersDBTVUSERNAME.VisibleIndex = 0;
            this.cxGridUsersDBTVUSERNAME.Width = 95;
            // 
            // cxGridUsersDBTVDISPLAYNAME
            // 
            this.cxGridUsersDBTVDISPLAYNAME.Caption = "Display Name";
            this.cxGridUsersDBTVDISPLAYNAME.FieldName = "DISPLAYNAME";
            this.cxGridUsersDBTVDISPLAYNAME.Name = "cxGridUsersDBTVDISPLAYNAME";
            this.cxGridUsersDBTVDISPLAYNAME.OptionsColumn.AllowEdit = false;
            this.cxGridUsersDBTVDISPLAYNAME.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.cxGridUsersDBTVDISPLAYNAME.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            this.cxGridUsersDBTVDISPLAYNAME.Visible = true;
            this.cxGridUsersDBTVDISPLAYNAME.VisibleIndex = 1;
            this.cxGridUsersDBTVDISPLAYNAME.Width = 280;
            // 
            // cxGridUsersDBTVCODA_USERNAME
            // 
            this.cxGridUsersDBTVCODA_USERNAME.Caption = "CODA User Name";
            this.cxGridUsersDBTVCODA_USERNAME.FieldName = "CODA_USERNAME";
            this.cxGridUsersDBTVCODA_USERNAME.Name = "cxGridUsersDBTVCODA_USERNAME";
            this.cxGridUsersDBTVCODA_USERNAME.OptionsColumn.AllowEdit = false;
            this.cxGridUsersDBTVCODA_USERNAME.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.cxGridUsersDBTVCODA_USERNAME.OptionsFilter.AllowFilter = false;
            this.cxGridUsersDBTVCODA_USERNAME.Visible = true;
            this.cxGridUsersDBTVCODA_USERNAME.VisibleIndex = 2;
            this.cxGridUsersDBTVCODA_USERNAME.Width = 95;
            // 
            // cxGridUsersDBTVEMAIL
            // 
            this.cxGridUsersDBTVEMAIL.Caption = "E-Mail";
            this.cxGridUsersDBTVEMAIL.ColumnEdit = this.repHyperLinkEMAIL;
            this.cxGridUsersDBTVEMAIL.FieldName = "EMAIL";
            this.cxGridUsersDBTVEMAIL.Name = "cxGridUsersDBTVEMAIL";
            this.cxGridUsersDBTVEMAIL.OptionsColumn.AllowEdit = false;
            this.cxGridUsersDBTVEMAIL.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.cxGridUsersDBTVEMAIL.OptionsFilter.AllowFilter = false;
            this.cxGridUsersDBTVEMAIL.Visible = true;
            this.cxGridUsersDBTVEMAIL.VisibleIndex = 3;
            this.cxGridUsersDBTVEMAIL.Width = 280;
            // 
            // repHyperLinkEMAIL
            // 
            this.repHyperLinkEMAIL.AutoHeight = false;
            this.repHyperLinkEMAIL.LinkColor = System.Drawing.Color.Blue;
            this.repHyperLinkEMAIL.Name = "repHyperLinkEMAIL";
            // 
            // popupMenu1
            // 
            this.popupMenu1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.dxBarBtnNewUser),
            new DevExpress.XtraBars.LinkPersistInfo(this.dxBarBtnEditUser)});
            this.popupMenu1.Manager = this.barManager1;
            this.popupMenu1.Name = "popupMenu1";
            // 
            // ErpUsers
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(979, 486);
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "ErpUsers";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "User Setup";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.ErpUsers_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cxGrid1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cxGrid1DBTableView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repChkLinkComp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cxGridUsers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cxGridUsersDBTV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repHyperLinkEMAIL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraGrid.GridControl cxGrid1;
        private DevExpress.XtraGrid.Views.Grid.GridView cxGrid1DBTableView1;
        private DevExpress.XtraGrid.Columns.GridColumn cxGrid1DBTableView1COMPANY_LINK;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repChkLinkComp;
        private DevExpress.XtraGrid.Columns.GridColumn cxGrid1DBTableView1COMPANY_ID;
        private DevExpress.XtraGrid.Columns.GridColumn cxGrid1DBTableView1COMPANY_CODE;
        private DevExpress.XtraGrid.Columns.GridColumn cxGrid1DBTableView1NAME;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem dxBarBtnNewUser;
        private DevExpress.XtraBars.BarButtonItem dxBarBtnEditUser;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraGrid.GridControl cxGridUsers;
        private DevExpress.XtraGrid.Views.Grid.GridView cxGridUsersDBTV;
        private DevExpress.XtraGrid.Columns.GridColumn cxGridUsersDBTVUSERNAME;
        private DevExpress.XtraGrid.Columns.GridColumn cxGridUsersDBTVDISPLAYNAME;
        private DevExpress.XtraGrid.Columns.GridColumn cxGridUsersDBTVCODA_USERNAME;
        private DevExpress.XtraGrid.Columns.GridColumn cxGridUsersDBTVEMAIL;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit repHyperLinkEMAIL;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraBars.PopupMenu popupMenu1;
    }
}