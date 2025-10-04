using GreekBeachesGuide.Models;
using GreekBeachesGuide.Services;
using System;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Media;
using System.Windows.Forms;

namespace GreekBeachesGuide.Forms
{
    public partial class FormBeachDetails : Form
    {
        private Beach _b;
        private readonly string _user;

        public FormBeachDetails()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterParent;
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            richTextBox.ReadOnly = true;
        }

        // <-- ο παραμετρικός ctor πρέπει να κάνει chaining στο default
        internal FormBeachDetails(Beach b, string user) : this()
        {
            _b = b;
            _user = user;
            Bind();
            AddToHistorySafe();
        }

        private void Bind()
        {
            if (_b == null) return;

            // ΜΗΝ γράφεις δύο φορές Text και το πατάς.
            richTextBox.Text = $"{_b.Name} — {_b.Region}"
                             + Environment.NewLine + Environment.NewLine
                             + (_b.Description ?? "");

            // Φόρτωση εικόνας χωρίς file-lock + σωστό πλήρες path
            var fullImg = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _b.ImagePath ?? "");
            try
            {
                if (File.Exists(fullImg))
                {
                    using (var img = Image.FromFile(fullImg))
                        pictureBox.Image = new Bitmap(img);   // αποφεύγει κλείδωμα αρχείου
                }
                else pictureBox.Image = null;
            }
            catch { pictureBox.Image = null; }
        }

        private void AddToHistorySafe()
        {
            try
            {
                using var con = new SQLiteConnection(Db.ConnStr);
                con.Open();
                using var cmd = new SQLiteCommand(
                    "INSERT INTO History(Username,BeachId,ViewedAt) VALUES(@u,@b,datetime('now'))", con);
                cmd.Parameters.AddWithValue("@u", _user ?? "guest");
                cmd.Parameters.AddWithValue("@b", _b?.Id ?? 0);
                cmd.ExecuteNonQuery();
            }
            catch { /* σιωπηλά: δεν χαλάμε τη φόρμα αν λείπει DB */ }
        }

        private void btnSound_Click(object sender, EventArgs e)
        {
            try
            {
                var full = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _b?.AudioPath ?? "");
                if (!File.Exists(full)) { MessageBox.Show("Δεν βρέθηκε αρχείο ήχου."); return; }

                // ΣΗΜΑΝΤΙΚΟ: SoundPlayer παίζει μόνο .wav
                if (Path.GetExtension(full).Equals(".wav", StringComparison.OrdinalIgnoreCase))
                {
                    using var sp = new SoundPlayer(full);
                    sp.Play();
                }
                else
                {
                    MessageBox.Show("Το SoundPlayer υποστηρίζει μόνο .wav. Αν έχεις .mp3, βάλε AxWindowsMediaPlayer.");
                }
            }
            catch
            {
                MessageBox.Show("Δεν ήταν δυνατή η αναπαραγωγή ήχου.");
            }
        }
    }
}

