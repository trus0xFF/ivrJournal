using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data;
using System.Data.OleDb;
using Microsoft.Win32;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Checksums;

namespace ivrJournal
{
    class IVRTransfer
    {
        public SQLDBConnect sqlConSrc;
        public SQLDBConnect sqlConDst;
        private String dataFile;
        private String zipPassword;
        private String tempDir;
        //private Dictionary<int, int> mapSpr;

        enum AlignMode
        {
            add,
            dialog,
            ignore
        }

        public IVRTransfer()
        {
            sqlConSrc = new SQLDBConnect();
            sqlConDst = new SQLDBConnect();
            dataFile = "dataset.xml";
            zipPassword = "ivrpacket";
            tempDir = Environment.GetEnvironmentVariable("TEMP") + @"\" + RandomString(8, true);
            //mapSpr = new Dictionary<int, int>();
        }

        //Процедура выгрузки
        public void UploadData(String strSQL, Boolean moveToArh)
        {
            String stringSQL = null;
            String stringSQL1 = null;
            String stringSQL2 = null;

            //spec
            stringSQL = "SELECT * FROM spec WHERE id IN (" + strSQL + ")";
            DataTable dt = sqlConSrc.GetDataTable("spec", stringSQL);
            
            stringSQL1 = "SELECT DISTINCT edu_id FROM spec WHERE id IN (" + strSQL + ")";
            stringSQL2 = "SELECT * FROM spr_edu WHERE id IN (" + stringSQL1 + ")";
            sqlConSrc.GetDataTable("spr_edu", stringSQL2);

            stringSQL1 = "SELECT DISTINCT nation_id FROM spec WHERE id IN (" + strSQL + ")";
            stringSQL2 = "SELECT * FROM spr_nation WHERE id IN (" + stringSQL1 + ")";
            sqlConSrc.GetDataTable("spr_nation", stringSQL2);

            stringSQL1 = "SELECT DISTINCT mstatus_id FROM spec WHERE id IN (" + strSQL + ")";
            stringSQL2 = "SELECT * FROM spr_mstatus WHERE id IN (" + stringSQL1 + ")";
            sqlConSrc.GetDataTable("spr_mstatus", stringSQL2);

            stringSQL1 = "SELECT DISTINCT mstatus_id FROM spec WHERE id IN (" + strSQL + ")";
            stringSQL2 = "SELECT * FROM spr_mstatus WHERE id IN (" + stringSQL1 + ")";
            sqlConSrc.GetDataTable("spr_mstatus", stringSQL2);

            stringSQL1 = "SELECT DISTINCT profession_id FROM spec WHERE id IN (" + strSQL + ")";
            stringSQL2 = "SELECT * FROM spr_profession WHERE id IN (" + stringSQL1 + ")";
            sqlConSrc.GetDataTable("spr_profession", stringSQL2);

            //party_number
            //stringSQL1 = "SELECT DISTINCT party_id FROM spec WHERE id IN (" + strSQL + ")";
            //stringSQL2 = "SELECT * FROM spr_party_number WHERE id IN (" + stringSQL1 + ")";
            stringSQL2 = "SELECT * FROM spr_party_number";
            sqlConSrc.GetDataTable("spr_party_number", stringSQL2);

            //relations
            stringSQL = "SELECT * FROM relations WHERE id_spec IN (" + strSQL + ")";
            sqlConSrc.GetDataTable("relations", stringSQL);

            stringSQL1 = "SELECT DISTINCT degree_id FROM relations WHERE id_spec IN (" + strSQL + ")";
            stringSQL2 = "SELECT * FROM spr_degree WHERE id IN (" + stringSQL1 + ")";
            sqlConSrc.GetDataTable("spr_degree", stringSQL2);

            //bonus
            stringSQL = "SELECT * FROM bonus WHERE id_spec IN (" + strSQL + ")";
            sqlConSrc.GetDataTable("bonus", stringSQL);

            stringSQL1 = "SELECT DISTINCT bonus_type_id FROM bonus WHERE id_spec IN (" + strSQL + ")";
            stringSQL2 = "SELECT * FROM spr_bonus_type WHERE id IN (" + stringSQL1 + ")";
            sqlConSrc.GetDataTable("spr_bonus_type", stringSQL2);

            stringSQL1 = "SELECT DISTINCT performer_id FROM bonus WHERE id_spec IN (" + strSQL + ")";
            stringSQL2 = "SELECT * FROM spr_performers WHERE id IN (" + stringSQL1 + ")";
            sqlConSrc.GetDataTable("spr_performers", stringSQL2);

            //penalty
            stringSQL = "SELECT * FROM penalty WHERE id_spec IN (" + strSQL + ")";
            sqlConSrc.GetDataTable("penalty", stringSQL);

            stringSQL1 = "SELECT DISTINCT penalty_type_id FROM penalty WHERE id_spec IN (" + strSQL + ")";
            stringSQL2 = "SELECT * FROM spr_penalty_type WHERE id IN (" + stringSQL1 + ")";
            sqlConSrc.GetDataTable("spr_penalty_type", stringSQL2);

            //party
            stringSQL = "SELECT * FROM party WHERE id_spec IN (" + strSQL + ")";
            sqlConSrc.GetDataTable("party", stringSQL);
            /*
            stringSQL1 = "SELECT DISTINCT party_number_id FROM party WHERE id IN (" + strSQL + ")";
            stringSQL2 = "SELECT * FROM spr_party_number WHERE id IN (" + stringSQL1 + ")";
            sqlConSrc.GetDataTable("spr_party_number2", stringSQL2);
            */
            //spec_psycho
            stringSQL = "SELECT * FROM spec_psycho WHERE id_spec IN (" + strSQL + ")";
            sqlConSrc.GetDataTable("spec_psycho", stringSQL);

            //psycho_char
            stringSQL = "SELECT * FROM psycho_char WHERE id_spec IN (" + strSQL + ")";
            sqlConSrc.GetDataTable("psycho_char", stringSQL);

            //prev_conv
            stringSQL = "SELECT * FROM prev_conv WHERE id_spec IN (" + strSQL + ")";
            sqlConSrc.GetDataTable("prev_conv", stringSQL);

            stringSQL1 = "SELECT DISTINCT release_reason_id FROM prev_conv WHERE id_spec IN (" + strSQL + ")";
            stringSQL2 = "SELECT * FROM spr_release_reason WHERE id IN (" + stringSQL1 + ")";
            sqlConSrc.GetDataTable("spr_release_reason", stringSQL2);

            //profilact_ychet
            stringSQL = "SELECT * FROM profilact_ychet WHERE id_spec IN (" + strSQL + ")";
            sqlConSrc.GetDataTable("profilact_ychet", stringSQL);

            stringSQL1 = "SELECT DISTINCT id_profilact_ychet FROM profilact_ychet WHERE id_spec IN (" + strSQL + ")";
            stringSQL2 = "SELECT * FROM spr_profilact_ychet WHERE id IN (" + stringSQL1 + ")";
            sqlConSrc.GetDataTable("spr_profilact_ychet", stringSQL2);

            //ivr
            stringSQL = "SELECT * FROM ivr WHERE id_spec IN (" + strSQL + ")";
            sqlConSrc.GetDataTable("ivr", stringSQL);

            stringSQL1 = "SELECT DISTINCT employee_id FROM ivr WHERE id_spec IN (" + strSQL + ")";
            stringSQL2 = "SELECT * FROM employee WHERE id IN (" + stringSQL1 + ")";
            sqlConSrc.GetDataTable("employee", stringSQL2);

            stringSQL1 = "SELECT DISTINCT work_type FROM ivr WHERE id_spec IN (" + strSQL + ")";
            stringSQL2 = "SELECT * FROM spr_work_type WHERE id IN (" + stringSQL1 + ")";
            sqlConSrc.GetDataTable("spr_work_type", stringSQL2);

            //resolution
            stringSQL = "SELECT * FROM resolution WHERE id_spec IN (" + strSQL + ")";
            sqlConSrc.GetDataTable("resolution", stringSQL);

            //system
            stringSQL = "SELECT * FROM system";
            sqlConSrc.GetDataTable("system", stringSQL);

            //results
            stringSQL = "SELECT * FROM results WHERE id_spec IN (" + strSQL + ")";
            sqlConSrc.GetDataTable("results", stringSQL);
            
            DirectoryInfo dir = new DirectoryInfo(tempDir);
            dir.Create();


            CopyFoto( GetDBDirPath(), tempDir, dt);

            DataSet ds = sqlConSrc.GetDataSet();
            ds.WriteXml(tempDir + @"\" + dataFile, XmlWriteMode.WriteSchema);

            Boolean svResult = SaveFile(tempDir);
            if (!svResult)
                return;

            if (moveToArh)
                sqlConSrc.DoQuery("UPDATE spec SET is_present = false WHERE id IN (" + strSQL + ")");
        }

        //Подготовка данных для загрузки
        public Boolean PrepareData(out DataTable dt, out String dep)
        {
            dt = null;
            dep = null;

            //tempDir = Environment.GetEnvironmentVariable("TEMP") + @"\" + RandomString(8, true);
            DirectoryInfo dir = new DirectoryInfo(tempDir);
            dir.Create();

            Boolean status = OpenFile(tempDir);

            if (!status)
                return false;

            DataSet ds = sqlConSrc.GetDataSet();

            String dsFilePath = tempDir + @"\" + dataFile;
            FileInfo dsFile = new FileInfo(dsFilePath);
            if (!dsFile.Exists)
                return false;

            ds.ReadXml(dsFilePath);

            if (!ds.Tables.Contains("spec"))
                return false;
            
            dt = ds.Tables["spec"];

            if (!ds.Tables.Contains("system"))
                return false;

            String dep_id = null;

            foreach(DataRow row  in ds.Tables["system"].Select("name = 'Department'"))
                dep_id = Convert.ToString(row["system_value"]);

            //foreach (DataRow row in ds.Tables["system"].Select("name = 'Database Version'"))
            //    MessageBox.Show(Convert.ToString(row["system_value"]));

            String sqlString = "SELECT name FROM department WHERE id='" + dep_id + "'";
            DataTable dt2 = sqlConSrc.GetDataTable("dep", sqlString);
            foreach(DataRow row in dt2.Rows)
                dep = row["name"].ToString();

            return true;
        }

        //Процедура загрузки
        public Boolean DownloadData(DataTable dt_buf)
        {
            DataTable dtSrc = sqlConSrc.GetDataTable("spec");
            DataTable dtDst = sqlConDst.GetDataTable("spec", "SELECT * FROM spec");
            Dictionary<int, int> dictSprNat;
            Dictionary<int, int> dictSprProf;
            Dictionary<int, int> dictSprEdu;
            Dictionary<int, int> dictSprMS;
            Dictionary<int, int> dictSprParty;
            Dictionary<int, int> dictSprDegree;
            Dictionary<int, int> dictSprBT;
            Dictionary<int, int> dictSprPerf;
            Dictionary<int, int> dictSprPT;
            Dictionary<int, int> dictSprRelR;
            Dictionary<int, int> dictSprProfY;
            Dictionary<int, int> dictSprEmpl;
            Dictionary<int, int> dictSprWT;

            Dictionary<int, int> dictSpec;
            
            Boolean alignStatus = 
            AlignSpr("spr_nation", out dictSprNat, AlignMode.dialog) &
            AlignSpr("spr_profession", out dictSprProf, AlignMode.dialog) &
            AlignSpr("spr_edu", out dictSprEdu, AlignMode.dialog) &
            AlignSpr("spr_mstatus", out dictSprMS, AlignMode.dialog) &
            AlignSpr("spr_party_number", out dictSprParty, AlignMode.add) &
            AlignSpr("spr_degree", out dictSprDegree, AlignMode.dialog) &
            AlignSpr("spr_bonus_type", out dictSprBT, AlignMode.dialog) &
            AlignSpr("spr_performers", out dictSprPerf, AlignMode.dialog) &
            AlignSpr("spr_penalty_type", out dictSprPT, AlignMode.dialog) &
            AlignSpr("spr_release_reason", out dictSprRelR, AlignMode.dialog) &
            AlignSpr("spr_profilact_ychet", out dictSprProfY, AlignMode.dialog) &
            AlignSpr("employee", out dictSprEmpl, AlignMode.add) &
            AlignSpr("spr_work_type", out dictSprWT, AlignMode.dialog);

            if (!alignStatus)
                return false;

            ReplaceSprID(dtSrc, "nation_id", dictSprNat);
            ReplaceSprID(dtSrc, "profession_id", dictSprProf);
            ReplaceSprID(dtSrc, "edu_id", dictSprEdu);
            ReplaceSprID(dtSrc, "mstatus_id", dictSprMS);
            ReplaceSprID(dtSrc, "party_id", dictSprParty);

            ArrayList idList = new ArrayList();

            GetNotSelected(dtSrc, dt_buf, idList);

            CompareTables(dtSrc, dtDst, idList, "first_name", "last_name", "patronymic", "birthdate");
            
            int count = RemoveByFilter(dtSrc, "id", idList);
            //MessageBox.Show("Сообщение", "Информация о " +count.ToString()+ " абонентах не была загружена " );

            FixRowState(dtSrc);
            MergeTables(dtSrc, dtDst);
            sqlConDst.UpdateDataTable("spec");
            
            CopyFoto(tempDir, GetDBDirPath(), dtSrc);

            GetDict(dtSrc, dtDst, out dictSpec);

            //relations
            dtSrc = sqlConSrc.GetDataTable("relations");
            dtDst = sqlConDst.GetDataTable("relations", "SELECT * FROM relations");

            count = RemoveByFilter(dtSrc, "id_spec", idList);

            ReplaceSprID(dtSrc, "degree_id", dictSprDegree);
            ReplaceSprID(dtSrc, "id_spec", dictSpec);

            FixRowState(dtSrc);
            MergeTables(dtSrc, dtDst);
            sqlConDst.UpdateDataTable("relations");
            
            //bonus
            dtSrc = sqlConSrc.GetDataTable("bonus");
            dtDst = sqlConDst.GetDataTable("bonus", "SELECT * FROM bonus");

            RemoveByFilter(dtSrc, "id_spec", idList);
            ReplaceSprID(dtSrc, "bonus_type_id", dictSprBT);
            ReplaceSprID(dtSrc, "performer_id", dictSprPerf);
            ReplaceSprID(dtSrc, "id_spec", dictSpec);

            FixRowState(dtSrc);
            MergeTables(dtSrc, dtDst);
            sqlConDst.UpdateDataTable("bonus");

            //penalty
            dtSrc = sqlConSrc.GetDataTable("penalty");
            dtDst = sqlConDst.GetDataTable("penalty", "SELECT * FROM penalty");

            RemoveByFilter(dtSrc, "id_spec", idList);
            ReplaceSprID(dtSrc, "penalty_type_id", dictSprPT);
            ReplaceSprID(dtSrc, "id_spec", dictSpec);

            FixRowState(dtSrc);
            MergeTables(dtSrc, dtDst);
            sqlConDst.UpdateDataTable("penalty");

            //party
            dtSrc = sqlConSrc.GetDataTable("party");
            dtDst = sqlConDst.GetDataTable("party", "SELECT * FROM party");

            RemoveByFilter(dtSrc, "id_spec", idList);
            //AlignSpr("spr_party_number", out dictSpr, AlignMode.add);
            ReplaceSprID(dtSrc, "party_number_id", dictSprParty);
            ReplaceSprID(dtSrc, "id_spec", dictSpec);

            FixRowState(dtSrc);
            MergeTables(dtSrc, dtDst);
            sqlConDst.UpdateDataTable("party");

            //spec_psycho
            dtSrc = sqlConSrc.GetDataTable("spec_psycho");
            dtDst = sqlConDst.GetDataTable("spec_psycho", "SELECT * FROM spec_psycho");

            RemoveByFilter(dtSrc, "id_spec", idList);
            ReplaceSprID(dtSrc, "id_spec", dictSpec);

            FixRowState(dtSrc);
            MergeTables(dtSrc, dtDst);
            sqlConDst.UpdateDataTable("spec_psycho");
            
            //psycho_char
            dtSrc = sqlConSrc.GetDataTable("psycho_char");
            dtDst = sqlConDst.GetDataTable("psycho_char", "SELECT * FROM psycho_char");

            RemoveByFilter(dtSrc, "id_spec", idList);
            ReplaceSprID(dtSrc, "id_spec", dictSpec);

            FixRowState(dtSrc);
            MergeTables(dtSrc, dtDst);
            sqlConDst.UpdateDataTable("psycho_char");

            //prev_conv
            dtSrc = sqlConSrc.GetDataTable("prev_conv");
            dtDst = sqlConDst.GetDataTable("prev_conv", "SELECT * FROM prev_conv");

            RemoveByFilter(dtSrc, "id_spec", idList);
            ReplaceSprID(dtSrc, "release_reason_id", dictSprRelR);
            ReplaceSprID(dtSrc, "id_spec", dictSpec);

            FixRowState(dtSrc);
            MergeTables(dtSrc, dtDst);
            sqlConDst.UpdateDataTable("prev_conv");

            //profilact_ychet
            dtSrc = sqlConSrc.GetDataTable("profilact_ychet");
            dtDst = sqlConDst.GetDataTable("profilact_ychet", "SELECT * FROM profilact_ychet");

            RemoveByFilter(dtSrc, "id_spec", idList);
            ReplaceSprID(dtSrc, "id_profilact_ychet", dictSprProfY);
            ReplaceSprID(dtSrc, "id_spec", dictSpec);

            FixRowState(dtSrc);
            MergeTables(dtSrc, dtDst);
            sqlConDst.UpdateDataTable("profilact_ychet");

            //ivr
            dtSrc = sqlConSrc.GetDataTable("ivr");
            dtDst = sqlConDst.GetDataTable("ivr", "SELECT * FROM ivr");

            RemoveByFilter(dtSrc, "id_spec", idList);
            ReplaceSprID(dtSrc, "employee_id", dictSprEmpl);
            ReplaceSprID(dtSrc, "work_type", dictSprWT);
            ReplaceSprID(dtSrc, "id_spec", dictSpec);

            FixRowState(dtSrc);
            MergeTables(dtSrc, dtDst);
            sqlConDst.UpdateDataTable("ivr");

            //resolution
            dtSrc = sqlConSrc.GetDataTable("resolution");
            dtDst = sqlConDst.GetDataTable("resolution", "SELECT * FROM resolution");

            RemoveByFilter(dtSrc, "id_spec", idList);
            ReplaceSprID(dtSrc, "id_spec", dictSpec);

            FixRowState(dtSrc);
            MergeTables(dtSrc, dtDst);
            sqlConDst.UpdateDataTable("resolution");

            //results
            dtSrc = sqlConSrc.GetDataTable("results");
            dtDst = sqlConDst.GetDataTable("results", "SELECT * FROM results");

            RemoveByFilter(dtSrc, "id_spec", idList);
            ReplaceSprID(dtSrc, "id_spec", dictSpec);

            FixRowState(dtSrc);
            MergeTables(dtSrc, dtDst);
            sqlConDst.UpdateDataTable("results");

            if (idList.Count > 0)
                MessageBox.Show("Информация о " + idList.Count.ToString() + " абонентах не была загружена ", "Сообщение");
            return true;
        }

        private Boolean MergeTables(DataTable dtSrc, DataTable dtDst)
        {
            if (dtSrc == null | dtDst == null)
                return false;

            dtDst.Merge(dtSrc, false);

            return true;
        }


        public Boolean DownloadDataLocal(DataTable dt_buf)
        {
            DataTable dtSrc = sqlConSrc.GetDataTable("spec");
            DataTable dtDst = sqlConDst.GetDataTable("spec", "SELECT * FROM spec");
            Dictionary<int, int> dictSprNat;
            Dictionary<int, int> dictSprProf;
            Dictionary<int, int> dictSprEdu;
            Dictionary<int, int> dictSprMS;
            Dictionary<int, int> dictSprParty;
            Dictionary<int, int> dictSprDegree;
            Dictionary<int, int> dictSprBT;
            Dictionary<int, int> dictSprPerf;
            Dictionary<int, int> dictSprPT;
            Dictionary<int, int> dictSprRelR;
            Dictionary<int, int> dictSprProfY;
            Dictionary<int, int> dictSprEmpl;
            Dictionary<int, int> dictSprWT;

            Dictionary<int, int> dictSpec;

            Boolean alignStatus =
            AlignSpr("spr_nation", out dictSprNat, AlignMode.dialog) &
            AlignSpr("spr_profession", out dictSprProf, AlignMode.dialog) &
            AlignSpr("spr_edu", out dictSprEdu, AlignMode.dialog) &
            AlignSpr("spr_mstatus", out dictSprMS, AlignMode.dialog) &
            AlignSpr("spr_party_number", out dictSprParty, AlignMode.add) &
            AlignSpr("spr_degree", out dictSprDegree, AlignMode.dialog) &
            AlignSpr("spr_bonus_type", out dictSprBT, AlignMode.dialog) &
            AlignSpr("spr_performers", out dictSprPerf, AlignMode.dialog) &
            AlignSpr("spr_penalty_type", out dictSprPT, AlignMode.dialog) &
            AlignSpr("spr_release_reason", out dictSprRelR, AlignMode.dialog) &
            AlignSpr("spr_profilact_ychet", out dictSprProfY, AlignMode.dialog) &
            AlignSpr("employee", out dictSprEmpl, AlignMode.add) &
            AlignSpr("spr_work_type", out dictSprWT, AlignMode.dialog);

            if (!alignStatus)
                return false;

            ReplaceSprID(dtSrc, "nation_id", dictSprNat);
            ReplaceSprID(dtSrc, "profession_id", dictSprProf);
            ReplaceSprID(dtSrc, "edu_id", dictSprEdu);
            ReplaceSprID(dtSrc, "mstatus_id", dictSprMS);
            ReplaceSprID(dtSrc, "party_id", dictSprParty);

            ArrayList idList = new ArrayList();

            GetNotSelected(dtSrc, dt_buf, idList);

            //CompareTables(dtSrc, dtDst, idList, "first_name", "last_name", "patronymic", "birthdate");

            int count = RemoveByFilter(dtSrc, "id", idList);

            GetDict(dtSrc, dtDst, out dictSpec, "first_name", "last_name", "patronymic", "birthdate");

            //MessageBox.Show("dict " + dictSpec.Count.ToString() + "dt" + dtSrc.Rows.Count.ToString());
            if (dictSpec.Count < dtSrc.Rows.Count)
                return false;

            //spec_psycho
            dtSrc = sqlConSrc.GetDataTable("spec_psycho");
            dtDst = sqlConDst.GetDataTable("spec_psycho", "SELECT * FROM spec_psycho");

            RemoveByFilter(dtSrc, "id_spec", idList);
            ReplaceSprID(dtSrc, "id_spec", dictSpec);

            FixRowState(dtSrc);
            MergeTables(dtSrc, dtDst);
            sqlConDst.UpdateDataTable("spec_psycho");

            //psycho_char
            dtSrc = sqlConSrc.GetDataTable("psycho_char");
            dtDst = sqlConDst.GetDataTable("psycho_char", "SELECT * FROM psycho_char");

            RemoveByFilter(dtSrc, "id_spec", idList);
            ReplaceSprID(dtSrc, "id_spec", dictSpec);

            FixRowState(dtSrc);
            MergeTables(dtSrc, dtDst);
            sqlConDst.UpdateDataTable("psycho_char");

            //ivr
            dtSrc = sqlConSrc.GetDataTable("ivr");
            dtDst = sqlConDst.GetDataTable("ivr", "SELECT * FROM ivr");

            RemoveByFilter(dtSrc, "id_spec", idList);
            ReplaceSprID(dtSrc, "employee_id", dictSprEmpl);
            ReplaceSprID(dtSrc, "work_type", dictSprWT);
            ReplaceSprID(dtSrc, "id_spec", dictSpec);

            FixRowState(dtSrc);
            MergeTables(dtSrc, dtDst);
            sqlConDst.UpdateDataTable("ivr");

            //resolution
            dtSrc = sqlConSrc.GetDataTable("resolution");
            dtDst = sqlConDst.GetDataTable("resolution", "SELECT * FROM resolution");

            RemoveByFilter(dtSrc, "id_spec", idList);
            ReplaceSprID(dtSrc, "id_spec", dictSpec);

            FixRowState(dtSrc);
            MergeTables(dtSrc, dtDst);
            sqlConDst.UpdateDataTable("resolution");

            return true;
        }

        //Выравнивание справочников
        private Boolean AlignSpr(String sprName, out Dictionary<int, int> dictSpr, AlignMode mode)
        {
            dictSpr = null;
            GetDictSprForm getDictSprForm;
            DataTable dtSrcSpr = sqlConSrc.GetDataTable(sprName);
            DataTable dtDstSpr = sqlConDst.GetDataTable(sprName, "SELECT * FROM " + sprName);

            if (dtDstSpr == null)
                return false;

            if (dtSrcSpr == null)
                return true;

            GetDict(dtSrcSpr, dtDstSpr, out dictSpr);
            
            if (dtSrcSpr.Rows.Count == dictSpr.Count)
                return true;
            switch(mode)
            {
            case AlignMode.add:
                DataTable dtSrcBuf = dtSrcSpr.Copy();
                dtSrcBuf.AcceptChanges();

                CompareTables(dtSrcBuf, dtDstSpr);
                FixRowState(dtSrcBuf);
/*
                if (sprName == "employee")
                    MessageBox.Show(dtSrcBuf.Rows.Count.ToString() + " dict:" + dictSpr.Count.ToString());
                    */
                dtDstSpr.Merge(dtSrcBuf, false);
                sqlConDst.UpdateDataTable(sprName);
                dtDstSpr = sqlConDst.RefreshDataTable(sprName);

                GetDict(dtSrcSpr, dtDstSpr, out dictSpr);
/*
                if (sprName == "employee")
                    MessageBox.Show(dtSrcSpr.Rows.Count.ToString() + " dict:" + dictSpr.Count.ToString());
 */
                break;
            case AlignMode.dialog:
                getDictSprForm = new GetDictSprForm(dtSrcSpr, dtDstSpr, dictSpr);
                getDictSprForm.ShowDialog();
                if (getDictSprForm.DialogResult == DialogResult.Cancel)
                {
                    //MessageBox.Show(sprName);
                    return false;
                }
                break;
            default: break;
            }
            return true;
        }

        //Замена идентификатора значениями из словаря
        private void ReplaceSprID(DataTable dtSrc, String columnName, Dictionary<int,int> dictSpr)
        {
            if (dtSrc == null)
                return;
            foreach (DataRow row in dtSrc.Rows)
            {
                if (Convert.IsDBNull(row[columnName]))
                    continue;
                if (dictSpr.ContainsKey(Convert.ToInt32(row[columnName])))
                {
                    //MessageBox.Show(row[columnName].ToString());
                    row[columnName] = dictSpr[Convert.ToInt32(row[columnName])];
                    //MessageBox.Show(row[columnName].ToString());
                }
            }
            dtSrc.AcceptChanges();
        }

        //Получение словаря соответствий справочников
        private void GetDict(DataTable dtSrc, DataTable dtDst, out Dictionary<int, int> dictSpec)
        {
            dictSpec = new Dictionary<int, int>();
            if (dtSrc == null | dtDst == null)
                return;
            foreach (DataRow srcRow in dtSrc.Rows)
            {
                foreach (DataRow dstRow in dtDst.Rows)
                {
                    if (CompareRows(srcRow, dstRow, dtSrc))
                    {
                        dictSpec[ Convert.ToInt32(srcRow["id"]) ] = Convert.ToInt32(dstRow["id"]);
                        break;
                    }
                }
            }
        }

        private void GetDict(DataTable dtSrc, DataTable dtDst, out Dictionary<int, int> dict, params String[] colNameList)
        {
            dict = new Dictionary<int, int>();
            if (dtSrc == null | dtDst == null)
                return;
            foreach (DataRow srcRow in dtSrc.Rows)
            {
                foreach (DataRow dstRow in dtDst.Rows)
                {
                    if (CompareRows(srcRow, dstRow, colNameList))
                    {
                        dict[Convert.ToInt32(srcRow["id"])] = Convert.ToInt32(dstRow["id"]);
                        break;
                    }
                }
            }
        }

        public void GetNotSelected(DataTable dtSrc, DataTable dt_buf, ArrayList idList)
        {
            if (dtSrc == null)
                return;
            DataRow bufRow = null;
            for (int idx = 0; idx < dtSrc.Rows.Count; idx++)
            {
                bufRow = dt_buf.Rows[idx];
                if (Convert.IsDBNull(bufRow["selected"]))
                    continue;
                if (Convert.ToBoolean(bufRow["selected"]) == false)
                    idList.Add(dtSrc.Rows[idx]["id"]);
            }
        }

        public int RemoveByFilter(DataTable dtSrc, String colName, ArrayList valList)
        {
            int count = 0;
            List<DataRow> rowsToRemove = new List<DataRow>();

            if (dtSrc == null)
                return count;

            foreach(DataRow bufRow in dtSrc.Rows)
            {
                if (Convert.IsDBNull(bufRow[colName]))
                    continue;

                if (valList.Contains(Convert.ToInt32(bufRow[colName])) )
                {
                    rowsToRemove.Add(bufRow);
                    count++;
                }
            }
            
            foreach(DataRow dr in rowsToRemove)
                dtSrc.Rows.Remove(dr);

            dtSrc.AcceptChanges();
            return count;
        }

        //Сравнение таблиц с удалением повторяющихся строк в источнике
        public void CompareTables(DataTable dtSrc, DataTable dtDst)
        {
            if (dtSrc == null | dtDst == null)
                return;
            
            List<DataRow> rowsToRemove = new List<DataRow>();
            
            foreach(DataRow srcRow in dtSrc.Rows)
            {
                foreach (DataRow dstRow in dtDst.Rows)
                {
                    if(CompareRows(srcRow, dstRow, dtSrc) )
                    {
                        rowsToRemove.Add(srcRow);
                        break;
                    }
                }
            }

            foreach(DataRow dr in rowsToRemove)
                dtSrc.Rows.Remove(dr);

            dtSrc.AcceptChanges();
        }

        public void CompareTables(DataTable dtSrc, DataTable dtDst, ArrayList idList, params String[] colNameList)
        {
            if (dtSrc == null | dtDst == null)
                return;

            DataRow srcRow = null;
            for (int idx = 0; idx < dtSrc.Rows.Count; idx++)
            {
                srcRow = dtSrc.Rows[idx];
                foreach (DataRow dstRow in dtDst.Rows)
                {
                    if (CompareRows(srcRow, dstRow, colNameList))
                    {
                        idList.Add( Convert.ToInt32(srcRow["id"]) );
                        break;
                    }
                }
            }
        }

        //Изменение статуса строки
        private void FixRowState(DataTable dt)
        {
            if (dt == null)
                return;
            foreach (DataRow row in dt.Rows)
                row.SetAdded();
        }

        //Сравнение строк таблиц
        public Boolean CompareRows(DataRow srcRow, DataRow dstRow, DataTable dt)
        {
            Boolean result = true;
            String columnName = null;

            foreach (DataColumn column in dt.Columns)
            {
                columnName = column.ColumnName;
                if (columnName == "id")
                    continue;
                if (Convert.IsDBNull(srcRow[columnName]) & Convert.IsDBNull(dstRow[columnName]))
                    continue;
                if (Convert.IsDBNull(srcRow[columnName]) | Convert.IsDBNull(dstRow[columnName]))
                {
                    result = false;
                    break;
                }

                if (column.ColumnName.ToUpper().Contains("DATE"))
                {
                    DateTime dt1 = DateTime.Parse(Convert.ToString(srcRow[columnName]));
                    DateTime dt2 = DateTime.Parse(Convert.ToString(dstRow[columnName]));
                    if (dt1 != dt2)
                    {
                        result = false;
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }

                if (srcRow[columnName].ToString().ToUpper() != dstRow[columnName].ToString().ToUpper())
                {
                    result = false;
                    break;
                }
            }
            return result;
        }

        public Boolean CompareRows(DataRow srcRow, DataRow dstRow, params String[] colNameList)
        {
            Boolean result = true;

            foreach ( String columnName in colNameList)
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
                    if (dt1 != dt2)
                    {
                        result = false;
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }

                if (srcRow[columnName].ToString().ToUpper() != dstRow[columnName].ToString().ToUpper())
                {
                    result = false;
                    break;
                }
            }
            return result;
        }

        //Копирование файлов фотографий из источника в получатель в соответствии с полем foto
        private void CopyFoto(String srcDir, String dstDir, DataTable dt)
        {
            try
            {
                DirectoryInfo  dir = new DirectoryInfo(dstDir + @"\foto");
                dir.Create();

                String fotoPath = null;
                FileInfo fotoFile;
                foreach (DataRow row in dt.Rows)
                {
                    if (Convert.IsDBNull(row["foto"]))
                        continue;
                    fotoPath = row["foto"].ToString();
                    if (row["foto"].ToString() == String.Empty)
                        continue;

                    dir = new DirectoryInfo(dstDir + @"\foto" + @"\" + fotoPath.Substring(6, 9));
                    dir.Create();

                    fotoFile = new FileInfo(srcDir + @"\" + fotoPath);
                    if (!fotoFile.Exists)
                        continue;
                    fotoFile.CopyTo(dstDir + @"\" + fotoPath);
                }
            }
            catch (IOException ioe)
            {
                MessageBox.Show(ioe.Message, "Ошибка");
            }
        }

        //Открытие и извлечение zip файла в указанный каталог
        private Boolean OpenFile(String tempDir)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Zip Files (*.zip)|*.zip|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return false;

            try
            {

                FastZip fZip = new FastZip();
                fZip.Password = zipPassword;
                fZip.ExtractZip(openFileDialog.FileName, tempDir, null);
            }
            catch (Exception)
            {
                //MessageBox.Show(ioe.Message, "Ошибка при разархивировании файла переноса");
                return false;
            }
            return true;
        }

        //Сохранение в Zip файл содержимого указанного каталога
        private Boolean SaveFile(String tempDir)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Zip Files (*.zip)|*.zip";

                DateTime dateToSave = DateTime.Today;
                string dateString = dateToSave.ToString("d");

                saveFileDialog.FileName = "пакет " + dateString + ".zip";
                if (saveFileDialog.ShowDialog() != DialogResult.OK)
                    return false;

                if (saveFileDialog.FileName == "")
                    return false;

                DirectoryInfo diSource = new DirectoryInfo(tempDir);
                FastZip fZip = new FastZip();
                fZip.Password = zipPassword;
                fZip.CreateZip(@saveFileDialog.FileName, tempDir, true, null, null);
                MessageBox.Show("Пакет выгрузки успешно сохранен", "Сообщение");
                return true;
            }
            catch (IOException ioe)
            {
                MessageBox.Show(ioe.Message, "Ошибка");
                return false;
            }
        }

        //Получение пути к базе данных
        private String GetDBDirPath()
        {
            RegistryKey regKey = Registry.CurrentUser;
            regKey = regKey.CreateSubKey("Software\\UFSIN\\ivrJournal");
            String dbPath = regKey.GetValue("dbPath", @"c:\Program files\ufsin_rk\Дневник ИВР\divr.mdb").ToString();
            FileInfo file = new FileInfo(dbPath);
            String dbDirPath = file.DirectoryName;
            return dbDirPath;
        }

        //Получение строки со случайным именем
        private String RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }

    }
}
