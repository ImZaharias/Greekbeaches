namespace GreekBeachesGuide.Forms
{
    partial class FormHelp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormHelp));
            label = new Label();
            richTextBox = new RichTextBox();
            SuspendLayout();
            // 
            // label
            // 
            label.AutoSize = true;
            label.Font = new Font("Segoe UI", 12F);
            label.Location = new Point(266, 22);
            label.Name = "label";
            label.Size = new Size(187, 32);
            label.TabIndex = 0;
            label.Text = "Οδηγίες χρήσης";
            // 
            // richTextBox
            // 
            richTextBox.Location = new Point(31, 72);
            richTextBox.Name = "richTextBox";
            richTextBox.ReadOnly = true;
            richTextBox.Size = new Size(712, 687);
            richTextBox.TabIndex = 1;
            richTextBox.Text = resources.GetString("richTextBox.Text");
            // 
            // FormHelp
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(795, 791);
            Controls.Add(richTextBox);
            Controls.Add(label);
            Name = "FormHelp";
            Text = "FormHelp";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label;
        private RichTextBox richTextBox;
    }
}