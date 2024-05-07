namespace PAC.Vidly.WebApi.Controllers.Movies.Models
{
    public class CreateMovieArguments
    {
        public string? Name { get; init; }

        public CreateMovieArguments (string? name)
        {
            Name = name;
        }   
    }
}
