using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PreOrderPlatform.API.Filters;
using PreOrderPlatform.API.Ultils;
using PreOrderPlatform.Entity;
using PreOrderPlatform.Entity.Data;
using PreOrderPlatform.Entity.Models;
using PreOrderPlatform.Service;
using PreOrderPlatform.Service.Enums;
using PreOrderPlatform.Service.Middleware;
using ScheduledTaskService;

static Task WriteResponse(HttpContext httpContext, HealthReport result)
{
    httpContext.Response.ContentType = "application/json";

    var json = new JObject(
        new JProperty("status", result.Status.ToString()),
        new JProperty("results", new JObject(result.Entries.Select(pair =>
            new JProperty(pair.Key, new JObject(
                new JProperty("status", pair.Value.Status.ToString()),
                new JProperty("description", pair.Value.Description),
                new JProperty("data", new JObject(pair.Value.Data.Select(
                    p => new JProperty(p.Key, p.Value))))))))));

    return httpContext.Response.WriteAsync(json.ToString(Formatting.Indented));
}


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ConfigureDBContext(builder.Configuration);

builder.Services.AddControllers()
                .AddNewtonsoftJson();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Pre Order System API", Version = "v1" });

    // Add this line to customize the schemaId generation
    c.CustomSchemaIds(type => type.FullName.Replace('.', '_'));

    // Add this block for JWT authentication
    c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
     {
         {
             new OpenApiSecurityScheme
             {
                 Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "bearerAuth" }
             },
             new string[] { }
         }
     });

    c.DescribeAllParametersInCamelCase();
    // Use string representation for enums in Swagger
    c.UseInlineDefinitionsForEnums();
    c.SchemaFilter<EnumSchemaFilter>();
    // Add the custom parameter filter
    c.OperationFilter<DescriptionOperationFilter>();
    //c.DescribeAllParametersInCamelCase();

    // If you have XML comments enabled, include the path here
    // var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    // var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    // c.IncludeXmlComments(xmlPath);
});

builder.Services.AddHostedService<CampaignStatusUpdater>();

//builder.Services.AddAutoMapper(typeof(ApplicationAutoMapperProfile));

// Add the following lines within the ConfigureServices method : AutoMapper, DBContext, Repos, Services
builder.Services.RegisterBusiness(builder.Configuration);

var jwtSettings = builder.Configuration.GetSection("Jwt");
var jwtSecret = jwtSettings.GetValue<string>("Secret");
var key = Encoding.ASCII.GetBytes(jwtSecret);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        RequireExpirationTime = false,
        ValidateLifetime = true
    };
});

builder.Services.AddAntiforgery(options => options.HeaderName = "X-XSRF-TOKEN");

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ADMIN", policy => policy.RequireRole(UserRole.ADMIN.ToString()));
    options.AddPolicy("BUSINESS_OWNER", policy => policy.RequireRole(UserRole.BUSINESS_OWNER.ToString()));
    options.AddPolicy("BUSINESS_STAFF", policy => policy.RequireRole(UserRole.BUSINESS_STAFF.ToString()));
    options.AddPolicy("CUSTOMER", policy => policy.RequireRole(UserRole.CUSTOMER.ToString()));
});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});
builder.Services.AddControllers(options =>
{   
    options.InputFormatters.Insert(0, MyJPIF.GetJsonPatchInputFormatter());
});

//builder.Services.AddControllers().AddJsonOptions(options =>
//{
//    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
//});
builder.Services.AddMemoryCache();
//builder.Services.AddHealthChecks()
//    .AddCheck<SystemResourcesHealthCheck>("System Resources Check");

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<PreOrderSystemContext>();
        context.Database.EnsureCreated();
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<PreOrderPlatform.API.Program>>();
        logger.LogError(ex, "An error occurred while migrating the database.");
    }
}

//app.UseRouting();

//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapHealthChecks("/healthz", new HealthCheckOptions
//    {
//        ResponseWriter = WriteResponse
//    });
//});
// Ensure the database is updated to the latest version on startup

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
    dbInitializer.Initialize();
    
}
app.UseStaticFiles();

app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<RequestResponseLoggingMiddleware>();



app.UseAuthentication(); // Add this line
app.UseAuthorization();

app.MapControllers();
app.Run();



namespace PreOrderPlatform.API
{
    public partial class Program { }
}
