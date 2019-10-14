using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace ivrJournal
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
            SQLDBConnect conSql = new SQLDBConnect();
            lblDBVersion.Text = conSql.GetSystemValue("Database Version");
            lblPrgVersion.Text = AssemblyName.GetAssemblyName(Assembly.Load("ivrJournal").Location).Version.ToString();
        }

        private void bnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}