using Business.Interfaces;
using Common.Enums;
using Common.Message;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using Model.Entities.Base;
using StudentManagementUI.Functions;
using StudentManagementUI.UserControls.Controls;
using System;
using System.Windows.Forms;

namespace StudentManagementUI.Forms.BaseForms
{
    public partial class BaseEditForm : RibbonForm
    {
        #region Comment
        /*
         * Here we have declared BaseProccessType enum in ShowEditForms when a user wants to insert entity or updated entity we will identify from here and in ShowEditForms if id =-1 then it will be set EntityInsert if id is different then -1 then it will be EntityUpdate will be set here 
         * Here we have Id value when we open our EditForms from ListForms it bring id and set it in EditForms in order to make updates
         * BaseWillRefresh is going to tell us if we did some changes on EditForms and we want refresh in ListForms so we will simply put true or false here
         * MyDataLayoutControl BaseDataLayoutControl; Here in order to use events on EditForms we have send dataLayoutControl to BaseEditForm since all controls are inside dataLayoutControl
         * IBaseBll BaseBll; here we will make update,delete and insert
         * Here we have BaseEntity OldEntity; and BaseEntity CurrentEntity; on our EditForms if we try to update an entity first on EditForm first it will get our OldEntity in our EditForms then if we try to click the save then it will save current entity into CurrentEntity then it will compare both of them if there are any changes then it will save and it returns id into ListForm and it will be focused on its Row 
         * BaseIsLoaded is that whenever we open our EditForms that if we open and loads the Form is true if not then it is false
         * MyDataLayoutControl[] BaseDataLayoutControls; Here actually main thing we should send our events from DataLayoutControl to our BaseEditForm so sometimes our Form can contain multiply DataLayoutControls so that's why we have declared as array also
         * protected bool FormTemplateWillBeSaved; Here we simply know that will be saving our Form Template or not
         */
        #endregion
        protected internal ProccessType BaseProccessType;
        protected bool BaseFormTemplateWillBeSaved;
        protected internal long BaseEditId;
        protected internal bool BaseWillRefresh;
        protected internal MyDataLayoutControl BaseDataLayoutControl;
        protected internal MyDataLayoutControl[] BaseDataLayoutControls;
        protected IBaseBll BaseBll;
        protected FormType BaseFormType;
        protected BaseEntity BaseOldEntity;
        protected BaseEntity BaseCurrentEntity;
        protected bool BaseIsLoaded;
        protected bool CloseFormAfterInsert = true;

        public BaseEditForm()
        {
            InitializeComponent();
        }

        protected internal void EventsLoad()
        {
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

            //FormEvents
            Load += BaseEditForm_Load;
            FormClosing += BaseEditForm_FormClosing;

            #region Comment
            /*
             * Here whenever we change our Formsize or location of the Form these events will invoke and we will set our BaseFormTemplateWillBeSaved to true
             */
            #endregion
            LocationChanged += BaseEditForm_LocationChanged;
            SizeChanged += BaseEditForm_SizeChanged;


            #region Comment
            /*
             * Here we have created a method called ControlEvents that it will simply run our own Events on EditForms such as KeyDown here control.KeyDown += Control_KeyDown; in our EditForm which componant we press that KeyDown event will work and KeyDown is only one press on the componant
             */
            #endregion
            void ControlEvents(Control control)
            {
                control.KeyDown += Control_KeyDown;
                switch (control)
                {
                    case MyButtonEdit edit:
                        edit.IdChanged += Control_IdChanged;
                        edit.EnableChange += Control_EnableChange;
                        edit.DoubleClick += Control_DoubleClick;
                        edit.ButtonClick += Control_ButtonClick;
                        break;
                    case BaseEdit edit:
                        edit.EditValueChanged += Control_EditValueChanged;
                        break;
                }
            }

            #region Comment
            /*
             * Here we will be sending our controls from DataLayoutControl to here we will catch their events since we have two DataLayoutControl one is array and another is only one (It will be used for Single DataLayoutControl in EditForms)
             */
            #endregion
            if (BaseDataLayoutControls == null)
            {
                if (BaseDataLayoutControl == null) return;
                foreach (Control ctrl in BaseDataLayoutControl.Controls)
                    ControlEvents(ctrl);
            }
            else
            {
                foreach (var layout in BaseDataLayoutControls)
                    foreach (Control ctrl in layout.Controls)
                        ControlEvents(ctrl);
            }


        }

        #region Comment
        /*
         * Here whenever we change the size our Form that we will be setting BaseFormTemplateWillBeSaved to true in order to save it in Template
         */
        #endregion
        private void BaseEditForm_SizeChanged(object sender, EventArgs e)
        {
            BaseFormTemplateWillBeSaved = true;
        }

