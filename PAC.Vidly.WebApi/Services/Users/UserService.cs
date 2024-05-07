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
        
        public void Add(User userToSave)
        {
            var exists = _userRepository.GetOrDefault(u => u.Email == userToSave.Email);
            if (exists != null)
            {
                throw new ValidationException("User already exists");
            }
            _userRepository.Add(userToSave);
        }
    }
}
