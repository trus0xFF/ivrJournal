namespace ivrJournal
{
    partial class SprControls
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SprControls));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.bnUpdate = new System.Windows.Forms.Button();
            this.bnLoad = new System.Windows.Forms.Button();
            this.bnClose = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dgControls = new System.Windows.Forms.DataGridView();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgControls)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(592, 33);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.bnUpdate);
            this.panel2.Controls.Add(this.bnLoad);
            this.panel2.Controls.Add(this.bnClose);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 334);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(592, 32);
            this.panel2.TabIndex = 1;
            // 
            // bnUpdate
            // 
            this.bnUpdate.Location = new System.Drawing.Point(236, 6);
            this.bnUpdate.Name = "bnUpdate";
            this.bnUpdate.Size = new System.Drawing.Size(107, 23);
            this.bnUpdate.TabIndex = 2;
            this.bnUpdate.Text = "Загрузить новые";
            this.bnUpdate.UseVisualStyleBackColor = true;
            this.bnUpdate.Click += new System.EventHandler(this.bnUpdate_Click);
            // 
            // bnLoad
            // 
            this.bnLoad.Location = new System.Drawing.Point(3, 6);
            this.bnLoad.Name = "bnLoad";
            this.bnLoad.Size = new System.Drawing.Size(227, 23);
            this.bnLoad.TabIndex = 1;
            this.bnLoad.Text = "Перезагрузить перечень форм целиком";
            this.bnLoad.UseVisualStyleBackColor = true;
            this.bnLoad.Click += new System.EventHandler(this.bnLoad_Click);
            // 
            // bnClose
            // 
            this.bnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bnClose.Location = new System.Drawing.Point(514, 6);
            this.bnClose.Name = "bnClose";
            this.bnClose.Size = new System.Drawing.Size(75, 23);
            this.bnClose.TabIndex = 0;
            this.bnClose.Text = "Закрыть";
            this.bnClose.UseVisualStyleBackColor = true;
            this.bnClose.Click += new System.EventHandler(this.bnClose_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.dgControls);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 33);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(592, 301);
            this.panel3.TabIndex = 2;
            // 
            // dgControls
            // 
            this.dgControls.AllowUserToAddRows = false;
            this.dgControls.AllowUserToDeleteRows = false;
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
            this.dgControls.MultiSelect = false;
            this.dgControls.Name = "dgControls";
            this.dgControls.ReadOnly = true;
            this.dgControls.RowHeadersVisible = false;
            this.dgControls.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgControls.Size = new System.Drawing.Size(592, 301);
            this.dgControls.TabIndex = 0;
            // 
            // SprControls
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bnClose;
            this.ClientSize = new System.Drawing.Size(592, 366);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "SprControls";
            this.Text = "Элементы управления";
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgControls)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView dgControls;
        private System.Windows.Forms.Button bnClose;
        private System.Windows.Forms.Button bnLoad;
        private System.Windows.Forms.Button bnUpdate;
    }
}