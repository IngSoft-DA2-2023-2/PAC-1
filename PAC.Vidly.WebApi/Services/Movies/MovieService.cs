using PAC.Vidly.WebApi.DataAccess;
using PAC.Vidly.WebApi.Services.Movies.Entities;
using PAC.Vidly.WebApi.Services.Users;
using PAC.Vidly.WebApi.Services.Users.Entities;

namespace PAC.Vidly.WebApi.Services.Movies
{
    public sealed class MovieService : IMovieService
    {
        private readonly IRepository<Movie> _movieRepository;

        private readonly IUserService _userService;

        public MovieService(IRepository<Movie> movieRepository, IUserService userService)
        {
            _movieRepository = movieRepository;
            _userService = userService;
        }

        public string Create(MovieArguments args, string userLoggedId)
        {
            User user = _userService.GetById(userLoggedId);

            var movie = new Movie
            {
                Name = args.Name,
                Creator = user,
                CreatorId = user.Id
            };

            List<Movie> movies = _movieRepository.GetAll();
            if (movies.Any(m => m.Name == movie.Name))
            {
                throw new InvalidOperationException("Movie already exists");
            }
            _movieRepository.Add(movie);
            return movie.Id;
        }

        public List<Movie> GetAll()
        {
            return _movieRepository.GetAll();
        }
    }
}
