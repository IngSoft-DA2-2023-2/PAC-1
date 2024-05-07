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
        
        public User Create(User user)
        {
            if (string.IsNullOrWhiteSpace(user.Name))
            {
                throw new ValidationException("Name cannot be empty");
            }
            if (IsUserExist(user.Email))
            {
                throw new ValidationException("User already exists");
            }
            _userRepository.Add(user);
            return user;
        }
        
        public bool IsUserExist(string email)
        {
            return _userRepository.GetOrDefault(u => u.Email == email) != null;
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
