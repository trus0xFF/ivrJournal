
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;
using Microsoft.Win32;

namespace ivrJournal
{
    class akusDbConnect
    {
        private DataSet sprSet;
        private OleDbConnection OleDbCon;
        
        //const String DataProvider = @"Microsoft.Jet.OLEDB.4.0";
        const String DataProvider = @"vfpoledb.1";
        private String dbPath;

        //public Boolean errorStatus = false;

        public akusDbConnect(String dbPath)
        {
            OleDbConnectionStringBuilder bldr = new OleDbConnectionStringBuilder();
            this.dbPath = dbPath;
            bldr.DataSource = dbPath;
            bldr.Provider = DataProvider;
            OleDbCon = new OleDbConnection(bldr.ConnectionString);
        }

        public akusDbConnect(String dbPath, params String[] tableNameList)
        {
            //RegistryKey regKey = Registry.CurrentUser;
            //regKey = regKey.OpenSubKey("Software\\UFSIN\\ivrJournal");

            OleDbConnectionStringBuilder bldr = new OleDbConnectionStringBuilder();

            //bldr.DataSource = @"\\xaos\spec\Integrator\IK1\"; // ��������� ����
            this.dbPath = dbPath;
            bldr.DataSource = dbPath;
            bldr.Provider = DataProvider; // ��������� ����������

            OleDbCon = new OleDbConnection(bldr.ConnectionString);

            DataTable result = null;
            sprSet = new DataSet();
            for (int i = 0; i < tableNameList.Length; i++ )
            {
                switch (tableNameList[i])
                {
                    case "spec":
                        result = InitDataTableSpec();
                        break;
                    case "relations":
                        result = InitDataTableRelations();
                        break;
                    case "bonus":
                        result = InitDataTableBonus();
                        break;
                    case "penalty":
                        result = InitDataTablePenalty();
                        break;
                    default:
                        result = InitDataTable(tableNameList[i]);
                        break;
                }
                if (result == null)
                    return;
            }
            
        }

        public Boolean CheckDbBool()
        {
            try
            {
                OleDbCon.Open();

                OleDbCon.Close();
                return true;

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "������ ����������");
                return false;
            }
        }

        public DataTable GetDataTable(String tableName)
        {
            return sprSet.Tables[tableName];
        }

        public DataSet GetDataSet()
        {
            return sprSet;
        }

        private DataTable InitDataTable(String tableName)
        {
            try
            {
                OleDbCon.Open();

                OleDbCommand cmd = new OleDbCommand();
                //cmd.CommandText = @"SELECT * FROM " + tableName; // ������ �������� SQL
                cmd.CommandText = @"SELECT * FROM " + @"Qualif\" + tableName;

                cmd.Connection = OleDbCon; // ������ �������� ������

                // ������ OleDbDataAdapter ��������� ������� ����� ����� DataTable � ���������� ������.
                OleDbDataAdapter dataAdapter = new OleDbDataAdapter();
                dataAdapter.SelectCommand = cmd;

                OleDbCommandBuilder builder = new OleDbCommandBuilder(dataAdapter);

                dataAdapter.Fill(sprSet, tableName);

                OleDbCon.Close();

                return sprSet.Tables[tableName];
            }
            catch (Exception e)
            {
                OleDbCon.Close();
                MessageBox.Show(e.Message, "������");
                //Console.WriteLine(e.Message);
                return null;
            }
        }

