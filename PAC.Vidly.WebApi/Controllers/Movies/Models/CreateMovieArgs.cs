using PAC.Vidly.WebApi.Services.Users.Entities;

namespace PAC.Vidly.WebApi.Controllers.Movies.Models;

public class CreateMovieArgs
{
    public readonly string Name;
    public CreateMovieArgs(string name)
    {
        if (string.IsNullOrEmpty(name)) throw new Exception("Name cannot be empty or null");
        Name = name;
    }
}