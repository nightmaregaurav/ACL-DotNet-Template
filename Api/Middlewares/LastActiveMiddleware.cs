using RedisCacheHelper;

namespace Api.Middlewares
{
    public class LastActiveMiddleware(RequestDelegate next)
    {
        public async Task Invoke(HttpContext httpContext)
        {
            var user = httpContext.User;
            if (user.Identity?.IsAuthenticated ?? false)
            {
                var userId = user.FindFirst("uid")?.Value;
                if (!string.IsNullOrEmpty(userId))
                {
                    const string cacheKey = "UsersLastSeen";
                    var values = await CacheHelper.GetAsync<Dictionary<string, DateTime>>(cacheKey).ConfigureAwait(true) ?? new Dictionary<string, DateTime>();
                    if (values.ContainsKey(userId)) values[userId] = DateTime.UtcNow;
                    else values.Add(userId, DateTime.Now);
                    await CacheHelper.SetAsync(cacheKey, values, TimeSpan.FromDays(1)).ConfigureAwait(true);
                }
            }
            await next(httpContext).ConfigureAwait(true);
        }
    }

    public static class LastActiveMiddlewareExtensions
    {
        public static void UseLastActiveMiddleware(this IApplicationBuilder builder) => builder.UseMiddleware<LastActiveMiddleware>();
    }
}
