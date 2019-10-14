namespace ivrJournal
{
    partial class KartArchiveForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KartArchiveForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.bnFindDel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbLastName = new System.Windows.Forms.TextBox();
            this.bnFind = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.labelHelp = new System.Windows.Forms.Label();
            this.bnClose = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.bnDel = new System.Windows.Forms.Button();
            this.bnUnDelete = new System.Windows.Forms.Button();
            this.bnReports = new System.Windows.Forms.Button();
            this.ctMenuReports = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiƒневник»¬–Word = new System.Windows.Forms.ToolStripMenuItem();
            this.дневник»¬–полныйToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi—правкаќѕоощрени€х»¬зыскани€х¬Word = new System.Windows.Forms.ToolStripMenuItem();
            this.отчетToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel4 = new System.Windows.Forms.Panel();
            this.dgArchive = new System.Windows.Forms.DataGridView();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.ctMenuReports.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgArchive)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.bnFindDel);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.tbLastName);
            this.panel1.Controls.Add(this.bnFind);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(572, 33);
            this.panel1.TabIndex = 0;
            // 
            // bnFindDel
            // 
            this.bnFindDel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bnFindDel.Location = new System.Drawing.Point(546, 6);
            this.bnFindDel.Name = "bnFindDel";
            this.bnFindDel.Size = new System.Drawing.Size(22, 22);
            this.bnFindDel.TabIndex = 22;
            this.bnFindDel.Text = "X";
            this.bnFindDel.UseVisualStyleBackColor = true;
            this.bnFindDel.Click += new System.EventHandler(this.bnFindDel_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(202, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 13);
            this.label1.TabIndex = 23;
            this.label1.Text = "ѕоиск по фамилии:";
            // 
            // tbLastName
            // 
            this.tbLastName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbLastName.Location = new System.Drawing.Point(315, 7);
            this.tbLastName.Name = "tbLastName";
            this.tbLastName.Size = new System.Drawing.Size(188, 20);
            this.tbLastName.TabIndex = 20;
            this.tbLastName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbLastName_KeyDown);
            // 
            // bnFind
            // 
            this.bnFind.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bnFind.Image = global::ivrJournal.Properties.Resources.find;
            this.bnFind.Location = new System.Drawing.Point(510, 6);
            this.bnFind.Name = "bnFind";
            this.bnFind.Size = new System.Drawing.Size(30, 22);
            this.bnFind.TabIndex = 21;
            this.bnFind.UseVisualStyleBackColor = true;
            this.bnFind.Click += new System.EventHandler(this.bnFind_Click);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.labelHelp);
            this.panel2.Controls.Add(this.bnClose);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 334);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(572, 32);
            this.panel2.TabIndex = 1;
            // 
            // labelHelp
            // 
            this.labelHelp.AutoSize = true;
            this.labelHelp.Location = new System.Drawing.Point(11, 9);
            this.labelHelp.Name = "labelHelp";
            this.labelHelp.Size = new System.Drawing.Size(332, 13);
            this.labelHelp.TabIndex = 20;
            this.labelHelp.Text = "‘орма списка осужденных. ¬оспользуйтесь кнопками справа.";
            // 
            // bnClose
            // 
            this.bnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bnClose.Image = ((System.Drawing.Image)(resources.GetObject("bnClose.Image")));
            this.bnClose.Location = new System.Drawing.Point(461, 2);
            this.bnClose.Name = "bnClose";
            this.bnClose.Size = new System.Drawing.Size(105, 25);
            this.bnClose.TabIndex = 19;
            this.bnClose.Text = "«акрыть";
            this.bnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.bnClose.UseVisualStyleBackColor = true;
            this.bnClose.Click += new System.EventHandler(this.bnClose_Click);
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.bnDel);
            this.panel3.Controls.Add(this.bnUnDelete);
            this.panel3.Controls.Add(this.bnReports);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(457, 33);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(115, 301);
            this.panel3.TabIndex = 2;
            // 
            // bnDel
            // 
            this.bnDel.Image = ((System.Drawing.Image)(resources.GetObject("bnDel.Image")));
            this.bnDel.Location = new System.Drawing.Point(4, 130);
            this.bnDel.Name = "bnDel";
            this.bnDel.Size = new System.Drawing.Size(105, 25);
            this.bnDel.TabIndex = 19;
            this.bnDel.Text = "”далить";
            this.bnDel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.bnDel.UseVisualStyleBackColor = true;
            this.bnDel.Click += new System.EventHandler(this.bnDel_Click);
            // 
            // bnUnDelete
            // 
            this.bnUnDelete.Image = ((System.Drawing.Image)(resources.GetObject("bnUnDelete.Image")));
            this.bnUnDelete.Location = new System.Drawing.Point(4, 16);
            this.bnUnDelete.Name = "bnUnDelete";
            this.bnUnDelete.Size = new System.Drawing.Size(105, 25);
            this.bnUnDelete.TabIndex = 19;
            this.bnUnDelete.Text = "¬осстановить";
            this.bnUnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.bnUnDelete.UseVisualStyleBackColor = true;
            this.bnUnDelete.Click += new System.EventHandler(this.bnUnDelete_Click);
            // 
            // bnReports
            // 
            this.bnReports.ContextMenuStrip = this.ctMenuReports;
            this.bnReports.Image = ((System.Drawing.Image)(resources.GetObject("bnReports.Image")));
            this.bnReports.Location = new System.Drawing.Point(4, 47);
            this.bnReports.Name = "bnReports";
            this.bnReports.Size = new System.Drawing.Size(105, 25);
            this.bnReports.TabIndex = 18;
            this.bnReports.Text = "ќтчеты";
            this.bnReports.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.bnReports.UseVisualStyleBackColor = true;
            this.bnReports.Click += new System.EventHandler(this.bnReports_Click);
            // 
            // ctMenuReports
            // 
            this.ctMenuReports.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiƒневник»¬–Word,
            this.дневник»¬–полныйToolStripMenuItem,
            this.tsmi—правкаќѕоощрени€х»¬зыскани€х¬Word,
            this.отчетToolStripMenuItem});
            this.ctMenuReports.Name = "ctMenuReports";
            this.ctMenuReports.Size = new System.Drawing.Size(330, 92);
            // 
            // tsmiƒневник»¬–Word
            // 
            this.tsmiƒневник»¬–Word.Name = "tsmiƒневник»¬–Word";
            this.tsmiƒневник»¬–Word.Size = new System.Drawing.Size(329, 22);
            this.tsmiƒневник»¬–Word.Text = "ƒневник »¬– (полный) в Word";
            this.tsmiƒневник»¬–Word.Click += new System.EventHandler(this.tsmiƒневник»¬–Word_Click);
            // 
            // дневник»¬–полныйToolStripMenuItem
            // 
            this.дневник»¬–полныйToolStripMenuItem.Name = "дневник»¬–полныйToolStripMenuItem";
            this.дневник»¬–полныйToolStripMenuItem.Size = new System.Drawing.Size(329, 22);
            this.дневник»¬–полныйToolStripMenuItem.Text = "ƒневник »¬– (полный) 2 вариант";
            this.дневник»¬–полныйToolStripMenuItem.Click += new System.EventHandler(this.дневник»¬–полныйToolStripMenuItem_Click);
            // 
            // tsmi—правкаќѕоощрени€х»¬зыскани€х¬Word
            // 
            this.tsmi—правкаќѕоощрени€х»¬зыскани€х¬Word.Name = "tsmi—правкаќѕоощрени€х»¬зыскани€х¬Word";
            this.tsmi—правкаќѕоощрени€х»¬зыскани€х¬Word.Size = new System.Drawing.Size(329, 22);
            this.tsmi—правкаќѕоощрени€х»¬зыскани€х¬Word.Text = "—правка о поощрени€х и взыскани€х в Word";
            this.tsmi—правкаќѕоощрени€х»¬зыскани€х¬Word.Click += new System.EventHandler(this.tsmi—правкаќѕоощрени€х»¬зыскани€х¬Word_Click);
            // 
            // отчетToolStripMenuItem
            // 
            this.отчетToolStripMenuItem.Name = "отчетToolStripMenuItem";
            this.отчетToolStripMenuItem.Size = new System.Drawing.Size(329, 22);
            this.отчетToolStripMenuItem.Text = "—правка о поощрени€х и взыскани€х 2 вариант";
            this.отчетToolStripMenuItem.Click += new System.EventHandler(this.отчетToolStripMenuItem_Click);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.dgArchive);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 33);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(457, 301);
            this.panel4.TabIndex = 3;
            // 
            // dgArchive
            // 
            this.dgArchive.AllowUserToAddRows = false;
            this.dgArchive.AllowUserToDeleteRows = false;
            this.dgArchive.AllowUserToResizeRows = false;
            this.dgArchive.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgArchive.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgArchive.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgArchive.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgArchive.Location = new System.Drawing.Point(0, 0);
            this.dgArchive.Name = "dgArchive";
            this.dgArchive.ReadOnly = true;
            this.dgArchive.RowHeadersWidth = 15;
            this.dgArchive.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgArchive.Size = new System.Drawing.Size(457, 301);
            this.dgArchive.TabIndex = 0;
            // 
            // KartArchiveForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bnClose;
            this.ClientSize = new System.Drawing.Size(572, 366);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(580, 400);
            this.Name = "KartArchiveForm";
            this.Text = "јрхив картотеки";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ctMenuReports.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgArchive)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.DataGridView dgArchive;
        private System.Windows.Forms.Button bnClose;
        private System.Windows.Forms.Button bnFindDel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbLastName;
        private System.Windows.Forms.Button bnFind;
        private System.Windows.Forms.Button bnReports;
        private System.Windows.Forms.ContextMenuStrip ctMenuReports;
        private System.Windows.Forms.ToolStripMenuItem дневник»¬–полныйToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiƒневник»¬–Word;
        private System.Windows.Forms.ToolStripMenuItem отчетToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmi—правкаќѕоощрени€х»¬зыскани€х¬Word;
        private System.Windows.Forms.Button bnUnDelete;
        private System.Windows.Forms.Label labelHelp;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button bnDel;
    }
}