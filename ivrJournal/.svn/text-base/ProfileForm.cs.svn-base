using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;

namespace ivrJournal
{
    public partial class ProfileForm : Form
    {
        private MainDBConnect dbCon;
        private Boolean IsNew;
        private DataRow selRow;
        private ProfileData pd;

        public ProfileForm()
        {
            InitializeComponent();
        }

        public ProfileForm(MainDBConnect newDbCon)
        {
            InitializeComponent();
            /*
            RegistryKey regKey = Registry.CurrentUser;
            regKey = regKey.OpenSubKey("Software\\UFSIN\\ivrJournal");
            MainDBConnect newDBcon = new MainDBConnect(regKey.GetValue("dbPath", "").ToString());
            */

            pd = new ProfileData();
            this.IsNew = true;
            this.dbCon = newDbCon;
            SetCBFields();
            SetRights();
            //pbFoto.Load(@"\\xaos\OK_AISS\Фото сотрудников\АППАРАТ\Балалаев Игорь Николаевич - Старая.jpg");

        }

        private void SetRights()
        {
            FormControlManager fcm = new FormControlManager();
            fcm.SetFormControlStatus(this);
        }

        private void SetCBFields()
        {
            cbEducation.DataSource = dbCon.GetDataTable("spr_edu");
            cbEducation.DisplayMember = "name";
            cbEducation.ValueMember = "id";

            cbProfession.DataSource = dbCon.GetDataTable("spr_profession");
            cbProfession.DisplayMember = "name";
            cbProfession.ValueMember = "id";

            cbMstatus.DataSource = dbCon.GetDataTable("spr_mstatus");
            cbMstatus.DisplayMember = "name";
            cbMstatus.ValueMember = "id";

            cbNation.DataSource = dbCon.GetDataTable("spr_nation");
            cbNation.DisplayMember = "name";
            cbNation.ValueMember = "id";

            cbParty.DataSource = dbCon.GetDataTable("spr_party_number");
            cbParty.DisplayMember = "name";
            cbParty.ValueMember = "id";

            toolTip1.SetToolTip(bnPeriod, "Заполнить");

        }

        private String CalcPeriod(String start_period, String end_period)
        {
            try
            {
                DateTime dt_start = DateTime.Parse(start_period);
                DateTime dt_end = DateTime.Parse(end_period);
                TimeSpan ts = dt_end - dt_start;
                DateTime dt = DateTime.MinValue + ts;

                int years = dt.Year - 1;
                int months = dt.Month - 1;
                int days = dt.Day;

                String period = years.ToString() + " л. " + 
                                            months.ToString() + " м. " + 
                                            days.ToString() + " д.";

                //return ts.TotalDays.ToString();
                return period;
            }
            catch (Exception)
            {
                //MessageBox.Show(e.Message, "Ошибка");
                return String.Empty;
            }
            
        }

