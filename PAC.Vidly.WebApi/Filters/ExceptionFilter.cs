using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BuildingManager.WebApi.Filters
{
    public sealed class ExceptionFilter : IExceptionFilter
    {
        private readonly Dictionary<Type, IActionResult> _errors = new Dictionary<Type, IActionResult>
        {
            {
                typeof(ArgumentNullException),
                new ObjectResult(new
                {
                    InnerCode = "InternalError",
                    Message = "Ocurrio un error, intente mas tarde.",
                    DeveloperMessage = "ArgumentNullException"
                })
                {
                    StatusCode = (int)HttpStatusCode.BadRequest
                }
            }
        };

    }
}