namespace Truckoom.Maintenance.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Truckoom.Maintenance.Core;
using Truckoom.Maintenance.Infrastructure.Repository;

public static class InfrastructureExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        _ = services.AddTransient<IServiceRepository, ServiceRepository>();
        return services;
    }
}
