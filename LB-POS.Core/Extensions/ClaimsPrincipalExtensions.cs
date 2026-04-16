using System.Security.Claims;

namespace LB_POS.Core.Extensions
{

    public static class ClaimsPrincipalExtensions
    {
        public static bool HasPermission(this ClaimsPrincipal user, string permission)
        {
            if (user == null || !user.Identity.IsAuthenticated)
                return false;

            // SuperAdmin bypass
            if (user.IsInRole("SuperAdmin"))
                return true;

            // جمع كل Claims الخاصة بالمستخدم
            var userClaims = user.Claims
                .Where(c => c.Type == "Permission")
                .Select(c => c.Value)
                .ToList();

            // لاحظ: لو عندك Role Claims إضافية، يمكن إضافتها هنا
            return userClaims.Contains(permission);
        }
    }
}
