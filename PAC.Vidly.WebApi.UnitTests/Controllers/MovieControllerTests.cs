
using FluentAssertions;
using Moq;
using PAC.Vidly.WebApi.Controllers.Movies;
using PAC.Vidly.WebApi.DataAccess;
using PAC.Vidly.WebApi.Services.Movies;
using PAC.Vidly.WebApi.Services.Movies.Entities;

namespace PAC.Vidly.WebApi.UnitTests.Controllers
{
    [TestClass]
    public sealed class MovieControllerTests
    {
        private MovieController _controller;
        private Mock<IMovieService> _movieServiceMock;

        [TestInitialize]
        public void Initialize()
        {
            _movieServiceMock = new Mock<IMovieService>(MockBehavior.Strict);
            _controller = new MovieController(_movieServiceMock.Object);
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
            _movieServiceMock.Setup(m => m.Create(It.IsAny<Movie>())).Returns(request);

            var id = _controller.Create(request);

            _movieServiceMock.VerifyAll();
            id.Should().NotBeNull();
            id.Should().Be(request);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Create_WhenNameIsEmpty_ShouldThrowException()
        {
            var request = new Movie
            {
                Id = "test",
                Name = string.Empty,
                CreatorId = "test",
            };

            try
            {
                _controller.Create(request);
            }
            catch (Exception ex)
            {
                ex.Message.Should().Be("Code:InvalidRequest, Description: Name cannot be empty");
                throw;
            }
        }
        #endregion

        #region GetAll
        [TestMethod]
        public void GetAll_WhenExistOnlyOneMovie_ShouldReturnMoviesMapped()
        {
            var request = new Movie
            {
                Id = "test",
                Name = string.Empty,
                CreatorId = "test",
            };
            
            _movieServiceMock.Setup(m => m.GetAll());

            var movies = _controller.GetAll();

            _movieServiceMock.VerifyAll();

            movies.Should().HaveCount(1);

            var movie = movies[0];
            movie.Name.Should().Be("test");
            movie.Creator.Name.Should().Be("test creator");
        }
        #endregion
    }
}
