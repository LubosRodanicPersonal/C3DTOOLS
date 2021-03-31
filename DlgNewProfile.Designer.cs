
namespace C3DTools {
    partial class DlgNewProfile {
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
            this.txtProfileHeight = new System.Windows.Forms.TextBox();
            this.lblProfileHeight = new System.Windows.Forms.Label();
            this.lblProfileName = new System.Windows.Forms.Label();
            this.txtProfileName = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnStorno = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtProfileHeight
            // 
            this.txtProfileHeight.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtProfileHeight.Location = new System.Drawing.Point(48, 40);
            this.txtProfileHeight.Name = "txtProfileHeight";
            this.txtProfileHeight.Size = new System.Drawing.Size(230, 20);
            this.txtProfileHeight.TabIndex = 11;
            this.txtProfileHeight.Text = "0";
            // 
            // lblProfileHeight
            // 
            this.lblProfileHeight.AutoSize = true;
            this.lblProfileHeight.Location = new System.Drawing.Point(7, 43);
            this.lblProfileHeight.Name = "lblProfileHeight";
            this.lblProfileHeight.Size = new System.Drawing.Size(39, 13);
            this.lblProfileHeight.TabIndex = 10;
            this.lblProfileHeight.Text = "Výška:";
            // 
            // lblProfileName
            // 
            this.lblProfileName.AutoSize = true;
            this.lblProfileName.Location = new System.Drawing.Point(7, 16);
            this.lblProfileName.Name = "lblProfileName";
            this.lblProfileName.Size = new System.Drawing.Size(41, 13);
            this.lblProfileName.TabIndex = 9;
            this.lblProfileName.Text = "Jméno:";
            // 
            // txtProfileName
            // 
            this.txtProfileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtProfileName.Location = new System.Drawing.Point(48, 12);
            this.txtProfileName.Name = "txtProfileName";
            this.txtProfileName.Size = new System.Drawing.Size(230, 20);
            this.txtProfileName.TabIndex = 8;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(122, 69);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 12;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnStorno
            // 
            this.btnStorno.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStorno.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnStorno.Location = new System.Drawing.Point(203, 69);
            this.btnStorno.Name = "btnStorno";
            this.btnStorno.Size = new System.Drawing.Size(75, 23);
            this.btnStorno.TabIndex = 13;
            this.btnStorno.Text = "Storno";
            this.btnStorno.UseVisualStyleBackColor = true;
            // 
            // DlgNewProfile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(290, 104);
            this.Controls.Add(this.btnStorno);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtProfileHeight);
            this.Controls.Add(this.lblProfileHeight);
            this.Controls.Add(this.lblProfileName);
            this.Controls.Add(this.txtProfileName);
            this.MinimumSize = new System.Drawing.Size(306, 143);
            this.Name = "DlgNewProfile";
            this.Text = "Nový profil";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblProfileHeight;
        private System.Windows.Forms.Label lblProfileName;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnStorno;
        public System.Windows.Forms.TextBox txtProfileHeight;
        public System.Windows.Forms.TextBox txtProfileName;
    }
}