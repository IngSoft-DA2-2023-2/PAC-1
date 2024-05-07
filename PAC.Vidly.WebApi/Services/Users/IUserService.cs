using PAC.Vidly.WebApi.Services.Users.Entities;

namespace PAC.Vidly.WebApi.Services.Users
{
    public interface IUserService
    {
        User GetByCredentials(string email, string password);

        void Create(User u);

        List<User> GetAll();
    }
}
