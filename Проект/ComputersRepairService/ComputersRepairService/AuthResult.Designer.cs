namespace ComputersRepairService
{
    partial class AuthResult
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.nameLabel = new System.Windows.Forms.Label();
            this.surnameLabel = new System.Windows.Forms.Label();
            this.roleLabel = new System.Windows.Forms.Label();
            this.EnterBTN = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(30, 29);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(186, 187);
            this.panel1.TabIndex = 0;
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(248, 46);
            this.nameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(34, 15);
            this.nameLabel.TabIndex = 1;
            this.nameLabel.Text = "Имя:";
            // 
            // surnameLabel
            // 
            this.surnameLabel.AutoSize = true;
            this.surnameLabel.Location = new System.Drawing.Point(248, 82);
            this.surnameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.surnameLabel.Name = "surnameLabel";
            this.surnameLabel.Size = new System.Drawing.Size(62, 15);
            this.surnameLabel.TabIndex = 2;
            this.surnameLabel.Text = "Фамилия:";
            // 
            // roleLabel
            // 
            this.roleLabel.AutoSize = true;
            this.roleLabel.Location = new System.Drawing.Point(248, 123);
            this.roleLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.roleLabel.Name = "roleLabel";
            this.roleLabel.Size = new System.Drawing.Size(37, 15);
            this.roleLabel.TabIndex = 3;
            this.roleLabel.Text = "Роль:";
            // 
            // EnterBTN
            // 
            this.EnterBTN.Location = new System.Drawing.Point(186, 285);
            this.EnterBTN.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.EnterBTN.Name = "EnterBTN";
            this.EnterBTN.Size = new System.Drawing.Size(88, 27);
            this.EnterBTN.TabIndex = 4;
            this.EnterBTN.Text = "Войти";
            this.EnterBTN.UseVisualStyleBackColor = true;
            this.EnterBTN.Click += new System.EventHandler(this.button1_Click);
            // 
            // AuthResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(465, 368);
            this.Controls.Add(this.EnterBTN);
            this.Controls.Add(this.roleLabel);
            this.Controls.Add(this.surnameLabel);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Bookman Old Style", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "AuthResult";
            this.Text = "Профиль";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Label surnameLabel;
        private System.Windows.Forms.Label roleLabel;
        private System.Windows.Forms.Button EnterBTN;
    }
}