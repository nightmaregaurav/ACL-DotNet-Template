using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ACL.Abstraction.MetaData;
using Microsoft.IdentityModel.Tokens;

namespace ACL.Business.Helpers
{
    public class JwtTokenGenerationHelper
    {
        public static string CreateToken(IEnumerable<Claim> claims, IJwtMeta options)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Key));
            var token = new JwtSecurityToken(
                options.Issuer,
                options.Audience,
                claims,
                expires: DateTime.Now.AddMinutes(options.ExpiryMinutes),
                notBefore: DateTime.Now.Subtract(TimeSpan.FromSeconds(1)),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );
            
            return $"{new JwtSecurityTokenHandler().WriteToken(token)}";
        }
    }
}