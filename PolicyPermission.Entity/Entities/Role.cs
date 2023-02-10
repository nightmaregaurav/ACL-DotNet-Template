namespace PolicyPermission.Entity.Entities
{
    public class Role
    {
        private long Id { get; }
        public Guid Guid { get; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string? Permissions { get; private set; }

        protected Role()
        {
        }
        
        public Role(string name, string description)
        {
            Guid = Guid.NewGuid();
            Name = name;
            Description = description;
        }

        public void Update(string name, string description)
        {
            Name = name;
            Description = description;
        }
        
        public void SetPermissions(IEnumerable<string> permissions) => Permissions = string.Join(", ", permissions);
        public IEnumerable<string> GetPermissions() => Permissions?.Split(", ", StringSplitOptions.RemoveEmptyEntries) ?? Array.Empty<string>();
    }
}