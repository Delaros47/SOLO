using Common.Enums;
using Common.Functions;
using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Base
{
    public class Repository<T> : IRepository<T> where T : class
    {
        #region Comment
        /*
         * Here Whenever we call our Repository class that we have to pass our Context means StudentManagementContext because we have to reach our context in order to make insert,update,delete and select so if it is null then it returns null if not then it return and _dbSet; is our Entities will be here and we set it already from our _context we will be using or _dbSet (Entity Table)
         */
        #endregion
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(DbContext context)
        {
            #region Comment
            /*
             * Here as we know that Context is our database and DbSet is our table when we want use it in entity framework we need both of them for using and we using DbSet especially when we use Find and Select queries
             */
            #endregion
            if (context == null)
            {
                return;
            }
            else
            {
                _context = context;
                _dbSet = context.Set<T>();
            }

        }

        #region Comment
        /*
         * Here it will simply insert one entity to our database
         */
        #endregion
        public void Insert(T entity)
        {
            _context.Entry(entity).State = EntityState.Added;
        }

        #region Comment
        /*
         * Here it will simply insert multiple entities to our database
         */
        #endregion
        public void Insert(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                _context.Entry(entity).State = EntityState.Added;
            }
        }

        #region Comment
        /*
         * Here it will simply updates entire one entity
         */
        #endregion
        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        #region Comment
        /*
         * Here it will simply updates one entity but with only certain Columns we want in this way it will be more performnaced for example in City we might only want to update CityName not PrivateCode,State or Description
         */
        #endregion
        public void Update(T entity, IEnumerable<string> fields)
        {
            #region Comment
            /*
             * Here we have attached our _dbSet with entity we want _dbSet to know which entity we will be working
             */
            #endregion
            _dbSet.Attach(entity);
            var entry = _context.Entry(entity);
            foreach (var field in fields)
            {
                #region Comment
                /*
                 * Here we will let it which field will be Modified(Updated) it if our field means that Columns in table here exists in fields parameters then it will be updated we set it as true
                 */
                #endregion
                entry.Property(field).IsModified = true;
            }
        }

        #region Comment
        /*
         * Here it will simply updates multiple entities
         */
        #endregion
        public void Update(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                _context.Entry(entity).State = EntityState.Modified;
            }
        }

        #region Comment
        /*
         * Here it will simply deletes one entity in our Database
         */
        #endregion
        public void Delete(T entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
        }

        #region Comment
        /*
         * Here it will simply deletes multiple entities in our Database
         */
        #endregion
        public void Delete(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                _context.Entry(entity).State = EntityState.Deleted;
            }
        }

        #region Comment
        /*
         * Here we have Find method but it returns TResult if we use this Expression<Func<T, bool>> filter but when we try to use _dbSet.Select() method it will give error that it will not work because it takes T type and return a bool but we need it takes T and return TResult we will go to interface we need to declare a selector there because when we use _dbSet.Select() then is asks selector so we will give T type and return TResult as selector expression because we cannot use T,bool since we need to takes T type and returns TResult
         * here if our filter is null then we will not be using it cause it will only returns _dbSet.Select(selector).FirstOrDefault() means that only TResult will be returned if our filter is not null then it will function our filter then returns as TResult
         */
        #endregion
        public TResult Find<TResult>(Expression<Func<T, bool>> filter, Expression<Func<T, TResult>> selector)
        {
            return filter == null ? _dbSet.Select(selector).FirstOrDefault() : _dbSet.Where(filter).Select(selector).FirstOrDefault();
        }

        #region Comment
        /*
         * Here IQueryable<TResult> return Sql result codes just when we make query in the end code we will be adding ToList() or Order By and send our code to database
         */
        #endregion
        public IQueryable<TResult> Select<TResult>(Expression<Func<T, bool>> filter, Expression<Func<T, TResult>> selector)
        {
            return filter == null ? _dbSet.Select(selector) : _dbSet.Where(filter).Select(selector);
        }

        #region Comment
        /*
         * Here we have created GeneratePrivateCode method that it will generate a Private Code for our EditForms
         */
        #endregion
        public string GeneratePrivateCode(FormType formType, Expression<Func<T, string>> filter, Expression<Func<T, bool>> where = null)
        {
            #region Comment
            /*
             * Here our FirstPrivateCode method it will run when we want to create a first Private Code for any Entity and then it will go to our FormType method and it will run our extension mehod ToName then get the Attribute Description to us then split If let's say that it School Record then it will get School only then in the end will insert -000001 to it School-000001 sometimes our FormType name can be longer than two word such as School Group Record then it will take only 
             * School Group-000001
             */
            #endregion
            string FirstPrivateCode()
            {
                string privateCode = null;
                var formTypeName = formType.ToName().Split(' ');

                for (int i = 0; i < formTypeName.Length - 1; i++)
                {
                    privateCode += formTypeName[i];
                    if ((i + 1) == 2)
                    {
                        privateCode += " " + formTypeName[i];
                        break;
                    }

                }

                return privateCode + "-000001";

            }

            #region Comment
            /*
             * Here we have created NotFirstPrivateCode whenever we have already Private Code on our database so it will be increase by 1 then it returns to us our value
             */
            #endregion
            string NotFirstPrivateCode(string privateCode)
            {
                string digits = null;
                string value = null;
                foreach (var character in privateCode)
                {
                    if (char.IsDigit(character))
                    {
                        digits += character;
                    }
                    else
                    {
                        value += character;
                    }
                }
                var oldDigits = digits;
                var newDigits = (int.Parse(digits) + 1).ToString();
                int minus = (oldDigits.Length) - (newDigits.Length);
                if (minus>0)
                {
                    for (int i = 1; i <=minus; i++)
                    {
                       newDigits = "0" +newDigits;
                    }
                }

                return value + newDigits;

            }

            #region Comment
            /*
             * Here first if we have any PrivateCode that it will return us as maxPrivateCode if not then our var will be null if it is null then here our condition return maxPrivateCode == null ? FirstPrivateCode() : NotFirstPrivateCode(maxPrivateCode); it will generate First Time a new with method called FirstPrivateCode();
             */
            #endregion
            var maxPrivateCode = where == null ? _dbSet.Max(filter) : _dbSet.Where(where).Max(filter);
            return maxPrivateCode == null ? FirstPrivateCode() : NotFirstPrivateCode(maxPrivateCode);

        }


        private bool _disposedValue;
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    #region Comment
                    /*
                     * Here if it is dispose then it will remove our _context from the memory
                     */
                    #endregion
                    _context.Dispose();
                }
                _disposedValue = true;
            }
        }

        void IDisposable.Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }


    }
}
