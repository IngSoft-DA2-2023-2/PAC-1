using PAC.Vidly.WebApi.Services.Movies.Entities;

namespace PAC.Vidly.WebApi.Controllers.Movies.Models;

public class CreateMovieResponse
{
    public string Id { get; set; }

    public CreateMovieResponse(Movie movie)
    {
        Id = movie.Id;
    }
}