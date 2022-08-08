﻿using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using StudentManagementUI.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementUI.UserControls.Controls
{
    [ToolboxItem(true)]
    public class MyButtonEdit : ButtonEdit, IStatusBarShortcut
    {
        public MyButtonEdit()
        {
            //Here we made our Button Edit text edit as disable we cannot write anything inside ButtonEdit
            Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
            //Here whenever focus on our ButtonEdit that it changes the color into LightCyan
            Properties.AppearanceFocused.BackColor = Color.LightCyan;

        }
        //Here whenever we enter on our control that it goes to next control or next componant
        public override bool EnterMoveNextControl { get; set; } = true;
        //StatusBars whenever we enter or move or enter the componant on RibbonStatusBar our description will be regarding the componants
        public string StatusBarShortcut { get; set; } = "F4 :";
        public string StatusBarShortcutDescription { get; set; }
        public string StatusBarDescription { get; set; }


        private long? _id;
        //Browsable(false) means that it will not be shown on our Button Edit Properties in order to prevent a programmer to enter a value on it
        [Browsable(false)]
        public long? Id
        {
            get { return _id; }
            set
            {
                #region Comment
                //Here we set our old value always with _id; and new Value current value we assign and in if statement that if they are equal to each other it can be for example in we choose Mardin city then again we second time choose the same city so oldValue and newValue will be equal to each other if they are different then we will call our event and send our oldValue and newValue 
                #endregion
                var oldValue = _id;
                var newValue = value;
                if (oldValue == newValue) return;
                _id = value;
                IdChanged(this, new IdChangedEventArgs(oldValue, newValue));
            }
        }

        #region Comment
        //Here we declare our own event so we use EventHandler generic and it asks ofr EventArgs so we enter our own class and this delegate{}; means it will prevent error whenever we have null value that's why we write it
        #endregion
        public event EventHandler<IdChangedEventArgs> IdChanged = delegate { };


    }

    #region Comment
    //We want to get old value and new value on ButtonEdit whenever we change the value of it and whenever we use EventArgs that means in method e.NewValue and e.OldValue we can reach them out
    #endregion
    public class IdChangedEventArgs : EventArgs
    {
        public IdChangedEventArgs(long? oldValue, long? newValue)
        {
            OldValue = oldValue;
            NewValue = newValue;

        }

        public long? NewValue { get; }
        public long? OldValue { get; }
    }
}
