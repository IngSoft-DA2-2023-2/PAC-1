using PAC.Vidly.WebApi.Services.Movies.Entities;

namespace PAC.Vidly.WebApi.Controllers.Movies.Models
{
    public sealed record class MovieBasicInfoResponse
    {
        public string Id { get; init; }

        public MovieBasicInfoResponse(Movie movie)
        {
           this.Id = movie.Id;
        }
    }
}
