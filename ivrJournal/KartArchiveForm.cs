using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Reflection;
using System.IO;
namespace ivrJournal
{
    public partial class KartArchiveForm : Form
    {
        private MainDBConnect newDBcon;

        public KartArchiveForm()
        {
            InitializeComponent();
            RegistryKey regKey = Registry.CurrentUser;
            regKey = regKey.OpenSubKey("Software\\UFSIN\\ivrJournal");
            newDBcon = new MainDBConnect(regKey.GetValue("dbPath", "").ToString());

            DataGridViewTextBoxColumn textColumn = new DataGridViewTextBoxColumn();
            {
                textColumn.DataPropertyName = "id";
                textColumn.HeaderText = "���";
                textColumn.Width = 30;
                textColumn.MinimumWidth = 50;
                textColumn.Visible = false;
                textColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            }
            dgArchive.Columns.Add(textColumn);

            textColumn = new DataGridViewTextBoxColumn();
            {
                textColumn.DataPropertyName = "last_name";
                textColumn.HeaderText = "�������";
                textColumn.Width = 100;
                textColumn.MinimumWidth = 90;
                textColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            dgArchive.Columns.Add(textColumn);

            textColumn = new DataGridViewTextBoxColumn();
            {
                textColumn.DataPropertyName = "first_name";
                textColumn.HeaderText = "���";
                textColumn.Width = 100;
                textColumn.MinimumWidth = 90;
                textColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            dgArchive.Columns.Add(textColumn);

            textColumn = new DataGridViewTextBoxColumn();
            {
                textColumn.DataPropertyName = "patronymic";
                textColumn.HeaderText = "��������";
                textColumn.Width = 100;
                textColumn.MinimumWidth = 90;
                textColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            dgArchive.Columns.Add(textColumn);

            textColumn = new DataGridViewTextBoxColumn();
            {
                textColumn.DataPropertyName = "birthdate";
                textColumn.HeaderText = "���� ��������";
                textColumn.Width = 80;
                textColumn.MinimumWidth = 80;
                textColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            }
            dgArchive.Columns.Add(textColumn);

            DataView IsPresentView = new DataView(newDBcon.GetDataTable("spec"));
            IsPresentView.RowFilter = "is_present = false";
            dgArchive.AutoGenerateColumns = false;

            dgArchive.DataSource = IsPresentView;

            labelHelp.Text = "��������� �������: " + dgArchive.RowCount;

            toolTip1.SetToolTip(bnUnDelete, "������������ �� ������");
            toolTip1.SetToolTip(bnClose, "������� �����");

            SetRights();
        }

        private void SetRights()
        {
            FormControlManager fcm = new FormControlManager();
            fcm.SetFormControlStatus(this);
        }

        public void RefreshDG()
        {
            newDBcon.RefreshDataTable();
            DataView IsPresentView = new DataView(newDBcon.GetDataTable("spec"));
            IsPresentView.RowFilter = "is_present = false";
            dgArchive.DataSource = IsPresentView;
            labelHelp.Text = "��������� �������: " + dgArchive.RowCount;
        }

        private void EditFind()
        {
            DataView IsPresentView = new DataView(newDBcon.GetDataTable("spec"));
            if (tbLastName.Text != String.Empty)
                IsPresentView.RowFilter = "(is_present = false) AND (last_name LIKE '%" + tbLastName.Text + "%')";
            else
                IsPresentView.RowFilter = "(is_present = false)";
            dgArchive.AutoGenerateColumns = false;
            dgArchive.DataSource = IsPresentView;
            labelHelp.Text = "��������� �������: " + dgArchive.RowCount;
        }


        private void bnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void bnUnDelete_Click(object sender, EventArgs e)
        {
            if (((DataView)dgArchive.DataSource).Count == 0)
                return;

            DialogResult result =
                MessageBox.Show("�� ������������� ������ ������������ ������ �� ������?", "���������", MessageBoxButtons.YesNo);
            if (result == DialogResult.No)
                return;
            int RowIndex = 0;
            RowIndex = dgArchive.CurrentRow.Index;
            DataRow row = ((DataView)dgArchive.DataSource)[RowIndex].Row;

            newDBcon.MarkAsPresent(row);

            labelHelp.Text = "��������� �������: " + dgArchive.RowCount;

            Form[] MDIf = this.MdiParent.MdiChildren;
            foreach (Form f in MDIf)
            {
                if (f.Name == "KartForm")//���� ����� ������� ����� ��� ��������
                {
                    ((KartForm)f).RefreshDG();
                    f.Refresh();//��������� ����� (���� �� ��������)
                    break;
                }
            }
        }

        private void bnReports_Click(object sender, EventArgs e)
        {
            Point CurrentLocation = (sender as Control).PointToScreen(new Point(bnReports.Width, bnReports.Height));
            bnReports.ContextMenuStrip.Show(CurrentLocation);
        }

        private void ����������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (((DataView)dgArchive.DataSource).Count == 0)
                return;

            int RowIndex = 0;
            RowIndex = dgArchive.CurrentRow.Index;

            DataRow row = ((DataView)dgArchive.DataSource)[RowIndex].Row;

            try
            {
                RepDIVRForm repDIVRForm = new RepDIVRForm(row);
                repDIVRForm.MdiParent = this.MdiParent;
                repDIVRForm.Show();
            }
            catch
            {
                MessageBox.Show("��� ������������ ������ ��������� ������, ���������� ������������ ����� ��� ���.", "������");
            }

        }

        private void �����ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (((DataView)dgArchive.DataSource).Count == 0)
                return;

            int RowIndex = 0;
            RowIndex = dgArchive.CurrentRow.Index;

            DataRow row = ((DataView)dgArchive.DataSource)[RowIndex].Row;
            try
            {
                RepBonusAndPenaltyForm repBonusAndPenaltyForm = new RepBonusAndPenaltyForm(row);
                repBonusAndPenaltyForm.MdiParent = this.MdiParent;
                repBonusAndPenaltyForm.Show();
            }
            catch
            {
                MessageBox.Show("��� ������������ ������ ��������� ������, ���������� ������������ ����� ��� ���.", "������");
            }

        }

        private void tsmi����������Word_Click(object sender, EventArgs e)
        {
            if (((DataView)dgArchive.DataSource).Count == 0)
                return;

            int RowIndex = 0;
            RowIndex = dgArchive.CurrentRow.Index;
            DataRow row = ((DataView)dgArchive.DataSource)[RowIndex].Row;

            ReportsWordClass rw = new ReportsWordClass();
            rw.IVRJournal(row);

        }

        private void tsmi������������������������������Word_Click(object sender, EventArgs e)
        {
            if (((DataView)dgArchive.DataSource).Count == 0)
                return;

            int RowIndex = 0;
            RowIndex = dgArchive.CurrentRow.Index;
            DataRow row = ((DataView)dgArchive.DataSource)[RowIndex].Row;

            ReportsWordClass rw = new ReportsWordClass();
            rw.BonusAndPenalty(row);
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

        private void tbLastName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                EditFind();
            }
        }

