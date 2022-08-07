using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Common.Message
{
    public class Messages
    {

        public static void ErrorMessage(string errorMessage)
        {
            XtraMessageBox.Show(errorMessage,"Error", MessageBoxButtons.OK,MessageBoxIcon.Error);
        }

        public static void WarningMessage(string warningMessage)
        {
            XtraMessageBox.Show(warningMessage, "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static DialogResult YesSelectedYesNo(string message,string title)
        {
            return XtraMessageBox.Show(message,title,MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button1);
        }

        public static DialogResult NoSelectedYesNo(string message, string title)
        {
            return XtraMessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
        }

        public static DialogResult DeleteMessage(string formTypeMessage)
        {
            return YesSelectedYesNo($"Selected {formTypeMessage} will be delete. Do you confirm it?","Delete Confirmation");
        }

        public static void NotSelectedProperRow()
        {
            WarningMessage("Please select the proper row on the table");
        }

    }
}
