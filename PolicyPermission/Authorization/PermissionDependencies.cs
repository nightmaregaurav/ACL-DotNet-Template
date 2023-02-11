using PolicyPermission.Types;

namespace PolicyPermission.Authorization
{
    public class PermissionDependencies
    {
        private static readonly IEnumerable<PermissionDependency> Map = new PermissionDependency[]
        {
            new(
                Permission.AddPolicy, new[] {
                    Permission.ViewPolicy
                }
            ),
            new(
                Permission.DeletePolicy, new[] {
                    Permission.ViewPolicy
                }
            ),
        };
        
        public static IDictionary<string, IEnumerable<string>> GetDependencyMap()
        {
            return Map.ToDictionary(x => x.Permission.ToString(), x => x.Dependencies.Select(y => y.ToString()));
        }
    }
}