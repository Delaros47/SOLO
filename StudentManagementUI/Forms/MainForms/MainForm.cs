using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using StudentManagementUI.Forms.SchoolForms;
using StudentManagementUI.Show;
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
using StudentManagementUI.Forms.CityForms;

namespace StudentManagementUI.Forms.MainForms
{
    public partial class MainForm : RibbonForm
    {
        public MainForm()
        {
            InitializeComponent();
            EventsLoad();
        }


        private void EventsLoad()
        {
            foreach (var item in ribbonControl.Items)
            {
                switch (item)
                {
                    case BarButtonItem button:
                        button.ItemClick += Button_ItemClick;
                        break;
                    default:
                        break;
                }
            }
        }

        private void Button_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.Item==btnSchools)
            {
                ShowListForms<SchoolListForm>.ShowListForm(FormType.School);
            }
            else if (e.Item==btnCities)
            {
                ShowListForms<CityListForm>.ShowListForm(FormType.City);
            }
        }
    }
}