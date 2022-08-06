using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    #region Comment
    /*
     * IUnitOfWork will do update,select,delete,insert at once to our database,simple if we want to add a city in our database IUnitOfWork will insert to our database
	because here our Save method will sipmly use context.SaveChanges(); only once and all of Delete,Update,and Insert will be going via Save method so there we can         control our exception our try catch only once that's it and also our IUnitOfWork<T> is generic because when we try to reach our IRepository<T> then T is our entity so pass it from here, If we don't use UnitOfWork then each method in Repository we have use context.SaveChanges(); and secondly we have to use try catch exception in each method but UnitOfWork gives us possibities to manage them all easily once
     */
    #endregion
    public interface IUnitOfWork<T> : IDisposable where T : class
    {
        #region Comment
        /*
         *Here with Rep we can access to all Repository<T> functions we don't need to set it only we want to access and use them and whenever we try to reach them we send our T type means that our entity in order to reach Insert,Update,Delete there or Select,Find
         */
        #endregion
        IRepository<T> Rep { get; }
        #region Comment
        /*
         * Save function will return when data is saved or nor or any changes happen or not we will know exactly any changes happened or not
         */
        #endregion
        bool Save();
    }
}
