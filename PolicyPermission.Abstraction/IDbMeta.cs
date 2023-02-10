namespace PolicyPermission.Abstraction
{
    public class IDbMeta
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public string Port { get; set; }
        public string Database { get; set; }
    }
}