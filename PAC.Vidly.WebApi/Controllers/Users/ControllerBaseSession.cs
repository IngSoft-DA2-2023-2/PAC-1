using Microsoft.AspNetCore.Mvc;
using PAC.Vidly.WebApi.Services.Sessions;
using PAC.Vidly.WebApi.Services.Users.Entities;

namespace PAC.Vidly.WebApi.Controllers.Users;


    public class ControllerBaseSession : ControllerBase
    {
        private readonly ISessionService _sessionService;
        public ControllerBaseSession(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }
        protected User GetUserLogged(string token)
        {
            var session = _sessionService.GetUserByToken(token);
            
            return session;
        }
    }
