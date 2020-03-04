namespace FedexTestProject.Core.BusinessModels
{
    public class TrackingPackage
    {
        public string TrackNumber { get; set; }

        public string TerminalAddress { get; set; }

        public string TrackingStatus { get; set; }

        public string OriginAddress { get; set; }

        public string OriginDate { get; set; }

        public string DeliveryAddress { get; set; }

        public string DeliveryDate { get; set; }
    }
}