using PAC.Vidly.WebApi.Services.Movies.Entities;
namespace PAC.Vidly.WebApi.Controllers.Movies
{
    public class CreateMovieResponse
    {

        public string Id { get; }

        public CreateMovieResponse(Movie movie)
        {
            Id = movie.Id;
        }
    }
}