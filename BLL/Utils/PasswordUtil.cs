using System.Security.Cryptography;
using System.Text;

namespace BLL.Utils
{
    internal static class PasswordUtil
    {
        static private readonly int KEY_SIZE = 64;
        static private readonly int ITERATIONS = 100_000;
        static private readonly HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA256;
        private static readonly byte[] salt = RandomNumberGenerator.GetBytes(KEY_SIZE);

        public static string HashPassword(string password)
        {
            byte[] hashed = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                ITERATIONS,
                hashAlgorithm,
                KEY_SIZE);

            return Convert.ToHexString(hashed);
        }

        public static bool IsValidPassword(string password, string hashedPassword)
        {
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, ITERATIONS, hashAlgorithm, KEY_SIZE);

            return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hashedPassword));
        }
    }
}
