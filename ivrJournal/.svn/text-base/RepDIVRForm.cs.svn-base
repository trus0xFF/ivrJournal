using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using Microsoft.Reporting.WinForms;
using System.IO;
using Microsoft.Win32;
using System.Reflection;

namespace ivrJournal
{
    public partial class RepDIVRForm : Form
    {
        private DataRow selRow;
        private Int32 idSpec;

        internal protected ReportViewer reportviewer;
        SQLDBConnectLite dbConSQL;

        private DataTable GetTableLogo(string imagePath)
        {
            RegistryKey regKey = Registry.CurrentUser;
            regKey = regKey.OpenSubKey("Software\\UFSIN\\ivrJournal");
            FileInfo file = new FileInfo(regKey.GetValue("dbPath", "").ToString());
            String dbDirPath = file.DirectoryName;

            DataSet dsProduct = new DataSet();
            DataTable dtImageTable = new DataTable("ImageTable");
            DataColumn imgCol = new DataColumn("logoImage", typeof(byte[]));
            dtImageTable.Columns.Add(imgCol);
            DataRow dRow = dtImageTable.NewRow();

            if (imagePath == String.Empty)
                imagePath = "no_image.jpg";
            else
                imagePath = dbDirPath + @"\" + imagePath;

            try {

                FileStream imgFS = File.OpenRead(imagePath);

                byte[] byteArray = new byte[imgFS.Length];
                imgFS.Read(byteArray, 0, Convert.ToInt32(imgFS.Length));
                imgFS.Close();
                dRow[0] = byteArray;
                dtImageTable.Rows.Add(dRow);
                return dtImageTable;
            }
            catch (IOException ioe)
            {
                MessageBox.Show(ioe.Message, "Ошибка");
                return dtImageTable;
            }
        }

