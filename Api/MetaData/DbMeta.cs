using Api.Exceptions;
using Data.Abstraction.MetaData;

namespace Api.MetaData
{
    internal class DbMeta : IDbMeta
    {
        public string UserName { get; }
        public string Password { get; }
        public string Host { get; }
        public string Port { get; }
        public string Database { get; }

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
