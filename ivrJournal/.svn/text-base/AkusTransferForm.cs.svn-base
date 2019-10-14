using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Schema;
using System.IO;

namespace ivrJournal
{
    public partial class AkusTransferForm : Form
    {
        private akusDbConnect akusDbCon;
        private SprDbConnect mainDbCon;
        private DataTable dtMarked;
        private DataTable dtMarkedNotExist;
        private IniParser parser;
        private String strDBPath;
        private String strFotoDirPath;
        private bool tabControlChanging;

        private BackgroundWorker backgroundWorker;
        private BackgroundWorker backgroundWorker2;

        public AkusTransferForm()
        {
            InitializeComponent();

            bnNext.Enabled = false;
            bnPrev.Enabled = false;

            DataGridViewTextBoxColumn textColumn = new DataGridViewTextBoxColumn();
            {
                textColumn.DataPropertyName = "id";
                textColumn.HeaderText = "код";
                textColumn.Width = 30;
                textColumn.MinimumWidth = 50;
                textColumn.Visible = false;
                textColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            }
            dgAkusList.Columns.Add(textColumn);

            textColumn = new DataGridViewTextBoxColumn();
            {
                textColumn.DataPropertyName = "last_name";
                textColumn.HeaderText = "Фамилия";
                textColumn.Width = 100;
                textColumn.MinimumWidth = 90;
                textColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            dgAkusList.Columns.Add(textColumn);

            textColumn = new DataGridViewTextBoxColumn();
            {
                textColumn.DataPropertyName = "first_name";
                textColumn.HeaderText = "Имя";
                textColumn.Width = 100;
                textColumn.MinimumWidth = 90;
                textColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            dgAkusList.Columns.Add(textColumn);

            textColumn = new DataGridViewTextBoxColumn();
            {
                textColumn.DataPropertyName = "patronymic";
                textColumn.HeaderText = "Отчество";
                textColumn.Width = 100;
                textColumn.MinimumWidth = 90;
                textColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            dgAkusList.Columns.Add(textColumn);

            textColumn = new DataGridViewTextBoxColumn();
            {
                textColumn.DataPropertyName = "birthdate";
                textColumn.HeaderText = "Дата рождения";
                textColumn.Width = 80;
                textColumn.MinimumWidth = 80;
                textColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            }
            dgAkusList.Columns.Add(textColumn);

            DataGridViewCheckBoxColumn columnCheck =
                new DataGridViewCheckBoxColumn();
            {
                columnCheck.ReadOnly = false;
                columnCheck.Name = "is_present";
                columnCheck.DataPropertyName = "is_present";
                columnCheck.HeaderText = "Метка";
                columnCheck.Width = 50;
                columnCheck.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            }
            dgAkusList.Columns.Add(columnCheck);

            dgAkusList.AutoGenerateColumns = false;

            this.tabControl1.Selected += new TabControlEventHandler(tabControl1_Selected);
        }

        void tabControl1_Selected(object sender, TabControlEventArgs e)
        {

            switch (e.TabPageIndex)
            {
                case 1:
                    if (parser == null)
                        return;

                    tabControl1.Enabled = false;

                    backgroundWorker = new BackgroundWorker();
                    backgroundWorker.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
                    backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
                    this.Cursor = Cursors.WaitCursor;
                    backgroundWorker.RunWorkerAsync();

                    /*
                    akusDbCon = new akusDbConnect(strDBPath, "spec", "PC6", "PC7", "PC22", "PC20",
                        "relations", "bonus", "PC37", "penalty", "PC29", "PC56");
                    */

                    /*
                    DataTable dtSpec = akusDbCon.GetDataTable("spec");
                    if (dtSpec == null)
                    {
                        tabControl1.SelectedTab = tabPage1;
                        return;
                    }

                    DataView dv = new DataView(dtSpec);
                    dgAkusList.DataSource = dv;
                    this.lblRecCount.Text = akusDbCon.GetDataTable("spec").Rows.Count.ToString();
                     */
                    break;
                case 2:
                    if (akusDbCon == null)
                        return;
                    
                    clbLoadList.SetItemChecked(0, true);
                    clbLoadList.SetItemChecked(1, true);
                    clbLoadList.SetItemChecked(2, true);
                    clbLoadList.SetItemChecked(3, true);

                    if (backgroundWorker.IsBusy)
                    {
                        tabControl1.SelectedTab = tabPage2;
                        return;
                    }

                    tabControl1.Enabled = false;
                    backgroundWorker2 = new BackgroundWorker();
                    backgroundWorker2.DoWork += new DoWorkEventHandler(backgroundWorker2_DoWork);
                    backgroundWorker2.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker2_RunWorkerCompleted);
                    this.Cursor = Cursors.WaitCursor;
                    backgroundWorker2.RunWorkerAsync();
                    /*
                    DelNotMarkedRec(akusDbCon.GetDataTable("spec"));
                    mainDbCon = new SprDbConnect("spec", "spr_edu", "spr_profession", "spr_nation", "spr_mstatus",
                                                    "spr_degree", "relations",
                                                    "spr_bonus_type", "bonus", 
                                                    "spr_penalty_type", "spr_performers", "penalty");
                    */
                    break;
            }

         }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            /*
 * PC6 - справочник национальностей
 * PC7 - справочник профессий
 * PC22 - справочник образований
 * PC20 - справочник степеней родства
 * PC37 - справочник видов поощрений
 * PC29 - справочник видов взысканий
 * PC56 - справочник видов нарушений
 */
            akusDbCon = new akusDbConnect(strDBPath, "spec", "PC6", "PC7", "PC22", "PC20",
                        "relations", "bonus", "PC37", "penalty", "PC29", "PC56");
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Cursor = Cursors.Default;
            DataTable dtSpec = akusDbCon.GetDataTable("spec");
            if (dtSpec == null)
            {
                tabControl1.SelectedTab = tabPage1;
                return;
            }

            DataView dv = new DataView(dtSpec);
            dgAkusList.DataSource = dv;
            this.lblRecCount.Text = akusDbCon.GetDataTable("spec").Rows.Count.ToString();
            tabControl1.Enabled = true;
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            //this.Cursor = Cursors.WaitCursor;
            DelNotMarkedRec(akusDbCon.GetDataTable("spec"));
            mainDbCon = new SprDbConnect("spec", "spr_edu", "spr_profession", "spr_nation", "spr_mstatus",
                                            "spr_degree", "relations",
                                            "spr_bonus_type", "bonus",
                                            "spr_penalty_type", "spr_performers", "penalty");
        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Cursor = Cursors.Default;
            tabControl1.Enabled = true;
        }

        private void bnSetPath_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            //openFileDialog.InitialDirectory = this.tbDBPath.Text;
            
            openFileDialog.Filter = "Ini Files (*.ini)|*.ini|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                parser = new IniParser(openFileDialog.FileName);
                this.lblDataPath.Text = parser.GetSetting("System", "Data");
                this.lblMetadataPath.Text = parser.GetSetting("System", "Metadata");
                this.lblFotoPath.Text = parser.GetSetting("System", "Foto");
                this.lblQualifPath.Text = parser.GetSetting("System", "Qualif");
                
                int len = parser.GetSetting("System", "Data").LastIndexOf('D') - 1;
                strDBPath = parser.GetSetting("System", "Data").Substring(0, len);
                strFotoDirPath = parser.GetSetting("System", "Foto");
                //string FileName = openFileDialog.FileName;
                //this.tbDBPath.Text = openFileDialog.FileName;
                // TODO: Add code here to open the file.
                bnNext.Enabled = true;
            }
        }

