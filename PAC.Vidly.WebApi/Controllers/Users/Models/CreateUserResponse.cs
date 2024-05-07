namespace PAC.Vidly.WebApi.Controllers.Users.Models;

public sealed record class CreateUserResponse
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;
}