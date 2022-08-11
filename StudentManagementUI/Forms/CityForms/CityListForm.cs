using Business.General;
using DevExpress.XtraEditors;
using StudentManagementUI.Forms.BaseForms;
using Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StudentManagementUI.Show;
using StudentManagementUI.Functions;
using Model.Entities;

namespace StudentManagementUI.Forms.CityForms
{
    public partial class CityListForm : BaseListForm
    {
        public CityListForm()
        {
            InitializeComponent();
            BaseBll = new CityBll();
        }

        protected override void FillMyVariables()
        {
            BaseTable = table;
            BaseFormType = FormType.City;
            BaseFormShow = new ShowEditForms<CityEditForm>();
            BaseNavigator = longNavigator.Navigator;
        }

        protected override void EntityRefresh()
        {
            BaseTable.GridControl.DataSource = ((CityBll)BaseBll).List(FilterFunctions.Filter<City>(BaseShowActivePassiveList));
        }
    }
}