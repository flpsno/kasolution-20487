using System;
using System.Security.Cryptography;
using System.Text;

namespace FN.Store.Domain.Helpers
{
    public static class StringHelper
    {

        public static string Encrypt(this string senha)
        {
            var salt = "22A7AC470DD24816A4E049A6DA4B4CE27593AE55A750451CB0DDBC8E57BCC52A06E451C504264BAD9967F08EC8C3A8C0";

            var arrayBytes = Encoding.UTF8.GetBytes(senha + salt);
            byte[] hash;
            using (var sha = SHA512.Create())
            {
                hash = sha.ComputeHash(arrayBytes);
            }

            return Convert.ToBase64String(hash);
        }

    }
}
