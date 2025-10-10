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
    // Shows detailed info for a selected beach (image, description, sound)
    public partial class FormBeachDetails : Form
    {
        private Beach _b;
        private readonly string _user;
        private SoundPlayer _sp;  // Audio player instance
        private bool _isPlaying;  // Playback state flag

        public FormBeachDetails()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterParent;
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            richTextBox.ReadOnly = true;
        }

        // Overloaded constructor used when opening from main form
        internal FormBeachDetails(Beach b, string user) : this()
        {
            _b = b;
            _user = user;
            Bind();              // Load UI content
            AddToHistorySafe();  // Log view into History table
        }

        // Populate controls with beach data
        private void Bind()
        {
            if (_b == null) return;

            richTextBox.Text = $"{_b.Name} — {_b.Region}"
                             + Environment.NewLine + Environment.NewLine
                             + (_b.Description ?? "");

            // Load image safely (avoid file locking)
            var fullImg = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _b.ImagePath ?? "");
            try
            {
                if (File.Exists(fullImg))
                {
                    using (var img = Image.FromFile(fullImg))
                        pictureBox.Image = new Bitmap(img);
                }
                else pictureBox.Image = null;
            }
            catch { pictureBox.Image = null; }
        }

        // Add record to History table (safe, silent fail)
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
            catch { /* Ignore if DB not available */ }
        }

        // Play or stop ambient sound (.wav only)
        private void btnSound_Click(object sender, EventArgs e)
        {
            try
            {
                var full = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Data\Media\waves.wav");

                if (!File.Exists(full))
                {
                    MessageBox.Show($"Δεν βρέθηκε αρχείο ήχου στο:\n{full}");
                    return;
                }

                if (Path.GetExtension(full).Equals(".wav", StringComparison.OrdinalIgnoreCase))
                {
                    // Toggle playback state
                    if (_isPlaying)
                    {
                        _sp?.Stop();
                        _isPlaying = false;
                        btnSound.Text = "Αναπαραγωγή";
                    }
                    else
                    {
                        _sp = new SoundPlayer(full);
                        _sp.Play();
                        _isPlaying = true;
                        btnSound.Text = "Παύση";
                    }
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

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _sp?.Stop();
            _sp?.Dispose();
            base.OnFormClosing(e);
        }
    }
}


