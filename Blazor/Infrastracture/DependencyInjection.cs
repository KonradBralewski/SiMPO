﻿using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor.Services;
using Blazor.Infrastracture.AppSettings;
using Blazor.Infrastracture.Authentication;
using Shared.Validation.Validators;
using Shared.Abstraction.Managers.Identity;
using Blazor.Infrastracture.Managers.Identity.UsersManager;
using Shared.Abstraction.Managers.Authentication;
using Blazor.Infrastracture.Managers.Authentication;
using Blazor.Infrastracture.Interceptors.Http;

namespace Blazor.Infrastracture
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddMudServices();

            services.AddBlazoredLocalStorage();

            services.AddScoped<IAppSettingsProvider, AppSettingsProvider>();

            services.AddScoped<IUserManager, UserManager>();

            services.AddScoped<IAuthenticationManager, AuthenticationManager>();

            services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

            services.AddValidators();

            return services;
        }
    }
}
