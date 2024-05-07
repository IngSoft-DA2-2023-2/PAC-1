using PAC.Vidly.WebApi.Controllers.Movies.Models;
using PAC.Vidly.WebApi.DataAccess;
using PAC.Vidly.WebApi.Services.Movies.Entities;
using PAC.Vidly.WebApi.Services.Sessions;
using PAC.Vidly.WebApi.Services.Users;
using PAC.Vidly.WebApi.Services.Users.Entities;
using System.Linq.Expressions;
using System;

namespace PAC.Vidly.WebApi.Services.Movies
{
    public sealed class MovieService : IMovieService
    {
        private readonly IRepository<Movie> _movieRepository;
        private readonly ISessionService _sessionService;

        public MovieService(IRepository<Movie> movieRepository, ISessionService sessionService)
        {
            _movieRepository = movieRepository;
            _sessionService = sessionService;
        }

        public Movie Create(CreateMovieRequest movieReq, string userToken)
        {
            User creatorFromDB= _sessionService.GetUserByToken(userToken);
            var movieToCreate = new Movie
            {
                Name = movieReq.Name,
            };
            movieToCreate.Creator = creatorFromDB;
            _movieRepository.Add(movieToCreate);

            return movieToCreate;
        }

        public List<Movie> GetAll()
        {
            return _movieRepository.GetAll();
        }

        public Movie GetById(string id)
        {
            return _movieRepository.GetOrDefault(m => m.Id == id);
        }
    }
}
