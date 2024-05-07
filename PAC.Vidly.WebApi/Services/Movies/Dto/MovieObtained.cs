using PAC.Vidly.WebApi.Services.Users.Entities;

namespace PAC.Vidly.WebApi.Services.Movies.Dto;

public class MovieObtained
{
        public string Name { get; set; }
        public int usersAmount { get; set; }
        public List<User> Users { get; set; }
    
}