        public ProfileForm(MainDBConnect newDbCon, DataRow selRow)
        {
            InitializeComponent();

            pd = new ProfileData();
            this.IsNew = false;
            this.selRow = selRow;

            this.dbCon = newDbCon;
            SetCBFields();

            this.tbLastName.Text = selRow["last_name"].ToString();
            this.tbFirstName.Text = selRow["first_name"].ToString();
            this.tbPatronymic.Text = selRow["patronymic"].ToString();
            this.mtbBirthDate.Text = selRow["birthdate"].ToString();
            
            //MessageBox.Show(cbEducation.SelectedValue.ToString());

            if ((selRow["edu_id"].ToString()) != "")
                cbEducation.SelectedValue = (int)selRow["edu_id"];
            if ((selRow["mstatus_id"].ToString()) != "")
                cbMstatus.SelectedValue = (int)selRow["mstatus_id"];
            if ((selRow["profession_id"].ToString()) != "")
                cbProfession.SelectedValue = (int)selRow["profession_id"];
            if ((selRow["nation_id"].ToString()) != "")
                cbNation.SelectedValue = (int)selRow["nation_id"];
            if ((selRow["party_id"].ToString()) != "")
                cbParty.SelectedValue = (int)selRow["party_id"];

            this.tbCourt.Text = selRow["court"].ToString();
            this.tbArticle.Text = selRow["article"].ToString();
            this.mtbCrimeDate.Text = selRow["crime_date"].ToString();
            this.tbPeriod.Text = selRow["period"].ToString();
            this.lblPeriod.Text = CalcPeriod(selRow["period_start"].ToString(),
                                selRow["period_end"].ToString());
            this.mtbPeriodStart.Text = selRow["period_start"].ToString();
            this.mtbPeriodEnd.Text = selRow["period_end"].ToString();

            this.mtbDateLight.Text = selRow["period_light"].ToString();
            this.mtbDateNormal.Text = selRow["period_normal"].ToString();
            this.mtbDateKp.Text = selRow["period_kp"].ToString();
            this.mtbDateUdo.Text = selRow["period_udo"].ToString();

            this.tbCrimeDesc.Text = selRow["crime_description"].ToString();
            this.tbMedDesc.Text = selRow["med_description"].ToString();
            this.tbOther.Text = selRow["other"].ToString();
            this.tbResult.Text = selRow["result"].ToString();

            try 
            {
                String fotoPath = selRow["foto"].ToString();
                if (fotoPath != String.Empty)
                {
                    pbFoto.Load( dbCon.GetDBDirPath() + @"\" + fotoPath);
                }
                pd.foto = fotoPath;
            } 
            catch(IOException ioe) 
            {
                MessageBox.Show(ioe.Message, "Ошибка");
            }
            SetRights();
        }

        private void bnSave_Click(object sender, EventArgs e)
        {
            //ProfileData pd = new ProfileData();

            pd.last_name = this.tbLastName.Text;
            pd.first_name = this.tbFirstName.Text;
            pd.patronymic = this.tbPatronymic.Text;
            pd.birthdate = this.mtbBirthDate.Text;
            pd.edu_id = (int)this.cbEducation.SelectedValue;
            pd.mstatus_id = (int)this.cbMstatus.SelectedValue;
            pd.profession_id = (int)this.cbProfession.SelectedValue;
            pd.nation_id = (int)this.cbNation.SelectedValue;
            if (this.cbParty.SelectedValue != null)
                pd.party_id = (int)this.cbParty.SelectedValue;
            pd.court = this.tbCourt.Text;
            pd.article = this.tbArticle.Text;
            pd.crime_date = this.mtbCrimeDate.Text;
            pd.period = this.tbPeriod.Text;
            pd.period_start = this.mtbPeriodStart.Text;
            pd.period_end = this.mtbPeriodEnd.Text;

            pd.period_light = this.mtbDateLight.Text;
            pd.period_normal = this.mtbDateNormal.Text;
            pd.period_kp = this.mtbDateKp.Text;
            pd.period_udo = this.mtbDateUdo.Text;

            pd.crime_desc = this.tbCrimeDesc.Text;
            pd.med_desc = this.tbMedDesc.Text;
            pd.other = this.tbOther.Text;
            pd.result = this.tbResult.Text;

            if (IsNew)
                dbCon.SaveProfileData(pd);
            else
                dbCon.SaveProfileData(pd, selRow);

            this.Close();
        }

        private void bnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pbFoto_MouseClick(object sender, EventArgs e)
        {
            RegistryKey regKey = Registry.CurrentUser;
            regKey = regKey.CreateSubKey("Software\\UFSIN\\ivrJournal");
            String dbPath = regKey.GetValue("dbPath", @"c:\Program files\ufsin_rk\Дневник ИВР\divr.mdb").ToString();
            
            FileInfo file = new FileInfo(dbPath);
            String dbDirPath = file.DirectoryName;

            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.InitialDirectory = dbPath;
                openFileDialog.Filter = "Image Files (*.jpg)|*.jpg|All Files (*.*)|*.*";
                if (openFileDialog.ShowDialog(this) != DialogResult.OK)
                    return;

                FileInfo fileImg = new FileInfo(openFileDialog.FileName);
                //MessageBox.Show(DateTime.UtcNow.ToBinary().ToString());

                String fotoPath = dbCon.getFotoPath();
                //String newFileName = dbDirPath + @"\" + dbCon.getFotoPath();
                String newFileName = dbDirPath + @"\" + fotoPath;
                fileImg.CopyTo(newFileName);

                pbFoto.Load(newFileName);
                pd.foto = fotoPath;
               
            } catch(Exception ioe)
            {
                MessageBox.Show(ioe.Message, "Ошибка");
            }

        }

