using DevExpress.XtraEditors;
using StudentManagementUI.Forms.BaseForms;
using System;
using Common.Enums;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Business.General;
using StudentManagementUI.Show;

namespace StudentManagementUI.Forms.DistrictForms
{
    public partial class DistrictListForm : BaseListForm
    {
        #region Comment
        /*
         * Here is our District Form will not be opening from the Main Form only from CityListForm we will be adding extra Connected Card to our BaseListForm in order to change its name as Districts on CityListForm and also we send _cityId and _cityName from constructor parameters as prm 
         */
        #endregion
        private readonly long _cityId;
        private readonly string _cityName;

        public DistrictListForm(params object[] prm)
        {
            InitializeComponent();
            _cityId = (long)prm[0];
            _cityName = prm[1].ToString();
            BaseBll = new DistrictBll();
        }


        protected override void FillMyVariables()
        {
            BaseTable = table;
            BaseFormType = FormType.District;
            BaseNavigator = longNavigator.Navigator;
            Text = Text + $" ({_cityName}) ";
        }

        protected override void EntityRefresh()
        {
            #region Comment
            /*
             * Here since our District required a CityId so according to CityId we will be listing it on Table (GridView) first Expression is it will list active record and secondly let's say that we would like to list Mardin so it will only go for Mardin City Id and brings its Districts as it starts from District-000001 and each City's District starts like that
             */
            #endregion
            BaseTable.GridControl.DataSource = ((DistrictBll)BaseBll).List(x => x.State == BaseShowActivePassiveList && x.CityId == _cityId);
        }

        #region Comment
        /*
         * The reason we have overrode that we need to use the one with params so we could get CityId and CityName and CityId for adding a new Distirct and CityName is for showing in DistrictListForm caption in order for use to be undertood which city user is trying to add 
         * Here we have used ShowEditFormsDefault(result); because since overrode our District so then for focuses we have to use ShowEditFormsDefault method it our Focuses should be running on DistrictListForm as well
         */
        #endregion
        protected override void ShowEditForms(long id)
        {
            var result = new ShowEditForms<DistrictEditForm>().ShowEditDialogForm(FormType.District, id, _cityId, _cityName);
            ShowEditFormsDefault(result);
        }

    }
}