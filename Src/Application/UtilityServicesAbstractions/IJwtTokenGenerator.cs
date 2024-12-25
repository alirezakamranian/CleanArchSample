using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.UtilityServicesAbstractions
{
    public interface IJwtTokenGenerator
    {
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        public JwtSecurityToken CreateToken(List<Claim> authClaims);
        public string GenerateRefreshToken();
    }
}
