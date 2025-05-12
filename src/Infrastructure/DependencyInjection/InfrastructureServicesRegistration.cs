using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DependencyInjection;

public static class InfrastructureServicesRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddPersistence(configuration)
            .AddAuthenticationServices(configuration)
            .AddSecurity(configuration)
            .AddOtherInfrastructure();

        return services;
    }
}
// Infrastructure/DependencyInjection/InfrastructureServicesRegistration.cs
// Registers infrastructure-level services into the DI container:
// - AddPersistence → Registers DbContext (EF Core)
// - AddAuthenticationServices → Configures JWT, TokenGenerator, PasswordHasher
// - AddSecurity → Shared password hashing services
// - AddOtherInfrastructure → Clock, Email, Redis etc.