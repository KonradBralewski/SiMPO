using Core.Infrastracture.Persistence.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Shared.Contracts.Constants.Users;

namespace Core.Extensions
{
    internal static class WebApplicationExtensions
    {
        internal static async Task<WebApplication> Initialize(this WebApplication app)
        {
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

            await app.CreateDefinedRoles();
            app.ConfigureCors();

            app.UseHttpsRedirection();

            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();
            app.MapControllers();
            app.MapFallbackToFile("index.html");

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

        private static async Task<WebApplication> CreateDefinedRoles(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            if(roleManager is null || userManager is null)
            {
                // to do: log to database
                return app;
            }

            IdentityResult roleResult;

            foreach (var roleName in Enum.GetNames(typeof(UserRoles)))
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);

                if (!roleExist)
                {
                    roleResult = await roleManager.CreateAsync(new ApplicationRole { Name = roleName});

                    if(!roleResult.Succeeded)
                    {
                        //to do: log to database
                    }
                }
            }

            return app;
        }
    }
}
