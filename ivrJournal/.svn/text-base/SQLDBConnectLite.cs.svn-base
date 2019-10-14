using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;
using Microsoft.Win32;

namespace ivrJournal
{
    class SQLDBConnectLite
    {
        private OleDbConnection OleDbCon;
        const String DataProvider = @"Microsoft.Jet.OLEDB.4.0";

        public SQLDBConnectLite()
        {
            OleDbConnectionStringBuilder bldr = new OleDbConnectionStringBuilder();

            bldr.DataSource = IVRShared.GetDBPath();
            bldr.Provider = DataProvider; // Указываем провайдера
            bldr.Add("Jet OLEDB:Database Password", "ivr32a");

            OleDbCon = new OleDbConnection(bldr.ConnectionString);
        }

        public void DoQuery(String stringSQL)
        {
            try
            {
                OleDbCon.Open();
                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandText = @stringSQL;
                cmd.Connection = OleDbCon;
                cmd.ExecuteNonQuery();
                OleDbCon.Close();
            }
            catch (Exception e)
            {
                OleDbCon.Close();
                MessageBox.Show(e.Message, "Ошибка");
                return;
            }
        }

        public DataTable GetDataTable(String tableName, String stringSQL)
        {
            try
            {
                DataSet sprSet = new DataSet();
                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandText = @stringSQL;
                cmd.Connection = OleDbCon;
                OleDbDataAdapter dataAdapter = new OleDbDataAdapter();
                dataAdapter.SelectCommand = cmd;

                OleDbCon.Open();
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

        public String GetSystemValue(String sysPrm)
        {
            try
            {
                DataSet sprSet = new DataSet();
                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandText = @"SELECT * FROM system WHERE name='" + sysPrm + "'";
                cmd.Connection = OleDbCon;

                OleDbDataAdapter dataAdapter = new OleDbDataAdapter();
                dataAdapter.SelectCommand = cmd;

                OleDbCon.Open();
                dataAdapter.Fill(sprSet, "system");
                OleDbCon.Close();

                if (sprSet.Tables["system"].Rows.Count == 0)
                    return null;
                else
                    return sprSet.Tables["system"].Rows[0]["system_value"].ToString();
            }
            catch (Exception e)
            {
                OleDbCon.Close();
                MessageBox.Show(e.Message, "Ошибка");
                return null;
            }
        }

        public String GetValue(String stringValue, String stringSQL)
        {
            try
            {
                DataSet sprSet = new DataSet();
                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandText = @stringSQL;
                cmd.Connection = OleDbCon;

                OleDbDataAdapter dataAdapter = new OleDbDataAdapter();
                dataAdapter.SelectCommand = cmd;

                OleDbCon.Open();
                dataAdapter.Fill(sprSet, "tmp");
                OleDbCon.Close();

                if (sprSet.Tables["tmp"].Rows.Count == 0)
                    return null;
                else
                    return sprSet.Tables["tmp"].Rows[0][stringValue].ToString();
            }
            catch (Exception e)
            {
                OleDbCon.Close();
                MessageBox.Show(e.Message, "Ошибка");
                return null;
            }

        }


    }
}
