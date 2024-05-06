namespace PAC.Vidly.WebApi.Services.Users.Entities
{
    public sealed record class User
    {
        public string Id { get; init; }

        public string Name { get; init; } = null!;

        public string Email { get; init; } = null!;

        public string Password { get; init; } = null!;

        public User() 
        { 
            Id = Guid.NewGuid().ToString();
        }

        public User(
            string name,
            string email,
            string password)
            : this()
        {
            Name = name;
            Email = email;
            Password = password;
        }
    }
}
