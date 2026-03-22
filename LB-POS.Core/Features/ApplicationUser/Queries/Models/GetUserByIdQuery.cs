using LB_POS.Core.Base;
using LB_POS.Core.Behavior;
using LB_POS.Core.Features.ApplicationUser.Queries.Result;
using MediatR;

namespace LB_POS.Core.Features.ApplicationUser.Queries.Models
{
    [AuthorizeClaim("Permission", "CanEditInvoice")]
    public class GetUserByIdQuery : IRequest<Response<GetUserByIdResponse>>
    {
        public int Id { get; set; }
        public GetUserByIdQuery(int id)
        {
            Id = id;
        }
    }
}
