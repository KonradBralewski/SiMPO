using Microsoft.AspNetCore.Mvc.Infrastructure;
using Core.Common.Errors;

namespace Core
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddRazorPages();

            services.AddSingleton<ProblemDetailsFactory, CustomProblemDetailsFactory>();

            return services;
        }
    }
}
