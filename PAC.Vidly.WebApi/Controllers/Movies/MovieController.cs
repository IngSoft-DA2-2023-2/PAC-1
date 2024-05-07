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

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpPost]
        public MovieBasicInfoResponse Create(CreateMovieRequest? request, string userToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            if (request.Name == null)
            {
                throw new Exception("Name is required");
            }

            if (string.IsNullOrWhiteSpace(userToken))
            {
                throw new ArgumentNullException(nameof(userToken));
            }
            Movie movieToCreate = _movieService.Create(request, userToken);
            return new MovieBasicInfoResponse(movieToCreate);
        }

        [HttpGet("{id}")]
        public Movie Get(string id)
        {
            var movie = _movieService.GetById(id);

            return movie;
        }

        [HttpGet]
        public List<Movie> GetAll()
        {
            var movies = _movieService.GetAll();

            return movies;
        }
    }
}
