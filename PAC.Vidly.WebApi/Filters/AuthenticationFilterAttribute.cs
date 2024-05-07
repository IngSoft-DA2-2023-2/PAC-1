using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PAC.Vidly.WebApi.Services.Sessions;
using PAC.Vidly.WebApi.Services.Users.Entities;

namespace PAC.Vidly.WebApi.Filters
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthenticationFilterAttribute : Attribute, IAuthorizationFilter
    {
        private const string AUTHORIZATION_HEADER = "Authorization";

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var authorizationHeader = context.HttpContext.Request.Headers[AUTHORIZATION_HEADER];

            if (string.IsNullOrEmpty(authorizationHeader))
            {
                context.Result = new ObjectResult(new
                {
                    InnerCode = "Unauthenticated",
                    Message = "You are not authenticated"
                })
                {
                    StatusCode = (int)HttpStatusCode.Unauthorized
                };
                return;
            }

            var user = GetUserOfAuthorization(authorizationHeader, context);
            context.HttpContext.Items[Items.UserLogged] = user;
            if (user == null)
            {
                context.Result = new ObjectResult(new
                {
                    InnerCode = "Unauthenticated",
                    Message = "You are not authenticated"
                })
                {
                    StatusCode = (int)HttpStatusCode.Unauthorized
                };
            }
        }

        private User? GetUserOfAuthorization(string authorization, AuthorizationFilterContext context)
        {
            var sessionService = context.HttpContext.RequestServices.GetService<ISessionService>();
            User user = sessionService.GetUserByToken(authorization);
            return user;
        }
    }
}