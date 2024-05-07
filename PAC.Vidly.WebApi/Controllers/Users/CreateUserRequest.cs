namespace PAC.Vidly.WebApi.Controllers.Users
{
    public class CreateUserRequest
    {
        public string? Name { get; init; }
        public string? Email { get; init; }
        public string? Password { get; init; }
    }
}