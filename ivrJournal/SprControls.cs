using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows;
using System.Windows.Forms;


namespace ivrJournal
{
    public partial class SprControls : Form
    {
        private SprDbConnect newDBcon;
        private FormControlManager fcm;

        public SprControls()
        {
            fcm = new FormControlManager();
            InitializeComponent();
            newDBcon = new SprDbConnect("spr_user_controls");
            dgControls.DataSource = newDBcon.GetDataTable("spr_user_controls");
            dgControls.Columns[0].HeaderText = "Код";
            dgControls.Columns[0].Width = 50;
            dgControls.Columns[0].DataPropertyName = "id";
            dgControls.Columns[0].Visible = false;
            dgControls.Columns[1].HeaderText = "Наименование формы";
            dgControls.Columns[1].Width = 100;
            dgControls.Columns[1].DataPropertyName = "name_form";
            dgControls.Columns[1].Visible = false;

            dgControls.Columns[2].HeaderText = "Наименование системное";
            dgControls.Columns[2].Width = 180;
            dgControls.Columns[2].DataPropertyName = "name";
            dgControls.Columns[2].Visible = false;

            dgControls.Columns[3].HeaderText = "Наименование элемента";
            dgControls.Columns[3].Width = 150;
            dgControls.Columns[3].DataPropertyName = "name_rus";

            dgControls.Columns[4].HeaderText = "Описание";
            dgControls.Columns[4].Width = 150;
            dgControls.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgControls.Columns[4].DataPropertyName = "opisanie";
        }

        private void bnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void bnLoad_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("При полной перезагрузке форм будет полностью очищена таблица прав пользователей! Вы уверены, что хотите продолжить?", "Полная перезагрузка форм", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                return;
            fcm.ReloadControls("MainForm", "KartForm", "ProfileForm", "KartArchiveForm");

            newDBcon.RefreshDataTable("spr_user_controls");

        }

        private void bnUpdate_Click(object sender, EventArgs e)
        {
            fcm.InsertControls("MainForm", "KartForm", "ProfileForm", "KartArchiveForm");
            newDBcon.RefreshDataTable("spr_user_controls");
        }

    }
}