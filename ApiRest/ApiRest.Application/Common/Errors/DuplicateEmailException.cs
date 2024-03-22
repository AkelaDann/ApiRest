//using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ApiRest.Application.Common.Errors
{
    public class DuplicateEmailException : Exception, IServiceException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.Conflict;

        public string ErrorMessage => "Email already exists.";
    }

    //public record struct DuplicateEmailError :IError
    //{
    //    //metodo oneof
    //    public HttpStatusCode StatusCode => HttpStatusCode.Conflict;
    //    public string ErrorMessage => "Email already exists.";
    //}

    
}
