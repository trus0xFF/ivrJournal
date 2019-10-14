using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;
using Microsoft.Win32;
using System.IO;

namespace ivrJournal
{
    class SprDbConnect
    {

        private DataSet sprSet;
        private OleDbConnection OleDbCon;
        private OleDbDataAdapter dataAdapter;
        private OleDbCommandBuilder builder;

        const String DataProvider = @"Microsoft.Jet.OLEDB.4.0";

        public SprDbConnect(params String[] tableNameList)
        {
            RegistryKey regKey = Registry.CurrentUser;
            regKey = regKey.OpenSubKey("Software\\UFSIN\\ivrJournal");

            OleDbConnectionStringBuilder bldr = new OleDbConnectionStringBuilder();

            bldr.DataSource = regKey.GetValue("dbPath", "").ToString(); // Указываем путь
            bldr.Provider = DataProvider; // Указываем провайдера
            
            bldr.Add("Jet OLEDB:Database Password", "ivr32a");
            
            OleDbCon = new OleDbConnection(bldr.ConnectionString);

            sprSet = new DataSet();
            for (int i = 0; i < tableNameList.Length; i++ )
            {
                InitDataTable( tableNameList[i] );
            }
            
        }


        public DataTable GetDataTable(String tableName)
        {
            return sprSet.Tables[tableName];
        }

        private DataTable InitDataTable(String tableName)
        {
            try
            {
                OleDbCon.Open();

                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandText = @"SELECT * FROM " + tableName; // Задаем оператор SQL
                cmd.Connection = OleDbCon; // Задаем источник данных

                // Объект OleDbDataAdapter выполняет функцию моста между DataTable и источником данных.
//                OleDbDataAdapter dataAdapter = new OleDbDataAdapter();
                dataAdapter = new OleDbDataAdapter();
                dataAdapter.SelectCommand = cmd;

                builder = new OleDbCommandBuilder(dataAdapter);
//                OleDbCommandBuilder builder = new OleDbCommandBuilder(dataAdapter);

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

        public void RefreshDataTable( String tableName )
        {
            try
            {
                OleDbCon.Open();
                sprSet.Tables[tableName].Clear();
                OleDbCon.Close();

                InitDataTable(tableName);

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка");
            }
        }

        public void UpdateDataTable(String tableName)
        {
            try
            {
                OleDbCon.Open();

                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandText = @"SELECT * FROM " + tableName; // Задаем оператор SQL
                cmd.Connection = OleDbCon; // Задаем источник данных
                // Объект OleDbDataAdapter выполняет функцию моста между DataTable и источником данных.
//                OleDbDataAdapter dataAdapter = new OleDbDataAdapter();
                dataAdapter = new OleDbDataAdapter();

                dataAdapter.SelectCommand = cmd;

                OleDbCommandBuilder builder = new OleDbCommandBuilder(dataAdapter);

                dataAdapter.Update(sprSet, tableName);
                
                OleDbCon.Close();

                return;
            }
            catch (Exception e)
            {
                OleDbCon.Close();
                MessageBox.Show(e.Message, "Ошибка");
                return;
            }
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

        public String GetFotoPath()
        {
            RegistryKey regKey = Registry.CurrentUser;
            regKey = regKey.CreateSubKey("Software\\UFSIN\\ivrJournal");
            String dbPath = regKey.GetValue("dbPath", @"C:\Program files\ufsin_rk\Дневник ИВР\divr.mdb").ToString();

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

    }
}
