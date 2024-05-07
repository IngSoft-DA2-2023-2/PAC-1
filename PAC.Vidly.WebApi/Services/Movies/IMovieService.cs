using PAC.Vidly.WebApi.Controllers.Movies.Models;
using PAC.Vidly.WebApi.Services.Movies.Entities;
using PAC.Vidly.WebApi.Services.Users.Entities;

namespace PAC.Vidly.WebApi.Services.Movies
{
    public interface IMovieService
    {
        string Create(CreateMovieArgs movie, User userLogged);

        List<MovieBasicInfoResponse> GetAll();
    }
}
