using Core;
using Core.Extensions;
using Core.Infrastracture;

namespace Core
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddPresentation();

            builder.Services.AddInfrastructure(builder.Configuration);

            builder.Services.AddDependencies();

            builder.Services.ConfigureServices();
            builder.Configure();

            var app = builder.Build();

            app.Initialize()
               .Run();
        }
    }
}
