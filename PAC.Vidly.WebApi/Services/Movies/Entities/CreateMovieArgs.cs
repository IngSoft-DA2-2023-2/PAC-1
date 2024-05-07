using PAC.Vidly.WebApi.Exceptions;

namespace PAC.Vidly.WebApi.Services.Movies.Entities;

public sealed record class CreateMovieArgs
{
    public string Name;

    public CreateMovieArgs(
        string name
        )
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgsNullException("Name cannot be empty or null");
        Name = name;
    }
}
