namespace Api.ACL
{
    public static class PermissionConfiguration
    {
        public static readonly IDictionary<Permission, IEnumerable<Permission>> PermissionDependencyMap = new Dictionary<Permission, IEnumerable<Permission>>
        {
        };

        public static readonly IDictionary<Permission, IEnumerable<Permission>> IncompatiblePermissionMap = new Dictionary<Permission, IEnumerable<Permission>>
        {
        };
    }
}
