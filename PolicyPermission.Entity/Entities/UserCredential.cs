namespace PolicyPermission.Entity.Entities
{
    public class UserCredential
    {
        private long Id { get; }
        public Guid Guid { get; }
        public string UserName { get; private set; }
        public string Password { get; private set; }
        public virtual User User { get; }

        protected UserCredential()
        {
        }

        public UserCredential(string userName, string password, User user)
        {
            Guid = Guid.NewGuid();
            UserName = userName;
            Password = password;
            User = user;
        }

        public void Update(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }
}