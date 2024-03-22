using ApiRest.Application.Services.Authentication.Common;
using ErrorOr;
using MediatR;
using MediatR.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRest.Application.Authentication.Queries.Login
{
    public record LoginQuery( string Email, string Password): IRequest<ErrorOr<AuthenticationResult>>;
    
}
