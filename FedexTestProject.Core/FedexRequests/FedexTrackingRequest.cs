using System.Collections.Generic;
using Newtonsoft.Json;

namespace FedexTestProject.Core.FedexRequests
{
    public class FedexTrackingRequest
    {
        public FedexTrackingRequest(IEnumerable<string> trackingNumbers)
        {
            TrackPackagesRequest = new TrackPackagesRequest(trackingNumbers);
        }

        [JsonProperty("TrackPackagesRequest")]
        public TrackPackagesRequest TrackPackagesRequest { get; set; }
    }
}