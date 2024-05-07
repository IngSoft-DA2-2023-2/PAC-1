using PAC.Vidly.WebApi.Services.Movies.Entities;

namespace PAC.Vidly.WebApi.Services.Movies
{
    public interface IMovieService
    {
        string Create(CreateMovieArgs args);

        List<Movie> GetAll();
    }
}
