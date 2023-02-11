namespace PolicyPermission.Abstraction.MetaData
{
    public interface IJwtMeta
    {
        public string Issuer { get; }
        public string Audience { get; }
        public string Key { get; }
        public int ExpiryMinutes { get; }
    }
}