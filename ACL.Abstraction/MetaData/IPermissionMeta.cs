namespace ACL.Abstraction.MetaData
{
    public interface IPermissionMeta
    {
        public IEnumerable<string> Scopes { get; }
        public IEnumerable<string> Permissions { get; }
        public IDictionary<string, IEnumerable<string>> PermissionScopeMap { get; }
        public IDictionary<string, IEnumerable<string>> PermissionDependencyMap { get; }

        IEnumerable<string> ListPermissions(string scope);
        IEnumerable<string> ListDependencies(string permission);
        IEnumerable<string> ListPermissionsDependencies(IEnumerable<string> permissions);
    }
}