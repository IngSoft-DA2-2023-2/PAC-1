using PAC.Vidly.WebApi.Services.Movies.Entities;

namespace PAC.Vidly.WebApi.Services.Movies;

public class CreateMovieArgs
{
    public Movie Movie { get; init; } = null!;
    public string UserLoggedId { get; init; } = null!;
    
    public CreateMovieArgs(Movie movie, string userLoggedId)
    {
        Movie = movie;
        UserLoggedId = userLoggedId;
    }
    
}