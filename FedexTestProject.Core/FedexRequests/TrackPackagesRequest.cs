using System.Collections.Generic;
using Newtonsoft.Json;

namespace FedexTestProject.Core.FedexRequests
{
    public class TrackPackagesRequest
    {
        public TrackPackagesRequest(IEnumerable<string> trackingNumbers)
        {
            AppType = "wtrk";
            SupportHtml = true;
            SupportCurrentLocation = true;
            UniqueKey = string.Empty;
            ProcessingParameters = new ProcessingParameters();
            TrackingInfoList = new List<TrackingInfoList>();
            foreach (var trackingNumber in trackingNumbers)
            {
                TrackingInfoList.Add(new TrackingInfoList(trackingNumber));
            }
        }

        [JsonProperty("appType")]
        public string AppType { get; set; }

        [JsonProperty("supportHTML")]
        public bool SupportHtml { get; set; }

        [JsonProperty("supportCurrentLocation")]
        public bool SupportCurrentLocation { get; set; }

        [JsonProperty("uniqueKey")]
        public string UniqueKey { get; set; }

        [JsonProperty("processingParameters")]
        public ProcessingParameters ProcessingParameters { get; set; }

        [JsonProperty("trackingInfoList")]
        public IList<TrackingInfoList> TrackingInfoList { get; set; }
    }
}