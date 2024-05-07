using Microsoft.AspNetCore.Mvc;
using PAC.Vidly.WebApi.Controllers.Movies.Models;
using PAC.Vidly.WebApi.Filters;
using PAC.Vidly.WebApi.Services.Movies;
using PAC.Vidly.WebApi.Services.Movies.Entities;
using PAC.Vidly.WebApi.Services.Users.Entities;
using System.Linq;

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
       
        public MovieBasicInfoResponse Create([FromBody] Movie? request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                throw new ArgumentException("Name is required", nameof(request.Name));
            }
            if (string.IsNullOrWhiteSpace(request.CreatorId))
            {
                throw new ArgumentException("CreatorId is required", nameof(request.CreatorId));
            }
            var userLogged = GetUserLogged();

            if (request.CreatorId != userLogged.Id)
            {
                throw new UnauthorizedAccessException("You can't create a movie for another user");
            }

            var movie = new Movie(request.Name, request.CreatorId);

            _movieService.Create(movie, userLogged.ToString());

            return new MovieBasicInfoResponse(movie);
        }

        [HttpGet]
        public List<MovieBasicInfoResponse> GetAll()
        {
            var movies = _movieService.GetAll();
            var movieResponses = movies.Select(m => new MovieBasicInfoResponse(m)).ToList();
            return movieResponses;
        }

        public User GetUserLogged()
        {
            var userLogged = (User)HttpContext.Items[Items.UserLogged];
            return userLogged;
        }
    }
}
