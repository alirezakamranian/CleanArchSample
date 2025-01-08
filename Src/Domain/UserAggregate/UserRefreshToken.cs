using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UserAggregate
{
    public class UserRefreshToken
    {
        public UserRefreshToken() { }
        public UserRefreshToken(string refreshToken,Guid id)
        {
            Id=id;
            RefreshToken = refreshToken ?? throw new ArgumentNullException(nameof(RefreshToken));
            ExpireDate = DateTime.UtcNow.AddDays(10);

        }

        public Guid Id { get; set; }
        public string RefreshToken { get; set; } 
        public DateTime ExpireDate { get; set; }
        public ApplicationUser User { get; set; } 

        public static UserRefreshToken Create() => new();
    }
}
