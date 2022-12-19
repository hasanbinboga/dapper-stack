using System.Security.Cryptography;

namespace NetFrame.Common.Extension
{
    /// <summary>
    /// Crypto Extensions
    /// Author: havoc AT defuse.ca
    /// www: http://crackstation.net/hashing-security.htm
    /// Compatibility: .NET 3.0 and later.
    /// </summary>
#pragma warning disable SYSLIB0023
#pragma warning disable SYSLIB0041
    public class CryptoExtensions
    {
        // The following constants may be changed without breaking existing hashes.
        /// <summary>
        /// salt byte size - default 24
        /// </summary>
        public const int SaltByteSize = 24;
        /// <summary>
        /// hash byte size - default 24
        /// </summary>
        public const int HashByteSize = 24;
        /// <summary>
        /// Pbkdf2 iteration count - default 1000
        /// </summary>
        public const int Pbkdf2Iterations = 1000;

        /// <summary>
        /// Iteration Index = 0
        /// </summary>
        public const int IterationIndex = 0;
        /// <summary>
        /// Salt Index value -> 1
        /// </summary>
        public const int SaltIndex = 1;
        /// <summary>
        /// Pbkdf2 index value -> 2
        /// </summary>
        public const int Pbkdf2Index = 2;

        /// <summary>
        /// Creates Pbkdf2 hash FROM given password.
        /// </summary>
        /// <param name="pass">Password for hash</param>
        /// <returns>Hash of the password</returns>
        public static string CreateHash(string pass)
        {
            // Generate a random salt
            var csprng = new RNGCryptoServiceProvider();
            byte[] salt = new byte[SaltByteSize];
            csprng.GetBytes(salt);

            // Hash the password and encode the parameters
            byte[] hash = Pbkdf2(pass, salt, Pbkdf2Iterations, HashByteSize);
            return $"{Pbkdf2Iterations}:{Convert.ToBase64String(salt)}:{Convert.ToBase64String(hash)}";
        }

        /// <summary>
        /// Check the pass with given hash
        /// </summary>
        /// <param name="pass">Kontrol edilecek şifre.</param>
        /// <param name="hashValue">Hash value of password.</param>
        /// <returns>Return true if pass is valid else return false</returns>
        public static bool CheckPassword(string pass, string hashValue)
        {
            // Extract the parameters FROM the hash
            char[] delimiter = { ':' };
            string[] split = hashValue.Split(delimiter);
            int iterations = int.Parse(split[IterationIndex]);
            byte[] salt = Convert.FromBase64String(split[SaltIndex]);
            byte[] hash = Convert.FromBase64String(split[Pbkdf2Index]);

            byte[] testHash = Pbkdf2(pass, salt, iterations, hash.Length);
            return SlowEquals(hash, testHash);
        }

        /// <summary>
        /// Byte arrays are equals
        /// </summary>
        /// <param name="a">The first byte array.</param>
        /// <param name="b">The second byte array.</param>
        /// <returns>True if both byte arrays are equal. False otherwise.</returns>
        private static bool SlowEquals(byte[] a, byte[] b)
        {
            uint diff = (uint)a.Length ^ (uint)b.Length;
            for (int i = 0; i < a.Length && i < b.Length; i++)
                diff |= (uint)(a[i] ^ b[i]);
            return diff == 0;
        }

        /// <summary>
        /// Calculates password's Pbkdf2-SHA1 encryption.
        /// </summary>
        /// <param name="password">The password to hash.</param>
        /// <param name="salt">The salt.</param>
        /// <param name="iterations">The Pbkdf2 iteration count.</param>
        /// <param name="outputBytes">The length of the hash to generate, in bytes.</param>
        /// <returns>A hash of the password.</returns>
        private static byte[] Pbkdf2(string password, byte[] salt, int iterations, int outputBytes)
        {
            Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
            return pbkdf2.GetBytes(outputBytes);
        }
    }
}
