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

        public string Create(CreateMovieArgs movieRequest, User userLogged)
        {
            Movie movie = new Movie
            {
                Name = movieRequest.Name,
                CreatorId = userLogged.Id,
                Creator = userLogged
            };

            validateMovieIsNotDuplicated(movie);
            _movieRepository.Add(movie);
            return movie.Id;
        }

        public List<MovieBasicInfoResponse> GetAll()
        {
            List<Movie> movies = _movieRepository.GetAll();
            List<MovieBasicInfoResponse> basicInfo = new List<MovieBasicInfoResponse>();
            
            foreach (Movie movie in movies)
            {
                MovieBasicInfoResponse movieBasicInfo = new MovieBasicInfoResponse(movie);
            }

            return basicInfo;
        }

        private void validateMovieIsNotDuplicated(Movie movie)
        {
            Movie? duplicatedMovie = _movieRepository.
                GetOrDefault(movie => movie.Name == movie.Name);
            if (duplicatedMovie is not null)
                throw new InvalidOperationException("Movie duplicated");
        }
    }
}
