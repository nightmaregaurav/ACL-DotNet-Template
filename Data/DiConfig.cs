using Data.Abstraction.Repositories;
using Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Data
{
    public static class DiConfig
    {
        public static void UseDataDi(this IServiceCollection services)
        {
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
