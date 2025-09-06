using System;
using BusinessLayer;
using System.Windows.Forms;

namespace FormCodeGenerator
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btngenerate_Click(object sender, EventArgs e)
        {
            string server = txtServer.Text.Trim();
            string user = txtUserName.Text.Trim();
            string password = txtPassword.Text.Trim();

            try
            {
                bool ok = clsConnectionManager.Authenticate(server, user, password);

                if (ok)
                {
                    MessageBox.Show("✅ Login successful", "Login", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    frmMain dbForm = new frmMain();
                    dbForm.Show();

                }
                else
                {
                    MessageBox.Show("❌ Login failed", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($" Error : {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}