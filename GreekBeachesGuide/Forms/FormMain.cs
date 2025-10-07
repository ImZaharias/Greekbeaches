using GreekBeachesGuide.Models;
using GreekBeachesGuide.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;

namespace GreekBeachesGuide.Forms
{
    // Main screen: list/search beaches, preview, TTS, slideshow, export, history
    public partial class FormMain : Form
    {
        // ---- State ----
        private readonly string _user;
        private readonly string _role;
        private readonly List<Beach> _history = new();
        private List<Beach> _current = new();
        private int _slideIndex = 0;
        private bool _colsInit = false;

        // ---- Constructors ----
        public FormMain()
        {
            InitializeComponent();
            _user = "guest";
            _role = "Visitor";
            LoadBeaches(string.Empty); // initial load
            SetupMenuByRole();         // adjust UI by role
            this.FormClosed += (s, e) => Application.Exit(); // close app when main closes
        }

        public FormMain(string user, string role)
        {
            InitializeComponent();
            _user = user;
            _role = role;
            LoadBeaches(string.Empty);
            SetupMenuByRole();
            this.FormClosed += (s, e) => Application.Exit();
        }

        // ---- Role-based UI toggles ----
        private void SetupMenuByRole()
        {
            bool isVisitor = string.Equals(_role, "Visitor", StringComparison.OrdinalIgnoreCase);
            if (btnClearHistory != null) btnClearHistory.Visible = !isVisitor;
            if (historyToolStripMenuItem != null) historyToolStripMenuItem.Visible = !isVisitor;
            if (btnExport != null) btnExport.Visible = !isVisitor;
            if (exportToolStripMenuItem != null) exportToolStripMenuItem.Visible = !isVisitor;
        }

        // ---- ListView column setup (once) ----
        private void EnsureListViewColumns()
        {
            if (_colsInit) return;
            lvBeaches.View = View.Details;
            lvBeaches.FullRowSelect = true;
            lvBeaches.HideSelection = false;

            lvBeaches.Columns.Clear();
            lvBeaches.Columns.Add("Όνομα", 150);
            lvBeaches.Columns.Add("Περιοχή", 120);
            lvBeaches.Columns.Add("Χαρακτηριστικά", 180);

            _colsInit = true;
        }

        // ---- Load data + bind to ListView ----
        private void LoadBeaches(string q)
        {
            try
            {
                EnsureListViewColumns();

                _current = Db.SearchBeaches(q) ?? new List<Beach>();

                lvBeaches.BeginUpdate();
                lvBeaches.Items.Clear();

                foreach (var b in _current)
                {
                    var it = new ListViewItem(new[] { b.Name, b.Region, b.Features ?? "" }) { Tag = b };
                    lvBeaches.Items.Add(it);
                }

                lvBeaches.EndUpdate();

                // Auto-select first and show its preview
                if (_current.Count > 0)
                {
                    lvBeaches.Items[0].Selected = true;
                    ShowPreview(_current[0]);
                }
                else
                {
                    ShowPreview(null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Σφάλμα φόρτωσης: " + ex.Message);
            }
        }

        // ---- Preview panel (labels + image + TTS button state) ----
        private void ShowPreview(Beach b)
        {
            if (b == null)
            {
                lblName.Text = "—";
                lblRegion.Text = "";
                rtbDescription.Clear();
                pbPreview.Image = null;
                btnTTSPlay.Enabled = false;
                return;
            }

            lblName.Text = b.Name;
            lblRegion.Text = b.Region;
            rtbDescription.Text = b.Description ?? "";
            btnTTSPlay.Enabled = !string.IsNullOrWhiteSpace(rtbDescription.Text);

            // Load preview image safely (avoid file lock)
            try
            {
                var full = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, b.ImagePath ?? "");
                if (File.Exists(full))
                {
                    using (var img = Image.FromFile(full))
                        pbPreview.Image = new Bitmap(img);
                }
                else pbPreview.Image = null;
            }
            catch { pbPreview.Image = null; }
        }

        // ====== Menu Handlers ======
        private void exportToolStripMenuItem_Click(object sender, EventArgs e) => DoExportSelected();
        private void exitToolStripMenuItem_Click(object sender, EventArgs e) => Application.Exit();

        // Show recent history for current user
        private void historyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var history = GetUserHistory();
                if (history.Count == 0)
                {
                    MessageBox.Show("Δεν υπάρχει ιστορικό.");
                    return;
                }

                var lines = history.Select(x => $"{x.ViewedAt:dd/MM/yyyy HH:mm} - {x.BeachName} ({x.Region})");
                MessageBox.Show(string.Join(Environment.NewLine, lines),
                    $"Ιστορικό χρήστη: {_user}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Σφάλμα ανάγνωσης ιστορικού: {ex.Message}");
            }
        }

