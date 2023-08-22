using Microsoft.AspNetCore.Mvc.Infrastructure;
using Core.Common.Errors;
using System.Net.Http;
using Blazor.Infrastracture.Interceptors.Http;

namespace Core
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddRazorPages();

            return services;
        }

        public static IServiceCollection AddDependencies(this IServiceCollection services)
        {
            services.AddSingleton<ProblemDetailsFactory, CustomProblemDetailsFactory>();

            services.AddTransient<HttpRequestInterceptor>();

            return services;
        }
    }
}
