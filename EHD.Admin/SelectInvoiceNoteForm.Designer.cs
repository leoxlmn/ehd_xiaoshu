namespace EHD.Admin {
    partial class SelectInvoiceNoteForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
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
            this.lstInvoiceNote = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lstInvoiceNote
            // 
            this.lstInvoiceNote.FormattingEnabled = true;
            this.lstInvoiceNote.HorizontalScrollbar = true;
            this.lstInvoiceNote.Location = new System.Drawing.Point(0, 26);
            this.lstInvoiceNote.Name = "lstInvoiceNote";
            this.lstInvoiceNote.Size = new System.Drawing.Size(428, 238);
            this.lstInvoiceNote.TabIndex = 0;
            this.lstInvoiceNote.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstInvoiceNote_MouseDoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(-3, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(263, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Double-click on one of the Invoice Note to select:";
            // 
            // SelectInvoiceNoteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(428, 262);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstInvoiceNote);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectInvoiceNoteForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select Invoice Note Form";
            this.Load += new System.EventHandler(this.SelectInvoiceNoteForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstInvoiceNote;
        private System.Windows.Forms.Label label1;
    }
}