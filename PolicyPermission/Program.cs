using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using PolicyPermission;
using PolicyPermission.Abstraction.MetaData;
using PolicyPermission.Data;
using PolicyPermission.Exceptions;
using PolicyPermission.MetaData;

var builder = WebApplication.CreateBuilder(args);
builder.Services.UseDi();

var dbSection = builder.Configuration.GetSection("DatabaseOption");
var jwtSection = builder.Configuration.GetSection("JwtOption");
var jwtOption = jwtSection.Get<JwtMeta>() ?? throw new JwtOptionsNotConfiguredException();

builder.Services.Configure<IDbMeta>(dbSection);
builder.Services.Configure<IJwtMeta>(jwtSection);

builder.Services.AddControllers().AddJsonOptions(jsonOption => jsonOption.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SupportNonNullableReferenceTypes();
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Policy Permission", Version = "v1" });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header. \r\n\r\n Enter 'Bearer'<space><token> in the text input below.\r\n\r\nExample: \"Bearer OurHardWorkAreProtectedByTheseWordsPleaseDontSteal(c)PremiumTechnologies\""
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
builder.Services.AddAuthentication(x => {
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; 
}).AddJwtBearer(o =>
{
    o.RequireHttpsMetadata = false;
    o.SaveToken = true;
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtOption.Issuer,
        ValidAudience = jwtOption.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOption.Key))
    };
}).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
{
    options.ExpireTimeSpan = TimeSpan.FromMinutes(jwtOption.ExpiryMinutes);
});

builder.Services.AddCors(corsOptions => corsOptions.AddDefaultPolicy(policy => policy.AllowAnyHeader().AllowAnyMethod().SetIsOriginAllowed(_ => true).AllowCredentials()));
builder.Services.AddDbContext<AppDbContext>();

var app = builder.Build();
if (!app.Environment.IsDevelopment()) app.UseHsts();

app.UseExceptionHandler(options => {
    options.Run(async context => {  
        context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;  
        context.Response.ContentType = "application/json";  
        var exceptionObject = context.Features.Get<IExceptionHandlerFeature>();  
        if (null != exceptionObject)
        {
            var result = new Dictionary<string, object?>
            {
                {"Endpoint", exceptionObject.Endpoint?.ToString()},
                {"Error", exceptionObject.Error.GetType().Name},
                {"Message", exceptionObject.Error.Message},
                {"Data", exceptionObject.Error.Data},
                {"HelpLink", exceptionObject.Error.HelpLink},
                {"StackTrace", exceptionObject.Error.StackTrace},
                {"InnerException", exceptionObject.Error.InnerException != null ? new Dictionary<string, object?> {
                    {"Error", exceptionObject.Error.InnerException?.GetType().Name},
                    {"Message", exceptionObject.Error.InnerException?.Message},
                    {"Data", exceptionObject.Error.InnerException?.Data},
                    {"HelpLink", exceptionObject.Error.InnerException?.HelpLink},
                    {"StackTrace", exceptionObject.Error.InnerException?.StackTrace}
                } : new Dictionary<string, object?>()}
            };
            
            var jsonResponse = JsonConvert.SerializeObject(result, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            await context.Response.WriteAsync(jsonResponse).ConfigureAwait(false);
        }  
    });  
});

app.UseHttpsRedirection();
app.UseStaticFiles();app.UseSwagger(c => c.RouteTemplate = "/swagger/{documentName}/swagger.json");
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Policy Permission");
    c.RoutePrefix = "swagger";
});

app.UseRouting();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller}/{action=Index}/{id?}");
    endpoints.MapFallbackToFile("index.html");
});

app.Run();