using PAC.Vidly.WebApi.Services.Movies.Entities;

namespace PAC.Vidly.WebApi.Controllers.Movies.Models;

public class CreateMovieResponse
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string CreatorId { get; set; } = null!;

    public CreateMovieResponse(Movie movie)
    {
        Id = Guid.NewGuid().ToString();
        Name = movie.Name;
        CreatorId = movie.CreatorId;
    }

}