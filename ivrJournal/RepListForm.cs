using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Globalization;
//using Excel;
using Excel = Microsoft.Office.Interop.Excel;

namespace ivrJournal
{
    public partial class RepListForm : Form
    {
        
        SQLDBConnect eurDBcon;
        DataTable dtListField;
        DataTable dtSprCondition;

        public RepListForm()
        {
            InitializeComponent();

            eurDBcon = new SQLDBConnect();
            dtListField = eurDBcon.GetDataTable("list_field", "SELECT * FROM stuctur_base");

            DataGridViewComboBoxColumn column = new DataGridViewComboBoxColumn();
            {
                column.HeaderText = "Поля базы";
                column.DropDownWidth = 250;
                column.Width = 90;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                column.MaxDropDownItems = 10;
                column.FlatStyle = FlatStyle.Flat;

                column.DataSource = dtListField;
                column.ValueMember = "id";
                //column.ValueMember = "spr";
                column.DisplayMember = "field_name_rus";
                column.Name = "fields";
            }
            dgListField.Columns.Add(column);
            /*
                        DataGridViewCheckBoxColumn columnCheck = new DataGridViewCheckBoxColumn();
                        {
                            columnCheck.Name = "sort";
                            //                columnCheck.DataPropertyName = "enabled";
                            columnCheck.HeaderText = "Сортировать";
                            columnCheck.Width = 100;
                            columnCheck.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                        }
                        dgListField.Columns.Add(columnCheck);
            */
            column = new DataGridViewComboBoxColumn();
            {
                column.Name = "field_name_rus";
                column.HeaderText = "Поля базы";
                column.DropDownWidth = 250;
                column.Width = 90;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                column.MaxDropDownItems = 10;
                column.FlatStyle = FlatStyle.Flat;

                column.DataSource = dtListField;
                //column.ValueMember = "spr";
                column.ValueMember = "id";
                column.DisplayMember = "field_name_rus";
                column.Name = "fields";
            }
            dgWhere.Columns.Add(column);

            dtSprCondition = eurDBcon.GetDataTable("enum_condition", "SELECT * FROM enum_condition");

            column = new DataGridViewComboBoxColumn();
            {
                column.HeaderText = "Условие";
                column.DropDownWidth = 150;
                column.Width = 90;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                column.MaxDropDownItems = 10;
                column.FlatStyle = FlatStyle.Flat;

                column.DataSource = dtSprCondition;
                column.ValueMember = "id";
                column.DisplayMember = "name";
                column.Name = "conditions";
            }
            dgWhere.Columns.Add(column);

            DataGridViewTextBoxColumn columnText = new DataGridViewTextBoxColumn();
            {
                columnText.Name = "Value";
                columnText.HeaderText = "Значение";
                columnText.Width = 150;
                columnText.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                columnText.Name = "value";
            }
            dgWhere.Columns.Add(columnText);

        }

        private void bnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private ArrayList GetFieldList()
        {
            String fieldName = null;
            String rusName = null;
            ArrayList fieldList = new ArrayList();

            foreach (DataGridViewRow gridRow in dgListField.Rows)
            {
                if (gridRow.Cells["fields"].Value == null)
                    continue;
                String id = gridRow.Cells["fields"].Value.ToString();
                foreach (DataRow row in dtListField.Select("id = '" + id + "'"))
                {
                    rusName = row["field_name_rus"].ToString().Length > 60 ? 
                        row["field_name_rus"].ToString().Substring(0, 60).Replace(",", "") : row["field_name_rus"].ToString().Replace(",", "");
                    fieldName = row["field_name"].ToString();
                    fieldName = (String.IsNullOrEmpty(row["spr"].ToString())) ?
                        "spec." + fieldName + " as '" + rusName + "'" :
                        row["spr"].ToString() + ".name" + " as '" + rusName + "'";
                }
                fieldList.Add(fieldName);
            }
            return fieldList;
        }

        private ArrayList GetConditionList()
        {
            String fieldName = null;
            String fieldType = null;
            String conditionName = null;
            String condition = null;
            ArrayList conditionList = new ArrayList();

            foreach (DataGridViewRow gridRow in dgWhere.Rows)
            {
                if (gridRow.Cells["fields"].Value == null |
                    gridRow.Cells["conditions"].Value == null |
                    gridRow.Cells["value"].Value == null)
                    continue;

                String idFields = gridRow.Cells["fields"].Value.ToString();
                String idConditions = gridRow.Cells["conditions"].Value.ToString();
                String value = gridRow.Cells["value"].Value.ToString();

                foreach (DataRow row in dtListField.Select("id = '" + idFields + "'"))
                {
                    fieldName = row["field_name"].ToString();
                    fieldName = (String.IsNullOrEmpty(row["spr"].ToString())) ?
                        "spec." + fieldName : row["spr"].ToString() + ".id";
                    fieldType = row["field_type"].ToString();
                }

                foreach (DataRow row in dtSprCondition.Select("id = '" + idConditions + "'"))
                    conditionName = row["condition"].ToString();

                switch (fieldType)
                {
                    case "Дата":
                        try
                        {
                            value = DateTime.Parse(value).ToOADate().ToString();
                        }
                        catch (FormatException)
                        {
                            MessageBox.Show("Неверное условие или формат даты", "Сообщение");
                        }
                        break;
                    case "Текстовый":
                        if (conditionName == "LIKE")
                            value = "%" + value + "%";
                        value = "'" + value + "'";
                        break;
                    case "Подставное":
                        break;
                    default:
                        break;
                }

                condition = fieldName + " " + conditionName + " " + value;

                conditionList.Add(condition);
            }
            return conditionList;
        }

