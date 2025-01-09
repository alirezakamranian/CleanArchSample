using Domain.ArticleAggregate;
using Domain.Common;
using Domain.Common.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UserAggregate
{
    public class ApplicationUser : BaseEntity
    {
        public ApplicationUser() { }
        public ApplicationUser(string userName, PhoneNumber phoneNumber, string passwordHash, string hashSalt)
        {
            UserName = userName ?? throw new ArgumentNullException(nameof(UserName));
            PhoneNumber = phoneNumber ?? throw new ArgumentNullException(nameof(PhoneNumber));
            PasswordHash = passwordHash ?? throw new ArgumentNullException(nameof(PasswordHash));
            HashSalt = hashSalt ?? throw new ArgumentNullException(nameof(HashSalt));
            Role = UserRoles.Basic;
        }

        public string UserName { get; private set; } 
        public PhoneNumber PhoneNumber { get; private set; }
        public UserRoles Role { get; private set; }
        public string PasswordHash { get; private set; } 
        public string HashSalt { get; private set; }

        public UserRefreshToken RefreshToken { get; private set; } 
        public List<Article> Articles { get; private set; } = [];

        public void UpdateRole(UserRoles role) =>
            Role = role;

        public void UpdateUserName(string userName) =>
            UserName = userName ?? throw new ArgumentNullException(nameof(UserName));

        public void UpdatePhone(PhoneNumber phoneNumber) =>
            PhoneNumber = phoneNumber ?? throw new ArgumentNullException(nameof(UserName));

        public void UpdatePassword(string newPasswordHash) =>
            PasswordHash = newPasswordHash ?? throw new ArgumentNullException(nameof(PasswordHash));
    }
}