        private void bnCheck_Click(object sender, EventArgs e)
        {
            if (parser == null)
            {
                MessageBox.Show("Необходимо указать путь к базе данных", "Ошибка");
                return;
            }

            akusDbCon = new akusDbConnect(parser.GetSetting("System", "Data"));
            if (akusDbCon.CheckDbBool() )
                MessageBox.Show("Проверка соединения выполнена", "Сообщение");
        }

        private void bnLoadSpr_Click(object sender, EventArgs e)
        {

            int nation_count = akusDbCon.mapDataSpr(dtMarked, mainDbCon.GetDataTable("spr_nation"), "nation_name");
            mainDbCon.UpdateDataTable("spr_nation");
            int edu_count = akusDbCon.mapDataSpr(dtMarked, mainDbCon.GetDataTable("spr_edu"), "edu_name");
            mainDbCon.UpdateDataTable("spr_edu");
            int profession_count = akusDbCon.mapDataSpr(dtMarked, mainDbCon.GetDataTable("spr_profession"),"profession_name");
            mainDbCon.UpdateDataTable("spr_profession");

            int mstatus_count = akusDbCon.mapDataMstatus(dtMarked, mainDbCon.GetDataTable("spr_mstatus"), mainDbCon);
            //mainDbCon.UpdateDataTable("spr_mstatus");

            int degree_count = akusDbCon.mapDataSpr(akusDbCon.GetDataTable("relations"), 
                                                    mainDbCon.GetDataTable("spr_degree"), "degree_name");
            mainDbCon.UpdateDataTable("spr_degree");

            int bonus_type_count = akusDbCon.mapDataSpr(akusDbCon.GetDataTable("bonus"),
                                                    mainDbCon.GetDataTable("spr_bonus_type"), "bonus_type_name");
            mainDbCon.UpdateDataTable("spr_bonus_type");

            int penalty_type_count = akusDbCon.mapDataSpr(akusDbCon.GetDataTable("penalty"),
                                                    mainDbCon.GetDataTable("spr_penalty_type"), "penalty_type_name");
            mainDbCon.UpdateDataTable("spr_penalty_type");

            int performers_count = akusDbCon.mapDataSpr(akusDbCon.GetDataTable("penalty"),
                                                    mainDbCon.GetDataTable("spr_performers"), "performer");
            mainDbCon.UpdateDataTable("spr_performers");

            MessageBox.Show("Национальности: " + nation_count.ToString() + "\n" +
                            "Образования: " + edu_count.ToString() + "\n" +
                            "Профессии: " + profession_count.ToString() + "\n" +
                            "Семейные положения: " + mstatus_count.ToString() + "\n" +
                            "Виды поощрений: " + bonus_type_count.ToString() + "\n" +
                            "Виды взысканий: " + penalty_type_count.ToString() + "\n" +
                            "Сотрудники: " + performers_count.ToString() + "\n" +
                            "Родственные отношения: " + degree_count.ToString(), "Добавлено новых записей в справочники");
        }

