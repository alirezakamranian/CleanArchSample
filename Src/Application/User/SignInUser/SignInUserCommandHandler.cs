using Application.Common.Abstractions;
using Application.UtilityServicesAbstractions;
using Domain.UserAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.SignInUser
{
    public class SignInUserCommandHandler(IDataContext context, IPasswordHasher passwordHasher,
        IJwtTokenGenerator jwtTokenGenerator) : IRequestHandler<SignInUserCommand, SignInUserCommandResponse>
    {
        private readonly IDataContext _context = context;
        private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;
        private readonly IPasswordHasher _passwordHasher = passwordHasher;
        public async Task<SignInUserCommandResponse> Handle(SignInUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .AsNoTracking().FirstOrDefaultAsync(u => 
                    u.UserName.Equals(request.UserName), 
                        cancellationToken: cancellationToken);

            if (user == null || !_passwordHasher.VerifyPassword(
                request.Password, user.PasswordHash, user.HashSalt))
                    throw new InvalidUserCredentialsException();

            var authClaims = new List<Claim>
            {
                new("Id", user.Id.ToString())
            };

            var token = _jwtTokenGenerator
                .CreateToken(authClaims);

            var accesstoken = new JwtSecurityTokenHandler()
                .WriteToken(token);

            var refreshToken = _jwtTokenGenerator
                .GenerateRefreshToken();

            await _context.Reference(user, nameof(user.RefreshToken))
                .LoadAsync(cancellationToken);

            if (user.RefreshToken != null)
                _context.RemoveRecord<UserRefreshToken>(user.RefreshToken);

            _context.RefreshTokens
                .Add(new(refreshToken, user.Id));

            await _context.SaveChangesAsync(cancellationToken);

            return new SignInUserCommandResponse(accesstoken, refreshToken);
        }
    }
}
