using Business.General;
using StudentManagementUI.Forms.BaseForms;
using Common.Enums;
using Model.Entities;
using StudentManagementUI.Functions;

namespace StudentManagementUI.Forms.DistrictForms
{
    public partial class DistrictEditForm : BaseEditForm
    {
        private readonly long _cityId;
        private readonly string _cityName;

        public DistrictEditForm(params object[] prm)
        {
            InitializeComponent();
            _cityId = (long)prm[0];
            _cityName = prm[1].ToString();
            BaseDataLayoutControl = myDataLayoutControl;
            BaseBll = new DistrictBll(myDataLayoutControl);
            BaseFormType = FormType.District;
            EventsLoad();
        }

        #region Comment
        /*
         * Here Text = Text + $" ({_cityName}) "; whenever we open our DistrictListForm it will be written in the caption as City name
         * 
         */
        #endregion
        protected internal override void MyBaseEditLoads()
        {
            BaseOldEntity = BaseProccessType == ProccessType.EntityInsert ? new District() : ((DistrictBll)BaseBll).Single(FilterFunctions.Filter<District>(BaseEditId));           
            BindEntityToControls();
            if (BaseProccessType != ProccessType.EntityInsert) return;
            txtPrivateCode.Text = ((DistrictBll)BaseBll).GeneratePrivateCode(x => x.CityId == _cityId);
            txtDistrictName.Focus();

        }

        protected override void BindEntityToControls()
        {
            var entity = (District)BaseOldEntity;
            txtPrivateCode.Text = entity.PrivateCode;
            txtDistrictName.Text = entity.DistrictName;
            tglState.IsOn = entity.State;
            txtDescription.Text = entity.Description;
        }

        protected override void CreateUpdatedEntity()
        {
            BaseCurrentEntity = new District
            {
                Id = BaseEditId,
                PrivateCode = txtPrivateCode.Text,
                CityId = _cityId,
                DistrictName = txtDistrictName.Text,
                State = tglState.IsOn,
                Description = txtDescription.Text
            };

            ButtonEnabledState();
        }

        #region Comment
        /*
         * Here x=>x.PrivateCode==BaseCurrentEntity.PrivateCode && x.CityId==_cityId First expression is that our Private Code should be equal to our CurrentEntity Private Code because we might accidentlly changed during updating or inserting that's why
         * And second expression is that we will be inserting a new District according to our CityId so we set our Insert value
         * 
         */
        #endregion
        protected override bool EntityInsert()
        {
            return ((DistrictBll)BaseBll).Insert(BaseCurrentEntity,x=>x.PrivateCode==BaseCurrentEntity.PrivateCode && x.CityId==_cityId);
        }


        protected override bool EntityUpdate()
        {
            return ((DistrictBll)BaseBll).Update(BaseOldEntity, BaseCurrentEntity,x=>x.PrivateCode==BaseCurrentEntity.PrivateCode && x.CityId==_cityId);

        }



    }
}