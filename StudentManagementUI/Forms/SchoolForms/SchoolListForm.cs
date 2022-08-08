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

namespace StudentManagementUI.Forms.SchoolForms
{
    public partial class SchoolListForm : BaseListForm
    {
        public SchoolListForm()
        {
            InitializeComponent();
            BaseBll = new SchoolBll();
        }

        protected override void FillMyVariables()
        {
            Table = table;
            BaseFormType = FormType.School;
            BaseFormShow = new ShowEditForms<SchoolEditForm>();
            Navigator = longNavigator.Navigator;
        }

        protected override void EntityRefresh()
        {
            Table.GridControl.DataSource = ((SchoolBll)BaseBll).List(FilterFunctions.Filter<School>(ShowActivePassiveList));
        }

    }
}