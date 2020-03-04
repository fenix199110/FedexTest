using System;
using System.Collections.Generic;
using System.IO;
using System.Web.UI;
using FedexTestProject.Core;
using FedexTestProject.Core.BusinessModels;
using FedexTestProject.Core.Extensions;

namespace FedexTestProject.Web.Views
{
    public partial class MainPage : Page
    {
        public TrackingManager TrackingManager { get; set; }
        public TextFileProcessor TextFileProcessor { get; set; }
        public XlsFileProcessor XlsFileProcessor { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "Fedex Test App";
            GridPanel.Visible = false;
        }

        protected void UploadButton_Click(object sender, EventArgs e)
        {
            if (!FileUploadControl.HasFile) return;
            try
            {
                var extension = Path.GetExtension(FileUploadControl.FileName);
                var fileContent = FileUploadControl.FileContent;
                var trackingNumbers = extension == ".txt"
                    ? TextFileProcessor.GetLines(fileContent)
                    : XlsFileProcessor.GetLines(fileContent);

                RegisterAsyncTask(new PageAsyncTask(async () =>
                {
                    var trackingPackages = await TrackingManager.GetTrackingInfo(trackingNumbers);
                    TrackingInfoGrid.DataSource = Session["TrackingInfoDataSource"] = trackingPackages;
                    TrackingInfoGrid.DataBind();
                    MainPanel.Visible = false;
                    GridPanel.Visible = true;
                }));
            }
            catch (Exception ex)
            {
                //show error
            }
        }

        protected void ExportXlsxButton_Click(object sender, EventArgs e)
        {
            var items = (IList<TrackingPackage>) Session["TrackingInfoDataSource"];
            var excelFile = XlsFileProcessor.BuildExcelFile(items.ToDataTable());
            using (var exportData = new MemoryStream())
            {
                excelFile.Write(exportData);
                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("Content-Disposition", "attachment;filename=Track.xlsx");
                Response.BinaryWrite(exportData.ToArray());
            }

            Response.Flush();
            Response.End();
            LockPanel.Visible = false;
        }

        protected void ExportTxtButton_Click(object sender, EventArgs e)
        {
            var items = (IList<TrackingPackage>)Session["TrackingInfoDataSource"];
            var result = TextFileProcessor.BuildTxtFile(items.ToDataTable());

            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=Track.txt");
            Response.ContentType = "application/text";
            Response.Output.Write(result);

            Response.Flush();
            Response.End();
            LockPanel.Visible = false;
        }
    }
}