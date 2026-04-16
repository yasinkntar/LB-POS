using LB_POS.Core.Base;
using LB_POS.Data.DTOs;
using MediatR;

namespace LB_POS.Core.Features.Authorization.Queries.Models
{
    public class GetManageRoleClaimsQuery : IRequest<Response<ManageRoleClaimsResult>>
    {
        public int RoleId { get; set; }

        public GetManageRoleClaimsQuery(int roleId)
        {
            RoleId = roleId;
        }
    }
}
