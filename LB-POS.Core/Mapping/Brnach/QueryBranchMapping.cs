using LB_POS.Core.Features.Brnach.Queries.Result;
using LB_POS.Data.Commons;
using LB_POS.Data.Entities;

namespace LB_POS.Core.Mapping.Brnach
{
    public partial class BrnachProfile
    {
        public void GetBranchListMapping()
        {
            CreateMap<Branch, GetBranchListResponse>()
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(src => src.Localize(src.Name, src.NameEn)))

                .ForMember(dest => dest.FullAddressc,
                    opt => opt.MapFrom(src =>
                        src.Localize(
                            $"{src.Address.Country},({src.Address.Governate}:{src.Address.PostalCode}),{src.Address.RegionCity},{src.Address.BuildingNumber},{src.Address.Floor},{src.Address.Room},{src.Address.BuildingNumber},",
                            $"{src.Address.Country},({src.Address.GovernateEn}:{src.Address.PostalCode}),{src.Address.RegionCityEn},{src.Address.BuildingNumberEn}"
                        )));

            CreateMap<BranchAddress, BranchAddressResponse>()
                .ForMember(dest => dest.Governate,
                    opt => opt.MapFrom(src => src.Localize(src.Governate, src.GovernateEn)))

                .ForMember(dest => dest.RegionCity,
                    opt => opt.MapFrom(src => src.Localize(src.RegionCity, src.RegionCityEn)))

                .ForMember(dest => dest.Street,
                    opt => opt.MapFrom(src => src.Localize(src.Street, src.StreetEn)))

                .ForMember(dest => dest.BuildingNumber,
                    opt => opt.MapFrom(src => src.Localize(src.BuildingNumber, src.BuildingNumberEn)))

                .ForMember(dest => dest.PostalCode,
                    opt => opt.MapFrom(src => src.PostalCode))

                .ForMember(dest => dest.Floor,
                    opt => opt.MapFrom(src => src.Localize(src.Floor, src.FloorEn)))

                .ForMember(dest => dest.Room,
                    opt => opt.MapFrom(src => src.Localize(src.Room, src.RoomEn)))

                .ForMember(dest => dest.Landmark,
                    opt => opt.MapFrom(src => src.Localize(src.Landmark, src.LandmarkEn)))

                .ForMember(dest => dest.AdditionalInformation,
                    opt => opt.MapFrom(src => src.Localize(src.AdditionalInformation, src.AdditionalInformationEn)));
        }
        public void GetBranchByIDMapping()
        {
            CreateMap<Branch, GetSingleBranchResponse>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.Localize(src.Name, src.NameEn)));
            //.ForMember(br => br.FullAddressc, ad => ad.MapFrom(sc => sc.Address.ToString()));
        }
    }
}
