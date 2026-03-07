//using EntityFrameworkCore.EncryptColumn.Extension;
//using EntityFrameworkCore.EncryptColumn.Interfaces;
//using EntityFrameworkCore.EncryptColumn.Util;
using LB_POS.Data.Entities;
using LB_POS.Data.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LB_POS.Infrastructure.Data
{
    public class ApplicationDBContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, IdentityUserRole<int>, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public ApplicationDBContext()
        {

        }
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }
        public DbSet<User> User { get; set; }
        public DbSet<Branch> Branches { get; set; }
    }
}
