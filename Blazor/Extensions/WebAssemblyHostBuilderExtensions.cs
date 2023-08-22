﻿using Blazor.Infrastracture;
using Blazor.Infrastracture.Interceptors.Http;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace Blazor.Extensions
{
    public static class WebAssemblyHostBuilderExtensions
    {
        public static string HttpClientName = "SimpoApiClient";

        public static WebAssemblyHostBuilder AddRootComponents(this WebAssemblyHostBuilder builder)
        {
            builder.RootComponents.Add<App>("#app");

            builder.RootComponents.Add<HeadOutlet>("head::after");

            return builder;
        }

        public static WebAssemblyHostBuilder AddClientServices(this WebAssemblyHostBuilder builder)
        {
            //string baseAddress = builder.Configuration.GetValue<string>("apiBaseUrl");

            builder.Services.AddHttpClient(HttpClientName, config =>
            {
                config.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
            })
                .AddHttpMessageHandler<HttpRequestInterceptor>();

            builder.Services.AddInfrastructure();

            builder.Services.AddOptions();

            builder.Services.AddAuthorizationCore();

            return builder;
        }
    }
}
