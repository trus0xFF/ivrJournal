using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ivrJournal
{
    public partial class SprForm : Form
    {
        private SprDbConnect newDBcon;
        private String nameSpr;
        //private string departmentID;

        private const int CP_NOCLOSE_BUTTON = 0x200;
/*
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON ;
                   return myCp;
            }
        }
*/
        public SprForm(String nameSpr, String TextSpr)
        {
            InitializeComponent();

            //this.departmentID = ((MainForm)this.MdiParent).departmentID;
            this.nameSpr = nameSpr;
            newDBcon = new SprDbConnect(nameSpr);
            this.Text = TextSpr;

            dgSpr.Columns.Add(new DataGridViewTextBoxColumn());
            dgSpr.Columns[0].HeaderText = "Код";
            dgSpr.Columns[0].Visible = false;
            dgSpr.Columns[0].DataPropertyName = "id";
            dgSpr.Columns.Add(new DataGridViewTextBoxColumn());
            dgSpr.Columns[1].HeaderText = "Наименование";
            dgSpr.Columns[1].DataPropertyName = "name";

            if (nameSpr == "spr_party_number")
            {
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
                dgSpr.Columns.Add(column);
            }

            dgSpr.DataSource = newDBcon.GetDataTable(nameSpr);
            dgSpr.AutoGenerateColumns = false;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgSpr_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if ( dgSpr[e.ColumnIndex,e.RowIndex].Value == null)
                return;
            if (dgSpr[e.ColumnIndex,e.RowIndex].Value.ToString() == String.Empty)
            {
                MessageBox.Show("Наименование не может содержать пустое значение!", "Сообщение");
                newDBcon.RefreshDataTable(nameSpr);
                return;
            }
            newDBcon.UpdateDataTable(nameSpr);
        }


        private void SprForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            newDBcon.UpdateDataTable(nameSpr);
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {
            if (!IVRShared.IsCurrentUserAdmin())
            {
                MessageBox.Show("Удалять записи может только Пользователь с правами Администратора!");
                return;
            }

            int index = dgSpr.CurrentRow.Index;
            if ((index != -1) & (index != dgSpr.NewRowIndex))
            {
                try
                {
                    dgSpr.Rows.RemoveAt(index);
                    newDBcon.UpdateDataTable(nameSpr);
                }
                catch
                {
                    MessageBox.Show("Запись в базе не обнаружена!!!", "Сообщение о базе", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
        }

        private void SprForm_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            Help.ShowHelp(this, "help_ivr.chm", HelpNavigator.Topic, "help_ivr_4.htm");
        }

        private void tsbRefresh_Click(object sender, EventArgs e)
        {
            newDBcon.RefreshDataTable(nameSpr);
        }

        private void tsbEdit_Click(object sender, EventArgs e)
        {
            int index = dgSpr.CurrentRow.Index;
            if (index != -1)
            {
                dgSpr.BeginEdit(true);
            }
        }

        private void tsbNew_Click(object sender, EventArgs e)
        {
            dgSpr.CurrentCell = dgSpr[1, dgSpr.NewRowIndex];
            dgSpr.Focus();
            dgSpr.BeginEdit(true);
        }

        private void dgSpr_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            if (nameSpr == "spr_party_number")
            {
                SQLDBConnect sqlCon = new SQLDBConnect();
                dgSpr.CurrentRow.Cells[2].Value = sqlCon.GetSystemValue("Department");
            }

        }

        private void dgSpr_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (!IVRShared.IsCurrentUserAdmin())
            {
                MessageBox.Show("Удалять записи может только Пользователь с правами Администратора!");
                e.Cancel = true;
            }
            else
                newDBcon.UpdateDataTable(nameSpr);

        }

    }
}