using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PAC.Vidly.WebApi.Services.Sessions;

namespace PAC.Vidly.WebApi.Filters;

public class AuthenticationFilter: Attribute, IAuthorizationFilter
{
    private readonly ISessionService _sessionService;
    public AuthenticationFilter(ISessionService sessionService)
    {
        _sessionService = sessionService;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var authorizationHeader = context.HttpContext.Request.Headers["Authorization"].ToString();
        Guid token = Guid.Empty;

        if (string.IsNullOrEmpty(authorizationHeader) || !Guid.TryParse(authorizationHeader, out token))
        {
            context.Result = new ObjectResult(new { Message = "Falta el encabezado de autorización" })
            {
                StatusCode = 401
            };
        }
        else
        {
            var currentUser = _sessionService.GetCurrentUser(token);

            if (currentUser == null)
            {
                context.Result = new ObjectResult(new { Message = "No autorizado" })
                {
                    StatusCode = 403
                };
            }
        }
    }
}