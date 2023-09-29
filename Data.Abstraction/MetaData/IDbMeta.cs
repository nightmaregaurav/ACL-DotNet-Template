namespace Data.Abstraction.MetaData
{
    public interface IDbMeta
    {
        public string Username { get; }
        public string Password { get; }
        public string Host { get; }
        public int Port { get; }
        public string Database { get; }
    }
}
