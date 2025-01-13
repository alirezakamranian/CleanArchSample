using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Application.UtilityServicesAbstractions;
using Konscious.Security.Cryptography;

namespace Infrastructure.UtilityServices
{
    public class PasswordHasher : IPasswordHasher
    {
        public string HashPassword(string password, out string salt)
        {
            byte[] saltBytes = GenerateSalt(16);
            salt = Convert.ToBase64String(saltBytes);

            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

            using var argon2 = new Argon2id(passwordBytes);
            argon2.Salt = saltBytes;
            argon2.DegreeOfParallelism = 8;
            argon2.MemorySize = 65536;
            argon2.Iterations = 10;

            byte[] hashBytes = argon2.GetBytes(32);
            return Convert.ToBase64String(hashBytes);
        }

        public bool VerifyPassword(string password, string hashedPassword, string salt)
        {
            byte[] saltBytes = Convert.FromBase64String(salt);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

            using var argon2 = new Argon2id(passwordBytes);
            argon2.Salt = saltBytes;
            argon2.DegreeOfParallelism = 8;
            argon2.MemorySize = 65536;
            argon2.Iterations = 10;

            byte[] hashBytes = argon2.GetBytes(32);
            string newHash = Convert.ToBase64String(hashBytes);

            return newHash == hashedPassword;
        }

        private byte[] GenerateSalt(int length)
        {
            var salt = new byte[length];

            using var rng = System.Security.Cryptography
                .RandomNumberGenerator.Create();

            rng.GetBytes(salt);

            return salt;
        }
    }
}
