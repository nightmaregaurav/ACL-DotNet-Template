using System.Text.Json.Serialization;
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

        [JsonConstructor]
        protected DbMeta(string username, string password, string host, int port, string database)
        {
            Username = username;
            Password = password;
            Host = host;
            Port = port;
            Database = database;
        }

        public DbMeta(IWebHostEnvironment webHostEnvironment)
        {
            var configFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "databases.json");
            if (!File.Exists(configFilePath)) throw new DatabaseOptionsNotConfiguredException();

            var jsonContent = File.ReadAllText(configFilePath);
            var jsonObject = JsonHelper.Deserialize<Dictionary<string, DbMeta>>(jsonContent, options =>
            {
                options.IncludeFields = true;
                options.NumberHandling = JsonNumberHandling.AllowReadingFromString;
                return options;
            });
            // todo: Use based on host
            var config = jsonObject?.Where(x => x.Key == "localhost:3000").Select(x => x.Value).FirstOrDefault();
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
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Host) || Port == 0 || string.IsNullOrWhiteSpace(Database)) throw new DatabaseOptionsNotConfiguredException();
        }
    }
}
