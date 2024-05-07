namespace PAC.Vidly.WebApi.Controllers.Users.Models
{
    public class UserDto
    {
        public string Id { get; init; }
        public string Name { get; init; }
        public string Email { get; init; }
        public string Password { get; init; }
        public UserDto(CreateUserRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request), "CreateUserRequest cannot be null");

            Id = Guid.NewGuid().ToString();

            Name = request.Name ?? throw new ArgumentNullException(nameof(request.Name), "Name cannot be null");
            Email = request.Email ?? throw new ArgumentNullException(nameof(request.Email), "Email cannot be null");
            Password = request.Password ?? throw new ArgumentNullException(nameof(request.Password), "Password cannot be null");
        }
    }
}
