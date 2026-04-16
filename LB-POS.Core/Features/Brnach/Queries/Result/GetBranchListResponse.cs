namespace LB_POS.Core.Features.Brnach.Queries.Result
{
    public class GetBranchListResponse
    {
        public int Id { get; set; }

        public required string Code { get; set; }

        public required string Name { get; set; }
        public required string FullAddressc { get; set; }

        //public required BranchAddressResponse Address { get; set; }
        public string? SyndicateLicenseNumber { get; set; }

        public required string ActivityCode { get; set; }
    }
    public class BranchAddressResponse
    {
        public string Country { get; set; }


        public string Governate { get; set; }


        public string RegionCity { get; set; }


        public string Street { get; set; }


        public string BuildingNumber { get; set; }


        public string? PostalCode { get; set; }


        public string? Floor { get; set; }


        public string? Room { get; set; }

        public string? Landmark { get; set; }

        public string? AdditionalInformation { get; set; }

        public override string ToString()
        {
            return $"@{Country},({Governate}:{PostalCode}),{RegionCity},{BuildingNumber},{Floor},{Room},{Landmark}\n{AdditionalInformation}";
        }
    }
}
