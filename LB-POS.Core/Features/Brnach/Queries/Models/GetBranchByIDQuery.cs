using LB_POS.Core.Base;
using LB_POS.Core.Features.Brnach.Queries.Result;
using LB_POS.Data.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LB_POS.Core.Features.Brnach.Queries.Models
{
    public class GetBranchByIDQuery : IRequest<Response<GetSingleBranchResponse>>
    {
        public Guid ID { get; set; }
    }
}
