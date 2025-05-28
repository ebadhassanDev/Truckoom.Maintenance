namespace Truckoom.Maintenance.Api.Configurations;

using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Options;
using Truckoom.Maintenance.Api.Constants;

/// <summary>
///     Configures cross-origin resource sharing (CORS) policies.
///     See https://docs.asp.net/en/latest/security/cors.html.
/// </summary>
public class CorsConfiguration : IConfigureOptions<CorsOptions>
{
    public void Configure(CorsOptions options)
    {
        options.AddPolicy(
            CorsPolicyName.AllowDomainAny,
            builder => builder
                .SetIsOriginAllowedToAllowWildcardSubdomains()
                .AllowAnyHeader()
                .WithHeaders("POST", "PUT", "PATCH", "GET", "DELETE")
                .AllowCredentials());

        options.AddPolicy(
            CorsPolicyName.AllowAny,
            x => x
                .AllowAnyOrigin()
                .WithHeaders("POST", "PUT", "PATCH", "GET", "DELETE")
                .AllowAnyHeader());
    }
}
