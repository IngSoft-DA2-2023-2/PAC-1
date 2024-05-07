using System.Runtime.InteropServices.JavaScript;
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

        // public Movie Create(Movie movie, string userLoggedId)
        // {
        //     _movieRepository.Add(movie);
        //     return movie;
        // }
        public Movie Create(Movie movie)
        {
            var existMovie = _movieRepository.GetOrDefault(m => m.Name == movie.Name);;
            if (existMovie != null)
                throw new Exception("movie duplicated");
            _movieRepository.Add(movie);
            return movie;
        }

        
        public List<Movie> GetAll()
        {
            return _movieRepository.GetAll();
        }

        public Movie Get(string id)
        {
            var movie =  _movieRepository.GetOrDefault(m => m.Id == id);
            return movie;
        }
    }
}
