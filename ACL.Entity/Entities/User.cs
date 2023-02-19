namespace ACL.Entity.Entities
{
    public class User
    {
        private long Id { get; }
        public Guid Guid { get; }
        public string FullName { get; private set; }
        public virtual Role Role { get; private set; }
        public string? Permissions { get; private set; }

        protected User()
        {
        }
        
        public User(string fullName, Role role)
        {
            Guid = Guid.NewGuid();
            Role = role;
            FullName = fullName;
        }

        public void Update(string fullName, Role role)
        {
            FullName = fullName;
            Role = role;
        }
        
        public void SetPermissions(IEnumerable<string> permissions) => Permissions = string.Join(", ", permissions);
        public IEnumerable<string> GetPermissions() => Permissions?.Split(", ", StringSplitOptions.RemoveEmptyEntries) ?? Array.Empty<string>();
    }
}