using Common.Enums;
using Common.Message;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.Grid;
using Model.Entities.Base;
using StudentManagementUI.UserControls.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        #region Comment
        /*
         * Here we have created GetRow<T> extension method when we click Edit button or double click on GridView it will get us our Focused Row if we clicked elsewhere then it will give us error so when it gives us Row of GridView then on BaseListForm we will convert into BaseEntity because later we will be needing Value of it on our ButtonEdit control amd beside we need its Id number in order to save on our Database
         * 
         */
        #endregion
        public static T GetRow<T>(this GridView table, bool giveMessage = true)
        {
            if (table.FocusedRowHandle > -1)
            {
                return (T)table.GetRow(table.FocusedRowHandle);
            }
            else
            {
                if (giveMessage)
                    Messages.NotSelectedProperRow();
            }
            return default(T);
        }

        #region Comment
        /*
         * Here our method GetDataChangedPlace it is enum method it will simply compare two entities if there are any changes then it will let us know according to this one we will enable or disable our buttons  so it returns us value as enums
         */
        #endregion
        private static DataChangedPlace GetDataChangedPlace<T>(T oldEntity, T currentEntity)
        {
            foreach (var prop in currentEntity.GetType().GetProperties())
            {
                #region Comment
                /*
                 * Here later we will be using on our Entities ICollection<> generic interface in order to reach each other Entities so if our value is ICollection then it will continue cause it is not our Entity field that's why we put a condition like that here
                 * oldValue we get the value if it is null then we convert into string.Empty because since we cannot compare Null values with each other this ?? checks if our value is null then it assigns Empty value there (?? it checks if it is null if it null first value it will be our assign) 
                 *  If our values are byte[] means that it is a image of picture so we have to check and assign its value if it is empty of null so we will put 0 in order not to be null then we will compare to each other if they are not equal that then it is a new image or picture has been added to our database then we will save it as its Name in our fields IList<string> 
                 */
                #endregion
                if (prop.PropertyType.Namespace == "System.Collections.Generic") continue;
                var oldValue = prop.GetValue(oldEntity) ?? string.Empty;
                var currentValue = prop.GetValue(currentEntity) ?? string.Empty;

                if (prop.PropertyType == typeof(byte[]))
                {
                    if (string.IsNullOrEmpty(oldValue.ToString()))
                    {
                        oldValue = new byte[] { 0 };
                    }
                    if (string.IsNullOrEmpty(currentEntity.ToString()))
                    {
                        currentValue = new byte[] { 0 };
                    }

                    if (((byte[])oldValue).Length != ((byte[])currentValue).Length)
                    {
                        return DataChangedPlace.EditForm;
                    }

                }
                else if (!currentValue.Equals(oldValue))
                {

                    return DataChangedPlace.EditForm;
                }
            }
            return DataChangedPlace.NoChangeData;
        }

        #region Comment
        /*
         * Here ButtonEnabledState method is that first it will send our GetDataChangedPlace enum will send to method oldEntity and currentEntity then it will make some compared to each other then it returns as enum value if it EditForm then it is true 0 when it returns EditForm then our btnSave.Enabled and btnUndo.Enabled will be true then btnNew and btnDelete will be false ! makes then reverse
         */
        #endregion
        public static void ButtonEnabledState<T>(BarButtonItem btnNew, BarButtonItem btnSave, BarButtonItem btnUndo, BarButtonItem btnDelete, T oldEntity, T currentEntity)
        {

            var dataChangedPlace = GetDataChangedPlace(oldEntity, currentEntity);
            var buttonEnabledState = dataChangedPlace == DataChangedPlace.EditForm;
            btnSave.Enabled = buttonEnabledState;
            btnUndo.Enabled = buttonEnabledState;
            btnNew.Enabled = !buttonEnabledState;
            btnDelete.Enabled = !buttonEnabledState;

        }

        #region Comment
        /*
         * Here we have created a method called CreateId it will simply generate long type Id for us according to the year month day hour minute second millisecond and moreover three digits with randomly in this way we will almost have unique Id each time when we create because it will be based what we code and we will be craeting our CreateId as an extension method here also return proccessType == ProccessType.EntityUpdate ? selectedEntity.Id : long.Parse(Id()); If our ProccessType is Update then we will be getting our Id from BaseEntity(Database) if not then we are going to generate a new Id thanks to our CreateId method it is almost impossible the same id comes for the second time
         */
        #endregion
        public static long CreateId(this ProccessType proccessType, BaseEntity selectedEntity)
        {

            string AddOneZero(string value)
            {
                if (value.Length == 1)
                    return "0" + value;
                else
                    return value;
            }

            string AddTwoZero(string value)
            {
                switch (value.Length)
                {
                    case 1:
                        return "00" + value;
                    case 2:
                        return "0" + value;
                }
                return value;
            }

            string Id()
            {
                var year = DateTime.Now.Date.Year.ToString();
                var month = AddOneZero(DateTime.Now.Date.Month.ToString());
                var day = AddOneZero(DateTime.Now.Date.Day.ToString());
                var hour = AddOneZero(DateTime.Now.Hour.ToString());
                var minute = AddOneZero(DateTime.Now.Minute.ToString());
                var second = AddOneZero(DateTime.Now.Second.ToString());
                var millisecond = AddTwoZero(DateTime.Now.Millisecond.ToString());
                var random = AddOneZero(new Random().Next(0, 100).ToString());

                return year + month + day + hour + minute + second + millisecond + random;

            }

            return proccessType == ProccessType.EntityUpdate ? selectedEntity.Id : long.Parse(Id());


        }

        #region Comment
        /*
         * Here we will be using method that whenever we delete our btnCity Id and value then it will automaticlly disable the btnDistrict
         */
        #endregion
        public static void ControlEnabledChange(this MyButtonEdit baseEdit, Control prmEdit)
        {
            switch (prmEdit)
            {
                case MyButtonEdit edt:
                    edt.Enabled = baseEdit.Id.HasValue && baseEdit.Id>0;
                    edt.Id = null;
                    edt.EditValue = null;
                    break;
            }

        }

        #region Commnet
        /*
         * Here we have created an extension method simply we will be passing field which entity name here we mostly give Id on Table and value is our Id value very long value so it will go search one by one as it is counting then it will find our RowHandle value and rowhandle is which row in our Table (GridView) then when it finds and values are matching in the end it will be focused on our Row which we passed as parameters
         */
        #endregion
        public static void RowFocus(this GridView table,string field,object value)
        {
            int rowHandle = 0;
            for (int i = 0; i < table.RowCount; i++)
            {
                var result = table.GetRowCellValue(i, field);
                if (value.Equals(result))
                    rowHandle = i;
            }

            table.FocusedRowHandle = rowHandle;
        }

        #region Comment
        /*
         * Here we have created RowFocus for focusing after the deleting
         */
        #endregion
        public static void RowFocus(this GridView table,int rowHandle)
        {
            if (rowHandle<=0) return;

            if (rowHandle == table.RowCount - 1)
                table.FocusedRowHandle = rowHandle;
            else
                table.FocusedRowHandle = rowHandle - 1;
        }


    }
}
