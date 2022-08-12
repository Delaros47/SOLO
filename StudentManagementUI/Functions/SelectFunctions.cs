using Common.Enums;
using Model.Entities;
using StudentManagementUI.Forms.CityForms;
using StudentManagementUI.Show;
using StudentManagementUI.UserControls.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementUI.Functions
{
    #region Comment
    /*
     * This class will basiclly takes Id and Values from the Table (GridView) to our ButtonEdit controls
     * 
     */
    #endregion
    public class SelectFunctions : IDisposable
    {
        #region Comment
        /*
         * we have declared two buttonEdits one for example for City and second for District because District one is connected with City one
         * 
         */
        #endregion
        private MyButtonEdit _btnEdit;
        private MyButtonEdit _prmEdit;
        private FormType _formType;

        public void Selection(MyButtonEdit btnEdit)
        {
            _btnEdit = btnEdit;
            SelectionProccess();
        }

        public void Selection(MyButtonEdit btnEdit,MyButtonEdit prmEdit)
        {
            _btnEdit = btnEdit;
            _prmEdit = prmEdit;
            SelectionProccess();
        }


        #region Comment
        /*
         * Here we have two method one with one ButtonEdit parameters another with two and with switch case control we identify it is btnCityName or not if it is we set our Id and Value inside our btnCityName ButtonEdit and since District is different because we need cityid and its value
         */
        #endregion
        public void SelectionProccess()
        {
            switch (_btnEdit.Name)
            {
                case "btnCityName":
                    {
                        var entity = (City)ShowListForms<CityListForm>.ShowDialogListForm(_formType, _btnEdit.Id);
                        if (entity != null)
                        {
                            _btnEdit.Id = entity.Id;
                            _btnEdit.EditValue = entity.CityName;
                        }
                    }
                    break;
                case "btnDistrictName":
                    {
                        var entity = (District)ShowListForms<CityListForm>.ShowDialogListForm(_formType, _btnEdit.Id,_prmEdit.Id,_prmEdit.Text);
                        if (entity != null)
                        {
                            _btnEdit.Id = entity.Id;
                            _btnEdit.EditValue = entity.DistrictName;
                        }
                    }
                    break;
            }
        }






        #region Comment
        /*
         * Here we dispose our class from the memory
         */
        #endregion
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
