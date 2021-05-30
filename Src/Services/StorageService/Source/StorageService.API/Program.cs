using System;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using StorageService.Domain;
using StorageService.Persistence;

namespace StorageService.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var loggerFactory = services.GetService<ILoggerFactory>();
                var logger = loggerFactory.CreateLogger<Program>();
                var config = services.GetService<IConfiguration>();
                var env = services.GetService<IWebHostEnvironment>();

                try
                {
                    // ==================== MIGRATIONS ==================
                    if (env.IsDevelopment() && config.GetValue<bool>(nameof(DatabaseSettings.MigrationEnabled)))
                    {
                        MigrateDb(services, logger);
                    }

                    logger.LogInformation($"Running {Assembly.GetExecutingAssembly().FullName}");
                    host.Run();
                }
                catch (Exception e)
                {
                    logger.LogError($"{Assembly.GetExecutingAssembly().FullName} startup failed {e.Message} {e.InnerException?.Message}");
                    throw;
                }
                finally
                {
                    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                    NLog.LogManager.Shutdown();
                }
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).ConfigureLogging((ctx, logging) =>
                {
                    logging.ClearProviders();
                    logging.AddConfiguration(ctx.Configuration.GetSection("Logging"));
                })
                .UseNLog();

        /// <summary>
        /// Migrates database
        /// </summary>
        private static void MigrateDb(IServiceProvider services, ILogger<Program> logger)
        {
            logger.LogInformation("Migrating DB");

            var context = services.GetService<ApplicationDbContext>();
            context.Database.Migrate();

            logger.LogInformation("Finished migrating DB");
        }
    }
}