        //Оставить только отмеченные записи
        private void DelNotMarkedRec(DataTable dtakus_spec)
        {
            DataTable dt_buf = dtakus_spec.Copy();
            List<DataRow> rowsToRemove = new List<DataRow>();

            foreach(DataRow row in dt_buf.Rows)
            {
                if (Convert.ToBoolean(row["is_present"]) == false)
                {
                    rowsToRemove.Add(row);
                    continue;
                }
            }

            foreach (DataRow dr in rowsToRemove)
                dt_buf.Rows.Remove(dr);

            dt_buf.AcceptChanges();

            dtMarked = dt_buf;
        }

        //Удалить уже существующие записи
        private int DelExistRec(DataTable dtakus_spec)
        {
            DataTable dt_buf = dtakus_spec.Copy();

            SprDbConnect newDbCon = new SprDbConnect("spec");
            DataTable dtivr_spec = newDbCon.GetDataTable("spec");

            int count = CompareTables(dt_buf, dtivr_spec, "last_name", "first_name", "patronymic", "birthdate");

            dtMarkedNotExist = dt_buf;

            return count;
        }

        //Сравниваем и удаляем совпадающие строки в источнике
        public int CompareTables(DataTable dtSrc, DataTable dtDst, params String[] colNameList)
        {
            int count = 0;
            List<DataRow> rowsToRemove = new List<DataRow>();

            foreach(DataRow srcRow in dtSrc.Rows)
            {
                foreach (DataRow dstRow in dtDst.Rows)
                {
                    if (CompareRows(srcRow, dstRow, colNameList))
                    {
                        rowsToRemove.Add(srcRow);
                        count++;
                        break;
                    }
                }
            }

            foreach(DataRow dr in rowsToRemove)
                dtSrc.Rows.Remove(dr);

            dtSrc.AcceptChanges();
            return count;
        }

