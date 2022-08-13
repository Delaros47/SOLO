using Business.Interfaces;
using Common.Enums;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Model.Entities.Base;
using StudentManagementUI.Functions;
using StudentManagementUI.Show.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentManagementUI.Forms.BaseForms
{
    public partial class BaseListForm : RibbonForm
    {
        #region Comment
        /*
         * Here we have declared IBaseFormShow interface from other Forms we will be passing EditForms here since interface keeps referances
         * FormType what kind of forms we use we want to know of couse in delete method here also we want to get Description attribute from it as well in other things we will be using it
         * Here GridView Table; we will be sending our GridView from other Forms here because we will do a lot of things on our GridView firstly when we Edit button it will be opened as EditForms we will be updating data
         * Here BaseShowActivePassiveList = true; whenever we click on our Active Passive List so as default we set it true but when we click it that it will be set false and will list passive records
         * bool BaseMultiSelect; means that in our some GridView we could select multiply columns and save them all, they will have checkbox in the beggining
         * BaseEntity BaseSelectedEntity; Here whenever we choose from ButtonEdit and our GridView opens then we double click or Select button then it will save our Row into BaseEntity BaseSelectedEntity;
         * IBaseBll BaseBll; Here we send all our Business codes here we can delete,update insert or list any entity here thanks to IBaseBll interface it will get all kind of Business codes such CityBll(); DistrictBll(); becuase these classes inherited from BaseBll and BaseBll inherited from IBaseBll that means IBaseBll can hold their referances to reach it
         * ControlNavigator BaseNavigator; Here we will be sending from other forms Navigator here so it will be set here
         * 
         */
        #endregion

        protected IBaseFormShow BaseFormShow;
        protected FormType BaseFormType;
        protected internal GridView BaseTable;
        protected bool BaseShowActivePassiveList = true;
        protected internal bool BaseMultiSelect;
        protected internal BaseEntity BaseSelectedEntity;
        protected IBaseBll BaseBll;
        protected ControlNavigator BaseNavigator;
        protected internal long? BaseListSelectedId;

        public BaseListForm()
        {
            InitializeComponent();

        }

        #region Comment
        /*
         * Here we hava created a method called EventsLoad(); all our events will be here we will do our best to write all codes in base forms in order to write less codes in the projects 
         * Here our all buttons in RibbonControl are BarButtonItem but except the Send button it is BarSubItem so we have to look for inheritted for both of them and commen inheritted should have ItemClick item so when we go to their Go To Definition we find out that both of them are inheritted from BarItem that's why we have written it on case
         * 
         */
        #endregion
        private void EventsLoad()
        {
            //Button Events
            foreach (var item in ribbonControl.Items)
            {
                switch (item)
                {
                    case BarItem button:
                        button.ItemClick += Button_ItemClick;
                        break;
                    default:
                        break;
                }
            }

            //Table Events
            BaseTable.DoubleClick += Table_DoubleClick;
            BaseTable.KeyDown += Table_KeyDown;
            //Form Events

            #region Comment
            /*
             * This our event whenever first shows us that and Shown event will function 
             */
            #endregion
            Shown += BaseListForm_Shown;

        }

        private void BaseListForm_Shown(object sender, EventArgs e)
        {
            BaseTable.Focus();
            HideShowButtons();
            HideShowColumn();
        }

        private void HideShowColumn()
        {
            
        }

        private void HideShowButtons()
        {
            
        }

        #region Comment
        /*
         * This MyBaseListLoads will run in MainForm whenever we try to open a new ListForm this method will function and all Methods,Variables and Events will be loaded normally
         * We set MultiSelect and Navigator on MyBaseListLoads() and also it will refresh our Table (GridView) while we are running it
         */
        #endregion
        protected internal void MyBaseListLoads()
        {
            FillMyVariables();
            EventsLoad();
            BaseTable.OptionsSelection.MultiSelect = BaseMultiSelect;
            BaseNavigator.NavigatableControl = BaseTable.GridControl;
            Cursor.Current = Cursors.WaitCursor;
            EntityRefresh();
            Cursor.Current = DefaultCursor;
        }
        #region Comment
        /*
         * Here we will be set Table,FormType,and other here since it is virtual so it requires to be inheritted from other list form and set there here it will be empty we can access all others because they are protected
         */
        #endregion
        protected virtual void FillMyVariables() { }

        #region Comment
        /*
         * Here when we press any key on our (Table)GridView
         */
        #endregion
        private void Table_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    EntitySelectProccessType();
                    break;
                case Keys.Escape:
                    Close();
                    break;
            }
        }

        #region Comment
        /*
         * Here we created event that if we double click on our (Table)GridView that according to EntitySelectProccessType will open Edit Forms or get Row from Table into ButtonEdit our EntitySelectProccessType will check it
         */
        #endregion
        private void Table_DoubleClick(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            EntitySelectProccessType();
            Cursor.Current = DefaultCursor;
        }
        #region Comment
        /*
         * Here is whenever we double click on our Table(GridView) but if double click or Enter first one we selecting values into ButtonEdit second one is for opening EditForms with values
         */
        #endregion
        private void EntitySelectProccessType()
        {
            #region Comment
            /*
             * Here if our Form is not MdiChild means that Doesn't open inside main form as mdi then it will run EntitySelect();
             * For example some ListForms will be selected values and those values will be inside our ButtonEdit control
             * If it is  MdiChild means that in our GridView we double click then it will open an EditForm in order to update it
             * 
             */
            #endregion
            if (!IsMdiChild)
            {
                EntitySelect();
            }
            else
            {
                btnEdit.PerformClick();
            }
        }

        #region Comment
        /*
         * Here whenever we select our Table(GridView) sometimes we are able to select multiple rows and its values so here we put condition for checking for that
         */
        #endregion
        private void EntitySelect()
        {
            if (BaseMultiSelect)
            {

            }
            else
            {
                #region Comment
                /*
                 * Here we will create extension method it will get Row from GridView then it will returns to us as BaseEntity type so now we go to GeneralFunctions method and start writing it
                 * 
                 */
                #endregion
                BaseSelectedEntity = BaseTable.GetRow<BaseEntity>();
            }
            #region Comment
            /*
             * Here We have written below if we select our Row then it will put DialogResult OK then it will close our ListForm then we will be using DialogResult
             */
            #endregion
            DialogResult = DialogResult.OK;
            Close();
        }

        #region Comment
        /*
         * Here ShowEdit Form will open EditForms if id is -1 then it will open an empty EditForm if not then it will set the Id in EditForms in order to be able to make updates there
         * We have create interface IBaseFormShow FormShow; so simply we will be passing all our EditForms here it keeps referances then our ShowEditForm will be functioned in New,Edit buttons and in double click Gridview 
         * Here we have changed the modifier of our ShowEditForms because in entity such District in some entities we required params[] we have to override there in order to use alternative method we need to pass params[] parameters from the constructor now we have to make another method in ShowEditForms but this time we will be passing with params[]
         */
        #endregion
        protected virtual void ShowEditForms(long id)
        {
            var result = BaseFormShow.ShowEditDialogForm(BaseFormType, id);
        }

        private void EntityDelete()
        {
            
        }

        private void EntityPrint()
        {
           
        }

        private void EntityFilter()
        {
            
        }
        #region Comment
        /*
         * Here we made our EntityRefresh method as virtual cause we will not anything here we will override on other ListForms
         */
        #endregion
        protected virtual void EntityRefresh() { }

        

        private void EntityActivePassiveListCaption()
        {
            
        }


        private void Button_ItemClick(object sender, ItemClickEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            if (e.Item == btnSend)
            {
                var link = (BarSubItemLink)e.Item.Links[0];
                link.Focus();
                link.OpenMenu();
                link.Item.ItemLinks[0].Focus();
            }
            else if (e.Item == btnExcelFileStandard)
            {

            }
            else if (e.Item == btnExcelFileFormatted)
            {

            }
            else if (e.Item == btnExcelFileUnformatted)
            {

            }
            else if (e.Item == btnPdfFile)
            {

            }
            else if (e.Item == btnWordFile)
            {

            }
            else if (e.Item == btnTxtFile)
            {

            }
            else if (e.Item == btnNew)
            {
                #region Comment
                /*
                 * Here Whenever we click New button that it runs ShowEditForm and ShowEditForm is class that will open our EditForms but when it opens we have to send id as well if id =-1 then it will open empty EditForm if not it will set id in EditForm and our data will be there too in order to update them that's why we gave -1 here
                 */
                #endregion
                ShowEditForms(-1);
            }
            else if (e.Item == btnEdit)
            {
                #region Comment
                /*
                 * Here Whenever we click New button that it runs ShowEditForm and ShowEditForm is class that will open our EditForms but when it opens we have to send id as well if id =-1 then it will open empty EditForm if not it will set id in EditForm and our data will be there too in order to update them that's why we gave -1 here
                 * And also we will create an extension method in GeneralFunctions so it will also prevent if we focus on not Row other part of GridView it will give message as well
                 */
                #endregion
                ShowEditForms(BaseTable.GetRowId());
            }
            else if (e.Item==btnDelete)
            {
                EntityDelete();
            }
            else if (e.Item==btnSelect)
            {
                EntitySelect();
            }
            else if (e.Item==btnRefresh)
            {
                EntityRefresh();
            }
            else if (e.Item==btnFilter)
            {
                EntityFilter();
            }
            else if (e.Item==btnColumns)
            {
                #region Comment
                /*
                 * Here Whenever we click on our Columns button it will show down right side CustomizationForm we could move or move back our columns to our GridView
                 */
                #endregion
                if (BaseTable.CustomizationForm==null)
                {
                    BaseTable.ShowCustomization();
                }
                else
                {
                    BaseTable.HideCustomization();
                }
            }
            else if (e.Item==btnPrint)
            {
                EntityPrint();
            }
            else if (e.Item==btnExit)
            {
                Close();
            }
            else if (e.Item==btnActivePassiveList)
            {
                BaseShowActivePassiveList = !BaseShowActivePassiveList;
                EntityActivePassiveListCaption();
            }

            Cursor.Current = DefaultCursor;

        }

        
    }
}