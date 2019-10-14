using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ivrJournal
{
    public partial class GetDictSprForm : Form
    {
        private Dictionary<int, int> dictSprLocal;
        public GetDictSprForm(DataTable dtSrc, DataTable dtDst, Dictionary<int, int> dictSpr)
        {
            InitializeComponent();
            dictSprLocal = dictSpr;

            DataTable dt_buf = dtSrc.Copy();
            //RowState - Added, поэтому после Delete строки не помечаются на удаление а удаляются!!!
            dt_buf.AcceptChanges();

            DataRow bufRow = null;
            int key = 0;

            for (int idx = 0; idx < dt_buf.Rows.Count; idx++ )
            {
                bufRow = dt_buf.Rows[idx];
                if (Convert.IsDBNull(bufRow["id"]))
                    continue;
                key = Convert.ToInt32(bufRow["id"]);
                if (dictSpr.ContainsKey(key))
                    dt_buf.Rows[idx].Delete();
            }
            dt_buf.AcceptChanges();

            DataGridViewTextBoxColumn textColumn = new DataGridViewTextBoxColumn();
            {
                textColumn.DataPropertyName = "id";
                textColumn.Name = "id";
                textColumn.HeaderText = "код";
                textColumn.Visible = false;
            }
            dgGetDict.Columns.Add(textColumn);

            textColumn = new DataGridViewTextBoxColumn();
            {
                textColumn.DataPropertyName = "name";
                textColumn.HeaderText = "Наименование";
                textColumn.Width = 150;
                textColumn.MinimumWidth = 150;
                textColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                textColumn.ReadOnly = true;
            }
            dgGetDict.Columns.Add(textColumn);


            DataGridViewComboBoxColumn columnCB =
             new DataGridViewComboBoxColumn();
            {
                columnCB.DataPropertyName = "id_new";
                columnCB.Name = "id_new";
                columnCB.HeaderText = "Соответствие";
                columnCB.DropDownWidth = 200;
                columnCB.Width = 150;
                columnCB.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                columnCB.MaxDropDownItems = 10;
                columnCB.FlatStyle = FlatStyle.Flat;

                columnCB.DataSource = dtDst;
                columnCB.ValueMember = "id";
                columnCB.DisplayMember = "name";
            }
            dgGetDict.Columns.Add(columnCB);

            dgGetDict.AutoGenerateColumns = false;
            dgGetDict.DataSource = dt_buf;

        }

        private void bnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void bnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            foreach (DataGridViewRow row in dgGetDict.Rows)
            {
                dictSprLocal[Convert.ToInt32(row.Cells["id"].Value)] = Convert.ToInt32(row.Cells["id_new"].Value);
            }

            Close();
        }

    }
}