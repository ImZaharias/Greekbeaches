using GreekBeachesGuide.Services;
using System;
using System.Data.SQLite;
using System.Windows.Forms;

namespace GreekBeachesGuide.Forms
{
    // Handles user login, guest mode, and registration redirection
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();

            // Basic UI setup
            this.StartPosition = FormStartPosition.CenterScreen;
            this.AcceptButton = btnLogin;   // Press Enter = Login
            // this.CancelButton = btnGuest; // Optional: Esc = Guest mode

            // Exit application if login window closes
            this.FormClosed += (s, e) => Application.Exit();
        }

        // Login button click handler
        private void btnLogin_Click(object sender, EventArgs e)
        {
            var user = (txtUser.Text ?? "").Trim();
            var pass = (txtPass.Text ?? "").Trim();

            // Validate input
            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Συμπλήρωσε όνομα και κωδικό.", "Login",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Verify credentials in DB
                using (var con = new SQLiteConnection(Db.ConnStr))
                using (var cmd = new SQLiteCommand(
                    "SELECT Role FROM Users WHERE Username=@u AND Password=@p LIMIT 1", con))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@u", user);
                    cmd.Parameters.AddWithValue("@p", pass);

                    var role = cmd.ExecuteScalar() as string;

                    // Successful login
                    if (!string.IsNullOrEmpty(role))
                    {
                        var main = new FormMain(user, role);
                        main.FormClosed += (s, args) => this.Close(); // Clean exit

                        this.Hide();
                        main.Show();
                    }
                    else
                    {
                        // Wrong credentials
                        MessageBox.Show("Λάθος στοιχεία.", "Login",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtPass.Clear();
                        txtPass.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Σφάλμα σύνδεσης στη βάση: " + ex.Message, "Login",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Guest mode (no credentials)
        private void btnGuest_Click(object sender, EventArgs e)
        {
            var main = new FormMain("guest", "Visitor");
            main.FormClosed += (s, args) => this.Close();
            this.Hide();
            main.Show();
        }

        // Open registration form as modal dialog
        private void btnRegister_Click(object sender, EventArgs e)
        {
            using var f = new FormRegister();
            f.ShowDialog(this);
        }
    }
}


