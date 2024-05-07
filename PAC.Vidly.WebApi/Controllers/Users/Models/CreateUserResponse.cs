namespace PAC.Vidly.WebApi.Controllers.Users.Models;

public sealed record class CreateUserResponse
{
    public string Email { get; set; }
    public CreateUserResponse(string mail)
    {
        Email = mail;
    }
}