using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Business.Helpers
{
    public class PasswordHelper
    {
        private static string CreateSalt(string userName) => Convert.ToBase64String(Encoding.UTF8.GetBytes(userName));
        public static string CreateHash(string password, string userName)
        {
            var salt = CreateSalt(userName);
            var hashBytes = KeyDerivation.Pbkdf2(
                password: password,
                salt: Encoding.UTF8.GetBytes(salt),
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 10000,
                numBytesRequested: 256 / 8
            );

            return Convert.ToBase64String(hashBytes);
        }
        public static bool Validate(string user, string password, string hash) => CreateHash(password, user) == hash;
    }
}