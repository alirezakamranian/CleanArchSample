using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UtilityServicesAbstractions
{
    public interface IPasswordHasher
    {
        byte[] GenerateSalt(int length);
        string HashPassword(string password, out string salt);
        bool VerifyPassword(string password, string hashedPassword, string salt);
    }
}
