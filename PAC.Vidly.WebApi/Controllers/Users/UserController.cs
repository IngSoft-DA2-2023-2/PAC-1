using Microsoft.AspNetCore.Mvc;
using PAC.Vidly.WebApi.Controllers.Movies.Models;
using PAC.Vidly.WebApi.Controllers.Users.Models;
using PAC.Vidly.WebApi.Services.Users;

namespace PAC.Vidly.WebApi.Controllers.Users
{


    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController (IUserService userService)
        {
            _userService = userService;
        }
        
        [HttpPost("users")] 
        public CreateUserRequest Create(CreateUserRequest? request)
        {
            if(request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            
            var token = _userService.Create(request.Email, request.Password);

            return token;
        }       
        
    }
}