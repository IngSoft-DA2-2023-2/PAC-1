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

        public string Create(CreateMovieRequest response, string userLoggedId)
        {
            if(response.Id == null)
            {
                throw new ArgumentNullException("Id can not be null");
            }
            if(response.Name == null)
            {
                throw new ArgumentNullException("Name can not be null");
            }
            var repeatedMovieName = _movieRepository.GetAll().FirstOrDefault(x => x.Name == response.Name);
            if(repeatedMovieName != null)
            {
                throw new ArgumentException("Movie name already exists");
            }
            if(response.Name.Length < 1 && response.Name.Length > 100)
            {
                throw new ArgumentException("Name must be between 1 and 100 characters long");
            }

            var movie = new Movie
            {
                Id = response.Id,
                Name = response.Name,
                CreatorId = userLoggedId,
            };
            
            _movieRepository.Add(movie);
            return movie.Id;
        }

        public List<Movie> GetAll()
        {
            return _movieRepository.GetAll();
        }
    }
}
