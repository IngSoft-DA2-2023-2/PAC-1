using Microsoft.AspNetCore.Mvc;
using PAC.Vidly.WebApi.Controllers.Users.Models;
using PAC.Vidly.WebApi.Services.Sessions;
using PAC.Vidly.WebApi.Services.Users;
using PAC.Vidly.WebApi.Services.Users.Entities;

namespace PAC.Vidly.WebApi.Controllers.Users;

[ApiController]
[Route("users")]
public class UserController : VidlyControllerBase
{
    private readonly IUserService _userService;


    public UserController(IUserService userService, ISessionService sessionService)
        : base(sessionService)
    {
        _userService = userService;
    }
    
    [HttpPost("users")]

    public string Create(CreateUserRequest? request, [FromHeader] string? authorization)
    {
        if (string.IsNullOrEmpty(authorization))
            throw new ArgumentNullException(nameof(authorization));

        var userLogged = base.GetUserLogged(authorization);
        
        if (request == null)
        {
            throw new ArgumentNullException("InvalidRequest", "Request can not be null");
        }

        var userToSave = new User(
            request.Name,
            request.Email,
            request.Password
        );
        
        _userService.Add(userToSave);

        return userToSave.Id;
    }
}