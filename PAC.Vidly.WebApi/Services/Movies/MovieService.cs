using Microsoft.AspNetCore.Identity;
using PAC.Vidly.WebApi.Controllers.Movies.Models;
using PAC.Vidly.WebApi.DataAccess;
using PAC.Vidly.WebApi.Services.Movies.Entities;
using PAC.Vidly.WebApi.Services.Users;
using PAC.Vidly.WebApi.Services.Users.Entities;

namespace PAC.Vidly.WebApi.Services.Movies
{
    public sealed class MovieService : IMovieService
    {
        private readonly IRepository<Movie> _movieRepository;
        private readonly IUserService _userService;

        public MovieService(IRepository<Movie> movieRepository, IUserService userService)
        {
            _movieRepository = movieRepository;
            _userService = userService;
        }

        public void Create(Movie movie, string userLoggedId)
        {
            _movieRepository.Add(movie);
        }

        public Movie Create(CreateMovieArgs args)
        {
            throw new NotImplementedException();
        }

        public List<Movie> GetAll()
        {
            return _movieRepository.GetAll();
        }

        public User GetUserLogged(string email, string password)
        {
            User user = _userService.GetByCredentials(email,password);
            return user;
        }
    }
}
