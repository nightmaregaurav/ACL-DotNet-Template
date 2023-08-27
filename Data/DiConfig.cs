using Data.Abstraction;
using Data.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Data
{
    public static class DiConfig
    {
        public static void UseDataDi(this IServiceCollection services)
        {
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserCredentialRepository, UserCredentialRepository>();
        }
    }
}