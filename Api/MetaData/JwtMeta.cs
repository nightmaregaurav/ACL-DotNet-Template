using Api.Exceptions;
using Business.Abstraction.MetaData;

namespace Api.MetaData
{
    internal class JwtMeta : IJwtMeta
    {
        public const string OptionKey = "JwtOption";
        public const string IssuerKey = "Issuer";
        public const string AudienceKey = "Audience";
        public const string KeyKey = "Key";
        public const string ExpiryMinutesKey = "ExpiryMinutes";

        public string Issuer { get; }
        public string Audience { get; }
        public string Key { get; }
        public int ExpiryMinutes { get; }


        public JwtMeta(IConfiguration configuration)
        {
            var jwtSection = configuration.GetSection(OptionKey);

            Issuer = jwtSection[IssuerKey]!;
            Audience = jwtSection[AudienceKey]!;
            Key = jwtSection[KeyKey]!;
            ExpiryMinutes = int.Parse(jwtSection[ExpiryMinutesKey]!);

            Validate();
        }

        private void Validate()
        {
            if (string.IsNullOrEmpty(Issuer) || string.IsNullOrEmpty(Audience) || string.IsNullOrEmpty(Key) || ExpiryMinutes <= 0) throw new JwtOptionsNotConfiguredException();
        }
    }
}
