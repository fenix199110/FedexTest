using Newtonsoft.Json;

namespace FedexTestProject.Core.FedexRequests
{
    public class TrackingInfoList
    {
        public TrackingInfoList(string trackingNumber)
        {
            TrackNumberInfo = new TrackNumberInfo(trackingNumber);
        }
        [JsonProperty("trackNumberInfo")]
        public TrackNumberInfo TrackNumberInfo { get; set; }
    }
}