using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PAC.Vidly.WebApi.Filters;

public class ExceptionFilterAttribute
{
    public sealed class ExceptionFilter : IExceptionFilter
    {
        private readonly Dictionary<Type, IActionResult> _errors = new Dictionary<Type, IActionResult>
        {
            {
                typeof(ArgumentNullException),
                new ObjectResult(new
                {
                    Code = "400",
                    Message = "InvalidArgument",
                    DeveloperMessage = "Argument can not be null or empty"
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
                    Code = "500",
                    Message = "InternalError",
                    DeveloperMessage = "There was an error when processing the request"
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