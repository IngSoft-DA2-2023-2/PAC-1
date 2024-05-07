namespace PAC.Vidly.WebApi.Controllers.Movies.Models
{
    public sealed record class CreateMovieRequest
    {

        public string? Name { get; init; }
        public string? CreatorId { get; init; }

    }
}
