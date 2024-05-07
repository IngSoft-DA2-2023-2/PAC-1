using PAC.Vidly.WebApi.Controllers.Movies.Models;
using PAC.Vidly.WebApi.Services.Movies.Entities;

namespace PAC.Vidly.WebApi.Services.Movies
{
    public interface IMovieService
    {
        void Create(Movie movie, string userLoggedId);

        List<MovieBasicInfoResponse> GetAll();
    }
}
