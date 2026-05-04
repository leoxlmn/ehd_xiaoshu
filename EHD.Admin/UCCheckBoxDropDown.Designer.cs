namespace EHD.Admin {
    partial class UCCheckBoxDropDown {
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.txtSelectedText = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtSelectedText
            // 
            this.txtSelectedText.Location = new System.Drawing.Point(0, 0);
            this.txtSelectedText.Name = "txtSelectedText";
            this.txtSelectedText.Size = new System.Drawing.Size(227, 20);
            this.txtSelectedText.TabIndex = 0;
            this.txtSelectedText.Click += new System.EventHandler(this.txtSelectedText_Click);
            // 
            // UCCheckBoxDropDown
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtSelectedText);
            this.Name = "UCCheckBoxDropDown";
            this.Size = new System.Drawing.Size(227, 20);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSelectedText;
    }
}
