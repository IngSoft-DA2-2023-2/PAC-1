using Microsoft.AspNetCore.Authentication;

namespace PAC.Vidly.WebApi.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

public class AuthenticationFilter : Attribute, IAuthorizationFilter
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationFilter(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (!context.HttpContext.Request.Headers.TryGetValue("Authorization", out var authorizationHeader))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var token = authorizationHeader.ToString().Replace("Bearer ", "");

        var authenticatedUser = _authenticationService.Equals(token);

        if (authenticatedUser == null)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        context.HttpContext.Items["User"] = authenticatedUser;
    }
}
