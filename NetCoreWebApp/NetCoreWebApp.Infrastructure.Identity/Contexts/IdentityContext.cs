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
            //modelBuilder.Entity<ApplicationUser>().Property<DateTime>("Created");
            //modelBuilder.Entity<ApplicationUser>().Property<DateTime>("LastModified");
            modelBuilder.Entity<ApplicationUser>().HasQueryFilter(m => EF.Property<bool>(m, "isDeleted") == false);

            //foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            //{
            //    // 1. Add properties
            //    entityType.AddProperty("IsDeleted", typeof(bool));
            //    entityType.AddProperty("Created", typeof(DateTime));
            //    entityType.AddProperty("LastModified", typeof(DateTime));

            //    // 2. Create the query filter
            //    var parameter = Expression.Parameter(entityType.ClrType);

            //    // EF.Property<bool>(post, "IsDeleted")
            //    var propertyMethodInfo = typeof(EF).GetMethod("Property").MakeGenericMethod(typeof(bool));
            //    var isDeletedProperty = Expression.Call(propertyMethodInfo, parameter, Expression.Constant("IsDeleted"));

            //    // EF.Property<bool>(post, "IsDeleted") == false
            //    BinaryExpression compareExpression = Expression.MakeBinary(ExpressionType.Equal, isDeletedProperty, Expression.Constant(false));

            //    // post => EF.Property<bool>(post, "IsDeleted") == false
            //    var lambda = Expression.Lambda(compareExpression, parameter);

            //    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
            //}

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
