namespace PolicyPermission.Abstraction.MetaData
{
    public interface IJwtMeta
    {
        public string Issuer { get; init; }
        public string Audience { get; init; }
        public string Key { get; init; }
        public int ExpiryMinutes { get; init; }
    }
}