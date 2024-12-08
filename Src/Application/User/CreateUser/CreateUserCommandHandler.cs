using Application.Common.Abstractions;
using Domain.Constants;
using Domain.UserAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.CreateUser
{
    public class CreateUserCommandHandler(
        IDataContext context) : IRequestHandler<CreateUserCommand, CreateUserCommandResponse>
    {
        private readonly IDataContext _context = context;
        public async Task<CreateUserCommandResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (_context.Users.Any(u => 
                u.UserName.Equals(request.UserName.ToLower())))
                    throw new UserAlredyExistsException();

            var user = new ApplicationUser()
            {
                PhoneNumber = request.Phone,
                UserName = request.UserName.ToLower(),
                Role = UserRoles.Basic
            };

            await _context.Users.AddAsync(user);

            await _context.SaveChangesAsync(cancellationToken);

            return new CreateUserCommandResponse(user.Id.ToString());
        }
    }
}
