using ApiRest.Application.Services.Authentication.Common;
using ErrorOr;
using MediatR;
using MediatR.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRest.Application.Authentication.Command.Register
{
    public record RegisterCommand(string FirstName, string LastName, string Email, string Password): IRequest<ErrorOr<AuthenticationResult>>;
    
}
