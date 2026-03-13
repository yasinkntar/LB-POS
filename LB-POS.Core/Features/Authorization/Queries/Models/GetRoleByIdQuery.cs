using LB_POS.Core.Base;
using LB_POS.Core.Features.Authorization.Queries.Results;
using MediatR;

namespace LB_POS.Core.Features.Authorization.Queries.Models
{
    public class GetRoleByIdQuery : IRequest<Response<GetRoleByIdResult>>
    {
        public int Id { get; set; }
    }
}
