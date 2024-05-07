using PAC.Vidly.WebApi.Services.Movies.Arguments;
using PAC.Vidly.WebApi.Services.Movies.Entities;

namespace PAC.Vidly.WebApi.Services.Movies
{
    public interface IMovieService
    {
        Movie Create(CreateMovieArgs movie);

        List<Movie> GetAll();
        bool IsMovieExist(string requestName);
    }
}
