using Microsoft.AspNetCore.Identity;
using NetCoreWebApp.Application.Enums;
using NetCoreWebApp.Infrastructure.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreWebApp.Infrastructure.Identity.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            //Seed Roles
            await roleManager.CreateAsync(new ApplicationRole
            {
                Name = Roles.SuperAdmin.ToString(),
                Description = "Role - Super Admin",
            });
            await roleManager.CreateAsync(new ApplicationRole { 
                Name = Roles.Admin.ToString(),
                Description = "Role - Admin",
            });
            await roleManager.CreateAsync(new ApplicationRole { 
                Name = Roles.Moderator.ToString(),
                Description = "Role - Moderator",
            });
            await roleManager.CreateAsync(new ApplicationRole { 
                Name = Roles.Basic.ToString(),
                Description = "Role - Basic",
            });
        }
    }
}
