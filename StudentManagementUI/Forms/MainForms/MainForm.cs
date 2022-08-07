using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using StudentManagementUI.Forms.SchoolForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
                SchoolListForm schoolListForm = new SchoolListForm();
                schoolListForm.MdiParent = ActiveForm;
                schoolListForm.Show();
            }
        }
    }
}