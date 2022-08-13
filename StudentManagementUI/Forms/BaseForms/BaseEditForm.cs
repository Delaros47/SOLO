﻿using Business.Interfaces;
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
         */
        #endregion
        protected internal ProccessType BaseProccessType;
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
         * Here we have made our Event as virtual because we will be using it from some EditForms who are connected with each other such City and District when we have no value on City that District should be automaticlly disabled and we will be using it as override in other EditForms
         */
        #endregion
        protected virtual void Control_EnableChange(object sender, EventArgs e) {  }

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

        private void BaseEditForm_Load(object sender, EventArgs e)
        {
            BaseIsLoaded = true;
            CreateUpdatedEntity();
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
        }

        private void EntityDelete()
        {

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
                    return true;
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

        private void EntityUndo()
        {

        }




    }
}