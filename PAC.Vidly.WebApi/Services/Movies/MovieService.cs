using PAC.Vidly.WebApi.DataAccess;
using PAC.Vidly.WebApi.Services.Movies.Entities;
using PAC.Vidly.WebApi.Services.Sessions;
using PAC.Vidly.WebApi.Services.Users;
using PAC.Vidly.WebApi.Services.Users.Entities;

namespace PAC.Vidly.WebApi.Services.Movies
{
    public sealed class MovieService : IMovieService
    {
        private readonly IRepository<Movie> _movieRepository;
        private readonly ISessionService _sessionService;

        public MovieService(IRepository<Movie> movieRepository, ISessionService sessionService)
        {
            _movieRepository = movieRepository;
            _sessionService = sessionService;
        }

        public void Create(Movie movie, string userToken)
        {
            User creatorFromDB= _sessionService.GetUserByToken(userToken);
            movie.Creator = creatorFromDB;
            _movieRepository.Add(movie);
        }

        public List<Movie> GetAll()
        {
            return _movieRepository.GetAll();
        }
    }
}
