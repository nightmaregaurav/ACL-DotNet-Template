using PolicyPermission.Abstraction.MetaData;
using PolicyPermission.Exceptions;

namespace PolicyPermission.MetaData
{
    internal class JwtMeta : IJwtMeta
    {
        public string Issuer { get; init; }
        public string Audience { get; init; }
        public string Key { get; init; }
        public int ExpiryMinutes { get; init; }

        public JwtMeta(IConfiguration configuration)
        {
            var jwtSection = configuration.GetSection("JwtOption");
            
            Issuer = jwtSection["Issuer"]!;
            Audience = jwtSection["Audience"]!;
            Key = jwtSection["Key"]!;
            ExpiryMinutes = int.Parse(jwtSection["ExpiryMinutes"]!);
            
            Validate();
        }
        
        private void Validate()
        {
            if (string.IsNullOrEmpty(Issuer) || string.IsNullOrEmpty(Audience) || string.IsNullOrEmpty(Key) || ExpiryMinutes <= 0)
            {
                throw new JwtOptionsNotConfiguredException();
            }
        }
    }
}