using Microsoft.AspNetCore.Mvc;
using PAC.Vidly.WebApi.Controllers.Movies.Models;
using PAC.Vidly.WebApi.Services.Movies;
using PAC.Vidly.WebApi.Services.Movies.Entities;

namespace PAC.Vidly.WebApi.Controllers.Movies
{
    [ApiController]
    [Route("movies")]
    public sealed class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpPost]
        public CreateMovieResponse Create(CreateMovieRequest? request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                throw new ArgumentNullException(nameof(request.Name));
            }

            var userLogged = _sessionService.GetUserLogged();
            //deberia de inyectarse el servicio de sesion, donde residiria esa logica.

            var movieToReturn = _movieService.Create(request, userLogged.Id);
            return new CreateMovieResponse(movieToReturn);
        }

        [HttpGet]
        public List<MovieBasicInfoResponse> GetAll()
        {
            var movies = _movieService.GetAll();
            var movieBasicInfoResponses = new List<MovieBasicInfoResponse>();

            foreach (var movie in movies)
            {
                var movieBasicInfo = new MovieBasicInfoResponse(movie);

                movieBasicInfoResponses.Add(movieBasicInfo);
            }
            return movieBasicInfoResponses;
        }
    }
}
