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
            services.AddTransient<AppDbContext>();
            
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IDbMeta, DbMeta>();
            services.AddSingleton<IJwtMeta, JwtMeta>();

            services.AddScoped<IUserMeta, UserMeta>();
            
            services.UseDataDi();
            services.UseBusinessDi();
        }
    }
}