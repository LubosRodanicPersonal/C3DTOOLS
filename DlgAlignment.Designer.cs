
namespace C3DTools {
    partial class DlgAlignment {
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
            this.dgvProfiles = new System.Windows.Forms.DataGridView();
            this.txtAliHandle = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblAlignment = new System.Windows.Forms.Label();
            this.txtAliName = new System.Windows.Forms.TextBox();
            this.btnProfileCreate = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProfiles)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvProfiles
            // 
            this.dgvProfiles.AllowUserToAddRows = false;
            this.dgvProfiles.AllowUserToDeleteRows = false;
            this.dgvProfiles.AllowUserToResizeColumns = false;
            this.dgvProfiles.AllowUserToResizeRows = false;
            this.dgvProfiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvProfiles.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProfiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProfiles.Location = new System.Drawing.Point(3, 69);
            this.dgvProfiles.Name = "dgvProfiles";
            this.dgvProfiles.RowHeadersVisible = false;
            this.dgvProfiles.Size = new System.Drawing.Size(384, 112);
            this.dgvProfiles.TabIndex = 0;
            this.dgvProfiles.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProfiles_CellContentClick);
            // 
            // txtAliHandle
            // 
            this.txtAliHandle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAliHandle.Enabled = false;
            this.txtAliHandle.Location = new System.Drawing.Point(195, 44);
            this.txtAliHandle.Name = "txtAliHandle";
            this.txtAliHandle.Size = new System.Drawing.Size(100, 20);
            this.txtAliHandle.TabIndex = 2;
            this.txtAliHandle.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.btnProfileCreate);
            this.panel1.Controls.Add(this.txtAliName);
            this.panel1.Controls.Add(this.lblAlignment);
            this.panel1.Controls.Add(this.txtAliHandle);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.dgvProfiles);
            this.panel1.Location = new System.Drawing.Point(2, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(390, 185);
            this.panel1.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Seznam profilů:";
            // 
            // lblAlignment
            // 
            this.lblAlignment.AutoSize = true;
            this.lblAlignment.Location = new System.Drawing.Point(5, 9);
            this.lblAlignment.Name = "lblAlignment";
            this.lblAlignment.Size = new System.Drawing.Size(37, 13);
            this.lblAlignment.TabIndex = 9;
            this.lblAlignment.Text = "Trasa:";
            // 
            // txtAliName
            // 
            this.txtAliName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAliName.Enabled = false;
            this.txtAliName.Location = new System.Drawing.Point(48, 6);
            this.txtAliName.Name = "txtAliName";
            this.txtAliName.Size = new System.Drawing.Size(339, 20);
            this.txtAliName.TabIndex = 10;
            // 
            // btnProfileCreate
            // 
            this.btnProfileCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnProfileCreate.Image = global::C3DTools.Properties.Resources.Add_16x16;
            this.btnProfileCreate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnProfileCreate.Location = new System.Drawing.Point(301, 33);
            this.btnProfileCreate.Name = "btnProfileCreate";
            this.btnProfileCreate.Size = new System.Drawing.Size(86, 31);
            this.btnProfileCreate.TabIndex = 1;
            this.btnProfileCreate.Text = "Přidat profil";
            this.btnProfileCreate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnProfileCreate.UseVisualStyleBackColor = true;
            this.btnProfileCreate.Click += new System.EventHandler(this.btnCreateProfile_Click);
            // 
            // DlgAlignment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(396, 191);
            this.Controls.Add(this.panel1);
            this.MinimumSize = new System.Drawing.Size(412, 172);
            this.Name = "DlgAlignment";
            this.Text = "C3DTools Build20210331 - Trasa ";
            this.Load += new System.EventHandler(this.DlgAlignment_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProfiles)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvProfiles;
        private System.Windows.Forms.Button btnProfileCreate;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblAlignment;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtAliHandle;
        public System.Windows.Forms.TextBox txtAliName;
    }
}