        private void ProfileForm_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            Help.ShowHelp(this, "help_ivr.chm", HelpNavigator.Topic, "help_ivr_3.htm");
        }

        private void button1_Click(object sender, EventArgs e)
        {
//            if (!Convert.IsDBNull(selRow))
//                tbPeriod.Text = CalcPeriod(selRow["period_start"].ToString(),
//                                           selRow["period_end"].ToString());
//            else
                tbPeriod.Text = CalcPeriod(mtbPeriodStart.Text, mtbPeriodEnd.Text);
        }
    }



    public class ProfileData
    {
        private String FirstName;
        public String first_name
        {
            get { return FirstName; }
            set { FirstName = value; }
        }

        private String LastName;
        public String last_name
        {
            get { return LastName; }
            set { LastName = value; }
        }

        private String Patronymic;
        public String patronymic
        {
            get { return Patronymic; }
            set { Patronymic = value; }
        }

        private String BirthDate;
        public String birthdate
        {
            get { return BirthDate; }
            set { BirthDate = value; }
        }

        private int EduId;
        public int edu_id
        {
            get { return EduId; }
            set { EduId = value; }
        }

        private int MstatusId;
        public int mstatus_id
        {
            get { return MstatusId; }
            set { MstatusId = value; }
        }

        private int ProfessionId;
        public int profession_id
        {
            get { return ProfessionId; }
            set { ProfessionId = value; }
        }

        private int NationId;
        public int nation_id
        {
            get { return NationId; }
            set { NationId = value; }
        }

        private int PartyId;
        public int party_id
        {
            get { return PartyId; }
            set { PartyId = value; }
        }

        private String Court;
        public String court
        {
            get { return Court; }
            set { Court = value; }
        }

        private String Article;
        public String article
        {
            get { return Article; }
            set { Article = value; }
        }

        private String CrimeDate;
        public String crime_date
        {
            get { return CrimeDate; }
            set { 
                CrimeDate = value; }
        }

        private String Period;
        public String period
        {
            get { return Period; }
            set { Period = value; }
        }

        private String PeriodStart;
        public String period_start
        {
            get { return PeriodStart; }
            set { PeriodStart = value; }
        }

        private String PeriodEnd;
        public String period_end
        {
            get { return PeriodEnd; }
            set { PeriodEnd = value; }
        }


        private String PeriodLight;
        public String period_light
        {
            get { return PeriodLight; }
            set { PeriodLight = value; }
        }

        private String PeriodNormal;
        public String period_normal
        {
            get { return PeriodNormal; }
            set { PeriodNormal = value; }
        }

        private String PeriodKp;
        public String period_kp
        {
            get { return PeriodKp; }
            set { PeriodKp = value; }
        }

        private String PeriodUdo;
        public String period_udo
        {
            get { return PeriodUdo; }
            set { PeriodUdo = value; }
        }


        private String CrimeDesc;
        public String crime_desc
        {
            get { return CrimeDesc; }
            set { CrimeDesc = value; }
        }

        private String MedDesc;
        public String med_desc
        {
            get { return MedDesc; }
            set { MedDesc = value; }
        }

        private String Other;
        public String other
        {
            get { return Other; }
            set { Other = value; }
        }

        private String Result;
        public String result
        {
            get { return Result; }
            set { Result = value; }
        }

        private String Foto;
        public String foto
        {
            get { return Foto; }
            set { Foto = value; }
        }
    }
}
