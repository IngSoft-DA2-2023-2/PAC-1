using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PAC.Vidly.WebApi.Filters
{
    public sealed class ExceptionFilter : IExceptionFilter
    {
        private readonly Dictionary<Type, Func<Exception, IActionResult>>
            _errors = new Dictionary<Type, Func<Exception, IActionResult>>
            {
                {
                    typeof(Exception), exception =>
                        new ObjectResult(new
                        {
                            Code = "InternalError",
                            Message = exception.Message,
                            DeveloperMessage = exception.GetType().ToString()
                        })
                        {
                            StatusCode = (int)HttpStatusCode.BadRequest
                        }
                }
            };

        public void OnException(ExceptionContext context)
        {
            Type exceptionType = context.Exception.GetType();
            Func<Exception, IActionResult>? responseBuilder = _errors.GetValueOrDefault(exceptionType);

            if (responseBuilder == null)
            {
                context.Result = new ObjectResult(new
                {
                    InnerCode = "InternalError",
                    Message = "There was an error while processing the request"
                })
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
                return;
            }

            context.Result = responseBuilder(context.Exception);
        }
    }
}