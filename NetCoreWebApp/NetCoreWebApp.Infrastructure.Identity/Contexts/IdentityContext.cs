using Ardalis.EFCore.Extensions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NetCoreWebApp.Application.Interfaces;
using NetCoreWebApp.Infrastructure.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetCoreWebApp.Infrastructure.Identity.Contexts
{
    public class IdentityContext : IdentityDbContext<
        ApplicationUser, ApplicationRole, Guid,
        ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin,
        ApplicationRoleClaim, ApplicationUserToken>
    {
        private readonly IDateTimeService _dateTime;
        public IdentityContext(DbContextOptions<IdentityContext> options, IDateTimeService dateTime)
            : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _dateTime = dateTime;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<ApplicationUser>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues["isDeleted"] = false;
                        entry.Entity.Created = _dateTime.NowUtc;
                        entry.Entity.LastModified = _dateTime.NowUtc;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModified = _dateTime.NowUtc;
                        break;  
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["isDeleted"] = true;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyAllConfigurationsFromCurrentAssembly();

            modelBuilder.Entity<ApplicationUser>().Property<bool>("isDeleted");
            modelBuilder.Entity<ApplicationUser>().HasQueryFilter(m => EF.Property<bool>(m, "isDeleted") == false);

            modelBuilder.HasDefaultSchema("IDENTITY");

            modelBuilder.Entity<ApplicationUser>().ToTable("Users", "IDENTITY");

            modelBuilder.Entity<ApplicationUserClaim>().ToTable("UserClaims", "IDENTITY");

            modelBuilder.Entity<ApplicationUserLogin>().ToTable("UserLogins", "IDENTITY");

            modelBuilder.Entity<ApplicationUserToken>().ToTable("UserTokens", "IDENTITY");

            modelBuilder.Entity<ApplicationRole>().ToTable("Roles", "IDENTITY");

            modelBuilder.Entity<ApplicationRoleClaim>().ToTable("RoleClaims", "IDENTITY");

            modelBuilder.Entity<ApplicationUserRole>().ToTable("UserRoles", "IDENTITY");

        }
    }
}
