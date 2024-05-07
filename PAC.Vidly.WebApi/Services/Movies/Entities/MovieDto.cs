using PAC.Vidly.WebApi.Controllers.Movies.Models;
using PAC.Vidly.WebApi.Services.Users.Entities;

namespace PAC.Vidly.WebApi.Controllers.Movies
{
    public class MovieDto
    {
        public string Id { get; init; }

        public string Name { get; init; } = null!;

        public string CreatorId { get; init; } = null!;

        public User Creator { get; init; } = null!;

        public MovieDto(CreateMovieRequest request, User creator)
        {
            Id = Guid.NewGuid().ToString();  
            Name = request.Name ?? throw new ArgumentNullException(nameof(request.Name), "Name cannot be null");
            CreatorId = creator.Id;  
            Creator = creator ?? throw new ArgumentNullException(nameof(creator), "Creator cannot be null");
        }
    }
}