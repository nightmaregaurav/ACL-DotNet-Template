using PolicyPermission.Abstraction.MetaData;
using PolicyPermission.Exceptions;

namespace PolicyPermission.MetaData
{
    internal class DbMeta : IDbMeta
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public string Port { get; set; }
        public string Database { get; set; }

        public DbMeta(IConfiguration configuration)
        {
            var dbSection = configuration.GetSection("DatabaseOption");

            UserName = dbSection["UserName"]!;
            Password = dbSection["Password"]!;
            Host = dbSection["Host"]!;
            Port = dbSection["Port"]!;
            Database = dbSection["Database"]!;
            
            Validate();
        }
        
        private void Validate()
        {
            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(Host) || string.IsNullOrEmpty(Port) || string.IsNullOrEmpty(Database))
            {
                throw new DatabaseOptionsNotConfiguredException();
            }
        }
    }
}