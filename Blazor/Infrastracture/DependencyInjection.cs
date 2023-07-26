using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor.Services;
using Blazor.Infrastracture.AppSettings;
using Blazor.Infrastracture.Authentication;

namespace Blazor.Infrastracture
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddMudServices();
            services.AddBlazoredLocalStorage();

            services.AddScoped<IAppSettingsProvider, AppSettingsProvider>();
            services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

            return services;
        }
    }
}
