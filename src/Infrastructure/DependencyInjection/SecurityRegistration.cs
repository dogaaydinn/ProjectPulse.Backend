using Infrastructure.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Security;
using PasswordOptions = Infrastructure.Security.PasswordOptions;

namespace Infrastructure.DependencyInjection;

public static class SecurityRegistration
{
    public static IServiceCollection AddSecurity(this IServiceCollection services, IConfiguration configuration)
    {
        var passwordOptions = new PasswordOptions();
        configuration.GetSection("PasswordOptions").Bind(passwordOptions);

        services.AddSingleton(passwordOptions);
        services.AddScoped<IUserPasswordHasher, PasswordHasher>();


        return services;
    }
}