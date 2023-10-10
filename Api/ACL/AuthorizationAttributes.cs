using Api.MetaData;
using Business.Abstraction.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.ACL
{
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class RequirePermissionAttribute(Permission permission) : Attribute, IAuthorizationFilter
    {
        private string RequiredPermission { get; } = permission.ToString();

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userMeta = (context.HttpContext.RequestServices.GetService(typeof(UserMeta)) as UserMeta)!;
            if (string.IsNullOrWhiteSpace(userMeta.Guid))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var userService = (context.HttpContext.RequestServices.GetService(typeof(IUserService)) as IUserService)!;
            var userPermission = userService.GetPermissionsAsync(userMeta.Guid).Result;
            var direct = userPermission.DirectPermissions.ToList();
            var inheritedPermissions = userPermission.InheritedPermissions.ToList();
            var inherited = inheritedPermissions.SelectMany(x => x.Permissions).ToList();
            var permissions = new List<string>();
            permissions.AddRange(direct);
            permissions.AddRange(inherited);
            if (!permissions.Contains(RequiredPermission)) context.Result = new ForbidResult();
        }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class RequireAnonymousAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userMeta = (context.HttpContext.RequestServices.GetService(typeof(UserMeta)) as UserMeta)!;
            if (string.IsNullOrWhiteSpace(userMeta.Guid)) return;
            var result = new ObjectResult(new {message = "You are already logged in."})
            {
                StatusCode = StatusCodes.Status203NonAuthoritative
            };
            context.Result = result;
        }
    }
}
