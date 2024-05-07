using Microsoft.AspNetCore.Mvc;
using PAC.Vidly.WebApi.Controllers.Movies.Models;
using PAC.Vidly.WebApi.Services.Movies;
using PAC.Vidly.WebApi.Services.Movies.Entities;
using PAC.Vidly.WebApi.Services.Users.Entities;

namespace PAC.Vidly.WebApi.Controllers.Movies
{
    [ApiController]
    [Route("movie")]
    public sealed class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MovieController(MovieService movieService)
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

            var userLogged = GetUserLogged();

            _movieService.Create(request, userLogged.Id);
        }

        private User GetUserLogged()
        {
            var userLogged = HttpContext.Items[Items.UserLogged] as User;
            return userLogged;
        }

        [HttpGet]
        public List<MovieBasicInfoResponse> GetAll()
        {
            var movies = _movieService.GetAll();

            return movies;
        }
    }
}
