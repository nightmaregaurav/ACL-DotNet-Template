using Microsoft.Extensions.DependencyInjection;
using PolicyPermission.Abstraction.Data;
using PolicyPermission.Data.Repository;

namespace PolicyPermission.Data
{
    public static class DiConfig
    {
        public static void UseDataDi(this IServiceCollection services)
        {
            services.AddTransient<AppDbContext>();
            
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserCredentialRepository, UserCredentialRepository>();
        }
    }
}