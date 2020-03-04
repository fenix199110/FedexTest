using System;
using Newtonsoft.Json;

namespace FedexTestProject.Core.FedexRequests
{
    public class TrackNumberInfo
    {
        public TrackNumberInfo(string trackingNumber)
        {
            TrackingNumber = trackingNumber;
            TrackingQualifier = string.Empty;
            TrackingCarrier = string.Empty;
        }

        [JsonProperty("trackingNumber")]
        public string TrackingNumber { get; set; }

        [JsonProperty("trackingQualifier")]
        public string TrackingQualifier { get; set; }

        [JsonProperty("trackingCarrier")]
        public string TrackingCarrier { get; set; }
    }
}