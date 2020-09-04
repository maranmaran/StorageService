using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StorageService.Domain;
using StorageService.Persistence.Interfaces;
using StorageService.Persistence.Repositories;

namespace StorageService.Persistence
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigurePersistenceLayer(this IServiceCollection services, IConfiguration configuration)
        {
            // configure database settings
            //services.Configure<DatabaseSettings>(configuration.GetSection(nameof(DatabaseSettings)));
            //services.AddSingleton(x => x.GetService<IOptions<DatabaseSettings>>().Value);

            // Add database
            services.AddDbContext<ApplicationDbContext>(o =>
            {
                o.UseSqlServer(
                    configuration.GetSection(nameof(DatabaseSettings))["ConnectionString"]
                );
                o.EnableSensitiveDataLogging();
                o.EnableDetailedErrors();
                o.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            // Configure context for DI
            services.AddTransient<ApplicationDbContext>();

            // add repositories to DI
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IFolderRepository, FolderRepository>();
        }


    }
}
