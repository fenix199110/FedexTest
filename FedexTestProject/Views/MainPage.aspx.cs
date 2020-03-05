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
            TrackingInfoPanel.Visible = false;
            TrackNumberPanel.Visible = false;
        }

        protected void UploadButton_Click(object sender, EventArgs e)
        {
            if (!FileUploadControl.HasFile)
            {
                LockPanel.Visible = false;
                return;
            }
            try
            {
                var extension = Path.GetExtension(FileUploadControl.FileName);
                var fileContent = FileUploadControl.FileContent;
                var trackingNumbers = extension == ".txt"
                    ? TextFileProcessor.GetLines(fileContent)
                    : XlsFileProcessor.GetLines(fileContent);

                MainPanel.Visible = false;
                TrackNumberPanel.Visible = true;

                TrackingNumbersList.DataSource = Session["TrackingNumbersDataSource"] = trackingNumbers;
                TrackingNumbersList.DataBind();
            }
            catch (Exception ex)
            {
                ErrorLbl.Text = ex.Message;
            }
        }


        protected void ProcessTrackingNumbersButton_Click(object sender, EventArgs e)
        {
            try
            {
                RegisterAsyncTask(new PageAsyncTask(async () =>
                {
                    var trackingNumbers = (List<string>)Session["TrackingNumbersDataSource"];
                    var trackingPackages = await TrackingManager.GetTrackingInfo(trackingNumbers);
                    TrackingInfoGrid.DataSource = Session["TrackingInfoDataSource"] = trackingPackages;
                    TrackingInfoGrid.DataBind();
                    MainPanel.Visible = false;
                    TrackingInfoPanel.Visible = true;
                }));
            }
            catch (Exception ex)
            {
                ErrorLbl.Text = ex.Message;
            }
        }

        protected void ExportXlsxButton_Click(object sender, EventArgs e)
        {
            var items = (IList<TrackingPackage>) Session["TrackingInfoDataSource"];
            try
            {
                var excelFile = XlsFileProcessor.BuildExcelFile(items.ToDataTable());
                using (var exportData = new MemoryStream())
                {
                    excelFile.Write(exportData);
                    Response.Clear();
                    Response.BinaryWrite(exportData.ToArray());
                }
            }
            catch (Exception ex)
            {
                ErrorLbl.Text = ex.Message;
                return;
            }

            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("Content-Disposition", "attachment;filename=Track.xlsx");

            Response.Flush();
            Response.End();
        }

        protected void ExportTxtButton_Click(object sender, EventArgs e)
        {
            try
            {
                var items = (IList<TrackingPackage>)Session["TrackingInfoDataSource"];
                var result = TextFileProcessor.BuildTxtFile(items.ToDataTable());
                Response.Clear();
                Response.Output.Write(result);
            }
            catch (Exception ex)
            {
                ErrorLbl.Text = ex.Message;
                return;
            }

            Response.AddHeader("content-disposition", "attachment;filename=Track.txt");
            Response.ContentType = "application/text";
            Response.Flush();
            Response.End();
        }
    }
}