namespace PolicyPermission.Types
{
    public class ScopePermission
    {
        public ScopePermission(string scope, IEnumerable<string> permissions)
        {
            Scope = scope;
            Permissions = permissions;
        }

        public string Scope { get; set; }
        public IEnumerable<string> Permissions { get; set; }
    }
}