using PolicyPermission.Abstraction.MetaData;
using PolicyPermission.Authorization;

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
    }
}