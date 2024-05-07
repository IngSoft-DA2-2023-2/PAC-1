using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using PAC.Vidly.WebApi.Services.Sessions;
using PAC.Vidly.WebApi.Services.Users.Entities;
using PAC.Vidly.WebApi;
using System.Net;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthenticationFilterAttribute : Attribute, IAuthorizationFilter
{
    private const string AUTHORIZATION_HEADER = "Authorization";
    public virtual void OnAuthorization(AuthorizationFilterContext context)
    {
        var authorizationHeader = context.HttpContext.Request.Headers[AUTHORIZATION_HEADER];

        if (string.IsNullOrEmpty(authorizationHeader))
        {
            context.Result = new ObjectResult
            (new
            {
                InnerCode = "Unauthenticated",
                Message = "You are not authenticated"
            })
            {
                StatusCode = (int)HttpStatusCode.Unauthorized
            };
            return;
        }
        var isAuthorizationExpired = IsAuthorizationExpired(authorizationHeader, context);
        if (isAuthorizationExpired)
        {
            context.Result = new ObjectResult(
                new
                {
                    InnerCode = "ExpiredAuthorization",
                    Message = "The provided authorization header is expired"
                })
            {
                StatusCode = (int)HttpStatusCode.Unauthorized
            };
            return;
        }

        try
        {
            var userOfAuthorization = GetUserOfAuthorization(authorizationHeader, context);

            context.HttpContext.Items[Items.UserLogged] = userOfAuthorization;
        }
        catch (Exception)
        {
            context.Result = new ObjectResult(new
            {
                InnerCode = "InternalError",
                Message = "An error ocurred while processing the request"
            })
            {
                StatusCode = (int)HttpStatusCode.InternalServerError
            };
        }
    }

    private bool IsAuthorizationExpired(StringValues authorizationHeader, AuthorizationFilterContext context)
    {
        var sessionService =
            context.HttpContext.RequestServices.GetRequiredService<ISessionService>();
        bool isExpired = sessionService.IsAuthorizationExpired(authorizationHeader);
        return isExpired;
    }
    private User GetUserOfAuthorization(string authorization, AuthorizationFilterContext context)
    {
        var sessionService = context.HttpContext.RequestServices.GetRequiredService<ISessionService>();

        var user = sessionService.GetUserByToken(authorization);

        return user;
    }
}