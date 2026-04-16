using LB_POS.Data.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace LB_POS.Core.Behavior
{
    public class PermissionFilter : IAsyncAuthorizationFilter
    {
        private readonly string _permission;
        private readonly RoleManager<Role> _roleManager;

        public PermissionFilter(string permission, RoleManager<Role> roleManager)
        {
            _permission = permission;
            _roleManager = roleManager;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            // 1. فحص هل المستخدم مسجل دخول
            if (user == null || !user.Identity.IsAuthenticated)
            {
                context.Result = new ChallengeResult();
                return;
            }

            // 2. SuperAdmin bypass
            if (user.IsInRole("SuperAdmin")) return;

            // 3. جمع الـ Permissions من الـ User Claims
            var userClaims = user.Claims
                .Where(c => c.Type == "Permission")
                .Select(c => c.Value)
                .ToList();

            // 4. جلب الـ Role Claims من قاعدة البيانات (نفس منطقك)
            var roles = user.FindAll(ClaimTypes.Role).Select(r => r.Value).ToList();
            foreach (var roleName in roles)
            {
                var role = await _roleManager.FindByNameAsync(roleName);
                if (role != null)
                {
                    var roleClaims = await _roleManager.GetClaimsAsync(role);
                    userClaims.AddRange(roleClaims
                        .Where(c => c.Type == "Permission")
                        .Select(c => c.Value));
                }
            }

            // 5. التحقق من وجود الصلاحية المطلوبة
            if (!userClaims.Distinct().Contains(_permission))
            {
                context.Result = new ForbidResult(); // سيوجه المستخدم لصفحة AccessDenied
            }
        }
    }
}