        private DataTable InitDataTablePR13()
        {
            try
            {
                OleDbCon.Open();

                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandText = @"SELECT * FROM data\_pr13 ORDER BY ITEMPERSON";

                cmd.Connection = OleDbCon;

                OleDbDataAdapter dataAdapter = new OleDbDataAdapter();
                dataAdapter.SelectCommand = cmd;

                OleDbCommandBuilder builder = new OleDbCommandBuilder(dataAdapter);

                //dataAdapter.Fill(sprSet, tableName);

                DataTable dt_buf = new DataTable();
                dataAdapter.Fill(dt_buf);

                OleDbCon.Close();

                DataTable dt_buf2 = new DataTable(@"_pr13");
                
                DataColumn newColumn0 = new DataColumn();
                newColumn0.ColumnName = "ITEMPERSON";
                newColumn0.DataType = System.Type.GetType("System.String");
                dt_buf2.Columns.Add(newColumn0);
                
                DataColumn newColumn1 = new DataColumn();
                newColumn1.ColumnName = "NAME";
                newColumn1.DataType = System.Type.GetType("System.String");
                dt_buf2.Columns.Add(newColumn1);

                int idx2 = 0;
                //String buf = String.Empty;
                //MessageBox.Show(dt_buf.Rows.Count.ToString());
                for (int idx=0; idx < dt_buf.Rows.Count; )
                {
                    String buf = String.Empty;
                    idx2 = idx;
                    while ( dt_buf.Rows[idx]["ITEMPERSON"].ToString() == dt_buf.Rows[idx2]["ITEMPERSON"].ToString() ) {
                        buf += dt_buf.Rows[idx2]["NAME"].ToString();
                        if (++idx2 >= dt_buf.Rows.Count)
                        {
                            idx = idx2;
                            break;
                        }
                    }

                    if (idx >= dt_buf.Rows.Count)
                        break;

                    DataRow newRow = dt_buf2.NewRow();
                    newRow["ITEMPERSON"] = dt_buf.Rows[idx]["ITEMPERSON"].ToString();
                    newRow["NAME"] = buf;
                    dt_buf2.Rows.Add(newRow);

                    idx = idx2;
                }

                sprSet.Tables.Add(dt_buf2);

                return dt_buf2;
            }
            catch (Exception e)
            {
                OleDbCon.Close();
                MessageBox.Show(e.Message, "������");
                return null;
            }
        }

        public string RemoveWhitespace(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            string[] parts = inputString.Split(new char[] { ' ', '\n', '\t', '\r', '\f', '\v' }, 
                StringSplitOptions.RemoveEmptyEntries);
            int size = parts.Length;

            for (int i = 0; i < size; i++)
                sb.AppendFormat("{0} ", parts[i]);
            return sb.ToString();
        }

        public String FirstToUpperCase(String inputString)
        {
            StringBuilder sb = new StringBuilder();
            String buf = inputString.Trim().ToLower();
            
            if (buf == String.Empty)
                return String.Empty;
            
            char[] parts = buf.ToCharArray();
            parts[0] = Convert.ToChar(parts[0].ToString().ToUpper());

            int size = parts.Length;

            for (int i = 0; i < size; i++)
                sb.Append(parts[i]);
            return sb.ToString();
        }

        private Int16 Str2Int(String anyBytes)
        {
            Int16 result = 0;

            System.Text.Encoding def = System.Text.Encoding.Default;
            Byte[] encodedBytes = def.GetBytes(anyBytes);

            result = BitConverter.ToInt16(encodedBytes, 0);
            return result;
        }

        private String UncodeStatia (String codeStatia)
        {
            Int16 stIndex = 0;
            String statList = null;

            while (stIndex < codeStatia.Length)
            {
                String subcodeStatia = codeStatia.Substring(stIndex, 2);
                Int16 subcodeStatiaInt = Str2Int(subcodeStatia);

                foreach (DataRow row in sprSet.Tables["PC8"].Select("ITEM='" + subcodeStatia + "'"))
                {
                    //Select ������ ������� ��� ����� ��������
                    if (subcodeStatiaInt == Str2Int(row["ITEM"].ToString()))
                    {
                        statList = (statList == null) ? row["NAME"].ToString().Trim() :
                            statList + ", " + row["NAME"].ToString().Trim();
                    }
                }

                stIndex += 2;
            }
            return statList;
        }

        private DataTable InitDataTableBonus()
        {
            try
            {
                OleDbCon.Open();

                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandText = @"SELECT ITEMPERSON AS id_ptk_akus, 
                                    VDATAOB AS date_bonus,
                                    VDATAOB AS order_date,
                                    PC37.NAME AS bonus_type_name,
                                    VZACHTO AS bonus_reason,
                                    VNOMPRIK AS order_number
                                    FROM data\pooshren 
                                    LEFT JOIN Qualif\PC37 ON (pooshren.VVIDPOOSH=PC37.ITEM)";
                cmd.Connection = OleDbCon;

                OleDbDataAdapter dataAdapter = new OleDbDataAdapter();
                dataAdapter.SelectCommand = cmd;

                OleDbCommandBuilder builder = new OleDbCommandBuilder(dataAdapter);

                dataAdapter.Fill(sprSet, "bonus");

                OleDbCon.Close();

                DataColumn newColumn1 = new DataColumn();
                newColumn1.ColumnName = "bonus_type_id";
                newColumn1.DataType = System.Type.GetType("System.Int32");
                sprSet.Tables["bonus"].Columns.Add(newColumn1);

                DataColumn newColumn2 = new DataColumn();
                newColumn2.ColumnName = "id_spec";
                newColumn2.DataType = System.Type.GetType("System.Int32");
                sprSet.Tables["bonus"].Columns.Add(newColumn2);

                foreach (DataRow row in sprSet.Tables["bonus"].Rows)
                {
                    row["date_bonus"] = getValidDate(row["date_bonus"]);
                    row["order_date"] = getValidDate(row["order_date"]);
                }

                return sprSet.Tables["bonus"];
            }
            catch (Exception e)
            {
                OleDbCon.Close();
                MessageBox.Show(e.Message, "������");
                return null;
            }
        }

