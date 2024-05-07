using PAC.Vidly.WebApi.DataAccess;
using PAC.Vidly.WebApi.Services.Users.Entities;
using PAC.Vidly.WebApi.Services.Sessions;

namespace PAC.Vidly.WebApi.Services.Users
{
    public sealed class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly ISessionService _sessionService;

        public UserService(IRepository<User> userRepository,  ISessionService sessionService)
        {
            _userRepository = userRepository;
            _sessionService = sessionService;
        }
        
        public UserService(IRepository<User> userRepository)
        {
            throw new NotImplementedException();
        }

        public User GetByCredentials(string email, string password)
        {
            var user = _userRepository.GetOrDefault(u => u.Email == email && u.Password == password);

            if(user == null)
            {
                throw new InvalidOperationException("User not found");
            }

            return user;
        }
        


    }
}
