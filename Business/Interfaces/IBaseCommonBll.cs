using Model.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    #region Comment
    /*
     * Here we have created IBaseCommonBll it is just for deleting in both Edit and List forms since Delete only has BaseEntity parameters and all of the Forms are the same that's why we create this interface we have another interface named IBaseGeneralBll it has insert,update and generate private code for example we didn't use it on District Form because there in DistrictBll we have different parameters additionally we have added filter that's why we didn't use it on District instead we have overrode it on District but other City and School we have already in BaseEditForm Insert and Update we don't need to override on their forms it automaticlly does insert,update on BaseEditForm since this IBaseCommonBll is the same parameters so it will do in BaseForms for deleting we only need an entity from BaseEntity and now we will going to their Bll classes we will implement IBaseCommonBll interface then we could access from the BaseEditForm and BaseListForm
     */
    #endregion
    public interface IBaseCommonBll
    {
        bool Delete(BaseEntity entity);
    }
}
