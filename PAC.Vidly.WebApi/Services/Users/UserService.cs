using PAC.Vidly.WebApi.DataAccess;
using PAC.Vidly.WebApi.Services.Users.Entities;
using System.ComponentModel.DataAnnotations;

namespace PAC.Vidly.WebApi.Services.Users
{
    public sealed class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
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

        public void Create(User u)
        {
            _userRepository.Add(u);
        }

        public List<User> GetAll()
        {
            return _userRepository.GetAll();
        }
    }
}
