using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Extensions
{
    public static class Encryption
    {
        public static string Encrypt(this string input)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                // Convert the input string to a byte array and compute the hash.
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Create a new Stringbuilder to collect the bytes
                // and create a string.
                StringBuilder stringBuilder = new StringBuilder();

                // Loop through each byte of the hashed data
                // and format each one as a hexadecimal string.
                Array.ForEach(data,
                     item => stringBuilder.Append(item.ToString("x2"))
                    );
                // Return the hexadecimal string.
                return stringBuilder.ToString();
            }
        }

        public static bool Decrypt(this string hash, string input)
        {
            string hashOfInput = Encrypt(input);
            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            return (comparer.Compare(hashOfInput, hash) == default(int)) ? true : false;
        }
    }
}
