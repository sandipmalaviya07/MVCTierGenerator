namespace EntityGeneratorMVC
{
    partial class SqlLoginPage
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtServerName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.drpAuthenticationType = new System.Windows.Forms.ComboBox();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtServerPassword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_LoginServer = new System.Windows.Forms.Button();
            this.txtDatabaseName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(85, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = " Server Name";
            // 
            // txtServerName
            // 
            this.txtServerName.Location = new System.Drawing.Point(163, 60);
            this.txtServerName.Name = "txtServerName";
            this.txtServerName.Size = new System.Drawing.Size(176, 20);
            this.txtServerName.TabIndex = 1;
            this.txtServerName.Text = "SANDIP-PC\\SANDIP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(56, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Authentication Type";
            // 
            // drpAuthenticationType
            // 
            this.drpAuthenticationType.FormattingEnabled = true;
            this.drpAuthenticationType.Location = new System.Drawing.Point(163, 92);
            this.drpAuthenticationType.Name = "drpAuthenticationType";
            this.drpAuthenticationType.Size = new System.Drawing.Size(176, 21);
            this.drpAuthenticationType.TabIndex = 3;
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(163, 155);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(176, 20);
            this.txtUserName.TabIndex = 5;
            this.txtUserName.Text = "sandip";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(126, 156);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Login";
            // 
            // txtServerPassword
            // 
            this.txtServerPassword.Location = new System.Drawing.Point(163, 187);
            this.txtServerPassword.Name = "txtServerPassword";
            this.txtServerPassword.Size = new System.Drawing.Size(176, 20);
            this.txtServerPassword.TabIndex = 7;
            this.txtServerPassword.Text = "sandip";
            this.txtServerPassword.UseSystemPasswordChar = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(107, 189);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Password";
            // 
            // btn_LoginServer
            // 
            this.btn_LoginServer.Location = new System.Drawing.Point(164, 224);
            this.btn_LoginServer.Name = "btn_LoginServer";
            this.btn_LoginServer.Size = new System.Drawing.Size(75, 23);
            this.btn_LoginServer.TabIndex = 8;
            this.btn_LoginServer.Text = "Login";
            this.btn_LoginServer.UseVisualStyleBackColor = true;
            this.btn_LoginServer.Click += new System.EventHandler(this.btn_LoginServer_Click);
            // 
            // txtDatabaseName
            // 
            this.txtDatabaseName.Location = new System.Drawing.Point(164, 124);
            this.txtDatabaseName.Name = "txtDatabaseName";
            this.txtDatabaseName.Size = new System.Drawing.Size(175, 20);
            this.txtDatabaseName.TabIndex = 10;
            this.txtDatabaseName.Text = "Database Name";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(75, 127);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Database Name";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Location = new System.Drawing.Point(114, 293);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(201, 15);
            this.label6.TabIndex = 11;
            this.label6.Text = "Copyright Reserved By  Sandip Malaviya";
            // 
            // SqlLoginPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(422, 330);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtDatabaseName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btn_LoginServer);
            this.Controls.Add(this.txtServerPassword);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.drpAuthenticationType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtServerName);
            this.Controls.Add(this.label1);
            this.Name = "SqlLoginPage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SqlLoginPage";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtServerName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox drpAuthenticationType;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtServerPassword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_LoginServer;
        private System.Windows.Forms.TextBox txtDatabaseName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}