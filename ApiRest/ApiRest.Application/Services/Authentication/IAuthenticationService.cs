using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ApiRest.Application.Services.Authentication
{
    public interface IAuthenticationService
    {
        AuthenticationResult Register(string firstName,string LastName,string email,string token);

        AuthenticationResult Login (string email, string password);
    }
}
