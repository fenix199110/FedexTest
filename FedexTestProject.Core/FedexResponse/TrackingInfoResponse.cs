using Newtonsoft.Json;

namespace FedexTestProject.Core.FedexResponse
{
    public class TrackingInfoResponse
    {
        [JsonProperty("trackNumberInfo")]
        public string TrackNumber { get; set; }

        [JsonProperty("trackNumberInfo")]
        public string OriginCity { get; set; }

        [JsonProperty("trackNumberInfo")]
        public string OriginTerminal { get; set; }

        [JsonProperty("trackNumberInfo")]
        public string OriginDate{ get; set; }

        [JsonProperty("trackNumberInfo")]
        public string TrackingStatus { get; set; }

        [JsonProperty("trackNumberInfo")]
        public string DestinationCity { get; set; }

        [JsonProperty("trackNumberInfo")]
        public string DeliveryDate { get; set; }
    }
}