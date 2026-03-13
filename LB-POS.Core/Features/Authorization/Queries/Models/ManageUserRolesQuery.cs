using LB_POS.Core.Base;
using LB_POS.Data.Results;
using MediatR;

namespace LB_POS.Core.Features.Authorization.Queries.Models
{
    public class ManageUserRolesQuery : IRequest<Response<ManageUserRolesResult>>
    {
        public int UserId { get; set; }
    }
}
