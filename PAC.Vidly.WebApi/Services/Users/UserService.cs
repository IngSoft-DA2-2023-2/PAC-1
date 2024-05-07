using PAC.Vidly.WebApi.DataAccess;
using PAC.Vidly.WebApi.Services.Users.Entities;
using System.ComponentModel.DataAnnotations;
using PAC.Vidly.WebApi.Controllers.Users.Models;

namespace PAC.Vidly.WebApi.Services.Users
{
    public sealed class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public User Create(UserDto userDto)
        {
            var userExists = _userRepository.GetOrDefault(u => u.Email == userDto.Email);

            if(userExists != null)
            {
                throw new ValidationException("User already exists");
            }
            var user = new User
            {
                Id = userDto.Id,
                Name = userDto.Name,
                Email = userDto.Email,
                Password = userDto.Password
            };
            _userRepository.Add(user);
            return user;
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
