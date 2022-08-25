using Common.Enums;
using Model.Entities.Base;
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

        #region Comment
        /*
         * Here we will be opening our Districts from CityListForm so we will pass CityId and CityName that's why we have declared params as prm
         */
        #endregion
        public static void ShowListForm(FormType formType, params object[] prm)
        {
            var frm = (TForm)Activator.CreateInstance(typeof(TForm),prm);
            frm.MdiParent = Form.ActiveForm;
            frm.MyBaseListLoads();
            frm.Show();
        }

        #region Comment
        /*
         * Here we have created method named ShowDialogListForm and simply we will be using when we select any List Froms from ButtonEdit controls and our ListForm is not mdiChild anymore it means that it will be open as Dialog like our EditForms
         */
        #endregion
        public static BaseEntity ShowDialogListForm(FormType formType,long? selectedId,params object[] prm)
        {
            #region Comment
            /*
             * Here we have created instance from our ListForm and we set parameters our params while we create it in runtime then in BaseListForm we have created a nullable long variable we set it from here because when we set it then from there we can reach our Row on our Table (GridView) in the end if we selected anything then it returns to us as BaseEntity
             */
            #endregion
            using (var frm = (TForm)Activator.CreateInstance(typeof(TForm),prm))
            {
                frm.BaseListSelectedId = selectedId;
                frm.MyBaseListLoads();
                frm.ShowDialog();
                return frm.DialogResult == DialogResult.OK ? frm.BaseSelectedEntity : null;
            }
        }

    }
}
