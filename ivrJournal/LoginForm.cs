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
    public partial class LoginForm : Form
    {
        private DBConDialog dbConDialog;

        public LoginForm()
        {
            InitializeComponent();

            RegistryKey regKey = Registry.CurrentUser;
            regKey = regKey.CreateSubKey("Software\\UFSIN\\ivrJournal");
            loginBox.Text = regKey.GetValue("login", "admin").ToString();

        }

        private void buttonSetting_Click(object sender, EventArgs e)
        {
            dbConDialog = new DBConDialog();
            dbConDialog.MaximizeBox = false;
            dbConDialog.MinimizeBox = false;
            dbConDialog.ControlBox = false;
            dbConDialog.ShowDialog();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            RegistryKey regKey = Registry.CurrentUser;
            regKey = regKey.CreateSubKey("Software\\UFSIN\\ivrJournal");
            String dbPath = regKey.GetValue("dbPath", @"C:\Program files\ufsin_rk\Дневник ИВР\divr.mdb").ToString();

            SQLDBConnect newDBcon = new SQLDBConnect();
            if (!newDBcon.DBConCheckBool())
            {
                MessageBox.Show("Ошибка соединения с базой данных. Проверьте настройки.");
                DialogResult = DialogResult.None;
                return;
            }

            String login = loginBox.Text;
            String password = passwordBox.Text;
            if (newDBcon.checkUser(login, password) == false)
            { 
                MessageBox.Show("Пользователь с таким именем и паролем не обнаружен. В доступе отказано. ");
                DialogResult = DialogResult.None;
                return;
            }

            regKey.SetValue("login", login);

            return;

        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LoginForm_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            Help.ShowHelp(this, "help_ivr.chm", HelpNavigator.Topic, "help_ivr_1.htm");
        }
    }
}