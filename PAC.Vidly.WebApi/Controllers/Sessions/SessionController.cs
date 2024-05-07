using Domain;
using Microsoft.AspNetCore.Mvc;
using PAC.Vidly.WebApi.Controllers.Sessions.Models;
using PAC.Vidly.WebApi.Services.Sessions;

namespace PAC.Vidly.WebApi.Controllers.Sessions
{
    [ApiController]
    [Route("login")]
    public sealed class SessionController : ControllerBase
    {
        private readonly ISessionService _sessionService;

        public SessionController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        [HttpPost("sessions")]
        public string Create(CreateSessionRequest? request)
        {
            if(request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var token = _sessionService.Create(request.Email, request.Password);

            return token;
        }
    }
}
