using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NetCoreWebApp.Infrastructure.Identity.Contexts;
using NetCoreWebApp.Infrastructure.Identity.Models;
using NetCoreWebApp.Infrastructure.Persistence.Contexts;
using NLog;
using NLog.Web;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

            var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                try
                {

                    var isDevelopment = services.GetRequiredService<IWebHostEnvironment>().IsDevelopment();

                    using var appContext = services.GetRequiredService<ApplicationDbContext>();
                    using var identityC0otext = services.GetRequiredService<IdentityContext>();

                    if (isDevelopment)
                    {
                        await identityC0otext.Database.EnsureCreatedAsync();
                        await appContext.Database.EnsureCreatedAsync();
                    }
                    else
                    {
                        await identityC0otext.Database.MigrateAsync();
                        await appContext.Database.MigrateAsync();
                    }

                    if (isDevelopment)
                    {
                        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                        var roleManager = services.GetRequiredService<RoleManager<ApplicationRole>>();

                        await Infrastructure.Identity.Seeds.DefaultRoles.SeedAsync(userManager, roleManager);
                        await Infrastructure.Identity.Seeds.DefaultSuperAdmin.SeedAsync(userManager, roleManager);
                        await Infrastructure.Identity.Seeds.DefaultBasicUser.SeedAsync(userManager, roleManager);
                        logger.Debug("Finished Seeding Default Data");
                        logger.Debug("Application Starting");

                    }

                }
                catch (Exception ex)
                {
                    //var logger = loggerFactory.CreateLogger<Program>();
                    logger.Error(ex, "Error al ejecutar los seeds en seguridad");
                    //Log.Error(ex, "An error occurred seeding the DB");
                }
                finally
                {
                    NLog.LogManager.Shutdown();
                    //Log.CloseAndFlush();
                }
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                }).UseNLog();
    }
}
