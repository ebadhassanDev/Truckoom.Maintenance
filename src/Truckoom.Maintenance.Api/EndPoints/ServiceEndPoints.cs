namespace Truckoom.Maintenance.Api.EndPoints;

using Microsoft.AspNetCore.Mvc;
using Truckoom.Maintenance.Api.Filters;
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
        _ = app.MapGet("/", GetAllServicesAsync)
        .Produces(200, typeof(IEnumerable<Service>))
        .ProducesProblem(401)
        .ProducesProblem(429)
        .ProducesProblem(500);
#pragma warning disable CA2263

        _ = app.MapPost("/", AddServiceAsync)
        .Accepts<Service>("application/json")
        .Produces(201)
        .Produces(200)
        .ProducesProblem(500);

        _ = app.MapDelete("/{id}", DeleteServiceAsync)
        .Produces(204).ProducesProblem(404).ProducesProblem(401).Produces(429);

        _ = app.MapGet("/{id}", GetServiceByIdAsync)
        .Produces(200, typeof(Service))
        .ProducesProblem(401)
        .Produces(429);

        return app;
    }
    internal static async Task<IResult> GetAllServicesAsync([FromServices] IServiceRepository serviceRepository)
    {
        try
        {
            return Results.Ok(await serviceRepository.GetAllServicesAsync().ConfigureAwait(false));
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.StackTrace, ex.Message, StatusCodes.Status500InternalServerError);
        }
    }
    internal static async Task<IResult> AddServiceAsync([FromServices] IServiceRepository serviceRepository, Service service)
    {
        try
        {
            await serviceRepository.AddServiceAsync(service).ConfigureAwait(false);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.StackTrace, ex.Message, StatusCodes.Status500InternalServerError);
        }
    }
    internal static async Task<IResult> GetServiceByIdAsync([FromServices] IServiceRepository serviceRepository, int id)
    {
        try
        {
            return Results.Ok(await serviceRepository.GetServiceByIdAsync(id).ConfigureAwait(false));
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.StackTrace, ex.Message, StatusCodes.Status500InternalServerError);
        }
    }
    internal static async Task<IResult> DeleteServiceAsync([FromServices] IServiceRepository serviceRepository, int id)
    {
        try
        {
            await serviceRepository.DeleteServiceAsync(id).ConfigureAwait(false);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.StackTrace, ex.Message, StatusCodes.Status500InternalServerError);
        }
    }
}