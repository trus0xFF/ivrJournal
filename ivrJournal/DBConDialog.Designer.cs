namespace ivrJournal
{
    partial class DBConDialog
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
            this.tbDBPath = new System.Windows.Forms.TextBox();
            this.bnSave = new System.Windows.Forms.Button();
            this.bnCancel = new System.Windows.Forms.Button();
            this.bnFind = new System.Windows.Forms.Button();
            this.bnCheckCon = new System.Windows.Forms.Button();
            this.lblPath = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tbDBPath
            // 
            this.tbDBPath.Location = new System.Drawing.Point(82, 21);
            this.tbDBPath.Name = "tbDBPath";
            this.tbDBPath.Size = new System.Drawing.Size(350, 20);
            this.tbDBPath.TabIndex = 0;
            // 
            // bnSave
            // 
            this.bnSave.Location = new System.Drawing.Point(316, 62);
            this.bnSave.Name = "bnSave";
            this.bnSave.Size = new System.Drawing.Size(75, 25);
            this.bnSave.TabIndex = 1;
            this.bnSave.Text = "Сохранить";
            this.bnSave.UseVisualStyleBackColor = true;
            this.bnSave.Click += new System.EventHandler(this.bnSave_Click);
            // 
            // bnCancel
            // 
            this.bnCancel.Location = new System.Drawing.Point(397, 62);
            this.bnCancel.Name = "bnCancel";
            this.bnCancel.Size = new System.Drawing.Size(75, 25);
            this.bnCancel.TabIndex = 2;
            this.bnCancel.Text = "Отмена";
            this.bnCancel.UseVisualStyleBackColor = true;
            this.bnCancel.Click += new System.EventHandler(this.bnCancel_Click);
            // 
            // bnFind
            // 
            this.bnFind.Location = new System.Drawing.Point(439, 20);
            this.bnFind.Name = "bnFind";
            this.bnFind.Size = new System.Drawing.Size(25, 23);
            this.bnFind.TabIndex = 3;
            this.bnFind.Text = "...";
            this.bnFind.UseVisualStyleBackColor = true;
            this.bnFind.Click += new System.EventHandler(this.bnFind_Click);
            // 
            // bnCheckCon
            // 
            this.bnCheckCon.Location = new System.Drawing.Point(20, 62);
            this.bnCheckCon.Name = "bnCheckCon";
            this.bnCheckCon.Size = new System.Drawing.Size(137, 25);
            this.bnCheckCon.TabIndex = 4;
            this.bnCheckCon.Text = "Проверить соединение";
            this.bnCheckCon.UseVisualStyleBackColor = true;
            this.bnCheckCon.Click += new System.EventHandler(this.bnCheckCon_Click);
            // 
            // lblPath
            // 
            this.lblPath.AutoSize = true;
            this.lblPath.Location = new System.Drawing.Point(17, 24);
            this.lblPath.Name = "lblPath";
            this.lblPath.Size = new System.Drawing.Size(62, 13);
            this.lblPath.TabIndex = 5;
            this.lblPath.Text = "Путь к БД:";
            // 
            // DBConDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 102);
            this.Controls.Add(this.lblPath);
            this.Controls.Add(this.bnCheckCon);
            this.Controls.Add(this.bnFind);
            this.Controls.Add(this.bnCancel);
            this.Controls.Add(this.bnSave);
            this.Controls.Add(this.tbDBPath);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DBConDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Соединение с базой данных";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbDBPath;
        private System.Windows.Forms.Button bnSave;
        private System.Windows.Forms.Button bnCancel;
        private System.Windows.Forms.Button bnFind;
        private System.Windows.Forms.Button bnCheckCon;
        private System.Windows.Forms.Label lblPath;
    }
}