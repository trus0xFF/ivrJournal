using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using Microsoft.Win32;
using Word = Microsoft.Office.Interop.Word;
using System.Windows.Forms;
//using Word;

namespace ivrJournal
{
    class ReportsWordClass
    {
        SQLDBConnectLite sqlCon;

        public ReportsWordClass()
        {
            sqlCon = new SQLDBConnectLite();
        }

        public void IVRDoc(DataRow row)
        {
            DataTable sqlTable = sqlCon.GetDataTable("spec_psycho", "SELECT * FROM spec_psycho WHERE (id_spec=" + row["id"].ToString() + ")");

            int i = 0;
            String tempFilePath;
            FileStream fs;
            BinaryWriter binWriter;
            Byte[] buffer;

            Object filename;
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

            Word.Document worddocument;
            Word.Application wordApp;

            foreach (DataRow rowt in sqlTable.Rows)
            {
                i = i + 1;

                if (!Convert.IsDBNull(rowt["psycho_doc"]))
                {
                    try
                    {
                        tempFilePath = Path.GetTempPath() + @"\psycho" + i + ".doc";
                        fs = new FileStream(tempFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                        binWriter = new BinaryWriter(fs);

                        buffer = (Byte[])rowt["psycho_doc"];
                        binWriter.Write(buffer);
                        binWriter.Close();
                        fs.Close();
                        wordApp = new Word.Application();
                        wordApp.Visible = true;
                        filename = tempFilePath;

                        worddocument = wordApp.Documents.Open(ref filename,
                        ref confirmConversions, ref readOnly, ref addToRecentFiles,
                        ref passwordDocument, ref passwordTemplate, ref revert,
                        ref writePasswordDocument, ref writePasswordTemplate,
                        ref format, ref encoding, ref oVisible,
                        ref openAndRepair, ref documentDirection, ref noEncodingDialog, ref xmlTransform);
                    }
                    catch 
                    {
                    }
                }
            }
        }

        public void IVRJournal(DataRow row)
        {
            RegistryKey regKey = Registry.CurrentUser;
            regKey = regKey.OpenSubKey("Software\\UFSIN\\ivrJournal");
            FileInfo file = new FileInfo(regKey.GetValue("dbPath", "").ToString());
            String dbDirPath = file.DirectoryName;

            Word.Application wordapp;
            Word.Document worddocument;
            Word.Paragraph wordparagraph;

            wordapp = new Word.Application();
            wordapp.Visible = true;
            Object template = Type.Missing;
            Object newTemplate = false;
            Object documentType = Word.WdNewDocumentType.wdNewBlankDocument;
            Object visible = true;
            worddocument = wordapp.Documents.Add(ref template, ref newTemplate, ref documentType, ref visible);
            worddocument.Activate();

            object oMissing = System.Reflection.Missing.Value;
            object unit;
            object extend;
            unit = Word.WdUnits.wdStory;
            extend = Word.WdMovementType.wdMove;

            wordparagraph = worddocument.Paragraphs.Add(ref oMissing);

            string imagePath;

            if (row["foto"] != null)
                if ( !String.IsNullOrEmpty(row["foto"].ToString()) )
                {
                    imagePath = dbDirPath + @"\" + row["foto"].ToString();
                    if (File.Exists(@imagePath))
                    {
                        Word.InlineShape fotoImage = wordapp.Selection.InlineShapes.AddPicture(@imagePath, ref oMissing, ref oMissing, ref oMissing);
                        fotoImage.Width = 85;
                        fotoImage.Height = 115;
                        wordparagraph.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;
                    }
                }

            worddocument.Paragraphs.Add(ref oMissing);
            wordapp.Selection.EndKey(ref unit, ref extend);
            wordparagraph = worddocument.Paragraphs.Add(ref oMissing);

            wordparagraph.Range.Text = "� � � � � � �";
            wordparagraph.Range.Font.Color = Word.WdColor.wdColorBlack;
            wordparagraph.Range.Font.Size = 14;
            wordparagraph.Range.Font.Name = "Times New Roman";
            wordparagraph.Range.Font.Bold = 1;
            wordparagraph.Range.Font.Underline = Word.WdUnderline.wdUnderlineNone;
            wordparagraph.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

            worddocument.Paragraphs.Add(ref oMissing);
            wordapp.Selection.EndKey(ref unit, ref extend);
            wordparagraph = worddocument.Paragraphs.Add(ref oMissing);
            wordparagraph.Range.Text = "�������������� �������������� ������ � ���������� � �������������� ����������";
            wordparagraph.Range.Font.Color = Word.WdColor.wdColorBlack;
            wordparagraph.Range.Font.Size = 13;
            wordparagraph.Range.Font.Bold = 1;

            worddocument.Paragraphs.Add(ref oMissing);
            worddocument.Paragraphs.Add(ref oMissing);
            wordapp.Selection.EndKey(ref unit, ref extend);
            wordparagraph = worddocument.Paragraphs.Add(ref oMissing);
            wordparagraph.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

            Object defaultTableBehavior = Word.WdDefaultTableBehavior.wdWord9TableBehavior;
            Object autoFitBehavior = Word.WdAutoFitBehavior.wdAutoFitWindow;

            Word.Range wordrange = wordparagraph.Range;

            Word.Table wordtable1 = worddocument.Tables.Add(wordrange, 9, 2, ref defaultTableBehavior, ref autoFitBehavior);
            wordtable1.Borders.Enable = 0;
            wordtable1.Columns[1].Width = 200;
            wordtable1.Columns[2].Width = 300;
            wordtable1.Cell(1, 1).Range.Text = "�������";
            wordtable1.Cell(1, 2).Range.Text = row["last_name"].ToString();
            wordtable1.Cell(2, 1).Range.Text = "���";
            wordtable1.Cell(2, 2).Range.Text = row["first_name"].ToString();
            wordtable1.Cell(3, 1).Range.Text = "��������";
            wordtable1.Cell(3, 2).Range.Text = row["patronymic"].ToString();
            wordtable1.Cell(4, 1).Range.Text = "�����, ����� � ��� ��������";
            wordtable1.Cell(4, 2).Range.Text = getValidDate(row["birthdate"]);
            wordtable1.Cell(5, 1).Range.Text = "����� ����� � ����� �������";
            wordtable1.Cell(5, 2).Range.Text = row["court"].ToString() + " " + getValidDate(row["crime_date"]);
            wordtable1.Cell(6, 1).Range.Text = "������(�) ��";
            wordtable1.Cell(6, 2).Range.Text = row["article"].ToString();
            wordtable1.Cell(7, 1).Range.Text = "���� ������� �������";
            wordtable1.Cell(7, 2).Range.Text = row["period"].ToString();
            wordtable1.Cell(8, 1).Range.Text = "������ �����";
            wordtable1.Cell(8, 2).Range.Text = getValidDate(row["period_start"]);
            wordtable1.Cell(9, 1).Range.Text = "����� �����";
            wordtable1.Cell(9, 2).Range.Text = getValidDate(row["period_end"]);

            wordtable1.Cell(1, 2).Borders[Word.WdBorderType.wdBorderBottom].Visible = true;
            wordtable1.Cell(2, 2).Borders[Word.WdBorderType.wdBorderBottom].Visible = true;
            wordtable1.Cell(3, 2).Borders[Word.WdBorderType.wdBorderBottom].Visible = true;
            wordtable1.Cell(4, 2).Borders[Word.WdBorderType.wdBorderBottom].Visible = true;
            wordtable1.Cell(5, 2).Borders[Word.WdBorderType.wdBorderBottom].Visible = true;
            wordtable1.Cell(6, 2).Borders[Word.WdBorderType.wdBorderBottom].Visible = true;
            wordtable1.Cell(7, 2).Borders[Word.WdBorderType.wdBorderBottom].Visible = true;
            wordtable1.Cell(8, 2).Borders[Word.WdBorderType.wdBorderBottom].Visible = true;
            wordtable1.Cell(9, 2).Borders[Word.WdBorderType.wdBorderBottom].Visible = true;

            object begCell = wordtable1.Cell(1, 1).Range.Start;
            object endCell = wordtable1.Cell(9, 2).Range.End;
            Word.Range wordcellrange = worddocument.Range(ref begCell, ref endCell);
            wordcellrange.Font.Bold = 0;
            //            wordcellrange.Borders.Enable = 0;
            wordcellrange.Font.Italic = 0;
            wordcellrange.Font.Size = 13;
            wordcellrange.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;

            worddocument.Paragraphs.Add(ref oMissing);
            wordapp.Selection.EndKey(ref unit, ref extend);
            wordparagraph = worddocument.Paragraphs.Add(ref oMissing);

            Word.Table wordtable = worddocument.Tables.Add(wordparagraph.Range, 5, 2, ref defaultTableBehavior, ref autoFitBehavior);

            wordtable.Borders.Enable = 0;
            wordtable.Columns[1].Width = 300;
            wordtable.Columns[2].Width = 180;
            wordtable.Cell(1, 1).Range.Text = "����� ���������� �������� (�������������)";

            wordtable.Cell(2, 1).Range.Text = "�� ����������� ������� ��������� ���������";
            wordtable.Cell(2, 2).Range.Text = getValidDate(row["period_light"]);
            wordtable.Cell(3, 1).Range.Text = "�� ������� ������� ��������� ���������";
            wordtable.Cell(3, 2).Range.Text = getValidDate(row["period_normal"]);
            wordtable.Cell(4, 1).Range.Text = "� �������-���������";
            wordtable.Cell(4, 2).Range.Text = getValidDate(row["period_kp"]);
            wordtable.Cell(5, 1).Range.Text = "����������� �� �������-��������� ������������";
            wordtable.Cell(5, 2).Range.Text = getValidDate(row["period_udo"]);
            wordtable.Cell(1, 2).Borders[Word.WdBorderType.wdBorderBottom].Visible = true;
            wordtable.Cell(2, 2).Borders[Word.WdBorderType.wdBorderBottom].Visible = true;
            wordtable.Cell(3, 2).Borders[Word.WdBorderType.wdBorderBottom].Visible = true;
            wordtable.Cell(4, 2).Borders[Word.WdBorderType.wdBorderBottom].Visible = true;
            wordtable.Cell(5, 2).Borders[Word.WdBorderType.wdBorderBottom].Visible = true;

            begCell = wordtable.Cell(1, 1).Range.Start;
            endCell = wordtable.Cell(5, 2).Range.End;
            wordcellrange = worddocument.Range(ref begCell, ref endCell);
            wordcellrange.Font.Bold = 0;
            wordcellrange.Font.Italic = 0;
            wordcellrange.Font.Size = 13;
            wordcellrange.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;

            worddocument.Paragraphs.Add(ref oMissing);
            wordapp.Selection.EndKey(ref unit, ref extend);
            wordparagraph = worddocument.Paragraphs.Add(ref oMissing);

            wordtable = worddocument.Tables.Add(wordparagraph.Range, 1, 4, ref defaultTableBehavior, ref autoFitBehavior);
            wordtable.Borders.Enable = 1;
            wordtable.Columns[1].Width = 50;
            wordtable.Columns[2].Width = 100;
            wordtable.Columns[3].Width = 200;
            wordtable.Columns[4].Width = 150;
            wordtable.Cell(1, 1).Range.Text = "�";
            wordtable.Cell(1, 2).Range.Text = "���� �������� � �� (�����)";
            wordtable.Cell(1, 3).Range.Text = "����� ������ � ������ � ����������� � �����";
            wordtable.Cell(1, 4).Range.Text = "������� ��������";

            begCell = wordtable.Cell(1, 1).Range.Start;
            endCell = wordtable.Cell(1, 4).Range.End;
            wordcellrange = worddocument.Range(ref begCell, ref endCell);
            wordcellrange.Font.Bold = 0;
            wordcellrange.Font.Italic = 0;
            wordcellrange.Font.Size = 12;
            wordcellrange.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

            //SQLDBConnect sqlCon = new SQLDBConnect((int)row["id"], "party", "SELECT p.arr_date AS arr_date, p.ord AS ord, p.reason AS reason, spr_party_number.name AS party_number FROM party AS p LEFT JOIN spr_party_number ON spr_party_number.id=p.party_number_id WHERE (p.id_spec=" + row["id"].ToString() + ")");
            //sqlCon = new SQLDBConnect("party", "SELECT p.arr_date AS arr_date, p.ord AS ord, p.reason AS reason, spr_party_number.name AS party_number FROM party AS p LEFT JOIN spr_party_number ON spr_party_number.id=p.party_number_id WHERE (p.id_spec=" + row["id"].ToString() + ")");
            DataTable sqlTable = sqlCon.GetDataTable("party", "SELECT p.arr_date AS arr_date, p.ord AS ord, p.reason AS reason, spr_party_number.name AS party_number FROM party AS p LEFT JOIN spr_party_number ON spr_party_number.id=p.party_number_id WHERE (p.id_spec=" + row["id"].ToString() + ")");

            Word.Row currentRow;
            int i = 0;

            foreach (DataRow rowt in sqlTable.Rows)
            {
                //i = i + 1;
                currentRow = wordtable.Rows.Add(ref oMissing);
                currentRow.Range.Font.Bold = 0;
                currentRow.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                currentRow.Cells[1].Range.Text = (++i).ToString();
                currentRow.Cells[2].Range.Text = getValidDateShort(rowt["arr_date"]);
                currentRow.Cells[3].Range.Text = rowt["party_number"].ToString() + " ������ � " + rowt["ord"].ToString();
                currentRow.Cells[4].Range.Text = rowt["reason"].ToString();
            }

            worddocument.Paragraphs.Add(ref oMissing);
            worddocument.Paragraphs.Add(ref oMissing);
            wordapp.Selection.EndKey(ref unit, ref extend);
            wordparagraph = worddocument.Paragraphs.Add(ref oMissing);
            wordparagraph.Range.Text = "1. ����� �������� �� ����������";
            wordparagraph.Range.Font.Size = 14;
            wordparagraph.Range.Font.Bold = 1;
            wordparagraph.Range.Font.Underline = Word.WdUnderline.wdUnderlineNone;
            wordparagraph.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

            worddocument.Paragraphs.Add(ref oMissing);
            worddocument.Paragraphs.Add(ref oMissing);
            wordapp.Selection.EndKey(ref unit, ref extend);
            wordparagraph = worddocument.Paragraphs.Add(ref oMissing);
            wordparagraph.Range.Text = "(����������� �� ������ ������� ���������� ������� ����, ����������� �������� �������� ����������� �� ����� ��������� ��������� � ��.)";
            wordparagraph.Range.Font.Size = 13;
            wordparagraph.Range.Font.Bold = 0;
            wordparagraph.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            wordparagraph.Range.Font.Underline = Word.WdUnderline.wdUnderlineNone;

            worddocument.Paragraphs.Add(ref oMissing);
            worddocument.Paragraphs.Add(ref oMissing);
            wordapp.Selection.EndKey(ref unit, ref extend);
            wordparagraph = worddocument.Paragraphs.Add(ref oMissing);
            wordparagraph.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
            wordtable = worddocument.Tables.Add(wordparagraph.Range, 2, 2, ref defaultTableBehavior, ref autoFitBehavior);

            String nameNation = sqlCon.GetValue("name", "SELECT * FROM spr_nation WHERE id = " + row["nation_id"].ToString());

            String nameMstatus = sqlCon.GetValue("name", "SELECT * FROM spr_mstatus WHERE id = " + row["mstatus_id"].ToString());

            String nameEdu = sqlCon.GetValue("name", "SELECT * FROM spr_edu WHERE id = " + row["edu_id"].ToString());

            String nameProfession = sqlCon.GetValue("name", "SELECT * FROM spr_profession WHERE id = " + row["profession_id"].ToString());


            wordtable.Borders.Enable = 0;
            wordtable.Columns[1].Width = 180;
            wordtable.Columns[2].Width = 300;
            wordtable.Cell(1, 1).Range.Text = "1.1. ��������������";
            wordtable.Cell(1, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
            wordtable.Cell(1, 1).Range.Font.Bold = 1;
            wordtable.Cell(1, 2).Range.Text = nameNation;
            wordtable.Cell(1, 2).Range.Font.Bold = 0;
            wordtable.Cell(2, 1).Range.Text = "1.2. �������� ���������";
            wordtable.Cell(2, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
            wordtable.Cell(2, 1).Range.Font.Bold = 1;
            wordtable.Cell(2, 2).Range.Text = nameMstatus;
            wordtable.Cell(2, 2).Range.Font.Bold = 0;
            wordtable.Cell(1, 2).Borders[Word.WdBorderType.wdBorderBottom].Visible = true;
            wordtable.Cell(2, 2).Borders[Word.WdBorderType.wdBorderBottom].Visible = true;

            worddocument.Paragraphs.Add(ref oMissing);
            wordapp.Selection.EndKey(ref unit, ref extend);
            wordparagraph = worddocument.Paragraphs.Add(ref oMissing);
            wordparagraph.Range.Text = "1.3. �������� � �������������";
            wordparagraph.Range.Font.Size = 13;
            wordparagraph.Range.Font.Bold = 1;
            wordparagraph.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;

            //sqlCon = new SQLDBConnect((int)row["id"], "relations", "SELECT r.last_name AS last_name, r.first_name AS first_name, r.patronymic AS patronymic, r.address AS address, r.birthdate AS birthdate, spr_degree.name AS degree FROM relations AS r LEFT JOIN spr_degree ON spr_degree.id=r.degree_id WHERE (r.id_spec=" + row["id"].ToString() + ")");
            //sqlCon = new SQLDBConnect("relations", "SELECT r.last_name AS last_name, r.first_name AS first_name, r.patronymic AS patronymic, r.address AS address, r.birthdate AS birthdate, spr_degree.name AS degree FROM relations AS r LEFT JOIN spr_degree ON spr_degree.id=r.degree_id WHERE (r.id_spec=" + row["id"].ToString() + ")");
            sqlTable = sqlCon.GetDataTable("relations", "SELECT r.last_name AS last_name, r.first_name AS first_name, r.patronymic AS patronymic, r.address AS address, r.birthdate AS birthdate, spr_degree.name AS degree FROM relations AS r LEFT JOIN spr_degree ON spr_degree.id=r.degree_id WHERE (r.id_spec=" + row["id"].ToString() + ")");

            foreach (DataRow rowt in sqlTable.Rows)
            {
                worddocument.Paragraphs.Add(ref oMissing);
                wordapp.Selection.EndKey(ref unit, ref extend);
                wordparagraph = worddocument.Paragraphs.Add(ref oMissing);
                wordparagraph.Range.Text = rowt["degree"].ToString() + ": " + rowt["last_name"].ToString().Trim() + " " + rowt["first_name"].ToString().Trim() + " " + rowt["patronymic"].ToString().Trim() + " " + getValidDateShort(rowt["birthdate"]) + ", " + rowt["address"].ToString().Trim() + ";";
                wordparagraph.Range.Font.Size = 12;
                wordparagraph.Range.Font.Bold = 0;
                wordparagraph.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
            }

            worddocument.Paragraphs.Add(ref oMissing);
            wordapp.Selection.EndKey(ref unit, ref extend);
            wordparagraph = worddocument.Paragraphs.Add(ref oMissing);
            wordparagraph.Range.Font.Size = 13;
            wordtable = worddocument.Tables.Add(wordparagraph.Range, 2, 2, ref defaultTableBehavior, ref autoFitBehavior);
            wordtable.Borders.Enable = 0;
            wordtable.Columns[1].Width = 250;
            wordtable.Columns[2].Width = 230;
            wordtable.Cell(1, 1).Range.Text = "1.4. ����������� �� ���������";
            wordtable.Cell(1, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
            wordtable.Cell(1, 1).Range.Font.Size = 13;
            wordtable.Cell(1, 1).Range.Font.Bold = 1;
            wordtable.Cell(1, 2).Range.Text = nameEdu;
            wordtable.Cell(1, 2).Range.Font.Size = 13;
            wordtable.Cell(1, 2).Range.Font.Bold = 0;
            wordtable.Cell(1, 2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            wordtable.Cell(1, 2).Borders[Word.WdBorderType.wdBorderBottom].Visible = true;
            wordtable.Cell(2, 1).Range.Text = "1.5. ��������� (�������������) �� ���������";
            wordtable.Cell(2, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
            wordtable.Cell(2, 1).Range.Font.Bold = 1;
            wordtable.Cell(2, 1).Range.Font.Size = 13;
            wordtable.Cell(2, 2).Range.Text = nameProfession;
            wordtable.Cell(2, 2).Range.Font.Bold = 0;
            wordtable.Cell(2, 2).Range.Font.Size = 13;
            wordtable.Cell(2, 2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            wordtable.Cell(2, 2).Borders[Word.WdBorderType.wdBorderBottom].Visible = true;

            worddocument.Paragraphs.Add(ref oMissing);
            wordapp.Selection.EndKey(ref unit, ref extend);
            wordparagraph = worddocument.Paragraphs.Add(ref oMissing);
            wordparagraph.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
            wordparagraph.Range.Text = "1.6. ������� ��������� ������� ������������";
            wordparagraph.Range.Font.Size = 13;
            wordparagraph.Range.Font.Bold = 1;
            wordparagraph.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
            worddocument.Paragraphs.Add(ref oMissing);
            wordapp.Selection.EndKey(ref unit, ref extend);
            wordparagraph = worddocument.Paragraphs.Add(ref oMissing);
            wordparagraph.Range.Text = row["crime_description"].ToString();
            wordparagraph.Range.Font.Bold = 0;
            wordparagraph.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;

            worddocument.Paragraphs.Add(ref oMissing);
            worddocument.Paragraphs.Add(ref oMissing);
            wordapp.Selection.EndKey(ref unit, ref extend);
            wordparagraph = worddocument.Paragraphs.Add(ref oMissing);
            wordparagraph.Range.Text = "1.7. �������� �� ��������� ���������� � ��������� ������������";
            wordparagraph.Range.Font.Size = 13;
            wordparagraph.Range.Font.Bold = 1;
            wordparagraph.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;

            sqlTable = sqlCon.GetDataTable("prev_conv", "SELECT prev_conv.start_date AS start_date, prev_conv.text_prev AS text_prev, prev_conv.performer AS performer, prev_conv.article AS article, prev_conv.release_date AS release_date, spr_release_reason.name AS release_reason FROM prev_conv LEFT JOIN spr_release_reason ON spr_release_reason.id=prev_conv.release_reason_id WHERE (prev_conv.id_spec=" + row["id"].ToString() + ") ORDER BY start_date");

            if (sqlTable.Rows.Count == 0)
            {
                worddocument.Paragraphs.Add(ref oMissing);
                wordapp.Selection.EndKey(ref unit, ref extend);
                wordparagraph = worddocument.Paragraphs.Add(ref oMissing);
                wordparagraph.Range.Text = "�� �����";
                wordparagraph.Range.Font.Size = 12;
                wordparagraph.Range.Font.Bold = 0;
                wordparagraph.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
            }

            foreach (DataRow rowt in sqlTable.Rows)
            {
                worddocument.Paragraphs.Add(ref oMissing);
                wordapp.Selection.EndKey(ref unit, ref extend);
                wordparagraph = worddocument.Paragraphs.Add(ref oMissing);
                wordparagraph.Range.Text = getValidDateShort(rowt["start_date"]) + ": " + rowt["text_prev"].ToString().Trim() + " " + rowt["performer"].ToString().Trim() + " " + rowt["article"].ToString().Trim() + ", ���� ������������ " + getValidDateShort(rowt["release_date"]) + ";";
                wordparagraph.Range.Font.Size = 12;
                wordparagraph.Range.Font.Bold = 0;
                wordparagraph.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
            }

            worddocument.Paragraphs.Add(ref oMissing);
            worddocument.Paragraphs.Add(ref oMissing);
            wordapp.Selection.EndKey(ref unit, ref extend);
            wordparagraph = worddocument.Paragraphs.Add(ref oMissing);
            wordparagraph.Range.Text = "1.8. ����������� ���������� � ��������� �������� � ����������������";
            wordparagraph.Range.Font.Size = 13;
            wordparagraph.Range.Font.Bold = 1;
            worddocument.Paragraphs.Add(ref oMissing);
            wordapp.Selection.EndKey(ref unit, ref extend);
            wordparagraph = worddocument.Paragraphs.Add(ref oMissing);
            wordparagraph.Range.Text = row["med_description"].ToString();
            wordparagraph.Range.Font.Bold = 0;
            wordparagraph.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;

            worddocument.Paragraphs.Add(ref oMissing);
            worddocument.Paragraphs.Add(ref oMissing);
            wordapp.Selection.EndKey(ref unit, ref extend);
            wordparagraph = worddocument.Paragraphs.Add(ref oMissing);
            wordparagraph.Range.Text = "1.9. ���� ��������������, ��������������� ����������� � ���������� ����� � �������������� ������ � ���";
            wordparagraph.Range.Font.Size = 13;
            wordparagraph.Range.Font.Bold = 1;
            wordparagraph.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
            worddocument.Paragraphs.Add(ref oMissing);
            wordapp.Selection.EndKey(ref unit, ref extend);
            wordparagraph = worddocument.Paragraphs.Add(ref oMissing);
            wordparagraph.Range.Text = row["other"].ToString();
            wordparagraph.Range.Font.Bold = 0;
            wordparagraph.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;

            worddocument.Paragraphs.Add(ref oMissing);
            worddocument.Paragraphs.Add(ref oMissing);
            wordapp.Selection.EndKey(ref unit, ref extend);
            wordparagraph = worddocument.Paragraphs.Add(ref oMissing);
            wordparagraph.Range.Text = "���������������� ����:";
            wordparagraph.Range.Font.Size = 13;
            wordparagraph.Range.Font.Bold = 0;
            wordparagraph.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;

            sqlTable = sqlCon.GetDataTable("profilact_ychet", "SELECT profilact_ychet.data_post AS data_post, profilact_ychet.data_snyat AS data_snyat, spr_profilact_ychet.name as profilact_type FROM profilact_ychet LEFT JOIN spr_profilact_ychet ON spr_profilact_ychet.id=profilact_ychet.id_profilact_ychet WHERE (profilact_ychet.id_spec=" + row["id"].ToString() + ")");

            if (sqlTable.Rows.Count == 0)
            {
                worddocument.Paragraphs.Add(ref oMissing);
                wordapp.Selection.EndKey(ref unit, ref extend);
                wordparagraph = worddocument.Paragraphs.Add(ref oMissing);
                wordparagraph.Range.Text = "�� �������";
                wordparagraph.Range.Font.Size = 12;
                wordparagraph.Range.Font.Bold = 0;
                wordparagraph.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
            }

            foreach (DataRow rowt in sqlTable.Rows)
            {
                worddocument.Paragraphs.Add(ref oMissing);
                wordapp.Selection.EndKey(ref unit, ref extend);
                wordparagraph = worddocument.Paragraphs.Add(ref oMissing);
                wordparagraph.Range.Text = "���� ���������� �� ����: " + getValidDateShort(rowt["data_post"]) + ", ��������� �����: " + rowt["profilact_type"].ToString().Trim() + ", ���� ������ � ����� " + getValidDateShort(rowt["data_snyat"]) + ";";
                wordparagraph.Range.Font.Size = 12;
                wordparagraph.Range.Font.Bold = 0;
                wordparagraph.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
            }

            worddocument.Paragraphs.Add(ref oMissing);
            worddocument.Paragraphs.Add(ref oMissing);
            wordapp.Selection.EndKey(ref unit, ref extend);
            wordparagraph = worddocument.Paragraphs.Add(ref oMissing);
            wordparagraph.Range.Text = "2. �������������-��������������� ����������� �������� �����������";
            wordparagraph.Range.Font.Size = 14;
            wordparagraph.Range.Font.Bold = 1;
            wordparagraph.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

            worddocument.Paragraphs.Add(ref oMissing);
            wordapp.Selection.EndKey(ref unit, ref extend);
            wordparagraph = worddocument.Paragraphs.Add(ref oMissing);
            wordparagraph.Range.Text = "(������ ����������� ������ ������� ���������� � ����������� ������)";
            wordparagraph.Range.Font.Size = 13;
            wordparagraph.Range.Font.Bold = 0;
            wordparagraph.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

            //sqlCon = new SQLDBConnect((int)row["id"], "psycho_char", "SELECT date_meet, orientation, psycho_char, behavior FROM psycho_char WHERE (id_spec=" + row["id"].ToString() + ")");
            //sqlCon = new SQLDBConnect("psycho_char", "SELECT date_meet, orientation, psycho_char, behavior FROM psycho_char WHERE (id_spec=" + row["id"].ToString() + ")");
            sqlTable = sqlCon.GetDataTable("psycho_char", "SELECT date_meet, orientation, psycho_char, behavior FROM psycho_char WHERE (id_spec=" + row["id"].ToString() + ")");

            foreach (DataRow rowt in sqlTable.Rows)
            {
                worddocument.Paragraphs.Add(ref oMissing);
                worddocument.Paragraphs.Add(ref oMissing);
                wordapp.Selection.EndKey(ref unit, ref extend);
                wordparagraph = worddocument.Paragraphs.Add(ref oMissing);
                wordparagraph.Range.Text = getValidDate(rowt["date_meet"]);
                wordparagraph.Range.Font.Size = 13;
                wordparagraph.Range.Font.Bold = 1;
                wordparagraph.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;

                worddocument.Paragraphs.Add(ref oMissing);
                wordapp.Selection.EndKey(ref unit, ref extend);
                wordparagraph = worddocument.Paragraphs.Add(ref oMissing);
                wordparagraph.Range.Text = "2.1. �������������� ��������: ������ ���������� ������������, ���������� ����������, ��������� ����� � �.�";
                wordparagraph.Range.Font.Size = 13;
                wordparagraph.Range.Font.Bold = 0;
                wordparagraph.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;

                worddocument.Paragraphs.Add(ref oMissing);
                wordapp.Selection.EndKey(ref unit, ref extend);
                wordparagraph = worddocument.Paragraphs.Add(ref oMissing);
                wordparagraph.Range.Text = rowt["orientation"].ToString().Trim();
                wordparagraph.Range.Font.Size = 12;
                wordparagraph.Range.Font.Bold = 1;
                wordparagraph.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;

                worddocument.Paragraphs.Add(ref oMissing);
                worddocument.Paragraphs.Add(ref oMissing);
                wordapp.Selection.EndKey(ref unit, ref extend);
                wordparagraph = worddocument.Paragraphs.Add(ref oMissing);
                wordparagraph.Range.Text = "2.2. ���������������  �������������� (� ��������� � �� ���� �������������)";
                wordparagraph.Range.Font.Size = 13;
                wordparagraph.Range.Font.Bold = 0;
                wordparagraph.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;

                worddocument.Paragraphs.Add(ref oMissing);
                wordapp.Selection.EndKey(ref unit, ref extend);
                wordparagraph = worddocument.Paragraphs.Add(ref oMissing);
                wordparagraph.Range.Text = rowt["psycho_char"].ToString().Trim();
                wordparagraph.Range.Font.Size = 12;
                wordparagraph.Range.Font.Bold = 1;
                wordparagraph.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;

                worddocument.Paragraphs.Add(ref oMissing);
                worddocument.Paragraphs.Add(ref oMissing);
                wordapp.Selection.EndKey(ref unit, ref extend);
                wordparagraph = worddocument.Paragraphs.Add(ref oMissing);
                wordparagraph.Range.Text = "2.3. �������� � ��������� �����������, ������ ���������� �� ���������� ������������ ���������������� � ������ ����������� ���������� �������������� ����������";
                wordparagraph.Range.Font.Size = 13;
                wordparagraph.Range.Font.Bold = 0;
                wordparagraph.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;

                worddocument.Paragraphs.Add(ref oMissing);
                wordapp.Selection.EndKey(ref unit, ref extend);
                wordparagraph = worddocument.Paragraphs.Add(ref oMissing);
                wordparagraph.Range.Text = rowt["behavior"].ToString().Trim();
                wordparagraph.Range.Font.Size = 12;
                wordparagraph.Range.Font.Bold = 1;
                wordparagraph.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
            }

            worddocument.Paragraphs.Add(ref oMissing);
            worddocument.Paragraphs.Add(ref oMissing);
            wordapp.Selection.EndKey(ref unit, ref extend);
            wordparagraph = worddocument.Paragraphs.Add(ref oMissing);
            wordparagraph.Range.Text = "3. �������������� �������������� ������, ���������� � ����������";
            wordparagraph.Range.Font.Size = 14;
            wordparagraph.Range.Font.Bold = 1;
            wordparagraph.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

            worddocument.Paragraphs.Add(ref oMissing);
            worddocument.Paragraphs.Add(ref oMissing);
            wordapp.Selection.EndKey(ref unit, ref extend);
            wordparagraph = worddocument.Paragraphs.Add(ref oMissing);
            wordparagraph.Range.Text = "3.1. �������������� �������������� ������, ���������� � ���������� ����������� ������";
            wordparagraph.Range.Font.Size = 13;
            wordparagraph.Range.Font.Bold = 0;
            wordparagraph.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;

            worddocument.Paragraphs.Add(ref oMissing);
            worddocument.Paragraphs.Add(ref oMissing);
            wordapp.Selection.EndKey(ref unit, ref extend);
            wordparagraph = worddocument.Paragraphs.Add(ref oMissing);
            wordparagraph.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            wordtable = worddocument.Tables.Add(wordparagraph.Range, 1, 3, ref defaultTableBehavior, ref autoFitBehavior);
            wordtable.Columns[1].Width = 80;
            wordtable.Columns[2].Width = 300;
            wordtable.Columns[3].Width = 100;
            wordtable.Cell(1, 1).Range.Text = "����";
            wordtable.Cell(1, 2).Range.Text = "���������� ����������� � ���������� �������������� ������ (��� �������������� ������)";
            wordtable.Cell(1, 3).Range.Text = "����������";
            begCell = wordtable.Cell(1, 1).Range.Start;
            endCell = wordtable.Cell(1, 3).Range.End;
            wordcellrange = worddocument.Range(ref begCell, ref endCell);
            wordcellrange.Font.Bold = 1;
            wordcellrange.Font.Italic = 0;
            wordcellrange.Font.Size = 13;
            wordcellrange.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

            //sqlCon = new SQLDBConnect((int)row["id"], "ivr", "SELECT data_ivr, content, description FROM ivr WHERE ((id_spec=" + row["id"].ToString() + ") AND (work_type = 1))");
            //sqlCon = new SQLDBConnect("ivr", "SELECT data_ivr, content, description FROM ivr WHERE ((id_spec=" + row["id"].ToString() + ") AND (work_type = 1))");
            sqlTable = sqlCon.GetDataTable("ivr", "SELECT data_ivr, content, description FROM ivr WHERE ((id_spec=" + row["id"].ToString() + ") AND (id_type_ivr = 1))");

            foreach (DataRow rowt in sqlTable.Rows)
            {
                currentRow = wordtable.Rows.Add(ref oMissing);
                currentRow.Range.Font.Bold = 0;
                currentRow.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                currentRow.Cells[1].Range.Text = getValidDateShort(rowt["data_ivr"]);
                currentRow.Cells[2].Range.Text = rowt["content"].ToString();
                currentRow.Cells[3].Range.Text = rowt["description"].ToString();
            }

            worddocument.Paragraphs.Add(ref oMissing);
            wordapp.Selection.EndKey(ref unit, ref extend);
            wordparagraph = worddocument.Paragraphs.Add(ref oMissing);
            wordparagraph.Range.Text = "3.2. �������������� �������������� ������, ���������� � ���������� ������� ������ ������������ ������";
            wordparagraph.Range.Font.Size = 13;
            wordparagraph.Range.Font.Bold = 0;
            wordparagraph.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;

            worddocument.Paragraphs.Add(ref oMissing);
            worddocument.Paragraphs.Add(ref oMissing);
            wordapp.Selection.EndKey(ref unit, ref extend);
            wordparagraph = worddocument.Paragraphs.Add(ref oMissing);
            wordparagraph.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            wordtable = worddocument.Tables.Add(wordparagraph.Range, 1, 4, ref defaultTableBehavior, ref autoFitBehavior);
            wordtable.Columns[1].Width = 80;
            wordtable.Columns[2].Width = 120;
            wordtable.Columns[3].Width = 180;
            wordtable.Columns[4].Width = 100;
            wordtable.Cell(1, 1).Range.Text = "����";
            wordtable.Cell(1, 2).Range.Text = "�.�.�., ������, ��������� ����� ���";
            wordtable.Cell(1, 3).Range.Text = "���������� ����������� � ���������� �������������� ������ (��� �������������� ������)";
            wordtable.Cell(1, 4).Range.Text = "����������";
            begCell = wordtable.Cell(1, 1).Range.Start;
            endCell = wordtable.Cell(1, 4).Range.End;
            wordcellrange = worddocument.Range(ref begCell, ref endCell);
            wordcellrange.Font.Bold = 1;
            wordcellrange.Font.Italic = 0;
            wordcellrange.Font.Size = 13;
            wordcellrange.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

            //sqlCon = new SQLDBConnect((int)row["id"], "ivr", "SELECT ivr.data_ivr AS data_ivr, ivr.content AS content, ivr.description AS description, employee.last_name as last_name, employee.first_name as first_name, employee.patronymic as patronymic, employee.rank as rank, employee.post as post FROM ivr LEFT JOIN employee ON (ivr.employee_id = employee.id)  WHERE ((id_spec=" + row["id"].ToString() + ") AND (work_type = 2))");
            //sqlCon = new SQLDBConnect("ivr", "SELECT ivr.data_ivr AS data_ivr, ivr.content AS content, ivr.description AS description, employee.last_name as last_name, employee.first_name as first_name, employee.patronymic as patronymic, employee.rank as rank, employee.post as post FROM ivr LEFT JOIN employee ON (ivr.employee_id = employee.id)  WHERE ((id_spec=" + row["id"].ToString() + ") AND (work_type = 2))");
            sqlTable = sqlCon.GetDataTable("ivr2", "SELECT ivr.data_ivr AS data_ivr, ivr.content AS content, ivr.description AS description, employee.last_name as last_name, employee.first_name as first_name, employee.patronymic as patronymic, employee.rank as rank, employee.post as post FROM ivr LEFT JOIN employee ON (ivr.employee_id = employee.id)  WHERE ((id_spec=" + row["id"].ToString() + ") AND (id_type_ivr = 2))");

            foreach (DataRow rowt in sqlTable.Rows)
            {
                currentRow = wordtable.Rows.Add(ref oMissing);
                currentRow.Range.Font.Bold = 0;
                currentRow.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                currentRow.Cells[1].Range.Text = getValidDateShort(rowt["data_ivr"]);
                currentRow.Cells[2].Range.Text = rowt["last_name"].ToString() + " " + rowt["first_name"].ToString() + " " + rowt["patronymic"].ToString() + ", " + rowt["rank"].ToString() + ", " + rowt["post"].ToString();
                currentRow.Cells[3].Range.Text = rowt["content"].ToString();
                currentRow.Cells[4].Range.Text = rowt["description"].ToString();
            }

            worddocument.Paragraphs.Add(ref oMissing);
            wordapp.Selection.EndKey(ref unit, ref extend);
            wordparagraph = worddocument.Paragraphs.Add(ref oMissing);
            wordparagraph.Range.Text = "3.3. �������������� �������������� ������, ���������� � ���������� ����� ������������ ��������������� ����������";
            wordparagraph.Range.Font.Size = 13;
            wordparagraph.Range.Font.Bold = 0;
            wordparagraph.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;

            worddocument.Paragraphs.Add(ref oMissing);
            worddocument.Paragraphs.Add(ref oMissing);
            wordapp.Selection.EndKey(ref unit, ref extend);
            wordparagraph = worddocument.Paragraphs.Add(ref oMissing);
            wordparagraph.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            wordtable = worddocument.Tables.Add(wordparagraph.Range, 1, 4, ref defaultTableBehavior, ref autoFitBehavior);
            wordtable.Columns[1].Width = 80;
            wordtable.Columns[2].Width = 120;
            wordtable.Columns[3].Width = 180;
            wordtable.Columns[4].Width = 100;
            wordtable.Cell(1, 1).Range.Text = "����";
            wordtable.Cell(1, 2).Range.Text = "�.�.�., ������, ��������� ����� ���";
            wordtable.Cell(1, 3).Range.Text = "���������� ����������� � ���������� �������������� ������ (��� �������������� ������)";
            wordtable.Cell(1, 4).Range.Text = "����������";
            begCell = wordtable.Cell(1, 1).Range.Start;
            endCell = wordtable.Cell(1, 4).Range.End;
            wordcellrange = worddocument.Range(ref begCell, ref endCell);
            wordcellrange.Font.Bold = 1;
            wordcellrange.Font.Italic = 0;
            wordcellrange.Font.Size = 13;
            wordcellrange.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

            //sqlCon = new SQLDBConnect((int)row["id"], "ivr", "SELECT ivr.data_ivr AS data_ivr, ivr.content AS content, ivr.description AS description, employee.last_name as last_name, employee.first_name as first_name, employee.patronymic as patronymic, employee.rank as rank, employee.post as post FROM ivr LEFT JOIN employee ON (ivr.employee_id = employee.id) WHERE ((id_spec=" + row["id"].ToString() + ") AND (work_type = 3))");
            //sqlCon = new SQLDBConnect("ivr", "SELECT ivr.data_ivr AS data_ivr, ivr.content AS content, ivr.description AS description, employee.last_name as last_name, employee.first_name as first_name, employee.patronymic as patronymic, employee.rank as rank, employee.post as post FROM ivr LEFT JOIN employee ON (ivr.employee_id = employee.id) WHERE ((id_spec=" + row["id"].ToString() + ") AND (work_type = 3))");
            sqlTable = sqlCon.GetDataTable("ivr3", "SELECT ivr.data_ivr AS data_ivr, ivr.content AS content, ivr.description AS description, employee.last_name as last_name, employee.first_name as first_name, employee.patronymic as patronymic, employee.rank as rank, employee.post as post FROM ivr LEFT JOIN employee ON (ivr.employee_id = employee.id) WHERE ((id_spec=" + row["id"].ToString() + ") AND (id_type_ivr = 3))");
            foreach (DataRow rowt in sqlTable.Rows)
            {
                currentRow = wordtable.Rows.Add(ref oMissing);
                currentRow.Range.Font.Bold = 0;
                currentRow.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                currentRow.Cells[1].Range.Text = getValidDateShort(rowt["data_ivr"]);
                currentRow.Cells[2].Range.Text = rowt["last_name"].ToString() + " " + rowt["first_name"].ToString() + " " + rowt["patronymic"].ToString() + ", " + rowt["rank"].ToString() + ", " + rowt["post"].ToString();
                currentRow.Cells[3].Range.Text = rowt["content"].ToString();
                currentRow.Cells[4].Range.Text = rowt["description"].ToString();
            }

            worddocument.Paragraphs.Add(ref oMissing);
            wordapp.Selection.EndKey(ref unit, ref extend);
            wordparagraph = worddocument.Paragraphs.Add(ref oMissing);
            wordparagraph.Range.Text = "3.4. ������� ������ ������������ ������ � ��������� �����������";
            wordparagraph.Range.Font.Size = 13;
            wordparagraph.Range.Font.Bold = 0;
            wordparagraph.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;

            worddocument.Paragraphs.Add(ref oMissing);
            worddocument.Paragraphs.Add(ref oMissing);
            wordapp.Selection.EndKey(ref unit, ref extend);
            wordparagraph = worddocument.Paragraphs.Add(ref oMissing);
            wordparagraph.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            wordtable = worddocument.Tables.Add(wordparagraph.Range, 1, 3, ref defaultTableBehavior, ref autoFitBehavior);
            wordtable.Columns[1].Width = 100;
            wordtable.Columns[2].Width = 200;
            wordtable.Columns[3].Width = 170;
            wordtable.Cell(1, 1).Range.Text = "���� ��������� ���";
            wordtable.Cell(1, 2).Range.Text = "������� ���, �������� � ��������� �����������";
            wordtable.Cell(1, 3).Range.Text = "����������";
            begCell = wordtable.Cell(1, 1).Range.Start;
            endCell = wordtable.Cell(1, 3).Range.End;
            wordcellrange = worddocument.Range(ref begCell, ref endCell);
            wordcellrange.Font.Bold = 1;
            wordcellrange.Font.Italic = 0;
            wordcellrange.Font.Size = 13;
            wordcellrange.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

            //sqlCon = new SQLDBConnect((int)row["id"], "resolution", "SELECT date_resolution, resolution, description FROM resolution WHERE (id_spec=" + row["id"].ToString() + ")");
            //sqlCon = new SQLDBConnect("resolution", "SELECT date_resolution, resolution, description FROM resolution WHERE (id_spec=" + row["id"].ToString() + ")");
            sqlTable = sqlCon.GetDataTable("resolution", "SELECT date_resolution, resolution, description FROM resolution WHERE (id_spec=" + row["id"].ToString() + ")");

            foreach (DataRow rowt in sqlTable.Rows)
            {
                currentRow = wordtable.Rows.Add(ref oMissing);
                currentRow.Range.Font.Bold = 0;
                currentRow.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                currentRow.Cells[1].Range.Text = getValidDateShort(rowt["date_resolution"]);
                currentRow.Cells[2].Range.Text = rowt["resolution"].ToString();
                currentRow.Cells[3].Range.Text = rowt["description"].ToString();
            }

            worddocument.Paragraphs.Add(ref oMissing);
            worddocument.Paragraphs.Add(ref oMissing);
            wordapp.Selection.EndKey(ref unit, ref extend);
            wordparagraph = worddocument.Paragraphs.Add(ref oMissing);
            wordparagraph.Range.Text = "4. ���� ��������� � ���������";
            wordparagraph.Range.Font.Size = 13;
            wordparagraph.Range.Font.Bold = 1;
            wordparagraph.Range.Font.Underline = Word.WdUnderline.wdUnderlineNone;
            wordparagraph.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

            worddocument.Paragraphs.Add(ref oMissing);
            worddocument.Paragraphs.Add(ref oMissing);
            wordapp.Selection.EndKey(ref unit, ref extend);
            wordparagraph = worddocument.Paragraphs.Add(ref oMissing);
            wordparagraph.Range.Text = "4.1. ���� ���������, ����������� �����������";
            wordparagraph.Range.Font.Size = 13;
            wordparagraph.Range.Font.Bold = 0;
            wordparagraph.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;

            worddocument.Paragraphs.Add(ref oMissing);
            worddocument.Paragraphs.Add(ref oMissing);
            wordapp.Selection.EndKey(ref unit, ref extend);
            wordparagraph = worddocument.Paragraphs.Add(ref oMissing);
            wordparagraph.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            wordtable = worddocument.Tables.Add(wordparagraph.Range, 1, 6, ref defaultTableBehavior, ref autoFitBehavior);
            wordtable.Columns[1].Width = 30;
            wordtable.Columns[2].Width = 60;
            wordtable.Columns[3].Width = 100;
            wordtable.Columns[4].Width = 150;
            wordtable.Columns[5].Width = 80;
            wordtable.Columns[6].Width = 80;
            wordtable.Cell(1, 1).Range.Text = "�";
            wordtable.Cell(1, 2).Range.Text = "����";
            wordtable.Cell(1, 3).Range.Text = "��� ���������";
            wordtable.Cell(1, 4).Range.Text = "�� ��� ��������� ���������";
            wordtable.Cell(1, 5).Range.Text = "��� �������";
            wordtable.Cell(1, 6).Range.Text = "����, � �������";

            begCell = wordtable.Cell(1, 1).Range.Start;
            endCell = wordtable.Cell(1, 6).Range.End;
            wordcellrange = worddocument.Range(ref begCell, ref endCell);
            wordcellrange.Font.Bold = 1;
            wordcellrange.Font.Italic = 0;
            wordcellrange.Font.Size = 13;
            wordcellrange.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

            //sqlCon = new SQLDBConnect((int)row["id"], "bonus", "SELECT bonus.date_bonus AS date_bonus, bonus.bonus_reason AS bonus_reason, spr_bonus_type.name AS bonus_type, spr_performers.name AS performers, bonus.order_date AS order_date, bonus.order_number AS order_number FROM spr_performers RIGHT JOIN (spr_bonus_type RIGHT JOIN bonus ON spr_bonus_type.id=bonus.bonus_type_id) ON spr_performers.id=bonus.performer_id WHERE (bonus.id_spec=" + row["id"].ToString() + ")");
            //sqlCon = new SQLDBConnect("bonus", "SELECT bonus.date_bonus AS date_bonus, bonus.bonus_reason AS bonus_reason, spr_bonus_type.name AS bonus_type, spr_performers.name AS performers, bonus.order_date AS order_date, bonus.order_number AS order_number FROM spr_performers RIGHT JOIN (spr_bonus_type RIGHT JOIN bonus ON spr_bonus_type.id=bonus.bonus_type_id) ON spr_performers.id=bonus.performer_id WHERE (bonus.id_spec=" + row["id"].ToString() + ")");
            sqlTable = sqlCon.GetDataTable("bonus", "SELECT bonus.date_bonus AS date_bonus, bonus.bonus_reason AS bonus_reason, spr_bonus_type.name AS bonus_type, spr_performers.name AS performers, bonus.order_date AS order_date, bonus.order_number AS order_number FROM spr_performers RIGHT JOIN (spr_bonus_type RIGHT JOIN bonus ON spr_bonus_type.id=bonus.bonus_type_id) ON spr_performers.id=bonus.performer_id WHERE (bonus.id_spec=" + row["id"].ToString() + ")");

            i = 0;
            foreach (DataRow rowt in sqlTable.Rows)
            {
                i = i + 1;
                currentRow = wordtable.Rows.Add(ref oMissing);
                currentRow.Range.Font.Bold = 0;
                currentRow.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                currentRow.Cells[1].Range.Text = i.ToString();
                currentRow.Cells[2].Range.Text = getValidDateShort(rowt["date_bonus"]);
                currentRow.Cells[3].Range.Text = rowt["bonus_type"].ToString();
                currentRow.Cells[4].Range.Text = rowt["bonus_reason"].ToString();
                currentRow.Cells[5].Range.Text = rowt["performers"].ToString();
                currentRow.Cells[6].Range.Text = getValidDateShort(rowt["order_date"]) + ", " + rowt["order_number"].ToString();
            }

            worddocument.Paragraphs.Add(ref oMissing);
            worddocument.Paragraphs.Add(ref oMissing);
            wordapp.Selection.EndKey(ref unit, ref extend);
            wordparagraph = worddocument.Paragraphs.Add(ref oMissing);
            wordparagraph.Range.Text = "4.2. ���� ������ ���������, ���������� �� ����������� (��� �������������)";
            wordparagraph.Range.Font.Size = 13;
            wordparagraph.Range.Font.Bold = 0;
            wordparagraph.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;

            worddocument.Paragraphs.Add(ref oMissing);
            worddocument.Paragraphs.Add(ref oMissing);
            wordapp.Selection.EndKey(ref unit, ref extend);
            wordparagraph = worddocument.Paragraphs.Add(ref oMissing);
            wordparagraph.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            wordtable = worddocument.Tables.Add(wordparagraph.Range, 1, 7, ref defaultTableBehavior, ref autoFitBehavior);
            wordtable.Columns[1].Width = 30;
            wordtable.Columns[2].Width = 60;
            wordtable.Columns[3].Width = 70;
            wordtable.Columns[4].Width = 100;
            wordtable.Columns[5].Width = 80;
            wordtable.Columns[6].Width = 80;
            wordtable.Columns[7].Width = 70;
            wordtable.Cell(1, 1).Range.Text = "�";
            wordtable.Cell(1, 2).Range.Text = "����";
            wordtable.Cell(1, 3).Range.Text = "��� ���������";
            wordtable.Cell(1, 4).Range.Text = "�� ��� �������� ���������";
            wordtable.Cell(1, 5).Range.Text = "��� �������� ���������";
            wordtable.Cell(1, 6).Range.Text = "������� �� ������������ ����������� (������� �����.)";
            wordtable.Cell(1, 7).Range.Text = "������� � ������ ���������";

            begCell = wordtable.Cell(1, 1).Range.Start;
            endCell = wordtable.Cell(1, 7).Range.End;
            wordcellrange = worddocument.Range(ref begCell, ref endCell);
            wordcellrange.Font.Bold = 1;
            wordcellrange.Font.Italic = 0;
            wordcellrange.Font.Size = 13;
            wordcellrange.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

            //sqlCon = new SQLDBConnect((int)row["id"], "penalty", "SELECT penalty.date_penalty AS date_penalty, penalty.reason AS reason, spr_penalty_type.name AS penalty_type, penalty.order_date AS order_date, penalty.order_number AS order_number, penalty.removal AS removal, spr_performers.name AS performers FROM spr_performers RIGHT JOIN (spr_penalty_type RIGHT JOIN penalty ON spr_penalty_type.id=penalty.penalty_type_id) ON spr_performers.id=penalty.performer_id WHERE ((penalty.id_spec=" + row["id"].ToString() + ") AND (oral=true))");
            //sqlCon = new SQLDBConnect("penalty", "SELECT penalty.date_penalty AS date_penalty, penalty.reason AS reason, spr_penalty_type.name AS penalty_type, penalty.order_date AS order_date, penalty.order_number AS order_number, penalty.removal AS removal, spr_performers.name AS performers FROM spr_performers RIGHT JOIN (spr_penalty_type RIGHT JOIN penalty ON spr_penalty_type.id=penalty.penalty_type_id) ON spr_performers.id=penalty.performer_id WHERE ((penalty.id_spec=" + row["id"].ToString() + ") AND (oral=true))");
            sqlTable = sqlCon.GetDataTable("penalty", "SELECT penalty.date_penalty AS date_penalty, penalty.reason AS reason, spr_penalty_type.name AS penalty_type, penalty.order_date AS order_date, penalty.order_number AS order_number, penalty.removal AS removal, spr_performers.name AS performers FROM spr_performers RIGHT JOIN (spr_penalty_type RIGHT JOIN penalty ON spr_penalty_type.id=penalty.penalty_type_id) ON spr_performers.id=penalty.performer_id WHERE ((penalty.id_spec=" + row["id"].ToString() + ") AND (oral=true))");

            i = 0;
            foreach (DataRow rowt in sqlTable.Rows)
            {
                i = i + 1;
                currentRow = wordtable.Rows.Add(ref oMissing);
                currentRow.Range.Font.Bold = 0;
                currentRow.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                currentRow.Cells[1].Range.Text = i.ToString();
                currentRow.Cells[2].Range.Text = getValidDateShort(rowt["date_penalty"]);
                currentRow.Cells[3].Range.Text = rowt["penalty_type"].ToString();
                currentRow.Cells[4].Range.Text = rowt["reason"].ToString();
                currentRow.Cells[5].Range.Text = rowt["performers"].ToString();
                currentRow.Cells[7].Range.Text = rowt["removal"].ToString();
            }

            worddocument.Paragraphs.Add(ref oMissing);
            worddocument.Paragraphs.Add(ref oMissing);
            wordapp.Selection.EndKey(ref unit, ref extend);
            wordparagraph = worddocument.Paragraphs.Add(ref oMissing);
            wordparagraph.Range.Text = "4.3. ����  ���������,  ���������� �� ����������� �� �������������� ����������� ��";
            wordparagraph.Range.Font.Size = 13;
            wordparagraph.Range.Font.Bold = 0;
            wordparagraph.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;

            worddocument.Paragraphs.Add(ref oMissing);
            worddocument.Paragraphs.Add(ref oMissing);
            wordapp.Selection.EndKey(ref unit, ref extend);
            wordparagraph = worddocument.Paragraphs.Add(ref oMissing);
            wordparagraph.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            wordtable = worddocument.Tables.Add(wordparagraph.Range, 1, 6, ref defaultTableBehavior, ref autoFitBehavior);
            wordtable.Columns[1].Width = 30;
            wordtable.Columns[2].Width = 60;
            wordtable.Columns[3].Width = 80;
            wordtable.Columns[4].Width = 130;
            wordtable.Columns[5].Width = 100;
            wordtable.Columns[6].Width = 80;
            wordtable.Cell(1, 1).Range.Text = "�";
            wordtable.Cell(1, 2).Range.Text = "����";
            wordtable.Cell(1, 3).Range.Text = "��� ���������";
            wordtable.Cell(1, 4).Range.Text = "�� ��� �������� ���������";
            wordtable.Cell(1, 5).Range.Text = "��� �������� ���������";
            wordtable.Cell(1, 6).Range.Text = "������� � ������ ���������";

            begCell = wordtable.Cell(1, 1).Range.Start;
            endCell = wordtable.Cell(1, 6).Range.End;
            wordcellrange = worddocument.Range(ref begCell, ref endCell);
            wordcellrange.Font.Bold = 1;
            wordcellrange.Font.Italic = 0;
            wordcellrange.Font.Size = 13;
            wordcellrange.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

            //sqlCon = new SQLDBConnect((int)row["id"], "penalty", "SELECT penalty.date_penalty AS date_penalty, penalty.reason AS reason, spr_penalty_type.name AS penalty_type, penalty.order_date AS order_date, penalty.order_number AS order_number, penalty.removal AS removal, spr_performers.name AS performers FROM spr_performers RIGHT JOIN (spr_penalty_type RIGHT JOIN penalty ON spr_penalty_type.id=penalty.penalty_type_id) ON spr_performers.id=penalty.performer_id WHERE ((penalty.id_spec=" + row["id"].ToString() + ") AND (oral=false))");
            //sqlCon = new SQLDBConnect("penalty", "SELECT penalty.date_penalty AS date_penalty, penalty.reason AS reason, spr_penalty_type.name AS penalty_type, penalty.order_date AS order_date, penalty.order_number AS order_number, penalty.removal AS removal, spr_performers.name AS performers FROM spr_performers RIGHT JOIN (spr_penalty_type RIGHT JOIN penalty ON spr_penalty_type.id=penalty.penalty_type_id) ON spr_performers.id=penalty.performer_id WHERE ((penalty.id_spec=" + row["id"].ToString() + ") AND (oral=false))");
            sqlTable = sqlCon.GetDataTable("penalty2", "SELECT penalty.date_penalty AS date_penalty, penalty.reason AS reason, spr_penalty_type.name AS penalty_type, penalty.order_date AS order_date, penalty.order_number AS order_number, penalty.removal AS removal, spr_performers.name AS performers FROM spr_performers RIGHT JOIN (spr_penalty_type RIGHT JOIN penalty ON spr_penalty_type.id=penalty.penalty_type_id) ON spr_performers.id=penalty.performer_id WHERE ((penalty.id_spec=" + row["id"].ToString() + ") AND (oral=false))");

            i = 0;
            foreach (DataRow rowt in sqlTable.Rows)
            {
                i = i + 1;
                currentRow = wordtable.Rows.Add(ref oMissing);
                currentRow.Range.Font.Bold = 0;
                currentRow.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                currentRow.Cells[1].Range.Text = i.ToString();
                currentRow.Cells[2].Range.Text = getValidDateShort(rowt["date_penalty"]);
                currentRow.Cells[3].Range.Text = rowt["penalty_type"].ToString();
                currentRow.Cells[4].Range.Text = rowt["reason"].ToString();
                currentRow.Cells[5].Range.Text = rowt["performers"].ToString();
                currentRow.Cells[6].Range.Text = rowt["removal"].ToString();
            }

            worddocument.Paragraphs.Add(ref oMissing);
            worddocument.Paragraphs.Add(ref oMissing);
            wordapp.Selection.EndKey(ref unit, ref extend);
            wordparagraph = worddocument.Paragraphs.Add(ref oMissing);
            wordparagraph.Range.Text = "5. ���������� ������ �� ���������� ����������� � ������������, ��� ��������� � �������� ����������";
            wordparagraph.Range.Font.Size = 13;
            wordparagraph.Range.Font.Bold = 1;
            wordparagraph.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

            worddocument.Paragraphs.Add(ref oMissing);
            wordapp.Selection.EndKey(ref unit, ref extend);
            wordparagraph = worddocument.Paragraphs.Add(ref oMissing);
            begCell = wordparagraph.Range.Start;
            wordparagraph.Range.Font.Size = 13;
            wordparagraph.Range.Font.Bold = 0;
//            wordparagraph.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
//            wordparagraph.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
            wordparagraph.Range.Text = row["result"].ToString().Trim();
            endCell = wordparagraph.Range.End;

            wordcellrange = worddocument.Range(ref begCell, ref endCell);
            wordcellrange.Font.Bold = 0;
            wordcellrange.Font.Italic = 0;
            wordcellrange.Font.Size = 13;
            wordcellrange.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;

            worddocument.Paragraphs.Add(ref oMissing);
            worddocument.Paragraphs.Add(ref oMissing);
            wordapp.Selection.EndKey(ref unit, ref extend);
            wordparagraph = worddocument.Paragraphs.Add(ref oMissing);
            wordparagraph.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            wordtable = worddocument.Tables.Add(wordparagraph.Range, 1, 3, ref defaultTableBehavior, ref autoFitBehavior);
            wordtable.Columns[1].Width = 60;
            wordtable.Columns[2].Width = 300;
            wordtable.Columns[3].Width = 100;
            wordtable.Cell(1, 1).Range.Text = "����";
            wordtable.Cell(1, 2).Range.Text = "�����";
            wordtable.Cell(1, 3).Range.Text = "����������";

            begCell = wordtable.Cell(1, 1).Range.Start;
            endCell = wordtable.Cell(1, 3).Range.End;
            wordcellrange = worddocument.Range(ref begCell, ref endCell);
            wordcellrange.Font.Bold = 1;
            wordcellrange.Font.Italic = 0;
            wordcellrange.Font.Size = 13;
            wordcellrange.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

            sqlTable = sqlCon.GetDataTable("results", "SELECT * FROM results WHERE (id_spec=" + row["id"].ToString() + ")");

            i = 0;
            foreach (DataRow rowt in sqlTable.Rows)
            {
                currentRow = wordtable.Rows.Add(ref oMissing);
                currentRow.Range.Font.Bold = 0;
                currentRow.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                currentRow.Cells[1].Range.Text = getValidDateShort(rowt["result_date"]);
                currentRow.Cells[2].Range.Text = rowt["content"].ToString();
                currentRow.Cells[3].Range.Text = rowt["description"].ToString();
            }
        
        }

        public void BonusAndPenalty(DataRow row)
        {
            Word.Application wordapp;
            Word.Document worddocument;
            Word.Paragraph wordparagraph;

            wordapp = new Word.Application();
            wordapp.Visible = true;
            Object template = Type.Missing;
            Object newTemplate = false;
            Object documentType = Word.WdNewDocumentType.wdNewBlankDocument;
            Object visible = true;
            worddocument = wordapp.Documents.Add(ref template, ref newTemplate, ref documentType, ref visible);

            //            wordapp.Documents.Add(ref template, ref newTemplate, ref documentType, ref visible);
            //            template = @"C:\a1.doc";
            //            wordapp.Documents.Add(
            //            ref template, ref newTemplate, ref documentType, ref visible);
            //           worddocuments = wordapp.Documents;
            //            Object name = "��������1";
            //            worddocument = (Word.Document)worddocuments.get_Item(ref name);
            worddocument.Activate();

            object oMissing = System.Reflection.Missing.Value;
            object unit;
            object extend;
            unit = Word.WdUnits.wdStory;
            extend = Word.WdMovementType.wdMove;

            //          wordparagraphs = worddocument.Paragraphs;

            wordparagraph = worddocument.Paragraphs.Add(ref oMissing);
            wordparagraph.Range.Text = "� � � � � � �";
            wordparagraph.Range.Font.Color = Word.WdColor.wdColorBlack;
            wordparagraph.Range.Font.Size = 14;
            wordparagraph.Range.Font.Name = "Times New Roman";
            wordparagraph.Range.Font.Bold = 1;
            wordparagraph.Range.Font.Underline = Word.WdUnderline.wdUnderlineNone;
            wordparagraph.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

            worddocument.Paragraphs.Add(ref oMissing);
            wordapp.Selection.EndKey(ref unit, ref extend);
            wordparagraph = worddocument.Paragraphs.Add(ref oMissing);
            wordparagraph.Range.Text = "� ���������� � ����������";
            wordparagraph.Range.Font.Color = Word.WdColor.wdColorBlack;
            wordparagraph.Range.Font.Size = 12;
            wordparagraph.Range.Font.Bold = 0;

            worddocument.Paragraphs.Add(ref oMissing);
            worddocument.Paragraphs.Add(ref oMissing);
            wordapp.Selection.EndKey(ref unit, ref extend);
            wordparagraph = worddocument.Paragraphs.Add(ref oMissing);
            wordparagraph.Range.Text = "�.�.�. ";
            wordparagraph.Range.Font.Size = 13;
            wordparagraph.Range.Font.Bold = 0;
            wordparagraph.Range.Font.Underline = Word.WdUnderline.wdUnderlineNone;
            wordparagraph.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;

            Object begin = wordparagraph.Range.End - 1;
            Object end = wordparagraph.Range.End;
            Word.Range wordrange = worddocument.Range(ref begin, ref end);
            wordrange.Select();
            wordrange.Text = row["last_name"].ToString().Trim() + " " + row["first_name"].ToString().Trim() + " " + row["patronymic"].ToString().Trim();
            wordrange.Font.Size = 13;
            wordrange.Font.Bold = 1;
            //            wordrange.Font.Underline = Word.WdUnderline.wdUnderlineSingle;
            //            wordrange.Font.Color = Word.WdColor.wdColorRed;


            worddocument.Paragraphs.Add(ref oMissing);
            worddocument.Paragraphs.Add(ref oMissing);
            wordapp.Selection.EndKey(ref unit, ref extend);
            wordparagraph = worddocument.Paragraphs.Add(ref oMissing);
            wordparagraph.Range.Text = "���������: ";
            wordparagraph.Range.Font.Size = 13;
            wordparagraph.Range.Font.Bold = 1;
            wordparagraph.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
            wordparagraph.Range.Font.Underline = Word.WdUnderline.wdUnderlineNone;

            worddocument.Paragraphs.Add(ref oMissing);
            worddocument.Paragraphs.Add(ref oMissing);
            wordapp.Selection.EndKey(ref unit, ref extend);
            wordparagraph = worddocument.Paragraphs.Add(ref oMissing);
            wordparagraph.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            wordrange = wordparagraph.Range;
            Object defaultTableBehavior = Word.WdDefaultTableBehavior.wdWord9TableBehavior;
            Object autoFitBehavior = Word.WdAutoFitBehavior.wdAutoFitWindow;
            Word.Table wordtable1 = worddocument.Tables.Add(wordrange, 1, 4, ref defaultTableBehavior, ref autoFitBehavior);
            wordtable1.Columns[1].Width = 50;
            wordtable1.Columns[2].Width = 80;
            wordtable1.Columns[3].Width = 200;
            wordtable1.Columns[4].Width = 170;
            Word.Range wordcellrange = wordtable1.Cell(1, 1).Range;
            wordcellrange.Text = "� �/�";
            wordcellrange = wordtable1.Cell(1, 2).Range;
            wordcellrange.Text = "����";
            wordcellrange = wordtable1.Cell(1, 3).Range;
            wordcellrange.Text = "�� ��� ������";
            wordcellrange = wordtable1.Cell(1, 4).Range;
            wordcellrange.Text = "��� ���������";

            object begCell = wordtable1.Cell(1, 1).Range.Start;
            object endCell = wordtable1.Cell(1, 4).Range.End;
            wordcellrange = worddocument.Range(ref begCell, ref endCell);
            wordcellrange.Font.Bold = 1;
            wordcellrange.Font.Italic = 0;
            wordcellrange.Font.Size = 12;
            wordcellrange.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

            DataTable bonusTable = sqlCon.GetDataTable("bonus", "SELECT b1.date_bonus AS date_bonus, b1.bonus_reason AS bonus_reason, spr_bonus_type.name AS bonus_type FROM bonus AS b1 LEFT JOIN spr_bonus_type ON spr_bonus_type.id=b1.bonus_type_id WHERE (b1.id_spec=" + row["id"].ToString() + ") ORDER BY date_bonus");

            Word.Row currentRow;
            int i = 0;

            try
            {
                foreach (DataRow rowt in bonusTable.Rows)
                {
                    i++;
                    currentRow = wordtable1.Rows.Add(ref oMissing);
                    currentRow.Range.Font.Bold = 0;
                    currentRow.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                    currentRow.Cells[1].Range.Text = i.ToString();
                    if (!Convert.IsDBNull(rowt["date_bonus"]))
                        currentRow.Cells[2].Range.Text = ((DateTime)rowt["date_bonus"]).ToString("d");
                    if (!Convert.IsDBNull(rowt["bonus_reason"]))
                        currentRow.Cells[3].Range.Text = rowt["bonus_reason"].ToString();
                    if (!Convert.IsDBNull(rowt["bonus_type"]))
                        currentRow.Cells[4].Range.Text = rowt["bonus_type"].ToString();

                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "������");
            }
            worddocument.Paragraphs.Add(ref oMissing);
            worddocument.Paragraphs.Add(ref oMissing);
            wordapp.Selection.EndKey(ref unit, ref extend);
            wordparagraph = worddocument.Paragraphs.Add(ref oMissing);
            wordparagraph.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
            wordparagraph.Range.Text = "���������: ";
            wordparagraph.Range.Font.Size = 13;
            wordparagraph.Range.Font.Bold = 1;

            worddocument.Paragraphs.Add(ref oMissing);
            worddocument.Paragraphs.Add(ref oMissing);
            wordapp.Selection.EndKey(ref unit, ref extend);
            wordparagraph = worddocument.Paragraphs.Add(ref oMissing);
            wordrange = wordparagraph.Range;
            Word.Table wordtable2 = worddocument.Tables.Add(wordrange, 1, 5, ref defaultTableBehavior, ref autoFitBehavior);
            wordtable2.Columns[1].Width = 50;
            wordtable2.Columns[2].Width = 80;
            wordtable2.Columns[3].Width = 180;
            wordtable2.Columns[4].Width = 110;
            wordtable2.Columns[5].Width = 80;
            wordcellrange = wordtable2.Cell(1, 1).Range;
            wordcellrange.Text = "� �/�";
            wordcellrange = wordtable2.Cell(1, 2).Range;
            wordcellrange.Text = "���� ���������";
            wordcellrange = wordtable2.Cell(1, 3).Range;
            wordcellrange.Text = "�������� ���������";
            wordcellrange = wordtable2.Cell(1, 4).Range;
            wordcellrange.Text = "��� ���������";
            wordcellrange = wordtable2.Cell(1, 5).Range;
            wordcellrange.Text = "������� � ������, ���������";

            begCell = wordtable2.Cell(1, 1).Range.Start;
            endCell = wordtable2.Cell(1, 5).Range.End;
            wordcellrange = worddocument.Range(ref begCell, ref endCell);
            wordcellrange.Font.Bold = 1;
            wordcellrange.Font.Italic = 0;
            wordcellrange.Font.Size = 12;
            wordcellrange.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

            DataTable penaltyTable = sqlCon.GetDataTable("penalty3", "SELECT p1.date_penalty AS date_penalty, p1.reason AS reason, p1.removal AS removal, spr_penalty_type.name AS penalty_type FROM penalty AS p1 LEFT JOIN spr_penalty_type ON spr_penalty_type.id=p1.penalty_type_id WHERE (p1.id_spec=" + row["id"].ToString() + ") ORDER BY date_penalty");
            i = 0;

            try
            {
                foreach (DataRow rowt in penaltyTable.Rows)
                {
                    i++;
                    currentRow = wordtable2.Rows.Add(ref oMissing);
                    currentRow.Range.Font.Bold = 0;
                    currentRow.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                    currentRow.Cells[1].Range.Text = i.ToString();
                    if (!Convert.IsDBNull(rowt["date_penalty"]))
                        currentRow.Cells[2].Range.Text = ((DateTime)rowt["date_penalty"]).ToString("d");
                    if (!Convert.IsDBNull(rowt["reason"]))
                        currentRow.Cells[3].Range.Text = rowt["reason"].ToString();
                    if (!Convert.IsDBNull(rowt["penalty_type"]))
                        currentRow.Cells[4].Range.Text = rowt["penalty_type"].ToString();
                    if (!Convert.IsDBNull(rowt["removal"]))
                        currentRow.Cells[5].Range.Text = rowt["removal"].ToString();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "������");
            }

            worddocument.Paragraphs.Add(ref oMissing);
            worddocument.Paragraphs.Add(ref oMissing);
            wordapp.Selection.EndKey(ref unit, ref extend);
            wordparagraph = worddocument.Paragraphs.Add(ref oMissing);

            DateTime thisDay = DateTime.Today;
            wordparagraph.Range.Font.Bold = 0;
            wordparagraph.Range.Text = thisDay.ToString("D") + "          ��������� ������: __________________________";
        }

        public void ReportFilling()
        {
            DateTime dateReportsBegin = DateTime.Now;
            Word.Application wordapp;
            Word.Document worddocument;
            Word.Paragraph wordparagraph;

            wordapp = new Word.Application();
            wordapp.Visible = true;
            Object template = Type.Missing;
            Object newTemplate = false;
            Object documentType = Word.WdNewDocumentType.wdNewBlankDocument;
            Object visible = true;
            worddocument = wordapp.Documents.Add(ref template, ref newTemplate, ref documentType, ref visible);
            worddocument.Activate();

            object oMissing = System.Reflection.Missing.Value;
            object unit;
            object extend;
            unit = Word.WdUnits.wdStory;
            extend = Word.WdMovementType.wdMove;

            //���������
            worddocument.Paragraphs.Add(ref oMissing);
            wordapp.Selection.EndKey(ref unit, ref extend);
            wordparagraph = worddocument.Paragraphs.Add(ref oMissing);

            wordparagraph.Range.Text = "������� �� ���������� �������� ���� �������� ���";
            wordparagraph.Range.Font.Color = Word.WdColor.wdColorBlack;
            wordparagraph.Range.Font.Size = 14;
            wordparagraph.Range.Font.Name = "Times New Roman";
            wordparagraph.Range.Font.Bold = 1;
            wordparagraph.Range.Font.Underline = Word.WdUnderline.wdUnderlineNone;
            wordparagraph.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

            string departmentID = sqlCon.GetSystemValue("Department");
            string departmentName;
            if (departmentID != null)
            {
                DataTable dtDepartment = sqlCon.GetDataTable("department",
                            @"SELECT name FROM department WHERE id='" + departmentID + "'");
                departmentName = dtDepartment.Rows[0]["name"].ToString();
            }
            else
                departmentName = "���������� �� �������";

            DateTime thisDay = DateTime.Today;

            worddocument.Paragraphs.Add(ref oMissing);
            wordapp.Selection.EndKey(ref unit, ref extend);
            wordparagraph = worddocument.Paragraphs.Add(ref oMissing);
            wordparagraph.Range.Text = "���������� " + departmentName + " �� " + thisDay.ToString("D");
            wordparagraph.Range.Font.Color = Word.WdColor.wdColorBlack;
            wordparagraph.Range.Font.Size = 13;
            wordparagraph.Range.Font.Name = "Times New Roman";
            wordparagraph.Range.Font.Bold = 1;
            wordparagraph.Range.Font.Underline = Word.WdUnderline.wdUnderlineNone;
            wordparagraph.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            wordparagraph = worddocument.Paragraphs.Add(ref oMissing);

            DateTime dateBegin = DateTime.Now;
            
            //�������
            DataTable dtAllCount = sqlCon.GetDataTable("all_count", 
                                    @"SELECT COUNT(*) AS allCount FROM spec WHERE is_present=true");

            DataTable dtResultCount = sqlCon.GetDataTable("all_countResult",
                                    @"SELECT COUNT(result) AS allCount FROM spec WHERE is_present=true");

            DataTable dtParty = sqlCon.GetDataTable("party",
                                    @"SELECT COUNT(*) AS allCount FROM 
                                    (SELECT DISTINCT spec.id
                                    FROM spec RIGHT JOIN party ON spec.id=party.id_spec
                                    WHERE (is_present=true))"
                        );

            DataTable dtPrevConv = sqlCon.GetDataTable("prev_conv",
                                    @"SELECT COUNT(*) AS allCount FROM 
                                    (SELECT DISTINCT spec.id
                                    FROM spec RIGHT JOIN prev_conv ON spec.id=prev_conv.id_spec
                                    WHERE (is_present=true))"
                        );
            
            DataTable dtSpecPsycho1 = sqlCon.GetDataTable("spec_psycho1",
                                    @"SELECT COUNT(*) AS allCount FROM 
                                    (SELECT DISTINCT spec.id
                                    FROM spec RIGHT JOIN spec_psycho ON spec.id=spec_psycho.id_spec
                                    WHERE ((is_present=true) AND (type_doc = 1)))"
                        );

            DataTable dtSpecPsycho2 = sqlCon.GetDataTable("spec_psycho2",
                                    @"SELECT COUNT(*) AS allCount FROM 
                                    (SELECT DISTINCT spec.id
                                    FROM spec RIGHT JOIN spec_psycho ON spec.id=spec_psycho.id_spec
                                    WHERE ((is_present=true) AND (type_doc = 2)))"
                        );

            DataTable dtProfYchet = sqlCon.GetDataTable("profilact_ychet",
                                    @"SELECT COUNT(*) AS allCount FROM 
                                    (SELECT DISTINCT spec.id
                                    FROM spec RIGHT JOIN profilact_ychet ON spec.id=profilact_ychet.id_spec
                                    WHERE (is_present=true) AND (profilact_ychet.data_snyat IS NULL))"
                        );

            DataTable dtPsycho = sqlCon.GetDataTable("psycho_char",
                                    @"SELECT COUNT(*) AS allCount, COUNT(orientation) AS orientationCount, COUNT(psycho_char) AS psychoCount, COUNT(behavior) AS behaviorCount FROM 
                                    (SELECT DISTINCT spec.id, psycho_char.orientation, psycho_char.psycho_char, psycho_char.behavior
                                    FROM spec RIGHT JOIN psycho_char ON spec.id=psycho_char.id_spec
                                    WHERE (is_present=true))"
                        );

            DataTable dtIVR1 = sqlCon.GetDataTable("ivr1",
                                    @"SELECT COUNT(*) AS allCount FROM 
                                    (SELECT DISTINCT spec.id
                                    FROM spec RIGHT JOIN ivr ON spec.id=ivr.id_spec
                                    WHERE ((is_present=true) AND (id_type_ivr=1)))"
                        );

            DataTable dtIVR2 = sqlCon.GetDataTable("ivr2",
                                    @"SELECT COUNT(*) AS allCount FROM 
                                    (SELECT DISTINCT spec.id
                                    FROM spec RIGHT JOIN ivr ON spec.id=ivr.id_spec
                                    WHERE ((is_present=true) AND (id_type_ivr=2)))"
                        );

            DataTable dtIVR3 = sqlCon.GetDataTable("ivr3",
                                    @"SELECT COUNT(*) AS allCount FROM 
                                    (SELECT DISTINCT spec.id
                                    FROM spec RIGHT JOIN ivr ON spec.id=ivr.id_spec
                                    WHERE ((is_present=true) AND (id_type_ivr=3)))"
                        );

            DataTable dtResolution = sqlCon.GetDataTable("resolution",
                                    @"SELECT COUNT(*) AS allCount FROM 
                                    (SELECT DISTINCT spec.id
                                    FROM spec RIGHT JOIN resolution ON spec.id=resolution.id_spec
                                    WHERE (is_present=true))"
                                    );


            DataTable dtPartyRecords = sqlCon.GetDataTable("partyRecords",
                                    @"SELECT COUNT(party.id) AS allCount
                                    FROM spec INNER JOIN party ON spec.id=party.id_spec
                                    WHERE (is_present=true)"
                        );

            DataTable dtPrevConvRecords = sqlCon.GetDataTable("prev_convRecords",
                                    @"SELECT COUNT(prev_conv.id) AS allCount
                                    FROM spec INNER JOIN prev_conv ON spec.id=prev_conv.id_spec
                                    WHERE (is_present=true)"
                        );

            DataTable dtSpecPsychoRecords1 = sqlCon.GetDataTable("spec_psychoRecords1",
                                    @"SELECT COUNT(spec_psycho.id) AS allCount
                                    FROM spec INNER JOIN spec_psycho ON spec.id=spec_psycho.id_spec
                                    WHERE ((is_present=true) AND (type_doc = 1))"
                        );

            DataTable dtSpecPsychoRecords2 = sqlCon.GetDataTable("spec_psychoRecords2",
                                    @"SELECT COUNT(spec_psycho.id) AS allCount
                                    FROM spec INNER JOIN spec_psycho ON spec.id=spec_psycho.id_spec
                                    WHERE ((is_present=true) AND (type_doc = 2))"
                        );

            DataTable dtProfYchetRecords = sqlCon.GetDataTable("profilact_ychetRecords",
                                    @"SELECT COUNT(profilact_ychet.id) AS allCount
                                    FROM spec INNER JOIN profilact_ychet ON spec.id=profilact_ychet.id_spec
                                    WHERE (is_present=true)"
                        );

            DataTable dtPsychoRecords = sqlCon.GetDataTable("psycho_charRecords",
                                    @"SELECT COUNT(psycho_char.id) AS allCount, COUNT(psycho_char.psycho_char) AS psychoCount, COUNT(psycho_char.orientation) AS orientationCount, COUNT(psycho_char.behavior) AS behaviorCount
                                    FROM spec INNER JOIN psycho_char ON spec.id=psycho_char.id_spec
                                    WHERE (is_present=true)"
                        );

            DataTable dtIVR1Records = sqlCon.GetDataTable("ivr1Records",
                                    @"SELECT COUNT(ivr.id) AS allCount
                                    FROM spec INNER JOIN ivr ON spec.id=ivr.id_spec
                                    WHERE ((is_present=true) AND (id_type_ivr=1))"
                        );

            DataTable dtIVR2Records = sqlCon.GetDataTable("ivr2Records",
                                    @"SELECT COUNT(ivr.id) AS allCount
                                    FROM spec INNER JOIN ivr ON spec.id=ivr.id_spec
                                    WHERE ((is_present=true) AND (id_type_ivr=2))"
                        );

            DataTable dtIVR3Records = sqlCon.GetDataTable("ivr3Records",
                                    @"SELECT COUNT(ivr.id) AS allCount
                                    FROM spec INNER JOIN ivr ON spec.id=ivr.id_spec
                                    WHERE ((is_present=true) AND (id_type_ivr=3))"
                        );

            DataTable dtResolutionRecords = sqlCon.GetDataTable("resolutionRecords",
                                    @"SELECT COUNT(resolution.id) AS allCount
                                    FROM spec INNER JOIN resolution ON spec.id=resolution.id_spec
                                    WHERE (is_present=true)"
                                    );

            DateTime dateEnd = DateTime.Now;
            
            wordparagraph = worddocument.Paragraphs.Add(ref oMissing);
            wordparagraph.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
            wordparagraph = worddocument.Paragraphs.Add(ref oMissing);

            Object defaultTableBehavior = Word.WdDefaultTableBehavior.wdWord9TableBehavior;
            Object autoFitBehavior = Word.WdAutoFitBehavior.wdAutoFitWindow;
            Word.Range wordrange = wordparagraph.Range;

            Word.Table wordtable1 = worddocument.Tables.Add(wordrange, 16, 3, ref defaultTableBehavior, ref autoFitBehavior);
            wordtable1.Range.Font.Size = 12;
            wordtable1.Range.Font.Bold = 0;
            wordtable1.Borders.Enable = 1;
            wordtable1.Columns[1].Width = 250;
            wordtable1.Columns[2].Width = 100;
            wordtable1.Columns[3].Width = 100;
            wordtable1.Cell(1, 1).Range.Text = "������������ �������";
            wordtable1.Cell(1, 1).Range.Font.Bold = 1;
            wordtable1.Cell(1, 2).Range.Text = "���������� ����������, � ������� ���������";
            wordtable1.Cell(1, 2).Range.Font.Bold = 1;
            wordtable1.Cell(1, 3).Range.Text = "����� ���������� ��������� �������";
            wordtable1.Cell(1, 3).Range.Font.Bold = 1;
            wordtable1.Cell(2, 1).Range.Text = "������� ���������";
            wordtable1.Cell(2, 1).Range.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
            wordtable1.Cell(2, 2).Range.Text = dtPrevConv.Rows[0]["allCount"].ToString();
            wordtable1.Cell(2, 3).Range.Text = dtPrevConvRecords.Rows[0]["allCount"].ToString();
            wordtable1.Cell(3, 1).Range.Text = "����������� �� �������";
            wordtable1.Cell(3, 1).Range.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
            wordtable1.Cell(3, 2).Range.Text = dtParty.Rows[0]["allCount"].ToString();
            wordtable1.Cell(3, 3).Range.Text = dtPartyRecords.Rows[0]["allCount"].ToString();
            wordtable1.Cell(4, 1).Range.Text = "��������������� ��������������";
            wordtable1.Cell(4, 1).Range.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
            wordtable1.Cell(4, 2).Range.Text = dtSpecPsycho1.Rows[0]["allCount"].ToString();
            wordtable1.Cell(4, 3).Range.Text = dtSpecPsychoRecords1.Rows[0]["allCount"].ToString();
            wordtable1.Cell(5, 1).Range.Text = "��������� �� ���������� (� �.�. ����� ������ �� ��������� ������������)";
            wordtable1.Cell(5, 1).Range.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
            wordtable1.Cell(5, 2).Range.Text = dtSpecPsycho2.Rows[0]["allCount"].ToString();
            wordtable1.Cell(5, 3).Range.Text = dtSpecPsychoRecords2.Rows[0]["allCount"].ToString();
            wordtable1.Cell(6, 1).Range.Text = "��������";
            wordtable1.Cell(6, 1).Range.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
            wordtable1.Cell(6, 2).Range.Text = dtProfYchet.Rows[0]["allCount"].ToString();
            wordtable1.Cell(6, 3).Range.Text = dtProfYchetRecords.Rows[0]["allCount"].ToString();
            wordtable1.Cell(7, 1).Range.Text = "�������������� ��������������� ����������� (����������� 1 ��� � 6 �������)";
            wordtable1.Cell(7, 1).Range.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
            wordtable1.Cell(7, 2).Range.Text = dtPsycho.Rows[0]["allCount"].ToString();
            wordtable1.Cell(7, 3).Range.Text = dtPsychoRecords.Rows[0]["allCount"].ToString();
            wordtable1.Cell(8, 1).Range.Text = "� ��� �����: - �������������� ��������";
            wordtable1.Cell(8, 1).Range.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
            wordtable1.Cell(8, 2).Range.Text = dtPsycho.Rows[0]["orientationCount"].ToString();
            wordtable1.Cell(8, 3).Range.Text = dtPsychoRecords.Rows[0]["orientationCount"].ToString();
            wordtable1.Cell(9, 1).Range.Text = "             - ���������������  ��������������";
            wordtable1.Cell(9, 1).Range.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
            wordtable1.Cell(9, 2).Range.Text = dtPsycho.Rows[0]["psychoCount"].ToString();
            wordtable1.Cell(9, 3).Range.Text = dtPsychoRecords.Rows[0]["psychoCount"].ToString();
            wordtable1.Cell(10, 1).Range.Text = "             - �������� � ��������� �����������";
            wordtable1.Cell(10, 1).Range.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
            wordtable1.Cell(10, 2).Range.Text = dtPsycho.Rows[0]["behaviorCount"].ToString();
            wordtable1.Cell(10, 3).Range.Text = dtPsychoRecords.Rows[0]["behaviorCount"].ToString();
            wordtable1.Cell(11, 1).Range.Text = "���, ���������� ����������� ������";
            wordtable1.Cell(11, 1).Range.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
            wordtable1.Cell(11, 2).Range.Text = dtIVR1.Rows[0]["allCount"].ToString();
            wordtable1.Cell(11, 3).Range.Text = dtIVR1Records.Rows[0]["allCount"].ToString();
            wordtable1.Cell(12, 1).Range.Text = "���, ���������� ������� ������ ������������ ������";
            wordtable1.Cell(12, 1).Range.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
            wordtable1.Cell(12, 2).Range.Text = dtIVR2.Rows[0]["allCount"].ToString();
            wordtable1.Cell(12, 3).Range.Text = dtIVR2Records.Rows[0]["allCount"].ToString();
            wordtable1.Cell(13, 1).Range.Text = "���, ���������� ����� ������������";
            wordtable1.Cell(13, 1).Range.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
            wordtable1.Cell(13, 2).Range.Text = dtIVR3.Rows[0]["allCount"].ToString();
            wordtable1.Cell(13, 3).Range.Text = dtIVR3Records.Rows[0]["allCount"].ToString();
            wordtable1.Cell(14, 1).Range.Text = "������� ������ ������������ ������ (����������� �� ���� �������������)";
            wordtable1.Cell(14, 1).Range.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
            wordtable1.Cell(14, 2).Range.Text = dtResolution.Rows[0]["allCount"].ToString();
            wordtable1.Cell(14, 3).Range.Text = dtResolutionRecords.Rows[0]["allCount"].ToString();
            wordtable1.Cell(15, 1).Range.Text = "���������� ������ �� ���������� ����������� � ������������ (����������� �� ���� �������������)";
            wordtable1.Cell(15, 1).Range.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
            wordtable1.Cell(15, 2).Range.Text = dtResultCount.Rows[0]["allCount"].ToString();
            wordtable1.Cell(15, 3).Range.Text = "-";
            wordtable1.Cell(16, 1).Range.Text = "����� ������� �������";
            wordtable1.Cell(16, 1).Range.Font.Bold = 1;
            wordtable1.Cell(16, 1).Range.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
            wordtable1.Cell(16, 2).Range.Text = dtAllCount.Rows[0]["allCount"].ToString();
            wordtable1.Cell(16, 2).Range.Font.Bold = 1;
            wordtable1.Cell(16, 3).Range.Text = "-";
            wordtable1.Cell(16, 3).Range.Font.Bold = 1;

            worddocument.Paragraphs.Add(ref oMissing);
            worddocument.Paragraphs.Add(ref oMissing);
            wordapp.Selection.EndKey(ref unit, ref extend);
            wordparagraph = worddocument.Paragraphs.Add(ref oMissing);

            TimeSpan timeQuery = dateEnd - dateBegin;

            wordparagraph.Range.Font.Bold = 0;
            wordparagraph.Range.Font.Size = 8;
            wordparagraph.Range.Text = "����� ���������� �������: " + timeQuery.Milliseconds.ToString() + "ms";
            wordparagraph.Range.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;

            worddocument.Paragraphs.Add(ref oMissing);
            wordapp.Selection.EndKey(ref unit, ref extend);
            wordparagraph = worddocument.Paragraphs.Add(ref oMissing);
            wordparagraph.Range.Font.Bold = 0;
            wordparagraph.Range.Font.Size = 8;
            DateTime dateReportsEnd = DateTime.Now;
            TimeSpan timeReports = dateReportsEnd - dateReportsBegin;
//            wordparagraph.Range.Text = "����� ����� ������������ ������: " + timeReports.Milliseconds.ToString() + "ms";
            //wordparagraph.Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
        
        }

        private String getValidDate(Object objDate)
        {
            try
            {
                if (Convert.ToString(objDate).Trim() == String.Empty)
                    return String.Empty;
                DateTime dateTime = DateTime.Parse(Convert.ToString(objDate));
                return dateTime.ToString("D");
            }
            catch (FormatException)
            {
                return String.Empty;
            }
        }

        private String getValidDateShort(Object objDate)
        {
            try
            {
                if (Convert.ToString(objDate).Trim() == String.Empty)
                    return String.Empty;
                DateTime dateTime = DateTime.Parse(Convert.ToString(objDate));
                return dateTime.ToString("d");
            }
            catch (FormatException)
            {
                return String.Empty;
            }
        }


    }
}
