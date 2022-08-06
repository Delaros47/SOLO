﻿using DataAccess.Interfaces;
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
