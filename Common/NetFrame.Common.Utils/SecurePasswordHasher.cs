using System;
using System.Security.Cryptography;

namespace NetFrame.Common.Utils
{
#pragma warning disable SYSLIB0023
#pragma warning disable SYSLIB0041
    /// <summary>
    /// Class containing password hash functions
    /// </summary>
    public sealed class SecurePasswordHasher
    {
        /// <summary>
        /// Salt Size
        /// </summary>
        private const int SaltSize = 16;

        /// <summary>
        /// Hash size
        /// </summary>
        private const int HashSize = 20;

        /// <summary>
        /// Generates Hash FROM Password
        /// </summary>
        /// <param name="password">password</param>
        /// <param name="iterations">iteration count</param>
        /// <returns>returns hash value</returns>
        public static string Hash(string password, int iterations)
        {
            //create salt
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[SaltSize]);

            //create hash
            Rfc2898DeriveBytes pbkdf2 = new(password, salt, iterations);
            var hash = pbkdf2.GetBytes(HashSize);

            //combine salt and hash
            var hashBytes = new byte[SaltSize + HashSize];
            Array.Copy(salt, 0, hashBytes, 0, SaltSize);
            Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

            //convert to base64
            var base64Hash = Convert.ToBase64String(hashBytes);

            //format hash with extra information
            return string.Format("$MYHASH$V1${0}${1}", iterations, base64Hash);
        }

        /// <summary>
        ///by default hash the password with 1000 repetitions
        /// </summary>
        /// <param name="password">password</param>
        /// <returns>hash value</returns>
        public static string Hash(string password)
        {
            return Hash(password, 10000);
        }

        /// <summary>
        /// Returns whether the hash value is supported
        /// </summary>
        /// <param name="hashString">hash value</param>
        /// <returns>is supported</returns>
        public static bool IsHashSupported(string hashString)
        {
            return hashString.Contains("$MYHASH$V1$");
        }

        /// <summary>
        /// Confirms that the hash of the given password is the same as the given hash value.
        /// </summary>
        /// <param name="password">Pass</param>
        /// <param name="hashedPassword">Hash</param>
        /// <returns>Is verified</returns>
        public static bool Verify(string password, string hashedPassword)
        {
            //check hash
            if (!IsHashSupported(hashedPassword))
            {
                throw new NotSupportedException("The hashtype is not supported");
            }

            //extract iteration and Base64 string
            var splittedHashString = hashedPassword.Replace("$MYHASH$V1$", "").Split('$');
            var iterations = int.Parse(splittedHashString[0]);
            var base64Hash = splittedHashString[1];

            //get hashbytes
            var hashBytes = Convert.FromBase64String(base64Hash);

            //get salt
            var salt = new byte[SaltSize];
            Array.Copy(hashBytes, 0, salt, 0, SaltSize);

            //create hash with given salt
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
            byte[] hash = pbkdf2.GetBytes(HashSize);

            //get result
            for (var i = 0; i < HashSize; i++)
            {
                if (hashBytes[i + SaltSize] != hash[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