        private void bnDel_Click(object sender, EventArgs e)
        {
            if (((DataView)dgArchive.DataSource).Count == 0)
                return;

            if (!IVRShared.IsCurrentUserAdmin())
            {
                MessageBox.Show("������� ������ ����� ������ ������������ � ������� ��������������!");
                return;
            }

            DialogResult result =
                MessageBox.Show("�� ������������� ������ ������� ������ �� ������? ���������� �������������� ����� ����������!", "���������", MessageBoxButtons.YesNo);
            if (result == DialogResult.No)
                return;
            int RowIndex = 0;
            RowIndex = dgArchive.CurrentRow.Index;
            DataRow row = ((DataView)dgArchive.DataSource)[RowIndex].Row;

            SQLDBConnectLite sqlCon = new SQLDBConnectLite();
            sqlCon.DoQuery("DELETE FROM spec_psycho WHERE (id_spec=" + row["id"].ToString() + ")");

            sqlCon.DoQuery("DELETE FROM relations WHERE (id_spec=" + row["id"].ToString() + ")");
            sqlCon.DoQuery("DELETE FROM prev_conv WHERE (id_spec=" + row["id"].ToString() + ")");
            sqlCon.DoQuery("DELETE FROM party WHERE (id_spec=" + row["id"].ToString() + ")");
            sqlCon.DoQuery("DELETE FROM profilact_ychet WHERE (id_spec=" + row["id"].ToString() + ")");
            sqlCon.DoQuery("DELETE FROM bonus WHERE (id_spec=" + row["id"].ToString() + ")");
            sqlCon.DoQuery("DELETE FROM penalty WHERE (id_spec=" + row["id"].ToString() + ")");
            sqlCon.DoQuery("DELETE FROM ivr WHERE (id_spec=" + row["id"].ToString() + ")");
            sqlCon.DoQuery("DELETE FROM psycho_char WHERE (id_spec=" + row["id"].ToString() + ")");

            sqlCon.DoQuery("DELETE FROM resolution WHERE (id_spec=" + row["id"].ToString() + ")");
            sqlCon.DoQuery("DELETE FROM results WHERE (id_spec=" + row["id"].ToString() + ")");

            sqlCon.DoQuery("DELETE FROM spec WHERE (id=" + row["id"].ToString() + ")");

            RefreshDG();

        }
    }
}