        private DataTable InitDataTablePenalty()
        {
            try
            {
                OleDbCon.Open();

                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandText = @"SELECT ITEMPERSON AS id_ptk_akus, 
                                    VDATA AS date_penalty,
                                    VDATA AS order_date,
                                    PC29.NAME AS penalty_type_name,
                                    PC56.NAME AS reason,
                                    VKEM AS performer,
                                    VNOMPRIKAZ AS order_number, 
                                    VDATASNAJT + ' ' + VKEMSNYATO AS removal 
                                    FROM data\distipl 
                                    LEFT JOIN Qualif\PC29 ON (distipl.VVID=PC29.ITEM)
                                    LEFT JOIN Qualif\PC56 ON (distipl.VPRICHINY=PC56.ITEM)";
                cmd.Connection = OleDbCon;

                OleDbDataAdapter dataAdapter = new OleDbDataAdapter();
                dataAdapter.SelectCommand = cmd;

                OleDbCommandBuilder builder = new OleDbCommandBuilder(dataAdapter);

                dataAdapter.Fill(sprSet, "penalty");

                OleDbCon.Close();

                DataColumn newColumn1 = new DataColumn();
                newColumn1.ColumnName = "penalty_type_id";
                newColumn1.DataType = System.Type.GetType("System.Int32");
                sprSet.Tables["penalty"].Columns.Add(newColumn1);

                DataColumn newColumn2 = new DataColumn();
                newColumn2.ColumnName = "id_spec";
                newColumn2.DataType = System.Type.GetType("System.Int32");
                sprSet.Tables["penalty"].Columns.Add(newColumn2);

                DataColumn newColumn3 = new DataColumn();
                newColumn3.ColumnName = "performer_id";
                newColumn3.DataType = System.Type.GetType("System.Int32");
                sprSet.Tables["penalty"].Columns.Add(newColumn3);

                foreach (DataRow row in sprSet.Tables["penalty"].Rows)
                {
                    row["date_penalty"] = getValidDate(row["date_penalty"]);
                    row["order_date"] = getValidDate(row["order_date"]);
                }

                return sprSet.Tables["penalty"];
            }
            catch (Exception e)
            {
                OleDbCon.Close();
                MessageBox.Show(e.Message, "������");
                return null;
            }
        }

        private DataTable InitDataTableRelations()
        {
            try
            {
                OleDbCon.Open();

                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandText = @"SELECT ITEMPERSON AS id_ptk_akus, 
                                    VFAMILY AS last_name, VNAME AS first_name, VLASTNAME AS patronymic, VDATAR AS birthdate, 
                                    PC1.NAME + PC2.NAME + PC3.NAME + VADRES AS address, 
                                    PC20.NAME AS degree_name 
                                    FROM data\rodstv 
                                    LEFT JOIN Qualif\PC1 ON (rodstv.VGOSUD=PC1.ITEM)
                                    LEFT JOIN Qualif\PC2 ON (rodstv.VOBLAST=PC2.ITEM)
                                    LEFT JOIN Qualif\PC3 ON (rodstv.VGORODRAY=PC3.ITEM)
                                    LEFT JOIN Qualif\PC20 ON (rodstv.VSTEPEN=PC20.ITEM)
                                    ";
                cmd.Connection = OleDbCon;

                OleDbDataAdapter dataAdapter = new OleDbDataAdapter();
                dataAdapter.SelectCommand = cmd;

                OleDbCommandBuilder builder = new OleDbCommandBuilder(dataAdapter);

                dataAdapter.Fill(sprSet, "relations");

                OleDbCon.Close();

                DataColumn newColumn1 = new DataColumn();
                newColumn1.ColumnName = "degree_id";
                newColumn1.DataType = System.Type.GetType("System.Int32");
                sprSet.Tables["relations"].Columns.Add(newColumn1);
                
                DataColumn newColumn2 = new DataColumn();
                newColumn2.ColumnName = "id_spec";
                newColumn2.DataType = System.Type.GetType("System.Int32");
                sprSet.Tables["relations"].Columns.Add(newColumn2);

                foreach (DataRow row in sprSet.Tables["relations"].Rows)
                {
                    row["birthdate"] = getValidDate(row["birthdate"]);
                    row["address"] = RemoveWhitespace(row["address"].ToString());
                }


                return sprSet.Tables["relations"];
            }
            catch (Exception e)
            {
                OleDbCon.Close();
                MessageBox.Show(e.Message, "������");
                return null;
            }

        }

