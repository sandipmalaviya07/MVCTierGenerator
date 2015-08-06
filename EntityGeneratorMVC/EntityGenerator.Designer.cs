namespace EntityGeneratorMVC
{
    partial class EntityGenerator
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
            this.gridviewDatabaseTable = new System.Windows.Forms.DataGridView();
            this.checkboxEntityGenerator = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNameSpacetxt = new System.Windows.Forms.TextBox();
            this.btnGenerator = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSaveLocation = new System.Windows.Forms.TextBox();
            this.btnBrowser = new System.Windows.Forms.Button();
            this.saveFileLocation = new System.Windows.Forms.FolderBrowserDialog();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gridviewDatabaseTable)).BeginInit();
            this.SuspendLayout();
            // 
            // gridviewDatabaseTable
            // 
            this.gridviewDatabaseTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridviewDatabaseTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.checkboxEntityGenerator});
            this.gridviewDatabaseTable.Location = new System.Drawing.Point(12, 75);
            this.gridviewDatabaseTable.Name = "gridviewDatabaseTable";
            this.gridviewDatabaseTable.Size = new System.Drawing.Size(337, 258);
            this.gridviewDatabaseTable.TabIndex = 0;
            // 
            // checkboxEntityGenerator
            // 
            this.checkboxEntityGenerator.HeaderText = "Entity Generator";
            this.checkboxEntityGenerator.Name = "checkboxEntityGenerator";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Name Space";
            // 
            // txtNameSpacetxt
            // 
            this.txtNameSpacetxt.Location = new System.Drawing.Point(87, 16);
            this.txtNameSpacetxt.Name = "txtNameSpacetxt";
            this.txtNameSpacetxt.Size = new System.Drawing.Size(230, 20);
            this.txtNameSpacetxt.TabIndex = 2;
            // 
            // btnGenerator
            // 
            this.btnGenerator.Location = new System.Drawing.Point(223, 348);
            this.btnGenerator.Name = "btnGenerator";
            this.btnGenerator.Size = new System.Drawing.Size(126, 23);
            this.btnGenerator.TabIndex = 3;
            this.btnGenerator.Text = "Generat Entity Model";
            this.btnGenerator.UseVisualStyleBackColor = true;
            this.btnGenerator.Click += new System.EventHandler(this.btnGenerator_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Save File";
            // 
            // txtSaveLocation
            // 
            this.txtSaveLocation.Location = new System.Drawing.Point(87, 49);
            this.txtSaveLocation.Name = "txtSaveLocation";
            this.txtSaveLocation.ReadOnly = true;
            this.txtSaveLocation.Size = new System.Drawing.Size(149, 20);
            this.txtSaveLocation.TabIndex = 5;
            // 
            // btnBrowser
            // 
            this.btnBrowser.Location = new System.Drawing.Point(242, 47);
            this.btnBrowser.Name = "btnBrowser";
            this.btnBrowser.Size = new System.Drawing.Size(75, 23);
            this.btnBrowser.TabIndex = 6;
            this.btnBrowser.Text = "Open";
            this.btnBrowser.UseVisualStyleBackColor = true;
            this.btnBrowser.Click += new System.EventHandler(this.btnBrowser_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Location = new System.Drawing.Point(72, 374);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(201, 15);
            this.label6.TabIndex = 12;
            this.label6.Text = "Copyright Reserved By  Sandip Malaviya";
            // 
            // EntityGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(361, 401);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnBrowser);
            this.Controls.Add(this.txtSaveLocation);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnGenerator);
            this.Controls.Add(this.txtNameSpacetxt);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gridviewDatabaseTable);
            this.Name = "EntityGenerator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EntityGenerator";
            ((System.ComponentModel.ISupportInitialize)(this.gridviewDatabaseTable)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView gridviewDatabaseTable;
        private System.Windows.Forms.DataGridViewCheckBoxColumn checkboxEntityGenerator;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNameSpacetxt;
        private System.Windows.Forms.Button btnGenerator;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSaveLocation;
        private System.Windows.Forms.Button btnBrowser;
        private System.Windows.Forms.FolderBrowserDialog saveFileLocation;
        private System.Windows.Forms.Label label6;
    }
}