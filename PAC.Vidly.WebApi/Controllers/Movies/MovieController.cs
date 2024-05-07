using Microsoft.AspNetCore.Mvc;
using PAC.Vidly.WebApi.Controllers.Movies.Models;
using PAC.Vidly.WebApi.Services;
using PAC.Vidly.WebApi.Services.Movies;
using PAC.Vidly.WebApi.Services.Movies.Entities;
using Vidly.WebApi.Filters;

namespace PAC.Vidly.WebApi.Controllers.Movies
{
    [ApiController]
    [Route("movies")]
    [AuthenticationFilter]
    public sealed class MovieController : VidlyControllerBase
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

            var userLogged = base.GetUserLogged();
            
            var isNotAllowed = !userLogged.Role.HasPermission(PermissionKey.CreateMovie);

            if (isNotAllowed)
                throw new Exception($"Code: Forbidden, Message: Missing permission {PermissionKey.CreateMovie}");
            
            var arguments = new CreateMovieArgs(
                request.Name, userLogged);

            var movie = _movieService.Add(arguments);

            return new CreateMovieResponse(movie);

        }

        [HttpGet]
        public List<MovieBasicInfoResponse> GetAll()
        {
            return _movieService
                .GetAll()
                .Select(m => new MovieBasicInfoResponse(m))
                .ToList();
        }
    }
}