        public String UncodeName(String codeFilename, String sourcedir)
        {
            if (codeFilename.Trim() == String.Empty)
                return String.Empty;

            System.Text.Encoding def = System.Text.Encoding.Default;
            Byte[] encodedBytes = def.GetBytes(codeFilename);

            String filename = cton(encodedBytes, 142, 34).ToString();
            String uncodedFilename = sourcedir + @"\" + filename.Substring(0, 7) + @"\" + filename + @".jpg";
            return uncodedFilename;
        }

        private Int64 cton(Byte[] str, int p, int shift)
        {
            Int64 nresult = 0;
            for (int k = 0; k < str.Length; k++ )
            {
                nresult = nresult * p + Convert.ToInt32(str[k]) - shift;
            }
            //Console.WriteLine(nresult);
            return nresult;
        }

        private DataTable InitDataTableSpec()
        {
            try
            {
                OleDbCon.Open();

                OleDbCommand cmd = new OleDbCommand();
                //cmd.CommandText = @"SELECT * FROM " + tableName; // ������ �������� SQL
                //cmd.CommandType =CommandType.StoredProcedure
/*                
FUNCTION cton
PARAMETER STRING, P, SHIFT
PRIVATE K, NRESULT
NRESULT = 0
STRING = CHRTRAN(STRING, CHR(33), '')
FOR K = 1 TO LEN(STRING)
NRESULT = NRESULT*P+ASC(SUBSTR(STRING, K, 1))-SHIFT
ENDFOR
RETURN NRESULT
ENDFUNC;

FUNCTION UncodeName
PARAMETER CODEDFILENAME, SOURCEDIR
IF  .NOT. EMPTY(CODEDFILENAME)
FILENAME = STR(CTON(CODEDFILENAME, 142, 34), 14)
UNCODEDFILENAME = ADDBS(SOURCEDIR)+ADDBS(LEFT(FILENAME, 7))+FILENAME+'.jpg'
ELSE
UNCODEDFILENAME = ""
ENDIF
RETURN UNCODEDFILENAME
ENDFUNC;
*/
                cmd.CommandText = @"SELECT  DISTINCT card.ITEMPERSON AS id_ptk_akus,
                                            VFAMILY AS last_name, 
                                            VNAME AS first_name, 
                                            VLASTNAME AS patronymic, 
                                            VDATAR AS birthdate,
                                            VOSUDATA AS crime_date, 
                                            VNACHSROKA AS period_start, 
                                            VKONECSROK AS period_end, 
                                            VDATAUDO AS period_udo, 
                                            VDATAPOSEL AS period_kp,
                                            PC6.NAME AS nation_name,
                                            PC7.NAME AS profession_name,
                                            PC22.NAME AS edu_name,
                                            PC10.NAME + '; ' +PC27.NAME AS crime_description,
                                            PC5.NAME AS court,
                                            VSEMIA AS mstatus,
                                            fototeka.VFOTOFAS AS encodedfoto,
                                            VOSUSTATIA AS encoded_article

                                            FROM data\card
                                            LEFT JOIN Qualif\PC6 ON (card.VNATION=PC6.ITEM)
                                            LEFT JOIN Qualif\PC7 ON (card.VPROFESIA=PC7.ITEM)
                                            LEFT JOIN Qualif\PC22 ON (card.VOBRAZOV=PC22.ITEM)
                                            LEFT JOIN Qualif\PC10 ON (card.VHARPREST=PC10.ITEM)
                                            LEFT JOIN Qualif\PC27 ON (card.VKATPREST=PC27.ITEM)
                                            LEFT JOIN Qualif\PC5 ON (card.VSUDNAME=PC5.ITEM)
                                            LEFT JOIN data\fototeka ON (card.ITEMPERSON=fototeka.ITEMPERSON)
                                            WHERE (CARD.VPR_OSV+CARD.VUBYLPOST+CARD.VUMER=0)
                                            ";

                cmd.Connection = OleDbCon; // ������ �������� ������


                // ������ OleDbDataAdapter ��������� ������� ����� ����� DataTable � ���������� ������.
                OleDbDataAdapter dataAdapter = new OleDbDataAdapter();
                dataAdapter.SelectCommand = cmd;

                OleDbCommandBuilder builder = new OleDbCommandBuilder(dataAdapter);

                dataAdapter.Fill(sprSet, "spec");

                OleDbCon.Close();

                DataColumn newColumn0 = new DataColumn();
                newColumn0.ColumnName = "foto";
                newColumn0.DataType = System.Type.GetType("System.String");
                sprSet.Tables["spec"].Columns.Add(newColumn0);
                
                DataColumn newColumn1 = new DataColumn();
                newColumn1.ColumnName = "nation_id";
                newColumn1.DataType = System.Type.GetType("System.Int32");
                sprSet.Tables["spec"].Columns.Add(newColumn1);

                DataColumn newColumn2 = new DataColumn();
                newColumn2.ColumnName = "profession_id";
                newColumn2.DataType = System.Type.GetType("System.Int32");
                sprSet.Tables["spec"].Columns.Add(newColumn2);

                DataColumn newColumn3 = new DataColumn();
                newColumn3.ColumnName = "edu_id";
                newColumn3.DataType = System.Type.GetType("System.Int32");
                sprSet.Tables["spec"].Columns.Add(newColumn3);

                DataColumn newColumn4 = new DataColumn();
                newColumn4.ColumnName = "mstatus_id";
                newColumn4.DataType = System.Type.GetType("System.Int32");
                sprSet.Tables["spec"].Columns.Add(newColumn4);

                DataColumn newColumn5 = new DataColumn();
                newColumn5.ColumnName = "is_present";
                newColumn5.DataType = System.Type.GetType("System.Boolean");
                sprSet.Tables["spec"].Columns.Add(newColumn5);

                DataColumn newColumn6 = new DataColumn();
                newColumn6.ColumnName = "decodedfoto";
                newColumn6.DataType = System.Type.GetType("System.String");
                sprSet.Tables["spec"].Columns.Add(newColumn6);

                DataColumn newColumn7 = new DataColumn();
                newColumn7.ColumnName = "article";
                newColumn7.DataType = System.Type.GetType("System.String");
                sprSet.Tables["spec"].Columns.Add(newColumn7);

                //����� ��������� (������� ��������� ������� ������������)
                InitDataTablePR13();
                //�������� ������������
                InitDataTable("PC10");
                //��������� ������������
                InitDataTable("PC27");
                foreach (DataRow row in sprSet.Tables["spec"].Rows)
                {
                    //dtpr13 = sprSet.Tables["_pr13"];
                    foreach (DataRow row2 in sprSet.Tables["_pr13"].Rows)
                    {
                        if (row["id_ptk_akus"].ToString() == row2["ITEMPERSON"].ToString())
                        {
                            row["crime_description"] = RemoveWhitespace(row["crime_description"].ToString()) + "; " + row2["NAME"].ToString();
                            break;
                        }
                    }
                }

                //���������� ������
                InitDataTable("PC8");

                foreach (DataRow row in sprSet.Tables["spec"].Rows)
                {
                    row["last_name"] = FirstToUpperCase(row["last_name"].ToString());
                    row["first_name"] = FirstToUpperCase(row["first_name"].ToString());
                    row["patronymic"] = FirstToUpperCase(row["patronymic"].ToString());
                    row["is_present"] = false;
                    row["birthdate"] = getValidDate(row["birthdate"]);
                    row["crime_date"] = getValidDate(row["crime_date"]);
                    row["period_start"] = getValidDate(row["period_start"]);
                    row["period_end"] = getValidDate(row["period_end"]);
                    row["period_udo"] = getValidDate(row["period_udo"]);
                    row["period_kp"] = getValidDate(row["period_kp"]);
                    row["article"] = UncodeStatia(row["encoded_article"].ToString());
                    //MessageBox.Show( UncodeStatia( row["encoded_article"].ToString() ) );

                }

                return sprSet.Tables["spec"];
            }

            catch (Exception e)
            {
                OleDbCon.Close();
                MessageBox.Show(e.Message, "������");
                //Console.WriteLine(e.Message);
                return null;
            }
        }

