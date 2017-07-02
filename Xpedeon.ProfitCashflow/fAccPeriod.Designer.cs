namespace Xpedeon.ProfitCashflow
{
    partial class fAccPeriod
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fAccPeriod));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.cxDBTreeList1 = new DevExpress.XtraTreeList.TreeList();
            this.cxDBTreeList1cxDBTreeListCOMPANY_ID = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.repLookUpEditCompany = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.cxDBTreeList1cxDBTreeListCODE = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.repCode = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.cxDBTreeList1cxDBTreeListNAME = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.cxDBTreeList1cxDBTreeListFROM = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.repFromDateEdit = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.cxDBTreeList1cxDBTreeListTO = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.repEndDateEdit = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.cxDBTreeList1cxDBTreeListSTATUS = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.dxBarBtnAddNode = new DevExpress.XtraBars.BarButtonItem();
            this.dxBarBtnDelNode = new DevExpress.XtraBars.BarButtonItem();
            this.dxBarBtnsave = new DevExpress.XtraBars.BarButtonItem();
            this.dxBarBtnCancel = new DevExpress.XtraBars.BarButtonItem();
            this.dxBarBtnExp = new DevExpress.XtraBars.BarButtonItem();
            this.dxBarBtnCollp = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem6 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem7 = new DevExpress.XtraBars.BarButtonItem();
            this.popupMenu1 = new DevExpress.XtraBars.PopupMenu(this.components);
            this.splashScreenManager1 = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::Xpedeon.ProfitCashflow.WaitForm1), true, true);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cxDBTreeList1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repLookUpEditCompany)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repFromDateEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repFromDateEdit.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repEndDateEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repEndDateEdit.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.cxDBTreeList1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 35);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(663, 424);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // cxDBTreeList1
            // 
            this.cxDBTreeList1.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.cxDBTreeList1cxDBTreeListCOMPANY_ID,
            this.cxDBTreeList1cxDBTreeListCODE,
            this.cxDBTreeList1cxDBTreeListNAME,
            this.cxDBTreeList1cxDBTreeListFROM,
            this.cxDBTreeList1cxDBTreeListTO,
            this.cxDBTreeList1cxDBTreeListSTATUS});
            this.cxDBTreeList1.Cursor = System.Windows.Forms.Cursors.Default;
            this.cxDBTreeList1.KeyFieldName = "COMP_PERIOD_CODE";
            this.cxDBTreeList1.Location = new System.Drawing.Point(2, 2);
            this.cxDBTreeList1.Name = "cxDBTreeList1";
            this.cxDBTreeList1.OptionsBehavior.EnableFiltering = true;
            this.cxDBTreeList1.OptionsBehavior.ImmediateEditor = false;
            this.cxDBTreeList1.OptionsBehavior.PopulateServiceColumns = true;
            this.cxDBTreeList1.OptionsCustomization.AllowColumnMoving = false;
            this.cxDBTreeList1.OptionsCustomization.AllowQuickHideColumns = false;
            this.cxDBTreeList1.OptionsNavigation.AutoMoveRowFocus = true;
            this.cxDBTreeList1.OptionsNavigation.UseTabKey = true;
            this.cxDBTreeList1.OptionsSelection.UseIndicatorForSelection = true;
            this.cxDBTreeList1.OptionsView.EnableAppearanceEvenRow = true;
            this.cxDBTreeList1.OptionsView.EnableAppearanceOddRow = true;
            this.cxDBTreeList1.OptionsView.ShowAutoFilterRow = true;
            this.cxDBTreeList1.ParentFieldName = "COMP_MAJ_PERIOD_CODE";
            this.cxDBTreeList1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repFromDateEdit,
            this.repEndDateEdit});
            this.cxDBTreeList1.Size = new System.Drawing.Size(659, 420);
            this.cxDBTreeList1.TabIndex = 4;
            this.cxDBTreeList1.BeforeFocusNode += new DevExpress.XtraTreeList.BeforeFocusNodeEventHandler(this.cxDBTreeList1_BeforeFocusNode);
            this.cxDBTreeList1.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.cxDBTreeList1_FocusedNodeChanged);
            this.cxDBTreeList1.InvalidNodeException += new DevExpress.XtraTreeList.InvalidNodeExceptionEventHandler(this.cxDBTreeList1_InvalidNodeException);
            this.cxDBTreeList1.ValidateNode += new DevExpress.XtraTreeList.ValidateNodeEventHandler(this.cxDBTreeList1_ValidateNode);
            this.cxDBTreeList1.CellValueChanging += new DevExpress.XtraTreeList.CellValueChangedEventHandler(this.cxDBTreeList1_CellValueChanging);
            this.cxDBTreeList1.CellValueChanged += new DevExpress.XtraTreeList.CellValueChangedEventHandler(this.cxDBTreeList1_CellValueChanged);
            this.cxDBTreeList1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cxDBTreeList1_KeyDown);
            this.cxDBTreeList1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.cxDBTreeList1_MouseUp);
            // 
            // cxDBTreeList1cxDBTreeListCOMPANY_ID
            // 
            this.cxDBTreeList1cxDBTreeListCOMPANY_ID.Caption = "Company";
            this.cxDBTreeList1cxDBTreeListCOMPANY_ID.ColumnEdit = this.repLookUpEditCompany;
            this.cxDBTreeList1cxDBTreeListCOMPANY_ID.FieldName = "COMPANY_ID";
            this.cxDBTreeList1cxDBTreeListCOMPANY_ID.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText;
            this.cxDBTreeList1cxDBTreeListCOMPANY_ID.Name = "cxDBTreeList1cxDBTreeListCOMPANY_ID";
            this.cxDBTreeList1cxDBTreeListCOMPANY_ID.OptionsColumn.AllowEdit = false;
            this.cxDBTreeList1cxDBTreeListCOMPANY_ID.OptionsColumn.AllowMoveToCustomizationForm = false;
            this.cxDBTreeList1cxDBTreeListCOMPANY_ID.SortOrder = System.Windows.Forms.SortOrder.Ascending;
            this.cxDBTreeList1cxDBTreeListCOMPANY_ID.Visible = true;
            this.cxDBTreeList1cxDBTreeListCOMPANY_ID.VisibleIndex = 0;
            this.cxDBTreeList1cxDBTreeListCOMPANY_ID.Width = 195;
            // 
            // repLookUpEditCompany
            // 
            this.repLookUpEditCompany.AutoHeight = false;
            this.repLookUpEditCompany.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repLookUpEditCompany.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("COMPANY_ID", "CompanyId", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NAME", "Company", 20, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.repLookUpEditCompany.DisplayMember = "NAME";
            this.repLookUpEditCompany.Name = "repLookUpEditCompany";
            this.repLookUpEditCompany.NullText = "";
            this.repLookUpEditCompany.ReadOnly = true;
            this.repLookUpEditCompany.ValueMember = "COMPANY_ID";
            // 
            // cxDBTreeList1cxDBTreeListCODE
            // 
            this.cxDBTreeList1cxDBTreeListCODE.Caption = "Financial Year / Sub Period Code";
            this.cxDBTreeList1cxDBTreeListCODE.ColumnEdit = this.repCode;
            this.cxDBTreeList1cxDBTreeListCODE.FieldName = "PERIOD_CODE";
            this.cxDBTreeList1cxDBTreeListCODE.Name = "cxDBTreeList1cxDBTreeListCODE";
            this.cxDBTreeList1cxDBTreeListCODE.OptionsColumn.AllowEdit = false;
            this.cxDBTreeList1cxDBTreeListCODE.OptionsColumn.AllowMoveToCustomizationForm = false;
            this.cxDBTreeList1cxDBTreeListCODE.OptionsColumn.AllowSort = false;
            this.cxDBTreeList1cxDBTreeListCODE.Visible = true;
            this.cxDBTreeList1cxDBTreeListCODE.VisibleIndex = 1;
            // 
            // repCode
            // 
            this.repCode.AutoHeight = false;
            this.repCode.Mask.EditMask = "D";
            this.repCode.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.repCode.Name = "repCode";
            // 
            // cxDBTreeList1cxDBTreeListNAME
            // 
            this.cxDBTreeList1cxDBTreeListNAME.Caption = "Financial Year / Sub Period Name";
            this.cxDBTreeList1cxDBTreeListNAME.FieldName = "PERIOD_NAME";
            this.cxDBTreeList1cxDBTreeListNAME.Name = "cxDBTreeList1cxDBTreeListNAME";
            this.cxDBTreeList1cxDBTreeListNAME.OptionsColumn.AllowMoveToCustomizationForm = false;
            this.cxDBTreeList1cxDBTreeListNAME.OptionsColumn.AllowSort = false;
            this.cxDBTreeList1cxDBTreeListNAME.Visible = true;
            this.cxDBTreeList1cxDBTreeListNAME.VisibleIndex = 2;
            this.cxDBTreeList1cxDBTreeListNAME.Width = 195;
            // 
            // cxDBTreeList1cxDBTreeListFROM
            // 
            this.cxDBTreeList1cxDBTreeListFROM.Caption = "Start Date";
            this.cxDBTreeList1cxDBTreeListFROM.ColumnEdit = this.repFromDateEdit;
            this.cxDBTreeList1cxDBTreeListFROM.FieldName = "START_DATE";
            this.cxDBTreeList1cxDBTreeListFROM.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText;
            this.cxDBTreeList1cxDBTreeListFROM.Format.FormatString = "d";
            this.cxDBTreeList1cxDBTreeListFROM.Format.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.cxDBTreeList1cxDBTreeListFROM.Name = "cxDBTreeList1cxDBTreeListFROM";
            this.cxDBTreeList1cxDBTreeListFROM.OptionsColumn.AllowEdit = false;
            this.cxDBTreeList1cxDBTreeListFROM.OptionsColumn.AllowMoveToCustomizationForm = false;
            this.cxDBTreeList1cxDBTreeListFROM.SortOrder = System.Windows.Forms.SortOrder.Ascending;
            this.cxDBTreeList1cxDBTreeListFROM.Visible = true;
            this.cxDBTreeList1cxDBTreeListFROM.VisibleIndex = 3;
            // 
            // repFromDateEdit
            // 
            this.repFromDateEdit.AutoHeight = false;
            this.repFromDateEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repFromDateEdit.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repFromDateEdit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            this.repFromDateEdit.Mask.UseMaskAsDisplayFormat = true;
            this.repFromDateEdit.Name = "repFromDateEdit";
            // 
            // cxDBTreeList1cxDBTreeListTO
            // 
            this.cxDBTreeList1cxDBTreeListTO.Caption = "End Date";
            this.cxDBTreeList1cxDBTreeListTO.ColumnEdit = this.repEndDateEdit;
            this.cxDBTreeList1cxDBTreeListTO.FieldName = "END_DATE";
            this.cxDBTreeList1cxDBTreeListTO.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText;
            this.cxDBTreeList1cxDBTreeListTO.Format.FormatString = "d";
            this.cxDBTreeList1cxDBTreeListTO.Format.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.cxDBTreeList1cxDBTreeListTO.Name = "cxDBTreeList1cxDBTreeListTO";
            this.cxDBTreeList1cxDBTreeListTO.OptionsColumn.AllowMoveToCustomizationForm = false;
            this.cxDBTreeList1cxDBTreeListTO.OptionsColumn.AllowSort = false;
            this.cxDBTreeList1cxDBTreeListTO.Visible = true;
            this.cxDBTreeList1cxDBTreeListTO.VisibleIndex = 4;
            // 
            // repEndDateEdit
            // 
            this.repEndDateEdit.AutoHeight = false;
            this.repEndDateEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repEndDateEdit.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repEndDateEdit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            this.repEndDateEdit.Mask.UseMaskAsDisplayFormat = true;
            this.repEndDateEdit.Name = "repEndDateEdit";
            // 
            // cxDBTreeList1cxDBTreeListSTATUS
            // 
            this.cxDBTreeList1cxDBTreeListSTATUS.Caption = "Status";
            this.cxDBTreeList1cxDBTreeListSTATUS.FieldName = "STATUS";
            this.cxDBTreeList1cxDBTreeListSTATUS.Name = "cxDBTreeList1cxDBTreeListSTATUS";
            this.cxDBTreeList1cxDBTreeListSTATUS.OptionsColumn.AllowSort = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(663, 424);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.cxDBTreeList1;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(663, 424);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
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
            this.dxBarBtnAddNode,
            this.dxBarBtnDelNode,
            this.barButtonItem3,
            this.dxBarBtnsave,
            this.dxBarBtnCancel,
            this.barButtonItem6,
            this.barButtonItem7,
            this.dxBarBtnExp,
            this.dxBarBtnCollp});
            this.barManager1.MaxItemId = 9;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.dxBarBtnAddNode),
            new DevExpress.XtraBars.LinkPersistInfo(this.dxBarBtnDelNode, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.dxBarBtnsave, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.dxBarBtnCancel, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.dxBarBtnExp, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.dxBarBtnCollp, true)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.Text = "Tools";
            // 
            // dxBarBtnAddNode
            // 
            this.dxBarBtnAddNode.Caption = "Add Period";
            this.dxBarBtnAddNode.Id = 0;
            this.dxBarBtnAddNode.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("dxBarBtnAddNode.ImageOptions.Image")));
            this.dxBarBtnAddNode.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("dxBarBtnAddNode.ImageOptions.LargeImage")));
            this.dxBarBtnAddNode.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.Insert);
            this.dxBarBtnAddNode.Name = "dxBarBtnAddNode";
            this.dxBarBtnAddNode.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.dxBarBtnAddNode_ItemClick);
            // 
            // dxBarBtnDelNode
            // 
            this.dxBarBtnDelNode.Caption = "Delete Accounting Period";
            this.dxBarBtnDelNode.Id = 1;
            this.dxBarBtnDelNode.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("dxBarBtnDelNode.ImageOptions.Image")));
            this.dxBarBtnDelNode.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("dxBarBtnDelNode.ImageOptions.LargeImage")));
            this.dxBarBtnDelNode.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Delete));
            this.dxBarBtnDelNode.Name = "dxBarBtnDelNode";
            this.dxBarBtnDelNode.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.dxBarBtnDelNodeClick_ItemClick);
            // 
            // dxBarBtnsave
            // 
            this.dxBarBtnsave.Caption = "Save";
            this.dxBarBtnsave.Id = 3;
            this.dxBarBtnsave.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("dxBarBtnsave.ImageOptions.Image")));
            this.dxBarBtnsave.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("dxBarBtnsave.ImageOptions.LargeImage")));
            this.dxBarBtnsave.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S));
            this.dxBarBtnsave.Name = "dxBarBtnsave";
            this.dxBarBtnsave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.dxBarBtnSave_ItemClick);
            // 
            // dxBarBtnCancel
            // 
            this.dxBarBtnCancel.Caption = "Cancel";
            this.dxBarBtnCancel.Id = 4;
            this.dxBarBtnCancel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("dxBarBtnCancel.ImageOptions.Image")));
            this.dxBarBtnCancel.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("dxBarBtnCancel.ImageOptions.LargeImage")));
            this.dxBarBtnCancel.Name = "dxBarBtnCancel";
            this.dxBarBtnCancel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.dxBarBtnCancelClick_ItemClick);
            // 
            // dxBarBtnExp
            // 
            this.dxBarBtnExp.Caption = "Expand All";
            this.dxBarBtnExp.Id = 7;
            this.dxBarBtnExp.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("dxBarBtnExp.ImageOptions.Image")));
            this.dxBarBtnExp.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("dxBarBtnExp.ImageOptions.LargeImage")));
            this.dxBarBtnExp.Name = "dxBarBtnExp";
            this.dxBarBtnExp.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.dxBarBtnExpClick_ItemClick);
            // 
            // dxBarBtnCollp
            // 
            this.dxBarBtnCollp.Caption = "Collapse All";
            this.dxBarBtnCollp.Id = 8;
            this.dxBarBtnCollp.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("dxBarBtnCollp.ImageOptions.Image")));
            this.dxBarBtnCollp.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("dxBarBtnCollp.ImageOptions.LargeImage")));
            this.dxBarBtnCollp.Name = "dxBarBtnCollp";
            this.dxBarBtnCollp.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.dxBarBtnCollpClick_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(663, 35);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 459);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(663, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 35);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 424);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(663, 35);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 424);
            // 
            // barButtonItem3
            // 
            this.barButtonItem3.Caption = "barButtonItem3";
            this.barButtonItem3.Id = 2;
            this.barButtonItem3.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem3.ImageOptions.Image")));
            this.barButtonItem3.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem3.ImageOptions.LargeImage")));
            this.barButtonItem3.Name = "barButtonItem3";
            // 
            // barButtonItem6
            // 
            this.barButtonItem6.Caption = "barButtonItem6";
            this.barButtonItem6.Id = 5;
            this.barButtonItem6.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem6.ImageOptions.Image")));
            this.barButtonItem6.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem6.ImageOptions.LargeImage")));
            this.barButtonItem6.Name = "barButtonItem6";
            // 
            // barButtonItem7
            // 
            this.barButtonItem7.Caption = "barButtonItem7";
            this.barButtonItem7.Id = 6;
            this.barButtonItem7.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem7.ImageOptions.Image")));
            this.barButtonItem7.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem7.ImageOptions.LargeImage")));
            this.barButtonItem7.Name = "barButtonItem7";
            // 
            // popupMenu1
            // 
            this.popupMenu1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.dxBarBtnAddNode),
            new DevExpress.XtraBars.LinkPersistInfo(this.dxBarBtnDelNode),
            new DevExpress.XtraBars.LinkPersistInfo(this.dxBarBtnsave),
            new DevExpress.XtraBars.LinkPersistInfo(this.dxBarBtnCancel),
            new DevExpress.XtraBars.LinkPersistInfo(this.dxBarBtnExp),
            new DevExpress.XtraBars.LinkPersistInfo(this.dxBarBtnCollp)});
            this.popupMenu1.Manager = this.barManager1;
            this.popupMenu1.Name = "popupMenu1";
            // 
            // splashScreenManager1
            // 
            this.splashScreenManager1.ClosingDelay = 500;
            // 
            // fAccPeriod
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.ClientSize = new System.Drawing.Size(663, 459);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "fAccPeriod";
            this.Text = "Accounting Periods";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.fAccPeriod_FormClosing);
            this.Load += new System.EventHandler(this.fAccPeriod_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.fAccPeriod_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cxDBTreeList1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repLookUpEditCompany)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repFromDateEdit.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repFromDateEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repEndDateEdit.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repEndDateEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraTreeList.TreeList cxDBTreeList1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn cxDBTreeList1cxDBTreeListCOMPANY_ID;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem dxBarBtnAddNode;
        private DevExpress.XtraTreeList.Columns.TreeListColumn cxDBTreeList1cxDBTreeListCODE;
        private DevExpress.XtraTreeList.Columns.TreeListColumn cxDBTreeList1cxDBTreeListNAME;
        private DevExpress.XtraTreeList.Columns.TreeListColumn cxDBTreeList1cxDBTreeListFROM;
        private DevExpress.XtraTreeList.Columns.TreeListColumn cxDBTreeList1cxDBTreeListTO;
        private DevExpress.XtraTreeList.Columns.TreeListColumn cxDBTreeList1cxDBTreeListSTATUS;
        private DevExpress.XtraBars.BarButtonItem dxBarBtnDelNode;
        private DevExpress.XtraBars.BarButtonItem dxBarBtnsave;
        private DevExpress.XtraBars.BarButtonItem dxBarBtnCancel;
        private DevExpress.XtraBars.BarButtonItem dxBarBtnExp;
        private DevExpress.XtraBars.BarButtonItem dxBarBtnCollp;
        private DevExpress.XtraBars.BarButtonItem barButtonItem3;
        private DevExpress.XtraBars.BarButtonItem barButtonItem6;
        private DevExpress.XtraBars.BarButtonItem barButtonItem7;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repLookUpEditCompany;
        private DevExpress.XtraBars.PopupMenu popupMenu1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repCode;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit repFromDateEdit;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit repEndDateEdit;
        public DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManager1;

    }
}