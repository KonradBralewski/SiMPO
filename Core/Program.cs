using Core;
using Core.Extensions;
using Core.Infrastracture;

namespace Core
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddPresentation();

            builder.Services.AddInfrastructure(builder.Configuration);

            builder.Services.AddDependencies();

            builder.Services.ConfigureServices(builder.Configuration);

            builder.Configure();

            var app = builder.Build();

            await app.Initialize();

            await app.RunAsync();
               
        }
    }
}
