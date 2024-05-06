namespace PAC.Vidly.WebApi.Controllers.Sessions.Models
{
    public sealed record class CreateSessionRequest
    {
        public string? Email { get; init; }

        public string? Password { get; init; }
    }
}
