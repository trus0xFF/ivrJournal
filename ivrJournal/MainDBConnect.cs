
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;
using System.IO;
using Microsoft.Win32;

namespace ivrJournal
{

    public class MainDBConnect
    {
        const String DataProvider = @"Microsoft.Jet.OLEDB.4.0";

        private String dbPath;
        private OleDbConnection OleDbCon;
        private OleDbDataAdapter MainDataAdapter;
        private DataSet MainDataSet;

        private void SetOleDbCon(String DataPath)
        {
            try
            {
                // Для того, чтобы создать OLE – соединение с локальной базой данных,
                // необходимо указать 2 параметра – провайдера и путь к базе данных.
                // Для наглядности я воспользовался построителем строк подключения –
                // OleDbConnectionStringBuilder.
                OleDbConnectionStringBuilder bldr = new OleDbConnectionStringBuilder();
                bldr.DataSource = DataPath; // Указываем путь
                bldr.Provider = DataProvider; // Указываем провайдера

                bldr.Add("Jet OLEDB:Database Password", "ivr32a");

                // Создаем подключение к источнику данных
                //MessageBox.Show(bldr.ConnectionString);
                OleDbCon = new OleDbConnection(bldr.ConnectionString);
            }
            catch (OleDbException odbe)
            {
                MessageBox.Show(odbe.Message, "Ошибка соединения");
            }
        }

        public DataSet CopyDataSet(String tableName)
        {
            // Класс, необходимый для задания оператора SQL и источника данных
            OleDbCommand cmd = new OleDbCommand();
            cmd.CommandText = @"SELECT * FROM " + tableName; // Задаем оператор SQL
            cmd.Connection = OleDbCon; // Задаем источник данных

            // Объект OleDbDataAdapter выполняет функцию моста между DataTable и источником данных.
            OleDbDataAdapter da = new OleDbDataAdapter();

            //OleDbCommandBuilder invBuilder = new OleDbCommandBuilder(da);

            da.SelectCommand = cmd;

            //DataTable tbl = new DataTable();
            DataSet ds = new DataSet();
            // Обеспечивает он такой мост с помощью метода Fill.
            //da.Fill(tbl);
            da.Fill(ds, tableName);
            return ds;
        }

        private DataTable CopyDataTable(String tableName)
        {
            return CopyDataSet(tableName).Tables[tableName];
        }

        public MainDBConnect(String dbPath)
        {
            this.dbPath = dbPath;
            SetOleDbCon(dbPath);
            PrepareTables();
            
        }

        public Boolean MainDBConCheckBool()
        {
            try
            {
                OleDbCon.Open();
                OleDbCon.Close();
                return true;

            }
            catch (Exception)
            {
                //MessageBox.Show(e.Message, "Ошибка соединения");
                return false;
            }
        }

        public DataTable GetDataTable(String tblName)
        {
            try
            {
                OleDbCon.Open();
                
                DataTable dt = MainDataSet.Tables[tblName];

                OleDbCon.Close();
                return dt;
            }
            catch (Exception e)
            {
                OleDbCon.Close();
                MessageBox.Show(e.Message, "Ошибка соединения");
                return null;
            }
        }

        public DataTable GetDataTable_spec()
        {
            DataTable personsDataTable = GetDataTable("spec");

            DataView dv = new DataView(personsDataTable);
            DataTable dt = dv.ToTable(true, "last_name", "first_name", "patronymic", "birthdate");
            return dt;
        }

