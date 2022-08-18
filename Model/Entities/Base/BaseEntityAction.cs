﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities.Base
{
    #region Comment
    /*
     * Here BaseEntityAction is that it will automatically create any id and then send it to the database but main thing that we will be using BaseEntityAction some of our GridView will be editable so we will be using it for that purpose for example we will click our GridView and change directly data on it
     */
    #endregion
    public class BaseEntityAction
    {
        public int Id { get; set; }

    }
}
