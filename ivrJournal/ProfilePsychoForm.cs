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
    public partial class ProfilePsychoForm : Form
    {
        private ChildDBConnect dbCon;
        private Boolean IsNew;
        private DataRow selRow;
        private PsychoData pd;
        private Byte[] bufferDoc;
        private int idSpec;
        private string typeDoc;

        public ProfilePsychoForm(ChildDBConnect newDbCon, int idSpec, string typeDoc)
        {
            InitializeComponent();
            pd = new PsychoData();
            this.IsNew = true;
            this.dbCon = newDbCon;
            this.idSpec = idSpec;
            this.typeDoc = typeDoc;

            bnOpen.Enabled = false;
        }


        public ProfilePsychoForm(ChildDBConnect newDbCon, int idSpec, string typeDoc, DataRow selRow)
        {
            InitializeComponent();
            this.selRow = selRow;
            pd = new PsychoData();
            this.IsNew = false;
            this.dbCon = newDbCon;
            this.idSpec = idSpec;
            this.typeDoc = typeDoc;

            tbName.Text = selRow["name"].ToString();
//            mtbDateDoc.Text = selRow["date_doc"].ToString();
            dtDateDoc.Text = selRow["date_doc"].ToString();

            if (selRow["psycho_doc"] == null)
                bnOpen.Enabled = false;
            else
                if (!Convert.IsDBNull(selRow["psycho_doc"]))
                    bufferDoc = (Byte[])selRow["psycho_doc"]; //System.DBNull
                else
                    bnOpen.Enabled = false;
        }

        private void bnOpen_Click(object sender, EventArgs e)
        {
            DataTable dt = dbCon.GetDataTable("spec_psycho");
            if (bufferDoc == null)
            {
                MessageBox.Show("�������� ����������� � ���� ������","���������");
                return;
            }

            try
            {
                String tempFilePath = Path.GetTempPath() + @"\psycho.doc";
                FileStream fs = new FileStream(tempFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                BinaryWriter binWriter = new BinaryWriter(fs);

//                Byte[] buffer = (Byte[])selRow["psycho_doc"];
                Byte[] buffer = this.bufferDoc;
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
                MessageBox.Show(ioe.Message.ToString(), "������");
            }

        }

        private void bnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void bnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Doc Files (*.doc)|*.doc|All Files (*.*)|*.*";
                if (openFileDialog.ShowDialog(this) != DialogResult.OK)
                    return;

                FileStream fs = new FileStream(openFileDialog.FileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                BinaryReader binReader = new BinaryReader(fs);
                binReader.BaseStream.Position = 0;

                int len = (int)fs.Length;
                Byte[] buffer = new Byte[len];

                binReader.Read(buffer, 0, len);

                binReader.Close();
                fs.Close();

                bufferDoc = buffer;

                bnOpen.Enabled = true;
            }
            catch (IOException ioe)
            {
                MessageBox.Show(ioe.Message, "������");
            }
        }

        private void bnSave_Click(object sender, EventArgs e)
        {
            pd.name = this.tbName.Text;
            pd.date_doc = this.dtDateDoc.Text;
            pd.psycho_doc = this.bufferDoc;
            pd.id_spec = idSpec;
            pd.type_doc = typeDoc;

            if (IsNew)
                dbCon.SavePsychoData(pd);
            else
                dbCon.SavePsychoData(pd, selRow);

            Close();
        }
    }

    public class PsychoData
    {
        private String Name;
        public String name
        {
            get { return Name; }
            set { Name = value; }
        }

        private String DateDoc;
        public String date_doc
        {
            get { return DateDoc; }
            set { DateDoc = value; }
        }

        private int IdSpec;
        public int id_spec
        {
            get { return IdSpec; }
            set { IdSpec = value; }
        }

        private string TypeDoc;
        public string type_doc
        {
            get { return TypeDoc; }
            set { TypeDoc = value; }
        }

        private byte[] PsychoDoc;
        public byte[] psycho_doc
        {
            get { return PsychoDoc; }
            set { PsychoDoc = value; }
        }

    }

}