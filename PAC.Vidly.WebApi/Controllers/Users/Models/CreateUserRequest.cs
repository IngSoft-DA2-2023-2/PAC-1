namespace PAC.Vidly.WebApi.Controllers.Users.Models;

public sealed record class CreateUserRequest
{
    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;
    
}