using PAC.Vidly.WebApi.Services.Movies.Entities;

namespace PAC.Vidly.WebApi.Controllers.Movies.Models;

public class CreateMovieIdResponse
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int QuantityOfUsersMarkedAsFavorite { get; set; }
    public List<string> UsersMarkedAsFavorite { get; set; }
    
    
    public CreateMovieIdResponse(Movie m, int quantity, List<string> usersName)
    {
        Id = m.Id;
        Name = m.Name;
        QuantityOfUsersMarkedAsFavorite = quantity;
        UsersMarkedAsFavorite = usersName;
    }
}