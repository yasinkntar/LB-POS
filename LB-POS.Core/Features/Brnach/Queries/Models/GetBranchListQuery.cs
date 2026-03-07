using LB_POS.Core.Base;

using LB_POS.Core.Features.Brnach.Queries.Result;
using MediatR;

namespace LB_POS.Core.Features.Brnach.Queries.Models
{
    public class GetBranchListQuery : IRequest<Response<List<GetBranchListResponse>>>
    {
    }
}
