using PAC.Vidly.WebApi.Services.Movies.Entities;

namespace PAC.Vidly.WebApi.Services.Movies
{
    public interface IMovieService
    {
        Movie Create(CreateMovieArgs args, string userLoggedId);

        List<Movie> GetAll();
    }
}
