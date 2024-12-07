using Domain.Common;
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
        public string PhoneNumber { get; set; }
        public int Role { get; set; }
    }
}
