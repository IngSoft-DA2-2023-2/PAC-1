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
            if (existingMovie != null)
            {
               throw new InvalidOperationException("Movie already exists");
            }
            if(args.Movie.CreatorId != args.UserLoggedId)
            {
                throw new InvalidOperationException("User is not the creator of the movie");
            }
            if (string.IsNullOrEmpty(args.Movie.Name))
            {
                throw new ArgumentNullException("Name cannot be empty or null");
            }
            var existsName = _movieRepository.GetOrDefault(movie => movie.Name == args.Movie.Name);
            if (existsName != null)
            {
                throw new InvalidOperationException("Movie name must be unique");
            }
            if(args.Movie.Name.Length > 100)
            {
                throw new InvalidOperationException("Movie name must be up to 100 characters");
            }
            
            var movieToAdd = new Movie(args.Movie.Name, args.UserLoggedId);
            _movieRepository.Add(movieToAdd);
            return movieToAdd.Id;
        }

        public List<Movie> GetAll()
        {
            return _movieRepository.GetAll();
        }
    }
}
