using ApiRest.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRest.Application.Services.Authentication.Common
{
    public record AuthenticationResult(
        User user,
        string Token
    );
}
