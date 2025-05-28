namespace Truckoom.Maintenance.Api.Extension;

using Truckoom.Maintenance.Api.EndPoints;
using Truckoom.Maintenance.Api.Middlewares;
using Truckoom.Maintenance.Api.Middlewares.Extension;

public static class WebApplicationExtension
{
    public static WebApplication ConfigureApplication(this WebApplication app)
    {
        #region logging
        _ = app.UseSerilogRequestLogging();
        #endregion logging

        #region Security
        _ = app.UseHsts();
        #endregion Security

        #region Api Configuration
        _ = app.UseHttpsRedirection();
        #endregion Api Configuration

        #region EndPoints Configuration
        _ = app.MapServicesEndPoints();
        _ = app.MapSignInEndPoints();
        #endregion EndPoints Configuration

        #region Swagger
        _ = app.UseSwagger();
        _ = app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Truckoom Maintenance v1"));
        #endregion Swagger

        #region Exception Handling Middleware

        _ = app.UseMiddleware<ExceptionHandlingMiddleware>();

        #endregion Exception Handling Middleware

        return app;
    }
}
