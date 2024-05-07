
using FluentAssertions;
using Moq;
using PAC.Vidly.WebApi.Controllers.Movies;
using PAC.Vidly.WebApi.Controllers.Movies.Models;
using PAC.Vidly.WebApi.Services.Movies;

namespace PAC.Vidly.WebApi.UnitTests.Controllers
{
    [TestClass]
    public sealed class MovieControllerTest
    {
        private MovieController _controller;
        private Mock<IMovieService> _movieServiceMock;

        [TestInitialize]
        public void Initialize()
        {
            _movieServiceMock = new Mock<IMovieService>();
            _controller = new MovieController(_movieServiceMock.Object);
        }

        #region Create
        [TestMethod]
        public void Create_WhenInfoIsCorrect_ShouldReturnId()
        {
            var request = new CreateMovieRequest(){ Name = "test", CreatorId = "1"};

            var id = _controller.Create(request);

            _movieServiceMock.VerifyAll();
            id.Should().NotBeNull();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_WhenNameIsEmpty_ShouldThrowException()
        {
            var request = new CreateMovieRequest(){ Name = "", CreatorId = "1"};
            try
            {
                _controller.Create(request);
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
            var request = new CreateMovieRequest(){ Name = "test", CreatorId = "1"};
            _controller.Create(request);
            
            var movies = _controller.GetAll();

            _movieServiceMock.VerifyAll();

            movies.Should().HaveCount(1);

            var movie = movies[0];
            movie.Name.Should().Be("test");
            movie.Id.Should().Be("1");
        }
        #endregion
    }
}