        private void PrepareTables()
        {
            try
            {
                OleDbCon.Open();
                OleDbDataAdapter specAdapter = new OleDbDataAdapter("SELECT * FROM spec ORDER BY last_name, first_name, patronymic", OleDbCon);
                OleDbDataAdapter eduAdapter = new OleDbDataAdapter("SELECT * FROM spr_edu", OleDbCon);
                OleDbDataAdapter mstatusAdapter = new OleDbDataAdapter("SELECT * FROM spr_mstatus", OleDbCon);
                OleDbDataAdapter profAdapter = new OleDbDataAdapter("SELECT * FROM spr_profession", OleDbCon);
                OleDbDataAdapter nationAdapter = new OleDbDataAdapter("SELECT * FROM spr_nation", OleDbCon);
                
                //OleDbDataAdapter partyAdapter = new OleDbDataAdapter("SELECT * FROM spr_party_number", OleDbCon);
                SQLDBConnect sqlCon = new SQLDBConnect();
                String dep = sqlCon.GetSystemValue("Department");
                String sqlStr = @"SELECT * FROM spr_party_number WHERE department_id='" + dep + @"'";
                OleDbDataAdapter partyAdapter = new OleDbDataAdapter(sqlStr, OleDbCon);


                OleDbDataAdapter spr_profilactAdapter = new OleDbDataAdapter("SELECT * FROM spr_profilact_ychet", OleDbCon);
                OleDbDataAdapter enum_periodAdapter = new OleDbDataAdapter("SELECT * FROM enum_period", OleDbCon);

                OleDbCommandBuilder specBuilder = new OleDbCommandBuilder(specAdapter);
                OleDbCommandBuilder mstatusBuilder = new OleDbCommandBuilder(mstatusAdapter);
                OleDbCommandBuilder profBuilder = new OleDbCommandBuilder(profAdapter);
                OleDbCommandBuilder eduBuilder = new OleDbCommandBuilder(eduAdapter);
                OleDbCommandBuilder nationBuilder = new OleDbCommandBuilder(nationAdapter);
                OleDbCommandBuilder partyBuilder = new OleDbCommandBuilder(partyAdapter);
                OleDbCommandBuilder spr_profilactBuilder = new OleDbCommandBuilder(spr_profilactAdapter);
//                OleDbCommandBuilder enum_periodBuilder = new OleDbCommandBuilder(enum_periodAdapter);

                DataSet ds = new DataSet();
                specAdapter.Fill(ds, "spec");
                eduAdapter.Fill(ds, "spr_edu");
                mstatusAdapter.Fill(ds, "spr_mstatus");
                profAdapter.Fill(ds, "spr_profession");
                nationAdapter.Fill(ds, "spr_nation");
                partyAdapter.Fill(ds, "spr_party_number");
                spr_profilactAdapter.Fill(ds, "spr_profilact_ychet");
                enum_periodAdapter.Fill(ds, "enum_period");

                BuildTableRelationship(ds);

                MainDataAdapter = specAdapter;
                MainDataSet = ds;
                OleDbCon.Close();
            }
            catch (OleDbException odbe)
            {
                MessageBox.Show(odbe.Message, "Ошибка");
            }
        }


        public void RefreshDataTable()
        {
            try
            {
                OleDbCon.Open();
                MainDataSet.Tables["spec"].Clear();
                OleDbCon.Close();

                PrepareTables();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка");
                //Console.WriteLine(e.Message);
            }
        }

        private Object getValidDate(Object objDate)
        {
            try
            {
                if (Convert.ToString(objDate).Trim() == String.Empty)
                    return DBNull.Value;
                DateTime.Parse(Convert.ToString(objDate)).ToString("s");
                return objDate;
            }
            catch (FormatException)
            {
                return DBNull.Value;
            }
        }

        private String getValidString(Object objString)
        {
            try
            {
                if (objString == null)
                    return String.Empty;
                String str = Convert.ToString(objString);
                MessageBox.Show(str);
                return str;
            }
            catch (NullReferenceException)
            {
                return String.Empty;
            }
        }

        private void SetRowData(ProfileData pd, DataRow row)
        {
            row["is_present"] = true;
            row["last_name"] = pd.last_name;
            row["first_name"] = pd.first_name;
            row["patronymic"] = pd.patronymic;
            row["birthdate"] = getValidDate(pd.birthdate);

            row["edu_id"] = pd.edu_id;
            row["mstatus_id"] = pd.mstatus_id;
            row["profession_id"] = pd.profession_id;
            row["nation_id"] = pd.nation_id;
            row["party_id"] = pd.party_id;

            row["court"] = pd.court;
            row["article"] = pd.article;
            row["crime_date"] = getValidDate( pd.crime_date);
            row["period"] = pd.period;
            row["period_start"] =getValidDate(  pd.period_start);
            row["period_end"] = getValidDate( pd.period_end);

            row["period_light"] = getValidDate( pd.period_light);
            row["period_normal"] = getValidDate( pd.period_normal);
            row["period_kp"] = getValidDate( pd.period_kp);
            row["period_udo"] = getValidDate( pd.period_udo);

            row["crime_description"] = pd.crime_desc;
            row["med_description"] = pd.med_desc;
            row["other"] = pd.other;
            row["result"] = pd.result;

            row["foto"] = pd.foto;

        }

        public Boolean SaveProfileData(ProfileData pd, DataRow afRow)
        {
            try
            {
                OleDbCon.Open();

                DataTable dt = MainDataSet.Tables["spec"];

                SetRowData(pd, afRow);

                //MainDataSet.Tables["spec"].Rows.Add(afRow);

                MainDataAdapter.Update(MainDataSet, "spec");

                OleDbCon.Close();

                return true;
            }
            catch (Exception e)
            {
                OleDbCon.Close();
                MessageBox.Show(e.Message, "Ошибка");
                RefreshDataTable();
                return false;
            }
        }
        public Boolean SaveProfileData(ProfileData pd)
        {
            try
            {
                OleDbCon.Open();

                //PrepareTables();

//                DataTable dt = MainDataSet.Tables["spec"];

                DataRow newRow = MainDataSet.Tables["spec"].NewRow();

                SetRowData(pd, newRow);
                
                MainDataSet.Tables["spec"].Rows.Add(newRow);

                //Обновим данные таблицы в БД
                MainDataAdapter.Update(MainDataSet, "spec");

                //Обновим содержимое таблицы из БД в памяти
                MainDataSet.Tables["spec"].Clear();
                MainDataAdapter.Fill(MainDataSet, "spec");
                    
                OleDbCon.Close();

                return true;
            }
            catch (Exception e)
            {
                OleDbCon.Close();
                MessageBox.Show(e.Message, "Ошибка");
                RefreshDataTable();
                return false;
            }
        }

