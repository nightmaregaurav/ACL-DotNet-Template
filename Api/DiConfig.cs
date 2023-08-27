using Api.ACL;
using Api.MetaData;
using Business;
using Business.Abstraction.Helpers;
using Business.Abstraction.MetaData;
using Data;
using Data.Abstraction.MetaData;

namespace Api
{
    internal static class DiConfig
    {
        public static void UseDi(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IJwtMeta, JwtMeta>();
            services.AddSingleton<PermissionHelper>();
            services.AddSingleton<IPermissionHelper, PermissionHelper>();

            services.AddScoped<IDbMeta, DbMeta>();
            services.AddScoped<UserMeta>();

            services.UseDataDi();
            services.UseBusinessDi();
        }
    }
}
