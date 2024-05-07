using PAC.Vidly.WebApi.Services.Movies.Entities;

namespace PAC.Vidly.WebApi.Controllers.Movies.Models;

public class GetMoviesResponse
{
    public List<Movie> Movies { get; init; }

    public GetMoviesResponse(List<Movie> movies)
    {
        Movies = movies;
    }
}