using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace ApiRest.Domain.Entities
{
    public record RegisterRequest
    (
        string FirstName ,
        string LastName ,
        string Email,
        string Password        
    );
}
