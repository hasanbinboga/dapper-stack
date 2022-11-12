﻿using System;
using System.Security.Cryptography;

namespace NetFrame.Common.Utils
{
    /// <summary>
    /// Şifre hash fonksiyonlarını içeren sınıf
    /// </summary>
    public sealed class SecurePasswordHasher
    {
        /// <summary>
        /// Salt Boyutu
        /// </summary>
        private const int SaltSize = 16;

        /// <summary>
        /// Hash boyutu
        /// </summary>
        private const int HashSize = 20;

        /// <summary>
        /// Şifreden Hash oluşturur
        /// </summary>
        /// <param name="password">şifre</param>
        /// <param name="iterations">tekrar sayısı</param>
        /// <returns>hash değerini döner</returns>
        public static string Hash(string password, int iterations)
        {
            //create salt
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[SaltSize]);

            //create hash
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
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
        /// varsayılan olarak 1000 tekrarla şifreyi hashler
        /// </summary>
        /// <param name="password">şifre</param>
        /// <returns>hash değeri</returns>
        public static string Hash(string password)
        {
            return Hash(password, 10000);
        }

        /// <summary>
        /// Hash değerinin desteklenip desteklenmediğini döner
        /// </summary>
        /// <param name="hashString">hash değeri</param>
        /// <returns>Desteklenme durumu</returns>
        public static bool IsHashSupported(string hashString)
        {
            return hashString.Contains("$MYHASH$V1$");
        }

        /// <summary>
        /// Verilen şifrenin hash lenmiş halinin, verilen hash değeriyle aynı olma durumunu onaylar.
        /// </summary>
        /// <param name="password">Şifre</param>
        /// <param name="hashedPassword">Hash</param>
        /// <returns>Doğrulanma durumu.</returns>
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
