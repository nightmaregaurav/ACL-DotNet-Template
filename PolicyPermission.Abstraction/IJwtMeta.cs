namespace PolicyPermission.Abstraction
{
    public class IJwtMeta
    {
        public string Issuer { get; init; }
        public string Audience { get; init; }
        public string Key { get; init; }
        public int ExpiryMinutes { get; init; }
    }
}