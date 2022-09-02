using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Business.General;
using Common.Enums;
using StudentManagementUI.Forms.BaseForms;

namespace StudentManagementUI.Forms.FilterForms
{
    public partial class FilterListForm : BaseListForm
    {
        public FilterListForm()
        {
            InitializeComponent();
            BaseBll = new FilterBll();
        }

        protected override void FillMyVariables()
        {
            BaseTable = table;
            BaseFormType = FormType.Filter;
            BaseNavigator = longNavigator.Navigator;

        }
    }
}