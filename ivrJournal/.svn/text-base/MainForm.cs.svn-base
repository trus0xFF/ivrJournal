using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Checksums;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Threading;
using System.Globalization;
using System.Xml;
using System.Security.Cryptography;
//using System.Security.Cryptography.Xml;

namespace ivrJournal
{
    public partial class MainForm : Form
    {
        private int childFormNumber = 0;
//        public DataRow userRow;
        public string dbPath;
        //public string departmentID;
        private ToolStripProgressBar toolStripProgressBar;
        private BackgroundWorker backgroundWorker;
        private String formName = null;
        private KartForm frmKart;

        public MainForm()
        {

            InitializeComponent();
            //”станавливаем русский €зык
            Thread.CurrentThread.CurrentCulture = new CultureInfo("ru-RU", false);
            foreach (InputLanguage lang in InputLanguage.InstalledInputLanguages)
            {
                if (lang.Culture.EnglishName == "Russian (Russia)")
                {
                    InputLanguage.CurrentInputLanguage = lang;
                }
            }

            RegistryKey regKey = Registry.CurrentUser;
            regKey = regKey.OpenSubKey("Software\\UFSIN\\ivrJournal");
            this.dbPath = regKey.GetValue("dbPath", @"C:\Program files\ufsin_rk\ƒневник »¬–\divr.mdb").ToString();

            InitRegistryKeys();

/*
            UpdateUnit upUnit = new UpdateUnit();
            upUnit.DoUpdate();
            SetRights();
*/
        }

        public MainForm(String formName)
        {
            InitializeComponent();
            //”станавливаем русский €зык
            Thread.CurrentThread.CurrentCulture = new CultureInfo("ru-RU", false);
            foreach (InputLanguage lang in InputLanguage.InstalledInputLanguages)
            {
                if (lang.Culture.EnglishName == "Russian (Russia)")
                {
                    InputLanguage.CurrentInputLanguage = lang;
                }
            }

            RegistryKey regKey = Registry.CurrentUser;
            regKey = regKey.OpenSubKey("Software\\UFSIN\\ivrJournal");
            this.dbPath = regKey.GetValue("dbPath", @"C:\Program files\ufsin_rk\ƒневник »¬–\divr.mdb").ToString();
            InitRegistryKeys();

            this.formName = formName;

/*
            UpdateUnit upUnit = new UpdateUnit();
            upUnit.DoUpdate();

            SetRights();

            MDIChildShow(formName);
*/        }

        private void loadStripMenu(ToolStripMenuItem item, string nameForm, DataTable tableControl)
        {
            foreach (ToolStripItem tmi in item.DropDownItems)
            {
                DataRow[] foundRows;
                String strFound = "name='" + tmi.Name + "' AND enabled='true'";
                foundRows = tableControl.Select(strFound);
                if (foundRows.Length < 1)
                    tmi.Enabled = false;

                if (tmi is ToolStripMenuItem)
                    loadStripMenu((ToolStripMenuItem)tmi, nameForm, tableControl);
            }
        }

        private void SetRights()
        {
            FormControlManager fcm = new FormControlManager();
            fcm.SetFormControlStatus(this);
        }

        public void InitRegistryKeys()
        {
            RegistryKey regKey = Registry.CurrentUser;
            regKey = regKey.CreateSubKey("Software\\UFSIN\\ivrJournal");
            //regKey.SetValue("dbPath", "");
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
                // TODO: Add code here to open the file.
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
                // TODO: Add code here to save the current contents of the form to a file.
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // TODO: Use System.Windows.Forms.Clipboard to insert the selected text or images into the clipboard
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // TODO: Use System.Windows.Forms.Clipboard to insert the selected text or images into the clipboard
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // TODO: Use System.Windows.Forms.Clipboard.GetText() or System.Windows.Forms.GetData to retrieve information from the clipboard.
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void MDIChildShow(string formName)
        {
            bool ind=false;
            Form[] MDIf=this.MdiChildren;
            foreach(Form f in MDIf)
            {
                if(f.Name==formName)//если форма найдена среди уже открытых
                {
                    ind=true;//установка флага поиска
                    f.Activate();//выводим найденную форму на передний план
                    break;
                }
            }
            if(!ind)//если форма не найдена среди открытых
            {
                Type type = Type.GetType(Path.GetFileNameWithoutExtension(Application.ExecutablePath)+"."+formName);
                ConstructorInfo ci=type.GetConstructor(new Type[]{});
                Form f=(Form)ci.Invoke(new object[]{});
                f.MdiParent=this;//установка родительской формы
                f.Show();//показываем форму
                try
                {
                    f.Update();//при необходимости проводим обновление (перерисовку)
                }
                catch
                {
                }
            }
        }

        private void MDIChildShowSpr(string formName, string formNameSpr, string formText)
        {
            bool ind = false;
            Form[] MDIf = this.MdiChildren;
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
                types[0] = typeof(String);
                types[1] = typeof(String);
                ConstructorInfo ci = type.GetConstructor(types);
                Form f = (Form)ci.Invoke(new object[] { formNameSpr, formText });
                f.MdiParent = this;//установка родительской формы
                f.Show();//показываем форму
                try
                {
                    f.Update();//при необходимости проводим обновление (перерисовку)
                }
                catch
                {
                }
            }
        }

        private void DBConToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MDIChildShow("DBConDialog");
        }

        private void viewMenu_Click(object sender, EventArgs e)
        {
            MDIChildShow("KartForm");
        }