        private void BuildTableRelationship(DataSet ds)
        {
            DataRelation dr = new DataRelation("EduOrder", 
                                               ds.Tables["spec"].Columns["edu_id"],
                                                ds.Tables["spr_edu"].Columns["id"],false);
            ds.Relations.Add(dr);

            dr = new DataRelation("ProfOrder",
                                                    ds.Tables["spec"].Columns["profession_id"],
                                                    ds.Tables["spr_profession"].Columns["id"],false);
            ds.Relations.Add(dr);

            dr = new DataRelation("NationOrder",
                                                    ds.Tables["spec"].Columns["nation_id"],
                                                    ds.Tables["spr_nation"].Columns["id"],false);
            ds.Relations.Add(dr);

            dr = new DataRelation("PartyOrder",
                                                    ds.Tables["spec"].Columns["party_id"],
                                                    ds.Tables["spr_party_number"].Columns["id"], false);
            ds.Relations.Add(dr);

            dr = new DataRelation("MstatusOrder",
                                                    ds.Tables["spec"].Columns["mstatus_id"],
                                                    ds.Tables["spr_mstatus"].Columns["id"],false);
            ds.Relations.Add(dr);

            dr = new DataRelation("PsihoKorrecOrder",
                                                    ds.Tables["spr_profilact_ychet"].Columns["psiho_korrec_id"],
                                                    ds.Tables["enum_period"].Columns["id"], false);
            ds.Relations.Add(dr);

            dr = new DataRelation("PsihoObsledOrder",
                                                    ds.Tables["spr_profilact_ychet"].Columns["psiho_obsled_id"],
                                                    ds.Tables["enum_period"].Columns["id"], false);
            ds.Relations.Add(dr);
        }

        public String getFotoPath()
        {
            RegistryKey regKey = Registry.CurrentUser;
            regKey = regKey.CreateSubKey("Software\\UFSIN\\ivrJournal");
            String dbPath = regKey.GetValue("dbPath", @"c:\Program files\ufsin_rk\Дневник ИВР\divr.mdb").ToString();

            FileInfo file = new FileInfo(dbPath);
            String dbDirPath = file.DirectoryName;
            
            String strFilename = DateTime.UtcNow.ToBinary().ToString() + @".jpg";
            String strDirname = strFilename.Substring(0, 9);

            DirectoryInfo dir = new DirectoryInfo(dbDirPath + @"\foto");
            dir.Create();
            
            dir = new DirectoryInfo(dbDirPath + @"\foto\" + strDirname);
            dir.Create();

            String strFotoPath = @"\foto\" + strDirname + @"\" + strFilename;
            return strFotoPath;
        }

        public String GetDBDirPath()
        {
            RegistryKey regKey = Registry.CurrentUser;
            regKey = regKey.CreateSubKey("Software\\UFSIN\\ivrJournal");
            String dbPath = regKey.GetValue("dbPath", @"C:\Program files\ufsin_rk\Дневник ИВР\divr.mdb").ToString();
            FileInfo file = new FileInfo(dbPath);
            String dbDirPath = file.DirectoryName;
            return dbDirPath;
        }

        public void MarkAsNotPresent(DataRow afRow)
        {
            try
            {
                OleDbCon.Open();
                DataTable dt = MainDataSet.Tables["spec"];
                afRow.BeginEdit();
                afRow["is_present"] = false;
                afRow.EndEdit();
                MainDataAdapter.Update(MainDataSet, "spec");
                OleDbCon.Close();
            }
            catch (Exception e)
            {
                OleDbCon.Close();
                MessageBox.Show(e.Message, "Ошибка");
                return;
            }

        }

        public void MarkAsPresent(DataRow afRow)
        {
            try
            {
                OleDbCon.Open();
                DataTable dt = MainDataSet.Tables["spec"];
                afRow.BeginEdit();
                afRow["is_present"] = true;
                afRow.EndEdit();
                MainDataAdapter.Update(MainDataSet, "spec");
                OleDbCon.Close();
            }
            catch (Exception e)
            {
                OleDbCon.Close();
                MessageBox.Show(e.Message, "Ошибка");
                return;
            }

        }

    }
}


