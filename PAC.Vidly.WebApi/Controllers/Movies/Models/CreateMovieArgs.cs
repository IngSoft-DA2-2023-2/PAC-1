namespace PAC.Vidly.WebApi.Controllers.Movies.Models;

public sealed record CreateMovieArgs
{
    public readonly string Name;
    public readonly Guid Token;
    

    public CreateMovieArgs(string name, string token)
    {
        if (string.IsNullOrEmpty(Name))
            throw new ArgumentNullException("El campo Name no puede ser vacio.");
        
        if (name.Length < 1 || name.Length > 100)
            throw new ArgumentException("El nombre debe tener entre 1 y 100 caracteres.");
        
        Name = name;
        
        Token = Guid.Empty;
        if (string.IsNullOrEmpty(token) || !Guid.TryParse(token, out Token))
            throw new ArgumentNullException("No se ecuentra el Token en el header");
    }
}