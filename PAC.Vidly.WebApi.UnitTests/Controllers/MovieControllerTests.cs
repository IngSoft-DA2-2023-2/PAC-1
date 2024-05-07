
using System.Linq.Expressions;
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
        
        private Mock<IRepository<Movie>> _repositoryMock;
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
                Name = "test",
                CreatorId = "test",
            };
            _repositoryMock.Setup(r => r.Exist(It.IsAny<Expression<Func<Movie, bool>>>())).Returns(false);
            _repositoryMock.Setup(r => r.Add(It.IsAny<Movie>()));
            var id = _controller.Create(request);

            id.Should().NotBeNull(request.Id);
            id.Should().Be(request.Id);
        }

      
        
        
        #endregion

        #region GetAll
        [TestMethod]
        public void GetAll_WhenExistOnlyOneMovie_ShouldReturnMoviesMapped()
        {
            var movie = new Movie
            {
                Name = "test",
                CreatorId = "test",
            };
            var movies = _controller.GetAll();

            _movieServiceMock.VerifyAll();

            movies.Should().HaveCount(1);
            movie.Id.Should().Be(movie.Id);
            movie.Name.Should().Be("test");
            movie.CreatorId.Should().Be("test");
        }
        #endregion
    }
}
