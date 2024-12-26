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

        #region MinimalApi
        _ = app.MapServicesEndPoints();
        #endregion MinimalApi

        #region Swagger
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Truckoom Maintenance v1");
        });

        #endregion Swagger


        return app;
    }
}
