namespace ivrJournal
{
    partial class ChooseDepartmentForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChooseDepartmentForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.bnLoad = new System.Windows.Forms.Button();
            this.bnBack = new System.Windows.Forms.Button();
            this.bnCancel = new System.Windows.Forms.Button();
            this.bnNext = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgRegion = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dgDepartment = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgRegion)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgDepartment)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.bnLoad);
            this.panel1.Controls.Add(this.bnBack);
            this.panel1.Controls.Add(this.bnCancel);
            this.panel1.Controls.Add(this.bnNext);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 274);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(398, 34);
            this.panel1.TabIndex = 0;
            // 
            // bnLoad
            // 
            this.bnLoad.Location = new System.Drawing.Point(7, 6);
            this.bnLoad.Name = "bnLoad";
            this.bnLoad.Size = new System.Drawing.Size(75, 23);
            this.bnLoad.TabIndex = 1;
            this.bnLoad.Text = "Загрузить";
            this.bnLoad.UseVisualStyleBackColor = true;
            this.bnLoad.Click += new System.EventHandler(this.bnLoad_Click);
            // 
            // bnBack
            // 
            this.bnBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bnBack.Location = new System.Drawing.Point(157, 6);
            this.bnBack.Name = "bnBack";
            this.bnBack.Size = new System.Drawing.Size(75, 23);
            this.bnBack.TabIndex = 0;
            this.bnBack.Text = "<< Назад";
            this.bnBack.UseVisualStyleBackColor = true;
            this.bnBack.Click += new System.EventHandler(this.bnBack_Click);
            // 
            // bnCancel
            // 
            this.bnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bnCancel.Location = new System.Drawing.Point(319, 6);
            this.bnCancel.Name = "bnCancel";
            this.bnCancel.Size = new System.Drawing.Size(75, 23);
            this.bnCancel.TabIndex = 0;
            this.bnCancel.Text = "Отмена";
            this.bnCancel.UseVisualStyleBackColor = true;
            this.bnCancel.Click += new System.EventHandler(this.bnCancel_Click);
            // 
            // bnNext
            // 
            this.bnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bnNext.Location = new System.Drawing.Point(238, 6);
            this.bnNext.Name = "bnNext";
            this.bnNext.Size = new System.Drawing.Size(75, 23);
            this.bnNext.TabIndex = 0;
            this.bnNext.Text = "Далее >>";
            this.bnNext.UseVisualStyleBackColor = true;
            this.bnNext.Click += new System.EventHandler(this.bnNext_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tabControl1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(398, 274);
            this.panel2.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(398, 274);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabControl1_Selecting);
            this.tabControl1.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl1_Selected);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgRegion);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(390, 248);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Выбор региона";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgRegion
            // 
            this.dgRegion.AllowUserToAddRows = false;
            this.dgRegion.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgRegion.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgRegion.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgRegion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgRegion.Location = new System.Drawing.Point(3, 3);
            this.dgRegion.MultiSelect = false;
            this.dgRegion.Name = "dgRegion";
            this.dgRegion.ReadOnly = true;
            this.dgRegion.RowHeadersWidth = 10;
            this.dgRegion.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgRegion.Size = new System.Drawing.Size(384, 242);
            this.dgRegion.TabIndex = 0;
            this.dgRegion.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgRegion_CellLeave);
            this.dgRegion.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgRegion_RowEnter);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgDepartment);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(390, 248);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Выбор учреждения";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dgDepartment
            // 
            this.dgDepartment.AllowUserToAddRows = false;
            this.dgDepartment.AllowUserToDeleteRows = false;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgDepartment.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgDepartment.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgDepartment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgDepartment.Location = new System.Drawing.Point(3, 3);
            this.dgDepartment.MultiSelect = false;
            this.dgDepartment.Name = "dgDepartment";
            this.dgDepartment.ReadOnly = true;
            this.dgDepartment.RowHeadersWidth = 10;
            this.dgDepartment.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgDepartment.Size = new System.Drawing.Size(384, 242);
            this.dgDepartment.TabIndex = 0;
            // 
            // ChooseDepartmentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(398, 308);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ChooseDepartmentForm";
            this.Text = "Выбор учреждения";
            this.Load += new System.EventHandler(this.ChooseDepartmentForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChooseDepartmentForm_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgRegion)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgDepartment)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button bnBack;
        private System.Windows.Forms.Button bnNext;
        private System.Windows.Forms.DataGridView dgRegion;
        private System.Windows.Forms.DataGridView dgDepartment;
        private System.Windows.Forms.Button bnCancel;
        private System.Windows.Forms.Button bnLoad;

    }
}