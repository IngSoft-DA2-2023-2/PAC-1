using PAC.Vidly.WebApi.Services.Users.Entities;

namespace PAC.Vidly.WebApi.Controllers.Movies.Models;

public class CreateMovieArgs
{
    public readonly string Name;
    public CreateMovieArgs(string name)
    {
        if (string.IsNullOrEmpty(name)) throw new Exception("Name cannot be empty or null");
        if (name.Length > 100) throw new Exception("Name cannot have more than 100 characters");
        Name = name;
    }
}