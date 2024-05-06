using PAC.Vidly.WebApi.Services.Movies.Entities;

namespace PAC.Vidly.WebApi.Services.Movies
{
    public interface IMovieService
    {
        void Create(Movie movie, string userLoggedId);

        List<Movie> GetAll();
    }
}
