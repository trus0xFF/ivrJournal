using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

namespace ivrJournal
{
    public partial class DBConDialog : Form
    {
        public DBConDialog()
        {
            InitializeComponent();
            RegistryKey regKey = Registry.CurrentUser;
            regKey = regKey.CreateSubKey("Software\\UFSIN\\ivrJournal");
            this.tbDBPath.Text = regKey.GetValue("dbPath",@"C:\Program files\ufsin_rk\������� ���\divr.mdb").ToString();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void bnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bnFind_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            //openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.InitialDirectory = this.tbDBPath.Text;
            openFileDialog.Filter = "Databases Files (*.mdb)|*.mdb|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                //string FileName = openFileDialog.FileName;
                this.tbDBPath.Text = openFileDialog.FileName;
                // TODO: Add code here to open the file.
                
                RegistryKey regKey = Registry.CurrentUser;

                regKey = regKey.CreateSubKey("Software\\UFSIN\\ivrJournal");
                regKey.SetValue("dbPath", this.tbDBPath.Text);
                if (this.MdiParent != null)
                    ((MainForm)this.MdiParent).dbPath = this.tbDBPath.Text;

            }
        }

        private void bnSave_Click(object sender, EventArgs e)
        {
            RegistryKey regKey = Registry.CurrentUser;
            
            regKey = regKey.CreateSubKey("Software\\UFSIN\\ivrJournal");
            regKey.SetValue("dbPath", this.tbDBPath.Text);
            if (this.MdiParent != null)
                ((MainForm)this.MdiParent).dbPath = this.tbDBPath.Text;

            this.Close();
        }

        private void bnCheckCon_Click(object sender, EventArgs e)
        {
            SQLDBConnect newDBcon = new SQLDBConnect();
            if(newDBcon.DBConCheckBool())
                MessageBox.Show("���������� ������� �����������", "���������");
            else
                MessageBox.Show("������ ���������� � ����� ������", "���������");
        }
    }
}