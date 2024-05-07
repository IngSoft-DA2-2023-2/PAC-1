using Microsoft.AspNetCore.Mvc;
using PAC.Vidly.WebApi.Controllers.Users.Models;
using PAC.Vidly.WebApi.Filters;
using PAC.Vidly.WebApi.Services.Users;
using PAC.Vidly.WebApi.Services.Users.Entities;

namespace PAC.Vidly.WebApi.Controllers.Users
{
    [ApiController]
    [Route("users")]
    public sealed class UserController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("new")]
        [AuthenticationFilter]
        public CreateUserResponse Create(CreateUserRequest? request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            User userCreated = _userService.Create(request);
            CreateUserResponse response = new CreateUserResponse(userCreated.Id, userCreated.Name);

            return response;
        }

    }
}
