using Microsoft.AspNetCore.Mvc;
using PAC.Vidly.WebApi.Controllers.Movies.Models;
using PAC.Vidly.WebApi.Filters;
using PAC.Vidly.WebApi.Services.Movies;
using PAC.Vidly.WebApi.Services.Movies.Entities;

namespace PAC.Vidly.WebApi.Controllers.Movies
{
    [ApiController]
    [Route("movies")]
    [AuthenticationFilterAttribute]
    public sealed class MovieController : VidlyController
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpPost]
        public string Create(CreateMovieRequest? request)
        {
            var userLogged = GetUserLogged();

            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            MovieArguments movieArguments = new MovieArguments(request.Name);

            string id = _movieService.Create(movieArguments, userLogged.Id);

            return id;

        }

        [HttpGet]
        public List<MovieBasicInfoResponse> GetAll()
        {
            var movies = _movieService.GetAll();

            List<MovieBasicInfoResponse> response = movies.Select(m => new MovieBasicInfoResponse(m)).ToList();
            return response;
        }
    }
}
