using LB_POS.Core.Base;
using LB_POS.Data.Results;
using MediatR;

namespace LB_POS.Core.Features.Authorization.Queries.Models
{
    public class ManageUserClaimsQuery : IRequest<Response<ManageUserClaimsResult>>
    {
        public int UserId { get; set; }
    }
}
