using PAC.Vidly.WebApi.Services.Movies.Entities;
using PAC.Vidly.WebApi.Controllers.Movies;
namespace PAC.Vidly.WebApi.Services.Movies
{
    public interface IMovieService
    {
        Movie Create(MovieDto movie, string userLoggedId);

        List<Movie> GetAll();
    }
}
