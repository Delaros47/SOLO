using DevExpress.XtraEditors;
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
    public class MyButtonEdit:ButtonEdit,IStatusBarShortcut
    {
        public MyButtonEdit()
        {
            Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
            Properties.AppearanceFocused.BackColor = Color.LightCyan;
        }

        public override bool EnterMoveNextControl { get; set; } = true;

        public string StatusBarShortcut { get; set; } = "F4 :";
        public string StatusBarShortcutDescription { get; set; }
        public string StatusBarDescription { get; set; }

        private long? _id;
        [Browsable(false)]
        public long? Id
        {
            get 
            { 
                return _id; 
            }
            set 
            {
                var oldValue = _id;
                var newValue = value;
                if (oldValue == newValue) return;
                _id = value;
                IdChanged(this, new IdChangedEventArgs(oldValue, newValue));
            }
        }



        public event EventHandler<IdChangedEventArgs> IdChanged = delegate { };

    }

    public class IdChangedEventArgs : EventArgs
    {
        public long? NewValue { get; }
        public long? OldValue { get; }

        public IdChangedEventArgs(long? oldValue,long? newValue)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }
    }
}
