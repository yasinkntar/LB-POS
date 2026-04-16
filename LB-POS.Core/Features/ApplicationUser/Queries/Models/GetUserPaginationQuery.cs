using LB_POS.Core.Features.ApplicationUser.Queries.Result;
using LB_POS.Core.Wrappers;
using MediatR;

namespace LB_POS.Core.Features.ApplicationUser.Queries.Models
{
    public class GetUserPaginationQuery : IRequest<PaginatedResult<GetUserPaginationReponse>>
    {
        public string Serarch { get; set; }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
