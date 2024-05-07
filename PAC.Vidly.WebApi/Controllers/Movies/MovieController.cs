using Microsoft.AspNetCore.Mvc;
using PAC.Vidly.WebApi.Controllers.Movies.Models;
using PAC.Vidly.WebApi.Filters;
using PAC.Vidly.WebApi.Services.Movies;
using PAC.Vidly.WebApi.Services.Movies.Entities;
using PAC.Vidly.WebApi.Services.Users;
using PAC.Vidly.WebApi.Services.Users.Entities;
using PAC.Vidly.WebApi.UnitTests.Controllers.Models;

namespace PAC.Vidly.WebApi.Controllers.Movies
{
    [ApiController]
    [Route("movies")]
    public sealed class MovieController : MovieControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly IUserService _userService;

        public MovieController(MovieService movieService, IUserService userService)
        {
            _movieService = movieService;
            _userService = userService;
        }

        [HttpPost]
        public CreateMovieResponse Create(Movie? request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var userLogged = GetUserLogged();

            _movieService.Create(request, userLogged.Id);

            return new CreateMovieResponse(request);
        }

        [HttpGet]
        [AuthenticationFilterAttribute.AuthenticationFilter]
        public List<MovieBasicInfoResponse> GetAll()
        {
            var movies = _movieService.GetAll();

            var result = new List<MovieBasicInfoResponse>();
            
            movies.ForEach(m => result.Add(new MovieBasicInfoResponse(m)));

            return result;
        }

        [HttpPost("{id}")]
        public CreateMovieIdResponse GetById(string id)
        {
            Movie? m = _movieService.GetAll().Find(x => x.Id == id);
            if (m == null)
            {
                throw new ArgumentException("Movie does not exist.");
            }

            var list = _userService.GetAll().Select(x => (x.FavoriteMovie.Find(x => x.Id == id) != null) );
            int q = 0;
            foreach (var elem in list)
            {
                q++;
            }
            
            
            return new CreateMovieIdResponse(m,q, new List<string>());
        }
    }
}
