﻿using Application.Common.Abstractions;
using Application.UtilityServicesAbstractions;
using Domain.Common.Constants;
using Domain.UserAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UserUsecases.CreateUser
{
    public class CreateUserCommandHandler(
        IDataContext context, IPasswordHasher passwordHasher) : IRequestHandler<CreateUserCommand, CreateUserCommandResponse>
    {
        private readonly IDataContext _context = context;
        private readonly IPasswordHasher _passwordHasher = passwordHasher;
        public async Task<CreateUserCommandResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            string hashSalt;
            if (_context.Users.Any(u =>
                u.UserName.Equals(request.UserName.ToLower())))
                throw new UserAlredyExistsException();

            var user = new ApplicationUser(
               request.UserName, new PhoneNumber(request.Phone),
                _passwordHasher.HashPassword(request.Password, out hashSalt), hashSalt);

            await _context.Users.AddAsync(user, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return new CreateUserCommandResponse(user.Id.ToString());
        }
    }
}
