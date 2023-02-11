namespace PolicyPermission.Types
{
    public class PermissionDependency
    {
        public PermissionDependency(Permission permission, IEnumerable<Permission> dependencies)
        {
            Permission = permission;
            Dependencies = dependencies;
        }

        public Permission Permission { get; init; }
        public IEnumerable<Permission> Dependencies { get; init; }
    }
}