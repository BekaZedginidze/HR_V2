using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace HR.Application.Service.Helpers
{
    public static class PasswordHash
    {

        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

                byte[] hashedBytes = sha256.ComputeHash(passwordBytes);

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashedBytes.Length; i++)
                {
                    builder.Append(hashedBytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }
    }
}
