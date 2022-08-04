using DevExpress.Utils;
using DevExpress.XtraEditors;
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
    public class MyToggleSwitch:ToggleSwitch,IStatusBarDescription
    {
        public MyToggleSwitch()
        {
            Name = "tglName";
            Properties.OffText = "Passive";
            Properties.OnText = "Active";
            Properties.AutoHeight = false;
            Properties.AutoWidth = true;
            Properties.GlyphAlignment = HorzAlignment.Far;
            Properties.Appearance.ForeColor = Color.Maroon;

        }

        public override bool EnterMoveNextControl { get; set; } = true;
        public string StatusBarDescription { get; set; } = "Choose the state of entity";
    }
}
