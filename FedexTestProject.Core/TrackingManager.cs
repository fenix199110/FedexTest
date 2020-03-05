using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using FedexTestProject.Core.BusinessModels;
using FedexTestProject.Core.FedexRequests;
using FedexTestProject.Core.FedexResponse;
using Newtonsoft.Json;

namespace FedexTestProject.Core
{
    public class TrackingManager
    {
        public IMapper Mapper { get; set; }

        private const string TrackingUrl = "https://www.fedex.com/trackingCal/track";
        private const int PageSize = 10; 

        public async Task<List<TrackingPackage>> GetTrackingInfo(IList<string> trackingNumbers)
        {
            var result = new List<TrackingPackage>();
            for (int i = 0; i < trackingNumbers.Count(); i = i + PageSize)
            {
                var items = await GetTrackingInfoChunk(trackingNumbers.Skip(i).Take(PageSize).ToList());
                result.AddRange(items);
            }

            return result.OrderBy(r => r.TrackingStatus).ToList();
        }

        private async Task<List<TrackingPackage>> GetTrackingInfoChunk(IList<string> trackingNumbers)
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