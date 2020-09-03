using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog.Web;
using StorageService.API.Middleware;
using StorageService.Business;
using StorageService.Persistence;
using Swashbuckle.AspNetCore.SwaggerUI;
using ILoggerFactory = Microsoft.Extensions.Logging.ILoggerFactory;

namespace StorageService.API
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env, IConfiguration config = null)
        {
            if (config == null)
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(env.ContentRootPath)
                    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                    .AddEnvironmentVariables();

                Configuration = builder.Build();
            }
            else
            {
                Configuration = config;
            }

            HostingEnvironment = env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment HostingEnvironment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // layers
            services.ConfigureBusinessLayer(Configuration);
            services.ConfigurePersistenceLayer(Configuration);

            // external libraries
            services.ConfigureMvcJsonFluentValidation();
            services.ConfigureMediatR();
            services.ConfigureSwagger();
            services.ConfigureLazyCache();
            services.ConfigureAutomapper();
            NLogBuilder.ConfigureNLog("nlog.config");

            //internal libs

            // system configuration
            services.ConfigureResponseCompression(); // response compression
            services.ConfigureCors(Configuration); // Cors
            services.ConfigureHealthCheck();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory _loggerFactory)
        {
            app.UseCors("AllowedCorsOrigins");

            // ===== Middleware to serve generated Swagger as a JSON endpoint and serve swagger-ui =====
            app.UseSwagger();
            app.UseSwaggerUI(setup =>
            {
                setup.SwaggerEndpoint("/swagger/v1/swagger.json", "Items API");
                setup.RoutePrefix = "api";
                setup.DocExpansion(DocExpansion.None);
            });

            app.UseHttpsRedirection();

            // ==== Response compression ====
            app.UseResponseCompression();

            app.UseMiddleware<GlobalExceptionMiddleware>();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("hc");
                endpoints.MapControllers();
            });
        }
    }
}
