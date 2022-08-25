using Business.General;
using StudentManagementUI.Forms.BaseForms;
using Common.Enums;
using StudentManagementUI.Show;
using StudentManagementUI.Functions;
using Model.Entities;
using DevExpress.XtraBars;
using StudentManagementUI.Forms.DistrictForms;

namespace StudentManagementUI.Forms.CityForms
{
    public partial class CityListForm : BaseListForm
    {
        public CityListForm()
        {
            InitializeComponent();
            BaseBll = new CityBll();
            #region Comment
            /*
             * Here we have set our btnConnectedBarButtonItem as Districts whenever it opens CityListForm
             */
            #endregion
            btnConnectedBarButtonItem.Caption = "Districts";
        }

        protected override void FillMyVariables()
        {
            BaseTable = table;
            BaseFormType = FormType.City;
            BaseFormShow = new ShowEditForms<CityEditForm>();
            BaseNavigator = longNavigator.Navigator;
            #region Comment
            /*
             * Here we say that if our Form is MdiChild then set our ShowItems as btnConnectedBarButtonItem
             */
            #endregion
            if (IsMdiChild)
                ShowItems = new BarItem[] { btnConnectedBarButtonItem};

        }

        protected override void EntityRefresh()
        {
            BaseTable.GridControl.DataSource = ((CityBll)BaseBll).List(FilterFunctions.Filter<City>(BaseShowActivePassiveList));
        }

        protected override void EntityConnectedBarButtonItem()
        {
            var entity = BaseTable.GetRow<City>();
            if (entity == null) return;
            ShowListForms<DistrictListForm>.ShowListForm(FormType.District,entity.Id,entity.CityName);
        }
    }
}