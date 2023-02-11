namespace PolicyPermission.Abstraction.MetaData
{
    public interface IDbMeta
    {
        public string UserName { get; }
        public string Password { get; }
        public string Host { get; }
        public string Port { get; }
        public string Database { get; }
    }
}