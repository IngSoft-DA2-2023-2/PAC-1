using PAC.Vidly.WebApi.DataAccess;
using PAC.Vidly.WebApi.Services.Movies.Arguments;
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

        public Movie Create(CreateMovieArgs movie)
        {
            var newMovie = new Movie(movie.Name, movie.CreatorId);
            _movieRepository.Add(newMovie);
            return newMovie;
        }

        public List<Movie> GetAll()
        {
            return _movieRepository.GetAll();
        }
        
        public bool IsMovieExist(string movieId)
        {
            return _movieRepository.GetAll().Any(x => x.Id == movieId);
        }
    }
}
