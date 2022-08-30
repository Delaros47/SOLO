using Business.Base;
using Data.Contexts;
using Model.Entities;
using Model.Entities.Base;
using System;
using System.Collections.Generic;
using Common.Enums;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Forms;
using Business.Interfaces;

namespace Business.General
{
    #region Comment
    /*
     * Here we will not be using IBaseGeneralBll interface the reason that District is not suitable for that 
     */
    #endregion
    public class DistrictBll : BaseBll<District, StudentManagementContext>, IBaseCommonBll
    {
        public DistrictBll() { }

        public DistrictBll(Control ctrl) : base(ctrl) { }

        #region Comment
        /*
         * Here x=>x means that go and get our Distict entity whatever we filtered in Forms we don't need to make like that x=>new District{ Id =x.Id};
         */
        #endregion
        public BaseEntity Single(Expression<Func<District,bool>> filter)
        {
            return BaseSingle(filter,x=>x);
        }

        public IEnumerable<BaseEntity> List(Expression<Func<District, bool>> filter)
        {
            return BaseList(filter,x=>x).OrderBy(x=>x.PrivateCode).ToList();
        }

        #region Comment
        /*
         * The reason we have used filter here that each City will have District-000001 and its the same for Mardin,Antalya so thanks to filter we will make expression for any city 
         */
        #endregion
        public bool Insert(BaseEntity entity, Expression<Func<District, bool>> filter)
        {
            return BaseInsert(entity, filter);
        }

        public bool Update(BaseEntity oldEntity,BaseEntity currentEntity, Expression<Func<District, bool>> filter)
        {
            return BaseUpdate(oldEntity, currentEntity, filter);
        }

        public bool Delete(BaseEntity entity)
        {
            return BaseDelete(entity, FormType.District);
        }

        public string GeneratePrivateCode(Expression<Func<District, bool>> filter)
        {
            return BaseGeneratePrivateCode(FormType.District, x=>x.PrivateCode,filter);
        }




    }
}
