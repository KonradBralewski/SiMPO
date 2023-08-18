using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazor.Infrastracture;
using Blazor.Extensions;

namespace Blazor
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args)
                   .AddRootComponents()
                   .AddClientServices();

            await builder.Build().RunAsync();
        }
    }
}