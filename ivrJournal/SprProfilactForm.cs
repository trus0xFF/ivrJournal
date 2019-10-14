using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace ivrJournal
{
    public partial class SprProfilactForm : Form
    {
        private SprDbConnect newDBcon;
        

        public SprProfilactForm()
        {
            InitializeComponent();

            newDBcon = new SprDbConnect("spr_profilact_ychet", "enum_period");

            dgListProfilact.Columns.Add(new DataGridViewTextBoxColumn());
            dgListProfilact.Columns[0].DataPropertyName = "id";
            dgListProfilact.Columns[0].HeaderText = "код";
            dgListProfilact.Columns[0].Visible = false;
            dgListProfilact.Columns.Add(new DataGridViewTextBoxColumn());
            dgListProfilact.Columns[1].DataPropertyName = "name";
            dgListProfilact.Columns[1].HeaderText = "Наименование";
            dgListProfilact.Columns[1].Width = 150;
            dgListProfilact.Columns[1].MinimumWidth = 150;
            dgListProfilact.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            DataGridViewCheckBoxColumn columnCheck =
                new DataGridViewCheckBoxColumn();
            {
                columnCheck.DataPropertyName = "pasport";
                columnCheck.HeaderText = "Паспорт";
                columnCheck.Width = 50;
                columnCheck.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            }
            dgListProfilact.Columns.Add(columnCheck);

            columnCheck =
                new DataGridViewCheckBoxColumn();
            {
                columnCheck.DataPropertyName = "plan";
                columnCheck.HeaderText = "План";
                columnCheck.Width = 50;
                columnCheck.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            }
            dgListProfilact.Columns.Add(columnCheck);

            DataGridViewComboBoxColumn column =
                new DataGridViewComboBoxColumn();
            {
                column.DataPropertyName = "psiho_korrec_id";
                column.HeaderText = "Периодичность коррекции";
                column.DropDownWidth = 100;
                column.Width = 90;
                columnCheck.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.MaxDropDownItems = 3;
                column.FlatStyle = FlatStyle.Flat;

                column.DataSource = newDBcon.GetDataTable("enum_period");
                column.ValueMember = "id";
                column.DisplayMember = "name";

            }
            dgListProfilact.Columns.Add(column);

            column =
                new DataGridViewComboBoxColumn();
            {
                column.DataPropertyName = "psiho_obsled_id";
                column.HeaderText = "Периодичность обследования";
                column.DropDownWidth = 100;
                column.Width = 90;
                columnCheck.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.MaxDropDownItems = 3;
                column.FlatStyle = FlatStyle.Flat;

                column.DataSource = newDBcon.GetDataTable("enum_period");
                column.ValueMember = "id";
                column.DisplayMember = "name";

            }
            dgListProfilact.Columns.Add(column);

            dgListProfilact.DataSource = newDBcon.GetDataTable("spr_profilact_ychet");
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
/*
        private void button1_Click(object sender, EventArgs e)
        {
            newDBcon.UpdateDataTable("spr_profilact_ychet");
        }

        private void button2_Click(object sender, EventArgs e)
        {
//            currentRow = dgListProfilact.CurrentRow;
            dgListProfilact.DataSource = newDBcon.GetDataTable("spr_profilact_ychet");

        }
*/
        private void tsbNew_Click(object sender, EventArgs e)
        {
            dgListProfilact.CurrentCell = dgListProfilact[1, dgListProfilact.NewRowIndex];
            dgListProfilact.Focus();
            dgListProfilact.BeginEdit(true);
        }

        private void tsbEdit_Click(object sender, EventArgs e)
        {
            int index = dgListProfilact.CurrentRow.Index;
            if (index != -1)
            {
                dgListProfilact.BeginEdit(true);
            }
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {
            if (!IVRShared.IsCurrentUserAdmin())
            {
                MessageBox.Show("Удалять записи может только Пользователь с правами Администратора!");
                return;
            }

            int index = dgListProfilact.CurrentRow.Index;
            if ((index != -1) & (index != dgListProfilact.NewRowIndex))
            {
                try
                {
                    dgListProfilact.Rows.RemoveAt(index);
                    newDBcon.UpdateDataTable("spr_profilact_ychet");
                }
                catch
                {
                    MessageBox.Show("Запись в базе не обнаружена!!!", "Сообщение о базе", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
        }

        private void tsbRefresh_Click(object sender, EventArgs e)
        {
            newDBcon.RefreshDataTable("spr_profilact_ychet");
        }

        private void dgListProfilact_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            newDBcon.UpdateDataTable("spr_profilact_ychet");
        }

        private void SprProfilactForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            newDBcon.UpdateDataTable("spr_profilact_ychet");
        }

    }
}