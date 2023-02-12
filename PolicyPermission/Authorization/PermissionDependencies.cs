using PolicyPermission.Types;

namespace PolicyPermission.Authorization
{
    public class PermissionDependencies
    {
        private static readonly IEnumerable<PermissionDependency> Map = new PermissionDependency[]
        {
            new(
                Permission.Admin__AddPolicy, new[] {
                    Permission.Admin__ViewPolicy
                }
            ),
            new(
                Permission.Admin__DeletePolicy, new[] {
                    Permission.Admin__ViewPolicy
                }
            ),
        };
        
        public static IDictionary<string, IEnumerable<string>> GetDependencyMap()
        {
            return Map.ToDictionary(x => x.Permission.ToString(), x => x.Dependencies.Select(y => y.ToString()));
        }
    }
}