using Microsoft.AspNetCore.Mvc;
using PAC.Vidly.WebApi.Controllers.Movies.Models;
using PAC.Vidly.WebApi.Services.Movies;
using PAC.Vidly.WebApi.Services.Movies.Entities;
using PAC.Vidly.WebApi.Services.Sessions;
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
        public MovieBasicInfoResponse Create(CreateMovieRequest? request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            
            User? user = HttpContext.Items[Items.UserLogged] as User;
            CreateMovieArgs args = new CreateMovieArgs(
                request.Name ?? string.Empty);
            
            Movie movieSaved = _movieService.Create(args, user.Id);
            
            return new MovieBasicInfoResponse(movieSaved);
        }

        [HttpGet]
        public GetMoviesResponse GetAll()
        {
            List<Movie> movies = _movieService.GetAll();
            return new GetMoviesResponse(movies);
        }
    }
}
