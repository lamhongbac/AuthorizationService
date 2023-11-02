using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace SharedLib
{
    /// <summary>
    /// lop nay dung de ma hoa cho saltHash
    /// cung co the dung de sinh ra random pass
    /// 
    /// </summary>
    public class MSASecurity
    {
        /// <summary>
        /// su dung khi dang ky 1 member 
        /// tao salt cho member hay user
        /// </summary>
        /// <returns></returns>
        public static string GetSalt()
        {
            byte[] bytes = new byte[128 / 8];
            using (var keyGenerator = RandomNumberGenerator.Create())
            {
                keyGenerator.GetBytes(bytes);
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }
        /// <summary>
        /// su dung de sinh ra hash cho member or user
        /// Usage:
        /// text=password + salt
        /// or text=BEncrypt(password)+salt
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string GetHash(string text)
        {
            // SHA512 is disposable by inheritance.  
            using (var sha256 = SHA256.Create())
            {
                // Send a sample text to hash.  
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(text));
                // Get the hashed string.  
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
    }
}
