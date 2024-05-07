using Microsoft.AspNetCore.Mvc;
using PAC.Vidly.WebApi.Controllers.Movies.Models;
using PAC.Vidly.WebApi.Services.Movies;
using PAC.Vidly.WebApi.Services.Movies.Entities;
using PAC.Vidly.WebApi.Services.Users.Entities;
using Vidly.WebApi.Filters;

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
        [AuthenticationFilter]
        public void Create(Movie? request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var userLogged = (User)HttpContext.Items["UserLogged"];

            _movieService.Create(request, userLogged.Id);
        }

        [HttpGet]
        public List<MovieBasicInfoResponse> GetAll()
        {
            var movies = _movieService.GetAll();
            var response = movies.Select(movie => new MovieBasicInfoResponse(movie)).ToList();
            return response;
        }
    }
}
