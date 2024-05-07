

namespace PAC.Vidly.WebApi.Controllers.Users.Models
{
    
    public sealed record class CreateUserRequest
    {
        public string Id { get; init; }

        public string? Name { get; init; } = null!;
        public string? Email { get; init; } = null!;
        public string? Password { get; init; } = null!;
    }
}