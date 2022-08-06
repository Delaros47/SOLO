using Business.Functions;
using DataAccess.Interfaces;
using Model.Entities.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Business.Base
{
    public class BaseBll<T, TContext>
        where T : BaseEntity
        where TContext : DbContext
    {


        private readonly Control _ctrl;
        private IUnitOfWork<T> _uof;

        protected BaseBll()
        {

        }

        protected BaseBll(Control ctrl)
        {
            _ctrl = ctrl;
        }

        protected TResult BaseSingle<TResult>(Expression<Func<T,bool>> filter,Expression<Func<T,TResult>> selector)
        {
            GeneralFunctions.CreateUnitOfWork<T, TContext>(ref _uof);
            return _uof.Rep.Find(filter,selector);
        }

        protected IQueryable<TResult> BaseList<TResult>(Expression<Func<T,bool>> filter,Expression<Func<T,TResult>> selector)
        {
            GeneralFunctions.CreateUnitOfWork<T, TContext>(ref _uof);
            return _uof.Rep.Select(filter, selector);
        }

        protected bool BaseInsert(BaseEntity entity,Expression<Func<T,bool>> filter)
        {
            GeneralFunctions.CreateUnitOfWork<T, TContext>(ref _uof);
            //return _uof.Rep.Insert()
        }

    }
}
