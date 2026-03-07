using LB_POS.Core.Base;
using MediatR;

namespace LB_POS.Core.Features.Brnach.Commands.Models
{
    public class EditBranchCommand : IRequest<Response<string>>
    {
        public required Guid Id { get; set; }
        public required string Code { get; set; }
        public required string Name { get; set; }
        public string? SyndicateLicenseNumber { get; set; }
        public required string ActivityCode { get; set; }
        public required string Country { get; set; }

        public required string Governate { get; set; }
        public required string RegionCity { get; set; }
        public required string Street { get; set; }
        public required string BuildingNumber { get; set; }
        public string? PostalCode { get; set; }
        public string? Floor { get; set; }
        public string? Room { get; set; }
        public string? Landmark { get; set; }
        public string? AdditionalInformation { get; set; }
    }
}
