namespace ivrJournal
{
    partial class RepBonusAndPenaltyForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RepBonusAndPenaltyForm));
            this.rvBandP = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // rvBandP
            // 
            resources.ApplyResources(this.rvBandP, "rvBandP");
            this.rvBandP.LocalReport.ReportEmbeddedResource = "";
            this.rvBandP.Name = "rvBandP";
            this.rvBandP.Load += new System.EventHandler(this.rvBandP_Load);
            // 
            // RepBonusAndPenaltyForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rvBandP);
            this.Name = "RepBonusAndPenaltyForm";
            this.Load += new System.EventHandler(this.RepBonusAndPenaltyForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer rvBandP;
    }
}