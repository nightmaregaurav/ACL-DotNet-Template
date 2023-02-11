using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PolicyPermission.Abstraction.Business;
using PolicyPermission.Abstraction.MetaData;
using PolicyPermission.Types;

namespace PolicyPermission.Authorization
{
    [AttributeUsage(AttributeTargets.Method)]
    public class RequirePermissionAttribute : Attribute, IAuthorizationFilter
    {
        private string Permission { get; }

        public RequirePermissionAttribute(Permission permission)
        {
            Permission = permission.ToString();
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
            var permissions = userService.GetAllPermissions().Result;
            if (!permissions.Contains(Permission)) context.Result = new ForbidResult();
        }
        
        private static IDictionary<string, IEnumerable<string>> GetUsedPermissionsInAssembly(Assembly assembly)
        {
            var controllerTypes = assembly.GetTypes().Where(x => x.IsSubclassOf(typeof(ControllerBase)));

            var map = new List<ScopePermission>();
            foreach (var controllerType in controllerTypes)
            {
                var methodInfos = controllerType.GetMethods();
                var attributeValues = new List<string>();
                foreach (var methodInfo in methodInfos)
                {
                    var attributes = methodInfo.GetCustomAttributes(typeof(RequirePermissionAttribute), false);
                    if (attributes.Length <= 0) continue;
                    var attribute = (RequirePermissionAttribute)attributes[0];
                    attributeValues.Add(attribute.Permission);
                }
                map.Add(new ScopePermission(
                    controllerType.Name.Replace("Controller", ""),
                    attributeValues.Distinct()
                ));
            }
            
            var finalMap = new Dictionary<string, IEnumerable<string>>();
            var previousPermissions = new List<string>();
            foreach (var item in map)
            {
                var key = item.Scope;
                var value = item.Permissions.Where(x => !previousPermissions.Contains(x)).OrderBy(x => x).ToList();
                previousPermissions.AddRange(value);
                finalMap.Add(key, value);
            }

            return finalMap;
        }
        
        public static IEnumerable<string> ListScopes() => GetUsedPermissionsInAssembly(Assembly.GetExecutingAssembly()).Keys;
        public static IEnumerable<string> ListPermissions() => GetUsedPermissionsInAssembly(Assembly.GetExecutingAssembly()).Values.SelectMany(x => x);
        public static IEnumerable<string> ListPermissions(string scope) => GetUsedPermissionsInAssembly(Assembly.GetExecutingAssembly())[scope];
        public static IDictionary<string, IEnumerable<string>> GetPermissionMap() => GetUsedPermissionsInAssembly(Assembly.GetExecutingAssembly());
    }
    
    [AttributeUsage(AttributeTargets.Method)]
    public class RequireAnonymousAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userMeta = (context.HttpContext.RequestServices.GetService(typeof(IUserMeta)) as IUserMeta)!;
            if (userMeta.Guid != Guid.Empty)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}