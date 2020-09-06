using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using AutoMapper;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using StorageService.API.LibraryConfigurations.MediatR;

namespace StorageService.API
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Configures MediatR for request pipeline
        /// With logging and validation middleware
        /// </summary>
        public static void ConfigureMediatR(this IServiceCollection services)
        {
            var assemblies = new[]
            {
                Assembly.GetAssembly(typeof(Business.Mappings)),
            };

            services.AddMediatR(assemblies.ToArray());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

        }

        /// <summary>
        /// Configures swagger ( Swashbuckle Core ) open api implementation
        /// For documentation and testing
        /// </summary>
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(action =>
            {
                action.SwaggerDoc("v1", new OpenApiInfo { Title = "Storage API", Version = "1" });


                // for swagger comments
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                action.IncludeXmlComments(xmlPath);
            });

            services.AddSwaggerGenNewtonsoftSupport(); // explicit opt-in - needs to be placed after AddSwaggerGen()
        }

        /// <summary>
        /// Configures MVC, JSON options and fluent validators
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureMvcJsonFluentValidation(this IServiceCollection services)
        {
            services.AddMvc()
                .AddFluentValidation(fv =>
                {
                    fv.RegisterValidatorsFromAssemblies(new[]
                    {
                        Assembly.GetAssembly(typeof(Business.Mappings)),
                    });

                    // so that incoming properties are not validated
                    // this is done through mediator pipeline behavior
                    fv.AutomaticValidationEnabled = false;
                })
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.AllowInputFormatterExceptionMessages = true;
                });
        }

        /// <summary>
        /// Configures response compression
        /// </summary>
        public static void ConfigureResponseCompression(this IServiceCollection services)
        {
            services.AddResponseCompression();
            services.Configure<BrotliCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Optimal;
            });
        }

        /// <summary>
        /// Configures core settings
        /// </summary>
        public static void ConfigureCors(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(opt =>
            {
                opt.AddPolicy("AllowedCorsOrigins",
                    builder => builder
                        .WithOrigins(configuration.GetSection("CORSAllowedOrigins").Get<string[]>())
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                );
            });
        }

        /// <summary>
        /// Configures lazy cache library
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureLazyCache(this IServiceCollection services)
        {
            services.AddLazyCache();
        }

        /// <summary>
        /// Adds health checks
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureHealthCheck(this IServiceCollection services)
        {
            services.AddHealthChecks();
        }

        /// <summary>
        /// Configures automapper on service level
        /// </summary>
        public static void ConfigureAutomapper(this IServiceCollection services)
        {
            services.AddSingleton<IMapper>(provider =>
            {
                var config = new MapperConfiguration(c =>
                {
                    c.AddProfile<Business.Mappings>();
                });

                return config.CreateMapper();
            });
        }
    }
}
