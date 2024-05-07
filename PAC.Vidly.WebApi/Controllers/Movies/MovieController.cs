using Microsoft.AspNetCore.Mvc;
using PAC.Vidly.WebApi.Controllers.Movies.Models;
using PAC.Vidly.WebApi.Services.Movies;
using PAC.Vidly.WebApi.Services.Movies.Entities;

namespace PAC.Vidly.WebApi.Controllers.Movies
{
    [ApiController]
    [Route("")]
    public sealed class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MovieController(MovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpPost]
        public MovieBasicInfoResponse Create(Movie? request, string userToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (string.IsNullOrWhiteSpace(userToken))
            {
                throw new ArgumentNullException(nameof(userToken));
            }
            _movieService.Create(request, userToken);
            return new MovieBasicInfoResponse(request);
        }

        [HttpGet]
        public List<Movie> GetAll()
        {
            var movies = _movieService.GetAll();

            return movies;
        }
    }
}
