using Microsoft.AspNetCore.Mvc.Filters;

namespace PAC.Vidly.WebApi.Filters;

public class ExceptionFilter : Attribute, IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        int statusCode = 500;
        ResponseDTO response = new()
        {
            Code = "InternalError",
            Message = "Ocurrio un error, intente mas tarde.",
            DeveloperMessage = context.Exception.Message
        };    
    }
}

public class ResponseDTO
{
    public object Content { get; set; }
    public string Code { get; set; }
    public string Message { get; set; }
    public string DeveloperMessage { get; set; }
}