using Common.Enums;
using StudentManagementUI.Forms.BaseForms;
using StudentManagementUI.Show.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementUI.Show
{
    #region Comment
    /*
     * Here we have created ShowEditForms it asks any BaseEditForms we will pass our EditForms here it will set and open for us
     * 
     */
    #endregion
    public class ShowEditForms<TForm>:IBaseFormShow where TForm:BaseEditForm
    {
        #region Comment
        /*
         * Here we have created ShowEditForms it asks any BaseEditForms we will pass our EditForms here it will set and open for us
         * id is we will check if id =-1 then the user has clicked New button if id is different then -1 then user either double click on GridView or click Edit button on RibbonControl
         */
        #endregion
        public long ShowEditDialogForm(FormType formType,long id)
        {
            #region Comment
            /*
             * Here we have create instance of our EditForm during the runtime
             *  Here if our id = -1 then we will set BaseProccessType in BaseEditForms EntityInsert if id is different then -1 then we will set BaseProccessType EntityUpdate in this way we will be known wheater a user click New or (Double click or Edit button on ListForm)
             * Here frm.Id is we have set in BaseEditForm id value because thanks to that Id value we will set inside our textboxes in order to make update there 
             * Here frm.WillRefresh ? frm.id : 0; we will know if we made some changes on EditForms and we want to see as updated in ListForms because first we need id of it after changes in our ListForms it will be focused on Row and refresh the GridControl if now it simply returns 0 then we don't need to refresh or GridControl Form
             */
            #endregion
            using (var frm = (TForm)Activator.CreateInstance(typeof(TForm)))
            {
                frm.BaseProccessType = id > 0 ? ProccessType.EntityUpdate : ProccessType.EntityInsert;
                frm.BaseEditId = id;
                frm.MyBaseEditLoads();
                frm.ShowDialog();
                return frm.BaseWillRefresh ? frm.BaseEditId : 0;
            }
        }

        #region Comment
        /*
         * Here we have created another ShowEditDiaglogForm but this time we pass params[] as object because when we are in our District then we need CityId in order to add a new District
         */
        #endregion
        public long ShowEditDialogForm(FormType formType, long id,params object[] prm)
        {
            
            using (var frm = (TForm)Activator.CreateInstance(typeof(TForm),prm))
            {
                frm.BaseProccessType = id > 0 ? ProccessType.EntityUpdate : ProccessType.EntityInsert;
                frm.BaseEditId = id;
                frm.MyBaseEditLoads();
                frm.ShowDialog();
                return frm.BaseWillRefresh ? frm.BaseEditId : 0;
            }
        }

    }
}
