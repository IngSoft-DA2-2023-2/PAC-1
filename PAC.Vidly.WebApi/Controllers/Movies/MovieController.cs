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
        public string Create(Movie? request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            
            User? user = HttpContext.Items[Items.UserLogged] as User;
            if (user == null)
            {
                throw new InvalidOperationException("User not found");
            }

            if (user.Id != request.CreatorId)
            {
                throw new Exception("The creator ID of the movie must be the same as the user ID.");
            }
            if(IsMovieNameAlreadyInUse(request.Name))
            {
                throw new Exception("The movie name is already in use.");
            }
            
            CreateMovieArgs arguments = new CreateMovieArgs(
                request.Name ?? string.Empty,
                request.CreatorId ?? string.Empty);
            
            
            Movie newMovie = new Movie(
                arguments.Name,
                arguments.CreatorId);

            _movieService.Create(newMovie, user.Id);
            return newMovie.Id;
        }

        [HttpGet]
        public List<Movie> GetAll()
        {
            var movies = _movieService.GetAll();

            return movies;
        }

        private bool IsMovieNameAlreadyInUse(string name)
        {
            var movies = _movieService.GetAll();
            return movies.Any(m => m.Name == name);
        }

    }
}
