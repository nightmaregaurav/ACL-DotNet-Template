using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Serilog;
using Serilog.Events;
using SystemTextJsonHelper;

namespace Api.Middlewares
{
    public static class ExceptionHandlerMiddleware
    {
        public static Action<IApplicationBuilder> GetInstance(bool isDevelopment) => options =>
        {
            options.Run(async context => {
                var exceptionObject = context.Features.Get<IExceptionHandlerFeature>();

                var statusCode = exceptionObject?.Error.GetType().GetProperty("ReferenceStatusCode")?.GetValue(exceptionObject.Error, null);
                context.Response.StatusCode = int.TryParse(statusCode?.ToString(), out var statusCodeValue) ? statusCodeValue : (int) HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                if (null != exceptionObject)
                {
                    var result = new Dictionary<string, object?>
                    {
                        {"Error", exceptionObject.Error.GetType().Name},
                        {"Message", exceptionObject.Error.Message}
                    };

                    if (isDevelopment)
                    {
                        result.Add("Endpoint", exceptionObject.Endpoint?.ToString());
                        result.Add("Data", exceptionObject.Error.Data);
                        result.Add("HelpLink", exceptionObject.Error.HelpLink);
                        result.Add("StackTrace", exceptionObject.Error.StackTrace);
                        result.Add("InnerException", exceptionObject.Error.InnerException != null ? new Dictionary<string, object?> {
                            {"Error", exceptionObject.Error.InnerException?.GetType().Name},
                            {"Message", exceptionObject.Error.InnerException?.Message},
                            {"Data", exceptionObject.Error.InnerException?.Data},
                            {"HelpLink", exceptionObject.Error.InnerException?.HelpLink},
                            {"StackTrace", exceptionObject.Error.InnerException?.StackTrace}
                        } : new Dictionary<string, object?>());
                    }

                    Log.Write(LogEventLevel.Error, exceptionObject.Error, "Error in {Endpoint}", exceptionObject.Endpoint?.ToString() ?? "UNKNOWN");

                    var jsonResponse = JsonHelper.Serialize(result);

                    await context.Response.WriteAsync(jsonResponse).ConfigureAwait(false);
                }
            });
        };
    }


    public static class ExceptionHandlerMiddlewareExtensions
    {
        public static void UseExceptionHandlerMiddleware(this WebApplication builder) => builder.UseExceptionHandler(ExceptionHandlerMiddleware.GetInstance(builder.Environment.IsDevelopment()));
    }
}
