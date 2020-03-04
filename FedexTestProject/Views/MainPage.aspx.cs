using System;
using System.Collections.Generic;
using System.IO;
using System.Web.UI;
using FedexTestProject.Core;
using FedexTestProject.Core.BusinessModels;

namespace FedexTestProject.Web.Views
{
    public partial class MainPage : Page
    {
        public ITrackingManager TrackingManager { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "Fedex Test App";
        }

        protected void UploadButton_Click(object sender, EventArgs e)
        {
            if (!FileUploadControl.HasFile) return;
            try
            {
                var trackingNumbers = new List<string>();
                using (var file = new StreamReader(FileUploadControl.FileContent))
                {
                    string line;
                    while ((line = file.ReadLine()) != null)
                    {
                        trackingNumbers.Add(line);
                    }
                }

                List<TrackingPackage> trackingPackages = null;
                ;
                RegisterAsyncTask(new PageAsyncTask(async () =>
                {
                    trackingPackages = await TrackingManager.GetTrackingInfo(trackingNumbers);
                    TrackingInfoGrid.DataSource = trackingPackages;
                    TrackingInfoGrid.DataBind();
                    MainPanel.Visible = false;
                }));
            }
            catch (Exception ex)
            {
                //show error
            }
        }
    }
}