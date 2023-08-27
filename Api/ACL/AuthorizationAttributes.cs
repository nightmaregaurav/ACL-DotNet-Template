using Api.MetaData;
using Business.Abstraction.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.ACL
{
    [AttributeUsage(AttributeTargets.Method)]
    public class RequirePermissionAttribute : Attribute, IAuthorizationFilter
    {
        private string RequiredPermission { get; }

        public RequirePermissionAttribute(Permission permission)
        {
            RequiredPermission = permission.ToString();
        }
        
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userMeta = (context.HttpContext.RequestServices.GetService(typeof(UserMeta)) as UserMeta)!;
            if (userMeta.Guid == Guid.Empty)
            {
                context.Result = new UnauthorizedResult();
                return;
            }
            
            var userService = (context.HttpContext.RequestServices.GetService(typeof(IUserService)) as IUserService)!;
            var permissions = userService.GetPermissions(userMeta.Guid).Result;
            if (!permissions.Contains(RequiredPermission)) context.Result = new ForbidResult();
        }
    }
    
    [AttributeUsage(AttributeTargets.Method)]
    public class RequireAnonymousAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userMeta = (context.HttpContext.RequestServices.GetService(typeof(UserMeta)) as UserMeta)!;
            if (userMeta.Guid == Guid.Empty) return;
            var result = new ObjectResult(new {message = "You are already logged in."})
            {
                StatusCode = StatusCodes.Status203NonAuthoritative
            };
            context.Result = result;
        }
    }
}
