using PAC.Vidly.WebApi.Controllers.Movies.Models;
using PAC.Vidly.WebApi.DataAccess;
using PAC.Vidly.WebApi.Services.Movies.Entities;

namespace PAC.Vidly.WebApi.Services.Movies
{
    public sealed class MovieService : IMovieService
    {
        private readonly IRepository<Movie> _movieRepository;

        public MovieService(IRepository<Movie> movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public Movie Create(CreateMovieArguments movie, string userLoggedId)
        {
            if (movie == null)
            {
                throw new ArgumentNullException(nameof(movie));
            }
            if (string.IsNullOrWhiteSpace(movie.Name))
            {
                throw new ArgumentNullException(nameof(movie.Name));
            }
            if (string.IsNullOrWhiteSpace(userLoggedId))
            {
                throw new ArgumentNullException(nameof(userLoggedId));
            }
            if (_movieRepository.GetByName(movie.Name) != null)
            {
                throw new InvalidOperationException("Movie already exists, cant create duplicated one.");
            }   

            Movie movieCreated = new Movie(movie.Name, userLoggedId);
            return movieCreated;
        }

        public List<Movie> GetAll()
        {
            return _movieRepository.GetAll();
        }
    }
}
