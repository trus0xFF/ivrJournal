using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ivrJournal
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            LoginForm logF = new LoginForm();

            if (logF.ShowDialog() != DialogResult.OK)
            {
                //MessageBox.Show("Ошибка соединения с базой данных. Проверьте настройки");
                return;
            }
         
            Application.Run(new MainForm("KartForm"));
        }

    }
}