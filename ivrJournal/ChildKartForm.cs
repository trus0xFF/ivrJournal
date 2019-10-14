using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
//using System.Globalization;

namespace ivrJournal
{

    public partial class ChildKartForm : Form
    {
        private ChildDBConnect dbCon;
        private SprDbConnect newDBcon;
        private DataRow selRow;
        private Int32 idSpec;
        private String nameTable;
        private String namePunkt;
        private TextBox textbox1 = new TextBox();
        private SQLDBConnect sqlCon = new SQLDBConnect();
        private ArrayList idList = new ArrayList();
        
        public ChildKartForm()
        {
            InitializeComponent();
            dgChild.AutoGenerateColumns = false;
        }

        public ChildKartForm(DataRow selRow, String nameForm, String punktText)
        {
            InitializeComponent();
            dgChild.AutoGenerateColumns = false;
            this.selRow = selRow;
            idSpec = (Int32)selRow["id"];
            nameTable = "ivr";
            namePunkt = punktText;

            LoadIVR(punktText);

//            CheckData("Окончание ChildKartForm");

            textbox1.Visible = false;
            this.Controls.Remove(textbox1);
            textbox1.Multiline = true;
            textbox1.BorderStyle = BorderStyle.Fixed3D;

            //            textbox1.KeyDown += new KeyEventHandler(OnKeyUp); Не проходит пришлось переопределять grid

            //            textbox1.ScrollBars = ScrollBars.Vertical;
            dgChild.Controls.Add(textbox1);        
        
        }

        private void CheckData(string str)
        {
            MessageBox.Show(str);
            DataTable dt = ((DataView)dgChild.DataSource).Table;
            foreach (DataRow row in dt.Rows)
            {
                MessageBox.Show(" 1. " + row[1].ToString() + " 2. " + row[2].ToString() + " 3. " + row[3].ToString());
            }
        }

        public ChildKartForm(DataRow selRow, String nameForm)
        {
            InitializeComponent();
            dgChild.AutoGenerateColumns = false;

            this.selRow = selRow;
            idSpec = (Int32)selRow["id"];

            switch (nameForm)
            {
                case "party":
                    nameTable = "party";
                    LoadParty();
                    break;
                case "profilact_ychet":
                    nameTable = "profilact_ychet";
                    LoadProfYchet();
                    break;
                case "relations":
                    nameTable = "relations";
                    LoadRelations();
                    break;
                case "bonus":
                    nameTable = "bonus";
                    LoadBonus();
                    break;
                case "penalty":
                    nameTable = "penalty";
                    LoadPenalty();
                    break;
                case "prev_conv":
                    nameTable = "prev_conv";
                    LoadPrev_conv();
                    break;
                case "psycho_char":
                    nameTable = "psycho_char";
                    LoadPsycho_char();
                    break;
                case "resolution":
                    nameTable = "resolution";
                    LoadResolution();
                    break;
                case "results":
                    nameTable = "results";
                    LoadResults();
                    break;
                default:
                    MessageBox.Show("Задан неверный параметр для формы. Окно будет закрыто!");
                    Close();
                    break;
            }

            textbox1.Visible = false;
            this.Controls.Remove(textbox1);
            textbox1.Multiline = true;
            textbox1.BorderStyle = BorderStyle.Fixed3D;

            //            textbox1.KeyDown += new KeyEventHandler(OnKeyUp); Не проходит пришлось переопределять grid

            //            textbox1.ScrollBars = ScrollBars.Vertical;
            dgChild.Controls.Add(textbox1);
        }

