namespace ivrJournal
{
    partial class PsychoListForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PsychoListForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.bnClose = new System.Windows.Forms.Button();
            this.bnShow = new System.Windows.Forms.Button();
            this.bnEdit = new System.Windows.Forms.Button();
            this.bnAdd = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgPsycho = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgPsycho)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.bnClose);
            this.panel1.Controls.Add(this.bnShow);
            this.panel1.Controls.Add(this.bnEdit);
            this.panel1.Controls.Add(this.bnAdd);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(406, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(86, 366);
            this.panel1.TabIndex = 0;
            // 
            // bnClose
            // 
            this.bnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bnClose.Location = new System.Drawing.Point(6, 331);
            this.bnClose.Name = "bnClose";
            this.bnClose.Size = new System.Drawing.Size(75, 23);
            this.bnClose.TabIndex = 3;
            this.bnClose.Text = "Закрыть";
            this.bnClose.UseVisualStyleBackColor = true;
            this.bnClose.Click += new System.EventHandler(this.bnClose_Click);
            // 
            // bnShow
            // 
            this.bnShow.Location = new System.Drawing.Point(6, 70);
            this.bnShow.Name = "bnShow";
            this.bnShow.Size = new System.Drawing.Size(75, 23);
            this.bnShow.TabIndex = 2;
            this.bnShow.Text = "Показать";
            this.bnShow.UseVisualStyleBackColor = true;
            this.bnShow.Click += new System.EventHandler(this.bnShow_Click);
            // 
            // bnEdit
            // 
            this.bnEdit.Location = new System.Drawing.Point(6, 41);
            this.bnEdit.Name = "bnEdit";
            this.bnEdit.Size = new System.Drawing.Size(75, 23);
            this.bnEdit.TabIndex = 1;
            this.bnEdit.Text = "Изменить";
            this.bnEdit.UseVisualStyleBackColor = true;
            this.bnEdit.Click += new System.EventHandler(this.bnEdit_Click);
            // 
            // bnAdd
            // 
            this.bnAdd.Location = new System.Drawing.Point(6, 12);
            this.bnAdd.Name = "bnAdd";
            this.bnAdd.Size = new System.Drawing.Size(75, 23);
            this.bnAdd.TabIndex = 0;
            this.bnAdd.Text = "Добавить";
            this.bnAdd.UseVisualStyleBackColor = true;
            this.bnAdd.Click += new System.EventHandler(this.bnAdd_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dgPsycho);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(5);
            this.panel2.Size = new System.Drawing.Size(406, 366);
            this.panel2.TabIndex = 1;
            // 
            // dgPsycho
            // 
            this.dgPsycho.AllowUserToAddRows = false;
            this.dgPsycho.AllowUserToDeleteRows = false;
            this.dgPsycho.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgPsycho.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgPsycho.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgPsycho.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgPsycho.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgPsycho.Location = new System.Drawing.Point(5, 5);
            this.dgPsycho.Name = "dgPsycho";
            this.dgPsycho.ReadOnly = true;
            this.dgPsycho.RowHeadersWidth = 10;
            this.dgPsycho.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgPsycho.ShowCellErrors = false;
            this.dgPsycho.ShowCellToolTips = false;
            this.dgPsycho.ShowEditingIcon = false;
            this.dgPsycho.ShowRowErrors = false;
            this.dgPsycho.Size = new System.Drawing.Size(396, 356);
            this.dgPsycho.TabIndex = 0;
            // 
            // PsychoListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bnClose;
            this.ClientSize = new System.Drawing.Size(492, 366);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(500, 400);
            this.Name = "PsychoListForm";
            this.Text = "Перечень документов";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgPsycho)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dgPsycho;
        private System.Windows.Forms.Button bnShow;
        private System.Windows.Forms.Button bnEdit;
        private System.Windows.Forms.Button bnAdd;
        private System.Windows.Forms.Button bnClose;
    }
}