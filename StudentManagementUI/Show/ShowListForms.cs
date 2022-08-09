using Common.Enums;
using StudentManagementUI.Forms.BaseForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentManagementUI.Show
{
    #region Comment
    /*
     * Here we have created ShowListForms that it will open certainly our ListForms
     */
    #endregion
    public class ShowListForms<TForm> where TForm:BaseListForm
    {
        #region Comment
        /*
         * Here we have created ShowListForm it will open all our ListForms from MainForm and formType we will be using for authorize in the end
         * var frm = (TForm)Activator.CreateInstance(typeof(TForm)); Here we have created instance from our ListForm during runtime
         * frm.MdiParent = Form.ActiveForm; Here we set our ListForm as child and since they will be opened from MainForm so our MainForm will be MdiParent
         * frm.MyBaseListLoads(); Here we will be running all our neccerally methods and events before our Forms opens so pretty much everything we needed will be running there like below
         * FillMyVariables();
            EventsLoad();
            BaseTable.OptionsSelection.MultiSelect = BaseMultiSelect;
            BaseNavigator.NavigatableControl = BaseTable.GridControl;
            Cursor.Current = Cursors.WaitCursor;
            EntityRefresh();
            Cursor.Current = DefaultCursor;

         */
        #endregion
        public static void ShowListForm(FormType formType)
        {
            var frm = (TForm)Activator.CreateInstance(typeof(TForm));
            frm.MdiParent = Form.ActiveForm;
            frm.MyBaseListLoads();
            frm.Show();
        }

    }
}
