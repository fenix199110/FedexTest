using Newtonsoft.Json;

namespace FedexTestProject.Core.FedexResponse
{
    public class FedexTrackingResponse
    {
        [JsonProperty("TrackPackagesResponse")]
        public TrackPackagesResponse TrackPackagesResponse { get; set; }
    }
}