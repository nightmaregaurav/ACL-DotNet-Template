namespace PolicyPermission.ACL
{
    public class PermissionDependencies
    {
        private static readonly IDictionary<Permission, IEnumerable<Permission>> Map = new Dictionary<Permission, IEnumerable<Permission>>
        {
        };

        public static IDictionary<string, IEnumerable<string>> GetDependencyMap() => Map.ToDictionary(x => x.Key.ToString().Split("__").Last(), x => x.Value.Select(y => y.ToString().Split("__").Last()));
    }
}