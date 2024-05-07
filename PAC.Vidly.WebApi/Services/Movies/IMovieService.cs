using PAC.Vidly.WebApi.Controllers.Movies.Models;
using PAC.Vidly.WebApi.Services.Movies.Entities;
using PAC.Vidly.WebApi.Services.Users.Entities;

namespace PAC.Vidly.WebApi.Services.Movies
{
    public interface IMovieService
    {
        Movie Create(CreateMovieArgs args);

        List<Movie> GetAll();
        User GetUserLogged(string email, string password);
    }
}
