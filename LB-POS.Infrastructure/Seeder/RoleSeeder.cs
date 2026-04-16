using LB_POS.Data.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace LB_POS.Infrastructure.Seeder
{
    public static class RoleSeeder
    {
        public static async Task SeedAsync(RoleManager<Role> _roleManager)
        {
            if (!await _roleManager.RoleExistsAsync("SuperAdmin"))
            {
                var superAdminRole = new Role { Name = "SuperAdmin" };
                await _roleManager.CreateAsync(superAdminRole);
                // SuperAdmin يعمل كل شيء، مش محتاج Claims
            }

            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                var adminRole = new Role { Name = "Admin" };
                await _roleManager.CreateAsync(adminRole);

                // إضافة بعض الصلاحيات للـ Admin Role
                // await _roleManager.AddClaimAsync(adminRole, new Claim("Permission", Permission.UserManger.CreateUser));
                //  await _roleManager.AddClaimAsync(adminRole, new Claim("Permission", Permission.UserManger.UpdateUser));
                //   await _roleManager.AddClaimAsync(adminRole, new Claim("Permission", Permission.UserManger.ViewUser));
            }

            if (!await _roleManager.RoleExistsAsync("User"))
            {
                var userRole = new Role { Name = "User" };
                await _roleManager.CreateAsync(userRole);

                // أقل صلاحيات ممكنة
                //  await _roleManager.AddClaimAsync(userRole, new Claim("Permission", Permission.UserManger.ViewUser));
            }
        }
    }
}