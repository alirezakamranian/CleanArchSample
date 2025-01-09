using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UserAggregate
{
    public class UserRefreshToken
    {
        private UserRefreshToken() { }
        public UserRefreshToken(string refreshToken, Guid id)
        {
            Id = id;
            RefreshToken = refreshToken ?? throw new ArgumentNullException(nameof(RefreshToken));
            ExpireDate = DateTime.UtcNow.AddDays(10);

        }

        public Guid Id { get; private set; }
        public string RefreshToken { get; private set; }
        public DateTime ExpireDate { get; private set; }
        public ApplicationUser User { get; private set; }

        public static UserRefreshToken Create() => new();
    }
}
