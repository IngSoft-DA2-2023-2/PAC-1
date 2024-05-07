using PAC.Vidly.WebApi.Services.Movies.Entities;

namespace PAC.Vidly.WebApi.Controllers.Movies.Models;

public sealed class CreateMovieResponse
{
    public string Id { get; init; }
    public CreateMovieResponse(Movie movie)
    {
        this.Id = movie.Id;
    }
}