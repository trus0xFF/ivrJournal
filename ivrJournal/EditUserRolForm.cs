using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ivrJournal
{
    public partial class EditUserRolForm : Form
    {
        private SprDbConnect newDBcon;
        private SQLDBConnect sqlCon;
        private Boolean notSaved;

        public EditUserRolForm()
        {
            InitializeComponent();

            notSaved = false;

            sqlCon = new SQLDBConnect();

            newDBcon = new SprDbConnect("spr_user_rol");
            dgUserRol.DataSource = newDBcon.GetDataTable("spr_user_rol");
            dgUserRol.Columns[0].HeaderText = "Код";
            dgUserRol.Columns[0].Visible = false;
            dgUserRol.Columns[0].DataPropertyName = "id";
            dgUserRol.Columns[1].HeaderText = "Наименование";

            dgControls.AutoGenerateColumns = false;

            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
            /*
            column.HeaderText = "Код";
            column.Visible = false;
            column.DataPropertyName = "id";
            column.Name = "id";
            dgControls.Columns.Add(column);
            */

            column = new DataGridViewTextBoxColumn();
            column.HeaderText = "Наименование";
            column.Visible = true;
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            column.DataPropertyName = "opisanie";
            dgControls.Columns.Add(column);

            DataGridViewCheckBoxColumn columnCheck =
                new DataGridViewCheckBoxColumn();
            {
                columnCheck.Name = "enabled";
                columnCheck.DataPropertyName = "enabled";
                columnCheck.HeaderText = "Разрешено";
                columnCheck.Width = 80;
                columnCheck.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            }
            dgControls.Columns.Add(columnCheck);

            //Заполняем таблицу user_rol_access на случай появления новых контролов или групп пользователей
            sqlCon.DoQuery(@"INSERT INTO user_rol_access
                SELECT spr_user_controls.id AS id_user_controls, spr_user_rol.id AS id_user_rol 
                FROM spr_user_controls, spr_user_rol
                WHERE spr_user_controls.id NOT IN (SELECT DISTINCT id_user_controls FROM user_rol_access) OR 
                spr_user_rol.id NOT IN (SELECT DISTINCT id_user_rol FROM user_rol_access)");

            DataTable dt = sqlCon.GetDataTable("user_access",
                @"SELECT user_rol_access.id, user_rol_access.id_user_rol, user_rol_access.enabled, spr_user_controls.opisanie FROM user_rol_access 
                LEFT JOIN spr_user_controls ON (user_rol_access.id_user_controls = spr_user_controls.id)");
            DataView dv = new DataView(dt);

            dgControls.DataSource = dv;
        }

        private void bnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SaveUserRol()
        {
            DataTable dt2update = sqlCon.GetDataTable("user_rol_access", "SELECT * FROM user_rol_access");
            DataTable dt = ((DataView)dgControls.DataSource).Table;
            
            foreach (DataRow rowSrc in dt.Rows)
            {
                foreach (DataRow rowDst in dt2update.Rows)
                {
                    if (rowSrc["id"].ToString() == rowDst["id"].ToString())
                        rowDst["enabled"] = rowSrc["enabled"];
                }
            }

            sqlCon.UpdateDataTable("user_rol_access");
        }

        private void bnSave_Click(object sender, EventArgs e)
        {
            SaveUserRol();
            notSaved = false;
        }

        private void dgUserRol_RowEnter(object sender, DataGridViewCellEventArgs e)
        {

            DataView dv = (DataView)dgControls.DataSource;
            dv.RowFilter = "id_user_rol=" + dgUserRol.Rows[e.RowIndex].Cells[0].Value.ToString();

        }

        private void dgUserRol_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            notSaved = false;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgControls.Rows)
            {
                row.Cells["enabled"].Value = cbAll.Checked;
            }
            if (!notSaved) notSaved = true;
        }

        private void dgControls_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.ColumnIndex == -1) | (e.RowIndex == -1))
                return;
            if (!Convert.IsDBNull(dgControls.Rows[e.RowIndex].Cells["enabled"].Value))
            {
                dgControls.Rows[e.RowIndex].Cells["enabled"].Value =
                    ! Convert.ToBoolean( dgControls.Rows[e.RowIndex].Cells["enabled"].Value );
            }
            else
                dgControls.Rows[e.RowIndex].Cells["enabled"].Value = true;

            if (!notSaved) notSaved = true;
        }

        private void dgControls_Leave(object sender, EventArgs e)
        {
            /*
            if (notSaved)
            {
                if (MessageBox.Show("Данные были изменены. Сохранить изменения?", "Внимание!",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.Yes)
                {
                    SaveUserRol();
                }
            }
            notSaved = false;
             */
        }
    }
}