using LB_POS.Core.Base;
using LB_POS.Core.Features.ApplicationUser.Queries.Result;
using MediatR;

namespace LB_POS.Core.Features.ApplicationUser.Queries.Models
{
    public class GetUserByIdQuery : IRequest<Response<GetUserByIdResponse>>
    {
        public int Id { get; set; }
        public GetUserByIdQuery(int id)
        {
            Id = id;
        }
    }
}
