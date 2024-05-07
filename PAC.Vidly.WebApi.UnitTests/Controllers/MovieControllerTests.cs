
using FluentAssertions;
using Moq;
using PAC.Vidly.WebApi.Controllers.Movies;
using PAC.Vidly.WebApi.Controllers.Movies.Models;
using PAC.Vidly.WebApi.DataAccess;
using PAC.Vidly.WebApi.Services.Movies;
using PAC.Vidly.WebApi.Services.Movies.Entities;
using PAC.Vidly.WebApi.Services.Users.Entities;

namespace PAC.Vidly.WebApi.UnitTests.Controllers
{
    [TestClass]
    public sealed class MovieControllerTests
    {
        private Mock<IMovieService> _movieServiceMock = new();
        private MovieController _controller;

        [TestInitialize]
        public void Initialize()
        {
            _controller = new MovieController(_movieServiceMock.Object);
        }

        #region Create
        [TestMethod]
        public void Create_WhenInfoIsCorrect_ShouldReturnId()
        {
            var request = new CreateMovieRequest
            {
                Name = "test",
            };

            var id = _controller.Create(request, "userToken");

            _movieServiceMock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_WhenNameIsEmpty_ShouldThrowException()
        {

            var request = new CreateMovieRequest
            {
                Name = string.Empty,
            };

            try
            {
                _controller.Create(request, "userToken");
            }
            catch (Exception ex)
            {
                ex.Message.Should().Be("Name is required");
            }
        }
        #endregion

        #region GetAll
        [TestMethod]
        public void GetAll_WhenExistOnlyOneMovie_ShouldReturnMoviesMapped()
        {
            var movies = _controller.GetAll();

            _movieServiceMock.VerifyAll();
            var movie = new Movie
            {
                Name = "test",
                Creator = new User
                {
                    Name = "test creator",
                },
            };

            movies.Should().HaveCount(1);

            var movieToGet = movies[0];
            movieToGet.Name.Should().Be("test");
            movieToGet.Creator.Name.Should().Be("test creator");
        }
        #endregion
    }
}
