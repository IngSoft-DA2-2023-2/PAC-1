using PAC.Vidly.WebApi.Services.Users.Entities;

namespace PAC.Vidly.WebApi.Controllers.Movies.Models;

public sealed record class CreateMovieArgs
{
    public readonly string Id;
    public readonly string Name;
    public readonly string Mail;
    public readonly string Password;
    
    public CreateMovieArgs(string requestId, string? requestName, string mail, string password)
    {
        if (string.IsNullOrEmpty(requestId))
            throw new Exception("El campo id no puede ser vacio.");
        Id = requestId;
        
        if (string.IsNullOrEmpty(requestName))
            throw new Exception("El campo name no puede ser vacio.");
        Name = requestName;
        
        if (string.IsNullOrEmpty(mail))
            throw new Exception("El campo mail no puede ser vacio.");
        Mail = mail;
        
        if (string.IsNullOrEmpty(password))
            throw new Exception("El campo password no puede ser vacio.");
        Password = password;
    }
}