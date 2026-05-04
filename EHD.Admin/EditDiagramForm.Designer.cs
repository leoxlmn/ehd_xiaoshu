namespace EHD.Admin {
    partial class EditDiagramForm {
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
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtImageType = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnOpenImage = new System.Windows.Forms.Button();
            this.label18 = new System.Windows.Forms.Label();
            this.txtDiagramName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.picDiagram = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.numImageWidth = new System.Windows.Forms.NumericUpDown();
            this.numImageHeight = new System.Windows.Forms.NumericUpDown();
            this.dlgOpenFile = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.picDiagram)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numImageWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numImageHeight)).BeginInit();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(326, 120);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(16, 16);
            this.label6.TabIndex = 128;
            this.label6.Text = "*";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(176, 117);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 13);
            this.label5.TabIndex = 121;
            this.label5.Text = "Image Height:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(179, 89);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 13);
            this.label4.TabIndex = 120;
            this.label4.Text = "Image Width:";
            // 
            // txtImageType
            // 
            this.txtImageType.Enabled = false;
            this.txtImageType.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtImageType.Location = new System.Drawing.Point(261, 58);
            this.txtImageType.Name = "txtImageType";
            this.txtImageType.Size = new System.Drawing.Size(59, 22);
            this.txtImageType.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(188, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 119;
            this.label3.Text = "Image Type:";
            // 
            // btnOpenImage
            // 
            this.btnOpenImage.Location = new System.Drawing.Point(12, 151);
            this.btnOpenImage.Name = "btnOpenImage";
            this.btnOpenImage.Size = new System.Drawing.Size(152, 23);
            this.btnOpenImage.TabIndex = 5;
            this.btnOpenImage.Text = "Select a New Image";
            this.btnOpenImage.UseVisualStyleBackColor = true;
            this.btnOpenImage.Click += new System.EventHandler(this.btnChangeDiagram_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.Red;
            this.label18.Location = new System.Drawing.Point(431, 18);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(16, 16);
            this.label18.TabIndex = 117;
            this.label18.Text = "*";
            // 
            // txtDiagramName
            // 
            this.txtDiagramName.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDiagramName.Location = new System.Drawing.Point(261, 12);
            this.txtDiagramName.Name = "txtDiagramName";
            this.txtDiagramName.Size = new System.Drawing.Size(164, 22);
            this.txtDiagramName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(170, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 116;
            this.label1.Text = "Diagram Name:";
            // 
            // picDiagram
            // 
            this.picDiagram.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picDiagram.Location = new System.Drawing.Point(12, 12);
            this.picDiagram.Name = "picDiagram";
            this.picDiagram.Size = new System.Drawing.Size(152, 139);
            this.picDiagram.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picDiagram.TabIndex = 129;
            this.picDiagram.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(326, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(16, 16);
            this.label2.TabIndex = 130;
            this.label2.Text = "*";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(365, 188);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.Location = new System.Drawing.Point(12, 188);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // numImageWidth
            // 
            this.numImageWidth.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numImageWidth.Location = new System.Drawing.Point(261, 87);
            this.numImageWidth.Maximum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.numImageWidth.Name = "numImageWidth";
            this.numImageWidth.Size = new System.Drawing.Size(59, 22);
            this.numImageWidth.TabIndex = 3;
            this.numImageWidth.ThousandsSeparator = true;
            // 
            // numImageHeight
            // 
            this.numImageHeight.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numImageHeight.Location = new System.Drawing.Point(261, 115);
            this.numImageHeight.Maximum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.numImageHeight.Name = "numImageHeight";
            this.numImageHeight.Size = new System.Drawing.Size(59, 22);
            this.numImageHeight.TabIndex = 4;
            this.numImageHeight.ThousandsSeparator = true;
            // 
            // dlgOpenFile
            // 
            this.dlgOpenFile.FileName = "openFileDialog1";
            // 
            // EditDiagramForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(452, 223);
            this.Controls.Add(this.numImageHeight);
            this.Controls.Add(this.numImageWidth);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.picDiagram);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtImageType);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnOpenImage);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.txtDiagramName);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditDiagramForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit Diagram";
            this.Load += new System.EventHandler(this.EditDiagramForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picDiagram)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numImageWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numImageHeight)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtImageType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnOpenImage;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtDiagramName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox picDiagram;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.NumericUpDown numImageWidth;
        private System.Windows.Forms.NumericUpDown numImageHeight;
        private System.Windows.Forms.OpenFileDialog dlgOpenFile;
    }
}