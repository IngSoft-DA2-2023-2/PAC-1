using Microsoft.AspNetCore.Mvc;
using PAC.Vidly.WebApi.Controllers.Movies.Models;
using PAC.Vidly.WebApi.Filters;
using PAC.Vidly.WebApi.Services.Movies;
using PAC.Vidly.WebApi.Services.Movies.Arguments;
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
        [AuthenticationFilter]
        public CreateMovieResponse Create(CreateMovieRequest? request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (request.Name == null)
            {
                throw new ArgumentNullException("Name cannot be empty");
            }

            if (_movieService.IsMovieExist(request.Name))
            {
                throw new Exception("Movie already exists");
            }

            var args = new CreateMovieArgs()
            {
                Name = request.Name,
                CreatorId = request.CreatorId
            };

            var movieSaved = _movieService.Create(args);

            return new CreateMovieResponse(movieSaved);

        }

        [HttpGet]
        public List<Movie> GetAll()
        {
            return _movieService.GetAll();
        }
    }
}
