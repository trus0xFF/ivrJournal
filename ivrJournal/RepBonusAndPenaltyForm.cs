using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using Microsoft.Reporting.WinForms;

namespace ivrJournal
{
    public partial class RepBonusAndPenaltyForm : Form
    {
        private DataRow selRow;
        private Int32 idSpec;
        SQLDBConnect dbCon;
//        internal protected ReportViewer reportviewer;

        public RepBonusAndPenaltyForm(DataRow selRow)
        {
            this.selRow = selRow;
            idSpec = (Int32)selRow["id"];

            InitializeComponent();
            dbCon = new SQLDBConnect();

            try
            {
                DateTimeFormatInfo fmt = (new CultureInfo("ru-RU")).DateTimeFormat;

                rvBandP.LocalReport.ReportPath = @"ReportBandP.rdlc";

                DataTable dtBonus = dbCon.GetDataTable("bonus", "SELECT COUNT(*) AS numberpp, b1.date_bonus AS date_bonus, b1.bonus_reason AS bonus_reason, spr_bonus_type.name AS bonus_type FROM (bonus AS b1 LEFT JOIN bonus AS b2 ON b2.id<=b1.id) LEFT JOIN spr_bonus_type ON spr_bonus_type.id=b1.bonus_type_id WHERE (b1.id_spec=" + idSpec + ") AND (b2.id_spec=" + idSpec + ") GROUP BY b1.id, b1.date_bonus, b1.bonus_reason, spr_bonus_type.name");
                rvBandP.LocalReport.DataSources.Add(new ReportDataSource("DataSetBandP_DataTableBonus", dtBonus));

                DataTable dtPenalty = dbCon.GetDataTable("penalty", "SELECT COUNT(*) AS numberpp, p1.date_penalty AS date_penalty, p1.reason AS reason, p1.removal AS removal, spr_penalty_type.name AS penalty_type FROM (penalty AS p1 LEFT JOIN penalty AS p2 ON p2.id<=p1.id) LEFT JOIN spr_penalty_type ON spr_penalty_type.id=p1.penalty_type_id WHERE (p1.id_spec=" + idSpec + ") AND (p2.id_spec=" + idSpec + ") GROUP BY p1.id, p1.date_penalty, p1.reason, p1.removal, spr_penalty_type.name");
                rvBandP.LocalReport.DataSources.Add(new ReportDataSource("DataSetBandP_DataTablePenalty", dtPenalty));

                ReportParameter p1 = new
                   ReportParameter("name", selRow["last_name"].ToString() + " " + selRow["first_name"].ToString() + " " + selRow["patronymic"].ToString());

                rvBandP.LocalReport.SetParameters(new ReportParameter[] { p1 });

                rvBandP.ProcessingMode = ProcessingMode.Local;
                rvBandP.RefreshReport();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка при построении отчета");
                Close();
            }
        }

        private void RepBonusAndPenaltyForm_Load(object sender, EventArgs e)
        {

            this.rvBandP.RefreshReport();
        }

        private void rvBandP_Load(object sender, EventArgs e)
        {

        }
    }
}