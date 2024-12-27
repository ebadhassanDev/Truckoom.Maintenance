namespace Truckoom.Maintenance.Api.Extension;

using Truckoom.Maintenance.Api.Configurations;

public static class ServiceExtension
{
    public static IServiceCollection ConfigureCorsPolicy(this IServiceCollection services) =>
    services.ConfigureOptions<CorsConfiguration>();
}