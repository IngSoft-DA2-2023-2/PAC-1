namespace PAC.Vidly.WebApi.Controllers.Movies.Models
{
    public sealed record class CreateMovieRequest
    {
        public string Id { get; init; }

        public string? Name { get; init; }
    }
}
