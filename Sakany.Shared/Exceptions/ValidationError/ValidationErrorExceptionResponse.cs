using Sakany.Shared.Responses;
using System.Net;

namespace Sakany.Shared.Exceptions.ValidationError
{
    public class ValidationErrorExceptionResponse : Response<ValidationErrorExceptionResponse>
    {
        public List<string> Errors { get; set; }

        #region Constructors

        public ValidationErrorExceptionResponse()
        {
            Message = "Bad Request";
            StatusCode = HttpStatusCode.BadRequest;
            Succeeded = false;
            Errors = new List<string>();
        }

        #endregion Constructors
    }
}