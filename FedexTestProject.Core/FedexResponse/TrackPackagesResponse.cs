using System.Collections.Generic;
using Newtonsoft.Json;

namespace FedexTestProject.Core.FedexResponse
{
    public class TrackPackagesResponse
    {
        [JsonProperty("packageList")]
        public IList<Package> Packages { get; set; }
    }
}