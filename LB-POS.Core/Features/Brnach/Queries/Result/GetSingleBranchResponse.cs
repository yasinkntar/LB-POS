using LB_POS.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LB_POS.Core.Features.Brnach.Queries.Result
{
    public class GetSingleBranchResponse
    {
        public Guid Id { get; set; }

        public required string Code { get; set; }

        public required string Name { get; set; }
        public required string FullAddressc { get; set; }

        public required BranchAddress Address { get; set; }
        public string? SyndicateLicenseNumber { get; set; }

        public required string ActivityCode { get; set; }
    }
}
