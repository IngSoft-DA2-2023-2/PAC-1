using PAC.Vidly.WebApi.Services.Users.Entities;

namespace PAC.Vidly.WebApi.Controllers.Movies.Models
{
    public sealed record class CreateMovieRequest
    {
        public string? Name { get; init; }

    }
}
