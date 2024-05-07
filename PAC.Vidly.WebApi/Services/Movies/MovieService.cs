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

        public Movie Create(Movie movie, string userLoggedId)
        {
            var addedMovie = new Movie()
            {
                Creator = movie.Creator,
                CreatorId = movie.CreatorId,
                Name = movie.Name
            };
            
            _movieRepository.Add(movie);

            return addedMovie;
        }

        public List<Movie> GetAll()
        {
            return _movieRepository.GetAll();
        }
    }
}
