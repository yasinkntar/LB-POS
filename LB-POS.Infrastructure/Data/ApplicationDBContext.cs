using EntityFrameworkCore.EncryptColumn.Extension;
using EntityFrameworkCore.EncryptColumn.Interfaces;
using EntityFrameworkCore.EncryptColumn.Util;
using LB_POS.Data.Entities;
using LB_POS.Data.Entities.Identity;
using LB_POS.Data.Enums;
using LB_POS.Infrastructure.Abstracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace LB_POS.Infrastructure.Data
{
    public class ApplicationDBContext
        : IdentityDbContext<User, Role, int,
        IdentityUserClaim<int>,
        IdentityUserRole<int>,
        IdentityUserLogin<int>,
        IdentityRoleClaim<int>,
        IdentityUserToken<int>>
    {
        private readonly IEncryptionProvider _encryptionProvider;
        private readonly ICurrentUser _currentUser;
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options,
    ICurrentUser currentUser)
            : base(options)
        {
            _currentUser = currentUser;

            _encryptionProvider = new GenerateEncryptionProvider("8a4dcaaec64d412380fe4b02193cd26f");
        }

        // ========================
        // Identity
        // ========================
        public DbSet<User> Users { get; set; }
        public DbSet<UserRefreshToken> UserRefreshToken { get; set; }
        public DbSet<UserBranch> UserBranches { get; set; }

        // ========================
        // Core Branch System
        // ========================
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Kitchen> Kitchens { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Waiter> Waiters { get; set; }

        // ========================
        // Customers
        // ========================
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerAddress> CustomerAddresses { get; set; }

        // ========================
        // Menu System
        // ========================
        public DbSet<Category> Categories { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemKitchen> ItemKitchens { get; set; }
        public DbSet<ItemModifierGroup> ItemModifierGroups { get; set; }
        public DbSet<ItemModifierOption> ItemModifierOptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Branch>()
    .HasQueryFilter(x =>
        !_currentUser.AllowedBranches.Any()
        || _currentUser.AllowedBranches.Contains(x.Id)
    );

            modelBuilder.Entity<Section>()
    .HasQueryFilter(x =>
        !_currentUser.AllowedBranches.Any()
        || _currentUser.AllowedBranches.Contains(x.BranchId)
    );
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.UseEncryption(_encryptionProvider);
            modelBuilder.Entity<ItemKitchen>()
    .ToTable("ItemKitchens");
            // ========================
            // UserBranches (M2M)
            // ========================
            modelBuilder.Entity<UserBranch>()
                .HasKey(x => new { x.UserId, x.BranchId });

            modelBuilder.Entity<UserBranch>()
                .HasOne(x => x.User)
                .WithMany(x => x.UserBranches)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserBranch>()
                .HasOne(x => x.Branch)
                .WithMany(x => x.UserBranches)
                .HasForeignKey(x => x.BranchId)
                .OnDelete(DeleteBehavior.Restrict);

            // ========================
            // Branch Relations
            // ========================
            modelBuilder.Entity<Kitchen>()
                .HasOne(x => x.Branch)
                .WithMany(x => x.Kitchens)
                .HasForeignKey(x => x.BranchId)
                .OnDelete(DeleteBehavior.Cascade);

            // ========================
            // Driver / Waiter
            // ========================
            modelBuilder.Entity<Driver>()
                .HasOne(x => x.User)
                .WithOne(x => x.Driver)
                .HasForeignKey<Driver>(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Waiter>()
                .HasOne(x => x.User)
                .WithOne(x => x.Waiter)
                .HasForeignKey<Waiter>(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Waiter>()
                .HasOne(x => x.Section)
                .WithMany(x => x.Waiters)
                .HasForeignKey(x => x.SectionId)
                .OnDelete(DeleteBehavior.SetNull);

            // ========================
            // Customer
            // ========================
            modelBuilder.Entity<Customer>()
                .HasMany(x => x.Addresses)
                .WithOne(x => x.Customer)
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            // ========================
            // Menu System (IMPORTANT FIX AREA)
            // ========================

            modelBuilder.Entity<Item>()
                .HasOne(x => x.Category)
                .WithMany(x => x.Items)
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ItemModifierGroup>()
                .HasOne(x => x.Item)
                .WithMany(x => x.ModifierGroups)
                .HasForeignKey(x => x.ItemId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ItemModifierOption>()
                .HasOne(x => x.ModifierGroup)
                .WithMany(x => x.Options)
                .HasForeignKey(x => x.ModifierGroupId)
                .OnDelete(DeleteBehavior.Cascade);

            // ========================
            // ItemKitchen (Routing to Kitchen)
            // ========================
            modelBuilder.Entity<ItemKitchen>()
                .HasOne(x => x.Item)
                .WithMany(x => x.ItemKitchens)
                .HasForeignKey(x => x.ItemId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ItemKitchen>()
                .HasOne(x => x.Kitchen)
                .WithMany(x => x.ItemKitchens)
                .HasForeignKey(x => x.KitchenId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ItemKitchen>()
                .HasOne(x => x.Branch)
                .WithMany()
                .HasForeignKey(x => x.BranchId)
                .OnDelete(DeleteBehavior.Restrict); // 🔥 مهم جدًا لتجنب cascade cycles
        }
    }
}