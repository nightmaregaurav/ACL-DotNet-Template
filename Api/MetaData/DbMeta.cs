using Api.Exceptions;
using Data.Abstraction.MetaData;
using SystemTextJsonHelper;

namespace Api.MetaData
{
    internal class DbMeta : IDbMeta
    {
        public string Username { get; }
        public string Password { get; }
        public string Host { get; }
        public int Port { get; }
        public string Database { get; }

        public DbMeta(IWebHostEnvironment webHostEnvironment)
        {
            var configFilePath = Path.Combine(webHostEnvironment.WebRootPath, "databases.json");
            if (!File.Exists(configFilePath)) throw new DatabaseOptionsNotConfiguredException();

            var jsonContent = File.ReadAllText(configFilePath);
            var jsonObject = JsonHelper.Deserialize<Dictionary<string, DbMeta>>(jsonContent);
            // todo: Use based on host
            var config = jsonObject?.FirstOrDefault(x => x.Key == "Username").Value;
            if(config == null) throw new DatabaseOptionsNotConfiguredException();

            Username = config.Username;
            Password = config.Password;
            Host = config.Host;
            Port = config.Port;
            Database = config.Database;

            Validate();
        }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(Username) || Password == null || string.IsNullOrWhiteSpace(Host) || Port == 0 || string.IsNullOrWhiteSpace(Database)) throw new DatabaseOptionsNotConfiguredException();
        }
    }
}
