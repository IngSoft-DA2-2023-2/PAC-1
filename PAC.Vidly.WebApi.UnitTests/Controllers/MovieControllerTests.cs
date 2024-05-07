
using FluentAssertions;
using Moq;
using PAC.Vidly.WebApi.Controllers.Movies;
using PAC.Vidly.WebApi.Controllers.Movies.Models;
using PAC.Vidly.WebApi.DataAccess;
using PAC.Vidly.WebApi.Services.Movies;
using PAC.Vidly.WebApi.Services.Movies.Entities;
using PAC.Vidly.WebApi.Services.Sessions;
using PAC.Vidly.WebApi.Services.Sessions.Entities;
using PAC.Vidly.WebApi.Services.Users;
using PAC.Vidly.WebApi.Services.Users.Entities;

namespace PAC.Vidly.WebApi.UnitTests.Controllers
{
    [TestClass]
    public sealed class MovieControllerTests
    {
        private MovieController _controller;
        private Mock<IMovieService> _movieServiceMock;
        private Mock<ISessionService> _sessionServiceMock;

        [TestInitialize]
        public void Initialize()
        {
            _movieServiceMock = new Mock<IMovieService>(MockBehavior.Strict);
            _sessionServiceMock = new Mock<ISessionService>(MockBehavior.Strict);
            _controller = new MovieController(_movieServiceMock.Object, _sessionServiceMock.Object);
        }

        #region Create
        [TestMethod]
        public void Create_WhenInfoIsCorrect_ShouldReturnId()
        {
            var request = new CreateMovieRequest()
            {
                Name = "test",
            };

            var id = _controller.Create(request);

            _movieServiceMock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_WhenNameIsEmpty_ShouldThrowException()
        {
            var repositoryMock = new Mock<IRepository<Movie>>(MockBehavior.Strict);
            var sessionRepoMock = new Mock<IRepository<Session>>(MockBehavior.Strict);
            var userRepoMock = new Mock<IRepository<User>>(MockBehavior.Strict);
            var UserService = new UserService(userRepoMock.Object);
            var service = new MovieService(repositoryMock.Object);
            var sessionService = new SessionService(sessionRepoMock.Object, UserService);
            var controller = new MovieController(service, sessionService);

            var request = new CreateMovieRequest()
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
