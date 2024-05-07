using Microsoft.AspNetCore.Mvc;
using PAC.Vidly.WebApi.Controllers.Movies.Models;
using PAC.Vidly.WebApi.Services.Movies;
using PAC.Vidly.WebApi.Services.Movies.Entities;
using PAC.Vidly.WebApi.Services.Users.Entities;

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
        public MovieBasicInfoResponse Create(CreateMovieArgs? request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            User? userLogged = HttpContext.Items[Items.UserLogged] as User;

            Movie movieCreated = _movieService.Create(request, userLogged);

            return new MovieBasicInfoResponse(movieCreated);
        }

        [HttpGet]
        public List<MovieBasicInfoResponse> GetAllMovies()
        {
            var movies = _movieService.GetAll();

            return movies;
        }
    }
}
