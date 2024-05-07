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

        public string Create(CreateMovieArgs args)
        {
            var existingMovie = _movieRepository.GetOrDefault(movie => movie.Name == args.Movie.Name && movie.Creator.Name == args.Movie.Creator.Name);
            if (existingMovie == null)
            {
               throw new InvalidOperationException("Movie already exists");
            }
            _movieRepository.Add(args.Movie);
            return args.Movie.Id;
        }

        public List<Movie> GetAll()
        {
            return _movieRepository.GetAll();
        }
    }
}
