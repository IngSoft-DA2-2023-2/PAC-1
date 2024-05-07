using PAC.Vidly.WebApi.Controllers.Users.Models;
using PAC.Vidly.WebApi.Services.Users.Entities;

namespace PAC.Vidly.WebApi.Services.Users
{
    public interface IUserService
    {
        User Create(User user);
        
        bool IsUserExist(string email);
        User GetByCredentials(string email, string password);
    }
}
