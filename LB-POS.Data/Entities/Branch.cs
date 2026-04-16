using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LB_POS.Data.Entities
{
    public class Branch
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public required string Code { get; set; }
        [MaxLength(200)]
        public required string Name { get; set; }
        [MaxLength(200)]
        public required string NameEn { get; set; }
        public required BranchAddress Address { get; set; }
        [MaxLength(30)]
        public string? SyndicateLicenseNumber { get; set; }
        [MaxLength(10)]
        public required string ActivityCode { get; set; }
        public ICollection<Section> Sections { get; set; }
        public ICollection<Table> Tables { get; set; }
        public ICollection<Kitchen> Kitchens { get; set; }
        public ICollection<DeliveryZone> DeliveryZones { get; set; }
        public ICollection<UserBranch> UserBranches { get; set; }
    }
    [ComplexType]
    public class BranchAddress
    {
        #region Arabic Address

        [MaxLength(2)]
        public required string Country { get; set; }

        [MaxLength(100)]
        public required string Governate { get; set; }

        [MaxLength(100)]
        public required string RegionCity { get; set; }

        [MaxLength(200)]
        public required string Street { get; set; }

        [MaxLength(100)]
        public required string BuildingNumber { get; set; }

        [MaxLength(30)]
        public string? PostalCode { get; set; }


        [MaxLength(100)]
        public string? Floor { get; set; }

        [MaxLength(100)]
        public string? Room { get; set; }

        public string? Landmark { get; set; }

        public string? AdditionalInformation { get; set; }

        #endregion

        #region English Address

        [MaxLength(100)]
        public required string GovernateEn { get; set; }

        [MaxLength(100)]
        public required string RegionCityEn { get; set; }

        [MaxLength(200)]
        public required string StreetEn { get; set; }

        [MaxLength(100)]
        public required string BuildingNumberEn { get; set; }

        [MaxLength(100)]
        public string? FloorEn { get; set; }

        [MaxLength(100)]
        public string? RoomEn { get; set; }

        public string? LandmarkEn { get; set; }

        public string? AdditionalInformationEn { get; set; }

        #endregion
    }
}
