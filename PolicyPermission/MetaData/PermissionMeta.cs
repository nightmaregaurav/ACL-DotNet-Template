using PolicyPermission.Abstraction.MetaData;
using PolicyPermission.ACL;

namespace PolicyPermission.MetaData
{
    public class PermissionMeta : IPermissionMeta
    {
        public IEnumerable<string> Scopes { get; }
        public IEnumerable<string> Permissions { get; }
        public IDictionary<string, IEnumerable<string>> PermissionScopeMap { get; }
        public IDictionary<string, IEnumerable<string>> PermissionDependencyMap { get; }

        public PermissionMeta()
        {
            Scopes = RequirePermissionAttribute.ListScopes();
            Permissions = RequirePermissionAttribute.ListPermissions();
            PermissionScopeMap = RequirePermissionAttribute.GetPermissionMap();
            PermissionDependencyMap = PermissionDependencies.GetDependencyMap();
        }
        
        public IEnumerable<string> ListPermissions(string scope) => PermissionScopeMap[scope];
        public IEnumerable<string> ListDependencies(string permission) => PermissionDependencyMap.Where(x => x.Key == permission).SelectMany(x => x.Value).Distinct();

        public IEnumerable<string> ListPermissionsDependencies(IEnumerable<string> permissions)
        {
            return permissions.Select(x => ListPermissionsWithDependencies(x)).SelectMany(x => x);
        }
        private IEnumerable<string> ListPermissionsWithDependencies(string permission, IList<string>? resolvedPermissions = null)
        {
            resolvedPermissions ??= new List<string>();
            var dependencies = ListDependencies(permission).Except(resolvedPermissions).ToList();
            resolvedPermissions = resolvedPermissions.Concat(dependencies).ToList();
            return dependencies.Aggregate(resolvedPermissions, (current, dep) => ListPermissionsWithDependencies(dep, current).ToList()).Distinct();
        }
    }
}