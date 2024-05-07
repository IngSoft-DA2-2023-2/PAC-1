using PAC.Vidly.WebApi.Services.Users.Entities;
using PAC.Vidly.WebApi.Controllers.Users.Models;

namespace PAC.Vidly.WebApi.Services.Users
{
    public interface IUserService
    {
        User Create(UserDto user);
        User GetByCredentials(string email, string password);
    }
}
