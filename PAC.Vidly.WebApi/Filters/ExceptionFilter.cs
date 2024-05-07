using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PAC.Vidly.WebApi.Filters
{
    public sealed class ExceptionFilter : IExceptionFilter
    {
        private readonly Dictionary<Type, IActionResult> _errors = new Dictionary<Type, IActionResult>
        {
            {
                typeof(ArgumentNullException),
                new ObjectResult(new
                {
                    InnerCode = "InvalidArgument",
                    Message = "Argument can not be null or empty"
                })
                {
                    StatusCode = (int)HttpStatusCode.BadRequest
                } 
            }
        };

        public void OnException(ExceptionContext context)
        {
            var response = _errors.GetValueOrDefault(context.Exception.GetType());

            if (response == null)
            {
                context.Result = new ObjectResult(new
                {
                    InnerCode = "InternalError",
                    Message = "There was an error when processing the request"
                })
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
                return;
            }

            context.Result = response;
        }
    }
}