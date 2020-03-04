using Newtonsoft.Json;

namespace FedexTestProject.Core.FedexResponse
{
    public class Package
    {
        [JsonProperty("trackingNbr")]
        public string TrackNumber { get; set; }

        [JsonProperty("recipientCity")]
        public string RecipientCity { get; set; }

        [JsonProperty("recipientStateCD")]
        public string RecipientState { get; set; }

        [JsonProperty("originTermCity")] 
        public string OriginTermCity { get; set; }

        [JsonProperty("originTermStateCD")]
        public string OriginTermState { get; set; }

        [JsonProperty("displayShipDt")]
        public string OriginDate { get; set; }

        [JsonProperty("keyStatus")]
        public string TrackingStatus { get; set; }

        [JsonProperty("shipperCity")]
        public string ShipperCity { get; set; }

        [JsonProperty("shipperStateCD")]
        public string ShipperState { get; set; }

        [JsonProperty("displayActDeliveryDt")]
        public string DeliveryDate { get; set; }
    }
}