using PAC.Vidly.WebApi.Services.Movies.Entities;

namespace PAC.Vidly.WebApi.Services.Movies
{
    public interface IMovieService
    {
        string Create(MovieArguments movie, string userLoggedId);

        List<Movie> GetAll();
    }
}
