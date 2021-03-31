using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C3DTools {
    public partial class DlgAlignment : Form {

        public System.Data.DataTable dtProfiles = null;

        public DlgAlignment() {
            InitializeComponent();

            //custom init
            dtProfiles = new System.Data.DataTable();
            dtProfiles.TableName = "Profiles";

            //dtProfiles.Columns.Add("cID", System.Type.GetType("System.String")).Caption = "HANDLE";
            System.Data.DataColumn cl = new System.Data.DataColumn();
            cl.ColumnName = "ID";
            cl.DataType = System.Type.GetType("System.String");
            dtProfiles.Columns.Add(cl);

            //dtProfiles.Columns.Add("cName", System.Type.GetType("System.String")).Caption = "NAME";
            System.Data.DataColumn cl2 = new System.Data.DataColumn();
            cl2.ColumnName = "NAME";
            cl2.DataType = System.Type.GetType("System.String");
            dtProfiles.Columns.Add(cl2);
        }

        private void DlgAlignment_Load(object sender, EventArgs e) {
            RefreshDataGrid();
        }

        public void InitDataGrid(string aliHandle, string aliName) {
            
            RefreshDataGrid();

            txtAliHandle.Text = aliHandle;
            txtAliName.Text = aliName;

            dgvProfiles.Columns["ID"].Visible = false;
            
            //dgvProfiles.Refresh();
            // Initialize the button column.
            DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
            buttonColumn.Name = "ERASE";
            buttonColumn.HeaderText = "";
            buttonColumn.Text = "Vymazat";

            // Use the Text property for the button text for all cells rather
            // than using each cell's value as the text for its own button.
            buttonColumn.UseColumnTextForButtonValue = true;

            // Add the button column to the control.
            dgvProfiles.Columns.Insert(0, buttonColumn);
        }

        

        

        public void RefreshDataGrid() {
            dgvProfiles.DataSource = null;
            dgvProfiles.Columns.Clear();

            try {
                BindingSource source = new BindingSource();
                source.DataSource = dtProfiles;
                dgvProfiles.DataSource = source;
            }
            catch { }

            //dgvProfiles.Columns["ERASE"].Width = 40;
            dgvProfiles.Columns["ID"].Visible = false;
            dgvProfiles.Columns["NAME"].HeaderText = "Jméno";

            //dgvProfiles.Refresh();
            // Initialize the button column.
            //DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
            //buttonColumn.Name = "ERASE";
            //buttonColumn.HeaderText = "";
            //buttonColumn.Text = "Vymazat";

            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            imageColumn.Name = "ERASE";
            imageColumn.HeaderText = "Vymazat";
            imageColumn.Image = Properties.Resources.Cancel_16x16;


            // Use the Text property for the button text for all cells rather
            // than using each cell's value as the text for its own button.
            //buttonColumn.UseColumnTextForButtonValue = true;

            // Add the button column to the control.
            //dgvProfiles.Columns.Insert(0, buttonColumn);
            dgvProfiles.Columns.Insert(0, imageColumn);
            dgvProfiles.Columns["ERASE"].Width = 60;
        }

        private void btnCreateProfile_Click(object sender, EventArgs e) {

            string profileName = "";
            string profileHeight = "0";
            System.Windows.Forms.DialogResult result = System.Windows.Forms.DialogResult.None;

            while (result == System.Windows.Forms.DialogResult.None || result == System.Windows.Forms.DialogResult.Retry || result == System.Windows.Forms.DialogResult.Abort) {
                using (DlgNewProfile NewProfile = new DlgNewProfile()) {
                    
                    result = NewProfile.ShowDialog();

                    profileName = NewProfile.txtProfileName.Text;
                    profileHeight = NewProfile.txtProfileHeight.Text;
                }
            }


            if (result == System.Windows.Forms.DialogResult.OK) {
                if (profileName == "") {
                    Commands.AddToLog("Zadejte jméno profilu!", true);
                }
                else {
                    double height = 0;
                    double.TryParse(profileHeight, out height);

                    Commands.CreateProfile(txtAliHandle.Text, profileName, height);
                    RefreshDataGrid();
                }
            }




            


            
        }

        private void dgvProfiles_CellContentClick(object sender, DataGridViewCellEventArgs e) {
            var senderGrid = (DataGridView)sender;

            //if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewImageColumn &&
                e.RowIndex >= 0) {
                Commands.DeleteProfile(txtAliHandle.Text, senderGrid.Rows[e.RowIndex].Cells["ID"].Value.ToString());
                RefreshDataGrid();
            }
        }
    }
}
