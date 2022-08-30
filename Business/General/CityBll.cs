using Business.Base;
using Business.Interfaces;
using Data.Contexts;
using Model.Entities;
using Model.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Common.Enums;
using System.Windows.Forms;

namespace Business.General
{
    public class CityBll : BaseBll<City, StudentManagementContext>, IBaseGeneralBll,IBaseCommonBll
    {
        public CityBll() { }

        public CityBll(Control ctrl) : base(ctrl) { }

        public BaseEntity Single(Expression<Func<City, bool>> filter)
        {
            return BaseSingle(filter, x => x);
        }

        public IEnumerable<BaseEntity> List(Expression<Func<City, bool>> filter)
        {
            return BaseList(filter, x => x).OrderBy(x => x.PrivateCode).ToList();
        }

        public bool Insert(BaseEntity entity)
        {
            return BaseInsert(entity, x => x.PrivateCode == entity.PrivateCode);
        }

        public bool Update(BaseEntity oldEntity, BaseEntity currenyEntity)
        {
            return BaseUpdate(oldEntity, currenyEntity, x => x.PrivateCode == currenyEntity.PrivateCode);
        }

        public bool Delete(BaseEntity entity)
        {
            return BaseDelete(entity, FormType.City);
        }

        public string GeneratePrivateCode()
        {
            return BaseGeneratePrivateCode(FormType.City, x => x.PrivateCode);
        }
    }
}
