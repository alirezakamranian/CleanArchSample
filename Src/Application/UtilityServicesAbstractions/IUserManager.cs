using Domain.Common.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UtilityServicesAbstractions
{
    public interface IUserManager
    {
        Task<bool> CheckRole(string userId,UserRoles requierdRole);
    }
}