        private void LoadIVR(String punktText)
        {

            switch (punktText)
            {
                case "1":
                    if (Convert.IsDBNull(selRow["birthdate"]))
                        this.labelHelp.Text = "ИВР, проводимая иными сотрудниками с: " + selRow["last_name"].ToString().Trim() + " " + selRow["first_name"].ToString().Trim() + " " + selRow["patronymic"].ToString().Trim();
                    else
                        this.labelHelp.Text = "ИВР, проводимая иными сотрудниками с: " + selRow["last_name"].ToString().Trim() + " " + selRow["first_name"].ToString().Trim() + " " + selRow["patronymic"].ToString().Trim() + " д.р. " + ((DateTime)selRow["birthdate"]).ToString("D");
                    this.Text = "ИВР, проводимая начальником отряда";
                    break;
                case "2":
                    if (Convert.IsDBNull(selRow["birthdate"]))
                        this.labelHelp.Text = "ИВР, проводимая членами совета воспитателей отряда с: " + selRow["last_name"].ToString().Trim() + " " + selRow["first_name"].ToString().Trim() + " " + selRow["patronymic"].ToString().Trim();
                    else
                        this.labelHelp.Text = "ИВР, проводимая членами совета воспитателей отряда с: " + selRow["last_name"].ToString().Trim() + " " + selRow["first_name"].ToString().Trim() + " " + selRow["patronymic"].ToString().Trim() + " д.р. " + ((DateTime)selRow["birthdate"]).ToString("D");
                    this.Text = "ИВР, проводимая членами совета воспитателей отряда";
                    break;
                case "3":
                    if (Convert.IsDBNull(selRow["birthdate"]))
                        this.labelHelp.Text = "ИВР, проводимая иными сотрудниками с: " + selRow["last_name"].ToString().Trim() + " " + selRow["first_name"].ToString().Trim() + " " + selRow["patronymic"].ToString().Trim();
                    else
                        this.labelHelp.Text = "ИВР, проводимая иными сотрудниками с: " + selRow["last_name"].ToString().Trim() + " " + selRow["first_name"].ToString().Trim() + " " + selRow["patronymic"].ToString().Trim() + " д.р. " + ((DateTime)selRow["birthdate"]).ToString("D");
                    this.Text = "ИВР, проводимая иными сотрудниками";
                    break;
            }

            dbCon = new ChildDBConnect(idSpec, "ivr", punktText);
            newDBcon = new SprDbConnect("spr_work_type", "employee");

            dgChild.Columns.Add(new DataGridViewTextBoxColumn());
            dgChild.Columns[0].DataPropertyName = "id";
            dgChild.Columns[0].HeaderText = "код";
            dgChild.Columns[0].Visible = false;

            dgChild.Columns.Add(new DataGridViewTextBoxColumn());
            dgChild.Columns[1].DataPropertyName = "id_spec";
            dgChild.Columns[1].HeaderText = "код";
            dgChild.Columns[1].Visible = false;

            dgChild.Columns.Add(new DataGridViewTextBoxColumn());
            dgChild.Columns[2].DataPropertyName = "id_type_ivr";
            dgChild.Columns[2].Name = "id_type_ivr";
            dgChild.Columns[2].HeaderText = "Тип";
            dgChild.Columns[2].Visible = false;

            CalendarColumn col = new CalendarColumn();
            {
                col.DataPropertyName = "data_ivr";
                col.HeaderText = "Дата проведения";
                col.Width = 90;
                col.MinimumWidth = 90;
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                col.SortMode = DataGridViewColumnSortMode.Automatic;
            }
            dgChild.Columns.Add(col);

            String departmentID = sqlCon.GetSystemValue("Department");

            DataGridViewComboBoxColumn column = new DataGridViewComboBoxColumn();
            {
                column.DataPropertyName = "employee_id";
                column.HeaderText = "Сотрудник";
                column.DropDownWidth = 250;
                column.Width = 90;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.MaxDropDownItems = 10;
                column.FlatStyle = FlatStyle.Flat;

                DataView dvEmp = new DataView(sqlCon.GetDataTable("employee", "SELECT id, department_id, last_name + ' ' + first_name + ' ' + patronymic as name FROM employee"));
                column.DataSource = dvEmp;
                dvEmp.RowFilter = "department_id=" + departmentID;

                //column.DataSource = sqlCon.GetDataTable("employee", "SELECT id, department_id, last_name + ' ' + first_name + ' ' + patronymic as name FROM employee");
                column.ValueMember = "id";
                column.DisplayMember = "name";
            }
            dgChild.Columns.Add(column);


            DataGridViewTextBoxColumn textColumn = new DataGridViewTextBoxColumn();
            {
                textColumn.DataPropertyName = "content";
                textColumn.HeaderText = "Содержание ИВР";
                textColumn.Width = 120;
                textColumn.MinimumWidth = 150;
                textColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            dgChild.Columns.Add(textColumn);

            column = new DataGridViewComboBoxColumn();
            {
                column.DataPropertyName = "work_type";
                column.HeaderText = "Вид воспитательной работы";
                column.DropDownWidth = 250;
                column.Width = 90;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.MaxDropDownItems = 10;
                column.FlatStyle = FlatStyle.Flat;

                column.DataSource = newDBcon.GetDataTable("spr_work_type");
                column.ValueMember = "id";
                column.DisplayMember = "name";
            }
            dgChild.Columns.Add(column);

            textColumn = new DataGridViewTextBoxColumn();
            {
                textColumn.DataPropertyName = "description";
                textColumn.HeaderText = "Примечание";
                textColumn.Width = 120;
                textColumn.MinimumWidth = 150;
                textColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            dgChild.Columns.Add(textColumn);
            
            DataView dv = new DataView(dbCon.GetDataTable("ivr"));
            dv.Sort = "id";
            dgChild.DataSource = dv;
            //dgChild.DataSource = dbCon.GetDataTable("ivr");
        }

        private void LoadResolution()
        {
//            DateTimeFormatInfo fmt = (new CultureInfo("ru-RU")).DateTimeFormat;

            if (Convert.IsDBNull(selRow["birthdate"]))
                this.labelHelp.Text = "Решения совета воспитателей отряда: " + selRow["last_name"].ToString().Trim() + " " + selRow["first_name"].ToString().Trim() + " " + selRow["patronymic"].ToString().Trim();
            else
                this.labelHelp.Text = "Решения совета воспитателей отряда: " + selRow["last_name"].ToString().Trim() + " " + selRow["first_name"].ToString().Trim() + " " + selRow["patronymic"].ToString().Trim() + " д.р. " + ((DateTime)selRow["birthdate"]).ToString("D");
            this.Text = "Решения совета воспитателей отряда";

            dbCon = new ChildDBConnect(idSpec, "resolution");

            dgChild.Columns.Add(new DataGridViewTextBoxColumn());
            dgChild.Columns[0].DataPropertyName = "id";
            dgChild.Columns[0].HeaderText = "код";
            dgChild.Columns[0].Visible = false;

            dgChild.Columns.Add(new DataGridViewTextBoxColumn());
            dgChild.Columns[1].DataPropertyName = "id_spec";
            dgChild.Columns[1].HeaderText = "код";
            dgChild.Columns[1].Visible = false;

             CalendarColumn col = new CalendarColumn();
            {
                col.DataPropertyName = "date_resolution";
                col.HeaderText = "Дата заседания СВО";
                col.Width = 90;
                col.MinimumWidth = 90;
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                col.SortMode = DataGridViewColumnSortMode.Automatic;
            }
            dgChild.Columns.Add(col);

            DataGridViewTextBoxColumn textColumn = new DataGridViewTextBoxColumn();
            {
                textColumn.DataPropertyName = "resolution";
                textColumn.HeaderText = "Решение СВО";
                textColumn.Width = 150;
                textColumn.MinimumWidth = 200;
                textColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            dgChild.Columns.Add(textColumn);

            textColumn = new DataGridViewTextBoxColumn();
            {
                textColumn.DataPropertyName = "description";
                textColumn.HeaderText = "Примечание";
                textColumn.Width = 100;
                textColumn.MinimumWidth = 130;
                textColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            dgChild.Columns.Add(textColumn);

            DataView dv = new DataView(dbCon.GetDataTable("resolution"));
            dv.Sort = "id";
            dgChild.DataSource = dv;
            //dgChild.DataSource = dbCon.GetDataTable("resolution");
        }


        private void LoadPsycho_char()
        {
            if (Convert.IsDBNull(selRow["birthdate"]))
                this.labelHelp.Text = "Индивидуально-психологические особенности: " + selRow["last_name"].ToString().Trim() + " " + selRow["first_name"].ToString().Trim() + " " + selRow["patronymic"].ToString().Trim();
            else
                this.labelHelp.Text = "Индивидуально-психологические особенности: " + selRow["last_name"].ToString().Trim() + " " + selRow["first_name"].ToString().Trim() + " " + selRow["patronymic"].ToString().Trim() + " д.р. " + ((DateTime)selRow["birthdate"]).ToString("D");
            this.Text = "Индивидуально-психологические особенности";

            dbCon = new ChildDBConnect(idSpec, "psycho_char");

            dgChild.Columns.Add(new DataGridViewTextBoxColumn());
            dgChild.Columns[0].DataPropertyName = "id";
            dgChild.Columns[0].HeaderText = "код";
            dgChild.Columns[0].Visible = false;

            dgChild.Columns.Add(new DataGridViewTextBoxColumn());
            dgChild.Columns[1].DataPropertyName = "id_spec";
            dgChild.Columns[1].HeaderText = "код";
            dgChild.Columns[1].Visible = false;

            CalendarColumn col = new CalendarColumn();
            {
                col.DataPropertyName = "date_meet";
                col.HeaderText = "Дата проведения";
                col.Width = 90;
                col.MinimumWidth = 90;
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                col.SortMode = DataGridViewColumnSortMode.Automatic;
            }
            dgChild.Columns.Add(col);

            DataGridViewTextBoxColumn textColumn = new DataGridViewTextBoxColumn();
            {
//                textColumn.DataPropertyName = "orient";
                textColumn.DataPropertyName = "orientation";
                textColumn.HeaderText = "Направленность личности";
                textColumn.Width = 120;
                textColumn.MinimumWidth = 150;
                textColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            dgChild.Columns.Add(textColumn);

            textColumn = new DataGridViewTextBoxColumn();
            {
                textColumn.DataPropertyName = "psycho_char";
                textColumn.HeaderText = "Психологическая  характеристика ";
                textColumn.Width = 120;
                textColumn.MinimumWidth = 150;
                textColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            dgChild.Columns.Add(textColumn);

            textColumn = new DataGridViewTextBoxColumn();
            {
                textColumn.DataPropertyName = "behavior";
                textColumn.HeaderText = "Сведения о поведении ";
                textColumn.Width = 120;
                textColumn.MinimumWidth = 150;
                textColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            dgChild.Columns.Add(textColumn);

            DataView dv = new DataView(dbCon.GetDataTable("psycho_char"));
            dv.Sort = "id";
            dgChild.DataSource = dv;
            //dgChild.DataSource = dbCon.GetDataTable("psycho_char");
        }


        private void LoadPrev_conv()
        {
            if (Convert.IsDBNull(selRow["birthdate"]))
                this.labelHelp.Text = "Прежние судимости: " + selRow["last_name"].ToString().Trim() + " " + selRow["first_name"].ToString().Trim() + " " + selRow["patronymic"].ToString().Trim();
            else
                this.labelHelp.Text = "Прежние судимости: " + selRow["last_name"].ToString().Trim() + " " + selRow["first_name"].ToString().Trim() + " " + selRow["patronymic"].ToString().Trim() + " д.р. " + ((DateTime)selRow["birthdate"]).ToString("D");
            this.Text = "Прежние судимости";

            dbCon = new ChildDBConnect(idSpec, "prev_conv");
            newDBcon = new SprDbConnect("spr_release_reason");

            dgChild.Columns.Add(new DataGridViewTextBoxColumn());
            dgChild.Columns[0].DataPropertyName = "id";
            dgChild.Columns[0].HeaderText = "код";
            dgChild.Columns[0].Visible = false;

            dgChild.Columns.Add(new DataGridViewTextBoxColumn());
            dgChild.Columns[1].DataPropertyName = "id_spec";
            dgChild.Columns[1].HeaderText = "код";
            dgChild.Columns[1].Visible = false;

            CalendarColumn col = new CalendarColumn();
            {
                col.DataPropertyName = "start_date";
                col.HeaderText = "Дата начала срока";
                col.Width = 90;
                col.MinimumWidth = 90;
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                col.SortMode = DataGridViewColumnSortMode.Automatic;
            }
            dgChild.Columns.Add(col);

            DataGridViewTextBoxColumn textColumn = new DataGridViewTextBoxColumn();
            {
                textColumn.DataPropertyName = "period";
                textColumn.HeaderText = "Срок";
                textColumn.Width = 120;
                textColumn.MinimumWidth = 120;
                textColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            }
            dgChild.Columns.Add(textColumn);

            textColumn = new DataGridViewTextBoxColumn();
            {
                textColumn.DataPropertyName = "text_prev";
                textColumn.HeaderText = "Текст приговора";
                textColumn.Width = 150;
                textColumn.MinimumWidth = 100;
                textColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            dgChild.Columns.Add(textColumn);

            textColumn = new DataGridViewTextBoxColumn();
            {
                textColumn.DataPropertyName = "performer";
                textColumn.HeaderText = "Кем";
                textColumn.Width = 100;
                textColumn.MinimumWidth = 70;
                textColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            dgChild.Columns.Add(textColumn);

            textColumn = new DataGridViewTextBoxColumn();
            {
                textColumn.DataPropertyName = "article";
                textColumn.HeaderText = "Статья";
                textColumn.Width = 100;
                textColumn.MinimumWidth = 70;
                textColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            dgChild.Columns.Add(textColumn);

            col = new CalendarColumn();
            {
                col.DataPropertyName = "release_date";
                col.HeaderText = "Дата освобождения";
                col.Width = 90;
                col.MinimumWidth = 90;
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            }
            dgChild.Columns.Add(col);

            DataGridViewComboBoxColumn column =
             new DataGridViewComboBoxColumn();
            {
                column.DataPropertyName = "release_reason_id";
                column.HeaderText = "Причина освобождения";
                column.DropDownWidth = 250;
                column.Width = 90;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                column.MaxDropDownItems = 10;
                column.FlatStyle = FlatStyle.Flat;

                column.DataSource = newDBcon.GetDataTable("spr_release_reason");
                column.ValueMember = "id";
                column.DisplayMember = "name";
            }
            dgChild.Columns.Add(column);

            dgChild.AutoGenerateColumns = false;

            DataView dv = new DataView(dbCon.GetDataTable("prev_conv"));
            dv.Sort = "start_date";
//            dv.Sort = "id";
            dgChild.DataSource = dv;
            //dgChild.DataSource = dbCon.GetDataTable("prev_conv");

        }


        private void LoadPenalty()
        {
            if (Convert.IsDBNull(selRow["birthdate"]))
                this.labelHelp.Text = "Взыскания: " + selRow["last_name"].ToString().Trim() + " " + selRow["first_name"].ToString().Trim() + " " + selRow["patronymic"].ToString().Trim();
            else
                this.labelHelp.Text = "Взыскания: " + selRow["last_name"].ToString().Trim() + " " + selRow["first_name"].ToString().Trim() + " " + selRow["patronymic"].ToString().Trim() + " д.р. " + ((DateTime)selRow["birthdate"]).ToString("D");
            this.Text = "Взыскания";

            dbCon = new ChildDBConnect(idSpec, "penalty");
            newDBcon = new SprDbConnect("spr_penalty_type", "spr_performers");

            dgChild.Columns.Add(new DataGridViewTextBoxColumn());
            dgChild.Columns[0].DataPropertyName = "id";
            dgChild.Columns[0].HeaderText = "код";
            dgChild.Columns[0].Visible = false;

            dgChild.Columns.Add(new DataGridViewTextBoxColumn());
            dgChild.Columns[1].DataPropertyName = "id_spec";
            dgChild.Columns[1].HeaderText = "код";
            dgChild.Columns[1].Visible = false;

            DataGridViewCheckBoxColumn columnCheck =
                new DataGridViewCheckBoxColumn();
            {
                columnCheck.DataPropertyName = "oral";
                columnCheck.HeaderText = "Отметка, что взыскание устное";
                columnCheck.Width = 60;
                columnCheck.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            }
            dgChild.Columns.Add(columnCheck);

            DataGridViewComboBoxColumn column =
             new DataGridViewComboBoxColumn();
            {
                column.DataPropertyName = "penalty_type_id";
                column.HeaderText = "Вид взыскания";
                column.DropDownWidth = 250;
                column.Width = 130;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                column.MaxDropDownItems = 10;
                column.FlatStyle = FlatStyle.Flat;

                column.DataSource = newDBcon.GetDataTable("spr_penalty_type");
                column.ValueMember = "id";
                column.DisplayMember = "name";
                column.SortMode = DataGridViewColumnSortMode.Automatic;
            }
            dgChild.Columns.Add(column);

            dgChild.Columns.Add(new DataGridViewTextBoxColumn());
            dgChild.Columns[4].DataPropertyName = "reason";
            dgChild.Columns[4].HeaderText = "За что объявлено взыскание";
            dgChild.Columns[4].Width = 100;
            dgChild.Columns[4].MinimumWidth = 100;
            dgChild.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            CalendarColumn col = new CalendarColumn();
            {
                col.DataPropertyName = "date_penalty";
                col.HeaderText = "Дата наложения взыскания";
                col.Width = 90;
                col.MinimumWidth = 90;
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                col.SortMode = DataGridViewColumnSortMode.Automatic;
            }
            dgChild.Columns.Add(col);

            column =
             new DataGridViewComboBoxColumn();
            {
                column.DataPropertyName = "performer_id";
                column.HeaderText = "Кем наложено взыскание";
                column.DropDownWidth = 150;
                column.Width = 130;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                column.MaxDropDownItems = 3;
                column.FlatStyle = FlatStyle.Flat;

                column.DataSource = newDBcon.GetDataTable("spr_performers");
                column.ValueMember = "id";
                column.DisplayMember = "name";
            }
            dgChild.Columns.Add(column);

            dgChild.Columns.Add(new DataGridViewTextBoxColumn());
            dgChild.Columns[7].DataPropertyName = "order_number";
            dgChild.Columns[7].HeaderText = "Номер приказа";
            dgChild.Columns[7].Width = 100;
            dgChild.Columns[7].MinimumWidth = 100;
            dgChild.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            col = new CalendarColumn();
            {
                col.DataPropertyName = "order_date";
                col.HeaderText = "Дата приказа";
                col.Width = 90;
                col.MinimumWidth = 90;
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                col.SortMode = DataGridViewColumnSortMode.Automatic;
            }
            dgChild.Columns.Add(col);

            dgChild.Columns.Add(new DataGridViewTextBoxColumn());
            dgChild.Columns[9].DataPropertyName = "removal";
            dgChild.Columns[9].HeaderText = "Отметка о снятии взыскания";
            dgChild.Columns[9].Width = 100;
            dgChild.Columns[9].MinimumWidth = 100;
            dgChild.Columns[9].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            DataView dv = new DataView(dbCon.GetDataTable("penalty"));
            dv.Sort = "id";
            dgChild.DataSource = dv;
            //dgChild.DataSource = dbCon.GetDataTable("penalty");

        }


        private void LoadBonus()
        {
            if (Convert.IsDBNull(selRow["birthdate"]))
                this.labelHelp.Text = "Поощрения: " + selRow["last_name"].ToString().Trim() + " " + selRow["first_name"].ToString().Trim() + " " + selRow["patronymic"].ToString().Trim();
            else
                this.labelHelp.Text = "Поощрения: " + selRow["last_name"].ToString().Trim() + " " + selRow["first_name"].ToString().Trim() + " " + selRow["patronymic"].ToString().Trim() + " д.р. " + ((DateTime)selRow["birthdate"]).ToString("D");
            this.Text = "Поощрения";

            dbCon = new ChildDBConnect(idSpec, "bonus");
            newDBcon = new SprDbConnect("spr_bonus_type", "spr_performers");

            dgChild.Columns.Add(new DataGridViewTextBoxColumn());
            dgChild.Columns[0].DataPropertyName = "id";
            dgChild.Columns[0].HeaderText = "код";
            dgChild.Columns[0].Visible = false;

            dgChild.Columns.Add(new DataGridViewTextBoxColumn());
            dgChild.Columns[1].DataPropertyName = "id_spec";
            dgChild.Columns[1].HeaderText = "код";
            dgChild.Columns[1].Visible = false;

            DataGridViewComboBoxColumn column =
             new DataGridViewComboBoxColumn();
            {
                column.DataPropertyName = "bonus_type_id";
                column.HeaderText = "Вид поощрения";
                column.DropDownWidth = 250;
                column.Width = 130;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                column.MaxDropDownItems = 10;
                column.FlatStyle = FlatStyle.Flat;

                column.DataSource = newDBcon.GetDataTable("spr_bonus_type");
                column.ValueMember = "id";
                column.DisplayMember = "name";
            }
            dgChild.Columns.Add(column);

            dgChild.Columns.Add(new DataGridViewTextBoxColumn());
            dgChild.Columns[3].DataPropertyName = "bonus_reason";
            dgChild.Columns[3].HeaderText = "За что объявлено поощрение";
            dgChild.Columns[3].Width = 100;
            dgChild.Columns[3].MinimumWidth = 100;
            dgChild.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            CalendarColumn col = new CalendarColumn();
            {
                col.DataPropertyName = "date_bonus";
                col.HeaderText = "Дата поощрения";
                col.Width = 90;
                col.MinimumWidth = 90;
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                col.SortMode = DataGridViewColumnSortMode.Automatic;
            }
            dgChild.Columns.Add(col);

            column =
             new DataGridViewComboBoxColumn();
            {
                column.DataPropertyName = "performer_id";
                column.HeaderText = "Кем поощрен";
                column.DropDownWidth = 150;
                column.Width = 130;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                column.MaxDropDownItems = 3;
                column.FlatStyle = FlatStyle.Flat;

                column.DataSource = newDBcon.GetDataTable("spr_performers");
                column.ValueMember = "id";
                column.DisplayMember = "name";
            }
            dgChild.Columns.Add(column);

            dgChild.Columns.Add(new DataGridViewTextBoxColumn());
            dgChild.Columns[6].DataPropertyName = "order_number";
            dgChild.Columns[6].HeaderText = "Номер приказа";
            dgChild.Columns[6].Width = 90;
            dgChild.Columns[6].MinimumWidth = 90;
            dgChild.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            col = new CalendarColumn();
            {
                col.DataPropertyName = "order_date";
                col.HeaderText = "Дата приказа";
                col.Width = 90;
                col.MinimumWidth = 90;
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            }
            dgChild.Columns.Add(col);

            DataView dv = new DataView(dbCon.GetDataTable("bonus"));
            dv.Sort = "id";
            dgChild.DataSource = dv;
            //dgChild.DataSource = dbCon.GetDataTable("bonus");
        }


        private void LoadRelations()
        {
            if (Convert.IsDBNull(selRow["birthdate"]))
                this.labelHelp.Text = "Родственники: " + selRow["last_name"].ToString().Trim() + " " + selRow["first_name"].ToString().Trim() + " " + selRow["patronymic"].ToString().Trim();
            else
                this.labelHelp.Text = "Родственники: " + selRow["last_name"].ToString().Trim() + " " + selRow["first_name"].ToString().Trim() + " " + selRow["patronymic"].ToString().Trim() + " д.р. " + ((DateTime)selRow["birthdate"]).ToString("D");
            this.Text = "Родственники";

            dbCon = new ChildDBConnect(idSpec, "relations");
            newDBcon = new SprDbConnect("spr_degree");

            dgChild.Columns.Add(new DataGridViewTextBoxColumn());
            dgChild.Columns[0].DataPropertyName = "id";
            dgChild.Columns[0].HeaderText = "код";
            dgChild.Columns[0].Visible = false;

            dgChild.Columns.Add(new DataGridViewTextBoxColumn());
            dgChild.Columns[1].DataPropertyName = "id_spec";
            dgChild.Columns[1].HeaderText = "код";
            dgChild.Columns[1].Visible = false;

            DataGridViewComboBoxColumn column =
             new DataGridViewComboBoxColumn();
            {
                column.DataPropertyName = "degree_id";
                column.HeaderText = "Степень родства";
                column.DropDownWidth = 100;
                column.Width = 90;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.MaxDropDownItems = 3;
                column.FlatStyle = FlatStyle.Flat;

                column.DataSource = newDBcon.GetDataTable("spr_degree");
                column.ValueMember = "id";
                column.DisplayMember = "name";
            }
            dgChild.Columns.Add(column);

            dgChild.Columns.Add(new DataGridViewTextBoxColumn());
            dgChild.Columns[3].DataPropertyName = "last_name";
            dgChild.Columns[3].HeaderText = "Фамилия";
            dgChild.Columns[3].Width = 100;
            dgChild.Columns[3].MinimumWidth = 100;
            dgChild.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgChild.Columns.Add(new DataGridViewTextBoxColumn());
            dgChild.Columns[4].DataPropertyName = "first_name";
            dgChild.Columns[4].HeaderText = "Имя";
            dgChild.Columns[4].Width = 100;
            dgChild.Columns[4].MinimumWidth = 100;
            dgChild.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgChild.Columns.Add(new DataGridViewTextBoxColumn());
            dgChild.Columns[5].DataPropertyName = "patronymic";
            dgChild.Columns[5].HeaderText = "Отчество";
            dgChild.Columns[5].Width = 100;
            dgChild.Columns[5].MinimumWidth = 100;
            dgChild.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            CalendarColumn col = new CalendarColumn();
            {
                col.DataPropertyName = "birthdate";
                col.HeaderText = "Дата рождения";
                col.Width = 90;
                col.MinimumWidth = 90;
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            }
            dgChild.Columns.Add(col);

            dgChild.Columns.Add(new DataGridViewTextBoxColumn());
            dgChild.Columns[7].DataPropertyName = "address";
            dgChild.Columns[7].HeaderText = "Адрес";
            dgChild.Columns[7].Width = 150;
            dgChild.Columns[7].MinimumWidth = 150;
            dgChild.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            DataView dv = new DataView(dbCon.GetDataTable("relations"));
            dv.Sort = "id";
            dgChild.DataSource = dv;
            //dgChild.DataSource = dbCon.GetDataTable("relations");

        }

        private void LoadResults()
        {
            if (Convert.IsDBNull(selRow["birthdate"]))
                this.labelHelp.Text = "Работа по: " + selRow["last_name"].ToString().Trim() + " " + selRow["first_name"].ToString().Trim() + " " + selRow["patronymic"].ToString().Trim();
            else
                this.labelHelp.Text = "Работа по: " + selRow["last_name"].ToString().Trim() + " " + selRow["first_name"].ToString().Trim() + " " + selRow["patronymic"].ToString().Trim() + " д.р. " + ((DateTime)selRow["birthdate"]).ToString("D");
            this.Text = "Работа по подготовке осужденного к освобождению";

            dbCon = new ChildDBConnect(idSpec, "results");
            //newDBcon = new SprDbConnect("spr_bonus_type", "spr_performers");

            dgChild.Columns.Add(new DataGridViewTextBoxColumn());
            dgChild.Columns[0].DataPropertyName = "id";
            dgChild.Columns[0].HeaderText = "код";
            dgChild.Columns[0].Visible = false;

            dgChild.Columns.Add(new DataGridViewTextBoxColumn());
            dgChild.Columns[1].DataPropertyName = "id_spec";
            dgChild.Columns[1].HeaderText = "код";
            dgChild.Columns[1].Visible = false;

            CalendarColumn col = new CalendarColumn();
            {
                col.DataPropertyName = "result_date";
                col.HeaderText = "Дата";
                col.Width = 90;
                col.MinimumWidth = 90;
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                col.SortMode = DataGridViewColumnSortMode.Automatic;
            }
            dgChild.Columns.Add(col);

            dgChild.Columns.Add(new DataGridViewTextBoxColumn());
            dgChild.Columns[3].DataPropertyName = "content";
            dgChild.Columns[3].HeaderText = "Содержание";
            dgChild.Columns[3].Width = 100;
            dgChild.Columns[3].MinimumWidth = 100;
            dgChild.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgChild.Columns.Add(new DataGridViewTextBoxColumn());
            dgChild.Columns[4].DataPropertyName = "description";
            dgChild.Columns[4].HeaderText = "Примечание";
            dgChild.Columns[4].Width = 100;
            dgChild.Columns[4].MinimumWidth = 100;
            dgChild.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            DataView dv = new DataView(dbCon.GetDataTable("results"));
            dv.Sort = "id";
            dgChild.DataSource = dv;
            //dgChild.DataSource = dbCon.GetDataTable("results");

        }

        private void LoadParty()
        {
            if (Convert.IsDBNull(selRow["birthdate"]))
                this.labelHelp.Text = "Перемещения по отрядам: " + selRow["last_name"].ToString().Trim() + " " + selRow["first_name"].ToString().Trim() + " " + selRow["patronymic"].ToString().Trim();
            else
                this.labelHelp.Text = "Перемещения по отрядам: " + selRow["last_name"].ToString().Trim() + " " + selRow["first_name"].ToString().Trim() + " " + selRow["patronymic"].ToString().Trim() + " д.р. " + ((DateTime)selRow["birthdate"]).ToString("D");
            this.Text = "Перемещения по отрядам";

            dbCon = new ChildDBConnect(idSpec, "party");
            newDBcon = new SprDbConnect("spr_party_number");

            dgChild.Columns.Add(new DataGridViewTextBoxColumn());
            dgChild.Columns[0].DataPropertyName = "id";
            dgChild.Columns[0].HeaderText = "код";
            dgChild.Columns[0].Visible = false;

            dgChild.Columns.Add(new DataGridViewTextBoxColumn());
            dgChild.Columns[1].DataPropertyName = "id_spec";
            dgChild.Columns[1].HeaderText = "код";
            dgChild.Columns[1].Visible = false;

            String departmentID = sqlCon.GetSystemValue("Department");

            DataGridViewComboBoxColumn column =
             new DataGridViewComboBoxColumn();
            {
                column.DataPropertyName = "party_number_id";
                column.HeaderText = "Отряд";
                //                column.DropDownWidth = 100;
                column.Width = 120;
                column.DropDownWidth = 150;
                column.MaxDropDownItems = 7;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.FlatStyle = FlatStyle.Flat;

                DataView dvParty = new DataView(sqlCon.GetDataTable("spr_party_number", "SELECT id, department_id, name FROM spr_party_number"));
                column.DataSource = dvParty;
                dvParty.RowFilter = "department_id=" + departmentID;

                //column.DataSource = sqlCon.GetDataTable("spr_party_number", "SELECT id, department_id, name FROM spr_party_number");

                column.ValueMember = "id";
                column.DisplayMember = "name";
            }
            dgChild.Columns.Add(column);

            CalendarColumn col = new CalendarColumn();
            {
                col.DataPropertyName = "arr_date";
                col.HeaderText = "Дата прибытия в ИУ (отряд)";
                col.Width = 90;
                col.MinimumWidth = 90;
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                col.SortMode = DataGridViewColumnSortMode.Automatic;
            }
            dgChild.Columns.Add(col);

            dgChild.Columns.Add(new DataGridViewTextBoxColumn());
            dgChild.Columns[4].DataPropertyName = "ord";
            dgChild.Columns[4].HeaderText = "Приказ о направлении в отряд";
            dgChild.Columns[4].Width = 150;
            dgChild.Columns[4].MinimumWidth = 150;
            dgChild.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgChild.Columns.Add(new DataGridViewTextBoxColumn());
            dgChild.Columns[5].DataPropertyName = "reason";
            dgChild.Columns[5].HeaderText = "Причины перевода";
            dgChild.Columns[5].Width = 150;
            dgChild.Columns[5].MinimumWidth = 150;
            dgChild.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            DataView dv = new DataView(dbCon.GetDataTable("party"));
            dv.Sort = "id";
            dgChild.DataSource = dv;
            //dgChild.DataSource = dbCon.GetDataTable("party");
        }

        private void LoadProfYchet()
        {
            if (Convert.IsDBNull(selRow["birthdate"]))
                this.labelHelp.Text = "Профилактический учет: " + selRow["last_name"].ToString().Trim() + " " + selRow["first_name"].ToString().Trim() + " " + selRow["patronymic"].ToString().Trim();
            else
                this.labelHelp.Text = "Профилактический учет: " + selRow["last_name"].ToString().Trim() + " " + selRow["first_name"].ToString().Trim() + " " + selRow["patronymic"].ToString().Trim() + " д.р. " + ((DateTime)selRow["birthdate"]).ToString("D");
            this.Text = "Профилактический учет";

            dbCon = new ChildDBConnect(idSpec, "profilact_ychet");
            newDBcon = new SprDbConnect("spr_profilact_ychet");

            dgChild.Columns.Add(new DataGridViewTextBoxColumn());
            dgChild.Columns[0].DataPropertyName = "id";
            dgChild.Columns[0].HeaderText = "код";
            dgChild.Columns[0].Visible = false;

            dgChild.Columns.Add(new DataGridViewTextBoxColumn());
            dgChild.Columns[1].DataPropertyName = "id_spec";
            dgChild.Columns[1].HeaderText = "код";
            dgChild.Columns[1].Visible = false;

            DataGridViewComboBoxColumn column =
             new DataGridViewComboBoxColumn();
            {
                column.DataPropertyName = "id_profilact_ychet";
                column.HeaderText = "Категория Учета";
                column.DropDownWidth = 400;
                column.Width = 250;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                column.MaxDropDownItems = 15;
                column.FlatStyle = FlatStyle.Flat;

                column.DataSource = newDBcon.GetDataTable("spr_profilact_ychet");
                column.ValueMember = "id";
                column.DisplayMember = "name";
            }
            dgChild.Columns.Add(column);

            CalendarColumn col = new CalendarColumn();
            {
                col.DataPropertyName = "data_post";
                col.HeaderText = "Дата постановки";
                col.Width = 90;
                col.MinimumWidth = 90;
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                col.SortMode = DataGridViewColumnSortMode.Automatic;
            }
            dgChild.Columns.Add(col);

            col = new CalendarColumn();
            {
                col.DataPropertyName = "data_snyat";
                col.HeaderText = "Дата снятия";
                col.Width = 90;
                col.MinimumWidth = 90;
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            }
            dgChild.Columns.Add(col);

            DataView dv = new DataView(dbCon.GetDataTable("profilact_ychet"));
            dv.Sort = "id";
            dgChild.DataSource = dv;
            //dgChild.DataSource = dbCon.GetDataTable("profilact_ychet");
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dgChild_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
//            CheckData("Начало UserAddedRow");
            dgChild.CurrentRow.Cells[1].Value = "" + idSpec;
            if (nameTable == "ivr")
                dgChild.CurrentRow.Cells["id_type_ivr"].Value = "" + namePunkt;
//            CheckData("Окончание UserAddedRow");
 
        }

        private void ChildKartForm_FormClosed(object sender, FormClosedEventArgs e)
        {
//            CheckData("НАчало FormClosed");
            dbCon.UpdateDataTable(idSpec, nameTable);
//            CheckData("Окончание FormClosed");
        }

        private void dgChild_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
//            CheckData("НАчало CellEndEdit");
            if ((dgChild.Columns[dgChild.CurrentCell.ColumnIndex].DataPropertyName == "content") | (dgChild.Columns[dgChild.CurrentCell.ColumnIndex].DataPropertyName == "orientation") | (dgChild.Columns[dgChild.CurrentCell.ColumnIndex].DataPropertyName == "psycho_char") | (dgChild.Columns[dgChild.CurrentCell.ColumnIndex].DataPropertyName == "behavior"))
                if (textbox1.Visible == true)
                {
                    if (dgChild.CurrentRow.Index == dgChild.RowCount - 1)
                    {
                        dgChild.CurrentRow.Cells[1].Value = "" + idSpec;
                        if (nameTable == "ivr")
                            dgChild.CurrentRow.Cells["id_type_ivr"].Value = "" + namePunkt;
                    }

                    dgChild.CurrentCell.Value = textbox1.Text;
                    textbox1.Visible = false;
                }
//            CheckData("Окончание CellEndEdit");
            //            dbCon.UpdateDataTable(idSpec, nameTable);
        }

        private void dgChild_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //Исправление ошибки в программе на ввод даты через календарь
        }

        private void tsbRefresh_Click(object sender, EventArgs e)
        {
            dbCon.RefreshDataTable(idSpec, nameTable);
        }

        private void dgChild_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
// Разобраться с дизайном dg
             // получаем текущий элемент DataGridView
            DataGridView grid = sender as DataGridView;
            if (grid == null)
                return;

            //четная - нечетная
            
            grid.Rows[e.RowIndex].DefaultCellStyle.BackColor = (e.RowIndex % 2 == 1) ?
                Color.LemonChiffon : Color.WhiteSmoke;
            /*
            try
            {
                DataRow row = ((DataView)grid.DataSource)[e.RowIndex].Row;
                if (row.RowState != DataRowState.Unchanged)
                    grid.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Aquamarine;
            }
            catch (IndexOutOfRangeException)
            {
            }
             */
           
        }

        private void setfocus()
        {
            this.textbox1.Focus();
        }

        private void dgChild_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {

            if (e.Control is DataGridViewTextBoxEditingControl)
            {
                DataGridViewTextBoxEditingControl cellEdit = (DataGridViewTextBoxEditingControl)e.Control;
                cellEdit.WordWrap = true;
                cellEdit.Multiline = true;
                e.CellStyle.BackColor = Color.FloralWhite;
            }

            if ((dgChild.Columns[dgChild.CurrentCell.ColumnIndex].DataPropertyName == "content") | (dgChild.Columns[dgChild.CurrentCell.ColumnIndex].DataPropertyName == "orientation") | (dgChild.Columns[dgChild.CurrentCell.ColumnIndex].DataPropertyName == "psycho_char") | (dgChild.Columns[dgChild.CurrentCell.ColumnIndex].DataPropertyName == "behavior"))
            {
                this.BeginInvoke(new MethodInvoker(setfocus));
            }


            //Выбор из списка при начале набора текста + смена цвета ячейки
/*
            if (dgChild.Columns[dgChild.CurrentCell.ColumnIndex].DataPropertyName == "reason")
            {
                if (e.Control is DataGridViewTextBoxEditingControl)
                {
                    // Перехват управления редатирования ячейки как textBox

                    DataGridViewTextBoxEditingControl cellEdit = (DataGridViewTextBoxEditingControl)e.Control;
                    //                cellEdit.WordWrap = true;
                    //                cellEdit.Multiline = true;

                    e.CellStyle.BackColor = Color.Silver;

                    cellEdit.AutoCompleteMode = AutoCompleteMode.Append;
                    cellEdit.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    cellEdit.AutoCompleteCustomSource.AddRange(new String[]
                        {
                            "красный",
                            "оранжевый",
                            "желтый",
                            "зеленый",
                            "голубой",
                            "синий",
                            "фиолетовый"
                        });
                }
            }
 */
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {
            if ( !IVRShared.IsCurrentUserAdmin() )
            {
                MessageBox.Show("Удалять записи может только Пользователь с правами Администратора!");
                return;
            }

            int index = dgChild.CurrentRow.Index;
            if ((index != -1) & (index != dgChild.NewRowIndex))
            {
                try
                {
                    dgChild.Rows.RemoveAt(index);
                    dbCon.UpdateDataTable(idSpec, nameTable);
                }
                catch
                {
                        MessageBox.Show("Запись в базе не обнаружена!!!", "Сообщение о базе", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
        }

        private void tsbEdit_Click(object sender, EventArgs e)
        {
            int index = dgChild.CurrentRow.Index;
            if (index == -1)
                return;

            //разрешаем редактировать, если пользователь - Администратор
            if (IVRShared.IsCurrentUserAdmin())
            {
                dgChild.BeginEdit(true);
                return;
            }

            DataRow row = ((DataView)dgChild.DataSource)[index].Row;
            if (row.RowState == DataRowState.Unchanged)
                return;

            dgChild.BeginEdit(true);
        }

        private void tsbNew_Click(object sender, EventArgs e)
        {
            //DataView dv = (DataView)dgChild.DataSource;
            dgChild.CurrentCell = dgChild[2, dgChild.NewRowIndex];
            //dgChild.Focus();
            dgChild.BeginEdit(true);
            
        }

        private void dgChild_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (!IVRShared.IsCurrentUserAdmin())
            {
                MessageBox.Show("Удалять записи может только Пользователь с правами Администратора!");
                e.Cancel = true;
            }
            else
                dbCon.UpdateDataTable(idSpec, nameTable);


        }

        private void dgChild_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if ((dgChild.Columns[dgChild.CurrentCell.ColumnIndex].DataPropertyName == "content") | (dgChild.Columns[dgChild.CurrentCell.ColumnIndex].DataPropertyName == "orientation") | (dgChild.Columns[dgChild.CurrentCell.ColumnIndex].DataPropertyName == "psycho_char") | (dgChild.Columns[dgChild.CurrentCell.ColumnIndex].DataPropertyName == "behavior"))
            {
                Rectangle rect = dgChild.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                textbox1.Location = rect.Location;
                textbox1.Width = rect.Width;
                textbox1.Height = dgChild.Height - rect.Y;
                textbox1.Text = dgChild.CurrentCell.Value.ToString();
                textbox1.Multiline = true;
                textbox1.Visible = true;
            }
        }

        public void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Shift && e.KeyCode == Keys.Enter)
            {
//                MessageBox.Show("Uraaa!!!!!");
            }
        } 

        private void ChildKartForm_Load(object sender, EventArgs e)
        {

        }

        private void dgChild_Scroll(object sender, ScrollEventArgs e)
        {
            if (textbox1.Visible == true)
            {
                Rectangle r  = dgChild.GetCellDisplayRectangle(dgChild.CurrentCell.ColumnIndex, dgChild.CurrentCell.RowIndex, true);
                textbox1.Location = r.Location;
                textbox1.Width = r.Width;
                textbox1.Height = dgChild.Height - r.Y;
            }
        }
        
        private void dgChild_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Shift && e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                dgChild.BeginEdit(true);
            }
        }

        private void ChildKartForm_Resize(object sender, EventArgs e)
        {
            if (textbox1.Visible == true)
            {
                Rectangle r = dgChild.GetCellDisplayRectangle(dgChild.CurrentCell.ColumnIndex, dgChild.CurrentCell.RowIndex, true);
                textbox1.Location = r.Location;
                textbox1.Width = r.Width;
                textbox1.Height = dgChild.Height - r.Y;
            }
        }

        private void ChildKartForm_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void tsbCopy_Click(object sender, EventArgs e)
        {
            int index = dgChild.CurrentRow.Index;
            if ((index == -1) | (index == dgChild.NewRowIndex))
                return;

            //DataTable table = ((DataTable)dgChild.DataSource);
            DataTable table = ((DataView)dgChild.DataSource).Table;
            
            //DataRow row = ((DataTable)dgChild.DataSource).Rows[index];
            DataRow row = ((DataView)dgChild.DataSource)[index].Row;

            DataRow rowNew = table.Rows.Add();

            foreach (DataColumn column in table.Columns)
            {
                rowNew[column] = row[column];
            }

            //dbCon.UpdateDataTable(idSpec, nameTable);

            dgChild.Focus();
            dgChild.BeginEdit(true);
            dgChild.CurrentCell = dgChild[2, dgChild.NewRowIndex-1];


        }

        private void dgChild_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            int index = dgChild.CurrentRow.Index;

            if (index == 0)
                return;

            //разрешаем редактировать, если пользователь - Администратор
            if (IVRShared.IsCurrentUserAdmin())
            {
                dgChild.BeginEdit(true);
                return;
            }

            DataRow row = ((DataView)dgChild.DataSource)[index].Row;
            if (row.RowState == DataRowState.Unchanged)
                return;

            dgChild.BeginEdit(true);
        }

        private void dgChild_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = dgChild.CurrentRow.Index;

            //разрешаем редактировать, если пользователь - Администратор
            if (IVRShared.IsCurrentUserAdmin())
            {
                dgChild.BeginEdit(true);
                return;
            }

            DataRow row = ((DataView)dgChild.DataSource)[index].Row;
            if (row.RowState == DataRowState.Unchanged)
                return;

            dgChild.BeginEdit(true);
        }


    }
}