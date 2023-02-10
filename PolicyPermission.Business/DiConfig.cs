using Microsoft.Extensions.DependencyInjection;
using PolicyPermission.Abstraction.Business;
using PolicyPermission.Business.Services;

namespace PolicyPermission.Business
{
    public static class DiConfig
    {
        public static void UseBusinessDi(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserCredentialService, UserCredentialService>();
        }
    }
}