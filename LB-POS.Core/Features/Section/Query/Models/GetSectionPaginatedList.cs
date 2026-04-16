using LB_POS.Core.Features.Section.Query.Result;
using LB_POS.Core.Wrappers;
using LB_POS.Data.Enums;
using MediatR;

namespace LB_POS.Core.Features.Section.Query.Models
{
    public class GetSectionPaginatedList : IRequest<PaginatedResult<GetSectionListResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? Search { get; set; }
    }
}
