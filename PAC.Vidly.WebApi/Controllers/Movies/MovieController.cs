using Microsoft.AspNetCore.Mvc;
using PAC.Vidly.WebApi.Controllers.Movies.Models;
using PAC.Vidly.WebApi.Services;
using PAC.Vidly.WebApi.Services.Movies;
using PAC.Vidly.WebApi.Services.Movies.Entities;

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
            if(request.Name == null)
            {
                throw new Exception("Name cannot be empty");
            }

            var userLogged = (Services.Movies.Entities.Movie)HttpContext.Items[Item.UserLogged];
            //var userLogged = GetUserLogged();

            _movieService.Create(request, userLogged.Id);
        }

        [HttpGet]
        public List<MovieBasicInfoResponse> GetAll()
        {
            var movies = _movieService.GetAll();

            var basicInfoList = movies.Select(movie => new MovieBasicInfoResponse(movie)).ToList();

            return basicInfoList;
        }

    }
}
