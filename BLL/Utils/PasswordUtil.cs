using System.Security.Cryptography;
using System.Text;

namespace BLL.Utils
{
    internal static class PasswordUtil
    {
        static private readonly int KEY_SIZE = 64;
        static private readonly int ITERATIONS = 100_000;
        static private readonly HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA256;
        static private string hexString = "AC-BD-35-89-36-A7-7F-DC-56-B5-26-28-0E-85-4D-BE-A0-01-FC-14-DE-3E-53-0F-6E-33-15-4E-D0-5C-35-DC-59-5F-77-9B-40-61-85-43-58-86-0B-CE-93-01-A4-D7-5C-91-A3-17-6C-B3-2B-6E-69-26-1B-03-91-31-0C-FA";
        private static readonly byte[] salt = HexStringToByteArray(hexString.Replace("-", ""));
        static byte[] HexStringToByteArray(string hex)
        {
            int length = hex.Length / 2;
            byte[] byteArray = new byte[length];
            for (int i = 0; i < length; i++)
            {
                byteArray[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            }
            return byteArray;
        }
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
            var temp = Convert.FromHexString(hashedPassword);
            return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hashedPassword));
        }
    }
}