        #region Comment
        /*
         * Here whenever we change the location of our Form that we will be setting BaseFormTemplateWillBeSaved to true in order to save it in Template
         */
        #endregion
        private void BaseEditForm_LocationChanged(object sender, EventArgs e)
        {
            BaseFormTemplateWillBeSaved = true;
        }

        #region Comment
        /*
         * Here whenever we close our Form that event will function and this one  if (btnSave.Visibility == BarItemVisibility.Never || !btnSave.Enabled) return; we will be using from another brunch to other 
         * SaveTemplate(); is that whenever we open our Forms that which size will be what was the latest size and location of it so will be creating XML file and save informations inside it
         */
        #endregion
        private void BaseEditForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveTemplate();
            if (btnSave.Visibility == BarItemVisibility.Never || !btnSave.Enabled) return;

            if (!EntitySave(true))
                e.Cancel = true;
        }        

        #region Comment
        /*
         * Here we have made our Event as virtual because we will be using it from some EditForms who are connected with each other such City and District when we have no value on City that District should be automaticlly disabled and we will be using it as override in other EditForms
         */
        #endregion
        protected virtual void Control_EnableChange(object sender, EventArgs e) { }
        #region Comment
        /*
         * Here whenever we changed any value on our EditForm that CreatedUpdatedEntity method will be running and there it will compare each other BaseOldEntity and BaseCurrentEntity there will enable or disable the our BarButtomItems
         */
        #endregion
        private void Control_EditValueChanged(object sender, EventArgs e)
        {
            if (!BaseIsLoaded) return;
            CreateUpdatedEntity();
        }
        #region Comment
        /*
         * Here whenever our ButtonEdit event is button click then it will invoke DoSelection method with sender parameters then we will be getting Id and Value on our EditForms
         */
        #endregion
        private void Control_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            DoSelection(sender);
        }
        #region Comment
        /*
         * Here whenever our ButtonEdit event is double click then it will invoke DoSelection method with sender parameters then we will be getting Id and Value on our EditForms
         */
        #endregion
        private void Control_DoubleClick(object sender, EventArgs e)
        {
            DoSelection(sender);
        }

        private void Control_IdChanged(object sender, IdChangedEventArgs e)
        {
            if (!BaseIsLoaded) return;
            CreateUpdatedEntity();
        }

        #region Comment
        /*
         * Here whenever we press any key while we are on our controls in EditForms if we pressed on txtSchoolName.Text whenever we pressed Esc key then it will close our SchoolEditForm
         */
        #endregion
        private void Control_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
            if (sender is MyButtonEdit buttonEdit)
            {
                #region Comment
                /*
                 * Here whenever we press any key while we are on our controls in EditForms if we pressed on btnCityName EditButton control whenever we pressed Ctrl + Shift + Delete key then it will empty Id and EditValue in our btnCityName our SchoolEditForm
                 */
                #endregion
                switch (e.KeyCode)
                {
                    case Keys.Delete when e.Control && e.Shift:
                        buttonEdit.Id = null;
                        buttonEdit.EditValue = null;
                        break;
                    case Keys.Down when e.Modifiers == Keys.Alt:
                        DoSelection(buttonEdit);
                        break;
                }
            }
        }

        #region Comment
        /*
         * Here is our First Form will be run then it sets our BaseIsLoad as true
         * Second it calls our CreateUpdatedEntity(); method
         * Third is it will Load our settings when we last time we closed the Form but in order to know we made some changes or not we have to declare two events in our BaseEditForm
         * Forth one is that it will create and Id for our Entity
         */
        #endregion
        private void BaseEditForm_Load(object sender, EventArgs e)
        {
            BaseIsLoaded = true;
            CreateUpdatedEntity();
            LoadTemplate();
            BaseEditId = BaseProccessType.CreateId(BaseOldEntity);
        }

        #region Comment
        /*
         * Here our DoSelection will be overrode in our EditForms so there we will be catching from ListForms to our ButtonEdits Id and Values so during the selection but we have to create the class in Functions here we will do nothing just leave there 
         */
        #endregion
        protected virtual void DoSelection(object sender) { }
        #region Comment
        /*
         * Here is our virtual method simply will be override in other EditForms
         */
        #endregion
        protected internal virtual void MyBaseEditLoads() { }
        #region Comment
        /*
         * Here we have created method call BindEntityToControl so it will empty here we will override in BaseEditForms so it will go get our OldEntity according to its Id then we will fill our textbox,ButtonEdit and ToggleSwitch there
         */
        #endregion
        protected virtual void BindEntityToControls() { }
        #region Comment
        /*
         * Here we have created CreateUpdatedEntity and inside it we will put our CurrentEntity in EditForms
         */
        #endregion
        protected virtual void CreateUpdatedEntity() { }
        #region Comment
        /*
         * Here we have created ButtonsEnableState method so it simply will enables or disables our buttons in our EditForms when we make changes on our EditForms
         * Here if it is not IsLoaded means that if still we didn't open our EditForms or loaded then return nothing if it is IsLoaded then we create some functions that it will enable or disables our Buttons in EditForms
         */
        #endregion
        protected virtual void ButtonEnabledState()
        {
            if (!BaseIsLoaded)
            {
                return;
            }
            else
            {
                GeneralFunctions.ButtonEnabledState<BaseEntity>(btnNew, btnSave, btnUndo, btnDelete, BaseOldEntity, BaseCurrentEntity);
            }

        }

        private void Button_ItemClick(object sender, ItemClickEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            if (e.Item == btnNew)
            {
                BaseProccessType = ProccessType.EntityInsert;
                MyBaseEditLoads();
            }
            else if (e.Item == btnSave)
            {
                EntitySave(false);
            }
            else if (e.Item == btnUndo)
            {
                EntityUndo();
            }
            else if (e.Item == btnDelete)
            {
                EntityDelete();
            }
            else if (e.Item == btnExit)
            {
                Close();
            }

            Cursor.Current = DefaultCursor;
        }

        #region Comment
        /*
         * Here we have EntityDelete method that it will simply delete entity from the EditForms
         * BaseWillRefresh = true; it will simply refresh our ListForm cause if refresh is true then it returns Id then from ShowEdit methods will simply refresh our ListForms then in the end it closes our EditForms
         */
        #endregion
        private void EntityDelete()
        {
            if (!((IBaseCommonBll)BaseBll).Delete(BaseOldEntity)) return;
            BaseWillRefresh = true;
            Close();
        }

        private bool EntitySave(bool editFormClosing)
        {
            bool SaveProccess()
            {
                Cursor.Current = Cursors.WaitCursor;
                switch (BaseProccessType)
                {
                    case ProccessType.EntityInsert:
                        if (EntityInsert())
                            return ProccessAfterInsert();
                        break;
                    case ProccessType.EntityUpdate:
                        if (EntityUpdate())
                            return ProccessAfterInsert();
                        break;
                }

                bool ProccessAfterInsert()
                {
                    BaseOldEntity = BaseCurrentEntity;
                    BaseWillRefresh = true;
                    ButtonEnabledState();

                    if (CloseFormAfterInsert)
                        Close();
                    else
                        BaseProccessType = BaseProccessType == ProccessType.EntityInsert ? ProccessType.EntityUpdate : BaseProccessType;
                    return true;
                }

                return false;
            }
            var result = editFormClosing ? Messages.EditFormClosingMessage() : Messages.SaveMessage();

            switch (result)
            {
                case DialogResult.Yes:
                    return SaveProccess();
                case DialogResult.No:
                    if (editFormClosing)
                        btnSave.Enabled = true;
                    return true;
                case DialogResult.Cancel:
                    return false;
            }

            return false;
        }

        protected virtual bool EntityUpdate()
        {
            return ((IBaseGeneralBll)BaseBll).Update(BaseOldEntity, BaseCurrentEntity);
        }

        protected virtual bool EntityInsert()
        {
            return ((IBaseGeneralBll)BaseBll).Insert(BaseCurrentEntity);
        }

        #region Comment
        /*
         * Here in our EditForms Undo will simply whenever we open our EditForm as update it will be undoing the same
         */
        #endregion
        private void EntityUndo()
        {
            if (Messages.UndoMessage() != DialogResult.Yes) return;
            if (BaseProccessType == ProccessType.EntityUpdate)
                MyBaseEditLoads();
            else
                Close();
        }

        #region Comment
        /*
         * Here simply we have created a method that if our BaseFormTemplateWillBeSave true then we will save our Form size and location and WindowState
         * we will be using this whenever our Form is closing event but beside this one we will be declaring two events named LocationChanged and SizeChanged events there we will set our BaseFormTemplateWillBeSave as true or false
         */
        #endregion
        protected void SaveTemplate()
        {
            #region Comment
            /*
             * Here Name is that our Form Name and SaveFormTemplate is our extension method since Name is string method and when we created extension method first thing that was this string
             */
            #endregion
            if (BaseFormTemplateWillBeSaved)
                Name.SaveFormTemplate(Left, Top, Width, Height, WindowState);           
        }

        #region Comment
        /*
         * Here we have created method named LoadTemplate it will simply run our extension method and load our settings and LoadFormTemplate and for a form we assign this means that our current Form we will be using it when first our Form is Loaded
         */
        #endregion
        private void LoadTemplate()
        {
            Name.LoadFormTemplate(this);
        }

    }
}