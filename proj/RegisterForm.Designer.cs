
namespace proj
{
    partial class RegisterForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegisterForm));
            this.passwordBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.loginLabel = new System.Windows.Forms.Label();
            this.loginBox = new System.Windows.Forms.TextBox();
            this.buttonSignUp = new proj.RJButton();
            this.SuspendLayout();
            // 
            // passwordBox
            // 
            this.passwordBox.Location = new System.Drawing.Point(140, 184);
            this.passwordBox.Name = "passwordBox";
            this.passwordBox.Size = new System.Drawing.Size(156, 22);
            this.passwordBox.TabIndex = 14;
            this.passwordBox.UseSystemPasswordChar = true;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(109, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(187, 36);
            this.label3.TabIndex = 12;
            this.label3.Text = "Registration";
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Location = new System.Drawing.Point(40, 184);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(69, 17);
            this.passwordLabel.TabIndex = 10;
            this.passwordLabel.Text = "Password";
            // 
            // loginLabel
            // 
            this.loginLabel.AutoSize = true;
            this.loginLabel.Location = new System.Drawing.Point(66, 125);
            this.loginLabel.Name = "loginLabel";
            this.loginLabel.Size = new System.Drawing.Size(43, 17);
            this.loginLabel.TabIndex = 9;
            this.loginLabel.Text = "Login";
            // 
            // loginBox
            // 
            this.loginBox.Location = new System.Drawing.Point(140, 125);
            this.loginBox.Name = "loginBox";
            this.loginBox.Size = new System.Drawing.Size(156, 22);
            this.loginBox.TabIndex = 8;
            // 
            // buttonSignUp
            // 
            this.buttonSignUp.BackColor = System.Drawing.Color.White;
            this.buttonSignUp.BackgroundColor = System.Drawing.Color.White;
            this.buttonSignUp.BorderColor = System.Drawing.Color.White;
            this.buttonSignUp.BorderRadius = 15;
            this.buttonSignUp.BorderSize = 0;
            this.buttonSignUp.FlatAppearance.BorderSize = 0;
            this.buttonSignUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSignUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonSignUp.ForeColor = System.Drawing.Color.Black;
            this.buttonSignUp.Location = new System.Drawing.Point(140, 278);
            this.buttonSignUp.Name = "buttonSignUp";
            this.buttonSignUp.Size = new System.Drawing.Size(120, 43);
            this.buttonSignUp.TabIndex = 15;
            this.buttonSignUp.Text = "Sign Up";
            this.buttonSignUp.TextColor = System.Drawing.Color.Black;
            this.buttonSignUp.UseVisualStyleBackColor = false;
            this.buttonSignUp.Click += new System.EventHandler(this.SignUp_Click);
            // 
            // RegisterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Tan;
            this.ClientSize = new System.Drawing.Size(400, 383);
            this.Controls.Add(this.buttonSignUp);
            this.Controls.Add(this.passwordBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.passwordLabel);
            this.Controls.Add(this.loginLabel);
            this.Controls.Add(this.loginBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RegisterForm";
            this.Text = "MyApp";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox passwordBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.Label loginLabel;
        private System.Windows.Forms.TextBox loginBox;
        private RJButton buttonSignUp;
    }
}