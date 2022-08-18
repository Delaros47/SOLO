using Model.Entities.Base;
using System;
using System.Linq.Expressions;

namespace StudentManagementUI.Functions
{
    public class FilterFunctions
    {
        #region Comment
        /*
         * Here we have created Filter function it returns expressin and our T type is our Entities such as School,City,District it sets the State of our expression and returns back
         */
        #endregion
        public static Expression<Func<T,bool>> Filter<T>(bool showActivePassiveList) where T:BaseEntityState
        {
            return x => x.State == showActivePassiveList;

        }
        #region Comment
        /*
         * Here we have created Filter function it returns expressin and our T type is our Entities such as School,City,District it sets our Id value
         */
        #endregion
        public static Expression<Func<T,bool>> Filter<T>(long id) where T :BaseEntityState
        {
            return x => x.Id == id;
        }
  

    }
}
