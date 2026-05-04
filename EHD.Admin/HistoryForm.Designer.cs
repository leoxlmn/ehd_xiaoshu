namespace EHD.Admin {
    partial class HistoryForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HistoryForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.drpEntityType = new System.Windows.Forms.ComboBox();
            this.drpOperator = new System.Windows.Forms.ComboBox();
            this.txtId = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dtFrom = new System.Windows.Forms.DateTimePicker();
            this.dtTo = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.splHistory = new System.Windows.Forms.SplitContainer();
            this.grdHistory = new System.Windows.Forms.DataGridView();
            this.pnlValue = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.txtNewValue = new System.Windows.Forms.RichTextBox();
            this.txtOldValue = new System.Windows.Forms.RichTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.numPageSize = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.btnLast = new System.Windows.Forms.Button();
            this.btnFirst = new System.Windows.Forms.Button();
            this.lblTotalPage = new System.Windows.Forms.Label();
            this.numPageNumber = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.splLR = new System.Windows.Forms.SplitContainer();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splHistory)).BeginInit();
            this.splHistory.Panel1.SuspendLayout();
            this.splHistory.Panel2.SuspendLayout();
            this.splHistory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdHistory)).BeginInit();
            this.pnlValue.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPageSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPageNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splLR)).BeginInit();
            this.splLR.Panel1.SuspendLayout();
            this.splLR.Panel2.SuspendLayout();
            this.splLR.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Search History By:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(401, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Operator:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(379, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Id:";
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnSearch.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.Location = new System.Drawing.Point(660, 8);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(105, 50);
            this.btnSearch.TabIndex = 7;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // drpEntityType
            // 
            this.drpEntityType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.drpEntityType.FormattingEnabled = true;
            this.drpEntityType.Location = new System.Drawing.Point(126, 8);
            this.drpEntityType.Name = "drpEntityType";
            this.drpEntityType.Size = new System.Drawing.Size(269, 23);
            this.drpEntityType.TabIndex = 1;
            // 
            // drpOperator
            // 
            this.drpOperator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.drpOperator.FormattingEnabled = true;
            this.drpOperator.Location = new System.Drawing.Point(468, 8);
            this.drpOperator.Name = "drpOperator";
            this.drpOperator.Size = new System.Drawing.Size(174, 23);
            this.drpOperator.TabIndex = 2;
            // 
            // txtId
            // 
            this.txtId.Location = new System.Drawing.Point(405, 36);
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(68, 21);
            this.txtId.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(13, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 15);
            this.label4.TabIndex = 14;
            this.label4.Text = "After:";
            // 
            // dtFrom
            // 
            this.dtFrom.Checked = false;
            this.dtFrom.CustomFormat = "MMM dd, yyyy";
            this.dtFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtFrom.Location = new System.Drawing.Point(57, 37);
            this.dtFrom.Name = "dtFrom";
            this.dtFrom.ShowCheckBox = true;
            this.dtFrom.Size = new System.Drawing.Size(130, 21);
            this.dtFrom.TabIndex = 4;
            // 
            // dtTo
            // 
            this.dtTo.Checked = false;
            this.dtTo.CustomFormat = "MMM dd, yyyy";
            this.dtTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtTo.Location = new System.Drawing.Point(243, 37);
            this.dtTo.Name = "dtTo";
            this.dtTo.ShowCheckBox = true;
            this.dtTo.Size = new System.Drawing.Size(130, 21);
            this.dtTo.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(193, 39);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 15);
            this.label6.TabIndex = 18;
            this.label6.Text = "Before:";
            // 
            // splHistory
            // 
            this.splHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splHistory.Location = new System.Drawing.Point(0, 106);
            this.splHistory.Name = "splHistory";
            this.splHistory.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splHistory.Panel1
            // 
            this.splHistory.Panel1.Controls.Add(this.grdHistory);
            // 
            // splHistory.Panel2
            // 
            this.splHistory.Panel2.AutoScroll = true;
            this.splHistory.Panel2.Controls.Add(this.pnlValue);
            this.splHistory.Size = new System.Drawing.Size(782, 462);
            this.splHistory.SplitterDistance = 231;
            this.splHistory.TabIndex = 19;
            // 
            // grdHistory
            // 
            this.grdHistory.AllowUserToAddRows = false;
            this.grdHistory.AllowUserToDeleteRows = false;
            this.grdHistory.AllowUserToOrderColumns = true;
            this.grdHistory.AllowUserToResizeRows = false;
            this.grdHistory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grdHistory.CausesValidation = false;
            this.grdHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdHistory.Location = new System.Drawing.Point(0, 0);
            this.grdHistory.Name = "grdHistory";
            this.grdHistory.ReadOnly = true;
            this.grdHistory.RowHeadersVisible = false;
            this.grdHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdHistory.ShowEditingIcon = false;
            this.grdHistory.Size = new System.Drawing.Size(782, 231);
            this.grdHistory.TabIndex = 8;
            this.grdHistory.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.grdHistory_DataBindingComplete);
            this.grdHistory.SelectionChanged += new System.EventHandler(this.grdHistory_SelectionChanged);
            // 
            // pnlValue
            // 
            this.pnlValue.AutoScroll = true;
            this.pnlValue.Controls.Add(this.splLR);
            this.pnlValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlValue.Location = new System.Drawing.Point(0, 0);
            this.pnlValue.Name = "pnlValue";
            this.pnlValue.Size = new System.Drawing.Size(782, 227);
            this.pnlValue.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 15);
            this.label5.TabIndex = 0;
            this.label5.Text = "Old Value:";
            // 
            // txtNewValue
            // 
            this.txtNewValue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNewValue.Location = new System.Drawing.Point(3, 18);
            this.txtNewValue.Name = "txtNewValue";
            this.txtNewValue.ReadOnly = true;
            this.txtNewValue.Size = new System.Drawing.Size(391, 206);
            this.txtNewValue.TabIndex = 10;
            this.txtNewValue.Text = "";
            this.txtNewValue.WordWrap = false;
            // 
            // txtOldValue
            // 
            this.txtOldValue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOldValue.Location = new System.Drawing.Point(0, 18);
            this.txtOldValue.Name = "txtOldValue";
            this.txtOldValue.ReadOnly = true;
            this.txtOldValue.Size = new System.Drawing.Size(382, 206);
            this.txtOldValue.TabIndex = 9;
            this.txtOldValue.Text = "";
            this.txtOldValue.WordWrap = false;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(325, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 15);
            this.label7.TabIndex = 1;
            this.label7.Text = "New Value:";
            // 
            // numPageSize
            // 
            this.numPageSize.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numPageSize.Location = new System.Drawing.Point(574, 37);
            this.numPageSize.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numPageSize.Name = "numPageSize";
            this.numPageSize.Size = new System.Drawing.Size(68, 21);
            this.numPageSize.TabIndex = 6;
            this.numPageSize.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(502, 39);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(66, 15);
            this.label8.TabIndex = 21;
            this.label8.Text = "Page Size:";
            // 
            // btnLast
            // 
            this.btnLast.Location = new System.Drawing.Point(456, 81);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(27, 23);
            this.btnLast.TabIndex = 24;
            this.btnLast.Text = ">|";
            this.toolTip1.SetToolTip(this.btnLast, "Last Page");
            this.btnLast.UseVisualStyleBackColor = true;
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // btnFirst
            // 
            this.btnFirst.Location = new System.Drawing.Point(423, 81);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(27, 23);
            this.btnFirst.TabIndex = 25;
            this.btnFirst.Text = "|<";
            this.toolTip1.SetToolTip(this.btnFirst, "First Page");
            this.btnFirst.UseVisualStyleBackColor = true;
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // lblTotalPage
            // 
            this.lblTotalPage.AutoSize = true;
            this.lblTotalPage.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalPage.Location = new System.Drawing.Point(385, 88);
            this.lblTotalPage.Name = "lblTotalPage";
            this.lblTotalPage.Size = new System.Drawing.Size(32, 15);
            this.lblTotalPage.TabIndex = 28;
            this.lblTotalPage.Text = "total";
            // 
            // numPageNumber
            // 
            this.numPageNumber.Location = new System.Drawing.Point(309, 83);
            this.numPageNumber.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numPageNumber.Name = "numPageNumber";
            this.numPageNumber.Size = new System.Drawing.Size(48, 21);
            this.numPageNumber.TabIndex = 29;
            this.numPageNumber.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numPageNumber.ValueChanged += new System.EventHandler(this.numPageNumber_ValueChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(271, 90);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(32, 14);
            this.label9.TabIndex = 30;
            this.label9.Text = "Page";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(363, 90);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(16, 14);
            this.label10.TabIndex = 31;
            this.label10.Text = "of";
            // 
            // splLR
            // 
            this.splLR.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splLR.Location = new System.Drawing.Point(0, 0);
            this.splLR.Name = "splLR";
            // 
            // splLR.Panel1
            // 
            this.splLR.Panel1.Controls.Add(this.label5);
            this.splLR.Panel1.Controls.Add(this.txtOldValue);
            // 
            // splLR.Panel2
            // 
            this.splLR.Panel2.Controls.Add(this.label7);
            this.splLR.Panel2.Controls.Add(this.txtNewValue);
            this.splLR.Size = new System.Drawing.Size(782, 227);
            this.splLR.SplitterDistance = 384;
            this.splLR.SplitterWidth = 1;
            this.splLR.TabIndex = 11;
            // 
            // HistoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 568);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.numPageNumber);
            this.Controls.Add(this.lblTotalPage);
            this.Controls.Add(this.btnFirst);
            this.Controls.Add(this.btnLast);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.numPageSize);
            this.Controls.Add(this.splHistory);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dtTo);
            this.Controls.Add(this.dtFrom);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtId);
            this.Controls.Add(this.drpOperator);
            this.Controls.Add(this.drpEntityType);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "HistoryForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Data Modification History";
            this.Load += new System.EventHandler(this.HistoryForm_Load);
            this.splHistory.Panel1.ResumeLayout(false);
            this.splHistory.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splHistory)).EndInit();
            this.splHistory.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdHistory)).EndInit();
            this.pnlValue.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numPageSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPageNumber)).EndInit();
            this.splLR.Panel1.ResumeLayout(false);
            this.splLR.Panel1.PerformLayout();
            this.splLR.Panel2.ResumeLayout(false);
            this.splLR.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splLR)).EndInit();
            this.splLR.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ComboBox drpEntityType;
        private System.Windows.Forms.ComboBox drpOperator;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtFrom;
        private System.Windows.Forms.DateTimePicker dtTo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.SplitContainer splHistory;
        private System.Windows.Forms.DataGridView grdHistory;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RichTextBox txtNewValue;
        private System.Windows.Forms.RichTextBox txtOldValue;
        private System.Windows.Forms.Panel pnlValue;
        private System.Windows.Forms.NumericUpDown numPageSize;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.Button btnFirst;
        private System.Windows.Forms.Label lblTotalPage;
        private System.Windows.Forms.NumericUpDown numPageNumber;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.SplitContainer splLR;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}