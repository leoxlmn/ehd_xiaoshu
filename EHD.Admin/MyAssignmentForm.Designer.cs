namespace EHD.Admin {
    partial class MyAssignmentForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MyAssignmentForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkAutoRefresh = new System.Windows.Forms.CheckBox();
            this.btnSearchName = new System.Windows.Forms.Button();
            this.txtSearchName = new System.Windows.Forms.TextBox();
            this.drpListType = new System.Windows.Forms.ComboBox();
            this.btnSmallerFont = new System.Windows.Forms.Button();
            this.lstIcons = new System.Windows.Forms.ImageList(this.components);
            this.btnBiggerFont = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.spMain = new System.Windows.Forms.SplitContainer();
            this.pagerInitial = new EHD.Admin.UCPager();
            this.picInitTreatmentListLabel = new System.Windows.Forms.PictureBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.grdInitialTreatment = new System.Windows.Forms.DataGridView();
            this.PatientButton = new System.Windows.Forms.DataGridViewImageColumn();
            this.EditInitialTreatmentButton = new System.Windows.Forms.DataGridViewImageColumn();
            this.InitialTreatmentComplete = new System.Windows.Forms.DataGridViewImageColumn();
            this.pagerFollowup = new EHD.Admin.UCPager();
            this.panel2 = new System.Windows.Forms.Panel();
            this.grdFollowUpTreatment = new System.Windows.Forms.DataGridView();
            this.FollowUpPatientButton = new System.Windows.Forms.DataGridViewImageColumn();
            this.InitialTreatmentButton = new System.Windows.Forms.DataGridViewImageColumn();
            this.EditFollowUpTreatmentButton = new System.Windows.Forms.DataGridViewImageColumn();
            this.FollowUpTreatmentComplete = new System.Windows.Forms.DataGridViewImageColumn();
            this.picFollowUpTreatmentListLabel = new System.Windows.Forms.PictureBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsLabelAssignmentFor = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsLabelLogonUser = new System.Windows.Forms.ToolStripStatusLabel();
            this.timerReload = new System.Windows.Forms.Timer(this.components);
            this.BottomToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.TopToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.RightToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.LeftToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.ContentPanel = new System.Windows.Forms.ToolStripContentPanel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spMain)).BeginInit();
            this.spMain.Panel1.SuspendLayout();
            this.spMain.Panel2.SuspendLayout();
            this.spMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picInitTreatmentListLabel)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdInitialTreatment)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdFollowUpTreatment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picFollowUpTreatmentListLabel)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.chkAutoRefresh);
            this.panel1.Controls.Add(this.btnSearchName);
            this.panel1.Controls.Add(this.txtSearchName);
            this.panel1.Controls.Add(this.drpListType);
            this.panel1.Controls.Add(this.btnSmallerFont);
            this.panel1.Controls.Add(this.btnBiggerFont);
            this.panel1.Controls.Add(this.btnExit);
            this.panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(709, 38);
            this.panel1.TabIndex = 0;
            // 
            // chkAutoRefresh
            // 
            this.chkAutoRefresh.AutoSize = true;
            this.chkAutoRefresh.Checked = true;
            this.chkAutoRefresh.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoRefresh.Location = new System.Drawing.Point(316, 9);
            this.chkAutoRefresh.Name = "chkAutoRefresh";
            this.chkAutoRefresh.Size = new System.Drawing.Size(136, 21);
            this.chkAutoRefresh.TabIndex = 7;
            this.chkAutoRefresh.Text = "Auto Refresh List";
            this.toolTip1.SetToolTip(this.chkAutoRefresh, "Auto Refresh list every 10 seconds.");
            this.chkAutoRefresh.UseVisualStyleBackColor = true;
            // 
            // btnSearchName
            // 
            this.btnSearchName.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearchName.BackgroundImage")));
            this.btnSearchName.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearchName.Location = new System.Drawing.Point(269, 6);
            this.btnSearchName.Margin = new System.Windows.Forms.Padding(0);
            this.btnSearchName.Name = "btnSearchName";
            this.btnSearchName.Size = new System.Drawing.Size(26, 26);
            this.btnSearchName.TabIndex = 10;
            this.btnSearchName.TabStop = false;
            this.toolTip1.SetToolTip(this.btnSearchName, "Filter list by patient Name / File Number");
            this.btnSearchName.UseVisualStyleBackColor = true;
            this.btnSearchName.Click += new System.EventHandler(this.btnSearchName_Click);
            // 
            // txtSearchName
            // 
            this.txtSearchName.Location = new System.Drawing.Point(166, 7);
            this.txtSearchName.MaxLength = 50;
            this.txtSearchName.Name = "txtSearchName";
            this.txtSearchName.Size = new System.Drawing.Size(100, 23);
            this.txtSearchName.TabIndex = 9;
            this.toolTip1.SetToolTip(this.txtSearchName, "Entry First Name / Last Name / Full Name / File Number");
            this.txtSearchName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearchName_KeyPress);
            // 
            // drpListType
            // 
            this.drpListType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.drpListType.FormattingEnabled = true;
            this.drpListType.Items.AddRange(new object[] {
            "Today\'s List",
            "Up Coming List",
            "Full List"});
            this.drpListType.Location = new System.Drawing.Point(28, 6);
            this.drpListType.Name = "drpListType";
            this.drpListType.Size = new System.Drawing.Size(121, 24);
            this.drpListType.TabIndex = 6;
            this.drpListType.SelectedIndexChanged += new System.EventHandler(this.drpListType_SelectedIndexChanged);
            // 
            // btnSmallerFont
            // 
            this.btnSmallerFont.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSmallerFont.AutoSize = true;
            this.btnSmallerFont.FlatAppearance.BorderSize = 0;
            this.btnSmallerFont.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSmallerFont.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSmallerFont.ImageIndex = 12;
            this.btnSmallerFont.ImageList = this.lstIcons;
            this.btnSmallerFont.Location = new System.Drawing.Point(576, 2);
            this.btnSmallerFont.Name = "btnSmallerFont";
            this.btnSmallerFont.Size = new System.Drawing.Size(27, 32);
            this.btnSmallerFont.TabIndex = 5;
            this.btnSmallerFont.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSmallerFont.UseVisualStyleBackColor = true;
            this.btnSmallerFont.Click += new System.EventHandler(this.btnSmallerFont_Click);
            // 
            // lstIcons
            // 
            this.lstIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("lstIcons.ImageStream")));
            this.lstIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.lstIcons.Images.SetKeyName(0, "Delete");
            this.lstIcons.Images.SetKeyName(1, "Female");
            this.lstIcons.Images.SetKeyName(2, "Male");
            this.lstIcons.Images.SetKeyName(3, "Edit");
            this.lstIcons.Images.SetKeyName(4, "InitialTreatment");
            this.lstIcons.Images.SetKeyName(5, "FollowUpTreatment");
            this.lstIcons.Images.SetKeyName(6, "ViewDetail");
            this.lstIcons.Images.SetKeyName(7, "Remove");
            this.lstIcons.Images.SetKeyName(8, "Transparent");
            this.lstIcons.Images.SetKeyName(9, "Print");
            this.lstIcons.Images.SetKeyName(10, "Person");
            this.lstIcons.Images.SetKeyName(11, "BiggerFont");
            this.lstIcons.Images.SetKeyName(12, "SmallerFont");
            this.lstIcons.Images.SetKeyName(13, "Complete");
            // 
            // btnBiggerFont
            // 
            this.btnBiggerFont.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBiggerFont.AutoSize = true;
            this.btnBiggerFont.FlatAppearance.BorderSize = 0;
            this.btnBiggerFont.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBiggerFont.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBiggerFont.ImageIndex = 11;
            this.btnBiggerFont.ImageList = this.lstIcons;
            this.btnBiggerFont.Location = new System.Drawing.Point(543, 2);
            this.btnBiggerFont.Name = "btnBiggerFont";
            this.btnBiggerFont.Size = new System.Drawing.Size(27, 32);
            this.btnBiggerFont.TabIndex = 4;
            this.btnBiggerFont.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnBiggerFont.UseVisualStyleBackColor = true;
            this.btnBiggerFont.Click += new System.EventHandler(this.btnBiggerFont_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.AutoSize = true;
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExit.Location = new System.Drawing.Point(609, 1);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(97, 32);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "Sign Out";
            this.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // spMain
            // 
            this.spMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.spMain.Location = new System.Drawing.Point(0, 34);
            this.spMain.MinimumSize = new System.Drawing.Size(300, 200);
            this.spMain.Name = "spMain";
            this.spMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // spMain.Panel1
            // 
            this.spMain.Panel1.Controls.Add(this.pagerInitial);
            this.spMain.Panel1.Controls.Add(this.picInitTreatmentListLabel);
            this.spMain.Panel1.Controls.Add(this.panel3);
            this.spMain.Panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.spMain.Panel1MinSize = 120;
            // 
            // spMain.Panel2
            // 
            this.spMain.Panel2.Controls.Add(this.pagerFollowup);
            this.spMain.Panel2.Controls.Add(this.panel2);
            this.spMain.Panel2.Controls.Add(this.picFollowUpTreatmentListLabel);
            this.spMain.Panel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.spMain.Panel2MinSize = 130;
            this.spMain.Size = new System.Drawing.Size(706, 419);
            this.spMain.SplitterDistance = 194;
            this.spMain.TabIndex = 1;
            // 
            // pagerInitial
            // 
            this.pagerInitial.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pagerInitial.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pagerInitial.Location = new System.Drawing.Point(297, 165);
            this.pagerInitial.Margin = new System.Windows.Forms.Padding(0);
            this.pagerInitial.Name = "pagerInitial";
            this.pagerInitial.Page = 1;
            this.pagerInitial.PageSize = 10;
            this.pagerInitial.Size = new System.Drawing.Size(400, 27);
            this.pagerInitial.TabIndex = 6;
            this.pagerInitial.TotalPage = 0;
            // 
            // picInitTreatmentListLabel
            // 
            this.picInitTreatmentListLabel.Image = ((System.Drawing.Image)(resources.GetObject("picInitTreatmentListLabel.Image")));
            this.picInitTreatmentListLabel.Location = new System.Drawing.Point(0, 3);
            this.picInitTreatmentListLabel.Name = "picInitTreatmentListLabel";
            this.picInitTreatmentListLabel.Size = new System.Drawing.Size(27, 115);
            this.picInitTreatmentListLabel.TabIndex = 5;
            this.picInitTreatmentListLabel.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Controls.Add(this.grdInitialTreatment);
            this.panel3.Location = new System.Drawing.Point(28, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(678, 159);
            this.panel3.TabIndex = 1;
            // 
            // grdInitialTreatment
            // 
            this.grdInitialTreatment.AllowUserToAddRows = false;
            this.grdInitialTreatment.AllowUserToDeleteRows = false;
            this.grdInitialTreatment.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.grdInitialTreatment.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdInitialTreatment.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PatientButton,
            this.EditInitialTreatmentButton,
            this.InitialTreatmentComplete});
            this.grdInitialTreatment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdInitialTreatment.Location = new System.Drawing.Point(0, 0);
            this.grdInitialTreatment.MultiSelect = false;
            this.grdInitialTreatment.Name = "grdInitialTreatment";
            this.grdInitialTreatment.ReadOnly = true;
            this.grdInitialTreatment.RowHeadersVisible = false;
            this.grdInitialTreatment.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdInitialTreatment.Size = new System.Drawing.Size(678, 159);
            this.grdInitialTreatment.TabIndex = 0;
            this.grdInitialTreatment.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdInitialTreatment_CellClick);
            this.grdInitialTreatment.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.grdInitialTreatment_CellMouseDoubleClick);
            this.grdInitialTreatment.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.grdInitialTreatment_DataBindingComplete);
            this.grdInitialTreatment.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.grdInitialTreatment_DataError);
            // 
            // PatientButton
            // 
            this.PatientButton.Frozen = true;
            this.PatientButton.HeaderText = "";
            this.PatientButton.MinimumWidth = 25;
            this.PatientButton.Name = "PatientButton";
            this.PatientButton.ReadOnly = true;
            this.PatientButton.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.PatientButton.Width = 25;
            // 
            // EditInitialTreatmentButton
            // 
            this.EditInitialTreatmentButton.Frozen = true;
            this.EditInitialTreatmentButton.HeaderText = "";
            this.EditInitialTreatmentButton.MinimumWidth = 25;
            this.EditInitialTreatmentButton.Name = "EditInitialTreatmentButton";
            this.EditInitialTreatmentButton.ReadOnly = true;
            this.EditInitialTreatmentButton.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.EditInitialTreatmentButton.Width = 25;
            // 
            // InitialTreatmentComplete
            // 
            this.InitialTreatmentComplete.Frozen = true;
            this.InitialTreatmentComplete.HeaderText = "";
            this.InitialTreatmentComplete.MinimumWidth = 25;
            this.InitialTreatmentComplete.Name = "InitialTreatmentComplete";
            this.InitialTreatmentComplete.ReadOnly = true;
            this.InitialTreatmentComplete.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.InitialTreatmentComplete.Width = 25;
            // 
            // pagerFollowup
            // 
            this.pagerFollowup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pagerFollowup.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pagerFollowup.Location = new System.Drawing.Point(297, 191);
            this.pagerFollowup.Margin = new System.Windows.Forms.Padding(0);
            this.pagerFollowup.Name = "pagerFollowup";
            this.pagerFollowup.Page = 1;
            this.pagerFollowup.PageSize = 10;
            this.pagerFollowup.Size = new System.Drawing.Size(400, 27);
            this.pagerFollowup.TabIndex = 7;
            this.pagerFollowup.TotalPage = 0;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.grdFollowUpTreatment);
            this.panel2.Location = new System.Drawing.Point(28, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(678, 185);
            this.panel2.TabIndex = 6;
            // 
            // grdFollowUpTreatment
            // 
            this.grdFollowUpTreatment.AllowUserToAddRows = false;
            this.grdFollowUpTreatment.AllowUserToDeleteRows = false;
            this.grdFollowUpTreatment.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.grdFollowUpTreatment.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdFollowUpTreatment.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FollowUpPatientButton,
            this.InitialTreatmentButton,
            this.EditFollowUpTreatmentButton,
            this.FollowUpTreatmentComplete});
            this.grdFollowUpTreatment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdFollowUpTreatment.Location = new System.Drawing.Point(0, 0);
            this.grdFollowUpTreatment.MultiSelect = false;
            this.grdFollowUpTreatment.Name = "grdFollowUpTreatment";
            this.grdFollowUpTreatment.ReadOnly = true;
            this.grdFollowUpTreatment.RowHeadersVisible = false;
            this.grdFollowUpTreatment.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdFollowUpTreatment.Size = new System.Drawing.Size(678, 185);
            this.grdFollowUpTreatment.TabIndex = 0;
            this.grdFollowUpTreatment.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdFollowUpTreatment_CellClick);
            this.grdFollowUpTreatment.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.grdFollowUpTreatment_CellMouseDoubleClick);
            this.grdFollowUpTreatment.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.grdFollowUpTreatment_DataBindingComplete);
            this.grdFollowUpTreatment.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.grdFollowUpTreatment_DataError);
            // 
            // FollowUpPatientButton
            // 
            this.FollowUpPatientButton.Frozen = true;
            this.FollowUpPatientButton.HeaderText = "";
            this.FollowUpPatientButton.MinimumWidth = 25;
            this.FollowUpPatientButton.Name = "FollowUpPatientButton";
            this.FollowUpPatientButton.ReadOnly = true;
            this.FollowUpPatientButton.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.FollowUpPatientButton.Width = 25;
            // 
            // InitialTreatmentButton
            // 
            this.InitialTreatmentButton.Frozen = true;
            this.InitialTreatmentButton.HeaderText = "";
            this.InitialTreatmentButton.MinimumWidth = 25;
            this.InitialTreatmentButton.Name = "InitialTreatmentButton";
            this.InitialTreatmentButton.ReadOnly = true;
            this.InitialTreatmentButton.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.InitialTreatmentButton.Width = 25;
            // 
            // EditFollowUpTreatmentButton
            // 
            this.EditFollowUpTreatmentButton.Frozen = true;
            this.EditFollowUpTreatmentButton.HeaderText = "";
            this.EditFollowUpTreatmentButton.MinimumWidth = 25;
            this.EditFollowUpTreatmentButton.Name = "EditFollowUpTreatmentButton";
            this.EditFollowUpTreatmentButton.ReadOnly = true;
            this.EditFollowUpTreatmentButton.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.EditFollowUpTreatmentButton.Width = 25;
            // 
            // FollowUpTreatmentComplete
            // 
            this.FollowUpTreatmentComplete.Frozen = true;
            this.FollowUpTreatmentComplete.HeaderText = "";
            this.FollowUpTreatmentComplete.MinimumWidth = 25;
            this.FollowUpTreatmentComplete.Name = "FollowUpTreatmentComplete";
            this.FollowUpTreatmentComplete.ReadOnly = true;
            this.FollowUpTreatmentComplete.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.FollowUpTreatmentComplete.Width = 25;
            // 
            // picFollowUpTreatmentListLabel
            // 
            this.picFollowUpTreatmentListLabel.Image = ((System.Drawing.Image)(resources.GetObject("picFollowUpTreatmentListLabel.Image")));
            this.picFollowUpTreatmentListLabel.Location = new System.Drawing.Point(0, 3);
            this.picFollowUpTreatmentListLabel.Name = "picFollowUpTreatmentListLabel";
            this.picFollowUpTreatmentListLabel.Size = new System.Drawing.Size(27, 134);
            this.picFollowUpTreatmentListLabel.TabIndex = 5;
            this.picFollowUpTreatmentListLabel.TabStop = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.tsLabelAssignmentFor,
            this.toolStripStatusLabel3,
            this.tsLabelLogonUser});
            this.statusStrip1.Location = new System.Drawing.Point(0, 456);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(706, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(114, 17);
            this.toolStripStatusLabel1.Text = "Assignment List For:";
            // 
            // tsLabelAssignmentFor
            // 
            this.tsLabelAssignmentFor.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.tsLabelAssignmentFor.Name = "tsLabelAssignmentFor";
            this.tsLabelAssignmentFor.Size = new System.Drawing.Size(93, 17);
            this.tsLabelAssignmentFor.Text = "Assignment For";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(70, 17);
            this.toolStripStatusLabel3.Text = "Logon User:";
            // 
            // tsLabelLogonUser
            // 
            this.tsLabelLogonUser.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.tsLabelLogonUser.Name = "tsLabelLogonUser";
            this.tsLabelLogonUser.Size = new System.Drawing.Size(70, 17);
            this.tsLabelLogonUser.Text = "Logon User";
            // 
            // timerReload
            // 
            this.timerReload.Enabled = true;
            this.timerReload.Interval = 10000;
            this.timerReload.Tick += new System.EventHandler(this.timerReload_Tick);
            // 
            // BottomToolStripPanel
            // 
            this.BottomToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.BottomToolStripPanel.Name = "BottomToolStripPanel";
            this.BottomToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.BottomToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.BottomToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // TopToolStripPanel
            // 
            this.TopToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.TopToolStripPanel.Name = "TopToolStripPanel";
            this.TopToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.TopToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.TopToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // RightToolStripPanel
            // 
            this.RightToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.RightToolStripPanel.Name = "RightToolStripPanel";
            this.RightToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.RightToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.RightToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // LeftToolStripPanel
            // 
            this.LeftToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.LeftToolStripPanel.Name = "LeftToolStripPanel";
            this.LeftToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.LeftToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.LeftToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // ContentPanel
            // 
            this.ContentPanel.Size = new System.Drawing.Size(125, 150);
            // 
            // MyAssignmentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(706, 478);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.spMain);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MyAssignmentForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "My Assignments";
            this.Load += new System.EventHandler(this.MyAssignmentForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.spMain.Panel1.ResumeLayout(false);
            this.spMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spMain)).EndInit();
            this.spMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picInitTreatmentListLabel)).EndInit();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdInitialTreatment)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdFollowUpTreatment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picFollowUpTreatmentListLabel)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer spMain;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox picInitTreatmentListLabel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox picFollowUpTreatmentListLabel;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.DataGridView grdInitialTreatment;
        private System.Windows.Forms.DataGridView grdFollowUpTreatment;
        private System.Windows.Forms.ImageList lstIcons;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel tsLabelAssignmentFor;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel tsLabelLogonUser;
        private System.Windows.Forms.Timer timerReload;
        private System.Windows.Forms.DataGridViewImageColumn FollowUpPatientButton;
        private System.Windows.Forms.DataGridViewImageColumn InitialTreatmentButton;
        private System.Windows.Forms.DataGridViewImageColumn EditFollowUpTreatmentButton;
        private System.Windows.Forms.DataGridViewImageColumn FollowUpTreatmentComplete;
        private System.Windows.Forms.ToolStripPanel BottomToolStripPanel;
        private System.Windows.Forms.ToolStripPanel TopToolStripPanel;
        private System.Windows.Forms.ToolStripPanel RightToolStripPanel;
        private System.Windows.Forms.ToolStripPanel LeftToolStripPanel;
        private System.Windows.Forms.ToolStripContentPanel ContentPanel;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnBiggerFont;
        private System.Windows.Forms.Button btnSmallerFont;
        private System.Windows.Forms.DataGridViewImageColumn PatientButton;
        private System.Windows.Forms.DataGridViewImageColumn EditInitialTreatmentButton;
        private System.Windows.Forms.DataGridViewImageColumn InitialTreatmentComplete;
        private UCPager pagerInitial;
        private UCPager pagerFollowup;
        private System.Windows.Forms.ComboBox drpListType;
        private System.Windows.Forms.Button btnSearchName;
        private System.Windows.Forms.TextBox txtSearchName;
        private System.Windows.Forms.CheckBox chkAutoRefresh;
        private System.Windows.Forms.ToolTip toolTip1;

    }
}