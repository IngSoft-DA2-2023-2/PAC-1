using PAC.Vidly.WebApi.Services.Movies.Entities;

namespace PAC.Vidly.WebApi.Controllers.Users.Models;

public class CreateUserRequest
{
    public string Id { get; init; }

    public string? Name { get; init; }
    public string? Email { get; init; }
    public string? Password { get; init; }
    public List<Movie>? FavMovies { get; init; }
    


}