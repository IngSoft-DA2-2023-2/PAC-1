using FluentAssertions;
using Moq;
using PAC.Vidly.WebApi.Controllers.Movies.Models;
using PAC.Vidly.WebApi.DataAccess;
using PAC.Vidly.WebApi.Services.Movies;
using PAC.Vidly.WebApi.Services.Movies.Entities;
using PAC.Vidly.WebApi.Services.Sessions;
using System.Linq.Expressions;

namespace PAC.Vidly.WebApi.UnitTests.Services
{
    [TestClass]
    public sealed class MovieServiceTests
    {
        private Mock<IRepository<Movie>> _movieRepositoryMock;

        private MovieService _service;
        private SessionService _sessionService;

        [TestInitialize]
        public void Initialize()
        {
            _movieRepositoryMock = new Mock<IRepository<Movie>>();
            _service = new MovieService(_movieRepositoryMock.Object, _sessionService);

        }

        #region Create
        #region Error
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_WhenNameIsNull_ShouldThrowException()
        {
            var args = new CreateMovieRequest
            {
                Name = null,
            };
            var userLoggedId = "test2";

            try
            {
                _service.Create(args, userLoggedId);
            }
            catch (Exception ex)
            {
                ex.Message.Should().Be("Name cannot be empty or null");
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Create_WhenMovieIsDuplicated_ShouldThrowException()
        {
            var args = new CreateMovieRequest
            {
                Name = "duplicated",
            };
            var userLoggedId = "test2";

            _movieRepositoryMock.Setup(x => x.GetOrDefault(It.IsAny<Expression<Func<Movie, bool>>>())).Returns(new Movie());

            try
            {
                _service.Create(args, userLoggedId);
            }
            catch (Exception ex)
            {
                ex.Message.Should().Be("Movie is duplicated");
            }
        }
        #endregion

        #region Success
        [TestMethod]
        public void Create_WhenInfoIsCorrect_ShouldReturnId()
        {
            var args = new CreateMovieRequest
            {
                Name = "duplicated",
            };
            var userLoggedId = "test2";

            _movieRepositoryMock.Setup(x => x.GetOrDefault(It.IsAny<Expression<Func<Movie, bool>>>())).Returns((Movie)null);

            var movieId = _service.Create(args, userLoggedId);

            _movieRepositoryMock.VerifyAll();
            movieId.Should().NotBeNull();
        }
        #endregion
        #endregion
    }
}
