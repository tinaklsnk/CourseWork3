
namespace proj
{
    partial class LoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.loginBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelSignUp = new System.Windows.Forms.Label();
            this.passwordBox = new System.Windows.Forms.TextBox();
            this.adminLabel = new System.Windows.Forms.Label();
            this.signInButton = new proj.RJButton();
            this.SuspendLayout();
            // 
            // loginBox
            // 
            this.loginBox.Location = new System.Drawing.Point(152, 126);
            this.loginBox.Name = "loginBox";
            this.loginBox.Size = new System.Drawing.Size(120, 22);
            this.loginBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(78, 126);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Login";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(52, 185);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Password";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(116, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(205, 36);
            this.label3.TabIndex = 5;
            this.label3.Text = "Authorization";
            // 
            // labelSignUp
            // 
            this.labelSignUp.AutoSize = true;
            this.labelSignUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelSignUp.Location = new System.Drawing.Point(179, 423);
            this.labelSignUp.Name = "labelSignUp";
            this.labelSignUp.Size = new System.Drawing.Size(65, 17);
            this.labelSignUp.TabIndex = 6;
            this.labelSignUp.Text = "Sign Up";
            this.labelSignUp.Click += new System.EventHandler(this.labelSignUp_Click);
            this.labelSignUp.MouseEnter += new System.EventHandler(this.labelSignUp_MouseEnter);
            this.labelSignUp.MouseLeave += new System.EventHandler(this.labelSignUp_MouseLeave);
            // 
            // passwordBox
            // 
            this.passwordBox.Location = new System.Drawing.Point(152, 185);
            this.passwordBox.Name = "passwordBox";
            this.passwordBox.Size = new System.Drawing.Size(120, 22);
            this.passwordBox.TabIndex = 7;
            this.passwordBox.UseSystemPasswordChar = true;
            // 
            // adminLabel
            // 
            this.adminLabel.AutoSize = true;
            this.adminLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.adminLabel.Location = new System.Drawing.Point(305, 465);
            this.adminLabel.Name = "adminLabel";
            this.adminLabel.Size = new System.Drawing.Size(113, 17);
            this.adminLabel.TabIndex = 8;
            this.adminLabel.Text = "Sign In as Admin";
            this.adminLabel.Click += new System.EventHandler(this.adminLabel_Click);
            this.adminLabel.MouseEnter += new System.EventHandler(this.adminLabel_MouseEnter);
            this.adminLabel.MouseLeave += new System.EventHandler(this.adminLabel_MouseLeave);
            // 
            // signInButton
            // 
            this.signInButton.BackColor = System.Drawing.Color.White;
            this.signInButton.BackgroundColor = System.Drawing.Color.White;
            this.signInButton.BorderColor = System.Drawing.Color.White;
            this.signInButton.BorderRadius = 15;
            this.signInButton.BorderSize = 0;
            this.signInButton.FlatAppearance.BorderSize = 0;
            this.signInButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.signInButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.signInButton.ForeColor = System.Drawing.Color.Black;
            this.signInButton.Location = new System.Drawing.Point(152, 259);
            this.signInButton.Name = "signInButton";
            this.signInButton.Size = new System.Drawing.Size(120, 46);
            this.signInButton.TabIndex = 12;
            this.signInButton.Text = "Sign In";
            this.signInButton.TextColor = System.Drawing.Color.Black;
            this.signInButton.UseVisualStyleBackColor = false;
            this.signInButton.Click += new System.EventHandler(this.SignIn_Click);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Tan;
            this.ClientSize = new System.Drawing.Size(430, 491);
            this.Controls.Add(this.signInButton);
            this.Controls.Add(this.adminLabel);
            this.Controls.Add(this.passwordBox);
            this.Controls.Add(this.labelSignUp);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.loginBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LoginForm";
            this.Text = "MyApp";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.LoginForm_FormClosed);
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox loginBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelSignUp;
        private System.Windows.Forms.TextBox passwordBox;
        private System.Windows.Forms.Label adminLabel;
        private RJButton signInButton;
    }
}