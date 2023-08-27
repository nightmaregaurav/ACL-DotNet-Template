using Abstraction.MetaData;
using Api.MetaData;
using Business;
using Data;

namespace Api
{
    internal static class DiConfig
    {
        public static void UseDi(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IJwtMeta, JwtMeta>();
            services.AddSingleton<IPermissionMeta, PermissionMeta>();

            services.AddScoped<IDbMeta, DbMeta>();
            services.AddScoped<IUserMeta, UserMeta>();

            services.UseDataDi();
            services.UseBusinessDi();
        }
    }
}