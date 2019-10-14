using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ivrJournal
{
    public partial class IVRTransferForm : Form
    {
        SQLDBConnect sqlCon;
//        DataTable dt_buf;

        public IVRTransferForm()
        {
            InitializeComponent();

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

            sqlCon = new SQLDBConnect();
            DataTable dt_buf = sqlCon.GetDataTable("spec", "SELECT * FROM spec WHERE is_present = true");
            if (dt_buf == null)
                return;

            DataColumn newColumn5 = new DataColumn();
            newColumn5.ColumnName = "selected";
            newColumn5.DataType = System.Type.GetType("System.Boolean");
            dt_buf.Columns.Add(newColumn5);

            foreach (DataRow row in dt_buf.Rows)
            {
                row["selected"] = false;
            }

            dgIVRList.AutoGenerateColumns = false;
            dgIVRList.DataSource = dt_buf;

            String dep = sqlCon.GetSystemValue("Department");
            DataTable dt_party = sqlCon.GetDataTable("spr_party_number", @"SELECT * FROM spr_party_number WHERE department_id='" + dep + @"'");
            cbParty.DataSource = dt_party;
            cbParty.ValueMember = "id";
            cbParty.DisplayMember = "name";
            cbParty.SelectedIndex = -1;

        }

        private void bnLoad_Click(object sender, EventArgs e)
        {
            DataTable dt_buf = sqlCon.GetDataTable("spec");
            String str = null;
            foreach (DataRow row in dt_buf.Rows)
            {
                if ( Convert.IsDBNull( row["selected"]) )
                    continue;
                if (Convert.ToBoolean(row["selected"]) == true)
                    str = (str==null) ? str + Convert.ToInt32(row["id"]) : str + ", " + Convert.ToInt32(row["id"]);
            }
            if (str == null)
            {
                MessageBox.Show("Выберите осужденных, которых вы хотите выгрузить");
                return;
            }
            //MessageBox.Show(str);
            IVRTransfer ivr = new IVRTransfer();
            ivr.UploadData(str, cbMoveToArh.Checked);
        }

        private void bnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cbAll_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = sqlCon.GetDataTable("spec");

            DataView dv = (DataView)dgIVRList.DataSource;
            String filter = dv.RowFilter;
            foreach(DataRow row in dt.Select(filter))
                row["selected"] = cbAll.Checked;

        }

        private void dgIVRList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.ColumnIndex != -1) & (e.RowIndex != -1))
            {
                if (dgIVRList.Columns[e.ColumnIndex].Name == "selected")
                {
                    if (!Convert.IsDBNull(dgIVRList.Rows[e.RowIndex].Cells["selected"].Value))
                    {
                        if ((bool)dgIVRList.Rows[e.RowIndex].Cells["selected"].Value)
                            dgIVRList.Rows[e.RowIndex].Cells["selected"].Value = false;
                        else
                            dgIVRList.Rows[e.RowIndex].Cells["selected"].Value = true;
                    }
                    else
                        dgIVRList.Rows[e.RowIndex].Cells["selected"].Value = true;
                }
            }
        }

        private void tbLastName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                EditFind();
            }
        }

        private void EditFind()
        {
            DataView IsPresentView = new DataView(sqlCon.GetDataTable("spec"));
            if (cbParty.SelectedIndex > -1)
            {
                if (tbLastName.Text != String.Empty)
                    IsPresentView.RowFilter = "(is_present = true) AND (last_name LIKE '%" + tbLastName.Text + "%') AND (party_id = " + cbParty.SelectedValue + ")";
                else
                    IsPresentView.RowFilter = "(is_present = true) AND (party_id = " + cbParty.SelectedValue + ")";
            }
            else
            {
                if (tbLastName.Text != String.Empty)
                    IsPresentView.RowFilter = "(is_present = true) AND (last_name LIKE '%" + tbLastName.Text + "%')";
                else
                    IsPresentView.RowFilter = "is_present = true";
            }
            dgIVRList.AutoGenerateColumns = false;
            dgIVRList.DataSource = IsPresentView;
        }

        private void bnFind_Click(object sender, EventArgs e)
        {
            EditFind();
        }

        private void bnFindDel_Click(object sender, EventArgs e)
        {
            tbLastName.Text = "";
            EditFind();
        }

        private void cbParty_SelectedIndexChanged(object sender, EventArgs e)
        {
            EditFind();
        }

        private void bnPartyDel_Click(object sender, EventArgs e)
        {
            cbParty.SelectedIndex = -1;
            DataView IsPresentView = new DataView(sqlCon.GetDataTable("spec"));
            if (tbLastName.Text != String.Empty)
                IsPresentView.RowFilter = "(is_present = true) AND (last_name LIKE '%" + tbLastName.Text + "%')";
            else
                IsPresentView.RowFilter = "is_present = true";

            dgIVRList.AutoGenerateColumns = false;
            dgIVRList.DataSource = IsPresentView;
            //labelHelp.Text = "Загружено записей: " + dgListPerson.RowCount;
        }


    }
}