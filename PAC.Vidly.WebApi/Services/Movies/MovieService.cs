using PAC.Vidly.WebApi.Controllers.Movies.Models;
using PAC.Vidly.WebApi.DataAccess;
using PAC.Vidly.WebApi.Exceptions;
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

        public Movie Create(CreateMovieArgs args, User user)
        {
            Movie movieToSave = new Movie
            {
                Name = args.Name,
                CreatorId = user.Id,
                Creator = user
            };
            
            checkIfMovieIsAlreadyAdded(movieToSave);
            checkMovieName(movieToSave);
            _movieRepository.Add(movieToSave);

            return movieToSave;
        }

        public List<MovieBasicInfoResponse> GetAll()
        {
            List<Movie> = _movieRepository.GetAll(); 
        }

        private void checkIfMovieIsAlreadyAdded(Movie movie)
        {
            Movie movieSearched = _movieRepository.GetOrDefault(movieSearched => movieSearched.Name == movie.Name);
            if (movieSearched is not null)
            {
                throw new DuplicatedException("Movie is duplicated");
            }
        }

        private void checkMovieName(Movie movie)
        {
            if (movie.Name.Length == 0 || movie.Name.Length > 100)
            {
                throw new NameLenghtException("The name should have between 1 and 100 characters");
            }
        }
    }
}
