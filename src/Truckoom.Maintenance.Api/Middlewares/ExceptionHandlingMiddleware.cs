namespace Truckoom.Maintenance.Api.Middlewares;

using System.Text.Json;
using System.Text.Json.Serialization;

public class ExceptionHandlingMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;
    private readonly JsonSerializerOptions jsonOptions = new()
    {
        WriteIndented = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            await this.HandleExceptionAsync(context, e);
        }
    }
    private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        var statusCode = GetStatusCode(exception);
        var type = GetType(exception);

        var response = new
        {
            statusCode,
            type,
        };

        httpContext.Response.ContentType = "application/json";

        httpContext.Response.StatusCode = statusCode;

        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response, this.jsonOptions)).ConfigureAwait(false);
    }
    private static int GetStatusCode(Exception exception) =>
    exception switch
    {
        _ => StatusCodes.Status500InternalServerError
    };

    private static string GetType(Exception exception) => exception.GetType().Name;
}

