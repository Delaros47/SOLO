using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using StudentManagementUI.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementUI.UserControls.Grids
{
    [ToolboxItem(true)]
    public class MyGridControl:GridControl
    {






    }
    public class MyGridView :GridView, IStatusBarShortcut
    {











        public string StatusBarShortcut { get; set; }
        public string StatusBarShortcutDescription { get; set; }
        public string StatusBarDescription { get; set; }
    }

    public class MyGridColumn :GridColumn, IStatusBarShortcut
    {










        public string StatusBarShortcut { get; set; }
        public string StatusBarShortcutDescription { get; set; }
        public string StatusBarDescription { get; set; }
    }
}
