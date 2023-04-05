using System;
using System.Security.Cryptography;
using System.Text;

using IdentityModel;

namespace IdentityServer3.AccessTokenValidation.Extensions
{
    internal static class HashStringExtensions
    {
        public static string ToSha256(this string input, HashStringEncoding encoding = HashStringEncoding.Base64)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;
            using (SHA256 shA256 = SHA256.Create())
            {
                byte[] bytes = Encoding.ASCII.GetBytes(input);
                return HashStringExtensions.Encode(shA256.ComputeHash(bytes), encoding);
            }
        }

        public static string ToSha512(this string input, HashStringEncoding encoding = HashStringEncoding.Base64)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;
            using (SHA512 shA512 = SHA512.Create())
            {
                byte[] bytes = Encoding.ASCII.GetBytes(input);
                return HashStringExtensions.Encode(shA512.ComputeHash(bytes), encoding);
            }
        }

        private static string Encode(byte[] hash, HashStringEncoding encoding)
        {
            if (encoding == HashStringEncoding.Base64)
                return Convert.ToBase64String(hash);
            if (encoding == HashStringEncoding.Base64Url)
                return Base64Url.Encode(hash);
            throw new ArgumentException("Invalid encoding");
        }
    }

    public enum HashStringEncoding
    {
        Base64,
        Base64Url,
    }
}
