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
using Model.DTO;
using StudentManagementUI.Functions;
using Model.Entities;

namespace StudentManagementUI.Forms.SchoolForms
{
    public partial class SchoolEditForm : BaseEditForm
    {
        #region Comment
        /*
         * Here if want to use events on EditForms because all controls are inside DataLatoutControl so we have to send it to BaseEditForm
         * Bll = new SchoolBll(myDataLayoutControl); Here we sent our myDataLayoutControl to SchoolBll the reason that we have sent that if we don't write Private Code on SchoolEditForm and when we try to click Save button then first it will give us error then it will be focused on Private Code textbox in order to make focus that we have to send myDataLayoutControl to SchoolBll
         * 
         */
        #endregion
        public SchoolEditForm()
        {
            InitializeComponent();
            BaseDataLayoutControl = myDataLayoutControl;
            BaseBll = new SchoolBll(myDataLayoutControl);
            BaseFormType = FormType.School;
            EventsLoad();
        }
        #region Comment
        /*
         * Here we overrode our MyBaseEditLoads from BaseEditForm here if our ProccessType is EntityInsert then it will set our OldEntity new DTO new SchoolS(); since we will do all our insert,update,delete on DTO that's why it means that we definity know that we will insert a new entity if ProccessType is not EntityInsert if it is EntityUpdate then it will go to Bll find our Entity in our Database then it will set on Database then we could compare OldEntity and CurrentEntity
         * FilterFunctions.Filter<School>(Id) Here is our Function
         * if (ProccessType != ProccessType.EntityInsert) return; If our ProccessType is EntityUpdate then returns because we don't need to create PrivateCode if it is EntityInsert then we create PrivateCode and focus on txtSchool textbox
         */
        #endregion
        protected internal override void MyBaseEditLoads()
        {
            OldEntity = BaseProccessType == ProccessType.EntityInsert ? new SchoolS() : ((SchoolBll)BaseBll).Single(FilterFunctions.Filter<School>(BaseEditId));
            BindEntityToControls();
        }

        protected override void BindEntityToControls()
        {
            var entity = (SchoolS)OldEntity;
            txtPrivateCode.Text = entity.PrivateCode;
            txtSchoolName.Text = entity.SchoolName;
            btnCityName.Id = entity.CityId;
            btnCityName.Text = entity.CityName;
            btnDistrictName.Id = entity.DistrictId;
            btnDistrictName.Text = entity.DistrictName;
            tglState.IsOn = entity.State;
            txtDescription.Text = entity.Description;
        }

    }
}