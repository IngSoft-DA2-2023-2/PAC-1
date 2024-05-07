namespace PAC.Vidly.WebApi.Services.Movies.Entities;

public class CreateMovieArgs
{
    public readonly string Name;

    public CreateMovieArgs(
        string name)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentNullException("The movie name can not be null or empty.");
        
        Name = name;
    }
}