        public RepDIVRForm(DataRow selRow)
        {
            this.selRow = selRow;
            idSpec = (int)selRow["id"];

            InitializeComponent();
            dbConSQL = new SQLDBConnectLite();

            try
            {
                rvDIVR.LocalReport.ReportPath = @"ReportIVR.rdlc";
                if (selRow["foto"] != null)
                    rvDIVR.LocalReport.DataSources.Add(new ReportDataSource("DataSetDIVR_DataTableLogo", GetTableLogo(selRow["foto"].ToString())));
                else
                    rvDIVR.LocalReport.DataSources.Add(new ReportDataSource("DataSetDIVR_DataTableLogo", GetTableLogo("")));

                rvDIVR.LocalReport.DataSources.Add(new ReportDataSource("DataSetDIVR_DataTableParty", dbConSQL.GetDataTable("party", "SELECT p.arr_date AS arr_date, p.ord AS ord, p.reason AS reason, spr_party_number.name AS party_number FROM party AS p LEFT JOIN spr_party_number ON spr_party_number.id=p.party_number_id WHERE (p.id_spec=" + idSpec + ")")));

                rvDIVR.LocalReport.DataSources.Add(new ReportDataSource("DataSetDIVR_DataTableRelations", dbConSQL.GetDataTable("relations", "SELECT r.last_name AS last_name, r.first_name AS first_name, r.patronymic AS patronymic, r.address AS address, r.birthdate AS birthdate, spr_degree.name AS degree FROM relations AS r LEFT JOIN spr_degree ON spr_degree.id=r.degree_id WHERE (r.id_spec=" + idSpec + ")")));

                rvDIVR.LocalReport.DataSources.Add(new ReportDataSource("DataSetDIVR_DataTablePsycho_char", dbConSQL.GetDataTable("psycho_char", "SELECT date_meet, orientation, psycho_char, behavior FROM psycho_char WHERE (id_spec=" + idSpec + ")")));

                rvDIVR.LocalReport.DataSources.Add(new ReportDataSource("DataSetDIVR_DataTablePrev_conv", dbConSQL.GetDataTable("prev_conv", "SELECT start_date, period, text_prev, article, release_date FROM prev_conv WHERE (id_spec=" + idSpec + ")")));

                rvDIVR.LocalReport.DataSources.Add(new ReportDataSource("DataSetDIVR_DataTableIvr1", dbConSQL.GetDataTable("ivr1", "SELECT data_ivr, content, description FROM ivr WHERE ((id_spec=" + idSpec + ") AND (id_type_ivr = 1))")));
                rvDIVR.LocalReport.DataSources.Add(new ReportDataSource("DataSetDIVR_DataTableIvr2", dbConSQL.GetDataTable("ivr2", "SELECT ivr.data_ivr AS data_ivr, ivr.content AS content, ivr.description AS description, employee.last_name +  ' ' + employee.first_name + ' ' + employee.patronymic + ', ' + employee.rank + ', ' + employee.post as employee FROM ivr LEFT JOIN employee ON (ivr.employee_id = employee.id)  WHERE ((id_spec=" + idSpec + ") AND (id_type_ivr = 2))")));
                rvDIVR.LocalReport.DataSources.Add(new ReportDataSource("DataSetDIVR_DataTableIvr3", dbConSQL.GetDataTable("ivr3", "SELECT ivr.data_ivr AS data_ivr, ivr.content AS content, ivr.description AS description, employee.last_name + ' ' + employee.first_name + ' ' + employee.patronymic + ', ' + employee.rank + ', ' + employee.post as employee FROM ivr LEFT JOIN employee ON (ivr.employee_id = employee.id) WHERE ((id_spec=" + idSpec + ") AND (id_type_ivr = 3))")));
                rvDIVR.LocalReport.DataSources.Add(new ReportDataSource("DataSetDIVR_DataTableResolution", dbConSQL.GetDataTable("resolution", "SELECT date_resolution, resolution, description FROM resolution WHERE (id_spec=" + idSpec + ")")));

                rvDIVR.LocalReport.DataSources.Add(new ReportDataSource("DataSetDIVR_DataTableBonus", dbConSQL.GetDataTable("bonus", "SELECT bonus.date_bonus AS date_bonus, bonus.bonus_reason AS bonus_reason, spr_bonus_type.name AS bonus_type, spr_performers.name AS performers, bonus.order_date AS order_date, bonus.order_number AS order_number FROM spr_performers RIGHT JOIN (spr_bonus_type RIGHT JOIN bonus ON spr_bonus_type.id=bonus.bonus_type_id) ON spr_performers.id=bonus.performer_id WHERE (bonus.id_spec=" + idSpec + ")")));

                rvDIVR.LocalReport.DataSources.Add(new ReportDataSource("DataSetDIVR_DataTablePenalty1", dbConSQL.GetDataTable("penalty1", "SELECT penalty.date_penalty AS date_penalty, penalty.reason AS reason, spr_penalty_type.name AS penalty_type, penalty.order_date AS order_date, penalty.order_number AS order_number, penalty.removal AS removal, spr_performers.name AS performers FROM spr_performers RIGHT JOIN (spr_penalty_type RIGHT JOIN penalty ON spr_penalty_type.id=penalty.penalty_type_id) ON spr_performers.id=penalty.performer_id WHERE ((penalty.id_spec=" + idSpec + ") AND (oral=true))")));
                rvDIVR.LocalReport.DataSources.Add(new ReportDataSource("DataSetDIVR_DataTablePenalty2", dbConSQL.GetDataTable("penalty2", "SELECT penalty.date_penalty AS date_penalty, penalty.reason AS reason, spr_penalty_type.name AS penalty_type, penalty.order_date AS order_date, penalty.order_number AS order_number, penalty.removal AS removal, spr_performers.name AS performers FROM spr_performers RIGHT JOIN (spr_penalty_type RIGHT JOIN penalty ON spr_penalty_type.id=penalty.penalty_type_id) ON spr_performers.id=penalty.performer_id WHERE ((penalty.id_spec=" + idSpec + ") AND (oral=false))")));

                DateTimeFormatInfo fmt = (new CultureInfo("ru-RU")).DateTimeFormat;
                
                ReportParameter p1 = new ReportParameter("last_name", selRow["last_name"].ToString());
                ReportParameter p2 = new ReportParameter("first_name", selRow["first_name"].ToString());
                ReportParameter p3 = new ReportParameter("patronymic", selRow["patronymic"].ToString());
                ReportParameter p4 = new ReportParameter("birthdate", getValidDate(selRow["birthdate"]));
                ReportParameter p5 = new ReportParameter("court", selRow["court"].ToString() + " " + getValidDate(selRow["crime_date"]));
                ReportParameter p6 = new ReportParameter("article", selRow["article"].ToString());
                ReportParameter p7 = new ReportParameter("period", selRow["period"].ToString());
                ReportParameter p8 = new ReportParameter("period_start", getValidDate(selRow["period_start"]));
                ReportParameter p9 = new ReportParameter("period_end", getValidDate(selRow["period_end"]));
                ReportParameter p10 = new ReportParameter("period_light", getValidDate(selRow["period_light"]));
                ReportParameter p11 = new ReportParameter("period_normal", getValidDate(selRow["period_normal"]));
                ReportParameter p12 = new ReportParameter("period_kp", getValidDate(selRow["period_kp"]));
                ReportParameter p13 = new ReportParameter("period_udo", getValidDate(selRow["period_udo"]));

                SQLDBConnect sqlCon = new SQLDBConnect();

                String nameName = sqlCon.GetValue("name", "SELECT * FROM spr_nation WHERE id = " + selRow["nation_id"].ToString()); ;
                ReportParameter p14 = new ReportParameter("nation", nameName);

                nameName = sqlCon.GetValue("name", "SELECT * FROM spr_mstatus WHERE id = " + selRow["mstatus_id"].ToString());
                ReportParameter p15 = new ReportParameter("mstatus", nameName);

                nameName = sqlCon.GetValue("name", "SELECT * FROM spr_edu WHERE id = " + selRow["edu_id"].ToString());
                ReportParameter p16 = new ReportParameter("edu", nameName);

                nameName = sqlCon.GetValue("name", "SELECT * FROM spr_profession WHERE id = " + selRow["profession_id"].ToString());
                ReportParameter p17 = new ReportParameter("profession", nameName);

                ReportParameter p18 = new ReportParameter("crime_description", (string)selRow["crime_description"].ToString());
                ReportParameter p19 = new ReportParameter("med_description", (string)selRow["med_description"].ToString());
                ReportParameter p20 = new ReportParameter("other", (string)selRow["other"].ToString());
                ReportParameter p21 = new ReportParameter("result", (string)selRow["result"].ToString());

                this.rvDIVR.LocalReport.SetParameters(new ReportParameter[] { p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21 });
                this.rvDIVR.ProcessingMode = ProcessingMode.Local;
                this.rvDIVR.RefreshReport();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка при построении отчета");
                Close();
            }
        }

        private String getValidDate(Object objDate)
        {
            try
            {
                if (Convert.ToString(objDate).Trim() == String.Empty)
                    return String.Empty;
                DateTime dateTime = DateTime.Parse(Convert.ToString(objDate));
                return dateTime.ToString("D");
            }
            catch (FormatException)
            {
                return String.Empty;
            }
        }

        private void RepDIVRForm_Load(object sender, EventArgs e)
        {
            this.rvDIVR.RefreshReport();
        }

        private void RepDIVRForm_Resize(object sender, EventArgs e)
        {
 
        }

        private void rvDIVR_Load(object sender, EventArgs e)
        {

        }
    }
}