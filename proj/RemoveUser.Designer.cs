
namespace proj
{
    partial class RemoveUser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RemoveUser));
            this.loginLabel = new System.Windows.Forms.Label();
            this.loginBox = new System.Windows.Forms.TextBox();
            this.buttonRemove = new proj.RJButton();
            this.SuspendLayout();
            // 
            // loginLabel
            // 
            this.loginLabel.AutoSize = true;
            this.loginLabel.Location = new System.Drawing.Point(47, 37);
            this.loginLabel.Name = "loginLabel";
            this.loginLabel.Size = new System.Drawing.Size(43, 17);
            this.loginLabel.TabIndex = 14;
            this.loginLabel.Text = "Login";
            // 
            // loginBox
            // 
            this.loginBox.Location = new System.Drawing.Point(142, 37);
            this.loginBox.Name = "loginBox";
            this.loginBox.Size = new System.Drawing.Size(152, 22);
            this.loginBox.TabIndex = 15;
            // 
            // buttonRemove
            // 
            this.buttonRemove.BackColor = System.Drawing.Color.White;
            this.buttonRemove.BackgroundColor = System.Drawing.Color.White;
            this.buttonRemove.BorderColor = System.Drawing.Color.White;
            this.buttonRemove.BorderRadius = 15;
            this.buttonRemove.BorderSize = 0;
            this.buttonRemove.FlatAppearance.BorderSize = 0;
            this.buttonRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRemove.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonRemove.ForeColor = System.Drawing.Color.Black;
            this.buttonRemove.Location = new System.Drawing.Point(113, 103);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(120, 43);
            this.buttonRemove.TabIndex = 16;
            this.buttonRemove.Text = "Remove";
            this.buttonRemove.TextColor = System.Drawing.Color.Black;
            this.buttonRemove.UseVisualStyleBackColor = false;
            this.buttonRemove.Click += new System.EventHandler(this.Remove_Click);
            // 
            // RemoveUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Tan;
            this.ClientSize = new System.Drawing.Size(368, 191);
            this.Controls.Add(this.buttonRemove);
            this.Controls.Add(this.loginBox);
            this.Controls.Add(this.loginLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RemoveUser";
            this.Text = "MyApp";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label loginLabel;
        private System.Windows.Forms.TextBox loginBox;
        private RJButton buttonRemove;
    }
}