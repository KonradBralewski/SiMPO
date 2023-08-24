using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor.Services;
using Blazor.Infrastracture.AppSettings;
using Blazor.Infrastracture.Authentication;
using Shared.Validation.Validators;
using Shared.Abstraction.Managers.Identity;
using Shared.Abstraction.Managers.Authentication;
using Blazor.Infrastracture.Managers.Authentication;
using Blazor.Infrastracture.Interceptors.Http;
using MudBlazor;
using Blazor.Infrastracture.Managers.Identity;
using Blazor.Infrastracture.State;
using Shared.Abstraction.Managers;
using Blazor.Infrastracture.Extensions.Reflection;

namespace Blazor.Infrastracture
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddMudServices(configuration =>
             {
                 configuration.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopCenter;
                 configuration.SnackbarConfiguration.HideTransitionDuration = 200;
                 configuration.SnackbarConfiguration.ShowTransitionDuration = 200;
                 configuration.SnackbarConfiguration.VisibleStateDuration = 4000;
                 configuration.SnackbarConfiguration.ShowCloseIcon = true;
             });

            services.AddBlazoredLocalStorage();

            services.AddScoped<IAppSettingsProvider, AppSettingsProvider>();

            services.AddManagers();

            services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

            services.AddSingleton<ApplicationState>();

            services.AddValidators();

            return services;
        }

        public static IServiceCollection AddManagers(this IServiceCollection services)
        {
            var managersInterface = typeof(IManager);
            var exampleManager = typeof(UserManager);

            var implementations = exampleManager.Assembly
                .GetLoadableTypes()
                .Where(managersInterface.IsAssignableFrom)
                .ToList();

            foreach (var type in implementations)
            {
                // we filter interfaces to skip IManager
                // e.g. UserManager inherits from IUserManager and IUserManager inherits from IManager
                if (type.GetInterfaces()
                    .FirstOrDefault(x => !x.ToString().Contains(managersInterface.ToString())) 
                    is Type implementedInterface)
                {
                    services.AddScoped(implementedInterface, type);
                }
            }

            return services;
        }
    }
}

