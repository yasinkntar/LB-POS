using LB_POS.Core.Features.Authorization.Queries.Results;
using LB_POS.Data.DTOs;
using LB_POS.Data.Entities.Identity;

namespace LB_POS.Core.Mapping.Roles
{
    public partial class RoleProfile
    {
        public void GetRoleByIdMapping()
        {
            CreateMap<Role, GetRoleByIdResult>();
        }
        public void GetRolesListMapping()
        {
            CreateMap<RoleResponse, GetRolesListResult>();
        }

    }
}
