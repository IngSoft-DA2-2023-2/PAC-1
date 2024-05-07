using PAC.Vidly.WebApi.Controllers.Users.Models;
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

        public User Create(CreateUserRequest user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if(_userRepository.GetOrDefault(u => u.Email == user.Email) != null)
            {
                throw new InvalidOperationException("User already exists");
            }

            var userToCreate = new User
            {
                Email = user.Email,
                Password = user.Password,
                Name = user.Name,
            };

            _userRepository.Add(userToCreate);

            return userToCreate;    

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
