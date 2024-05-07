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
            if(string.IsNullOrEmpty(movie.Name))
                throw new ArgumentNullException("Name cannot be empty or null");
            
            var movieExists = _movieRepository.Exist(i => i.Name == movie.Name);
            if (movieExists)
                throw new ArgumentNullException ("Movie already exists");

            _movieRepository.Add(movie);

            return movie;
        }

        public List<Movie> GetAll()
        {
            return _movieRepository.GetAll();
        }
    }
}
