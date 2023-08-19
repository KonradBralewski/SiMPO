using Azure.Identity;

namespace Core.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static WebApplicationBuilder Configure(this WebApplicationBuilder builder)
        {
            builder.AddAzureKeyVaults();

            builder.DisableLoggingInProduction();

            return builder;
        }

        private static WebApplicationBuilder AddAzureKeyVaults(this WebApplicationBuilder builder)
        {
            if (builder.Environment.IsDevelopment())
            {
                return builder;
            }

            var keyVaultEndpoint = new Uri(builder.Configuration["VaultUri"]!);

            builder.Configuration.AddAzureKeyVault(keyVaultEndpoint, new DefaultAzureCredential());

            return builder;
        }

        private static WebApplicationBuilder DisableLoggingInProduction(this WebApplicationBuilder builder)
        {
            if (builder.Environment.IsProduction())
            {
                builder.Logging.SetMinimumLevel(LogLevel.None);
            }

            return builder;
        }


    }
}
