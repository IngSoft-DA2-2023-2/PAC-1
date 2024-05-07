namespace PAC.Vidly.WebApi.Services.Movies.Entities
{
    public sealed class MovieArguments
    {
        public string Name { get; init; }

        public MovieArguments(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Name is required", nameof(name));
            }

            if (name.Length > 100)
            {
                throw new ArgumentException("Name is too long", nameof(name));
            }

            Name = name;
        }
    }
}
