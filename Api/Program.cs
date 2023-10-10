#pragma warning disable CA1506
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Text;
using Api;
using Api.CronServices;
using Api.Exceptions;
using Api.MetaData;
using Api.Middlewares;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Scheduler;
using Serilog;
using Shared.JsonConverters;
using StaticAppSettings;
using SystemTextJsonHelper;

var builder = WebApplication.CreateBuilder(args);

#region logging_configuration
if (!Directory.Exists("Logs")) Directory.CreateDirectory("Logs");
builder.Logging.ClearProviders();
Log.Logger = new LoggerConfiguration()
    .WriteTo.File($"Logs/{Assembly.GetExecutingAssembly().GetName().Name}.log", rollingInterval: RollingInterval.Hour, rollOnFileSizeLimit:true, fileSizeLimitBytes: 10000000)
    .WriteTo.Console()
    .CreateLogger();
builder.Logging.AddSerilog();
#endregion

#region dependency_injection
builder.Services.UseDi();
builder.Services.StartScheduler();
AppSettingsHelper.Configure(builder.Configuration);
builder.Services.AddHealthChecks();
#endregion

#region default_scheduled_jobs
SchedulerService.ScheduleJobAsync(new UpdateUserLastSeenCronJob());
#endregion

#region json_serializer_configuration
JsonHelper.SetGlobalJsonSerializerOptions(options =>
{
    options.Converters.Add(new DateOnlyJsonConverter());
    options.Converters.Add(new TimeOnlyJsonConverter());
    return options;
});
#endregion

#region controllers_configuration
builder.Services.AddControllers().AddJsonOptions(_ => JsonHelper.GetJsonSerializerOptions());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(corsOptions => corsOptions.AddDefaultPolicy(policy => policy.AllowAnyHeader().AllowAnyMethod().SetIsOriginAllowed(_ => true).AllowCredentials()));
#endregion

#region swagger_configuration
builder.Services.AddSwaggerGen(options =>
{
    options.SupportNonNullableReferenceTypes();
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "ACL", Version = "v1" });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header. \r\n\r\n Enter 'Bearer'[space][token] in the text input below.\r\n\r\nExample: \"Bearer OurHardWorkAreProtectedByTheseWordsPleaseDontSteal\""
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
#endregion

#region authentication_configuration
var jwtIssuer = builder.Configuration[$"{JwtMeta.OptionKey}:{JwtMeta.IssuerKey}"];
var jwtAudience = builder.Configuration[$"{JwtMeta.OptionKey}:{JwtMeta.AudienceKey}"];
var jwtKey = builder.Configuration[$"{JwtMeta.OptionKey}:{JwtMeta.KeyKey}"];
var jwtExpiryMinutes = builder.Configuration[$"{JwtMeta.OptionKey}:{JwtMeta.ExpiryMinutesKey}"];
if(string.IsNullOrWhiteSpace(jwtIssuer) || string.IsNullOrWhiteSpace(jwtAudience) || string.IsNullOrWhiteSpace(jwtKey) || string.IsNullOrWhiteSpace(jwtExpiryMinutes)) throw new JwtOptionsNotConfiguredException();
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
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtAudience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
}).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
{
    options.ExpireTimeSpan = TimeSpan.FromMinutes(int.Parse(jwtExpiryMinutes));
});
#endregion

#region app_configuration
var app = builder.Build();
if (!app.Environment.IsDevelopment()) app.UseHsts();
app.UseExceptionHandlerMiddleware();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSwagger();
app.UseSwaggerUI();
app.UseRouting();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.UseLastActiveMiddleware();
app.MapControllers();
app.MapFallbackToFile("index.html");
app.MapHealthChecks("/health");
app.Run();
#endregion
#pragma warning restore CA1506
