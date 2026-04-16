using LB_POS.Core.Features.Brnach.Queries.Result;
using LB_POS.Core.Wrappers;
using LB_POS.Data.Enums;
using MediatR;

namespace LB_POS.Core.Features.Brnach.Queries.Models
{
    public class GetBrnachPaginatedListQuery : IRequest<PaginatedResult<GetBranchListResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public BranchOrderingEnum OrderBy { get; set; }
        public string? Search { get; set; }
    }
}
