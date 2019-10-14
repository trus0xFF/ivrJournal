using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using System.Data;

namespace ivrJournal
{
    static class IVRShared
    {

        public static String GetCurrentUserName()
        {
            RegistryKey regKey = Registry.CurrentUser;
            regKey = regKey.OpenSubKey("Software\\UFSIN\\ivrJournal");
            String login = regKey.GetValue("login").ToString();
            return login;
        }

        public static String GetDBPath()
        {
            RegistryKey regKey = Registry.CurrentUser;
            regKey = regKey.OpenSubKey("Software\\UFSIN\\ivrJournal");
            String path = regKey.GetValue("dbPath", "").ToString();
            return path;
        }

        public static int GetCurrentUserRol()
        {
            try
            {
                SQLDBConnect sqlCon = new SQLDBConnect();
                DataRow userRow = sqlCon.GetUser(GetCurrentUserName());

                if (userRow == null)
                    return -1;

                int rolID = Convert.ToInt32(userRow["user_rol_id"]);
                return rolID;
            }catch(Exception){
                return -1;
            }
        }

        public static Boolean IsCurrentUserAdmin()
        {
            if (GetCurrentUserRol() == 1)
                return true;
            else
                return false;
        }

    }
}
