namespace GreekBeachesGuide.Forms
{
    partial class FormBeachDetails
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
            pictureBox = new PictureBox();
            richTextBox = new RichTextBox();
            btnSound = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            SuspendLayout();
            // 
            // pictureBox
            // 
            pictureBox.Location = new Point(35, 47);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new Size(1633, 700);
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox.TabIndex = 0;
            pictureBox.TabStop = false;
            // 
            // richTextBox
            // 
            richTextBox.Location = new Point(35, 783);
            richTextBox.Name = "richTextBox";
            richTextBox.Size = new Size(694, 157);
            richTextBox.TabIndex = 1;
            richTextBox.Text = "";
            // 
            // btnSound
            // 
            btnSound.Location = new Point(817, 783);
            btnSound.Name = "btnSound";
            btnSound.Size = new Size(165, 75);
            btnSound.TabIndex = 2;
            btnSound.Text = "Αναπαραγωγή";
            btnSound.UseVisualStyleBackColor = true;
            btnSound.Click += btnSound_Click;
            // 
            // FormBeachDetails
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1695, 1004);
            Controls.Add(btnSound);
            Controls.Add(richTextBox);
            Controls.Add(pictureBox);
            Name = "FormBeachDetails";
            Text = "FormBeachDetails";
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureBox;
        private RichTextBox richTextBox;
        private Button btnSound;
    }
}