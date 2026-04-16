using LB_POS.Data.Entities.Identity;
using LB_POS.Data.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace LB_POS.Infrastructure.Seeder
{
    public static class UserSeeder
    {
        public static async Task SeedAsync(UserManager<User> _userManager, RoleManager<Role> _roleManager)
        {
            if (!await _userManager.Users.AnyAsync())
            {
                // SuperAdmin
                var superAdmin = new User
                {
                    UserName = "superadmin",
                    Email = "superadmin@project.com",
                    FullName = "Super Admin",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                };
                await _userManager.CreateAsync(superAdmin, "SuperAdmin@123");
                await _userManager.AddToRoleAsync(superAdmin, "SuperAdmin");

                // Admin
                var admin = new User
                {
                    UserName = "admin",
                    Email = "admin@project.com",
                    FullName = "Admin User",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                };
                await _userManager.CreateAsync(admin, "Admin@123");
                await _userManager.AddToRoleAsync(admin, "Admin");

                // Optional: إضافة صلاحيات إضافية للمستخدم مباشرة (User Claim)
                await _userManager.AddClaimAsync(admin, new Claim("Permission", Permission.UserManger.DeleteUser));

                // User عادي
                var normalUser = new User
                {
                    UserName = "user",
                    Email = "user@project.com",
                    FullName = "Normal User",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                };
                await _userManager.CreateAsync(normalUser, "User@123");
                await _userManager.AddToRoleAsync(normalUser, "User");
            }
        }
    }
}