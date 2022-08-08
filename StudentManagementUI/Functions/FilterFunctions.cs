using Model.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementUI.Functions
{
    public class FilterFunctions
    {

        public static Expression<Func<T,bool>> Filter<T>(bool showActivePassiveList) where T:BaseEntityState
        {
            return x => x.State == showActivePassiveList;

        }

    }
}
