using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;
using Microsoft.Win32;


namespace ivrJournal
{
    public class ChildDBConnect
    {

        private DataSet sprSet;
        private OleDbConnection OleDbCon;
        private OleDbDataAdapter dataAdapter;
        private string tabName;
        private string pName = "";
        
        const String DataProvider = @"Microsoft.Jet.OLEDB.4.0";


        public ChildDBConnect(int spec_id, String tableName)
        {
            tabName = tableName;
            RegistryKey regKey = Registry.CurrentUser;
            regKey = regKey.OpenSubKey("Software\\UFSIN\\ivrJournal");

            OleDbConnectionStringBuilder bldr = new OleDbConnectionStringBuilder();

            bldr.DataSource = regKey.GetValue("dbPath", "").ToString(); // Указываем путь
            bldr.Provider = DataProvider; // Указываем провайдера

            bldr.Add("Jet OLEDB:Database Password", "ivr32a");

            OleDbCon = new OleDbConnection(bldr.ConnectionString);

            sprSet = new DataSet();
            InitDataTable(spec_id, tableName);
        }

        public ChildDBConnect(int spec_id, String tableName, String punktName)
        {
            tabName = tableName;
            RegistryKey regKey = Registry.CurrentUser;
            regKey = regKey.OpenSubKey("Software\\UFSIN\\ivrJournal");

            OleDbConnectionStringBuilder bldr = new OleDbConnectionStringBuilder();

            bldr.DataSource = regKey.GetValue("dbPath", "").ToString(); // Указываем путь
            bldr.Provider = DataProvider; // Указываем провайдера

            bldr.Add("Jet OLEDB:Database Password", "ivr32a");

            OleDbCon = new OleDbConnection(bldr.ConnectionString);

            sprSet = new DataSet();
            if (tableName == "spec_psycho")
                InitDataTableSpecPsyho(spec_id, tableName, punktName);
            else
                InitDataTableIVR(spec_id, tableName, punktName);
        }

        public DataTable GetDataTable(String tableName)
        {
            return sprSet.Tables[tableName];
        }

        private DataTable InitDataTableSpecPsyho(int spec_id, String tableName, String punktName)
        {
            pName = punktName;
            try
            {
                OleDbCon.Open();
                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandText = @"SELECT * FROM " + tableName + " WHERE ((type_doc = " + punktName + ") AND (id_spec = " + spec_id + "))"; // Задаем оператор SQL
                cmd.Connection = OleDbCon; // Задаем источник данных
                dataAdapter = new OleDbDataAdapter();
                dataAdapter.SelectCommand = cmd;
                OleDbCommandBuilder builder = new OleDbCommandBuilder(dataAdapter);
                dataAdapter.Fill(sprSet, tableName);
                OleDbCon.Close();

                return sprSet.Tables[tableName];
            }
            catch (Exception e)
            {
                OleDbCon.Close();
                MessageBox.Show(e.Message, "Ошибка");
                return null;
            }
        }


        private DataTable InitDataTableIVR(int spec_id, String tableName, String punktName)
        {
            pName = punktName;
            try
            {
                OleDbCon.Open();
                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandText = @"SELECT * FROM " + tableName + " WHERE ((id_type_ivr = " + punktName + ") AND (id_spec = " + spec_id + "))"; // Задаем оператор SQL
                cmd.Connection = OleDbCon; // Задаем источник данных
//                OleDbDataAdapter dataAdapter = new OleDbDataAdapter();
                dataAdapter = new OleDbDataAdapter();
                dataAdapter.SelectCommand = cmd;

                
                
                
                /*
                cmd = new OleDbCommand();
                cmd.CommandText = @"INSERT INTO " + tableName + " (data_ivr, employee_id, content, work_type, description, id_type_ivr, id_spec) VALUES (?, ?, ?, ?, ?, " + punktName + ", " + spec_id  + ")"; // Задаем оператор SQL
                cmd.Parameters.Add("@data_ivr", OleDbType.Date, 50, "data_ivr");
                cmd.Parameters.Add("@employee_id", OleDbType.Integer, 50, "employee_id");
                cmd.Parameters.Add("@content", OleDbType.VarWChar, 60000, "content");
                cmd.Parameters.Add("@work_type", OleDbType.Integer, 50, "work_type");
                cmd.Parameters.Add("@description", OleDbType.VarChar, 250, "description");
                cmd.Connection = OleDbCon; // Задаем источник данных
                dataAdapter.InsertCommand = cmd;

                cmd = new OleDbCommand();
                cmd.CommandText = @"UPDATE " + tableName + " SET data_ivr = ?, employee_id = ?, content = ?, work_type = ?, description = ?, id_type_ivr = " + punktName + ", id_spec = " + spec_id + " WHERE (id = ?)"; // Задаем оператор SQL
                cmd.Parameters.Add("@data_ivr", OleDbType.Date, 50, "data_ivr");
                cmd.Parameters.Add("@employee_id", OleDbType.Integer, 50, "employee_id");
                cmd.Parameters.Add("@content", OleDbType.VarWChar, 60000, "content");
                cmd.Parameters.Add("@work_type", OleDbType.Integer, 50, "work_type");
                cmd.Parameters.Add("@description", OleDbType.VarChar, 250, "description");
                cmd.Parameters.Add("@id", OleDbType.Integer, 50, "id");
                cmd.Connection = OleDbCon; // Задаем источник данных
                dataAdapter.UpdateCommand = cmd;

                cmd = new OleDbCommand();
                cmd.CommandText = @"DELETE FROM " + tableName + " WHERE (id = ?)"; // Задаем оператор SQL
                cmd.Parameters.Add("@id", OleDbType.Integer, 50, "id");
                cmd.Connection = OleDbCon; // Задаем источник данных
                dataAdapter.DeleteCommand = cmd;
*/
                OleDbCommandBuilder builder = new OleDbCommandBuilder(dataAdapter);
                /*                dataAdapter.UpdateCommand = builder.GetUpdateCommand();
                                dataAdapter.InsertCommand = builder.GetInsertCommand();
                                dataAdapter.DeleteCommand = builder.GetDeleteCommand();
                */
                
                dataAdapter.Fill(sprSet, tableName);
                OleDbCon.Close();

                return sprSet.Tables[tableName];
            }
            catch (Exception e)
            {
                OleDbCon.Close();
                MessageBox.Show(e.Message, "Ошибка");
                return null;
            }
        }