        public Boolean CompareRows(DataRow srcRow, DataRow dstRow, params String[] colNameList)
        {
            Boolean result = true;

            foreach (String columnName in colNameList)
            {
                if (Convert.IsDBNull(srcRow[columnName]) & Convert.IsDBNull(dstRow[columnName]))
                    continue;
                if (Convert.IsDBNull(srcRow[columnName]) | Convert.IsDBNull(dstRow[columnName]))
                {
                    result = false;
                    break;
                }

                if (columnName.ToUpper().Contains("DATE"))
                {
                    DateTime dt1 = DateTime.Parse(Convert.ToString(srcRow[columnName]));
                    DateTime dt2 = DateTime.Parse(Convert.ToString(dstRow[columnName]));
                    if (dt1.CompareTo(dt2) != 0)
                    {
                        result = false;
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }

                if (srcRow[columnName].ToString().Trim().ToUpper() != dstRow[columnName].ToString().Trim().ToUpper())
                {
                    result = false;
                    break;
                }
            }
            return result;
        }

        //Удаляем повторяющиеся записи в дополнителных таблицах
        private int DelExistRecExtra(DataTable dtakus_extra, DataTable dtivr_extra, params String[] colNameList)
        {
            int delCount = 0;
            List<DataRow> rowsToRemove = new List<DataRow>();

            foreach(DataRow akusRow in dtakus_extra.Rows)
            {
                foreach (DataRow ivrRow in dtivr_extra.Rows)
                {
                    if ( CompareRows(akusRow, ivrRow, colNameList ) )
                    {
                        rowsToRemove.Add(akusRow);
                        delCount++;
                        break;
                    }
                }
            }

            foreach (DataRow dr in rowsToRemove)
                dtakus_extra.Rows.Remove(dr);

            dtakus_extra.AcceptChanges();
            return delCount;
        }


        private void bnLoadSpec_Click(object sender, EventArgs e)
        {
            if (clbLoadList.GetItemChecked(0))
                LoadSpec();
            if (clbLoadList.GetItemChecked(1))
                LoadExtra1();
            if (clbLoadList.GetItemChecked(2))
                LoadExtra2();
            if (clbLoadList.GetItemChecked(3))
                LoadExtra3();
        }

        private void LoadSpec()
        {
            if (mainDbCon == null)
                return;
            DataTable dtivr_spec = mainDbCon.GetDataTable("spec");

            //Удаляем уже существующие записи
            int existCount = DelExistRec(dtMarked);

            try
            {
                String akusFotoPath;
                String ivrDBDirPath = mainDbCon.GetDBDirPath();
                FileInfo fotoFile;
                foreach (DataRow row in dtMarkedNotExist.Rows)
                {
                    if ( Convert.IsDBNull(row["encodedfoto"]) )
                        continue;
                    akusFotoPath = akusDbCon.UncodeName((String)row["encodedfoto"], strFotoDirPath);
                    if (akusFotoPath == String.Empty)
                        continue;
                    fotoFile = new FileInfo(akusFotoPath);
                    
                    if (!fotoFile.Exists)
                        continue;
                    row["foto"] = mainDbCon.GetFotoPath();
                    fotoFile.CopyTo(ivrDBDirPath + @"\" + (String)row["foto"], true);
                }

                akusDbCon.SetSprFields(mainDbCon.GetDataTable("spr_nation"), dtMarkedNotExist, "nation_name", "nation_id");
                akusDbCon.SetSprFields(mainDbCon.GetDataTable("spr_edu"), dtMarkedNotExist, "edu_name", "edu_id");
                akusDbCon.SetSprFields(mainDbCon.GetDataTable("spr_profession"), dtMarkedNotExist, "profession_name", "profession_id");

                dtMarkedNotExist.WriteXml(@"spec.xml", XmlWriteMode.WriteSchema);

                dtivr_spec.ReadXml(@"spec.xml");

                mainDbCon.UpdateDataTable("spec");

                MessageBox.Show("Загружено записей об абонентах: " +
                                dtMarkedNotExist.Rows.Count.ToString() + "\n" +
                                "Не загружено (уже существуют в картотеке): " + 
                                existCount.ToString(), "Сообщение");
            }
            catch (IOException ioe)
            {
                MessageBox.Show(ioe.ToString(), "Ошибка");
            }
        }

        private void bnNext_Click(object sender, EventArgs e)
        {
            tabControlChanging = true;
            if (tabControl1.SelectedTab == tabPage1)
            {
                tabControl1.SelectedTab = tabPage2;
                bnPrev.Enabled = true;
            }
            else if (tabControl1.SelectedTab == tabPage2)
            {
                tabControl1.SelectedTab = tabPage3;
                bnPrev.Enabled = true;
                bnNext.Enabled = false;
            }
        }

        private void bnPrev_Click(object sender, EventArgs e)
        {
            tabControlChanging = true;
            if (tabControl1.SelectedTab == tabPage2)
            {
                tabControl1.SelectedTab = tabPage1;
                bnPrev.Enabled = false;
            }
            else if (tabControl1.SelectedTab == tabPage3)
            {
                tabControl1.SelectedTab = tabPage2;
                bnNext.Enabled = true;
            }
        }

        private void cbAll_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dtSpec = akusDbCon.GetDataTable("spec");
            if (dtSpec == null)
                return;
            foreach (DataRow row in dtSpec.Rows)
            {
                row["is_present"] = cbAll.Checked;
            }

        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (!tabControlChanging)
            {
                e.Cancel = true;
            }
            else
            {
                tabControlChanging = false;
            }
        }

        private void bnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        //Удаляем не отмеченные записи
        private void DelNotMarked(DataTable dtBuf, DataTable dtMarked)
        {
            Boolean isfound = false;
            List<DataRow> rowsToRemove = new List<DataRow>();

            foreach(DataRow bufRow in dtBuf.Rows)
            {
                isfound = false;
                foreach (DataRow specRow in dtMarked.Rows)
                {
                    if (specRow["id_ptk_akus"].ToString().Trim() == bufRow["id_ptk_akus"].ToString().Trim())
                    {
                        isfound = true;
                        break;
                    }
                }

                if (!isfound)
                    rowsToRemove.Add(bufRow);
            }

            foreach (DataRow dr in rowsToRemove)
                dtBuf.Rows.Remove(dr);

            dtBuf.AcceptChanges();
        }


        private void AddSpecId(DataTable dtBuf)
        {
            SprDbConnect newDbCon = new SprDbConnect("spec");
            DataTable dtivr_spec = newDbCon.GetDataTable("spec");

            foreach (DataRow relRow in dtBuf.Rows)
            {
                foreach (DataRow specRow in dtivr_spec.Rows)
                {
                    if (specRow["id_ptk_akus"].ToString().Trim() == relRow["id_ptk_akus"].ToString().Trim())
                    {
                        relRow["id_spec"] = Convert.ToInt32(specRow["id"]);
                        break;
                    }
                }
            }
        }

        //Загрузка информации о родственниках
        private void LoadExtra1()
        {
            if (mainDbCon == null)
                return;

            DataTable dtakus_rel = akusDbCon.GetDataTable("relations");
            DataTable dtivr_rel = mainDbCon.GetDataTable("relations");

            DataTable dt_buf = dtakus_rel.Copy();
            
            try
            {
                //Оставляем записи только для отмеченных
                DelNotMarked(dt_buf, dtMarked);

                //Добавляем идентификатор записи из таблицы spec
                //!!!!!!!!!!!!!!!!
                AddSpecId(dt_buf);

                //Заполняем поле degree_id значениями из справочника
                akusDbCon.SetSprFields(mainDbCon.GetDataTable("spr_degree"), dt_buf, "degree_name", "degree_id");

                int delCount = DelExistRecExtra(dt_buf, dtivr_rel, 
                        "id_spec", "degree_id", "last_name", "first_name", "patronymic", "birthdate");

                dt_buf.WriteXml(@"relations.xml", XmlWriteMode.WriteSchema);
                dtivr_rel.ReadXml(@"relations.xml");
                mainDbCon.UpdateDataTable("relations");

                MessageBox.Show("Загружено записей о родственниках: " + dt_buf.Rows.Count.ToString() + "\n" + 
                                "Не загружено (уже существуют): " + delCount.ToString(), "Сообщение");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка");
            }
        }

        //Загрузка информации о поощрениях
        private void LoadExtra2()
        {
            if (mainDbCon == null)
                return;

            DataTable dtakus_bonus = akusDbCon.GetDataTable("bonus");
            DataTable dtivr_bonus = mainDbCon.GetDataTable("bonus");

            DataTable dt_buf = dtakus_bonus.Copy();

            try
            {
                //Оставляем записи поощрений только для отмеченных
                DelNotMarked(dt_buf, dtMarked);

                //Добавляем идентификатор записи из таблицы spec
                AddSpecId(dt_buf);

                //Заполняем поле bonus_type_id значениями из справочника
                akusDbCon.SetSprFields(mainDbCon.GetDataTable("spr_bonus_type"), dt_buf, 
                    "bonus_type_name", "bonus_type_id");

                int delCount = DelExistRecExtra(dt_buf, dtivr_bonus,
                        "id_spec", "bonus_type_id", "bonus_reason", "order_number", "order_date");

                dt_buf.WriteXml(@"bonus.xml", XmlWriteMode.WriteSchema);
                dtivr_bonus.ReadXml(@"bonus.xml");
                mainDbCon.UpdateDataTable("bonus");

                MessageBox.Show("Загружено записей о поощрениях: " + dt_buf.Rows.Count.ToString() + "\n" +
                                "Не загружено (уже существуют): " + delCount.ToString(), "Сообщение");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка");
            }
        }

        //Загрузка информации о взысканиях
        private void LoadExtra3()
        {
            if (mainDbCon == null)
                return;

            DataTable dtakus_penalty = akusDbCon.GetDataTable("penalty");
            DataTable dtivr_penalty = mainDbCon.GetDataTable("penalty");

            DataTable dt_buf = dtakus_penalty.Copy();

            try
            {
                //Оставляем записи только для отмеченных абонентов
                DelNotMarked(dt_buf, dtMarked);

                //Добавляем идентификатор записи из таблицы spec
                AddSpecId(dt_buf);

                //Заполняем поле penalty_type_id значениями из справочника
                akusDbCon.SetSprFields(mainDbCon.GetDataTable("spr_penalty_type"), dt_buf,
                    "penalty_type_name", "penalty_type_id");
                
                //Заполняем поле performer_id значениями из справочника
                akusDbCon.SetSprFields(mainDbCon.GetDataTable("spr_performers"), dt_buf,
                    "performer", "performer_id");

                int delCount = DelExistRecExtra(dt_buf, dtivr_penalty,
                        "id_spec", "penalty_type_id", "reason", "order_number", "order_date", "removal");

                dt_buf.WriteXml(@"penalty.xml", XmlWriteMode.WriteSchema);
                dtivr_penalty.ReadXml(@"penalty.xml");
                mainDbCon.UpdateDataTable("penalty");

                MessageBox.Show("Загружено записей о взысканиях: " + dt_buf.Rows.Count.ToString() + "\n" +
                                "Не загружено (уже существуют): " + delCount.ToString(), "Сообщение");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка");
            }
        }
        
        private void dgAkusList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.ColumnIndex == -1) | (e.RowIndex == -1))
                return;

            DataRow row = ((DataView)dgAkusList.DataSource)[e.RowIndex].Row;
            row["is_present"] = !Convert.ToBoolean(row["is_present"]);

        }

        private void EditFind()
        {
            DataView IsPresentView = new DataView(akusDbCon.GetDataTable("spec"));

            if (tbLastName.Text != String.Empty)
                IsPresentView.RowFilter = "last_name LIKE '%" + tbLastName.Text + "%'";
            else
                IsPresentView.RowFilter = "";
            //            dgIVRList.AutoGenerateColumns = false;
            dgAkusList.DataSource = IsPresentView;
        }

        private void tbLastName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                EditFind();
            }
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
        

    }
}

