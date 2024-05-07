using Microsoft.AspNetCore.Mvc;
using PAC.Vidly.WebApi.Services.Movies;
using PAC.Vidly.WebApi.Services.Movies.Entities;
using PAC.Vidly.WebApi.Services.Users;
using PAC.Vidly.WebApi.Services.Users.Entities;
 

namespace PAC.Vidly.WebApi.Controllers.User;

public class UserController
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public Services.Users.Entities.User Create(Services.Users.Entities.User? request)
    {
        var user = _userService.Create(request);
        return user;
    }
}