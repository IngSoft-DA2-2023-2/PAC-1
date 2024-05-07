using PAC.Vidly.WebApi.Controllers.Movies.Models;
using PAC.Vidly.WebApi.Services.Movies.Entities;

namespace PAC.Vidly.WebApi.Services.Movies
{
    public interface IMovieService
    {
        string Create(CreateMovieRequest movie, string userLoggedId);

        List<Movie> GetAll();
    }
}
