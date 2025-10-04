namespace GreekBeachesGuide.Forms
{
    partial class FormLogin
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
            lblUser = new Label();
            lblPass = new Label();
            txtUser = new TextBox();
            txtPass = new TextBox();
            btnLogin = new Button();
            btnGuest = new Button();
            SuspendLayout();
            // 
            // lblUser
            // 
            lblUser.AutoSize = true;
            lblUser.Location = new Point(54, 46);
            lblUser.Name = "lblUser";
            lblUser.Size = new Size(133, 25);
            lblUser.TabIndex = 0;
            lblUser.Text = "Όνομα Χρήστη";
            // 
            // lblPass
            // 
            lblPass.AutoSize = true;
            lblPass.Location = new Point(54, 110);
            lblPass.Name = "lblPass";
            lblPass.Size = new Size(147, 25);
            lblPass.TabIndex = 1;
            lblPass.Text = "Κωδικός Χρήστη";
            // 
            // txtUser
            // 
            txtUser.Location = new Point(218, 46);
            txtUser.Name = "txtUser";
            txtUser.Size = new Size(228, 31);
            txtUser.TabIndex = 2;
            // 
            // txtPass
            // 
            txtPass.Location = new Point(218, 104);
            txtPass.Name = "txtPass";
            txtPass.Size = new Size(228, 31);
            txtPass.TabIndex = 3;
            txtPass.UseSystemPasswordChar = true;
            // 
            // btnLogin
            // 
            btnLogin.Location = new Point(275, 184);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(112, 52);
            btnLogin.TabIndex = 4;
            btnLogin.Text = "Είσοδος";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += btnLogin_Click;
            // 
            // btnGuest
            // 
            btnGuest.Location = new Point(275, 263);
            btnGuest.Name = "btnGuest";
            btnGuest.Size = new Size(112, 52);
            btnGuest.TabIndex = 5;
            btnGuest.Text = "Επισκέπτης";
            btnGuest.UseVisualStyleBackColor = true;
            btnGuest.Click += btnGuest_Click;
            // 
            // FormLogin
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(645, 382);
            Controls.Add(btnGuest);
            Controls.Add(btnLogin);
            Controls.Add(txtPass);
            Controls.Add(txtUser);
            Controls.Add(lblPass);
            Controls.Add(lblUser);
            Name = "FormLogin";
            Text = "FormLogin";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblUser;
        private Label lblPass;
        private TextBox txtUser;
        private TextBox txtPass;
        private Button btnLogin;
        private Button btnGuest;
    }
}