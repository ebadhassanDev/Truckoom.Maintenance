
var builder = WebApplication.CreateBuilder(args);
var jwtPolicyName = "jwt";
// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(setup =>
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
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddHealthChecks().AddDbContextCheck<TruckoomDbContext>();
builder.Services.AddEndpointsApiExplorer();
builder.WebHost.UseKestrel(options => options.AddServerHeader = false);
builder.Services.AddHttpContextAccessor();
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ApiVersionReader = new HeaderApiVersionReader("api-version");
});
builder.Services.AddAuthorization();
builder.Services.AddAuthentication("Bearer").AddJwtBearer();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
var versionSet = app.NewApiVersionSet()
                    .HasApiVersion(new ApiVersion(1, 0))
                    .HasApiVersion(new ApiVersion(2, 0))
                    .ReportApiVersions()
                    .Build();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Truckoom Maintenance v1");
});


var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/health", async (HealthCheckService healthCheckService) =>
{
    var report = await healthCheckService.CheckHealthAsync();
    return report.Status == HealthStatus.Healthy ?
        Results.Ok(report) : Results.StatusCode(StatusCodes.Status503ServiceUnavailable);
}).WithOpenApi()
.WithTags(["Health"])
.RequireRateLimiting(jwtPolicyName)
.Produces(200)
.ProducesProblem(503)
.Produces(429);

app.Run();