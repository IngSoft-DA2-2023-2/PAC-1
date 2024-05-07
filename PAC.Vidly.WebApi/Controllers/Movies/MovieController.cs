using Microsoft.AspNetCore.Mvc;
using PAC.Vidly.WebApi.Controllers.Movies.Models;
using PAC.Vidly.WebApi.Controllers.Users;
using PAC.Vidly.WebApi.Services.Movies;
using PAC.Vidly.WebApi.Services.Movies.Entities;
using PAC.Vidly.WebApi.Services.Users;

namespace PAC.Vidly.WebApi.Controllers.Movies
{
    [ApiController]
    [Route("movies")]
    public sealed class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly IUserService _userService;
        private readonly ControllerBaseSession _controllerBaseSession;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpPost]
        public CreateMovieResponse Create(Movie? request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var userLogged = _controllerBaseSession.GetUserLogged(authorization!);

            
            _movieService.Create(request, userLogged.Id);
            
            var movieToSave = new Movie(
                request.Id,
                request.Name,
                request.CreatorId,
            );

            var movieSaved = _movieService.Create(movieToSave);

            return new CreateMovieResponse(movieSaved);
            
        }

        [HttpGet]
        public List<Movie> GetAll()
        {
            List<Movie> movies = _movieService.GetAll();

            return movies;
        }
    }
}
