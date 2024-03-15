﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRest.Application.Services.Authentication
{
    internal class AuthenticationService : IAuthenticationService 
    {
        public AuthenticationResult Register(string firstName, string LastName, string email, string token)
        {
            return new AuthenticationResult(Guid.NewGuid(), firstName, LastName, email, "token");
        }

        public AuthenticationResult Login(string email, string password)
        {
            return new AuthenticationResult(Guid.NewGuid(), "firstName", "LastName", email, "token");
        }
    }
}