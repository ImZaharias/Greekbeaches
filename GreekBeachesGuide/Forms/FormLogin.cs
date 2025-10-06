using GreekBeachesGuide.Services;
using System;
using System.Data.SQLite;
using System.Windows.Forms;

namespace GreekBeachesGuide.Forms
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();

            // UX niceties
            this.StartPosition = FormStartPosition.CenterScreen;
            this.AcceptButton = btnLogin;  // Enter -> Login
            // this.CancelButton = btnGuest; // (optional) Esc -> Guest

            // If someone closes the login window directly, exit the app cleanly
            this.FormClosed += (s, e) => Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var user = (txtUser.Text ?? "").Trim();
            var pass = (txtPass.Text ?? "").Trim();

            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Συμπλήρωσε όνομα και κωδικό.", "Login",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var con = new SQLiteConnection(Db.ConnStr))
                using (var cmd = new SQLiteCommand(
                    "SELECT Role FROM Users WHERE Username=@u AND Password=@p LIMIT 1", con))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@u", user);
                    cmd.Parameters.AddWithValue("@p", pass);

                    var role = cmd.ExecuteScalar() as string;

                    if (!string.IsNullOrEmpty(role))
                    {
                        var main = new FormMain(user, role);
                        // When main closes, close login too => no zombie process
                        main.FormClosed += (s, args) => this.Close();

                        this.Hide();
                        main.Show();
                    }
                    else
                    {
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

        private void btnGuest_Click(object sender, EventArgs e)
        {
            var main = new FormMain("guest", "Visitor");
            main.FormClosed += (s, args) => this.Close();
            this.Hide();
            main.Show();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            using var f = new FormRegister();
            f.ShowDialog(this);
        }
    }
}

