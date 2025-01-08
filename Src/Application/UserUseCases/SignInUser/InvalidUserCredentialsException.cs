using Domain;
using Domain.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UserUsecases.SignInUser
{
    public class InvalidUserCredentialsException() : DomainException("InvalidUserCredentials");
}
