using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ivrJournal
{
    public partial class UsersForm : Form
    {
        private SprDbConnect newDBcon;

        public UsersForm()
        {
            InitializeComponent();
            newDBcon = new SprDbConnect("users", "spr_user_rol");
            LoadGrid(dgUsers);
            dgUsers.DataSource = newDBcon.GetDataTable("users");

        }

        private void LoadGrid(DataGridView dg)
        {
            DataGridViewTextBoxColumn textColumn = new DataGridViewTextBoxColumn();
            {
                textColumn.DataPropertyName = "id";
                textColumn.Visible = false;
            }
            dg.Columns.Add(textColumn);

            textColumn = new DataGridViewTextBoxColumn();
            {
                textColumn.DataPropertyName = "login";
                textColumn.HeaderText = "Логин";
                textColumn.Width = 60;
                textColumn.MinimumWidth = 50;
                textColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            }
            dg.Columns.Add(textColumn);

            textColumn = new DataGridViewTextBoxColumn();
            {                                   
                textColumn.DataPropertyName = "passwd";
                textColumn.HeaderText = "Пароль";
                textColumn.Width = 60;
                textColumn.MinimumWidth = 50;
                textColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            }
            dg.Columns.Add(textColumn);

            DataGridViewComboBoxColumn column =
             new DataGridViewComboBoxColumn();
            {
                column.DataPropertyName = "user_rol_id";
                column.HeaderText = "Статус пользователя";
                column.DropDownWidth = 150;
                column.Width = 110;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.MaxDropDownItems = 5;
                column.FlatStyle = FlatStyle.Flat;

                column.DataSource = newDBcon.GetDataTable("spr_user_rol");
                column.ValueMember = "id";
                column.DisplayMember = "name";
            }
            dg.Columns.Add(column);
            
            textColumn = new DataGridViewTextBoxColumn();
            {
                textColumn.DataPropertyName = "last_name";
                textColumn.HeaderText = "Фамилия";
                textColumn.Width = 100;
                textColumn.MinimumWidth = 70;
                textColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            }
            dg.Columns.Add(textColumn);

            textColumn = new DataGridViewTextBoxColumn();
            {
                textColumn.DataPropertyName = "first_name";
                textColumn.HeaderText = "Имя";
                textColumn.Width = 100;
                textColumn.MinimumWidth = 70;
                textColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            }
            dg.Columns.Add(textColumn);

            textColumn = new DataGridViewTextBoxColumn();
            {
                textColumn.DataPropertyName = "patronymic";
                textColumn.HeaderText = "Отчество";
                textColumn.Width = 100;
                textColumn.MinimumWidth = 70;
                textColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            dg.Columns.Add(textColumn);
        }

        private void bnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void UsersForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Boolean result = false;
            DataTable dt = (DataTable) dgUsers.DataSource;
            foreach (DataRow row in dt.Rows)
            {
                if (row.RowState == DataRowState.Deleted)
                    continue;
                int rol_id = Convert.ToInt32(row["user_rol_id"]);
                //Если остается пользователь с правами администратора, то разрешаем изменение таблицы
                if (rol_id == 1)
                {
                    result = true;
                    break;
                }
            }
            if (result)
            {
                newDBcon.UpdateDataTable("users");
            }
            else
            {
                MessageBox.Show("В системе должен быть хотя бы один пользователь с правами администратора", "Ошибка");
            }
        }

        private void dgUsers_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            newDBcon.UpdateDataTable("users");
        }
    }
}