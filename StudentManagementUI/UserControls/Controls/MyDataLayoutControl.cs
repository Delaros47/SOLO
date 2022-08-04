﻿using DevExpress.XtraDataLayout;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentManagementUI.UserControls.Controls
{
    [ToolboxItem(true)]
    public class MyDataLayoutControl:DataLayoutControl
    {
        public MyDataLayoutControl()
        {
            OptionsFocus.EnableAutoTabOrder = false;
        }

        protected override LayoutControlImplementor CreateILayoutControlImplementorCore()
        {
            return new MyLayoutControlImplementor(this);
        }


    }

    internal class MyLayoutControlImplementor : LayoutControlImplementor
    {
        public MyLayoutControlImplementor(ILayoutControlOwner controlOwner) : base(controlOwner)
        {

        }

        public override BaseLayoutItem CreateLayoutItem(LayoutGroup parent)
        {
            var item = base.CreateLayoutItem(parent);
            item.AppearanceItemCaption.ForeColor = Color.Maroon;
            return item;
        }

        public override LayoutGroup CreateLayoutGroup(LayoutGroup parent)
        {
            var grp = base.CreateLayoutGroup(parent);
            grp.LayoutMode = LayoutMode.Table;

            grp.OptionsTableLayoutGroup.ColumnDefinitions[0].SizeType = SizeType.Absolute;
            grp.OptionsTableLayoutGroup.ColumnDefinitions[0].Width = 230;
            grp.OptionsTableLayoutGroup.ColumnDefinitions[1].SizeType = SizeType.Percent;
            grp.OptionsTableLayoutGroup.ColumnDefinitions[1].Width = 100;
            grp.OptionsTableLayoutGroup.ColumnDefinitions.Add(new ColumnDefinition {Width=110,SizeType=SizeType.Absolute});
            grp.OptionsTableLayoutGroup.RowDefinitions.Clear();

            for (int i = 1; i <=10; i++)
            {

                grp.OptionsTableLayoutGroup.RowDefinitions.Add(new RowDefinition 
                { 
                    SizeType = SizeType.Absolute,
                    Height = 24
                });

                if (i + 1 != 10) continue;
                grp.OptionsTableLayoutGroup.RowDefinitions.Add(new RowDefinition { SizeType=SizeType.Percent,Height=100});
                break;
            }

            return grp;

        }


    }
}
