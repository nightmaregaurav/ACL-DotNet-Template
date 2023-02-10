namespace PolicyPermission.Entity.Entities
{
    public class RolePermission
    {
        private long Id { get; }
        public Guid Guid { get; }
        public Role Role { get; }
        public string Permission { get; }
        
        public RolePermission(Role role, string permission)
        {
            Guid = Guid.NewGuid();
            Role = role;
            Permission = permission;
        }
    }
}