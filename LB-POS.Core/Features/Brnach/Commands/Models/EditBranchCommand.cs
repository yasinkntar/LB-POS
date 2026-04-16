using LB_POS.Core.Base;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace LB_POS.Core.Features.Brnach.Commands.Models
{
    public class EditBranchCommand : IRequest<Response<string>>
    {
        public int Id { get; set; } // المعرف الأساسي للتعديل

        [MaxLength(50)]
        public required string Code { get; set; }

        [MaxLength(200)]
        public required string Name { get; set; }

        [MaxLength(200)]
        public required string NameEn { get; set; }

        [MaxLength(30)]
        public string? SyndicateLicenseNumber { get; set; }

        [MaxLength(10)]
        public required string ActivityCode { get; set; }

        #region Address Properties
        // تم تسطيح (Flattening) الخصائص لسهولة الربط مع الواجهة (Binding)
        public required string Country { get; set; }
        public required string Governate { get; set; }
        public required string GovernateEn { get; set; }
        public required string RegionCity { get; set; }
        public required string RegionCityEn { get; set; }
        public required string Street { get; set; }
        public required string StreetEn { get; set; }
        public required string BuildingNumber { get; set; }
        public required string BuildingNumberEn { get; set; }
        public string? PostalCode { get; set; }
        public string? PostalCodeEn { get; set; }
        public string? Floor { get; set; }
        public string? FloorEn { get; set; }
        public string? Room { get; set; }
        public string? RoomEn { get; set; }
        public string? Landmark { get; set; }
        public string? LandmarkEn { get; set; }
        public string? AdditionalInformation { get; set; }
        public string? AdditionalInformationEn { get; set; }
        #endregion
    }
}
