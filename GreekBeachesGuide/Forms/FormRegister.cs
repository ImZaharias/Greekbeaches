using System;
using System.Windows.Forms;
using GreekBeachesGuide.Services;

namespace GreekBeachesGuide.Forms
{
    public partial class FormRegister : Form
    {
        public FormRegister()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterParent;
            txtPassword.UseSystemPasswordChar = true;
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            var u = txtUsername.Text.Trim();
            var p = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(u) || string.IsNullOrEmpty(p))
            {
                MessageBox.Show("Συμπλήρωσε και τα δύο πεδία.");
                return;
            }

            try
            {
                if (AuthDb.Exists(u))
                {
                    MessageBox.Show("Αυτό το όνομα χρήστη υπάρχει ήδη.");
                    return;
                }

                AuthDb.CreateUser(u, p);
                MessageBox.Show("Ο λογαριασμός δημιουργήθηκε!");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Σφάλμα: " + ex.Message);
            }
        }
    }
}

