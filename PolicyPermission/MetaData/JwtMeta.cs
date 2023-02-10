using PolicyPermission.Abstraction;

namespace PolicyPermission.MetaData
{
    internal class JwtMeta : IJwtMeta
    {
        public string Issuer { get; init; }
        public string Audience { get; init; }
        public string Key { get; init; }
        public int ExpiryMinutes { get; init; }
    }
}