using LB_POS.Core.Base;
using MediatR;

namespace LB_POS.Core.Features.Brnach.Commands.Models
{
    public class AddBranchCommand : IRequest<Response<string>>
    {

        // Basic Info
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string NameEn { get; set; } = null!;
        public string ActivityCode { get; set; } = null!;
        public string? SyndicateLicenseNumber { get; set; }

        // Address - Arabic
        public string Country { get; set; } = null!;
        public string Governate { get; set; } = null!;
        public string RegionCity { get; set; } = null!;
        public string Street { get; set; } = null!;
        public string BuildingNumber { get; set; } = null!;
        public string? PostalCode { get; set; }
        public string? Floor { get; set; }
        public string? Room { get; set; }
        public string? Landmark { get; set; }
        public string? AdditionalInformation { get; set; }

        // Address - English
        public string GovernateEn { get; set; } = null!;
        public string RegionCityEn { get; set; } = null!;
        public string StreetEn { get; set; } = null!;
        public string BuildingNumberEn { get; set; } = null!;
        public string? PostalCodeEn { get; set; }
        public string? FloorEn { get; set; }
        public string? RoomEn { get; set; }
        public string? LandmarkEn { get; set; }
        public string? AdditionalInformationEn { get; set; }
    }
}
