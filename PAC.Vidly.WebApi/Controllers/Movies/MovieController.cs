using Microsoft.AspNetCore.Mvc;
using PAC.Vidly.WebApi.Controllers.Movies.Models;
using PAC.Vidly.WebApi.Filters;
using PAC.Vidly.WebApi.Services.Movies;
using PAC.Vidly.WebApi.Services.Movies.Entities;
using PAC.Vidly.WebApi.Services.Sessions;
using PAC.Vidly.WebApi.Services.Users.Entities;

namespace PAC.Vidly.WebApi.Controllers.Movies
{
    [ApiController]
    [ExceptionFilter]
    [Route("api/movies")]
    public sealed class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly ISessionService _sessionService;

        public MovieController(IMovieService movieService, ISessionService sessionService)
        {
            _movieService = movieService;
            _sessionService = sessionService;
        }

        [ServiceFilter(typeof(AuthenticationFilter))]
        [HttpPost]
        public CreateMovieResponse Create(CreateMovieRequest? request)
        {
            string userSessionToken = HttpContext.Request.Headers["Authorization"].ToString();
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var arguments = new CreateMovieArgs(request.Name ?? string.Empty, userSessionToken);

            User userLogged = _sessionService.GetCurrentUser(arguments.Token);

           Movie movie = _movieService.Create(arguments, userLogged.Id);
           return new CreateMovieResponse(movie);
        }
        
        [ServiceFilter(typeof(AuthenticationFilter))]
        [HttpGet]
        public List<MovieBasicInfoResponse> GetAll()
        {
            var movies = _movieService.GetAll();

            return movies.Select(m => new MovieBasicInfoResponse(m)).ToList();
        }
    }
}
