
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PAC.Vidly.WebApi.Controllers.Movies;
using PAC.Vidly.WebApi.Controllers.Movies.Models;
using PAC.Vidly.WebApi.DataAccess;
using PAC.Vidly.WebApi.Services.Movies;
using PAC.Vidly.WebApi.Services.Movies.Entities;
using PAC.Vidly.WebApi.Services.Users;
using PAC.Vidly.WebApi.Services.Users.Entities;

namespace PAC.Vidly.WebApi.UnitTests.Controllers
{
    [TestClass]
    public sealed class MovieControllerTests
    {
        private MovieController _controller;
        private Mock<IMovieService> _movieServiceMock = new Mock<IMovieService>(MockBehavior.Strict);
        private Mock<HttpContext> _httpContextMock;

        [TestInitialize]
        public void Initialize()
        {
            _httpContextMock = new Mock<HttpContext>(MockBehavior.Strict);
            ControllerContext controllerContext = new ControllerContext
            {
                HttpContext = _httpContextMock.Object
            };
            _controller = new MovieController(_movieServiceMock.Object);
            _controller.ControllerContext = controllerContext;

            _movieServiceMock = new Mock<IMovieService>(MockBehavior.Strict);
            
        }

        #region Create
        [TestMethod]
        public void Create_WhenInfoIsCorrect_ShouldReturnId()
        {
            var request = new CreateMovieRequest
            {
                Name = "testName"
            };
            User user = new User();
            _httpContextMock.SetupGet(c => c.Items[Items.UserLogged]).Returns(user);

            string idMocked = "testId";

            _movieServiceMock.Setup(s => s.Create(It.IsAny<MovieArguments>(), It.IsAny<string>())).Returns(idMocked);

            var id = _controller.Create(request);

            _movieServiceMock.VerifyAll();
            id.Should().NotBeNull(id);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_WhenNameIsEmpty_ShouldThrowException()
        {
            var repositoryMock = new Mock<IRepository<Movie>>(MockBehavior.Strict);
            var userRepositoryMock = new Mock<IRepository<User>>(MockBehavior.Strict);
            var userService = new UserService(userRepositoryMock.Object);

            var service = new MovieService(repositoryMock.Object, userService);
            var controller = new MovieController(service);

            var request = new CreateMovieRequest
            {
                Name = string.Empty,
            };

            try
            {
                controller.Create(request);
            }
            catch (Exception ex)
            {
                ex.Message.Should().Be("Name cannot be empty");
                throw;
            }

        }
        #endregion

        #region GetAll
        [TestMethod]
        public void GetAll_WhenExistOnlyOneMovie_ShouldReturnMoviesMapped()
        {
            var movies = _controller.GetAll();

            _movieServiceMock.VerifyAll();

            movies.Should().HaveCount(1);

            var movie = movies[0];
            movie.Name.Should().Be("test");
            movie.CreatorName.Should().Be("test creator");
        }
        #endregion
    }
}
