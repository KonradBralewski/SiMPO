using Blazorise;
using Microsoft.AspNetCore.HttpOverrides;
using System.Net;

namespace Core.Extensions
{
    internal static class ServiceCollectionExtensions
    {
        internal static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.ConfigureCorsPolicy();

            return services;
        }
        internal static IServiceCollection ConfigureCorsPolicy(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder
                            .AllowCredentials()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });

            return services;
        }
    }
}