        // About dialog
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using var dlg = new FormAbout();
            dlg.StartPosition = FormStartPosition.CenterParent;
            dlg.ShowDialog(this);
        }

        // ====== Buttons / Search ======

        // Open details dialog for selected beach
        private void btnSearch_Click_1(object sender, EventArgs e)
        {
            if (lvBeaches.SelectedItems.Count == 0)
            {
                MessageBox.Show("Επίλεξε μια παραλία πρώτα!");
                return;
            }

            var b = (Beach)lvBeaches.SelectedItems[0].Tag;
            using var f = new FormBeachDetails(b, _user);
            f.StartPosition = FormStartPosition.CenterParent;
            f.ShowDialog(this);
            _history.Add(b); // local in-session history
        }

        // Live search (empty or >=2 chars)
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text.Length == 0 || txtSearch.Text.Length >= 2)
                LoadBeaches(txtSearch.Text);
        }

        // TTS controls
        private void btnTTSPlay_Click_1(object sender, EventArgs e) => TtsService.SpeakAsync(rtbDescription.Text);
        private void btnTTSStop_Click_1(object sender, EventArgs e) => TtsService.Stop();

        // Start/stop slideshow and sync index with selection
        private void btnSlideshow_Click_1(object sender, EventArgs e)
        {
            if (_current.Count == 0) { MessageBox.Show("Δεν υπάρχουν παραλίες."); return; }

            tmrSlide.Enabled = !tmrSlide.Enabled;
            btnSlideshow.Text = tmrSlide.Enabled ? "Διακοπή προβολής" : "Προβολή διαφανειών";

            if (tmrSlide.Enabled && lvBeaches.SelectedIndices.Count > 0)
            {
                _slideIndex = lvBeaches.SelectedIndices[0];
            }
        }

        // Slideshow tick: move to next beach and update UI selection/preview
        private void tmrSlide_Tick_1(object sender, EventArgs e)
        {
            if (_current.Count == 0)
            {
                tmrSlide.Enabled = false;
                btnSlideshow.Text = "Προβολή διαφανειών";
                return;
            }

            _slideIndex = (_slideIndex + 1) % _current.Count;

            lvBeaches.SelectedIndices.Clear();
            lvBeaches.Items[_slideIndex].Selected = true;
            lvBeaches.Items[_slideIndex].EnsureVisible();

            ShowPreview(_current[_slideIndex]);
        }

        // Clear user's DB history (menu/button)
        private void btnClearHistory_Click(object sender, EventArgs e)
        {
            try
            {
                using (var con = new SQLiteConnection(Db.ConnStr))
                {
                    con.Open();
                    using (var cmd = new SQLiteCommand("DELETE FROM History WHERE Username = @user", con))
                    {
                        cmd.Parameters.AddWithValue("@user", _user);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Καθαρίστηκε το ιστορικό.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Σφάλμα: {ex.Message}");
            }
        }

        private void btnExport_Click_1(object sender, EventArgs e) => DoExportSelected();

        // Export selected beach info to .txt
        private void DoExportSelected()
        {
            if (lvBeaches.SelectedItems.Count == 0) { MessageBox.Show("Επίλεξε μια παραλία."); return; }
            var b = (Beach)lvBeaches.SelectedItems[0].Tag;

            using var sfd = new SaveFileDialog { Filter = "Text|*.txt", FileName = $"{b.Name}.txt" };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                var txt =
$@"Όνομα: {b.Name}
Περιοχή: {b.Region}
Χαρακτηριστικά: {b.Features}

Περιγραφή:
{b.Description}";
                File.WriteAllText(sfd.FileName, txt, Encoding.UTF8);
                MessageBox.Show("Αποθηκεύτηκε.");
            }
        }

        // ====== List/Preview events ======

        // Keep preview in sync with selection; update slideshow index if manual
        private void lvBeaches_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvBeaches.SelectedItems.Count == 0) return;
            var b = (Beach)lvBeaches.SelectedItems[0].Tag;

            if (!tmrSlide.Enabled)
            {
                _slideIndex = lvBeaches.SelectedIndices[0];
            }

            ShowPreview(b);
        }

        // Clicking the preview image opens the details dialog
        private void pbPreview_Click(object sender, EventArgs e)
        {
            if (lvBeaches.SelectedItems.Count == 0) return;
            var b = (Beach)lvBeaches.SelectedItems[0].Tag;
            using var f = new FormBeachDetails(b, _user);
            f.StartPosition = FormStartPosition.CenterParent;
            f.ShowDialog(this);
        }

        // Query last 20 viewed entries for the current user
        private List<(DateTime ViewedAt, string BeachName, string Region)> GetUserHistory()
        {
            var list = new List<(DateTime, string, string)>();
            using (var con = new SQLiteConnection(Db.ConnStr))
            {
                con.Open();
                var sql = @"
            SELECT h.ViewedAt, b.Name, b.Region 
            FROM History h
            JOIN Beaches b ON h.BeachId = b.Id
            WHERE h.Username = @user
            ORDER BY h.ViewedAt DESC
            LIMIT 20";

                using (var cmd = new SQLiteCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@user", _user);
                    using (var r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            var dt = DateTime.Parse(r["ViewedAt"].ToString());
                            list.Add((dt, r["Name"].ToString(), r["Region"].ToString()));
                        }
                    }
                }
            }
            return list;
        }

        // Enable/disable TTS play button based on text presence
        private void rtbDescription_TextChanged(object sender, EventArgs e)
        {
            btnTTSPlay.Enabled = !string.IsNullOrWhiteSpace(rtbDescription.Text);
        }
    }
}


