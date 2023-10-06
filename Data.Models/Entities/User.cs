namespace Data.Models.Entities
{
    public class User
    {
        private long Id { get; }
        public string Guid { get; }
        public string FullName { get; private set; }
        public string? Permissions { get; private set; }
        public virtual IEnumerable<Role> Roles { get; } = new List<Role>();

        protected User()
        {
        }
        
        public User(string fullName)
        {
            Guid = System.Guid.NewGuid().ToString();
            FullName = fullName;
        }

        public void SetPermissions(IEnumerable<string> permissions) => Permissions = string.Join(", ", permissions);
        public IEnumerable<string> GetPermissions() => Permissions?.Split(", ", StringSplitOptions.RemoveEmptyEntries) ?? Array.Empty<string>();
    }
}
