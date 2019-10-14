using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using System.Reflection;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;

namespace ivrJournal
{
    class FormControlManager
    {
        private SQLDBConnect sqlCon;
        public FormControlManager()
        {
            sqlCon = new SQLDBConnect();
            DataTable dt = sqlCon.GetDataTable("spr_user_controls", "SELECT * FROM spr_user_controls");
            dt = sqlCon.GetDataTable("user_rol_access", "SELECT * FROM user_rol_access");
        }

        private String GetParentChain(Control ctl)
        {
            String buf = ctl.Text;
            if (ctl.Parent != null)
            {
                buf = String.IsNullOrEmpty(buf) ? GetParentChain(ctl.Parent) :
                    GetParentChain(ctl.Parent) + " -> " + buf;
            }
            return buf;
        }

        private String GetParentChain(ToolStripItem tsi)
        {
            String buf = tsi.Text;
            if (tsi.OwnerItem != null)
            {
                buf = String.IsNullOrEmpty(buf) ? GetParentChain(tsi.OwnerItem) :
                    GetParentChain(tsi.OwnerItem) + " -> " + buf;
            }
            return buf;
        }

        public void InsertControls(params String[] formNameList)
        {
            for (int i = 0; i < formNameList.Length; i++)
            {
                InsertControlsBySingle(formNameList[i]);
            }
            
            sqlCon.UpdateDataTable("spr_user_controls");
        }

        public void ReloadControls(params String[] formNameList)
        {
            sqlCon.DoQuery("DELETE * FROM user_rol_access");
            sqlCon.DoQuery("DELETE * FROM spr_user_controls");
            DataTable dt = sqlCon.RefreshDataTable("spr_user_controls");

            for (int i = 0; i < formNameList.Length; i++)
            {
                InsertControlsBySingle(formNameList[i]);
            }
            
            sqlCon.UpdateDataTable("spr_user_controls");
        }

        private void InsertControlsBySingle(String formName)
        {
            ArrayList ctlList = GetAllControls(formName);

            //DataTable dt = sqlCon.GetDataTable("spr_user_controls", "SELECT * FROM spr_user_controls");
            DataTable dt = sqlCon.GetDataTable("spr_user_controls");
            if (dt == null) 
                return;

            foreach (Object obj in ctlList)
            {
                String name = null;
                String name_rus = null;
                String opisanie = null;

                if (obj is Button)
                {
                    name = ((Button)obj).Name;
                    name_rus = ((Button)obj).Text;
                    opisanie = GetParentChain((Button)obj) + " (кнопка)";
                }
                else if (obj is ToolStripItem)
                {
                    name = ((ToolStripItem)obj).Name;
                    name_rus = ((ToolStripItem)obj).Text;
                    opisanie = GetParentChain((ToolStripItem)obj) + " (пункт меню)";
                }

                if (String.IsNullOrEmpty(name) | String.IsNullOrEmpty(formName) | IsControlExist(formName, name))
                    continue;
                
                DataRow row = dt.NewRow();
                row["name"] = name;
                row["name_rus"] = name_rus;
                row["name_form"] = formName;
                row["opisanie"] = opisanie;
                dt.Rows.Add(row);
                //MessageBox.Show(dt.Rows.Count.ToString());
            }

        }

        private Boolean IsControlExist(String formName, String ctlName)
        {
            DataTable dt = sqlCon.GetDataTable("spr_user_controls");

            if (dt.Select("name_form='" + formName + "' AND name='" + ctlName + "'").Length == 0)
                return false;
            return true;
        }

        private void GetMenues(ArrayList ctlList, ToolStripItem tmi)
        {
            ctlList.Add(tmi);

            if (tmi is ToolStripMenuItem)
            {
                foreach (ToolStripItem menu in ((ToolStripMenuItem)tmi).DropDownItems)
                {
                    GetMenues(ctlList, menu);
                }
            }
        }

        private void GetCtlList(ArrayList ctlList, Control ctl)
        {

            ctlList.Add(ctl);

            //Итерация по всем контролам
            foreach (Control cctl in ctl.Controls)
            {
                GetCtlList(ctlList, cctl);
            }

            //Итерация по всем элементам контекстного меню контрола
            if (ctl.ContextMenuStrip != null)
            {
                foreach (ToolStripItem tmi in ctl.ContextMenuStrip.Items)
                {
                    GetMenues(ctlList, tmi);
                }
            }

            //Итерация по всем элементам меню если, контрол - меню
            if (ctl is MenuStrip)
            {
                foreach (ToolStripItem tmi in ((MenuStrip)ctl).Items)
                {
                    GetMenues(ctlList, tmi);
                }
            }
        }

        private ArrayList GetAllControls(String formName)
        {
            Type type = Type.GetType(Path.GetFileNameWithoutExtension(Application.ExecutablePath) + "." + formName);
            ConstructorInfo ci = type.GetConstructor(new Type[] { });
            Form f = (Form)ci.Invoke(new object[] { });

            ArrayList ctlList = new ArrayList();

            Control ctl = f as Control;
            GetCtlList(ctlList, ctl);

            //MessageBox.Show(" count: " + ctlList.Count.ToString());
            return ctlList;
        }

        private ArrayList GetAllControls(Form f)
        {
            ArrayList ctlList = new ArrayList();

            Control ctl = f as Control;
            GetCtlList(ctlList, ctl);

            //MessageBox.Show(" count: " + ctlList.Count.ToString());
            return ctlList;
        }

        private Boolean GetControlStatus(String formName, String ctlName)
        {
            String rolID = IVRShared.GetCurrentUserRol().ToString();
            String ctlID = null;
            String result = null;

            //DataTable dt = sqlCon.GetDataTable("spr_user_controls", "SELECT * FROM spr_user_controls");
            DataTable dt = sqlCon.GetDataTable("spr_user_controls");
            
            foreach(DataRow row in dt.Select("name_form='" + formName + "' AND name='" + ctlName + "'"))
                ctlID = Convert.ToString(row["id"]);

            if (String.IsNullOrEmpty(ctlID))
                return false;

            DataTable dtURA = sqlCon.GetDataTable("user_rol_access");
            foreach (DataRow row in dtURA.Select("id_user_rol=" + rolID + " AND id_user_controls=" + ctlID + ""))
                result = Convert.ToString(row["enabled"]);


//            String result = sqlCon.GetValue("enabled", @"SELECT enabled FROM user_rol_access 
//                WHERE (id_user_rol=" + rolID + ") AND " +
//                "(id_user_controls=" + ctlID + ")");

            if (String.IsNullOrEmpty(result))
                return false;
            Boolean status = Convert.ToBoolean(result);
            return status;
        }

        public void SetFormControlStatus(Form f)
        {
            if (IVRShared.IsCurrentUserAdmin())
                return;
            ArrayList ctlList = GetAllControls(f);
            String formName = f.Name;

            foreach (Object obj in ctlList)
            {
                if (obj is Button)
                {
                    String name = ((Button)obj).Name;
                    ((Button)obj).Enabled = GetControlStatus(formName, name);
//                    ((Button)obj).Update();
                }
                else if (obj is ToolStripItem)
                {
                    String name = ((ToolStripItem)obj).Name;
                    ((ToolStripItem)obj).Enabled = GetControlStatus(formName, name);
                }
            }
        }


    }
}
