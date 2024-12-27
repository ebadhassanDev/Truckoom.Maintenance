namespace Truckoom.Maintenance.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Truckoom.Maintenance.Application.Services;
using Truckoom.Maintenance.Core.Interfaces;
using Truckoom.Maintenance.Core.Models;
using Truckoom.Maintenance.Infrastructure.Repository;
using Truckoom.Maintenance.Infrastructure.TokenHelper;

public static class InfrastructureExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        _ = services.AddScoped<IServiceRepository, ServiceRepository>();
        _ = services.AddScoped<IUserRepository, UserRepository>();
        _ = services.AddTransient<IAuthService, AuthService>();
        _ = services.AddTransient<ITokenService, JwtTokenService>();
        _ = services.AddSingleton(TimeProvider.System);
        return services;
    }
}
