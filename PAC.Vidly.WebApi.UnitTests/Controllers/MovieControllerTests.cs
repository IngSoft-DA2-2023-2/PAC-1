
using FluentAssertions;
using Moq;
using PAC.Vidly.WebApi.Controllers.Movies;
using PAC.Vidly.WebApi.DataAccess;
using PAC.Vidly.WebApi.Services.Movies;
using PAC.Vidly.WebApi.Services.Movies.Entities;
using PAC.Vidly.WebApi.Services.Users;

namespace PAC.Vidly.WebApi.UnitTests.Controllers
{
    [TestClass]
    public sealed class MovieControllerTests
    {
        private MovieController _controller;
        private Mock<IMovieService> _movieServiceMock;
        private Mock<IUserService> _userServiceMock;
        private IRepository<Movie> repo;

        [TestInitialize]
        public void Initialize()
        {
            _movieServiceMock = new Mock<IMovieService>(MockBehavior.Strict);
            _userServiceMock = new Mock<IUserService>(MockBehavior.Strict);
            var movieService = new MovieService(repo);
            _controller = new MovieController(movieService, _userServiceMock.Object);
        }

        #region Create
        [TestMethod]
        public void Create_WhenInfoIsCorrect_ShouldReturnId()
        {
            var request = new Movie
            {
                Id = "test",
                Name = "test",
                CreatorId = "test",
            };

            var response = _controller.Create(request);

            _movieServiceMock.VerifyAll();
            response.Should().NotBeNull(response.Id);
            response.Should().Be(request.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_WhenNameIsEmpty_ShouldThrowException()
        {
            var repositoryMock = new Mock<IRepository<Movie>>(MockBehavior.Strict);
            var service = new MovieService(repositoryMock.Object);
            var controller = new MovieController(service, _userServiceMock.Object);

            var request = new Movie
            {
                Id = "test",
                Name = string.Empty,
                CreatorId = "test",
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