        private Object getValidDate(Object objDate)
        {
            try
            {
                if (Convert.ToString(objDate).Trim() == String.Empty)
                    return DBNull.Value;
                return DateTime.Parse(Convert.ToString(objDate)).ToString("s");
            }
            catch (FormatException)
            {
                return DBNull.Value;
            }
        }


        public void RefreshDataTable( String tableName )
        {
            try
            {
                OleDbCon.Open();
                sprSet.Tables[tableName].Clear();
                OleDbCon.Close();

                InitDataTable(tableName);

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "������");
                //Console.WriteLine(e.Message);
            }
        }

        public void UpdateDataTable(String tableName)
        {
            try
            {
                OleDbCon.Open();

                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandText = @"SELECT * FROM " + tableName; // ������ �������� SQL
                cmd.Connection = OleDbCon; // ������ �������� ������

                // ������ OleDbDataAdapter ��������� ������� ����� ����� DataTable � ���������� ������.
                OleDbDataAdapter dataAdapter = new OleDbDataAdapter();

                dataAdapter.SelectCommand = cmd;

                OleDbCommandBuilder builder = new OleDbCommandBuilder(dataAdapter);

                dataAdapter.Update(sprSet, tableName);

                OleDbCon.Close();

                return;
            }
            catch (Exception e)
            {
                OleDbCon.Close();
                MessageBox.Show(e.Message, "������");
                //Console.WriteLine(e.Message);
                return;
            }
        }

        public int mapDataSpr(DataTable dtSrc, DataTable dtDst, String colName)
        {
            Boolean isfound;
            DataRow newRow;
            int newRowCounter = 0;

            foreach (DataRow rowakus in dtSrc.Rows)
            {
                if (rowakus[colName] == null)
                    continue;

                if (rowakus[colName].ToString().Trim() == String.Empty)
                {
                    rowakus[colName] = "-";
                }

                isfound = false;
                foreach (DataRow rowivr in dtDst.Rows)
                {
                    if (rowakus[colName].ToString().Trim().ToUpper() ==
                                    rowivr["name"].ToString().Trim().ToUpper())
                    {
                        isfound = true;
                        break;
                    }
                }

                if (!isfound)
                {
                    newRow = dtDst.NewRow();
                    newRow["name"] = rowakus[colName].ToString();
                    dtDst.Rows.Add(newRow);
                    newRowCounter++;
                }
            }

            return newRowCounter;
        }

        public void SetSprFields(DataTable dtSpr, DataTable dtDst, String colName, String colID)
        {
            foreach (DataRow rowSpr in dtSpr.Rows)
            {
                foreach (DataRow rowDst in dtDst.Rows)
                {
                    if (rowDst[colName].ToString().Trim().ToUpper() ==
                                    rowSpr["name"].ToString().Trim().ToUpper())
                    {
                        rowDst[colID] = rowSpr["id"];
                    }
                }
            }
        }


        public int mapDataMstatus(DataTable dtSrc, DataTable dtDst, SprDbConnect mainDbCon)
        {
            String[] mstatus = { "-", "������� � �����", "�� ������� � �����", "��������(�)", "������/�����" };
            Boolean isfound;
            DataRow newRow;
            int newRowCounter = 0;

            for (int i = 0; i < 5; i++)
            {
                isfound = false;
                foreach (DataRow rowivr in dtDst.Rows)
                {
                    if (rowivr["name"].ToString().Trim().ToUpper() == mstatus[i].Trim().ToUpper())
                    {
                        isfound = true;
                        break;
                    }
                }

                if (!isfound)
                {
                    newRow = dtDst.NewRow();
                    newRow["name"] = mstatus[i];
                    dtDst.Rows.Add(newRow);
                    newRowCounter++;
                }

            }

            mainDbCon.UpdateDataTable("spr_mstatus");

            foreach (DataRow rowakus in dtSrc.Rows)
            {
                foreach (DataRow rowivr in dtDst.Rows)
                {
                    if (mstatus[Convert.ToInt32(rowakus["mstatus"])].Trim() == rowivr["name"].ToString().Trim())
                    {
                        rowakus["mstatus_id"] = rowivr["id"];
                    }
                }
            }

            return newRowCounter;
        }

    }
}

