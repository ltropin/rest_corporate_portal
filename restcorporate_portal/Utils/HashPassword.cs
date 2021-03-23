using System;
using System.Security.Cryptography;
using System.Text;

namespace restcorporate_portal.Utils
{
    public static class HashPassword
    {
        public static string Hash(string password)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(password));
                var sb = new StringBuilder(hash.Length * 2);

                foreach (byte b in hash)
                {
                    // lowercase
                    sb.Append(b.ToString("x2"));
                }

                return sb.ToString();

            }
        }
    }
}
