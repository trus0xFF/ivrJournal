using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Diagnostics;
using System.Xml;
using System.IO;

namespace ivrJournal
{
    public partial class ChooseDepartmentForm : Form
    {
        private bool tabControlChanging;
        private string region;
        private int regionIndex = 0;
        private int departmentIndex = 0;
        private string departmentID;

        SQLDBConnect sqlCon;

        public ChooseDepartmentForm()
        {
            InitializeComponent();

            sqlCon = new SQLDBConnect();

            bnBack.Enabled = false;

            dgRegion.Columns.Add(new DataGridViewTextBoxColumn());
            dgRegion.Columns[0].DataPropertyName = "id";
            dgRegion.Columns[0].Name = "id";
            dgRegion.Columns[0].HeaderText = "Код ОКПО";
            dgRegion.Columns[0].Width = 100;

            dgRegion.Columns.Add(new DataGridViewTextBoxColumn());
            dgRegion.Columns[1].DataPropertyName = "name";
            dgRegion.Columns[1].HeaderText = "Наименование региона";
            dgRegion.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgRegion.AutoGenerateColumns = false;

            dgDepartment.Columns.Add(new DataGridViewTextBoxColumn());
            dgDepartment.Columns[0].DataPropertyName = "id";
            dgDepartment.Columns[0].Name = "id";
            dgDepartment.Columns[0].HeaderText = "Код ОКПО";
            dgDepartment.Columns[0].Width = 100;

            dgDepartment.Columns.Add(new DataGridViewTextBoxColumn());
            dgDepartment.Columns[1].DataPropertyName = "name";
            dgDepartment.Columns[1].HeaderText = "Наименование подразделения";
            dgDepartment.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgDepartment.AutoGenerateColumns = false;

            /*
            RegistryKey regKey = Registry.CurrentUser;
            regKey = regKey.OpenSubKey("Software\\UFSIN\\ivrJournal");
            String dbPath = regKey.GetValue("dbPath", @"c:\Program files\ufsin_rk\Дневник ИВР\divr.mdb").ToString();
            */

            departmentID = sqlCon.GetSystemValue("Department");

            dgRegion.DataSource = sqlCon.GetDataTable("region", "SELECT id, name FROM department WHERE higher='0'");

            this.tabControl1.Selected += new TabControlEventHandler(tabControl1_Selected);

        }

