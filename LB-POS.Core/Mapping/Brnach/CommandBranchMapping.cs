using LB_POS.Core.Features.Brnach.Commands.Models;
using LB_POS.Data.Entities;

namespace LB_POS.Core.Mapping.Brnach
{
    public partial class BrnachProfile
    {
        void InsertBranchMapping()
        {
            CreateMap<AddBranchCommand, Branch>()
           .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new BranchAddress
           {
               Country = src.Country,
               Governate = src.Governate,
               RegionCity = src.RegionCity,
               Street = src.Street,
               BuildingNumber = src.BuildingNumber,

               GovernateEn = src.GovernateEn,
               RegionCityEn = src.RegionCityEn,
               StreetEn = src.StreetEn,
               BuildingNumberEn = src.BuildingNumberEn,

               PostalCode = src.PostalCode,
               Floor = src.Floor,
               Room = src.Room,
               Landmark = src.Landmark,
               AdditionalInformation = src.AdditionalInformation,
               FloorEn = src.FloorEn,
               RoomEn = src.RoomEn,
               LandmarkEn = src.LandmarkEn,
               AdditionalInformationEn = src.AdditionalInformationEn
           }))
           .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
        void EditeBranchMapping()
        {
            CreateMap<AddBranchCommand, Branch>()
           .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new BranchAddress
           {
               Country = src.Country,
               Governate = src.Governate,
               RegionCity = src.RegionCity,
               Street = src.Street,
               BuildingNumber = src.BuildingNumber,

               GovernateEn = src.GovernateEn,
               RegionCityEn = src.RegionCityEn,
               StreetEn = src.StreetEn,
               BuildingNumberEn = src.BuildingNumberEn,
               PostalCode = src.PostalCode,
               Floor = src.Floor,
               Room = src.Room,
               Landmark = src.Landmark,
               AdditionalInformation = src.AdditionalInformation,
               FloorEn = src.FloorEn,
               RoomEn = src.RoomEn,
               LandmarkEn = src.LandmarkEn,
               AdditionalInformationEn = src.AdditionalInformationEn
           }))
           .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
