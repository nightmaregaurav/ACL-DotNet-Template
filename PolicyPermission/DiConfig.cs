using PolicyPermission.Abstraction.MetaData;
using PolicyPermission.Business;
using PolicyPermission.Data;
using PolicyPermission.MetaData;

namespace PolicyPermission
{
    internal static class DiConfig
    {
        public static void UseDi(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IJwtMeta, JwtMeta>();

            services.AddScoped<IDbMeta, DbMeta>();
            services.AddScoped<IUserMeta, UserMeta>();

            services.UseDataDi();
            services.UseBusinessDi();
        }
    }
}