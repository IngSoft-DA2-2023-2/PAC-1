using System.Linq.Expressions;
using FluentAssertions;
using Moq;
using PAC.Vidly.WebApi.DataAccess;
using PAC.Vidly.WebApi.Services.Movies;
using PAC.Vidly.WebApi.Services.Movies.Entities;
using PAC.Vidly.WebApi.Services.Users;
using PAC.Vidly.WebApi.Services.Users.Entities;

namespace PAC.Vidly.WebApi.UnitTests.Services;

[TestClass]
public class UserServiceTest
{
    private Mock<IRepository<User>> _userRepositoryMock;

    private UserService _service;

    [TestInitialize]
    public void Initialize()
    {
        _userRepositoryMock = new Mock<IRepository<User>>();
        _service = new UserService(_userRepositoryMock.Object);
    }
    
    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public void Create_WhenMovieIsDuplicated_ShouldThrowException()
    {
        var args = new User
        {
            Id = "test",
            Name = "duplicated",
            Password = "test",
            Email = "fads@mail.com"
        };

        try
        {
            _userRepositoryMock.Setup(r => r.GetOrDefault(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns(args);

            _service.Create(args);
        }
        catch (Exception ex)
        {
            ex.Message.Should().Be("user duplicated");
            throw;
        }
    }
}