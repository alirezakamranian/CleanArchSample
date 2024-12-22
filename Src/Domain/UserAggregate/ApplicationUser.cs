using Domain.Common;
using Domain.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UserAggregate
{
    public class ApplicationUser : BaseEntity
    {
        private ApplicationUser() { }
        public ApplicationUser(string userName, PhoneNumber phoneNumber)
        {
            UserName = userName ?? throw new ArgumentNullException(nameof(UserName));
            PhoneNumber = phoneNumber ?? throw new ArgumentNullException(nameof(PhoneNumber));
            Role = UserRoles.Basic;
        }

        public string UserName { get; private set; }
        public PhoneNumber PhoneNumber { get; private set; }
        public UserRoles Role { get; private set; }

        public void UpdateRole(UserRoles role) => 
            Role = role;

        public void UpdateUserName(string userName) => 
            UserName = userName ?? throw new ArgumentNullException(nameof(UserName));

        public void UpdatePhone(PhoneNumber phoneNumber) => 
            PhoneNumber = phoneNumber ?? throw new ArgumentNullException(nameof(UserName));
    }
}
