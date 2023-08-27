using Abstraction.MetaData;
using Api.Exceptions;

namespace Api.MetaData
{
    internal class JwtMeta : IJwtMeta
    {
        public string Issuer { get; }
        public string Audience { get; }
        public string Key { get; }
        public int ExpiryMinutes { get; }

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