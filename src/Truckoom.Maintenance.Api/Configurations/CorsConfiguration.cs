namespace Truckoom.Maintenance.Api.Configurations;

using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Options;

/// <summary>
///     Configures cross-origin resource sharing (CORS) policies.
///     See https://docs.asp.net/en/latest/security/cors.html.
/// </summary>
public class CorsConfiguration : IConfigureOptions<CorsOptions>
{
    public void Configure(CorsOptions options)
    {
        var allowedOrigins = this.configuration.GetSection("CorsAllowedOrigins").Get<string[]>();
        // Allow client requests only from cashtag domains and localhost:
        options.AddPolicy(
            CorsPolicyName.AllowDomainAny,
            builder => builder
                .WithOrigins(allowedOrigins)
                .SetIsOriginAllowedToAllowWildcardSubdomains()
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials());

        options.AddPolicy(
            CorsPolicyName.AllowAny,
            x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
    }
}