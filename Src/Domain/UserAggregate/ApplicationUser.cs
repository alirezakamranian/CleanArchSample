using Domain.Common;
using Domain.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UserAggregate
{
    public class ApplicationUser : BaseEntity
    {
        public string UserName { get; set; }
        public PhoneNumber PhoneNumber { get; set; }
        public UserRoles Role { get; set; }
    }
}
