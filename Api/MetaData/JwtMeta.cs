using Api.Exceptions;
using Business.Abstraction.MetaData;

namespace Api.MetaData
{
    internal class JwtMeta : IJwtMeta
    {

        public static readonly string OptionKey = "JwtOption";
        public static readonly string IssuerKey = "Issuer";
        public static readonly string AudienceKey = "Audience";
        public static readonly string KeyKey = "Key";
        public static readonly string ExpiryMinutesKey = "ExpiryMinutes";

        public string Issuer { get; set; }
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
