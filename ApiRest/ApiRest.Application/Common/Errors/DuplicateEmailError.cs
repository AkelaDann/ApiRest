using ErrorOr;

namespace ApiRest.Application.Common.Errors
{
    public class DuplicateEmailError : IErrorOr
    {
        public List<Error> Reasons => throw new NotImplementedException();

        public Dictionary<string, object> Metadata => throw new NotImplementedException();

        public string Message => throw new NotImplementedException();

        public List<Error>? Errors => throw new NotImplementedException();

        public bool IsError => throw new NotImplementedException();
    }
}
