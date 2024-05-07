using BuildingManager.WebApi.Filters;
using Microsoft.AspNetCore.Mvc;
using PAC.Vidly.WebApi.Controllers.Movies.Models;
using PAC.Vidly.WebApi.Services.Movies;
using PAC.Vidly.WebApi.Services.Movies.Entities;
using PAC.Vidly.WebApi.Services.Sessions;
using PAC.Vidly.WebApi.Services.Users.Entities;

namespace PAC.Vidly.WebApi.Controllers.Movies
{
    [ApiController]
    [AuthenticationFilter]
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

            var userLogged = (User)HttpContext.Items["User"];

            _movieService.Create(request, userLogged.Id);

            return new CreateMovieResponse { Id = request.Id, Name = request.Name};
        }

        [HttpGet]

        public List<Movie> GetAll()
        {
            var movies = _movieService.GetAll();

            return movies;
        }
    }
}
