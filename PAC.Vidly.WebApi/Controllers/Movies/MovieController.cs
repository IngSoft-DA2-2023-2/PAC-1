using Microsoft.AspNetCore.Mvc;
using PAC.Vidly.WebApi.Controllers.Movies.Models;
using PAC.Vidly.WebApi.Filters;
using PAC.Vidly.WebApi.Services.Movies;
using PAC.Vidly.WebApi.Services.Movies.Entities;
using PAC.Vidly.WebApi.Services.Users;
using PAC.Vidly.WebApi.Services.Users.Entities;

namespace PAC.Vidly.WebApi.Controllers.Movies
{
    [ApiController]
    [Route("")]
    [AuthenticationFilter]
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

            User? userLogged = HttpContext.Items[Items.UserLogged] as User;
            
            var movieToSave = new CreateMovieArgs(request.Name);
            
            var movieSaved = _movieService.Create(movieToSave, userLogged);

            return new CreateMovieResponse(movieSaved);
        }

        [HttpGet]
        public List<MovieBasicInfoResponse> GetAll()
        {
            var movies = _movieService.GetAll();

            var moviesToReturn = new List<MovieBasicInfoResponse>();

            foreach (var movie in movies)
            {
                moviesToReturn.Add(new MovieBasicInfoResponse(movie));
            }

            return moviesToReturn;
        }
    }
}
