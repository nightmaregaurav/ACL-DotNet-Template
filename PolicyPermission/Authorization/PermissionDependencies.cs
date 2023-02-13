namespace PolicyPermission.Authorization
{
    public class PermissionDependencies
    {
        private static readonly IDictionary<Permission, IEnumerable<Permission>> Map = new Dictionary<Permission, IEnumerable<Permission>>
        {
            {
                Permission.Admin__AddPolicy, new[]
                {
                    Permission.Admin__ViewPolicy
                }
            },
            {
                Permission.Admin__DeletePolicy, new[]
                {
                    Permission.Admin__ViewPolicy
                }
            }
        };

        public static IDictionary<string, IEnumerable<string>> GetDependencyMap() => Map.ToDictionary(x => x.Key.ToString().Split("__").Last(), x => x.Value.Select(y => y.ToString().Split("__").Last()));
    }
}