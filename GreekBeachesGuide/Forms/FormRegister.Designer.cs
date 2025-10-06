namespace GreekBeachesGuide.Forms
{
    partial class FormRegister
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
            label1 = new Label();
            label2 = new Label();
            txtUsername = new TextBox();
            txtPassword = new TextBox();
            btnCreate = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(18, 58);
            label1.Name = "label1";
            label1.Size = new Size(132, 25);
            label1.TabIndex = 0;
            label1.Text = "Όνομα χρήστη";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(69, 123);
            label2.Name = "label2";
            label2.Size = new Size(81, 25);
            label2.TabIndex = 1;
            label2.Text = "Κωδικός";
            // 
            // txtUsername
            // 
            txtUsername.Location = new Point(202, 55);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(150, 31);
            txtUsername.TabIndex = 2;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(202, 117);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(150, 31);
            txtPassword.TabIndex = 3;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // btnCreate
            // 
            btnCreate.Location = new Point(202, 198);
            btnCreate.Name = "btnCreate";
            btnCreate.Size = new Size(150, 43);
            btnCreate.TabIndex = 4;
            btnCreate.Text = "Δημιουργία";
            btnCreate.UseVisualStyleBackColor = true;
            btnCreate.Click += btnCreate_Click;

            // 
            // FormRegister
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(493, 313);
            Controls.Add(btnCreate);
            Controls.Add(txtPassword);
            Controls.Add(txtUsername);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "FormRegister";
            Text = "FormRegister";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox txtUsername;
        private TextBox txtPassword;
        private Button btnCreate;
    }
}