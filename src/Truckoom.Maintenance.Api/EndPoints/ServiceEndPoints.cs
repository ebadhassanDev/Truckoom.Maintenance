namespace Truckoom.Maintenance.Api.EndPoints;

using Truckoom.Maintenance.Api.Filters;

public static class ServiceEndPoints
{
    public static WebApplication MapServicesEndPoints(this WebApplication app)
    {
        var root = app.MapGroup("/api/services")
        .AddEndpointFilterFactory(ValidationFilter.ValidationFilterFactory)
        .WithTags("services")
        .WithDescription("Looking and Find Services and Tasks")
        .WithOpenApi();

        return app;
    }
}