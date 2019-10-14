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
    public partial class KartForm : Form
    {
        private MainDBConnect newDBcon;
        private BackgroundWorker backgroundWorker;
        private ToolStripProgressBar toolStripProgressBar;
        private ToolStripStatusLabel toolStripStatusLabel;
        private DataView IsPresentView;

        public KartForm()
        {
            InitializeComponent();

//            SetRights();

            DataGridViewTextBoxColumn textColumn = new DataGridViewTextBoxColumn();
            {
                textColumn.DataPropertyName = "id";
                textColumn.HeaderText = "код";
                textColumn.Width = 30;
                textColumn.MinimumWidth = 50;
                textColumn.Visible = false;
                textColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            }
            dgListPerson.Columns.Add(textColumn);

            textColumn = new DataGridViewTextBoxColumn();
            {
                textColumn.DataPropertyName = "last_name";
                textColumn.HeaderText = "Фамилия";
                textColumn.Width = 100;
                textColumn.MinimumWidth = 90;
                textColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            dgListPerson.Columns.Add(textColumn);

            textColumn = new DataGridViewTextBoxColumn();
            {
                textColumn.DataPropertyName = "first_name";
                textColumn.HeaderText = "Имя";
                textColumn.Width = 100;
                textColumn.MinimumWidth = 90;
                textColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            dgListPerson.Columns.Add(textColumn);

            textColumn = new DataGridViewTextBoxColumn();
            {
                textColumn.DataPropertyName = "patronymic";
                textColumn.HeaderText = "Отчество";
                textColumn.Width = 100;
                textColumn.MinimumWidth = 90;
                textColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            dgListPerson.Columns.Add(textColumn);

            textColumn = new DataGridViewTextBoxColumn();
            {
                textColumn.DataPropertyName = "birthdate";
                textColumn.HeaderText = "Дата рождения";
                textColumn.Width = 80;
                textColumn.MinimumWidth = 80;
                textColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            }
            dgListPerson.Columns.Add(textColumn);

            dgListPerson.AutoGenerateColumns = false;

            toolTip1.SetToolTip(bnFind, "Выбрать по фамилии");
            toolTip1.SetToolTip(tbLastName, "Введите фамилию для выборки");
            toolTip1.SetToolTip(bnFindDel, "Отменить выборку по фамилии");
            toolTip1.SetToolTip(cbParty, "Выберите отряд для выборки");
            toolTip1.SetToolTip(bnPartyDel, "Отменить выборку по отрядам");

            toolTip1.SetToolTip(bnAdd, "Добавить новую запись");
            toolTip1.SetToolTip(bnChange, "Изменить реквизиты");
            toolTip1.SetToolTip(bnDoc, "Загрузить, показать психологическую характеристику");
            toolTip1.SetToolTip(bnDelete, "Удалить запись");

            toolTip1.SetToolTip(bnPrev_conv, "Сведения об имеющихся судимостях и основания освобождения");
            toolTip1.SetToolTip(bnRelations, "Сведения о родственниках");
            toolTip1.SetToolTip(bnParty, "Перемещения по отрядам");
            toolTip1.SetToolTip(bnProf, "Профилактический учет");
            toolTip1.SetToolTip(bnBonus, "Учет поощрений, объявленных осужденному");
            toolTip1.SetToolTip(bnPenalty, "Учет  взысканий,  наложенных на осужденного по постановлениям руководства ИУ");

            toolTip1.SetToolTip(bnIPO, "Индивидуально-психологические особенности личности осужденного");
            toolTip1.SetToolTip(bnIVR, "Индивидуальная воспитательная работа, проводимая с осужденным");
            toolTip1.SetToolTip(bnResolution, "Решения совета воспитателей отряда в отношении осужденного");

            toolTip1.SetToolTip(bnClose, "Закрыть форму");

            cbParty.ValueMember = "id";
            cbParty.DisplayMember = "name";
        

        }

        public void RefreshDG()
        {
            newDBcon.RefreshDataTable();
            DataView IsPresentView = new DataView(newDBcon.GetDataTable("spec"));
            IsPresentView.RowFilter = "is_present = true";
            dgListPerson.DataSource = IsPresentView;
            labelHelp.Text = "Загружено записей: " + dgListPerson.RowCount;
        }

        private void SetRights()
        {
            FormControlManager fcm = new FormControlManager();
            fcm.SetFormControlStatus(this);
//            this.Update();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ProfileForm profileForm = new ProfileForm(newDBcon);
            profileForm.MdiParent = this.MdiParent;
            profileForm.Show();
        }

        private void bnChange_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(((DataView)dgListPerson.DataSource).Count.ToString());
            if (((DataView)dgListPerson.DataSource).Count == 0)
                return;

            int RowIndex = 0;
            RowIndex = dgListPerson.CurrentRow.Index;
            //int RowIndex = (int)dgListPerson[dgListPerson.CurrentRowIndex,0];
            DataRow row = ((DataView)dgListPerson.DataSource)[RowIndex].Row;
            
            ProfileForm profileForm = new ProfileForm(newDBcon, row);
            profileForm.MdiParent = this.MdiParent;
            profileForm.Show();
        }

        private void bnDelete_Click(object sender, EventArgs e)
        {
            if (((DataView)dgListPerson.DataSource).Count == 0)
                return;

            DialogResult result = 
                MessageBox.Show("Вы действительно хотите переместить запись в архив?", "Сообщение", MessageBoxButtons.YesNo);
            if (result == DialogResult.No)
                return;
            int RowIndex = 0;
            RowIndex = dgListPerson.CurrentRow.Index;
            DataRow row = ((DataView)dgListPerson.DataSource)[RowIndex].Row;

            newDBcon.MarkAsNotPresent(row);

            labelHelp.Text = "Загружено записей: " + dgListPerson.RowCount;

            Form[] MDIf = this.MdiParent.MdiChildren;
            foreach (Form f in MDIf)
            {
                if (f.Name == "KartArchiveForm")//если форма найдена среди уже открытых
                {
                    ((KartArchiveForm)f).RefreshDG();
                    f.Refresh();//обновляем форму (пока не работает)
                    break;
                }
            }        
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MDIChildShow(string formName)
        {
            bool ind = false;
            Form[] MDIf = this.MdiParent.MdiChildren;
            foreach (Form f in MDIf)
            {
                if (f.Name == formName)//если форма найдена среди уже открытых
                {
                    ind = true;//установка флага поиска
                    f.Activate();//выводим найденную форму на передний план
                    break;
                }
            }
            if (!ind)//если форма не найдена среди открытых
            {
                Type type = Type.GetType(Path.GetFileNameWithoutExtension(Application.ExecutablePath) + "." + formName);
                ConstructorInfo ci = type.GetConstructor(new Type[] { });
                try
                {
                    Form f = (Form)ci.Invoke(new object[] { });
                    f.MdiParent = this.MdiParent;//установка родительской формы
                    f.Show();//показываем форму
                    f.Update();//при необходимости проводим обновление (перерисовку)
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Ошибка при открытии окна " + formName);
                }
            }
        }
        
        private void MDIChildShowSpr(string formName, DataRow row, string formSpr)
        {
            string formText;

            switch (formSpr)
            {
                case "resolution":
                    {
                        formText = "Решения совета воспитателей отряда";
                        break;
                    }
                case "ivr":
                    {
                        formText = "Индивидуальная воспитательная работа";
                        break;
                    }
                case "psycho_char":
                    {
                        formText = "Индивидуально-психологические особенности";
                        break;
                    }
                case "prev_conv":
                    {
                        formText = "Прежние судимости";
                        break;
                    }
                case "penalty":
                    {
                        formText = "Взыскания";
                        break;
                    }
                case "bonus":
                    {
                        formText = "Поощрения";
                        break;
                    }
                case "relations":
                    {
                        formText = "Родственники";
                        break;
                    }
                case "profilact_ychet":
                    {
                        formText = "Профилактический учет";
                        break;
                    }
                case "party":
                    {
                        formText = "Перемещения по отрядам";
                        break;
                    }
                case "spec_psycho":
                    {
                        formText = "Перечень документов психологов";
                        break;
                    }
                case "results":
                    {
                        formText = "Результаты работы по подготовке к освобождению";
                        break;
                    }
                default:
                    {
                        formText = "";
                        break;
                    }
            }

            bool ind = false;
            bool maxWin = false;
            Form[] MDIf = this.MdiParent.MdiChildren;
            foreach (Form f in MDIf)
            {
                if ((f.Name == formName) & (f.Text == formText)) //если форма найдена среди уже открытых
                {
                    ind = true;//установка флага поиска
                    f.Activate();//выводим найденную форму на передний план
                    break;
                }
            }
            if (!ind)//если форма не найдена среди открытых
            {
                Type type = Type.GetType(Path.GetFileNameWithoutExtension(Application.ExecutablePath) + "." + formName);

                Type[] types = new Type[2];
                types[0] = typeof(DataRow);
                types[1] = typeof(String);
                ConstructorInfo ci = type.GetConstructor(types);
                try
                {
                    if (this.WindowState == FormWindowState.Maximized)
                    {
                        this.WindowState = FormWindowState.Normal;
                        maxWin = true;

                    }
                    Form f = (Form)ci.Invoke(new object[] { row, formSpr });
                    f.MdiParent = this.MdiParent;//установка родительской формы
                    f.Show();//показываем форму
                    f.Update();//при необходимости проводим обновление (перерисовку)
                    if (maxWin)
                        f.WindowState = FormWindowState.Maximized;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Ошибка при открытии окна " + formText);
                }
            }
        }

        private void MDIChildShowIVR(string formName, DataRow row, string punktText)
        {
            string formText;
            string formSpr = "ivr";

            switch (punktText)
            {
                case "1":
                    {
                        if (formName == "ChildKartForm")
                            formText = "ИВР, проводимая начальником отряда";
                        else
                            formText = "Психологические характеристики";
                        break;
                    }
                case "2":
                    {
                        if (formName == "ChildKartForm")
                            formText = "ИВР, проводимая членами совета воспитателей отряда";
                        else
                            formText = "Документы по воспитательной работе";
                        break;
                    }
                case "3":
                    {
                        formText = "ИВР, проводимая иными сотрудниками";
                        break;
                    }
                default:
                    {
                        formText = "";
                        break;
                    }
            }

            bool ind = false;
            bool maxWin = false;
            Form[] MDIf = this.MdiParent.MdiChildren;
            foreach (Form f in MDIf)
            {
                if ((f.Name == formName) & (f.Text == formText)) //если форма найдена среди уже открытых
                {
                    ind = true;//установка флага поиска
                    f.Activate();//выводим найденную форму на передний план
                    break;
                }
            }
            if (!ind)//если форма не найдена среди открытых
            {
                Type type = Type.GetType(Path.GetFileNameWithoutExtension(Application.ExecutablePath) + "." + formName);

                Type[] types = new Type[3];
                types[0] = typeof(DataRow);
                types[1] = typeof(String);
                types[2] = typeof(String);
                ConstructorInfo ci = type.GetConstructor(types);
                try
                {
                    if (this.WindowState == FormWindowState.Maximized)
                    {
                        this.WindowState = FormWindowState.Normal;
                        maxWin = true;
                    }
                    Form f = (Form)ci.Invoke(new object[] { row, formSpr, punktText });
                    f.MdiParent = this.MdiParent;//установка родительской формы
                    f.Show();//показываем форму
                    f.Update();//при необходимости проводим обновление (перерисовку)
                    if (maxWin)
                        f.WindowState = FormWindowState.Maximized;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Ошибка при открытии окна " + formText);
                }
            }
        }


        private void bnParty_Click(object sender, EventArgs e)
        {
            if (((DataView)dgListPerson.DataSource).Count == 0)
                return;

            int RowIndex = 0;
            RowIndex = dgListPerson.CurrentRow.Index;

            DataRow row = ((DataView)dgListPerson.DataSource)[RowIndex].Row;

            MDIChildShowSpr("ChildKartForm", row, "party");
        }

        private void bnProf_Click(object sender, EventArgs e)
        {
            if (((DataView)dgListPerson.DataSource).Count == 0)
                return;

            int RowIndex = 0;
            RowIndex = dgListPerson.CurrentRow.Index;

            DataRow row = ((DataView)dgListPerson.DataSource)[RowIndex].Row;

            MDIChildShowSpr("ChildKartForm", row, "profilact_ychet");
        }

        private void buttonRelations_Click(object sender, EventArgs e)
        {
            if (((DataView)dgListPerson.DataSource).Count == 0)
                return;

            int RowIndex = 0;
            RowIndex = dgListPerson.CurrentRow.Index;

            DataRow row = ((DataView)dgListPerson.DataSource)[RowIndex].Row;

            MDIChildShowSpr("ChildKartForm", row, "relations");
        }

        private void bnBonus_Click(object sender, EventArgs e)
        {
            if (((DataView)dgListPerson.DataSource).Count == 0)
                return;

            int RowIndex = 0;
            RowIndex = dgListPerson.CurrentRow.Index;

            DataRow row = ((DataView)dgListPerson.DataSource)[RowIndex].Row;
 
            MDIChildShowSpr("ChildKartForm", row, "bonus");
        }

        private void bnPenalty_Click(object sender, EventArgs e)
        {
            if (((DataView)dgListPerson.DataSource).Count == 0)
                return;

            int RowIndex = 0;
            RowIndex = dgListPerson.CurrentRow.Index;

            DataRow row = ((DataView)dgListPerson.DataSource)[RowIndex].Row;

            MDIChildShowSpr("ChildKartForm", row, "penalty");
        }

        private void bnPrev_conv_Click(object sender, EventArgs e)
        {
            if (((DataView)dgListPerson.DataSource).Count == 0)
                return;

            int RowIndex = 0;
            RowIndex = dgListPerson.CurrentRow.Index;

            DataRow row = ((DataView)dgListPerson.DataSource)[RowIndex].Row;

            MDIChildShowSpr("ChildKartForm", row, "prev_conv");
        }

        private void bnIPO_Click(object sender, EventArgs e)
        {
            if (((DataView)dgListPerson.DataSource).Count == 0)
                return;

            int RowIndex = 0;
            RowIndex = dgListPerson.CurrentRow.Index;

            DataRow row = ((DataView)dgListPerson.DataSource)[RowIndex].Row;

            MDIChildShowSpr("ChildKartForm", row, "psycho_char");
        }

        private void bnIVR_Click(object sender, EventArgs e)
        {
            Point CurrentLocation = (sender as Control).PointToScreen(new Point(bnIVR.Width, bnIVR.Height));
            bnIVR.ContextMenuStrip.Show(CurrentLocation);
        }

        private void bnResolution_Click(object sender, EventArgs e)
        {
            if (((DataView)dgListPerson.DataSource).Count == 0)
                return;

            int RowIndex = 0;
            RowIndex = dgListPerson.CurrentRow.Index;

            DataRow row = ((DataView)dgListPerson.DataSource)[RowIndex].Row;

            MDIChildShowSpr("ChildKartForm", row, "resolution");
        }

        private void дневникИВРполныйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (((DataView)dgListPerson.DataSource).Count == 0)
                return;

            int RowIndex = 0;
            RowIndex = dgListPerson.CurrentRow.Index;

            DataRow row = ((DataView)dgListPerson.DataSource)[RowIndex].Row;

            try
            {
                RepDIVRForm repDIVRForm = new RepDIVRForm(row);
                repDIVRForm.MdiParent = this.MdiParent;
                repDIVRForm.Show();
            }
            catch 
            {
                MessageBox.Show("При формировании отчета произошла ошибка, попробуйте сформировать отчет еще раз.", "Ошибка");
            }
        }

        private void bnReports_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Point CurrentLocation = (sender as Control).PointToScreen(new Point(e.X, e.Y));
                bnReports.ContextMenuStrip.Show(CurrentLocation);
            }
        }

        private void EditFind(string strSQL)
        {
            DataView IsPresentView = new DataView(newDBcon.GetDataTable("spec"));

            if (strSQL != "")
            {
                SQLDBConnect sqlCon = new SQLDBConnect();
                DataTable dt = sqlCon.GetDataTable("dt", strSQL);
                if (dt == null)
                    return;
                String ids = null;
                foreach (DataRow row in dt.Rows)
                {
                    ids = (ids == null) ? row["id"].ToString() :
                                          ids + ", " + row["id"].ToString();
                }

                //String strID = (ids == null) ? "-1" : ids;
                //String strFilter = String.IsNullOrEmpty(strID) ? "(is_present = true)" : "(is_present = true) AND (id NOT IN (" + strID + "))";
                String strFilter = String.IsNullOrEmpty(ids) ? "(is_present = true)" : "(is_present = true) AND (id NOT IN (" + ids + "))";
                IsPresentView.RowFilter = strFilter;
            }
            else
            {
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
            }

            dgListPerson.AutoGenerateColumns = false;
            dgListPerson.DataSource = IsPresentView;
            labelHelp.Text = "Загружено записей: " + dgListPerson.RowCount;
        }
        
        private void bnFind_Click(object sender, EventArgs e)
        {
            EditFind("");
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            tbLastName.Text = "";
            EditFind("");
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                EditFind("");
            }
        }

        private void dgListPerson_DoubleClick(object sender, EventArgs e)
        {
            if (((DataView)dgListPerson.DataSource).Count == 0)
                return;

            int RowIndex = 0;
            RowIndex = dgListPerson.CurrentRow.Index;

            //MessageBox.Show(((DataView)dgListPerson.DataSource).Count.ToString() + RowIndex.ToString());
            /*
            if (((DataView)dgListPerson.DataSource).Count == RowIndex + 1)
                return;
            */
            DataRow row = ((DataView)dgListPerson.DataSource)[RowIndex].Row;

            ProfileForm profileForm = new ProfileForm(newDBcon, row);
            profileForm.MdiParent = this.MdiParent;
            profileForm.Show();
        }

        private void bnPsih_Click(object sender, EventArgs e)
        {
            Point CurrentLocation = (sender as Control).PointToScreen(new Point(bnDoc.Width, bnDoc.Height));
            bnDoc.ContextMenuStrip.Show(CurrentLocation);
        }

        private void KartForm_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            Help.ShowHelp(this, "help_ivr.chm", HelpNavigator.Topic, "help_ivr_2.htm");
        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }

        private void отчетToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (((DataView)dgListPerson.DataSource).Count == 0)
                return;

            int RowIndex = 0;
            RowIndex = dgListPerson.CurrentRow.Index;

            DataRow row = ((DataView)dgListPerson.DataSource)[RowIndex].Row;
            try
            {
                RepBonusAndPenaltyForm repBonusAndPenaltyForm = new RepBonusAndPenaltyForm(row);
                repBonusAndPenaltyForm.MdiParent = this.MdiParent;
                repBonusAndPenaltyForm.Show();
            }
            catch
            {
                MessageBox.Show("При формировании отчета произошла ошибка, попробуйте сформировать отчет еще раз.", "Ошибка");
            }

        }

        private void KartForm_Shown(object sender, EventArgs e)
        {
            //SetRights();
        }

        private void cbParty_SelectedIndexChanged(object sender, EventArgs e)
        {
            EditFind("");
        }

        private void bnPartyDel_Click(object sender, EventArgs e)
        {
            cbParty.SelectedIndex = -1;
            DataView IsPresentView = new DataView(newDBcon.GetDataTable("spec"));
            if (tbLastName.Text != String.Empty)
                IsPresentView.RowFilter = "(is_present = true) AND (last_name LIKE '%" + tbLastName.Text + "%')";
            else
                IsPresentView.RowFilter = "is_present = true";

            dgListPerson.AutoGenerateColumns = false;
            dgListPerson.DataSource = IsPresentView;
            labelHelp.Text = "Загружено записей: " + dgListPerson.RowCount;
        }

        private void bnReports_Click(object sender, EventArgs e)
        {
            Point CurrentLocation = (sender as Control).PointToScreen(new Point(bnReports.Width, bnReports.Height));
            bnReports.ContextMenuStrip.Show(CurrentLocation);
        }

        private void bnIVR_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Point CurrentLocation = (sender as Control).PointToScreen(new Point(e.X, e.Y));
                bnIVR.ContextMenuStrip.Show(CurrentLocation);
            }
        }

        private void иВРПроводимаяСОсужденнымНачальникомОтрядаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (((DataView)dgListPerson.DataSource).Count == 0)
                return;

            int RowIndex = 0;
            RowIndex = dgListPerson.CurrentRow.Index;

            DataRow row = ((DataView)dgListPerson.DataSource)[RowIndex].Row;

            MDIChildShowIVR("ChildKartForm", row, "1");
        }

        private void иВРПроводимаяСОсужденнымЧленамиСоветаВоспитателейОтрядаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (((DataView)dgListPerson.DataSource).Count == 0)
                return;

            int RowIndex = 0;
            RowIndex = dgListPerson.CurrentRow.Index;

            DataRow row = ((DataView)dgListPerson.DataSource)[RowIndex].Row;

            MDIChildShowIVR("ChildKartForm", row, "2");
        }

        private void иВРПроводимаяСОсужденнымИнымиСотрудникамиИсправительногоУчрежденияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (((DataView)dgListPerson.DataSource).Count == 0)
                return;

            int RowIndex = 0;
            RowIndex = dgListPerson.CurrentRow.Index;

            DataRow row = ((DataView)dgListPerson.DataSource)[RowIndex].Row;

            MDIChildShowIVR("ChildKartForm", row, "3");
        }

        private void tsmiСправкаОПоощренияхИВзысканияхВWord_Click(object sender, EventArgs e)
        {
            if (((DataView)dgListPerson.DataSource).Count == 0)
                return;

            int RowIndex = 0;
            RowIndex = dgListPerson.CurrentRow.Index;
            DataRow row = ((DataView)dgListPerson.DataSource)[RowIndex].Row;

            ReportsWordClass rw = new ReportsWordClass();
            rw.BonusAndPenalty(row);
        }

        private void tsmiДневникИВРWord_Click(object sender, EventArgs e)
        {
            if (((DataView)dgListPerson.DataSource).Count == 0)
                return;

            int RowIndex = 0;
            RowIndex = dgListPerson.CurrentRow.Index;
            DataRow row = ((DataView)dgListPerson.DataSource)[RowIndex].Row;

            ReportsWordClass rw = new ReportsWordClass();
            rw.IVRJournal(row);
        }

 
        private void bnArchive_Click(object sender, EventArgs e)
        {
            MDIChildShow("KartArchiveForm");
        }

        private void документыПсихологовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (((DataView)dgListPerson.DataSource).Count == 0)
                return;

            int RowIndex = 0;
            RowIndex = dgListPerson.CurrentRow.Index;
            DataRow row = ((DataView)dgListPerson.DataSource)[RowIndex].Row;

            MDIChildShowIVR("PsychoListForm", row, "1");
        }

        private void bnDoc_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Point CurrentLocation = (sender as Control).PointToScreen(new Point(e.X, e.Y));
                bnDoc.ContextMenuStrip.Show(CurrentLocation);
            }
        }

        private void tsmiДокументыВоспитателей_Click(object sender, EventArgs e)
        {
            if (((DataView)dgListPerson.DataSource).Count == 0)
                return;

            int RowIndex = 0;
            RowIndex = dgListPerson.CurrentRow.Index;
            DataRow row = ((DataView)dgListPerson.DataSource)[RowIndex].Row;

            MDIChildShowIVR("PsychoListForm", row, "2");
        }

        private void bnResults_Click(object sender, EventArgs e)
        {
            if (((DataView)dgListPerson.DataSource).Count == 0)
                return;

            int RowIndex = 0;
            RowIndex = dgListPerson.CurrentRow.Index;

            DataRow row = ((DataView)dgListPerson.DataSource)[RowIndex].Row;

            MDIChildShowSpr("ChildKartForm", row, "results");
        }

        private void произвольныеДокументывсеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (((DataView)dgListPerson.DataSource).Count == 0)
                return;

            int RowIndex = 0;
            RowIndex = dgListPerson.CurrentRow.Index;
            DataRow row = ((DataView)dgListPerson.DataSource)[RowIndex].Row;

            ReportsWordClass rw = new ReportsWordClass();
            rw.IVRDoc(row);
        }

         private void bnOptionalFind_Click(object sender, EventArgs e)
        {
            Point CurrentLocation = (sender as Control).PointToScreen(new Point(bnOptionalFind.Width, bnOptionalFind.Height));
            bnOptionalFind.ContextMenuStrip.Show(CurrentLocation);
        }

        private void bnOptionalFind_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Point CurrentLocation = (sender as Control).PointToScreen(new Point(e.X, e.Y));
                bnOptionalFind.ContextMenuStrip.Show(CurrentLocation);
            }
        }

       private void отрядыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditFind(@"SELECT DISTINCT spec.id
                       FROM spec RIGHT JOIN party ON spec.id=party.id_spec
                       WHERE (spec.is_present=true)");
        }

        private void родственникиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditFind(@"SELECT DISTINCT spec.id
                       FROM spec RIGHT JOIN relations ON spec.id=relations.id_spec
                       WHERE (spec.is_present=true)");

        }

        private void профучетToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditFind(@"SELECT DISTINCT spec.id
                       FROM spec RIGHT JOIN profilact_ychet ON spec.id=profilact_ychet.id_spec
                       WHERE (is_present=true) AND (profilact_ychet.data_snyat IS NULL)");
        }

        private void судимостиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditFind(@"SELECT DISTINCT spec.id
                      FROM spec RIGHT JOIN prev_conv ON spec.id=prev_conv.id_spec
                      WHERE (is_present=true)");
        }

        private void поощренияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditFind(@"SELECT DISTINCT spec.id
                      FROM spec RIGHT JOIN bonus ON spec.id=bonus.id_spec
                      WHERE (is_present=true)");
        }

        private void взысканияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditFind(@"SELECT DISTINCT spec.id
                      FROM spec RIGHT JOIN penalty ON spec.id=penalty.id_spec
                      WHERE (is_present=true)");
        }

        private void иВРПроводимаяНачальникомОтрядаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditFind(@"SELECT DISTINCT spec.id
                      FROM spec RIGHT JOIN ivr ON spec.id=ivr.id_spec
                      WHERE ((is_present=true) AND (id_type_ivr=1))");
        }

        private void иВРПроводимаяЧленамиСоветаВосОтрядаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditFind(@"SELECT DISTINCT spec.id
                      FROM spec RIGHT JOIN ivr ON spec.id=ivr.id_spec
                      WHERE ((is_present=true) AND (id_type_ivr=2))");
        }

        private void иВРПроводимаяИнымиСотрудникамиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditFind(@"SELECT DISTINCT spec.id
                      FROM spec RIGHT JOIN ivr ON spec.id=ivr.id_spec
                      WHERE ((is_present=true) AND (id_type_ivr=3))");
        }

        private void психологическиеХарактеристикиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditFind(@"SELECT DISTINCT spec.id
                       FROM spec RIGHT JOIN spec_psycho ON spec.id=spec_psycho.id_spec
                       WHERE ((is_present=true) AND (type_doc = 1))");
        }

        private void документыПоОсужденнымToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditFind(@"SELECT DISTINCT spec.id
                       FROM spec RIGHT JOIN spec_psycho ON spec.id=spec_psycho.id_spec
                       WHERE ((is_present=true) AND (type_doc = 2))");
        }

        private void фотоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditFind(@"SELECT DISTINCT spec.id
                       FROM spec
                       WHERE ((is_present=true) AND (foto IS NOT NULL))");
        }

        private void иПОToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditFind(@"SELECT DISTINCT spec.id
                       FROM spec RIGHT JOIN psycho_char ON spec.id=psycho_char.id_spec
                       WHERE (is_present=true)");
        }

        private void решенияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditFind(@"SELECT DISTINCT spec.id
                       FROM spec RIGHT JOIN resolution ON spec.id=resolution.id_spec
                       WHERE (is_present=true)");
        }

        private void соцРаботаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditFind(@"SELECT DISTINCT spec.id
                       FROM spec RIGHT JOIN results ON spec.id=results.id_spec
                       WHERE (is_present=true)");
        }

        private void KartForm_Load(object sender, EventArgs e)
        {
            ((MainForm)this.MdiParent).statusStrip.Items.Clear();

            toolStripProgressBar = new ToolStripProgressBar();
            toolStripProgressBar.Enabled = false;
            toolStripStatusLabel = new ToolStripStatusLabel();
            ((MainForm)this.MdiParent).statusStrip.Items.Add(toolStripProgressBar);
            ((MainForm)this.MdiParent).statusStrip.Items.Add(toolStripStatusLabel);

            backgroundWorker = new BackgroundWorker();
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
            backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
            backgroundWorker.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker1_ProgressChanged);

            toolStripProgressBar.Enabled = true;
            backgroundWorker.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            backgroundWorker.ReportProgress(0, "Загрузка данных...");

            newDBcon = new MainDBConnect(IVRShared.GetDBPath());

            IsPresentView = new DataView(newDBcon.GetDataTable("spec"));
            IsPresentView.RowFilter = "is_present = true";

            backgroundWorker.ReportProgress(100, "Загрузка завершена!");
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            toolStripProgressBar.Value = e.ProgressPercentage;
            toolStripStatusLabel.Text = e.UserState as String;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            toolStripProgressBar.Enabled = false;
            dgListPerson.DataSource = IsPresentView;

            SetRights();

            SQLDBConnect sqlCon = new SQLDBConnect();
            String dep = sqlCon.GetSystemValue("Department");
            DataTable dt = sqlCon.GetDataTable("spr_party_number", @"SELECT * FROM spr_party_number WHERE department_id='" + dep + @"'");
            cbParty.DataSource = dt;
                   
            cbParty.SelectedIndex = -1;

            labelHelp.Text = "Загружено записей: " + dgListPerson.RowCount;

            ((MainForm)this.MdiParent).statusStrip.Items.Clear();
        }

        private void KartForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Не закрываем форму пока воркер не отработает (иначе - эксепшн!)
            if (backgroundWorker.IsBusy)
                e.Cancel = true;
        }

    }
}