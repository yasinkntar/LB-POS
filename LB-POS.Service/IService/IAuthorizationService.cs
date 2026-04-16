using LB_POS.Data.DTOs;
using LB_POS.Data.Entities.Identity;
using LB_POS.Data.Results;

namespace LB_POS.Service.IService
{
    public interface IAuthorizationService
    {
        public Task<string> AddRoleAsync(string roleName);
        public Task<bool> IsRoleExistByName(string roleName);
        public Task<string> EditRoleAsync(EditRoleRequest request);
        public Task<string> DeleteRoleAsync(int roleId);
        public Task<bool> IsRoleExistById(int roleId);
        public Task<List<RoleResponse>> GetRolesList();
        public Task<Role> GetRoleById(int id);
        public Task<ManageUserRolesResult> ManageUserRolesData(User user);
        public Task<string> UpdateUserRoles(UpdateUserRolesRequest request);
        public Task<ManageUserClaimsResult> ManageUserClaimData(int userId);
        public Task<string> UpdateUserClaims(UpdateUserClaimsRequest request);
        public Task<ManageRoleClaimsResult> ManageRoleClaimsData(int roleId);
        public Task<string> UpdateRoleClaims(UpdateRoleClaimsRequest request);
    }
}
