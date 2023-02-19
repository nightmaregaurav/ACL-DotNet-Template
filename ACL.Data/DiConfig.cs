using ACL.Abstraction.Data;
using ACL.Data.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace ACL.Data
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