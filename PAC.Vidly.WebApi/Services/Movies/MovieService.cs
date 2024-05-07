using PAC.Vidly.WebApi.Controllers.Movies.Models;
using PAC.Vidly.WebApi.DataAccess;
using PAC.Vidly.WebApi.Services.Movies.Entities;

namespace PAC.Vidly.WebApi.Services.Movies
{
    public sealed class MovieService : IMovieService
    {
        private readonly IRepository<Movie> _movieRepository;

        public MovieService(IRepository<Movie> movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public Movie Add(CreateMovieArgs movie)
        {
            var existMovie = _movieRepository.Exist(m => m.Name == movie.Name);
            if (existMovie)
                throw new Exception("Movie with given name already created.");
            
            Movie movieToSave = new Movie
            {
                Name = movie.Name,
                Creator = movie.Creator
            };
            _movieRepository.Add(movieToSave);
            return movieToSave;
        }

        public List<Movie> GetAll()
        {
            return _movieRepository.GetAll();
        }
    }
}
