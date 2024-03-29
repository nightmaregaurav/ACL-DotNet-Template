using Business.Abstraction.Services;
using Business.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Business
{
    public static class DiConfig
    {
        public static void UseBusinessDi(this IServiceCollection services)
        {
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserService, UserService>();
        }
    }
}
