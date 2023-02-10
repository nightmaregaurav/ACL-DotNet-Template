namespace PolicyPermission.Entity.Entities
{
    public class UserPermission
    {
        public Guid Guid { get; }
        public User User { get; }
        public string Permission { get; }
        
        public UserPermission(User user, string permission)
        {
            Guid = Guid.NewGuid();
            User = user;
            Permission = permission;
        }
    }
}