namespace PolicyPermission.Entity.Entities
{
    public class User
    {
        public Guid Guid { get; }
        public string FullName { get; private set; }
        public Role Role { get; private set; }


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
    }
}