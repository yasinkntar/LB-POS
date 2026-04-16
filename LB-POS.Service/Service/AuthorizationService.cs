using LB_POS.Data.DTOs;
using LB_POS.Data.Entities.Identity;
using LB_POS.Data.Helpers;
using LB_POS.Data.Results;
using LB_POS.Infrastructure.Data;
using LB_POS.Service.IService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace LB_POS.Service.Service
{
    public class AuthorizationService : IAuthorizationService
    {
        #region Fields
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDBContext _dbContext;
        #endregion

        #region Constructors
        public AuthorizationService(
            RoleManager<Role> roleManager,
            UserManager<User> userManager,
            ApplicationDBContext dbContext)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _dbContext = dbContext;
        }
        #endregion

        #region Handle Functions
        public async Task<string> AddRoleAsync(string roleName)
        {
            var result = await _roleManager.CreateAsync(new Role { Name = roleName });
            return result.Succeeded ? "Success" : string.Join(", ", result.Errors.Select(e => e.Description));
        }

        public async Task<bool> IsRoleExistByName(string roleName) =>
            await _roleManager.RoleExistsAsync(roleName);

        public async Task<string> EditRoleAsync(EditRoleRequest request)
        {
            var role = await _roleManager.FindByIdAsync(request.Id.ToString());
            if (role == null) return "NotFound";

            role.Name = request.Name;
            var result = await _roleManager.UpdateAsync(role);

            return result.Succeeded ? "Success" : string.Join(", ", result.Errors.Select(e => e.Description));
        }

        public async Task<string> DeleteRoleAsync(int roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            if (role == null) return "NotFound";

            var users = await _userManager.GetUsersInRoleAsync(role.Name);
            if (users.Any()) return "Used"; // استخدام Any أسرع وأفضل من Count > 0

            var result = await _roleManager.DeleteAsync(role);

            return result.Succeeded ? "Success" : string.Join(", ", result.Errors.Select(e => e.Description));
        }

        public async Task<bool> IsRoleExistById(int roleId) =>
            await _roleManager.FindByIdAsync(roleId.ToString()) != null;

        //public async Task<List<Role>> GetRolesList() =>
        //    await _roleManager.Roles.ToListAsync();
        public async Task<List<RoleResponse>> GetRolesList()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            var response = new List<RoleResponse>();

            foreach (var role in roles)
            {
                var users = await _userManager.GetUsersInRoleAsync(role.Name);
                response.Add(new RoleResponse
                {
                    ID = role.Id,
                    Name = role.Name,
                    CountUser = users.Count
                });
            }
            return response;
        }

        public async Task<Role> GetRoleById(int id) =>
            await _roleManager.FindByIdAsync(id.ToString());

        public async Task<ManageUserRolesResult> ManageUserRolesData(User user)
        {
            var allRoles = await _roleManager.Roles.ToListAsync();
            // جلبنا أدوار المستخدم مرة واحدة لتجنب مشكلة N+1 Query
            var userRoles = await _userManager.GetRolesAsync(user);

            return new ManageUserRolesResult
            {
                UserId = user.Id,
                FullName = user.FullName,
                userRoles = allRoles.Select(role => new UserRoles
                {
                    Id = role.Id,
                    Name = role.Name,
                    HasRole = userRoles.Contains(role.Name) // فحص سريع في الذاكرة
                }).ToList()
            };
        }

        public async Task<string> UpdateUserRoles(UpdateUserRolesRequest request)
        {
            await using var transact = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var user = await _userManager.FindByIdAsync(request.UserId.ToString());
                if (user == null) return "UserIsNull";

                var userRoles = await _userManager.GetRolesAsync(user);

                var removeResult = await _userManager.RemoveFromRolesAsync(user, userRoles);
                if (!removeResult.Succeeded) return "FailedToRemoveOldRoles";

                var selectedRoles = request.userRoles.Where(x => x.HasRole).Select(x => x.Name);

                var addRolesresult = await _userManager.AddToRolesAsync(user, selectedRoles);
                if (!addRolesresult.Succeeded) return "FailedToAddNewRoles";

                await transact.CommitAsync();
                return "Success";
            }
            catch (Exception)
            {
                await transact.RollbackAsync();
                return "FailedToUpdateUserRoles";
            }
        }

        public async Task<ManageUserClaimsResult> ManageUserClaimData(int userId)
        {
            // 1. جلب المستخدم
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null) return null;

            // 2. جلب صلاحيات المستخدم الخاصة (Direct User Claims)
            var userClaims = await _userManager.GetClaimsAsync(user);

            // 3. جلب أدوار المستخدم وصلاحيات تلك الأدوار
            var userRoles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();
            foreach (var roleName in userRoles)
            {
                var role = await _roleManager.FindByNameAsync(roleName);
                if (role != null)
                {
                    var claims = await _roleManager.GetClaimsAsync(role);
                    roleClaims.AddRange(claims);
                }
            }

            // 4. جلب جميع صلاحيات النظام من الهيلبر
            var allPermissions = Permission.GetAllPermissions();


            // 5. بناء النتيجة ودمج الصلاحيات
            var result = new ManageUserClaimsResult
            {
                UserId = user.Id,
                FullName = user.UserName,
                userClaims = allPermissions.Select(p =>
                {
                    // استخدام Equals لتجاهل حالة الأحرف والمسافات الزائدة لضمان دقة المقارنة
                    var hasRoleClaim = roleClaims.Any(rc => string.Equals(rc.Value?.Trim(), p.Value?.Trim(), StringComparison.OrdinalIgnoreCase));
                    var hasUserClaim = userClaims.Any(uc => string.Equals(uc.Value?.Trim(), p.Value?.Trim(), StringComparison.OrdinalIgnoreCase));
                    Console.WriteLine(p.Value);
                    return new UserClaims
                    {
                        GroupName = p.Group ?? "عامة",
                        DisplayName = p.Name,
                        Type = p.Value,

                        // التعديل الأهم: الصلاحية مفعّلة إذا كانت عند المستخدم مباشرة أو موروثة من الدور
                        Value = hasUserClaim || hasRoleClaim,

                        IsInheritedFromRole = hasRoleClaim // تحديد إذا كانت موروثة
                    };
                }).ToList()
            };

            return result;
        }

        public async Task<string> UpdateUserClaims(UpdateUserClaimsRequest request)
        {
            await using var transact = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var user = await _userManager.FindByIdAsync(request.UserId.ToString());
                if (user == null) return "UserIsNull";

                var userClaims = await _userManager.GetClaimsAsync(user);

                var removeClaimsResult = await _userManager.RemoveClaimsAsync(user, userClaims);
                if (!removeClaimsResult.Succeeded) return "FailedToRemoveOldClaims";

                var claimsToAdd = request.userClaims
                    .Where(x => x.Value)
                    .Select(x => new Claim("Permission", x.Type));

                var addUserClaimResult = await _userManager.AddClaimsAsync(user, claimsToAdd);
                if (!addUserClaimResult.Succeeded) return "FailedToAddNewClaims";

                await transact.CommitAsync();
                return "Success";
            }
            catch (Exception)
            {
                await transact.RollbackAsync();
                return "FailedToUpdateClaims";
            }
        }
        #endregion
        #region Manage Role Claims

        // 1. استعلام: جلب كافة الصلاحيات المتاحة مع تحديد المفعل منها لهذا الدور
        public async Task<ManageRoleClaimsResult> ManageRoleClaimsData(int roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            if (role == null) return null;

            var existingRoleClaims = await _roleManager.GetClaimsAsync(role);
            var allPermissions = Permission.GetAllPermissions(); // جلب القائمة من الهيلبر

            var aa = new ManageRoleClaimsResult
            {
                RoleId = role.Id,
                RoleName = role.Name,
                RoleClaims = allPermissions.Select(p => new RoleClaims
                {
                    GroupName = p.Group ?? "عامة",
                    DisplayName = p.Name,
                    Type = p.Value,
                    Value = existingRoleClaims.Any(rc => rc.Type == p.Value)
                }).ToList()
            };
            return aa;
        }

        // 2. تنفيذ: تحديث صلاحيات الدور (حذف القديم وإضافة الجديد)
        public async Task<string> UpdateRoleClaims(UpdateRoleClaimsRequest request)
        {
            await using var transact = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var role = await _roleManager.FindByIdAsync(request.RoleId.ToString());
                if (role == null) return "RoleNotFound";

                // حذف كل الصلاحيات الحالية المرتبطة بالدور لتجنب التكرار أو التعارض
                var existingClaims = await _roleManager.GetClaimsAsync(role);
                foreach (var claim in existingClaims)
                {
                    await _roleManager.RemoveClaimAsync(role, claim);
                }

                // فلترة الصلاحيات التي تم اختيارها (Value == true)
                var selectedClaims = request.RoleClaims
                    .Where(x => x.Value)
                    .Select(x => new Claim("Permission", x.Type));

                // إضافة الصلاحيات الجديدة
                foreach (var claim in selectedClaims)
                {
                    var addResult = await _roleManager.AddClaimAsync(role, claim);
                    if (!addResult.Succeeded)
                    {
                        await transact.RollbackAsync();
                        return "FailedToUpdateRoleClaims";
                    }
                }

                await transact.CommitAsync();
                return "Success";
            }
            catch (Exception)
            {
                await transact.RollbackAsync();
                return "ExceptionOccurred";
            }
        }



        #endregion
    }
}