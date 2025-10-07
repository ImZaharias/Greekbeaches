using System;
using System.Windows.Forms;
using GreekBeachesGuide.Services;

namespace GreekBeachesGuide.Forms
{
    // Handles user registration (username + password)
    public partial class FormRegister : Form
    {
        public FormRegister()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterParent; // Open centered
            txtPassword.UseSystemPasswordChar = true;        // Hide password input
        }

        // Create account button click
        private void btnCreate_Click(object sender, EventArgs e)
        {
            var u = txtUsername.Text.Trim();
            var p = txtPassword.Text.Trim();

            // Basic input validation
            if (string.IsNullOrEmpty(u) || string.IsNullOrEmpty(p))
            {
                MessageBox.Show("Συμπλήρωσε και τα δύο πεδία.");
                return;
            }

            try
            {
                // Check if username already exists
                if (AuthDb.Exists(u))
                {
                    MessageBox.Show("Αυτό το όνομα χρήστη υπάρχει ήδη.");
                    return;
                }

                // Create new user
                AuthDb.CreateUser(u, p);
                MessageBox.Show("Ο λογαριασμός δημιουργήθηκε!");
                this.DialogResult = DialogResult.OK;
                this.Close(); // Return to previous form
            }
            catch (Exception ex)
            {
                MessageBox.Show("Σφάλμα: " + ex.Message);
            }
        }
    }
}


