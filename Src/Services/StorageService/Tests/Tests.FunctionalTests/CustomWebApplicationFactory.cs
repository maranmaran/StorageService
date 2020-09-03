using StorageService.Business.Settings;
using StorageService.Domain;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Tests.FunctionalTests
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        public ServiceProvider ServiceProvider { get; set; }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Create a new service provider.
                var serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider();

                // Add a database context using an in-memory 
                // database for testing.

                var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>(
                        new DbContextOptions<ApplicationDbContext>(new Dictionary<Type, IDbContextOptionsExtension>())
                    )
                    .UseInMemoryDatabase("InMemoryDbForTesting")
                    .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                    .UseInternalServiceProvider(serviceProvider);

                optionsBuilder.UseApplicationServiceProvider(serviceProvider);

                services.AddScoped(x => optionsBuilder.Options);
                services.AddSingleton<AppSettings>();
                services.AddSingleton(x => new ApplicationDbContext(optionsBuilder.Options));


                // Build the service provider.
                ServiceProvider = services.BuildServiceProvider();

                // Create a scope to obtain a reference to the database
                // context
                using var scope = ServiceProvider.CreateScope();

                var scopedServices = scope.ServiceProvider;
                var context = scopedServices.GetRequiredService<ApplicationDbContext>();

                // Ensure the database is created.
                context.Database.EnsureCreated();
                //context.Seed();

                NLog.LogManager.DisableLogging();
            });
        }
    }
}
