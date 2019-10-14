namespace ivrJournal
{
    partial class RepListForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RepListForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.dgListField = new System.Windows.Forms.DataGridView();
            this.dgWhere = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.bnShow = new System.Windows.Forms.Button();
            this.bnExcell = new System.Windows.Forms.Button();
            this.bnCancel = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgExport = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.lbRecords = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgListField)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgWhere)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgExport)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(612, 242);
            this.panel1.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.dgListField);
            this.panel4.Controls.Add(this.dgWhere);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.MinimumSize = new System.Drawing.Size(478, 242);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(478, 242);
            this.panel4.TabIndex = 8;
            // 
            // dgListField
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgListField.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgListField.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgListField.Location = new System.Drawing.Point(12, 33);
            this.dgListField.Name = "dgListField";
            this.dgListField.RowHeadersWidth = 20;
            this.dgListField.Size = new System.Drawing.Size(460, 74);
            this.dgListField.TabIndex = 0;
            // 
            // dgWhere
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgWhere.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgWhere.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgWhere.Location = new System.Drawing.Point(12, 136);
            this.dgWhere.Name = "dgWhere";
            this.dgWhere.RowHeadersWidth = 20;
            this.dgWhere.Size = new System.Drawing.Size(460, 86);
            this.dgWhere.TabIndex = 5;
            this.dgWhere.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgWhere_CellEndEdit);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(16, 228);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Результаты:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(16, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(221, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Выберите поля выводимые в отчет:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(16, 118);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(229, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Задайте условия для отбора в отчет:";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lbRecords);
            this.panel3.Controls.Add(this.bnShow);
            this.panel3.Controls.Add(this.bnExcell);
            this.panel3.Controls.Add(this.bnCancel);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(478, 0);
            this.panel3.MinimumSize = new System.Drawing.Size(134, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(134, 242);
            this.panel3.TabIndex = 7;
            // 
            // bnShow
            // 
            this.bnShow.Location = new System.Drawing.Point(4, 12);
            this.bnShow.Name = "bnShow";
            this.bnShow.Size = new System.Drawing.Size(122, 25);
            this.bnShow.TabIndex = 4;
            this.bnShow.Text = "Подготовить список";
            this.bnShow.UseVisualStyleBackColor = true;
            this.bnShow.Click += new System.EventHandler(this.bnShow_Click);
            // 
            // bnExcell
            // 
            this.bnExcell.Location = new System.Drawing.Point(4, 43);
            this.bnExcell.Name = "bnExcell";
            this.bnExcell.Size = new System.Drawing.Size(122, 23);
            this.bnExcell.TabIndex = 2;
            this.bnExcell.Text = "Выгрузить в Excell";
            this.bnExcell.UseVisualStyleBackColor = true;
            this.bnExcell.Click += new System.EventHandler(this.bnExcell_Click);
            // 
            // bnCancel
            // 
            this.bnCancel.Location = new System.Drawing.Point(51, 72);
            this.bnCancel.Name = "bnCancel";
            this.bnCancel.Size = new System.Drawing.Size(75, 23);
            this.bnCancel.TabIndex = 3;
            this.bnCancel.Text = "Отмена";
            this.bnCancel.UseVisualStyleBackColor = true;
            this.bnCancel.Click += new System.EventHandler(this.bnCancel_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dgExport);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 242);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(6);
            this.panel2.Size = new System.Drawing.Size(612, 210);
            this.panel2.TabIndex = 1;
            // 
            // dgExport
            // 
            this.dgExport.AllowUserToAddRows = false;
            this.dgExport.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgExport.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgExport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgExport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgExport.Location = new System.Drawing.Point(6, 6);
            this.dgExport.Name = "dgExport";
            this.dgExport.ReadOnly = true;
            this.dgExport.RowHeadersWidth = 20;
            this.dgExport.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgExport.Size = new System.Drawing.Size(600, 198);
            this.dgExport.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(345, 226);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(127, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Подготовлено записей:";
            // 
            // lbRecords
            // 
            this.lbRecords.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbRecords.AutoSize = true;
            this.lbRecords.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbRecords.Location = new System.Drawing.Point(6, 226);
            this.lbRecords.Name = "lbRecords";
            this.lbRecords.Size = new System.Drawing.Size(14, 13);
            this.lbRecords.TabIndex = 5;
            this.lbRecords.Text = "0";
            // 
            // RepListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(612, 452);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(620, 400);
            this.Name = "RepListForm";
            this.Text = "Вывод списка осужденных";
            this.panel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgListField)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgWhere)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgExport)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dgExport;
        private System.Windows.Forms.Button bnShow;
        private System.Windows.Forms.Button bnCancel;
        private System.Windows.Forms.Button bnExcell;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgListField;
        private System.Windows.Forms.DataGridView dgWhere;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbRecords;
    }
}