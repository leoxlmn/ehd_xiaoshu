namespace EHD.Admin {
    partial class UCAcupunctureDetail {
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtTreatmentPlan = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.picTongueDiagram = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtBloodPressure = new System.Windows.Forms.TextBox();
            this.txtTongue = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtBloodSugar = new System.Windows.Forms.TextBox();
            this.txtPulse = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtSubjective = new System.Windows.Forms.TextBox();
            this.txtTCMDiagnosis = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.picTongueDiagram)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 604);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Treatment Plan:";
            // 
            // txtTreatmentPlan
            // 
            this.txtTreatmentPlan.Location = new System.Drawing.Point(6, 620);
            this.txtTreatmentPlan.Multiline = true;
            this.txtTreatmentPlan.Name = "txtTreatmentPlan";
            this.txtTreatmentPlan.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtTreatmentPlan.Size = new System.Drawing.Size(631, 140);
            this.txtTreatmentPlan.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(571, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "P=Pulse, T=Tongue, A=Acupuncture, Rx=Herbal prescription, BP=Blood pressure, BS=B" +
    "lood sugar, E=Ear acupuncture";
            // 
            // picTongueDiagram
            // 
            this.picTongueDiagram.Location = new System.Drawing.Point(6, 41);
            this.picTongueDiagram.Margin = new System.Windows.Forms.Padding(0);
            this.picTongueDiagram.Name = "picTongueDiagram";
            this.picTongueDiagram.Size = new System.Drawing.Size(288, 288);
            this.picTongueDiagram.TabIndex = 3;
            this.picTongueDiagram.TabStop = false;
            this.picTongueDiagram.MouseClick += new System.Windows.Forms.MouseEventHandler(this.picTongueDiagram_MouseClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(327, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(19, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "P:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(327, 175);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(27, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "BP:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(19, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "T:";
            // 
            // txtBloodPressure
            // 
            this.txtBloodPressure.Location = new System.Drawing.Point(330, 191);
            this.txtBloodPressure.MaxLength = 250;
            this.txtBloodPressure.Multiline = true;
            this.txtBloodPressure.Name = "txtBloodPressure";
            this.txtBloodPressure.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtBloodPressure.Size = new System.Drawing.Size(307, 122);
            this.txtBloodPressure.TabIndex = 3;
            // 
            // txtTongue
            // 
            this.txtTongue.Location = new System.Drawing.Point(6, 332);
            this.txtTongue.MaxLength = 250;
            this.txtTongue.Multiline = true;
            this.txtTongue.Name = "txtTongue";
            this.txtTongue.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtTongue.Size = new System.Drawing.Size(288, 121);
            this.txtTongue.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(327, 316);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(27, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "BS:";
            // 
            // txtBloodSugar
            // 
            this.txtBloodSugar.Location = new System.Drawing.Point(330, 332);
            this.txtBloodSugar.MaxLength = 250;
            this.txtBloodSugar.Multiline = true;
            this.txtBloodSugar.Name = "txtBloodSugar";
            this.txtBloodSugar.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtBloodSugar.Size = new System.Drawing.Size(307, 121);
            this.txtBloodSugar.TabIndex = 4;
            // 
            // txtPulse
            // 
            this.txtPulse.Location = new System.Drawing.Point(330, 41);
            this.txtPulse.MaxLength = 250;
            this.txtPulse.Multiline = true;
            this.txtPulse.Name = "txtPulse";
            this.txtPulse.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtPulse.Size = new System.Drawing.Size(307, 121);
            this.txtPulse.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(3, 460);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Subjective:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(327, 460);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(96, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "TCM Diagnosis:";
            // 
            // txtSubjective
            // 
            this.txtSubjective.Location = new System.Drawing.Point(6, 476);
            this.txtSubjective.MaxLength = 1000;
            this.txtSubjective.Multiline = true;
            this.txtSubjective.Name = "txtSubjective";
            this.txtSubjective.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSubjective.Size = new System.Drawing.Size(288, 125);
            this.txtSubjective.TabIndex = 5;
            // 
            // txtTCMDiagnosis
            // 
            this.txtTCMDiagnosis.Location = new System.Drawing.Point(330, 476);
            this.txtTCMDiagnosis.MaxLength = 250;
            this.txtTCMDiagnosis.Multiline = true;
            this.txtTCMDiagnosis.Name = "txtTCMDiagnosis";
            this.txtTCMDiagnosis.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtTCMDiagnosis.Size = new System.Drawing.Size(307, 125);
            this.txtTCMDiagnosis.TabIndex = 6;
            // 
            // UCAcupunctureDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtTCMDiagnosis);
            this.Controls.Add(this.txtSubjective);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtPulse);
            this.Controls.Add(this.txtBloodSugar);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtTongue);
            this.Controls.Add(this.txtBloodPressure);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.picTongueDiagram);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtTreatmentPlan);
            this.Controls.Add(this.label1);
            this.Name = "UCAcupunctureDetail";
            this.Size = new System.Drawing.Size(640, 770);
            this.Load += new System.EventHandler(this.UCAcupunctureDetail_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picTongueDiagram)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTreatmentPlan;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox picTongueDiagram;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtBloodPressure;
        private System.Windows.Forms.TextBox txtTongue;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtBloodSugar;
        private System.Windows.Forms.TextBox txtPulse;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtSubjective;
        private System.Windows.Forms.TextBox txtTCMDiagnosis;
    }
}
