using ACL.Abstraction.Business;
using ACL.Business.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ACL.Business
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