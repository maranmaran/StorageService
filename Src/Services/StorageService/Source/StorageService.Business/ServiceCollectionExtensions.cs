using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using StorageService.Business.Settings;

namespace StorageService.Business
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureBusinessLayer(this IServiceCollection services, IConfiguration configuration)
        {
            // configure app settings
            services.Configure<AppSettings>(configuration.GetSection(nameof(AppSettings)));
            services.AddSingleton(x => x.GetService<IOptions<AppSettings>>().Value);
        }
    }
}
