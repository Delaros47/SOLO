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
        private IUnitOfWork<T> _uow;

        protected BaseBll() { }

        protected BaseBll(Control ctrl)
        {
            _ctrl = ctrl;
        }

        protected TResult BaseSingle<TResult>(Expression<Func<T,bool>> filter,Expression<Func<T,TResult>> selector)
        {
            GeneralFunctions.CreateUnitOfWork<T, TContext>(ref _uow);
            return _uow.Rep.Find(filter,selector);
        }

        protected IQueryable<TResult> BaseList<TResult>(Expression<Func<T,bool>> filter,Expression<Func<T,TResult>> selector)
        {
            GeneralFunctions.CreateUnitOfWork<T, TContext>(ref _uow);
            return _uow.Rep.Select(filter, selector);
        }

        protected bool BaseInsert(BaseEntity entity,Expression<Func<T,bool>> filter)
        {
            GeneralFunctions.CreateUnitOfWork<T, TContext>(ref _uow);
            _uow.Rep.Insert(entity.EntityConvert<T>());
            return _uow.Save();
        }

        protected bool BaseUpdate(BaseEntity oldEntity,BaseEntity currentEntity,Expression<Func<T,bool>> filter)
        {
            GeneralFunctions.CreateUnitOfWork<T, TContext>(ref _uow);
            var changedFields = GeneralFunctions.GetChangedFields(oldEntity, currentEntity);
            if (changedFields.Count == 0) return true;
            _uow.Rep.Update(currentEntity.EntityConvert<T>(), changedFields);
            return _uow.Save();
        }

    }
}
