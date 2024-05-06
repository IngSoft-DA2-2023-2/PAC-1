using PAC.Vidly.WebApi.Services.Users.Entities;

namespace PAC.Vidly.WebApi.Services.Sessions
{
    public interface ISessionService
    {
        string Create(string email, string password);

        User GetUserByToken(string token);

        bool IsValidToken(string token);
    }
}
