namespace PAC.Vidly.WebApi.Controllers.Movies.Models
{
    public sealed record class CreateMovieResponse
    {
        public string Id { get; init; }
        public string Name { get; init; }

        public CreateMovieResponse()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
