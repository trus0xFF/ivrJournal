namespace ivrJournal
{
    partial class ProfilePsychoForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProfilePsychoForm));
            this.bnLoad = new System.Windows.Forms.Button();
            this.bnOpen = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.bnSave = new System.Windows.Forms.Button();
            this.bnClose = new System.Windows.Forms.Button();
            this.dtDateDoc = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // bnLoad
            // 
            this.bnLoad.Location = new System.Drawing.Point(245, 90);
            this.bnLoad.Name = "bnLoad";
            this.bnLoad.Size = new System.Drawing.Size(75, 23);
            this.bnLoad.TabIndex = 2;
            this.bnLoad.Text = "Загрузить";
            this.bnLoad.UseVisualStyleBackColor = true;
            this.bnLoad.Click += new System.EventHandler(this.bnLoad_Click);
            // 
            // bnOpen
            // 
            this.bnOpen.Location = new System.Drawing.Point(326, 90);
            this.bnOpen.Name = "bnOpen";
            this.bnOpen.Size = new System.Drawing.Size(75, 23);
            this.bnOpen.TabIndex = 3;
            this.bnOpen.Text = "Показать";
            this.bnOpen.UseVisualStyleBackColor = true;
            this.bnOpen.Click += new System.EventHandler(this.bnOpen_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(403, 28);
            this.label1.TabIndex = 4;
            this.label1.Text = "Произвольные документы на осужденного загружается в виде файла в формате Word";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(135, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Краткое описание файла";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Дата файла";
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(8, 61);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(394, 20);
            this.tbName.TabIndex = 7;
            // 
            // bnSave
            // 
            this.bnSave.Location = new System.Drawing.Point(245, 119);
            this.bnSave.Name = "bnSave";
            this.bnSave.Size = new System.Drawing.Size(75, 23);
            this.bnSave.TabIndex = 9;
            this.bnSave.Text = "Сохранить";
            this.bnSave.UseVisualStyleBackColor = true;
            this.bnSave.Click += new System.EventHandler(this.bnSave_Click);
            // 
            // bnClose
            // 
            this.bnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bnClose.Location = new System.Drawing.Point(326, 119);
            this.bnClose.Name = "bnClose";
            this.bnClose.Size = new System.Drawing.Size(75, 23);
            this.bnClose.TabIndex = 10;
            this.bnClose.Text = "Закрыть";
            this.bnClose.UseVisualStyleBackColor = true;
            this.bnClose.Click += new System.EventHandler(this.bnClose_Click);
            // 
            // dtDateDoc
            // 
            this.dtDateDoc.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtDateDoc.Location = new System.Drawing.Point(109, 93);
            this.dtDateDoc.Name = "dtDateDoc";
            this.dtDateDoc.Size = new System.Drawing.Size(124, 20);
            this.dtDateDoc.TabIndex = 12;
            // 
            // ProfilePsychoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bnClose;
            this.ClientSize = new System.Drawing.Size(412, 146);
            this.Controls.Add(this.dtDateDoc);
            this.Controls.Add(this.bnClose);
            this.Controls.Add(this.bnSave);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bnOpen);
            this.Controls.Add(this.bnLoad);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(420, 180);
            this.Name = "ProfilePsychoForm";
            this.Text = "Произвольный документ";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bnLoad;
        private System.Windows.Forms.Button bnOpen;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Button bnSave;
        private System.Windows.Forms.Button bnClose;
        private System.Windows.Forms.DateTimePicker dtDateDoc;
    }
}