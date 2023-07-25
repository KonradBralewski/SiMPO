using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor.Services;
using SiMPO.Blazor.Infrastracture.AppSettings;
using SiMPO.Blazor.Infrastracture.Authentication;

namespace SiMPO.Blazor.Infrastracture
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
