using Microsoft.AspNetCore.Mvc;
using PAC.Vidly.WebApi.Controllers.Movies.Models;
using PAC.Vidly.WebApi.Filters;
using PAC.Vidly.WebApi.Services.Movies;
using PAC.Vidly.WebApi.Services.Movies.Entities;
using PAC.Vidly.WebApi.Services.Sessions;

namespace PAC.Vidly.WebApi.Controllers.Movies
{
    [ApiController]
    [Route("movies")]
    [AuthenticationFilter]
    public sealed class MovieController : VidlyControllerBase
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService,  ISessionService sessionService) : base(sessionService)
        {
            _movieService = movieService;
        }


        [HttpPost]
        public string Create(Movie? request, [FromHeader] string? authorization)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            
            var userLogged = base.GetUserLogged(authorization);
            
            var args = new CreateMovieArgs(request, userLogged.Id);

            return _movieService.Create(args);
        }

        [HttpGet]
        public List<MovieBasicInfoResponse> GetAll()
        {
            var movies = _movieService.GetAll();
            var movieBasicInfoList = new List<MovieBasicInfoResponse>();
            
            foreach (var movie in movies)
            {
                var movieBasicInfo = new MovieBasicInfoResponse(movie);
                movieBasicInfoList.Add(movieBasicInfo);
            }

            return movieBasicInfoList;
        }
    }
}
