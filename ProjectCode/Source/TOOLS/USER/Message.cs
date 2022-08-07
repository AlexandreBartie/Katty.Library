using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace BlueRocket
{
    public static class myMessage
    {

        public static bool ToConfirm(string prmText, string prmLabel)
        {
            return MessageBox.Show(prmText, prmLabel, MessageBoxButtons.YesNo) == DialogResult.Yes;
        }
    }
}
