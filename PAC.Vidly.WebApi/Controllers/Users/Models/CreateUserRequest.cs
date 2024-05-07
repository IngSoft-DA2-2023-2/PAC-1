using PAC.Vidly.WebApi.Services.Movies.Entities;

namespace PAC.Vidly.WebApi.Controllers.Users.Models;

public sealed record class CreateUserRequest
{
    public string Name { get; set; }
    
    public string Email { get; set; }
    
    public string Password { get; set; }
    
    public List<Movie>? FavoriteMovies { get; set; }

    public CreateUserRequest(string name, string email, string password, List<Movie>? list)
    {
        Name = name;
        Email = email;
        Password = password;
        FavoriteMovies = list;
    }
}