        private void bnShow_Click(object sender, EventArgs e)
        {
            String qstrSel = null;
            String qstrWhere = null;
            ArrayList fieldList = GetFieldList();
            ArrayList condList = GetConditionList();

            if (fieldList.Count == 0)
                return;

            foreach (Object obj in fieldList)
            {
                String field = obj.ToString();
                qstrSel = (qstrSel == null) ? field : qstrSel + ", " + field;
            }

            foreach (Object obj in condList)
            {
                String field = obj.ToString();
                qstrWhere = (qstrWhere == null) ? " WHERE (" + field + ")" : qstrWhere + " AND " + "(" + field + ")";
            }

            qstrWhere = (qstrWhere == null) ? " WHERE (spec.is_present = true) " : qstrWhere + " AND (spec.is_present = true)";

            String qstrJoin = @"((((spec LEFT JOIN spr_nation ON (spec.nation_id = spr_nation.id) )
                                LEFT JOIN spr_edu ON (spec.edu_id = spr_edu.id) )
                                LEFT JOIN spr_profession ON (spec.profession_id = spr_profession.id) )
                                LEFT JOIN spr_mstatus ON (spec.mstatus_id = spr_mstatus.id) )
                                LEFT JOIN spr_party_number ON (spec.party_id = spr_party_number.id)";
            String qstr = "SELECT " + qstrSel + " FROM " + qstrJoin + qstrWhere;

            //MessageBox.Show(qstr);
            DataTable dtResult = eurDBcon.GetDataTable("result", qstr);

            dgExport.DataSource = dtResult;
            lbRecords.Text = "" + dgExport.RowCount.ToString();

        }

        private void bnExcell_Click(object sender, EventArgs e)
        {
            if (dgExport.RowCount == 0)
            {
                MessageBox.Show("Отсутствуют данные для выгрузки");
                return;
            }

            Excel.Application excelApp = new Excel.Application();
            excelApp.Visible = true;
            excelApp.SheetsInNewWorkbook = 3;

            excelApp.Workbooks.Add(Type.Missing);
            Excel._Worksheet workSheet = (Excel.Worksheet)excelApp.ActiveSheet;

            int col = 1;
            foreach (DataGridViewColumn column in dgExport.Columns)
            {
                workSheet.Cells[1, col] = column.HeaderText.ToString();
                ((Excel.Range)workSheet.Cells[1, col]).Font.Bold = true;
                //                    ((Excel.Range)workSheet.Cells[1, col]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
                col++;
            }

            int r = 2;

            foreach (DataGridViewRow row in dgExport.Rows)
            {
                if (row.IsNewRow)
                    continue;
                for (int i = 1; i < col; i++)
                    workSheet.Cells[r, i] = row.Cells[i - 1].Value.ToString();
                r++;
            }

            Excel.Range exRange = workSheet.get_Range(workSheet.Cells[1, 1], workSheet.Cells[r - 1, col - 1]);
            exRange.EntireColumn.AutoFit();
            exRange.Borders.Color = System.Drawing.Color.Black.ToArgb();

            //Освобождаем ресурсы Excel
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
            excelApp = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();

        }

        private void dgWhere_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgWhere.Columns[dgWhere.CurrentCell.ColumnIndex].Name != "fields")
                return;

            if (dgWhere.Rows[e.RowIndex].Cells["fields"].Value == null)
                return;
            String id = dgWhere.Rows[e.RowIndex].Cells["fields"].Value.ToString();
            String sprName = null;
            foreach (DataRow row in dtListField.Select("id = '" + id + "'"))
                sprName = row["spr"].ToString();
            //MessageBox.Show(sprName);

            if (sprName == String.Empty)
            {
                DataGridViewTextBoxCell tbcell = new DataGridViewTextBoxCell();
                dgWhere.Rows[e.RowIndex].Cells["value"] = tbcell;
                dgWhere.Rows[e.RowIndex].Cells["conditions"].ReadOnly = false;
                return;
            }

            SprDbConnect sprCon = new SprDbConnect(sprName);
            DataGridViewComboBoxCell cell = new DataGridViewComboBoxCell();
            cell.DataSource = sprCon.GetDataTable(sprName);
            cell.ValueMember = "id";
            cell.DisplayMember = "name";
            cell.DropDownWidth = 100;
            cell.MaxDropDownItems = 7;
            dgWhere.Rows[e.RowIndex].Cells["value"] = cell;
            dgWhere.Rows[e.RowIndex].Cells["conditions"].Value = 1;
            dgWhere.Rows[e.RowIndex].Cells["conditions"].ReadOnly = true;

        }
    }
}
