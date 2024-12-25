﻿using Domain;
using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.SignInUser
{
    public class InvalidUserCredentialsException() : DomainException("InvalidUserCredentials");
}
