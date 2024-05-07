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

        public Movie Create(CreateMovieArgs args, string userLoggedId)
        {
            Movie movie = new(args.Name, userLoggedId);
            Movie MovieWithSameName = GetMovieByName(args.Name);
            if (MovieWithSameName != null)
                throw new ArgumentException("Movie is duplicated");
            _movieRepository.Add(movie);
            return movie;
        }

        public List<Movie> GetAll()
        {
            return _movieRepository.GetAll();
        }

        private Movie GetMovieByName(string name)
        {
            return _movieRepository.GetOrDefault(m=>m.Name == name);
        }
    }
}
