namespace ivrJournal
{
    partial class EditUserRolForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditUserRolForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgUserRol = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.dgControls = new System.Windows.Forms.DataGridView();
            this.panel5 = new System.Windows.Forms.Panel();
            this.cbAll = new System.Windows.Forms.CheckBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.bnSave = new System.Windows.Forms.Button();
            this.bnClose = new System.Windows.Forms.Button();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgUserRol)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgControls)).BeginInit();
            this.panel5.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(15);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel2);
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            this.splitContainer1.Panel1.Padding = new System.Windows.Forms.Padding(5, 0, 1, 5);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel4);
            this.splitContainer1.Panel2.Controls.Add(this.panel5);
            this.splitContainer1.Panel2.Controls.Add(this.panel3);
            this.splitContainer1.Panel2.Padding = new System.Windows.Forms.Padding(1, 5, 5, 0);
            this.splitContainer1.Size = new System.Drawing.Size(669, 504);
            this.splitContainer1.SplitterDistance = 199;
            this.splitContainer1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dgUserRol);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(5, 31);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(193, 468);
            this.panel2.TabIndex = 1;
            // 
            // dgUserRol
            // 
            this.dgUserRol.AllowUserToAddRows = false;
            this.dgUserRol.AllowUserToDeleteRows = false;
            this.dgUserRol.AllowUserToResizeColumns = false;
            this.dgUserRol.AllowUserToResizeRows = false;
            this.dgUserRol.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgUserRol.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgUserRol.ColumnHeadersVisible = false;
            this.dgUserRol.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgUserRol.Location = new System.Drawing.Point(0, 0);
            this.dgUserRol.Name = "dgUserRol";
            this.dgUserRol.ReadOnly = true;
            this.dgUserRol.RowHeadersVisible = false;
            this.dgUserRol.Size = new System.Drawing.Size(193, 468);
            this.dgUserRol.TabIndex = 0;
            this.dgUserRol.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgUserRol_RowEnter);
            this.dgUserRol.RowLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgUserRol_RowLeave);
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(5, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(193, 31);
            this.panel1.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.dgControls);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(1, 31);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(460, 442);
            this.panel4.TabIndex = 3;
            // 
            // dgControls
            // 
            this.dgControls.AllowUserToAddRows = false;
            this.dgControls.AllowUserToDeleteRows = false;
            this.dgControls.AllowUserToResizeColumns = false;
            this.dgControls.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgControls.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgControls.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgControls.Location = new System.Drawing.Point(0, 0);
            this.dgControls.Name = "dgControls";
            this.dgControls.ReadOnly = true;
            this.dgControls.RowHeadersVisible = false;
            this.dgControls.RowHeadersWidth = 5;
            this.dgControls.Size = new System.Drawing.Size(460, 442);
            this.dgControls.TabIndex = 0;
            this.dgControls.Leave += new System.EventHandler(this.dgControls_Leave);
            this.dgControls.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgControls_CellClick);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.cbAll);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(1, 5);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(460, 26);
            this.panel5.TabIndex = 2;
            // 
            // cbAll
            // 
            this.cbAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbAll.AutoSize = true;
            this.cbAll.Location = new System.Drawing.Point(358, 6);
            this.cbAll.Name = "cbAll";
            this.cbAll.Size = new System.Drawing.Size(96, 17);
            this.cbAll.TabIndex = 0;
            this.cbAll.Text = "выделить все";
            this.cbAll.UseVisualStyleBackColor = true;
            this.cbAll.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.bnSave);
            this.panel3.Controls.Add(this.bnClose);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(1, 473);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(460, 31);
            this.panel3.TabIndex = 0;
            // 
            // bnSave
            // 
            this.bnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bnSave.Location = new System.Drawing.Point(301, 5);
            this.bnSave.Name = "bnSave";
            this.bnSave.Size = new System.Drawing.Size(75, 23);
            this.bnSave.TabIndex = 1;
            this.bnSave.Text = "Сохранить";
            this.bnSave.UseVisualStyleBackColor = true;
            this.bnSave.Click += new System.EventHandler(this.bnSave_Click);
            // 
            // bnClose
            // 
            this.bnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bnClose.Location = new System.Drawing.Point(382, 5);
            this.bnClose.Name = "bnClose";
            this.bnClose.Size = new System.Drawing.Size(75, 23);
            this.bnClose.TabIndex = 0;
            this.bnClose.Text = "Закрыть";
            this.bnClose.UseVisualStyleBackColor = true;
            this.bnClose.Click += new System.EventHandler(this.bnClose_Click);
            // 
            // EditUserRolForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bnClose;
            this.ClientSize = new System.Drawing.Size(669, 504);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EditUserRolForm";
            this.Text = "Редактирование набора прав пользователей";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgUserRol)).EndInit();
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgControls)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dgUserRol;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgControls;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button bnSave;
        private System.Windows.Forms.Button bnClose;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.CheckBox cbAll;
    }
}