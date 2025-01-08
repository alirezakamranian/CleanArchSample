using Application.UtilityServicesAbstractions;
using Domain.Common.Constants;
using Infrastructure.DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.UtilityServices
{
    public class UserManager(DataContext context) : IUserManager
    {
        private readonly DataContext _context = context;

        public async Task<bool> CheckRole(string userId, UserRoles requierdRole)
        {
            var user = await _context.Users.AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id.ToString().Equals(userId));

            if (user.Role.Equals(requierdRole))
                return true;

            return false;
        }
    }
}
