using System.Text.RegularExpressions;
using ACL.Abstraction.Business;
using ACL.Abstraction.MetaData;
using ACL.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ACL.ACL
{
    [AttributeUsage(AttributeTargets.Method)]
    public class RequirePermissionAttribute : Attribute, IAuthorizationFilter
    {
        private string Permission { get; }

        public RequirePermissionAttribute(Permission permission)
        {
            Permission = permission.ToString().Split("__").Last();
        }
        
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userMeta = (context.HttpContext.RequestServices.GetService(typeof(IUserMeta)) as IUserMeta)!;
            if (userMeta.Guid == Guid.Empty)
            {
                context.Result = new UnauthorizedResult();
                return;
            }
            
            var userService = (context.HttpContext.RequestServices.GetService(typeof(IUserService)) as IUserService)!;
            var permissions = userService.GetPermissions().Result;
            if (!permissions.Contains(Permission)) context.Result = new ForbidResult();
        }

        private static IDictionary<string, IEnumerable<string>> PermissionMap { get; } = SetPermissionMap();
        private static IDictionary<string, IEnumerable<string>> SetPermissionMap()
        {
            var scopedPermissions = Enum.GetNames(typeof(Permission));
            
            var permissions = scopedPermissions.Select(x => x.Split("__").Last()).ToList();
            var duplicatePermissions = permissions.Where(x => permissions.Count(y => y == x) > 1).ToList();
            if (duplicatePermissions.Any()) throw new DuplicatePermissionException(duplicatePermissions);
            
            const string requiredPermissionPattern = "^[A-Z][a-zA-Z]*__[A-Z][a-zA-Z]*$";
            var illegalPermissions = scopedPermissions.Where(x => !Regex.IsMatch(x, requiredPermissionPattern)).ToList();
            if (illegalPermissions.Any()) throw new IllegalPermissionEnumNameException(illegalPermissions);

            IDictionary<string, IEnumerable<string>> permissionMap = new Dictionary<string, IEnumerable<string>>();
            foreach (var scopedPermission in scopedPermissions)
            {
                var scope = scopedPermission.Split("__").First();
                var permission = scopedPermission.Split("__").Last();
                if (!permissionMap.ContainsKey(scope)) permissionMap.Add(scope, new List<string>());
                permissionMap[scope] = permissionMap[scope].Append(permission);
            }
            return permissionMap;
        }
        
        public static IEnumerable<string> ListScopes() => PermissionMap.Keys;
        public static IEnumerable<string> ListPermissions() => Enum.GetNames(typeof(Permission)).Select(x => x.Split("__").Last()).Distinct();
        public static IDictionary<string, IEnumerable<string>> GetPermissionMap() => PermissionMap;
    }
    
    [AttributeUsage(AttributeTargets.Method)]
    public class RequireAnonymousAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userMeta = (context.HttpContext.RequestServices.GetService(typeof(IUserMeta)) as IUserMeta)!;
            if (userMeta.Guid == Guid.Empty) return;
            var result = new ObjectResult(new {message = "You are already logged in."})
            {
                StatusCode = StatusCodes.Status203NonAuthoritative
            };
            context.Result = result;
        }
    }
}