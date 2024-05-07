using PAC.Vidly.WebApi.Services.Users.Entities;

namespace PAC.Vidly.WebApi.Controllers.Movies.Models;

public class CreateMovieArgs
{
    public readonly string Name;
    public readonly User Creator;
    
    public CreateMovieArgs(
        string name,
        User creator)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentException("El nombre no puede estar vacío.");
        }
        if (name.Length > 100)
        {
            throw new ArgumentException("El nombre no puede tener más de 100 caracteres.");
        }
        Name = name;
        
        if(Creator == null)
        {
            throw new ArgumentNullException(nameof(creator));
        }
        Creator = creator;
        
    }
}