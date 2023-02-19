using ACL.Abstraction.MetaData;
using ACL.Business;
using ACL.Data;
using ACL.MetaData;

namespace ACL
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