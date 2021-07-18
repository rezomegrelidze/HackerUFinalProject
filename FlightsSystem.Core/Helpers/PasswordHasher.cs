using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FlightsSystem.Core.Helpers
{
    public static class PasswordHasher
    {
        public static string HashSHA1(string password)
        {
            var sha1 = new SHA1CryptoServiceProvider();
            var sha1data = sha1.ComputeHash(Encoding.ASCII.GetBytes(password));
            return Encoding.ASCII.GetString(sha1data);
        }
    }
}
