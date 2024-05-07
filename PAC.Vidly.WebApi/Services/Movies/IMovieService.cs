using PAC.Vidly.WebApi.Controllers.Movies.Models;
using PAC.Vidly.WebApi.Services.Movies.Entities;

namespace PAC.Vidly.WebApi.Services.Movies
{
    public interface IMovieService
    {
        Movie Create(CreateMovieRequest? movie, string userToken);

        List<Movie> GetAll();

        Movie GetById(string id);
    }
}
