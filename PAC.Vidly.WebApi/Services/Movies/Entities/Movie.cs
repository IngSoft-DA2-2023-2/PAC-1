using PAC.Vidly.WebApi.Services.Users.Entities;

namespace PAC.Vidly.WebApi.Services.Movies.Entities
{
    public sealed record class Movie
    {
        public string Id { get; init; }

        public string Name { get; init; } = null!;

        public string CreatorId { get; init; } = null!;

        public User Creator { get; init; } = null!;
        
        public List<User> Users { get; init; }

        public Movie()
        {
            Id = Guid.NewGuid().ToString();
        }

        public Movie(
            string name,
            string creatorId)
            : this()
        {
            Name = name;
            CreatorId = creatorId;
            Users = new List<User>();
        }
    }
}
