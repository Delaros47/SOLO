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

        public static DialogResult YesSelectedYesNoCancel(string message, string title)
        {
            return XtraMessageBox.Show(message, title, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
        }

        #region Comment
        /*
         * Here we have created EditFormClosingMessage(); if we do some changes on our EditForm and without clicking save button and try to close so then it will give us this message
         */
        #endregion
        public static DialogResult EditFormClosingMessage()
        {
            return YesSelectedYesNoCancel("Do you want to save changes?","Saving confirmation");
        }

        public static DialogResult SaveMessage()
        {
            return YesSelectedYesNo("Do you want to save changes?", "Saving confirmation");
        }

        public static DialogResult DeleteMessage(string formTypeMessage)
        {
            return YesSelectedYesNo($"Selected {formTypeMessage} will be delete. Do you confirm it?","Delete Confirmation");
        }

        public static void NotSelectedProperRow()
        {
            WarningMessage("Please select the proper row on the table");
        }

        public static DialogResult UndoMessage()
        {
            return YesSelectedYesNo("Do you want to undo the changes?","Undo Corfirmation");
        }

    }
}
