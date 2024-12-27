namespace Truckoom.Maintenance.Api.EndPoints;

using Microsoft.AspNetCore.Mvc;
using Truckoom.Maintenance.Api.Filters;
using Truckoom.Maintenance.Core.Interfaces;
using Truckoom.Maintenance.Core.Models;

public static class SignInEndPoints
{
    public static WebApplication MapSignInEndPoints(this WebApplication app)
    {
        var root = app.MapGroup("/api/auth")
        .AddEndpointFilterFactory(ValidationFilter.ValidationFilterFactory)
        .WithTags("Login")
        .WithDescription("Looking for login to access the dashboard")
        .WithOpenApi();

        _ = root.MapPost("/signup", UserSignupAsync)
        .Accepts<UserDto>("application/json")
        .Produces(200)
        .ProducesProblem(500);

        _ = root.MapPost("/signin", UserSignInAsync)
        .Produces(200)
        .Produces(201)
        .ProducesProblem(500);
        
        return app;
    }

    internal static async Task<IResult> UserSignInAsync([FromServices] IAuthService authService, string userName, string password)
    {
        try
        {
            return Results.Ok(await authService.SignInAsync(userName, password));
        }
        catch(Exception ex)
        {
            return Results.Problem(ex.StackTrace, ex.Message, StatusCodes.Status500InternalServerError);
        }
    }
    internal static async Task<IResult> UserSignupAsync([FromServices] IAuthService authService, UserDto user)
    {
        try
        {
            return Results.Ok(await authService.SignupAsync(user).ConfigureAwait(false));
        }
        catch(Exception ex)
        {
            return Results.Problem(ex.StackTrace, ex.Message, StatusCodes.Status500InternalServerError);
        }
    }
}