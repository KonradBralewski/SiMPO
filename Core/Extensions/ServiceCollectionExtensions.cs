using Blazorise;
using Microsoft.AspNetCore.HttpOverrides;
using System.Net;

namespace Core.Extensions
{
    internal static class ServiceCollectionExtensions
    {
        internal static IServiceCollection ConfigureServices(this IServiceCollection services, ConfigurationManager configuration)
        {
            return services;
        }
    }
}
