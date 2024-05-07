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

        public virtual void OnAuthorization(AuthorizationFilterContext context)
        {
            var authorizationHeader = context.HttpContext.Request.Headers[AUTHORIZATION_HEADER];

            if (string.IsNullOrEmpty(authorizationHeader))
            {
                context.Result = new ObjectResult(new
                    {
                        InnerCode = "Unauthenticated",
                        Message = "You are not authenticated"
                    })
                    { StatusCode = (int)HttpStatusCode.Unauthorized };
                return;
            }

            var isAuthorizationFormatNotValid = !IsAuthorizationFormatValid(authorizationHeader);
            if (isAuthorizationFormatNotValid)
            {
                context.Result = new ObjectResult(new
                    {
                        InnerCode = "InvalidAuthorization",
                        Message = "The provided authorization header format is invalid"
                    })
                    { StatusCode = (int)HttpStatusCode.Unauthorized };
                return;
            }


            var userOfAuthorization = GetUserOfAuthorization(authorizationHeader, context);
            context.HttpContext.Items[Items.UserLogged] = userOfAuthorization;
            if (userOfAuthorization == null)
            {
                context.Result = new ObjectResult(new
                    {
                        InnerCode = "Unauthenticated",
                        Message = "You are not authenticated"
                    })
                    { StatusCode = (int)HttpStatusCode.Unauthorized };
            }
        }

        private bool IsAuthorizationFormatValid(string authorization)
        {
            if (!Guid.TryParse(authorization, out _)) return false;

            return true;
        }

        private User GetUserOfAuthorization(string authorization, AuthorizationFilterContext context)
        {
            var sessionService = context.HttpContext.RequestServices.GetRequiredService<ISessionService>();

            var sesion = sessionService.GetUserByToken(authorization);

            if (sesion == null) return null;

            return sesion;
        }
    }
}