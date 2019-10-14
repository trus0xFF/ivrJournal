using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;

namespace ivrJournal
{
    public class MyDataGridView : DataGridView
    {
        protected override bool ProcessDataGridViewKey(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                return false;
//            if (e.Shift && e.KeyCode == Keys.Enter)

            return base.ProcessDataGridViewKey(e);
/*
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    {
                        return false;
                    }
                default:
                    {
                        return base.ProcessDataGridViewKey(e);
                        break;
                    }
            }
*/
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            // Extract the key code from the key value. 
            Keys key = (keyData & Keys.KeyCode);

            // Handle the ENTER key as if it were a RIGHT ARROW key. 
            if (key == Keys.Enter)
                {
                return false;
//                return this.ProcessRightKey(keyData);
            }
            return base.ProcessDialogKey(keyData);
        }

    }
}
