using FluentAssertions;
using Moq;
using PAC.Vidly.WebApi.Controllers.Movies;
using PAC.Vidly.WebApi.Controllers.User;
using PAC.Vidly.WebApi.Services.Movies;
using PAC.Vidly.WebApi.Services.Movies.Entities;
using PAC.Vidly.WebApi.Services.Users;
using PAC.Vidly.WebApi.Services.Users.Entities;

namespace PAC.Vidly.WebApi.UnitTests.Controllers;

[TestClass]
public class UserControllerTest
{
    private UserController _controller;
    private Mock<IUserService> _userServiceMock;

    [TestInitialize]
    public void Initialize()
    {
        _userServiceMock = new Mock<IUserService>(MockBehavior.Strict);
        _controller = new UserController(_userServiceMock.Object);
    }
    
    [TestMethod]
    public void Create_WhenInfoIsCorrect_ShouldReturnId()
    {
        var request = new User
        {
            Id = "test",
            Name = "test",
            Password = "test",
            Email = "algo@email.com"
             
        };
        _userServiceMock.Setup(m => m.Create(It.IsAny<User>())).Returns(request);

        var id = _controller.Create(request);

        _userServiceMock.VerifyAll();
        id.Should().NotBeNull();
        id.Should().Be(request);
    }
}