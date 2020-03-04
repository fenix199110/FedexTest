using AutoMapper;
using FedexTestProject.Core.BusinessModels;
using FedexTestProject.Core.FedexResponse;

namespace FedexTestProject.Web.Mappings
{
    public class TrackingProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Package, TrackingPackage>()
                .ForMember(dest => dest.TrackNumber, opt => opt.MapFrom(src => src.TrackNumber))
                .ForMember(dest => dest.TerminalAddress, opt => opt.MapFrom(src => $"{src.OriginTermCity} {src.OriginTermState}"))
                .ForMember(dest => dest.TrackingStatus, opt => opt.MapFrom(src => src.TrackingStatus))
                .ForMember(dest => dest.OriginAddress, opt => opt.MapFrom(src => $"{src.RecipientCity} {src.RecipientState}"))
                .ForMember(dest => dest.OriginDate, opt => opt.MapFrom(src => src.OriginDate))
                .ForMember(dest => dest.DeliveryAddress, opt => opt.MapFrom(src => $"{src.ShipperCity} {src.ShipperState}"))
                .ForMember(dest => dest.DeliveryDate, opt => opt.MapFrom(src => src.DeliveryDate))
                .ForAllOtherMembers(x => x.Ignore());
        }
    }
}