        private void видыѕрофилактического”четаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MDIChildShow("SprProfilactForm");
        }

        private void справочникќбразованийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MDIChildShowSpr("SprForm", "spr_edu", "—правочник образований");
        }

        private void национальностиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MDIChildShowSpr("SprForm", "spr_nation", "—правочник национальностей");
        }

        private void профессииToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MDIChildShowSpr("SprForm", "spr_profession", "—правочник профессий");
        }

        private void степень–одстваToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MDIChildShowSpr("SprForm", "spr_degree", "—правочник степеней родства");
        }

        private void семейнонѕоложениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MDIChildShowSpr("SprForm", "spr_mstatus", "—правочник семейное положение");
        }

        private void списокќтр€довToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MDIChildShowSpr("SprForm", "spr_party_number", "—писок отр€дов");
        }

        private void формToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MDIChildShow("SprControls");
        }

        private void пользователиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MDIChildShow("UsersForm");
        }

        private void сотрудникиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MDIChildShow("EmployeeForm");
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm frmAbout = new AboutForm();
            frmAbout.ShowDialog();
        }

        private void akusTransferToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MDIChildShow("AkusTransferForm");
        }

        private void упор€дочитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.Cascade);
        }

        private void contentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Help.ShowHelp(this, " help_ivr.chm ");
        }

        private void настройкаѕравѕользовател€ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MDIChildShow("EditUserRolForm");
        }

        private void отчетƒл€—оциальногоѕаспортаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MDIChildShow("RepListForm");
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            MDIChildShow("KartForm");
        }

        private void tsbRep1_Click(object sender, EventArgs e)
        {
            MDIChildShow("RepListForm");
        }

        private void tsbHelp_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Help.ShowHelp(this, " help_ivr.chm ");
        }

        private void tsbAbout_Click(object sender, EventArgs e)
        {
            AboutForm frmAbout = new AboutForm();
            frmAbout.ShowDialog();
        }

        private void виды¬оспитательной–аботыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MDIChildShowSpr("SprForm", "spr_work_type", "¬иды воспитательной работы");
        }

        private void статусыѕользоватеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MDIChildShowSpr("SprForm", "spr_user_rol", "√руппы пользователей");
        }

        private void сохранениеЅазыƒанныхToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Zip Files (*.zip)|*.zip";


                DateTime dateToSave = DateTime.Today;
                string dateString = dateToSave.ToString("d");

                saveFileDialog.FileName = "јрхив ƒ»¬– от " + dateString + ".zip";
                if (saveFileDialog.ShowDialog(this) != DialogResult.OK)
                    return;

                if (saveFileDialog.FileName != "")
                {
                    DirectoryInfo diSource = new DirectoryInfo(@dbPath);
                    FastZip fZip = new FastZip();
                    fZip.CreateZip(@saveFileDialog.FileName, @diSource.Parent.FullName, false, ".mdb$");
                    MessageBox.Show("Ѕаза данных успешно сохранена");
                }
            }
            catch (IOException ioe)
            {
                MessageBox.Show(ioe.Message, "ќшибка");
            }
        }

        private void справкаѕо«аполнениюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReportsWordClass repFilling = new ReportsWordClass();
            repFilling.ReportFilling();
        }

        private void выгрузкаƒанныхToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MDIChildShow("IVRTransferForm");
        }

        private void причиныќсвобождеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MDIChildShowSpr("SprForm", "spr_release_reason", "—правочник причины освобождени€");
        }

        private void загрузкаƒанныхtsmi_Click(object sender, EventArgs e)
        {
            //MDIChildShow("IVRReceiveForm");
            IVRReceiveForm ivrrform = new IVRReceiveForm(FormMode.full);

            ivrrform.MdiParent = this;
            ivrrform.Show();
        }

        private void виды¬зысканийtsmi_Click(object sender, EventArgs e)
        {
            MDIChildShowSpr("SprForm", "spr_penalty_type", "—правочник виды взысканий");
        }

        private void tsmiLoadLite_Click(object sender, EventArgs e)
        {
            IVRReceiveForm ivrrform = new IVRReceiveForm(FormMode.lite);
            
            ivrrform.MdiParent = this;
            ivrrform.Show();
        }

        private void видыѕоощренийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MDIChildShowSpr("SprForm", "spr_bonus_type", "—правочник виды поощрений");
        }


        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            backgroundWorker.ReportProgress(0, "ќбновление базы данных...");
            UpdateUnit upUnit = new UpdateUnit();
            upUnit.DoUpdate();
            //backgroundWorker.ReportProgress(50, "”становка прав пользовател€...");
            
            //Ќе работает ???
            //SetRights();

//            if (formName != null)
//                frmKart = new KartForm();

            backgroundWorker.ReportProgress(100, "«агрузка завершена!");
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            toolStripProgressBar.Value = e.ProgressPercentage;
            toolStripStatusLabel.Text = e.UserState as String;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            SetRights();

            if (formName != null)
            {
                frmKart = new KartForm();
                frmKart.MdiParent = this;
                frmKart.Show();
            }

            toolStripProgressBar.Enabled = false;
            statusStrip.Items.Clear();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

            statusStrip.Items.Clear();

            toolStripProgressBar = new ToolStripProgressBar();
            toolStripProgressBar.Enabled = false;
            toolStripStatusLabel = new ToolStripStatusLabel();
            statusStrip.Items.Add(toolStripProgressBar);
            statusStrip.Items.Add(toolStripStatusLabel);

            backgroundWorker = new BackgroundWorker();
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
            backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
            backgroundWorker.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker1_ProgressChanged);

            toolStripProgressBar.Enabled = true;
            backgroundWorker.RunWorkerAsync();

//            if (formName != null)
//                MDIChildShow(formName);

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //ChooseDepartmentForm frmChooseDepartment = new ChooseDepartmentForm();
            //frmChooseDepartment.ShowDialog();
            MDIChildShow("ChooseDepartmentForm");
        }

    }
}
