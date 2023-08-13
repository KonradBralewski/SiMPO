using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using Shared.Validation.Validators.Requests.Authentication;

namespace Shared.Validation.Validators
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<LoginRequestValidator>();

            return services;
        }
    }
}
