using PAC.Vidly.WebApi.Services.Users.Entities;

namespace PAC.Vidly.WebApi.Services.Movies.Entities;

public sealed record class CreateMovieArgs
{
    public readonly string Name;
    public readonly string CreatorId;
    
    public CreateMovieArgs(
        string name,
        string creatorId)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new Exception("The name cannot be null");
        }
        if(name.Length > 100)
        {
            throw new Exception("The name cannot be longer than 100 characters");
        }
        Name = name;

        if (string.IsNullOrEmpty(creatorId))
        {
            throw new Exception("The creatorId cannot be null");
        }
        CreatorId = creatorId;
    }   
    
}