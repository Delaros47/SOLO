using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Business.Base;
using Business.Interfaces;
using Common.Enums;
using Data.Contexts;
using Model.Entities;
using Model.Entities.Base;

namespace Business.General
{
    public class FilterBll:BaseBll<Filter,StudentManagementContext>,IBaseCommonBll
    {

        public FilterBll() { }

        public FilterBll(Control ctrl):base(ctrl) { }

        public BaseEntity Single(Expression<Func<Filter,bool>> filter)
        {
            return BaseSingle(filter, x => x);
        }

        public IEnumerable<BaseEntity> List(Expression<Func<Filter,bool>> filter)
        {
            return BaseList(filter, x => x).OrderBy(x => x.PrivateCode).ToList();
        }

        public bool Insert(BaseEntity entity,Expression<Func<Filter,bool>> filter)
        {
            return BaseInsert(entity, filter);
        }

        public bool Update(BaseEntity oldEntity,BaseEntity currentEntity,Expression<Func<Filter,bool>> filter)
        {
            return BaseUpdate(oldEntity, currentEntity, filter);
        }

        public bool Delete(BaseEntity entity)
        {
            return BaseDelete(entity, FormType.Filter);
        }

        public string GeneratePrivateCode(Expression<Func<Filter, bool>> filter)
        {
            return BaseGeneratePrivateCode(FormType.Filter, x => x.PrivateCode, filter);
        }

    }
}
