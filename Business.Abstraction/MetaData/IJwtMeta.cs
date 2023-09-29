namespace Business.Abstraction.MetaData
{
    public interface IJwtMeta
    {
        string Key { get; }
        string Issuer { get; }
        string Audience { get; }
        int ExpiryMinutes { get; }
    }
}
