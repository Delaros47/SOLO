using Common.Message;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementUI.Functions
{
    #region Comment
    /*
     * Here we have created GeneralFunctions class since we will be using extension methods class and methods should be static
     */
    #endregion
    public static class GeneralFunctions
    {
        #region Comment
        /*
         * Here we have created GetRowId extension method when we click Edit button or double click on GridView it will get us our Focused Row Id if we clicked elsewhere then it will give us error
         * 
         */
        #endregion
        public static long GetRowId(this GridView table)
        {
            if (table.FocusedRowHandle > -1)
            {
                return (long)table.GetFocusedRowCellValue("Id");
            }
            else
            {
                Messages.NotSelectedProperRow();
            }
            return -1;
        }

    }
}
