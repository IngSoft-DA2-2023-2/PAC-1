using PAC.Vidly.WebApi.Services.Users.Entities;

namespace PAC.Vidly.WebApi.Services.Sessions.Entities
{
    public sealed record class Session
    {
        public string Id { get; init; }

        public string Token { get; set; }

        public string UserId { get; init; } = null!;

        public User User { get; init; } = null!;

        public Session()
        {
            Id = Guid.NewGuid().ToString();
            Token = Guid.NewGuid().ToString().Replace("-", "");
        }

        public Session(string userId)
            : this()
        {
            UserId = userId;
        }
    }
}
