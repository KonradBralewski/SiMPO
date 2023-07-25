using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using SiMPO.Blazor;
using SiMPO.Blazor.Infrastracture;
using SiMPO.Blazor.Infrastracture.Authentication;

namespace SiMPO.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddInfrastructure();

            builder.Services.AddOptions();
            builder.Services.AddAuthorizationCore();

            await builder.Build().RunAsync();
        }
    }
}