using PAC.Vidly.WebApi.Services.Users.Entities;

namespace PAC.Vidly.WebApi.Services.Movies.Entities
{
    public sealed record class Movie
    {
        public string Id { get; init; }

        public string Name { get; init; } = null!;

        public User Creator { get; set; } = null!;

        public Movie()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
