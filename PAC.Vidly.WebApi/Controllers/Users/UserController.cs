using Microsoft.AspNetCore.Mvc;
using PAC.Vidly.WebApi.Controllers.Users.Models;
using PAC.Vidly.WebApi.Services.Users;
using PAC.Vidly.WebApi.Services.Users.Entities;

namespace PAC.Vidly.WebApi.Controllers.Users;

public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public CreateUserResponse Create(CreateUserRequest? request)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }
        if(request.Name == null)
        {
            throw new ArgumentNullException("Name cannot be empty");
        }
        if (_userService.IsUserExist(request.Name)){
            throw new Exception("User already exists");
        }
        _userService.Create(new User
        {
            Id = Guid.NewGuid().ToString(),
            Name = request.Name,
            Email = request.Email,
            Password = request.Password
        });
        
        return new CreateUserResponse
        {
            Id = Guid.NewGuid().ToString(),
            Name = request.Name,
            Email = request.Email,
            Password = request.Password
        };
    }
    
}