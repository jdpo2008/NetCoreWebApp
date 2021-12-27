using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NetCoreWebApp.Infrastructure.Persistence.Contexts;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NLog.Web;
using NLog;

namespace NetCoreWebApp.WebApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            //Read Configuration from appSettings
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            //Initialize Logger
            //Log.Logger = new LoggerConfiguration()
            //    .ReadFrom.Configuration(config)
            //    .CreateLogger();

            var nlog = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                try
                {
                    var isDevelopment = services.GetRequiredService<IWebHostEnvironment>().IsDevelopment();

                    using var appContext = services.GetRequiredService<ApplicationDbContext>();

                    if (isDevelopment)
                    {
                        await appContext.Database.EnsureCreatedAsync();
                        nlog.Debug("Databases created");
                    }
                    else
                    {
                        await appContext.Database.MigrateAsync();
                        nlog.Debug("Databases Migrated");
                    }

                    //if (isDevelopment)
                    //{
                    //    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                    //    var roleManager = services.GetRequiredService<RoleManager<ApplicationRole>>();

                    //    await Infrastructure.Identity.Seeds.DefaultRoles.SeedAsync(userManager, roleManager);
                    //    await Infrastructure.Identity.Seeds.DefaultSuperAdmin.SeedAsync(userManager, roleManager);
                    //    await Infrastructure.Identity.Seeds.DefaultBasicUser.SeedAsync(userManager, roleManager);
                    //    nlog.Debug("Finished Seeding Default Data");
                    //    nlog.Debug("Application Starting");
                    //}
                     
                }
                catch (Exception ex)
                {
                    nlog.Error(ex, "An error occurred seeding the DB");
                }
                finally
                {
                    //Log.CloseAndFlush();
                    NLog.LogManager.Shutdown();
                }
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                })
                .UseNLog();
    }
}
