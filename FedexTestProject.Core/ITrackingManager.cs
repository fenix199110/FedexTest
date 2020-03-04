using System.Collections.Generic;
using System.Threading.Tasks;
using FedexTestProject.Core.BusinessModels;

namespace FedexTestProject.Core
{
    public interface ITrackingManager
    {
        Task<List<TrackingPackage>> GetTrackingInfo(IEnumerable<string> trackingNumbers);
    }
}