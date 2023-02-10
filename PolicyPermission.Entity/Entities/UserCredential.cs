namespace PolicyPermission.Entity.Entities
{
    public class UserCredential
    {
        private long Id { get; }
        public Guid Guid { get; }
        public string UserName { get; }
        public string Password { get; }
        public User User { get; }

        public UserCredential(string userName, string password, User user)
        {
            Guid = Guid.NewGuid();
            UserName = userName;
            Password = password;
            User = user;
        }

        public UserCredential(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }
}