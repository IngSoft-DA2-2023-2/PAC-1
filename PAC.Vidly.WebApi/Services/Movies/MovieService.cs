using PAC.Vidly.WebApi.Controllers.Movies.Models;
using PAC.Vidly.WebApi.DataAccess;
using PAC.Vidly.WebApi.Services.Movies.Entities;
using PAC.Vidly.WebApi.Services.Users.Entities;

namespace PAC.Vidly.WebApi.Services.Movies
{
    public sealed class MovieService : IMovieService
    {
        private readonly IRepository<Movie> _movieRepository;

        public MovieService(IRepository<Movie> movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public Movie Create(CreateMovieArgs movie, User Creator)
        {
            var movieToSave = new Movie()
            {
                Creator = Creator,
                CreatorId = Creator.Id,
                Name = movie.Name,
            };

            if (_movieRepository.GetOrDefault(m => m.Name == movie.Name) != null)
                throw new Exception("Movie duplicated"); 
            
            _movieRepository.Add(movieToSave);

            return movieToSave;
        }

        public List<Movie> GetAll()
        {
            return _movieRepository.GetAll();
        }
    }
}
