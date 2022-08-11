using Common.Enums;
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



        public void SelectionProccess()
        {

        }







        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
