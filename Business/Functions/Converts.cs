using Model.Entities.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Functions
{
    public static class Converts
    {
        #region Comment
        /*
         * This is our Entity convert function it will convert from DTO to entities first it will get their properties if they have the same name then it will assign the values on them and TTarget is our DTO and source is our normal Entity such as School,City,District
         */
        #endregion
        public static TTarget EntityConvert<TTarget>(this IBaseEntity source)
        {
            #region Comment
            /*
             * If the source is null then it returns null value
             * destination is our target so we create an instance from it
             * sourceProp and destinationProp we reach both of them properties via reflection so since destinationProp is generic so we have to access to its properties by typeof
             * It simply takes all values from DTO to entity and if it is empty it assign null value
             *  
             */
            #endregion
            if (source == null) return default(TTarget);
            var destination = Activator.CreateInstance<TTarget>();
            var sourceProp = source.GetType().GetProperties();
            var destinationProp = typeof(TTarget).GetProperties();
            #region Comment
            /*
                Here it create instance from the Generic because in order to reach its properties we have to create a class instance later we  will set its properties from other which has value here is  var value = sp.GetValue(source); each time get properties value from sourceProperties
    var dp = destinationProp.FirstOrDefault(x=>x.Name==sp.Name); Here we go to our destination and start compare the Properties if propery name is the same then it assigns the name of it into dp and dp!=null here if it is not null means that if property name is matching then here we set our property value to dp dp.SetValue(destination,ReferenceEquals(value,"")?null:value); well if string value is "" (empty) then it will save it on database as "" (empty) so we don't want that we want it to be saved as null
                     */

            #endregion
            foreach (var sp in sourceProp)
            {
                var value = sp.GetValue(source);
                var dp = destinationProp.FirstOrDefault(x => x.Name == sp.Name);
                if (dp != null)
                {
                    dp.SetValue(destination, ReferenceEquals(value, "") ? null : value);
                }
            }
            return destination;
        }

    }
}
