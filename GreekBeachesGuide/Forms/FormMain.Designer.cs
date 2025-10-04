namespace GreekBeachesGuide.Forms
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            menuStrip = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            exportToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            προβολήToolStripMenuItem = new ToolStripMenuItem();
            historyToolStripMenuItem = new ToolStripMenuItem();
            hpToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            splitContainer1 = new SplitContainer();
            btnExport = new Button();
            btnTTSStop = new Button();
            btnClearHistory = new Button();
            btnSlideshow = new Button();
            btnTTSPlay = new Button();
            lvBeaches = new ListView();
            btnSearch = new Button();
            txtSearch = new TextBox();
            rtbDescription = new RichTextBox();
            lblRegion = new Label();
            lblName = new Label();
            pbPreview = new PictureBox();
            contextMenuStrip = new ContextMenuStrip(components);
            tmrSlide = new System.Windows.Forms.Timer(components);
            button1 = new Button();
            menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbPreview).BeginInit();
            SuspendLayout();
            // 
            // menuStrip
            // 
            menuStrip.ImageScalingSize = new Size(24, 24);
            menuStrip.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, προβολήToolStripMenuItem, hpToolStripMenuItem });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Size = new Size(1348, 33);
            menuStrip.TabIndex = 0;
            menuStrip.Text = "menuStrip";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { exportToolStripMenuItem, exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(85, 29);
            fileToolStripMenuItem.Text = "Αρχείο";
            // 
            // exportToolStripMenuItem
            // 
            exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            exportToolStripMenuItem.Size = new Size(185, 34);
            exportToolStripMenuItem.Text = "Εξαγωγή";
            exportToolStripMenuItem.Click += exportToolStripMenuItem_Click;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(185, 34);
            exitToolStripMenuItem.Text = "Έξοδος";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // προβολήToolStripMenuItem
            // 
            προβολήToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { historyToolStripMenuItem });
            προβολήToolStripMenuItem.Name = "προβολήToolStripMenuItem";
            προβολήToolStripMenuItem.Size = new Size(103, 29);
            προβολήToolStripMenuItem.Text = "Προβολή";
            // 
            // historyToolStripMenuItem
            // 
            historyToolStripMenuItem.Name = "historyToolStripMenuItem";
            historyToolStripMenuItem.Size = new Size(185, 34);
            historyToolStripMenuItem.Text = "Ιστορικό";
            historyToolStripMenuItem.Click += historyToolStripMenuItem_Click;
            // 
            // hpToolStripMenuItem
            // 
            hpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { helpToolStripMenuItem, aboutToolStripMenuItem });
            hpToolStripMenuItem.Name = "hpToolStripMenuItem";
            hpToolStripMenuItem.Size = new Size(94, 29);
            hpToolStripMenuItem.Text = "Βοήθεια";
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new Size(209, 34);
            helpToolStripMenuItem.Text = "Υποστήριξη";
            helpToolStripMenuItem.Click += helpToolStripMenuItem_Click;
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new Size(209, 34);
            aboutToolStripMenuItem.Text = "Σχετικά";
            aboutToolStripMenuItem.Click += aboutToolStripMenuItem_Click;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 33);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(btnExport);
            splitContainer1.Panel1.Controls.Add(btnTTSStop);
            splitContainer1.Panel1.Controls.Add(btnClearHistory);
            splitContainer1.Panel1.Controls.Add(btnSlideshow);
            splitContainer1.Panel1.Controls.Add(btnTTSPlay);
            splitContainer1.Panel1.Controls.Add(lvBeaches);
            splitContainer1.Panel1.Controls.Add(btnSearch);
            splitContainer1.Panel1.Controls.Add(txtSearch);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(button1);
            splitContainer1.Panel2.Controls.Add(rtbDescription);
            splitContainer1.Panel2.Controls.Add(lblRegion);
            splitContainer1.Panel2.Controls.Add(lblName);
            splitContainer1.Panel2.Controls.Add(pbPreview);
            splitContainer1.Size = new Size(1348, 628);
            splitContainer1.SplitterDistance = 448;
            splitContainer1.TabIndex = 1;
            // 
            // btnExport
            // 
            btnExport.Location = new Point(206, 231);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(210, 34);
            btnExport.TabIndex = 7;
            btnExport.Text = "Εξαγωγή";
            btnExport.UseVisualStyleBackColor = true;
            btnExport.Click += btnExport_Click_1;
            // 
            // btnTTSStop
            // 
            btnTTSStop.Location = new Point(28, 231);
            btnTTSStop.Name = "btnTTSStop";
            btnTTSStop.Size = new Size(139, 34);
            btnTTSStop.TabIndex = 6;
            btnTTSStop.Text = "Διακοπή";
            btnTTSStop.UseVisualStyleBackColor = true;
            btnTTSStop.Click += btnTTSStop_Click_1;
            // 
            // btnClearHistory
            // 
            btnClearHistory.Location = new Point(206, 156);
            btnClearHistory.Name = "btnClearHistory";
            btnClearHistory.Size = new Size(210, 34);
            btnClearHistory.TabIndex = 5;
            btnClearHistory.Text = "Εκκαθάριση ιστορικού";
            btnClearHistory.UseVisualStyleBackColor = true;
            btnClearHistory.Click += btnClearHistory_Click;
            // 
            // btnSlideshow
            // 
            btnSlideshow.Location = new Point(206, 80);
            btnSlideshow.Name = "btnSlideshow";
            btnSlideshow.Size = new Size(210, 34);
            btnSlideshow.TabIndex = 4;
            btnSlideshow.Text = "Προβολή διαφανειών";
            btnSlideshow.UseVisualStyleBackColor = true;
            btnSlideshow.Click += btnSlideshow_Click_1;
            // 
            // btnTTSPlay
            // 
            btnTTSPlay.Location = new Point(28, 156);
            btnTTSPlay.Name = "btnTTSPlay";
            btnTTSPlay.Size = new Size(139, 34);
            btnTTSPlay.TabIndex = 3;
            btnTTSPlay.Text = "Αναπαραγωγή";
            btnTTSPlay.UseVisualStyleBackColor = true;
            btnTTSPlay.Click += btnTTSPlay_Click_1;
            // 
            // lvBeaches
            // 
            lvBeaches.FullRowSelect = true;
            lvBeaches.Location = new Point(28, 318);
            lvBeaches.Name = "lvBeaches";
            lvBeaches.Size = new Size(388, 282);
            lvBeaches.TabIndex = 2;
            lvBeaches.UseCompatibleStateImageBehavior = false;
            lvBeaches.View = View.Details;
            lvBeaches.SelectedIndexChanged += lvBeaches_SelectedIndexChanged;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(28, 80);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(139, 32);
            btnSearch.TabIndex = 1;
            btnSearch.Text = "Φύγαμε";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click_1;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(28, 32);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(388, 31);
            txtSearch.TabIndex = 0;
            txtSearch.TextChanged += txtSearch_TextChanged;
            // 
            // rtbDescription
            // 
            rtbDescription.Location = new Point(14, 505);
            rtbDescription.Name = "rtbDescription";
            rtbDescription.ReadOnly = true;
            rtbDescription.Size = new Size(813, 95);
            rtbDescription.TabIndex = 3;
            rtbDescription.Text = "";
            rtbDescription.TextChanged += rtbDescription_TextChanged;
            // 
            // lblRegion
            // 
            lblRegion.AutoSize = true;
            lblRegion.Location = new Point(180, 466);
            lblRegion.Name = "lblRegion";
            lblRegion.Size = new Size(80, 25);
            lblRegion.TabIndex = 2;
            lblRegion.Text = "Περιοχή";
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new Point(14, 466);
            lblName.Name = "lblName";
            lblName.Size = new Size(151, 25);
            lblName.TabIndex = 1;
            lblName.Text = "Όνομα Παραλίας";
            // 
            // pbPreview
            // 
            pbPreview.Location = new Point(14, 32);
            pbPreview.Name = "pbPreview";
            pbPreview.Size = new Size(813, 414);
            pbPreview.SizeMode = PictureBoxSizeMode.Zoom;
            pbPreview.TabIndex = 0;
            pbPreview.TabStop = false;
            pbPreview.Click += pbPreview_Click;
            // 
            // contextMenuStrip
            // 
            contextMenuStrip.ImageScalingSize = new Size(24, 24);
            contextMenuStrip.Name = "contextMenuStrip";
            contextMenuStrip.Size = new Size(61, 4);
            // 
            // tmrSlide
            // 
            tmrSlide.Interval = 3000;
            tmrSlide.Tick += tmrSlide_Tick_1;
            // 
            // button1
            // 
            button1.Location = new Point(158, 185);
            button1.Name = "button1";
            button1.Size = new Size(112, 34);
            button1.TabIndex = 4;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1348, 661);
            Controls.Add(splitContainer1);
            Controls.Add(menuStrip);
            MainMenuStrip = menuStrip;
            Name = "FormMain";
            Text = "FormMain";
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pbPreview).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem exportToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem προβολήToolStripMenuItem;
        private ToolStripMenuItem historyToolStripMenuItem;
        private ToolStripMenuItem hpToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private SplitContainer splitContainer1;
        private TextBox txtSearch;
        private ContextMenuStrip contextMenuStrip;
        private ListView lvBeaches;
        private Button btnSearch;
        private RichTextBox rtbDescription;
        private Label lblRegion;
        private Label lblName;
        private PictureBox pbPreview;
        private Button btnExport;
        private Button btnTTSStop;
        private Button btnClearHistory;
        private Button btnSlideshow;
        private Button btnTTSPlay;
        private System.Windows.Forms.Timer tmrSlide;
        private Button button1;
    }
}