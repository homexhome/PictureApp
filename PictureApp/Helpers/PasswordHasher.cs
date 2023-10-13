using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PictureApp.Helpers
{
    public static class PasswordHasher
    {
        public static string HashPassword(string password, string salt) {
            using (SHA256 sha256 = SHA256.Create()) {
                string saltedPassword = password + salt;

                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));

                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes) {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public static string GenerateRandomSalt() {
            const int saltLength = 16;
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, saltLength)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
