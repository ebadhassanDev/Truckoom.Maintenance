namespace Truckoom.Maintenance.Api.Extension;

using System.Reflection;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

public static class WebApplicationBuilderExtension
{
    public static WebApplicationBuilder ConfigureApplicationBuilder(this WebApplicationBuilder builder)
    {
        #region Logging

        _ = builder.Host.UseSerilog((hostContext, loggerConfiguration) =>
        {
            var assembly = Assembly.GetEntryAssembly();

            _ = loggerConfiguration.ReadFrom.Configuration(hostContext.Configuration)
                .Enrich.WithProperty(
                    "Assembly Version",
                    assembly?.GetCustomAttribute<AssemblyFileVersionAttribute>()?.Version)
                .Enrich.WithProperty(
                    "Assembly Informational Version",
                    assembly?.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion);
        });

        #endregion Logging

        #region Serialisation

        _ = builder.Services.Configure<JsonOptions>(opt =>
        {
            opt.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            opt.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            opt.SerializerOptions.PropertyNameCaseInsensitive = true;
            opt.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            opt.SerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
        });

        #endregion Serialisation

        #region Swagger Documentation
        _ = builder.Services.AddOpenApi();
        _ = builder.Services.AddSwaggerGen(setup =>
        {
            setup.SwaggerDoc("v1", new OpenApiInfo()
            {
                Description = "An API for managing truck maintenance at Truckoom." +
                                  "This API allows users to create, view, update, and delete truck maintenance records. " +
                                  "It uses ASP.NET Core 8.0 Minimal API, Entity Framework Core for data access, " +
                                  "token authentication for secure access, versioning for API evolution, " +
                                  "unit testing for code quality, and OpenAPI (Swagger) for documentation.",
                Title = "Truckoom Maintenance Api",
                Version = "v1",
                Contact = new OpenApiContact()
                {
                    Name = "eHassan",
                    Url = new Uri("https://www.linkedin.com/in/ebad-hassan-5272a5192")
                }
            });

            setup.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer"
            });

            setup.OperationFilter<AuthorizationHeaderOperationHeader>();
            setup.OperationFilter<ApiVersionOperationFilter>();
        });
        #endregion Swagger Documentation

        #region Api Versionining
        builder.Services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.ReportApiVersions = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ApiVersionReader = new HeaderApiVersionReader("api-version");
        });
        #endregion Api Versionining

        #region Dependency Injection
        _ = builder.Services.AddInfrastructure();
        #endregion Dependency Injection

        #region Authentication
        _ = builder.Services.AddAuthorization();
        _ = builder.Services.AddAuthentication("Bearer").AddJwtBearer();
        _ = builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(option => option.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Secret"]!)),
            ValidateIssuerSigningKey = true
        });
        #endregion Authentication

        #region Cors Policy
        _ = builder.Services.ConfigureCorsPolicy();
        #endregion 

        #region Database Health  Checkup
        _ = builder.Services.AddDatabaseDeveloperPageExceptionFilter();
        _ = builder.Services.AddHealthChecks().AddDbContextCheck<TruckoomDbContext>();
        #endregion Database Health  Checkup
        
        #region Http & Endpoints
        _ = builder.Services.AddEndpointsApiExplorer();
        _ = builder.WebHost.UseKestrel(options => options.AddServerHeader = false);
        _ = builder.Services.AddHttpContextAccessor();
        #endregion Http & Endpoints
        
        return builder;
    }
}
