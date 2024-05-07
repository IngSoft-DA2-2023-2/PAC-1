using  Microsoft.AspNetCore.Mvc;
using PAC.Vidly.WebApi.Services.Users;

using PAC.Vidly.WebApi.Controllers.Movies;
using PAC.Vidly.WebApi.Services.Movies;
using PAC.Vidly.WebApi.Controllers.Users.Models;

namespace PAC.Vidly.WebApi.Controllers.Users
{
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public CreateUserResponse Create(CreateUserRequest request)
        {
            var userDto = new UserDto(request);
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            var movie = _userService.Create(userDto);
            var response = new CreateUserResponse(movie);
            return response;
            
        }

    }
}