        private void bnBack_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage2)
            {
                tabControlChanging = true;
                tabControl1.SelectedTab = tabPage1;
                departmentIndex = dgDepartment.CurrentRow.Index;
            }
        }

        private void bnNext_Click(object sender, EventArgs e)
        {
            tabControlChanging = true;
            if (tabControl1.SelectedTab == tabPage1)
            {
                if (((DataTable)dgRegion.DataSource).Rows.Count == 0)
                    return;

                int RowIndex = 0;
                RowIndex = dgRegion.CurrentRow.Index;
                regionIndex = RowIndex;
                region = dgRegion.Rows[RowIndex].Cells["id"].Value.ToString();

                tabControl1.SelectedTab = tabPage2;
            }
            else if (tabControl1.SelectedTab == tabPage2)
            {
                if (((DataTable)dgDepartment.DataSource).Rows.Count == 0)
                    return;

                int RowIndex = 0;
                RowIndex = dgDepartment.CurrentRow.Index;

                string department = dgDepartment.Rows[RowIndex].Cells["id"].Value.ToString();

                String stringSQL = "";

                 try
                {
                    stringSQL = (departmentID != null) ? "UPDATE system SET system_value = '" + department + "' WHERE (name = 'Department')" :
                        "INSERT INTO system (name, system_value) values ('Department', '" + department + "')";
                    sqlCon.DoQuery( stringSQL );

                    stringSQL = (departmentID != null) ? "UPDATE employee SET department_id = '" + department + "' WHERE (department_id = '" + departmentID + "')" :
                        "UPDATE employee SET department_id = '" + department + "'";
                    sqlCon.DoQuery(stringSQL);

                    stringSQL = (departmentID != null) ? "UPDATE spr_party_number SET department_id = '" + department + "' WHERE (department_id = '" + departmentID + "')" :
                        "UPDATE spr_party_number SET department_id = '" + department + "'";
                    sqlCon.DoQuery(stringSQL);
                }
                catch (Exception er)
                {
                    MessageBox.Show(er.Message, "Ошибка при обновлении справочников");
                }

                Close();
            }

        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (!tabControlChanging)
                e.Cancel = true;
            else
                if (region == null)
                    e.Cancel = true;
                else
                    tabControlChanging = false;
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            switch (e.TabPageIndex)
            {
                case 0:
                    bnBack.Enabled = false;
                    bnNext.Text = "Далее >>";

                    break;
                case 1:
                    bnBack.Enabled = true;
                    bnNext.Text = "Готово";

                    dgDepartment.DataSource = sqlCon.GetDataTable("department", "SELECT id, name FROM department WHERE (higher='" + region + "')");
                    dgDepartment.Rows[departmentIndex].Selected = true;

                    break;
            }
        }

        private void ChooseDepartmentForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!tabControlChanging)
                e.Cancel = true;
        }

        private void dgRegion_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dgRegion.CurrentRow == null)
                return;

            int RowIndex = 0;
            RowIndex = dgRegion.CurrentRow.Index;
            if (regionIndex != RowIndex)
                departmentIndex = 0;
        }

        private void ChooseDepartmentForm_Load(object sender, EventArgs e)
        {
        }

        private void bnCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы не выбрали учреждение, что может привести к некорректной работе программы. Вы действительно хотите выйти?", "Внимание!",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                == DialogResult.Yes)
            {
                tabControlChanging = true;
                Close();
            }
        }

        private void bnLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Xml Files (*.xml)|*.xml|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) != DialogResult.OK)
                return;
            //Удаляем связи между таблицами
            sqlCon.DoQuery("ALTER TABLE employee DROP CONSTRAINT fk_dep_id");
            sqlCon.DoQuery("ALTER TABLE spr_party_number DROP CONSTRAINT fk_dep_id2");
            //Удаляем содержимое справочников
            sqlCon.DoQuery("DELETE * FROM department");
            sqlCon.DoQuery("DELETE * FROM enum_department_type");
            sqlCon.GetDataTable("enum_department_type", "SELECT * FROM enum_department_type");
            sqlCon.GetDataTable("department", "SELECT * FROM department");
            DataTable ddt = sqlCon.GetDataTable("department", "SELECT * FROM department");
            //Загружаем данные из нового справочника
            FileInfo f = new FileInfo(openFileDialog.FileName);

            XmlDataDocument xml = new XmlDataDocument();
            xml.DataSet.ReadXmlSchema(Path.ChangeExtension(f.FullName, ".xsd"));
            xml.Load(f.FullName);

            sqlCon.MergeDataSet(xml.DataSet);

            sqlCon.UpdateDataTable("enum_department_type");
            sqlCon.UpdateDataTable("department");
            //Восстанавливаем связи между таблицами
            sqlCon.DoQuery("ALTER TABLE employee ADD CONSTRAINT fk_dep_id  FOREIGN KEY (department_id) REFERENCES department (id)");
            sqlCon.DoQuery("ALTER TABLE spr_party_number ADD CONSTRAINT fk_dep_id2  FOREIGN KEY (department_id) REFERENCES department (id)");

            dgRegion.DataSource = sqlCon.GetDataTable("region", "SELECT id, name FROM department WHERE higher='0'"); ;
        }

        private void dgRegion_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (dgRegion.CurrentRow == null)
                return;

            int RowIndex = 0;
            RowIndex = dgRegion.CurrentRow.Index;
            if (regionIndex != RowIndex)
                departmentIndex = 0;
        }

    }
}