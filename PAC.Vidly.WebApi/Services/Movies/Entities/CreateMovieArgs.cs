namespace PAC.Vidly.WebApi.Services.Movies.Entities;

public class CreateMovieArgs
{
    public string? Name { get; init; }
    
    public CreateMovieArgs(string name)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentNullException("Name cannot be empty");
        if (name.Length < 1 || name.Length > 100)
            throw new Exception("The name characters must be between 1 and 100.");
        Name = name;
    }
}
