﻿using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;
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

        private User GetUserOfAuthorization(string authorization, AuthorizationFilterContext context)
        {
            var sessionService = context.HttpContext.RequestServices.GetRequiredService<ISessionService>();

            var user = sessionService.GetUserByToken(authorization);

            return user;
        }
    }
}