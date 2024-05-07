using Microsoft.AspNetCore.Mvc;
using PAC.Vidly.WebApi.Controllers.Movies.Models;
using PAC.Vidly.WebApi.Services.Movies;
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
        public MovieBasicInfoResponse Create(CreateMovieRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var args = new CreateMovieArgs(
                request.Id,
                request.Name,
                request.Mail,
                request.Password
            );

            var userLogged = _movieService.GetUserLogged(args.Mail,args.Password);
            Movie movieCreated = _movieService.Create(args);
            return new MovieBasicInfoResponse(movieCreated);
        }
        
        [HttpGet]
        public List<MovieBasicInfoResponse> GetAll()
        {
            var movies = _movieService.GetAll();

            return movies;
        }
    }
}


