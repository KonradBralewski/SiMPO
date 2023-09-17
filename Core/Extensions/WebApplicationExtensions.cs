using Core.Infrastracture.Hubs;
using Core.Infrastracture.Persistence.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Shared.Contracts.Constants.Users;

namespace Core.Extensions
{
    internal static class WebApplicationExtensions
    {
        internal static WebApplication Initialize(this WebApplication app)
        {
            app.UseResponseCompression();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.ConfigureCors();
            app.UseDatabaseSeed();

            app.UseHttpsRedirection();

            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();
            app.MapControllers();
            app.MapFallbackToFile("index.html");
            app.MapHubs();

            return app;
        }

        private static WebApplication ConfigureCors(this WebApplication app)
        {
            if(app.Environment.IsDevelopment())
            {
                return app;
            }

            string[] allowedOrigins = app.Configuration.GetSection("AllowedOrigins").Get<string[]>();

            app.UseCors(configurePolicy =>
            {
                configurePolicy
                .AllowCredentials()
                .AllowAnyHeader()
                .AllowAnyMethod()
                .WithOrigins(allowedOrigins);
            });
            return app;
        }

        private static void UseDatabaseSeed(this WebApplication app)
        {
            CreateDefinedRoles(app);
        }

        private static void CreateDefinedRoles(WebApplication app)
        {
            var scope = app.Services.CreateScope();

            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            foreach (var roleName in Enum.GetNames(typeof(UserRoles)))
            {
                var roleExist = roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult();

                if (!roleExist)
                {
                    var roleResult = roleManager.CreateAsync(new ApplicationRole { Name = roleName }).GetAwaiter().GetResult();

                    if (!roleResult.Succeeded)
                    {
                        //to do: log to database
                    }
                }
            }
        }

        private static void MapHubs(this WebApplication app)
        {
            app.MapHub<ChatHub>($"/{nameof(ChatHub)}");
        }
    }
}
