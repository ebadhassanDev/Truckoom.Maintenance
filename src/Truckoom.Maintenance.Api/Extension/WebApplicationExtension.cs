namespace Truckoom.Maintenance.Api.Extension;

using Truckoom.Maintenance.Api.EndPoints;

public static class WebApplicationExtension
{
    public static WebApplication ConfigureApplication(this WebApplication app)
    {
        #region logging
        _ = app.UseSerilogRequestLogging();
        #endregion logging

        #region Api Configuration
        _ = app.UseHttpsRedirection();
        #endregion Api Configuration

        #region EndPoints Configuration
        _ = app.MapServicesEndPoints();
        #endregion EndPoints Configuration

        #region Swagger
        _ = app.UseSwagger();
        _ = app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Truckoom Maintenance v1"));
        #endregion Swagger

        return app;
    }
}
