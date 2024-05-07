using Microsoft.AspNetCore.Mvc;
using PAC.Vidly.WebApi.Controllers.Users.Models;
using PAC.Vidly.WebApi.DataAccess;
using PAC.Vidly.WebApi.Services.Users;
using PAC.Vidly.WebApi.Services.Users.Entities;

namespace PAC.Vidly.WebApi.Controllers.Users;

[ApiController]
[Route("users")]
public class UserController
{
    private readonly IUserService _service;
    private readonly IRepository<User> _repository;
    
    public UserController(IUserService service)
    {
        _service = service;
    }

    [HttpPost]
    public CreateUserResponse Create(CreateUserRequest request)
    {
        VerifyRepeatedMail(request.Email);
        User u = new(request.Name, request.Email, request.Password, request.FavoriteMovies);
        _service.Create(u);
        return new CreateUserResponse(u.Email);
    }

    private void VerifyRepeatedMail(string email)
    {
       User? found = _repository.GetAll().Find(x => x.Email == email);
       if (found != null)
       {
           throw new ArgumentException("User already exists.");
       }
    }
}