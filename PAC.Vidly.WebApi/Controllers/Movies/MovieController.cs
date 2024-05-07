using Microsoft.AspNetCore.Mvc;
using PAC.Vidly.WebApi.Controllers.Movies.Models;
using PAC.Vidly.WebApi.Services.Movies;
using PAC.Vidly.WebApi.Services.Movies.Entities;
using PAC.Vidly.WebApi.Services.Users.Entities;

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
        public void Create(Movie? request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            User? userLogged = HttpContext.Items[Items.UserLogged] as User;

            _movieService.Create(request, userLogged.Id);
        }

        [HttpGet]
        public List<MovieBasicInfoResponse> GetAll()
        {
            var movies = _movieService.GetAll();

            return movies;
        }
    }
}
