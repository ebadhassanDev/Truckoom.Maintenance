namespace Truckoom.Maintenance.Api.EndPoints;

using Microsoft.AspNetCore.Mvc;
using Truckoom.Maintenance.Api.Filters;
using Truckoom.Maintenance.Core;
using Truckoom.Maintenance.Core.Models;

public static class ServiceEndPoints
{
    public static WebApplication MapServicesEndPoints(this WebApplication app)
    {
        var root = app.MapGroup("/api/services")
        .AddEndpointFilterFactory(ValidationFilter.ValidationFilterFactory)
        .WithTags("services")
        .WithDescription("Looking and Find Services and Tasks")
        .WithOpenApi();

#pragma warning disable CA2263
        _ = root.MapGet("/", GetAllServicesAsync)
        .Produces(200, typeof(IEnumerable<Service>))
        .ProducesProblem(401)
        .ProducesProblem(429)
        .ProducesProblem(500);
#pragma warning disable CA2263

        _ = root.MapPost("/", AddServiceAsync)
        .Accepts<Service>("application/json")
        .Produces(201)
        .Produces(200)
        .ProducesProblem(500);

        _ = root.MapPut("/",UpdateServiceAsync)
        .Accepts<Service>("application/json")
        .Produces(200)
        .ProducesProblem(500);

        _ = root.MapDelete("/{id}", DeleteServiceAsync)
        .Produces(204).ProducesProblem(404).ProducesProblem(401).Produces(429);

        _ = root.MapGet("/{id}", GetServiceByIdAsync)
        .Produces(200, typeof(Service))
        .ProducesProblem(401)
        .Produces(429);

        return app;
    }
    internal static async Task<IResult> GetAllServicesAsync([FromServices] IServicesService serviceService)
    {
        try
        {
            return Results.Ok(await serviceService.GetAllServicesAsync().ConfigureAwait(false));
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.StackTrace, ex.Message, StatusCodes.Status500InternalServerError);
        }
    }
    internal static async Task<IResult> UpdateServiceAsync([FromServices] IServicesService serviceService, Service service)
    {
        try
        {
            await serviceService.UpdateServiceAsync(service).ConfigureAwait(false);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.StackTrace, ex.Message, StatusCodes.Status500InternalServerError);
        }
    }
    internal static async Task<IResult> AddServiceAsync([FromServices] IServicesService serviceService, Service service)
    {
        try
        {
            await serviceService.AddServiceAsync(service).ConfigureAwait(false);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.StackTrace, ex.Message, StatusCodes.Status500InternalServerError);
        }
    }
    internal static async Task<IResult> GetServiceByIdAsync([FromServices] IServicesService serviceService, int id)
    {
        try
        {
            return Results.Ok(await serviceService.GetServiceByIdAsync(id).ConfigureAwait(false));
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.StackTrace, ex.Message, StatusCodes.Status500InternalServerError);
        }
    }
    internal static async Task<IResult> DeleteServiceAsync([FromServices] IServicesService serviceService, int id)
    {
        try
        {
            await serviceService.DeleteServiceAsync(id).ConfigureAwait(false);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.StackTrace, ex.Message, StatusCodes.Status500InternalServerError);
        }
    }
}
