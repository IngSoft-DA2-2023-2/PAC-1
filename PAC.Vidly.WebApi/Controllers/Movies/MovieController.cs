using Microsoft.AspNetCore.Mvc;
using PAC.Vidly.WebApi.Controllers.Movies.Models;
using PAC.Vidly.WebApi.Services.Movies;
using PAC.Vidly.WebApi.Services.Movies.Dto;
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
        public Movie Create(Movie? request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            if (string.IsNullOrEmpty(request.Name))
                ThrowException("InvalidRequest", "Name cannot be empty");

            //var userLogged = GetUserLogged();

            //var movie = _movieService.Create(request, userLogged.Id);
            var movie = _movieService.Create(request);
            return movie;
        }

        [HttpGet]
        public List<Movie> GetAll()
        {
            var movies = _movieService.GetAll();

            return movies;
        }

        // [HttpGet]
        // public MovieObtained Get(string id)
        // {
        //     _movieService.Get(id);
        // }
        private static void ThrowException(string code, string description) => throw new Exception($"Code:{code}, Description: {description}");

    }
}
