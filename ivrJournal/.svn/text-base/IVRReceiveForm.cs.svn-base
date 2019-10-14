using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ivrJournal
{
    public enum FormMode
    {
        full,
        lite
    }

    public partial class IVRReceiveForm : Form
    {
        IVRTransfer ivr;
        FormMode fm;

        public IVRReceiveForm(FormMode fm)
        {
            InitializeComponent();
            
            this.fm = fm;
            
            DataGridViewTextBoxColumn textColumn = new DataGridViewTextBoxColumn();
            {
                textColumn.DataPropertyName = "id";
                textColumn.HeaderText = "код";
                textColumn.Width = 30;
                textColumn.MinimumWidth = 50;
                textColumn.Visible = false;
                textColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            }
            dgIVRList.Columns.Add(textColumn);

            textColumn = new DataGridViewTextBoxColumn();
            {
                textColumn.DataPropertyName = "last_name";
                textColumn.HeaderText = "Фамилия";
                textColumn.Width = 100;
                textColumn.MinimumWidth = 90;
                textColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            dgIVRList.Columns.Add(textColumn);

            textColumn = new DataGridViewTextBoxColumn();
            {
                textColumn.DataPropertyName = "first_name";
                textColumn.HeaderText = "Имя";
                textColumn.Width = 100;
                textColumn.MinimumWidth = 90;
                textColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            dgIVRList.Columns.Add(textColumn);

            textColumn = new DataGridViewTextBoxColumn();
            {
                textColumn.DataPropertyName = "patronymic";
                textColumn.HeaderText = "Отчество";
                textColumn.Width = 100;
                textColumn.MinimumWidth = 90;
                textColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            dgIVRList.Columns.Add(textColumn);

            textColumn = new DataGridViewTextBoxColumn();
            {
                textColumn.DataPropertyName = "birthdate";
                textColumn.HeaderText = "Дата рождения";
                textColumn.Width = 80;
                textColumn.MinimumWidth = 80;
                textColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            }
            dgIVRList.Columns.Add(textColumn);

            DataGridViewCheckBoxColumn columnCheck =
                new DataGridViewCheckBoxColumn();
            {
                columnCheck.Name = "selected";
                columnCheck.DataPropertyName = "selected";
                columnCheck.HeaderText = "Метка";
                columnCheck.Width = 50;
                columnCheck.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            }
            dgIVRList.Columns.Add(columnCheck);

            dgIVRList.AutoGenerateColumns = false;

            bnLoad.Enabled = false;
            
            ivr = new IVRTransfer();
        }

        private void bnOpen_Click(object sender, EventArgs e)
        {
            String dep = null;
            DataTable dt = null;
            //ivr = new IVRTransfer();
            Boolean status = ivr.PrepareData(out dt, out  dep);
            if (!status)
            {
                MessageBox.Show("Файл загрузки поврежден или имеет неправильный формат", "Ошибка");
                return;
            }
            DataTable dt_buf = dt.Copy();
            
            DataColumn newColumn5 = new DataColumn();
            newColumn5.ColumnName = "selected";
            newColumn5.DataType = System.Type.GetType("System.Boolean");
            dt_buf.Columns.Add(newColumn5);

            foreach (DataRow row in dt_buf.Rows)
            {
                row["selected"] = false;
            }

            dgIVRList.DataSource = dt_buf;

            lblDep.Text = dep;

            if (dgIVRList.RowCount > -1)
            {
                bnLoad.Enabled = true;
            }
        }

        private void bnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void bnLoad_Click(object sender, EventArgs e)
        {
            bnLoad.Enabled = false;
            bnLoad.Refresh();
            Boolean status = true;
            if (fm == FormMode.lite)
            {
                status = ivr.DownloadDataLocal((DataTable)dgIVRList.DataSource);
            }
            else
            {
                status = ivr.DownloadData((DataTable)dgIVRList.DataSource);
            }

            if (!status)
            {
                MessageBox.Show("Загрузка отменена", "Сообщение");
                return;
            }
            MessageBox.Show("Процедура загрузки завершена", "Сообщение");
            return;
        }

        private void dgIVRList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.ColumnIndex == -1) | (e.RowIndex == -1))
                return;

            if (!Convert.IsDBNull(dgIVRList.Rows[e.RowIndex].Cells["selected"].Value))
            {
                dgIVRList.Rows[e.RowIndex].Cells["selected"].Value =
                    !Convert.ToBoolean( dgIVRList.Rows[e.RowIndex].Cells["selected"].Value);
            }
            else
                dgIVRList.Rows[e.RowIndex].Cells["selected"].Value = true;
        }


    }
}