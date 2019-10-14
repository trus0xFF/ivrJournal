using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Word = Microsoft.Office.Interop.Word;
//using Word;

namespace ivrJournal
{
    public partial class PsychoListForm : Form
    {
        private ChildDBConnect dbCon;
        private DataRow selRow;
        private Int32 idSpec;
        private string typeDoc;

        public PsychoListForm(DataRow selRow, string nameForm, string punktNumber)
        {
            InitializeComponent();
            dgPsycho.AutoGenerateColumns = false;
            this.selRow = selRow;
            idSpec = (Int32)selRow["id"];
            typeDoc = punktNumber;
            if (punktNumber == "1")
                this.Text = "Перечень документов по психологической работе";
            else
                this.Text = "Перечень документов по воспитательной работе";
            LoadPsycho();
        }

        private void LoadPsycho()
        {
//            dbCon = new ChildDBConnect(idSpec, "spec_psycho");
            dbCon = new ChildDBConnect(idSpec, "spec_psycho", typeDoc);

            dgPsycho.Columns.Add(new DataGridViewTextBoxColumn());
            dgPsycho.Columns[0].DataPropertyName = "id";
            dgPsycho.Columns[0].HeaderText = "код";
            dgPsycho.Columns[0].Visible = false;

            dgPsycho.Columns.Add(new DataGridViewTextBoxColumn());
            dgPsycho.Columns[1].DataPropertyName = "id_spec";
            dgPsycho.Columns[1].HeaderText = "код";
            dgPsycho.Columns[1].Visible = false;


            CalendarColumn col = new CalendarColumn();
            {
                col.DataPropertyName = "date_doc";
                col.HeaderText = "Дата документа";
                col.Width = 90;
                col.MinimumWidth = 90;
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            }
            dgPsycho.Columns.Add(col);

            DataGridViewTextBoxColumn textColumn = new DataGridViewTextBoxColumn();
            {
                textColumn.DataPropertyName = "name";
                textColumn.HeaderText = "Наименование документа";
                textColumn.Width = 200;
                textColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            dgPsycho.Columns.Add(textColumn);

            dgPsycho.DataSource = new DataView(dbCon.GetDataTable("spec_psycho"));
        }

        private void bnEdit_Click(object sender, EventArgs e)
        {
            if (((DataView)dgPsycho.DataSource).Count == 0)
                return;

            int RowIndex = 0;
            RowIndex = dgPsycho.CurrentRow.Index;

            DataRow row = ((DataView)dgPsycho.DataSource)[RowIndex].Row;

            ProfilePsychoForm profilePsychoForm = new ProfilePsychoForm(dbCon, idSpec, typeDoc, row);
            profilePsychoForm.MdiParent = this.MdiParent;
            profilePsychoForm.Show();
 
        }

        private void bnAdd_Click(object sender, EventArgs e)
        {
            ProfilePsychoForm profilePsychoForm = new ProfilePsychoForm(dbCon, idSpec, typeDoc);
            profilePsychoForm.MdiParent = this.MdiParent;
            profilePsychoForm.Show();
        }

        private void bnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void bnShow_Click(object sender, EventArgs e)
        {

            if (((DataView)dgPsycho.DataSource).Count == 0)
                return;

            int RowIndex = 0;
            RowIndex = dgPsycho.CurrentRow.Index;

            DataRow row = ((DataView)dgPsycho.DataSource)[RowIndex].Row;

            if (row["psycho_doc"] != null)
            {
                if (!Convert.IsDBNull(row["psycho_doc"]))
                {
                    try
                    {
                        String tempFilePath = Path.GetTempPath() + @"\psycho.doc";
                        FileStream fs = new FileStream(tempFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                        BinaryWriter binWriter = new BinaryWriter(fs);

                        //                Byte[] buffer = (Byte[])selRow["psycho_doc"];
                        Byte[] buffer = (Byte[])row["psycho_doc"];
                        binWriter.Write(buffer);
                        binWriter.Close();
                        fs.Close();
                        Word.Application wordApp = new Word.Application();
                        wordApp.Visible = true;
                        Object filename = tempFilePath;
                        Object confirmConversions = true;
                        Object readOnly = true;
                        Object addToRecentFiles = true;
                        Object passwordDocument = Type.Missing;
                        Object passwordTemplate = Type.Missing;
                        Object revert = false;
                        Object writePasswordDocument = Type.Missing;
                        Object writePasswordTemplate = Type.Missing;
                        Object format = Type.Missing;
                        Object encoding = Type.Missing; ;
                        Object oVisible = Type.Missing;
                        Object openConflictDocument = Type.Missing;
                        Object openAndRepair = Type.Missing;
                        Object documentDirection = Type.Missing;
                        Object noEncodingDialog = false;
                        Object xmlTransform = Type.Missing;

                        Word.Document worddocument = worddocument = wordApp.Documents.Open(ref filename,
                        ref confirmConversions, ref readOnly, ref addToRecentFiles,
                        ref passwordDocument, ref passwordTemplate, ref revert,
                        ref writePasswordDocument, ref writePasswordTemplate,
                        ref format, ref encoding, ref oVisible,
                        ref openAndRepair, ref documentDirection, ref noEncodingDialog, ref xmlTransform);
                    }
                    catch (IOException ioe)
                    {
                        MessageBox.Show(ioe.Message.ToString(), "Ошибка");
                    }
                }
                else
                {
                    MessageBox.Show("Документ отсутствует в базе данных", "Сообщение");
                }
            }
            else
            {
                MessageBox.Show("Документ отсутствует в базе данных", "Сообщение");
            }
        }
    }
}