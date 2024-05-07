﻿using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PAC.Vidly.WebApi.Services.Sessions;
using PAC.Vidly.WebApi.Services.Users.Entities;
using PAC.Vidly.WebApi;


namespace BuildingManager.WebApi.Filters
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthenticationFilterAttribute : Attribute, IAuthorizationFilter
    {
        private const string AuthorizationHeader = "Authorization";

        public virtual void OnAuthorization(AuthorizationFilterContext context)
        {
            var authorizationHeader = context.HttpContext.Request.Headers[AuthorizationHeader];

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

            var isAuthorizationFormatNotValid = !IsAuthorizationFormatValid(authorizationHeader);
            if (isAuthorizationFormatNotValid)
            {
                context.Result = new ObjectResult(
                    new
                    {
                        InnerCode = "InvalidAuthorization",
                        Message = "The provided authorization header format is invalid"
                    })
                {
                    StatusCode = (int)HttpStatusCode.Unauthorized
                };
                return;
            }

            var isAuthorizationExpired = IsAuthorizationExpired(authorizationHeader);
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

        private bool IsAuthorizationFormatValid(string authorization)
        {
            return true;
        }

        private bool IsAuthorizationExpired(string authorization)
        {
            return false;
        }

        private User GetUserOfAuthorization(string authorization, AuthorizationFilterContext context)
        {
            var sessionService = context.HttpContext.RequestServices.GetRequiredService<ISessionService>();

            var user = sessionService.GetUserByToken(authorization);

            return user;
        }
    }
}