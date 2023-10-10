using Data.Models.Entities.Base;

namespace Data.Models.Entities
{
    public class Role : AuditableEntity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string? Permissions { get; private set; }

        public virtual IEnumerable<User> Users { get; } = new List<User>();

        protected Role()
        {
        }

        public Role(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public void SetPermissions(IEnumerable<string> permissions) => Permissions = string.Join(", ", permissions);
        public IEnumerable<string> GetPermissionList() => Permissions?.Split(", ", StringSplitOptions.RemoveEmptyEntries) ?? Array.Empty<string>();
    }
}
