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
    public partial class EmployeeForm : Form
    {
        private SprDbConnect newDBcon;
        //private string departmentID;

        public EmployeeForm()
        {
            InitializeComponent();
            newDBcon = new SprDbConnect("employee");
            LoadGrid(dgEmployee);
            dgEmployee.AutoGenerateColumns = false;

            dgEmployee.DataSource = newDBcon.GetDataTable("employee");

            RegistryKey regKey = Registry.CurrentUser;
            regKey = regKey.OpenSubKey("Software\\UFSIN\\ivrJournal");
            String dbPath = regKey.GetValue("dbPath", @"c:\Program files\ufsin_rk\Дневник ИВР\divr.mdb").ToString();

            //this.departmentID = ((MainForm)this.MdiParent).departmentID;

        }

        private void LoadGrid(DataGridView dg)
        {
            DataGridViewTextBoxColumn textColumn = new DataGridViewTextBoxColumn();
            {
                textColumn.DataPropertyName = "id";
                textColumn.Visible = false;
            }
            dg.Columns.Add(textColumn);

            SprDbConnect sprDBcon = new SprDbConnect("department");

            DataGridViewComboBoxColumn column = new DataGridViewComboBoxColumn();
            {
                column.DataPropertyName = "department_id";
                column.HeaderText = "Учреждение";
                column.DropDownWidth = 100;
                column.Width = 90;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.MaxDropDownItems = 10;
                column.FlatStyle = FlatStyle.Flat;

                column.DataSource = sprDBcon.GetDataTable("department");
                column.ValueMember = "id";
                column.DisplayMember = "name";
                column.ReadOnly = true;
            }
            dg.Columns.Add(column);
            
//            textColumn = new DataGridViewTextBoxColumn();
//            {
//                textColumn.DataPropertyName = "department_id";
//                textColumn.Visible = false;
//            }
//            dg.Columns.Add(textColumn);

            textColumn = new DataGridViewTextBoxColumn();
            {
                textColumn.DataPropertyName = "last_name";
                textColumn.HeaderText = "Фамилия";
                textColumn.Width = 80;
                textColumn.MinimumWidth = 70;
                textColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            }
            dg.Columns.Add(textColumn);

            textColumn = new DataGridViewTextBoxColumn();
            {
                textColumn.DataPropertyName = "first_name";
                textColumn.HeaderText = "Имя";
                textColumn.Width = 80;
                textColumn.MinimumWidth = 70;
                textColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            }
            dg.Columns.Add(textColumn);

            textColumn = new DataGridViewTextBoxColumn();
            {
                textColumn.DataPropertyName = "patronymic";
                textColumn.HeaderText = "Отчество";
                textColumn.Width = 80;
                textColumn.MinimumWidth = 70;
                textColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            }
            dg.Columns.Add(textColumn);

            textColumn = new DataGridViewTextBoxColumn();
            {
                textColumn.DataPropertyName = "rank";
                textColumn.HeaderText = "Звание";
                textColumn.Width = 80;
                textColumn.MinimumWidth = 70;
                textColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            }
            dg.Columns.Add(textColumn);

            textColumn = new DataGridViewTextBoxColumn();
            {
                textColumn.DataPropertyName = "post";
                textColumn.HeaderText = "Должность";
                textColumn.Width = 80;
                textColumn.MinimumWidth = 70;
                textColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            dg.Columns.Add(textColumn);
       }

        private void dgEmployee_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            newDBcon.UpdateDataTable("employee");
        }

        private void EmployeeForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            newDBcon.UpdateDataTable("employee");
        }

        private void dgEmployee_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            SQLDBConnect sqlCon = new SQLDBConnect();
            dgEmployee.CurrentRow.Cells[1].Value = sqlCon.GetSystemValue("Department");
        }

        private void bnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dgEmployee_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (!IVRShared.IsCurrentUserAdmin())
            {
                MessageBox.Show("Удалять записи может только Пользователь с правами Администратора!");
                e.Cancel = true;
            }
            else
                newDBcon.UpdateDataTable("employee");

        }

        private void tsbNew_Click(object sender, EventArgs e)
        {
            dgEmployee.CurrentCell = dgEmployee[1, dgEmployee.NewRowIndex];
            dgEmployee.Focus();
            dgEmployee.BeginEdit(true);
        }

        private void tsbEdit_Click(object sender, EventArgs e)
        {
            int index = dgEmployee.CurrentRow.Index;
            if (index != -1)
            {
                dgEmployee.BeginEdit(true);
            }
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {
            if (!IVRShared.IsCurrentUserAdmin())
            {
                MessageBox.Show("Удалять записи может только Пользователь с правами Администратора!");
                return;
            }

            int index = dgEmployee.CurrentRow.Index;
            if ((index != -1) & (index != dgEmployee.NewRowIndex))
            {
                try
                {
                    dgEmployee.Rows.RemoveAt(index);
                    newDBcon.UpdateDataTable("employee");
                }
                catch
                {
                    MessageBox.Show("Запись в базе не обнаружена!!!", "Сообщение о базе", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
        }

        private void tsbRefresh_Click(object sender, EventArgs e)
        {
            newDBcon.RefreshDataTable("employee");
        }

    }
}