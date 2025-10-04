using GreekBeachesGuide.Models;
using GreekBeachesGuide.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GreekBeachesGuide.Forms
{
    public partial class FormMain : Form
    {
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
            LoadBeaches(string.Empty);
            SetupMenuByRole();
            this.FormClosed += (s, e) => Application.Exit();
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

        // ---- Role UI ----
        private void SetupMenuByRole()
        {
            bool isVisitor = string.Equals(_role, "Visitor", StringComparison.OrdinalIgnoreCase);
            if (btnClearHistory != null) btnClearHistory.Visible = !isVisitor;
            // Τα βασικά μενού μένουν ορατά για όλους (Export/History/Help/About/Exit)
        }

        // ---- ListView Columns (μία φορά) ----
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

        // ---- Load data + fill ListView ----
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

        // ---- Preview panel ----
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

            try
            {
                var full = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, b.ImagePath ?? "");
                if (File.Exists(full))
                {
                    using (var img = Image.FromFile(full))
                        pbPreview.Image = new Bitmap(img); // χωρίς file-lock
                }
                else pbPreview.Image = null;
            }
            catch { pbPreview.Image = null; }
        }

        // ====== Menu Handlers ======
        private void exportToolStripMenuItem_Click(object sender, EventArgs e) => DoExportSelected();

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) => Application.Exit();

        private void historyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_history.Count == 0) { MessageBox.Show("Δεν υπάρχει ιστορικό."); return; }
            var lines = _history.TakeLast(20).Select(x => $"{x.Name} — {x.Region}");
            MessageBox.Show(string.Join(Environment.NewLine, lines), "Ιστορικό (τελευταία 20)");
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {

            using var dlg = new FormHelp();
            dlg.StartPosition = FormStartPosition.CenterParent;
            dlg.ShowDialog(this);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using var dlg = new FormAbout();
            dlg.StartPosition = FormStartPosition.CenterParent;
            dlg.ShowDialog(this);
        }

        // ====== Buttons/Search ======
        private void btnSearch_Click_1(object sender, EventArgs e) => LoadBeaches(txtSearch.Text);

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text.Length == 0 || txtSearch.Text.Length >= 2)
                LoadBeaches(txtSearch.Text);
        }

        private void btnTTSPlay_Click_1(object sender, EventArgs e) => TtsService.Speak(rtbDescription.Text);

        private void btnTTSStop_Click_1(object sender, EventArgs e) => TtsService.Stop();

        private void btnSlideshow_Click_1(object sender, EventArgs e)
        {
            if (_current.Count == 0) { MessageBox.Show("Δεν υπάρχουν παραλίες."); return; }
            tmrSlide.Enabled = !tmrSlide.Enabled;
        }

        private void tmrSlide_Tick_1(object sender, EventArgs e)
        {
            if (_current.Count == 0) return;
            _slideIndex = (_slideIndex + 1) % _current.Count;
            ShowPreview(_current[_slideIndex]);
        }

        private void btnClearHistory_Click(object sender, EventArgs e)
        {
            _history.Clear();
            MessageBox.Show("Καθαρίστηκε το ιστορικό.");
        }

        private void btnExport_Click_1(object sender, EventArgs e) => DoExportSelected();

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
        private void lvBeaches_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvBeaches.SelectedItems.Count == 0) return;
            var b = (Beach)lvBeaches.SelectedItems[0].Tag;
            ShowPreview(b);
        }

        private void pbPreview_Click(object sender, EventArgs e)
        {
            if (lvBeaches.SelectedItems.Count == 0) return;
            var b = (Beach)lvBeaches.SelectedItems[0].Tag;
            using var f = new FormBeachDetails(b, _user);
            f.StartPosition = FormStartPosition.CenterParent;
            f.ShowDialog(this);
            _history.Add(b);
        }

        private void rtbDescription_TextChanged(object sender, EventArgs e)
        {
            btnTTSPlay.Enabled = !string.IsNullOrWhiteSpace(rtbDescription.Text);
        }

    }
}

