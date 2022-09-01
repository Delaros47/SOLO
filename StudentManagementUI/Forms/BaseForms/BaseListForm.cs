using Business.Interfaces;
using Common.Enums;
using DevExpress.Utils.Extensions;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Model.Entities.Base;
using StudentManagementUI.Functions;
using StudentManagementUI.Show.Interfaces;
using System;
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
         * protected internal bool BaseShowActivePassiveListButton = false; Here we set it as false if it is false then we will not be showing our Select button on ribbonControl
         * protected BarItem[] ShowItems; Here we have declared ShowItems as array we will be showing some of our BarItem as Visibility true or false for example in CityListForm we will be showing Districts as true
         * BaseFormTemplateWillBeSaved; BaseTableTemplateWillBeSaved; we have here two variables that one is for saving template of for ListForm another one is just for Table(GridView)
         */
        #endregion
        private bool BaseFormTemplateWillBeSaved;
        private bool BaseTableTemplateWillBeSaved;
        protected IBaseFormShow BaseFormShow;
        protected FormType BaseFormType;
        protected internal GridView BaseTable;
        protected bool BaseShowActivePassiveList = true;
        protected internal bool BaseShowActivePassiveListButton = false;
        protected internal bool BaseMultiSelect;
        protected internal BaseEntity BaseSelectedEntity;
        protected IBaseBll BaseBll;
        protected ControlNavigator BaseNavigator;
        protected internal long? BaseListSelectedId;
        protected BarItem[] ShowItems;
        protected BarItem[] HideItems;

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
                }
            }

            //Table Events
            #region Comment
            /*
             * First event invokes whenever we double click on our Table(GridView)
             * Second event invokes whenever we press any key on our Table(GridView)
             * Third event invokes whenever we right click with our mouse and we release it on our Table(GridView)
             * Fourth event invokes whenever we change any column with within our Table(GridView)
             * Fifth event invokes whenever we change any position of any column within our Table(GridView)
             * Sixth event invokes whenever we make any column sorting from A-Z or Z-A in our Table(GridView)
             */
            #endregion
            BaseTable.DoubleClick += Table_DoubleClick;
            BaseTable.KeyDown += Table_KeyDown;
            BaseTable.MouseUp += BaseTable_MouseUp;
            BaseTable.ColumnWidthChanged += BaseTable_ColumnWidthChanged;
            BaseTable.ColumnPositionChanged += BaseTable_ColumnPositionChanged;
            BaseTable.EndSorting += BaseTable_EndSorting;

            //Form Events
            #region Comment
            /*
             * This our event whenever first shows us that and Shown event will function
             * Second event invokes whenever we open our ListForms
             * Third event invokes whenever we close our ListForms
             * Forth event invokes whenever we change location of our ListForms
             * Fifth event invokes whenever we change the size of our ListForms
             */
            #endregion
            Shown += BaseListForm_Shown;
            Load += BaseListForm_Load;
            Closing += BaseListForm_Closing;
            LocationChanged += BaseListForm_LocationChanged;
            SizeChanged += BaseListForm_SizeChanged;
            

        }

        private void BaseListForm_SizeChanged(object sender, EventArgs e)
        {
            if (!IsMdiChild)
                BaseFormTemplateWillBeSaved = true;
        }

        private void BaseListForm_LocationChanged(object sender, EventArgs e)
        {
            if (!IsMdiChild)
                BaseFormTemplateWillBeSaved = true;
        }

        private void BaseTable_EndSorting(object sender, EventArgs e)
        {
            BaseTableTemplateWillBeSaved = true;
        }

        private void BaseTable_ColumnPositionChanged(object sender, EventArgs e)
        {
            BaseTableTemplateWillBeSaved = true;
        }

        private void BaseTable_ColumnWidthChanged(object sender, DevExpress.XtraGrid.Views.Base.ColumnEventArgs e)
        {
            BaseTableTemplateWillBeSaved = true;
        }

        #region Comment
        /*
         * Here simply whenever our ListForm closes that it will invoke the BaseListFrom_Closing and run SaveTemplate and all settings will be set inside the XML file 
         */
        #endregion
        private void BaseListForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SaveTemplate();
        }

        #region Comment
        /*
         * Here simply whenever our ListForm loads that it will invoke the BaseListFrom_Load and run LoadTemplate and all settings will be getting from the XML file 
         */
        #endregion
        private void BaseListForm_Load(object sender, EventArgs e)
        {
            LoadTemplate();
        }

        #region Comment
        /*
         * Here we will be declaring our right click on Mouse in Table(GridView) so then our PopupMenu will be appering there we could do New,Delete,Select,Refresh and Edit it will be the same as with our BarItemButton
         */
        #endregion
        private void BaseTable_MouseUp(object sender, MouseEventArgs e)
        {
            e.ShowRightClickPopupMenu(BasePopupRightMenu);
        }

        private void BaseListForm_Shown(object sender, EventArgs e)
        {
            BaseTable.Focus();
            HideShowButtons();
            HideShowColumn();

            #region Comment
            /*
             * Here will be focusing on our Table (GridView) after we insert or update on EditForms but our Table shouldn't be MdiChild and BaseListSelectedId should has value not null,if both or condtion are true then it returns return if not the it will be focused on our Table Row then we will run our extension method it will take our Id value on Id field will search for which Row was then it will be focused on it
             */
            #endregion
            if (IsMdiChild || !BaseListSelectedId.HasValue) return;
            BaseTable.RowFocus("Id", BaseListSelectedId);
        }

        private void HideShowColumn()
        {

        }

        #region Comment
        /*
         * Here first one that if our BaseShowActivePassiveListButton is true and our form is MdiChild then then don't show us btnSelect on ribbonControl if it is false then show us
         * Here second one is that if our Form is MdiChild then don't show us barSelect
         * Here third one is that if our Form is MdiChild then don't show us barSelectDescription
         * Here third one is that if our Form is not MdiChild then don't show and if BaseShowActivePassiveListButton is true then show us if not both of them then show us our btnActivePassiveList
         */
        #endregion
        private void HideShowButtons()
        {
            btnSelect.Visibility = BaseShowActivePassiveListButton ? BarItemVisibility.Never : IsMdiChild ? BarItemVisibility.Never : BarItemVisibility.Always;
            barSelect.Visibility = IsMdiChild ? BarItemVisibility.Never : BarItemVisibility.Always;
            barSelectDescription.Visibility = IsMdiChild ? BarItemVisibility.Never : BarItemVisibility.Always;
            btnActivePassiveList.Visibility = BaseShowActivePassiveListButton ? BarItemVisibility.Always : !IsMdiChild ? BarItemVisibility.Never : BarItemVisibility.Always;
            #region Comment
            /*
             * Here we say that if our not null then go and look for BarItem and set their Visibility as true
             */
            #endregion
            ShowItems?.ForEach(x => x.Visibility = BarItemVisibility.Always);
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
            ShowEditFormsDefault(result);
        }

        #region Comment
        /*
         * This is after we insert any value or update on EditForms then it in ShowEditForms it will return long id then we pass it to our ShowEditFormsDefault then if it it null then it returns if not it set our BaseShowActivePassiveList into true then it check our EntityActivePassiveListCaption(); in the end it focuses on our Table (GridView)
         */
        #endregion
        protected void ShowEditFormsDefault(long id)
        {
            if (id <= 0) return;
            BaseShowActivePassiveList = true;
            EntityActivePassiveListCaption();
            BaseTable.RowFocus("Id", id);
        }

        #region Comment
        /*
         * Here we have EntityDelete method that it will simply delete our entities still we might override it maybe in other forms we could use it not from the BaseForms
         * var entity = BaseTable.GetRow<BaseEntity>(); here we get our entity but clicking on Tab
         */
        #endregion
        protected virtual void EntityDelete()
        {
            var entity = BaseTable.GetRow<BaseEntity>();
            if (entity == null) return;

            if (!((IBaseCommonBll)BaseBll).Delete(entity)) return;
            BaseTable.DeleteSelectedRows();
            BaseTable.RowFocus(BaseTable.FocusedRowHandle);

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

        #region Comment
        /*
         * Here we have created method named EntityConnectedBarButtonItem we will override it in CityListForm then there we will get our Row then we will open DistrictListForm with parameters CityId and CityName
         */
        #endregion
        protected virtual void EntityConnectedBarButtonItem() { }


        #region Comment
        /*
         * Here we have method named EntityActivePassiveListCaption() so it will change our ActivePassiveList caption
         * Here btnActivePassiveList==null means that we in some form we will not be using AcctivePassiveList so if it is null then run our Refresh method and return
         */
        #endregion
        private void EntityActivePassiveListCaption()
        {
            if (btnActivePassiveList == null)
            {
                EntityRefresh();
                return;
            }

            if (BaseShowActivePassiveList)
            {
                btnActivePassiveList.Caption = "Passive List";
                BaseTable.ViewCaption = Text;
            }
            else
            {
                btnActivePassiveList.Caption = "Active List";
                BaseTable.ViewCaption = Text + " - Passive List";
            }
            EntityRefresh();
        }

        #region Comment
        /*
         * Here simply we have created a method that if our BaseFormTemplateWillBeSave true then we will save our Form size and location and WindowState
         * we will be using this whenever our Form is closing event but beside this one we will be declaring two events named LocationChanged and SizeChanged events there we will set our BaseFormTemplateWillBeSave as true or false
         * Second condition state that if our Table is MdiChild then we save named like this FormName+Table if it is MDI form Then FormName+TableMDI
         */
        #endregion
        private void SaveTemplate()
        {
            if (BaseFormTemplateWillBeSaved)
                Name.SaveFormTemplate(Left, Top, Width, Height, WindowState);

            if (BaseTableTemplateWillBeSaved)
                BaseTable.SaveTableTemplate(IsMdiChild ? Name + " Table" : Name + " TableMDI");
        }

        #region Comment
        /*
         * Whenever our Form is MdiChild then we only need Table(GridView) settings since all MdiChild is the same size and fixed inside MDI parents we only need settings from our Table
         * Second one is if it is MDI form then for Table get FormTemplate plus TableTemplate
         */
        #endregion
        private void LoadTemplate()
        {
            if (IsMdiChild)
            {
                BaseTable.LoadTableTemplate(Name + " Table");
            }
            else
            {
                Name.LoadFormTemplate(this);
                BaseTable.LoadTableTemplate(Name + " TableMDI");
            }
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
                BaseTable.ExportTableData(FileType.ExcelStandard,e.Item.Caption,Text);
            }
            else if (e.Item == btnExcelFileFormatted)
            {
                BaseTable.ExportTableData(FileType.ExcelFormatted,e.Item.Caption,Text);
            }
            else if (e.Item == btnExcelFileUnformatted)
            {
                BaseTable.ExportTableData(FileType.ExcelUnFormatted,e.Item.Caption,Text);
            }
            else if (e.Item == btnPdfFile)
            {
                BaseTable.ExportTableData(FileType.PdfFile,e.Item.Caption);
            }
            else if (e.Item == btnWordFile)
            {
                BaseTable.ExportTableData(FileType.WordFile,e.Item.Caption);
            }
            else if (e.Item == btnTxtFile)
            {
                BaseTable.ExportTableData(FileType.TxtFile,e.Item.Caption);
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
            else if (e.Item == btnDelete)
            {
                EntityDelete();
            }
            else if (e.Item == btnSelect)
            {
                EntitySelect();
            }
            else if (e.Item == btnRefresh)
            {
                EntityRefresh();
            }
            else if (e.Item == btnFilter)
            {
                EntityFilter();
            }
            else if (e.Item == btnColumns)
            {
                #region Comment
                /*
                 * Here Whenever we click on our Columns button it will show down right side CustomizationForm we could move or move back our columns to our GridView
                 */
                #endregion
                if (BaseTable.CustomizationForm == null)
                {
                    BaseTable.ShowCustomization();
                }
                else
                {
                    BaseTable.HideCustomization();
                }
            }
            else if (e.Item == btnConnectedBarButtonItem)
            {
                EntityConnectedBarButtonItem();

            }
            else if (e.Item == btnPrint)
            {
                EntityPrint();
            }
            else if (e.Item == btnExit)
            {
                Close();
            }
            else if (e.Item == btnActivePassiveList)
            {
                BaseShowActivePassiveList = !BaseShowActivePassiveList;
                EntityActivePassiveListCaption();
            }

            Cursor.Current = DefaultCursor;

        }


    }
}