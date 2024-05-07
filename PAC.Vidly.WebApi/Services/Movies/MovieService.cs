using PAC.Vidly.WebApi.DataAccess;
using PAC.Vidly.WebApi.Services.Movies.Entities;
using PAC.Vidly.WebApi.Controllers.Movies;

namespace PAC.Vidly.WebApi.Services.Movies
{
    public sealed class MovieService : IMovieService
    {
        private readonly IRepository<Movie> _movieRepository;

        public MovieService(IRepository<Movie> movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public Movie Create(MovieDto movieDto, string userLoggedId)
        {
            var existingMovie = _movieRepository.GetAll().FirstOrDefault(m => m.Name == movieDto.Name);
            if (existingMovie != null)
            {
                throw new InvalidOperationException("Movie is duplicated");
            }

            var movie = new Movie
            {
                Id = movieDto.Id,
                Name = movieDto.Name,
                CreatorId = movieDto.CreatorId,
                Creator = movieDto.Creator
            };

            _movieRepository.Add(movie);
            return movie;
        }

        public List<Movie> GetAll()
        {
            return _movieRepository.GetAll();
        }
    }
}