        private DataTable InitDataTable(int spec_id, String tableName)
        {
            try
            {
                OleDbCon.Open();

                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandText = @"SELECT * FROM " + tableName + " WHERE (id_spec = " + spec_id + ")"; // Задаем оператор SQL
                cmd.Connection = OleDbCon; // Задаем источник данных
//                OleDbDataAdapter dataAdapter = new OleDbDataAdapter();
                dataAdapter = new OleDbDataAdapter();
                dataAdapter.SelectCommand = cmd;
                OleDbCommandBuilder builder = new OleDbCommandBuilder(dataAdapter);
                dataAdapter.Fill(sprSet, tableName);
                OleDbCon.Close();

                return sprSet.Tables[tableName];
            }
            catch (Exception e)
            {
                OleDbCon.Close();
                MessageBox.Show(e.Message, "Ошибка");
                return null;
            }
        }

        public void RefreshDataTable(int spec_id, String tableName )
        {
            try
            {
                OleDbCon.Open();
                sprSet.Tables[tableName].Clear();
                OleDbCon.Close();

                if (tabName == "ivr")
                    InitDataTableIVR(spec_id, tableName, pName);
                else if (tabName == "spec_psycho")
                    InitDataTableSpecPsyho(spec_id, tableName, pName);
                else
                    InitDataTable(spec_id, tableName);

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка");
            }
        }

        public void UpdateDataTable(int spec_id, String tableName)
        {
            try
            {
                OleDbCon.Open();
/*
                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandText = @"SELECT * FROM " + tableName + " WHERE (id_spec = " + spec_id + ")"; // Задаем оператор SQL
                cmd.Connection = OleDbCon; // Задаем источник данных
                OleDbDataAdapter dataAdapter = new OleDbDataAdapter();
                dataAdapter.SelectCommand = cmd;
                OleDbCommandBuilder builder = new OleDbCommandBuilder(dataAdapter);
*/
                dataAdapter.Update(sprSet, tableName);
//                sprSet.AcceptChanges();

                OleDbCon.Close();

                return ;
//                return;
            }
            catch (Exception e)
            {
                OleDbCon.Close();
                MessageBox.Show(e.Message, "Ошибка");
                return;
            }
        }

        private void SetRowData(PsychoData pd, DataRow row)
        {
            row["psycho_doc"] = pd.psycho_doc;
            row["date_doc"] = pd.date_doc;
            row["name"] = pd.name;
            row["id_spec"] = pd.id_spec;
            row["type_doc"] = pd.type_doc;
        }

        public Boolean SavePsychoData(PsychoData pd, DataRow afRow)
        {
            try
            {
                OleDbCon.Open();
                DataTable dt = sprSet.Tables["spec_psycho"];
                SetRowData(pd, afRow);
                dataAdapter.Update(sprSet, "spec_psycho");
                OleDbCon.Close();
                return true;
            }
            catch (Exception e)
            {
                OleDbCon.Close();
                MessageBox.Show(e.Message, "Ошибка");
                return false;
            }
        }
        public Boolean SavePsychoData(PsychoData pd)
        {
            try
            {
                OleDbCon.Open();
                DataTable dt = sprSet.Tables["spec_psycho"];
                DataRow newRow = sprSet.Tables["spec_psycho"].NewRow();
                SetRowData(pd, newRow);
                sprSet.Tables["spec_psycho"].Rows.Add(newRow);
                //Обновим данные таблицы в БД
                dataAdapter.Update(sprSet, "spec_psycho");
                //Обновим содержимое таблицы из БД в памяти
                sprSet.Tables["spec_psycho"].Clear();
                dataAdapter.Fill(sprSet, "spec_psycho");
                OleDbCon.Close();
                return true;
            }
            catch (Exception e)
            {
                OleDbCon.Close();
                MessageBox.Show(e.Message, "Ошибка");
                return false;
            }
        }

    }
}
