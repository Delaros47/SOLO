using Business.General;
using StudentManagementUI.Forms.BaseForms;
using Common.Enums;
using Model.Entities;
using StudentManagementUI.Functions;

namespace StudentManagementUI.Forms.CityForms
{
    public partial class CityEditForm : BaseEditForm
    {
        public CityEditForm()
        {
            InitializeComponent();
            BaseDataLayoutControl = myDataLayoutControl;
            BaseBll = new CityBll(myDataLayoutControl);
            BaseFormType = FormType.City;
            EventsLoad();
        }


        protected internal override void MyBaseEditLoads()
        {
            BaseOldEntity = BaseProccessType == ProccessType.EntityInsert ? new City() : ((CityBll)BaseBll).Single(FilterFunctions.Filter<City>(BaseEditId));
            BindEntityToControls();
            if (BaseProccessType != ProccessType.EntityInsert) return;
            BaseEditId = BaseProccessType.CreateId(BaseOldEntity);
            txtPrivateCode.Text = ((CityBll)BaseBll).GeneratePrivateCode();
            txtCityName.Focus();

        }

        protected override void BindEntityToControls()
        {
            var entity = (City)BaseOldEntity;
            txtPrivateCode.Text = entity.PrivateCode;
            txtCityName.Text = entity.CityName;
            txtDescription.Text = entity.Description;
            tglState.IsOn = entity.State;
        }

        protected override void CreateUpdatedEntity()
        {
            BaseCurrentEntity = new City
            {
                Id= BaseEditId,
                PrivateCode = txtPrivateCode.Text,
                CityName = txtCityName.Text,
                State = tglState.IsOn,
                Description = txtDescription.Text,
            };

            ButtonEnabledState();
        }



    }
}