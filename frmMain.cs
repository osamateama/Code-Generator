using BusinessLayer;
using System;
using System.Data;
using System.Windows.Forms;

namespace FormCodeGenerator
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dbs = clsDatabaseManager.GetDatabases();
                cbDatabases.DataSource = dbs;
                cbDatabases.DisplayMember = "name";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading databases: {ex.Message}");
            }
        }
        //

        private void btnUplode_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter = "Backup files (*.bak)|*.bak";
                dlg.Title = "Select a backup file";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    string filePath = dlg.FileName;
                    string fileName = System.IO.Path.GetFileName(filePath);

                    string currentDb = cbDatabases.Text;

                    DataTable dt = (DataTable)cbDatabases.DataSource;
                    DataRow newRow = dt.NewRow();
                    newRow["name"] = fileName;
                    dt.Rows.InsertAt(newRow, 0);

                    cbDatabases.DataSource = dt;
                    cbDatabases.DisplayMember = "name";
                    cbDatabases.SelectedIndex = 0;
                }
            }
        }
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.Title = "Select a folder to save generated classes";
                dlg.Filter = "All files (*.*)|*.*";
                dlg.CheckFileExists = false;
                dlg.FileName = "Select Folder";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    string folderPath = System.IO.Path.GetDirectoryName(dlg.FileName);
                    txtPath.Text = folderPath;
                }
            }
        }


        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btngenerate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPath.Text))
            {
                MessageBox.Show("Please select a folder to save classes.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(cbDatabases.Text))
            {
                MessageBox.Show("Please select a database.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var sw = System.Diagnostics.Stopwatch.StartNew();

                string outputPath = txtPath.Text;
                string dalFolder = System.IO.Path.Combine(outputPath, "DAL");
                string blFolder = System.IO.Path.Combine(outputPath, "Business");

                System.IO.Directory.CreateDirectory(dalFolder);
                System.IO.Directory.CreateDirectory(blFolder);

                DataTable tables = clsDatabaseManager.GetTables(cbDatabases.Text);

                foreach (DataRow row in tables.Rows)
                {
                    string tableName = row["TABLE_NAME"].ToString();

                    DataTable columns = clsDatabaseManager.GetColumns(cbDatabases.Text, tableName);

                    clsCodeGenerator.GenerateClassForTable(outputPath, cbDatabases.Text, tableName, columns);
                }

                sw.Stop();
                MessageBox.Show($" Generation completed in {sw.ElapsedMilliseconds} ms", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating classes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}