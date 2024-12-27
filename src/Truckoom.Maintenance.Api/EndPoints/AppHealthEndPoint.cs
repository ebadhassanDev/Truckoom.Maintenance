namespace Truckoom.Maintenance.Api.EndPoints;

public static class AppHealthEndPoint
{
    public static WebApplication ConfigureApiRoutes(this WebApplication app, IConfiguration configuration)
    {
        _ = app.ConfigureAppHealth(configuration);
        return app;
    }
    public static WebApplication ConfigureAppHealth(this WebApplication app, IConfiguration configuration)
    {
        _ = app.MapGet("/health", async (HealthCheckService healthCheckService) =>
        {
            var report = await healthCheckService.CheckHealthAsync();
            return report.Status == HealthStatus.Healthy ?
                Results.Ok(report) : Results.StatusCode(StatusCodes.Status503ServiceUnavailable);
        }).WithOpenApi()
           .WithTags(["Health"])
           .RequireRateLimiting(configuration["jwtPolicyName"]!)
           .Produces(200)
           .ProducesProblem(503)
           .Produces(429);
        return app;
    }
}
