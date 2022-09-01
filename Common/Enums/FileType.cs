using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Enums
{
    #region Comment
    /*
     * Here we have created our enum name FileType it will basiclly have our Export Data names
     */
    #endregion
    public enum FileType:byte
    {
        ExcelStandard=1,
        ExcelFormatted=2,
        ExcelUnFormatted=3,
        PdfFile=4,
        WordFile=5,
        TxtFile=6
    }
}
