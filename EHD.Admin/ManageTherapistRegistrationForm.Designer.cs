namespace EHD.Admin {
    partial class ManageTherapistRegistrationForm {
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
            this.tvReg = new System.Windows.Forms.TreeView();
            this.menu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuAddReg = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuEditReg = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDeleteReg = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // tvReg
            // 
            this.tvReg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tvReg.Location = new System.Drawing.Point(0, 0);
            this.tvReg.Name = "tvReg";
            this.tvReg.Size = new System.Drawing.Size(784, 394);
            this.tvReg.TabIndex = 0;
            this.tvReg.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvReg_NodeMouseClick);
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAddReg,
            this.toolStripSeparator1,
            this.mnuEditReg,
            this.mnuDeleteReg});
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(174, 98);
            // 
            // mnuAddReg
            // 
            this.mnuAddReg.Name = "mnuAddReg";
            this.mnuAddReg.Size = new System.Drawing.Size(173, 22);
            this.mnuAddReg.Text = "Add Registration";
            this.mnuAddReg.Click += new System.EventHandler(this.mnuAddReg_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(170, 6);
            // 
            // mnuEditReg
            // 
            this.mnuEditReg.Name = "mnuEditReg";
            this.mnuEditReg.Size = new System.Drawing.Size(173, 22);
            this.mnuEditReg.Text = "Edit Registration";
            this.mnuEditReg.Click += new System.EventHandler(this.mnuEditReg_Click);
            // 
            // mnuDeleteReg
            // 
            this.mnuDeleteReg.Name = "mnuDeleteReg";
            this.mnuDeleteReg.Size = new System.Drawing.Size(173, 22);
            this.mnuDeleteReg.Text = "Delete Registration";
            this.mnuDeleteReg.Click += new System.EventHandler(this.mnuDeleteReg_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 400);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(523, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "If you want to combine tow or more registrations for a therapist. Please press th" +
                "e [Ctrl] key.";
            // 
            // ManageTherapistRegistrationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 422);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tvReg);
            this.KeyPreview = true;
            this.Name = "ManageTherapistRegistrationForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Manage Therapist Registration";
            this.Load += new System.EventHandler(this.ManageTherapistRegistrationForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ManageTherapistRegistrationForm_KeyDown);
            this.menu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView tvReg;
        private System.Windows.Forms.ContextMenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem mnuAddReg;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnuEditReg;
        private System.Windows.Forms.ToolStripMenuItem mnuDeleteReg;
        private System.Windows.Forms.Label label1;
    }
}