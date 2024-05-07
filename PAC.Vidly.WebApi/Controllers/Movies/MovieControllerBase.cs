using Microsoft.AspNetCore.Mvc;
using PAC.Vidly.WebApi.Services.Users.Entities;

namespace PAC.Vidly.WebApi.Controllers.Movies;

public class MovieControllerBase : ControllerBase
{
    protected User GetUserLogged()
    {
        var userLogged = HttpContext.Items[Items.UserLogged];

        var userLoggedMapped = (User)userLogged;

        return userLoggedMapped;
    }
}