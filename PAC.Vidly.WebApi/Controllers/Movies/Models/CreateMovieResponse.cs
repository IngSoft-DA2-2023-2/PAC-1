using PAC.Vidly.WebApi.Services.Movies.Entities;

namespace PAC.Vidly.WebApi.UnitTests.Controllers.Models;

public sealed record class CreateMovieResponse
{
    public string Id { get; set; }
    public string Name { get; set; }
    
    public CreateMovieResponse(Movie m)
    {
        Id = m.Id;
        Name = m.Name;
    }
}