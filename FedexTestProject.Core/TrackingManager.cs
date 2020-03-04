using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using FedexTestProject.Core.BusinessModels;
using FedexTestProject.Core.FedexRequests;
using FedexTestProject.Core.FedexResponse;
using Newtonsoft.Json;

namespace FedexTestProject.Core
{
    public class TrackingManager : ITrackingManager
    {
        public IMapper Mapper { get; set; }

        private const string TrackingUrl = "https://www.fedex.com/trackingCal/track";

        public async Task<List<TrackingPackage>> GetTrackingInfo(IEnumerable<string> trackingNumbers)
        {
            var jsonData = JsonConvert.SerializeObject(new FedexTrackingRequest(trackingNumbers));

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, TrackingUrl);
            var keyValues = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("data", jsonData),
                new KeyValuePair<string, string>("action", "trackpackages")
            };

            request.Content = new FormUrlEncodedContent(keyValues);

            var httpResponse = await client.SendAsync(request);
            var stringResult = await httpResponse.Content.ReadAsStringAsync();
            var fedexTrackingResponse = JsonConvert.DeserializeObject<FedexTrackingResponse>(stringResult);

            return Mapper.Map<List<TrackingPackage>>(fedexTrackingResponse.TrackPackagesResponse.Packages);
        }
    }
}