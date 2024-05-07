using PAC.Vidly.WebApi.Services.Movies.Entities;
using PAC.Vidly.WebApi.Services.Users.Entities;

namespace PAC.Vidly.WebApi.Controllers.Users
{
    public class CreateUserResponse
    {
        private string Id;

        public CreateUserResponse(User user)
        {
            Id = user.Id;
        }
    }
}