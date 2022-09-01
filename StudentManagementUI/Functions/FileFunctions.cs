using Common.Message;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using Common.Enums;
using DevExpress.Export;
using DevExpress.XtraPrinting;

namespace StudentManagementUI.Functions
{
    #region Comment
    /*
     * Here we will be creating our templates whenever we open our EditForms we could resize and change the location when we do that it will save the left,top,width,height,template name and windows state of it and also in our Table(GridView) we will be doing the same thing for our columns of width and so on and beside these we will also set our ListForms not mdichild its locations and size of it so user do some settings then when it open it that it will be the same with latest settings for this we have to create extesion method
     */
    #endregion
    public static class FileFunctions
    {
        #region Comment
        /*
         * Here we have created an extention method named FormTemplateSave we will be saving inside the Xml file our latest form settings templateName,left,top,width,height,formWindowsSate
         */
        #endregion
        public static void SaveFormTemplate(this string templateName, int left, int top, int width, int height, FormWindowState formWindowState)
        {
            #region Comment
            /*
             * Here we have created try catch in case we get the error so we will check a directory named Save Template not exists then create a new one if we have then skip it
             */
            #endregion
            try
            {
                if (!Directory.Exists(Application.StartupPath + @"\Save Template"))
                    Directory.CreateDirectory(Application.StartupPath + @"\Save Template");
                #region Comment
                /*
                 * Here we have created settings we will set our xml file Indent as true because we will save more then one line that's why it should be indented
                 */
                #endregion
                var settings = new XmlWriterSettings { Indent = true };
                #region Comment
                /*
                 * Here we have created our xml file with templateName_location.xml and with indented settings in our application Directory
                 */
                #endregion
                var writer = XmlWriter.Create(Application.StartupPath + @"\Save Template\" + templateName + "_location.xml", settings);
                #region Comment
                /*
                 * Here we have we say that we will write inside our xml file
                 */
                #endregion
                writer.WriteStartDocument();
                #region Comment
                /*
                 * Here we have we say that we will write comment name as our Application name as comment we don't have obligation to write it
                 */
                #endregion
                writer.WriteComment("Created by SOLO Student Management Application");
                #region Comment
                /*
                 * Here we say that our main Element name is Table
                 */
                #endregion
                writer.WriteStartElement("Table");
                #region Comment
                /*
                 * Here we say that our second element under the Table will be Location
                 */
                #endregion
                writer.WriteStartElement("Location");
                #region Comment
                /*
                 * Here we say that our Left location where it will be started inside xml attribute
                 */
                #endregion
                writer.WriteAttributeString("Left", left.ToString());
                #region Comment
                /*
                 * Here we say that our Top location where it will be started inside xml attribute
                 */
                #endregion
                writer.WriteAttributeString("Top", top.ToString());
                #region Comment
                /*
                 * Here we say that we wil be closing our Location Element
                 */
                #endregion
                writer.WriteEndElement();


                #region Comment
                /*
                 * Here we have created a new Element named FormSize if our formsize is Maximized then we set in our attribute Width and Height as -1 because if a user latest maximized our Form when we close it will be saved in our Xml file then when we try to open it again that it will be opened in the same way the reason we set it as -1 because each user has different monitor some size is big some size is small that's why we cannot know that we set it as -1 its width and height if not then it will set out Width and Height in our Xml File as attribute and lastly we close our FormSize element as writer.WriteEndElement();
                 */
                #endregion
                writer.WriteStartElement("FormSize");
                if (formWindowState == FormWindowState.Maximized)
                {
                    writer.WriteAttributeString("Width", "-1");
                    writer.WriteAttributeString("Height", "-1");
                }
                else
                {
                    writer.WriteAttributeString("Width", width.ToString());
                    writer.WriteAttributeString("Height", height.ToString());
                }
                #region Comment
                /*
                 * Here first one we have closed our FormSize Element
                 * Here second one we have closed our Table Element
                 * Here third one we have closed our Xml document
                 * Here forth one we have save our data in our XML document
                 * Here fifth one we have closed everything compeletly
                 */
                #endregion
                writer.WriteEndElement();
                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Flush();
                writer.Close();

            }
            catch (Exception exception)
            {
                Messages.ErrorMessage(exception.Message);
            }
        }

        #region Comment
        /*
         * Here we have created method named LoadFormTemplate method it will simply go inside our XML file and read it and get our values from it
         */
        #endregion
        public static void LoadFormTemplate(this string templateName, XtraForm frm)
        {
            #region Comment
            /*
             * Here we have created list as genetic string type we will be saving our attribute from XML file and save inside it
             */
            #endregion
            var list = new List<string>();
            try
            {
                #region Comment
                /*
                 * Here we will be checking if our XML file exist or not if not we create it as a reader
                 */
                #endregion
                if (File.Exists(Application.StartupPath + @"\Save Template\" + templateName + "_location.xml"))
                {
                    var reader = XmlReader.Create(Application.StartupPath + @"\Save Template\" + templateName + "_location.xml");
                    while (reader.Read())
                    {
                        #region Comment
                        /*
                         * Here we will be checking if our XML file exists element and element name is Location then add it inside our list generic string when we will get it that we will get the Width and Height from it
                         */
                        #endregion
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "Location")
                        {
                            list.Add(reader.GetAttribute(0));
                            list.Add(reader.GetAttribute(1));
                        }
                        else if (reader.NodeType == XmlNodeType.Element && reader.Name == "FormSize")
                        {
                            list.Add(reader.GetAttribute(0));
                            list.Add(reader.GetAttribute(1));
                        }

                    }

                    reader.Close();
                    reader.Dispose();
                }


            }
            catch (Exception exception)
            {
                Messages.ErrorMessage(exception.Message);
            }

            #region Comment
            /*
             * First one if our list is empty that we simply return it 
             * Second one if it is not empty then we assign our Form locations left and top value where our forms location
             * Third one if our Width and Height is -1 means that maximized then we set our WindowState as maximized if it is not then we set out Width and Height values for our Forms
             */
            #endregion
            if (list.Count <= 0) return;

            frm.Location = new Point(int.Parse(list[0]), int.Parse(list[1]));
            if (list[2] == "-1" && list[3] == "-1")
                frm.WindowState = FormWindowState.Maximized;
            else
                frm.Size = new Size(int.Parse(list[2]), int.Parse(list[3]));
        }

        #region Comment
        /*
         * Here we have created SaveTableTemplate we simply save user about Columns in Table(GridView) settings and first one was about the Forms
         */
        #endregion
        public static void SaveTableTemplate(this GridView table, string templateName)
        {
            try
            {
                #region Comment
                /*
                 * First one we will clear our filters first because if we apply any filter that we don't want it to be saved with Filters
                 * Second one we check if our Save Template directory exists or not if not then create a new one
                 * Third one in Devexpress we have already method named SaveLayoutToXml it will save all columns name with their size so we simply want to save inside our templateName file inside Save Template directory
                 */
                #endregion
                table.ClearColumnsFilter();
                if (!Directory.Exists(Application.StartupPath + @"\Save Template"))
                    Directory.CreateDirectory(Application.StartupPath + @"\Save Template");

                table.SaveLayoutToXml(Application.StartupPath + $@"\Save Template\{templateName}.xml");

            }
            catch (Exception exception)
            {
                Messages.ErrorMessage(exception.Message);
            }
        }

        #region Comment
        /*
         * Here we have created a method named LoadTableTemplate it will simply load our Table(GridView) settings like before what we have left
         * First one checks if our templateName file exists in our Save Template directory 
         * Second one is that if it exists that it restores it for us it is Devexpress method easily can be used
         */
        #endregion
        public static void LoadTableTemplate(this GridView table, string templateName)
        {
            try
            {
                if (File.Exists(Application.StartupPath + $@"\Save Template\{templateName}.xml"))
                    table.RestoreLayoutFromXml(Application.StartupPath + $@"\Save Template\{templateName}.xml");

            }
            catch (Exception exception)
            {
                Messages.ErrorMessage(exception.Message);
            }
        }

        #region Comment
        /*
         * Here we have created an extension method that it will simply export our Table data ExcelStandard,ExcelFormatted,ExcelUnformatted,PdfFile,WordFile and TxtFile
         * First one is we will be getting DialogResult whenever user click Yes or No if yes then continue if no then return
         * Second one we check that if our Temp directory doesn't exist then create a new one
         * Third one we have created FileName as unique by using Guid
         * Fourth we have created our File Path
         *
         */
        #endregion
        public static void ExportTableData(this GridView table, FileType fileType, string fileExtension, string excelFileName = null)
        {
            if (Messages.ExportTableDataMessage(fileExtension) != DialogResult.Yes) return;

            if (!Directory.Exists(Application.StartupPath + @"\Temp"))
                Directory.CreateDirectory(Application.StartupPath + @"\Temp");

            var fileName = Guid.NewGuid().ToString();
            var filePath = Application.StartupPath + $@"\Temp\{fileName}";

            switch (fileType)
            {
                case FileType.ExcelStandard:
                    {
                        var options = new XlsxExportOptionsEx
                        {
                            ExportType = ExportType.Default,
                            SheetName = excelFileName,
                            TextExportMode = TextExportMode.Text
                        };

                        filePath = filePath + ".Xlsx";
                        table.ExportToXlsx(filePath, options);
                    }
                    break;
                case FileType.ExcelFormatted:
                    {
                        var options = new XlsxExportOptionsEx
                        {
                            ExportType = ExportType.WYSIWYG,
                            SheetName = excelFileName,
                            TextExportMode = TextExportMode.Text
                        };

                        filePath = filePath + ".Xlsx";
                        table.ExportToXlsx(filePath, options);
                    }
                    break;
                case FileType.ExcelUnFormatted:
                    {
                        var options = new CsvExportOptionsEx
                        {
                            ExportType = ExportType.WYSIWYG,
                            TextExportMode = TextExportMode.Text
                        };
                        filePath = filePath + ".Csv";
                        table.ExportToCsv(filePath, options);
                    }
                    break;
                case FileType.PdfFile:
                    filePath = filePath + ".Pdf";
                    table.ExportToPdf(filePath);
                    break;
                case FileType.WordFile:
                    filePath = filePath + ".Docx";
                    table.ExportToDocx(filePath);
                    break;
                case FileType.TxtFile:
                    {
                        var options = new TextExportOptions
                        {
                            TextExportMode = TextExportMode.Text
                        };
                        filePath = filePath + ".Txt";
                        table.ExportToText(filePath);
                    }
                    break;

            }

            if (!File.Exists(filePath))
            {
                Messages.ErrorMessage("Table data couldn't export");
                return;
            }

            Process.Start(filePath);

        }


    }
}
