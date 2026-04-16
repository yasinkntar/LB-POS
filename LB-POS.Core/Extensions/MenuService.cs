using LB_POS.Data.Helpers;
using System.Security.Claims;

namespace LB_POS.Core.Extensions
{
    public static class MenuService
    {
        public static List<MenuItem> FilterMenu(List<MenuItem> menu, ClaimsPrincipal user)
        {
            return menu
                .Where(item =>
                    user.IsInRole("SuperAdmin") ||
                    string.IsNullOrEmpty(item.Permission) ||
                    user.HasPermission(item.Permission))
                .Select(item => new MenuItem
                {
                    Title = item.Title,
                    Icon = item.Icon,
                    Url = item.Url,
                    Permission = item.Permission,
                    Children = FilterMenu(item.Children, user)
                })
                .Where(item => item.Children.Any() || item.Url != null)
                .ToList();
        }
    }
}
