using FluentAssertions;
using Moq;
using PAC.Vidly.WebApi.Controllers.Movies.Models;
using PAC.Vidly.WebApi.DataAccess;
using PAC.Vidly.WebApi.Services.Movies;
using PAC.Vidly.WebApi.Services.Movies.Entities;
using System.Linq.Expressions;

namespace PAC.Vidly.WebApi.UnitTests.Services
{
    [TestClass]
    public sealed class MovieServiceTests
    {
        private Mock<IRepository<Movie>> _movieRepositoryMock;

        private MovieService _service;

        [TestInitialize]
        public void Initialize()
        {
            _movieRepositoryMock = new Mock<IRepository<Movie>>(MockBehavior.Strict);
            _service = new MovieService(_movieRepositoryMock.Object);
        }

        #region Create
        #region Error
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_WhenNameIsNull_ShouldThrowException()
        {
            var args = new CreateMovieArguments(null);
            
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

        #endregion

        #region Success
        [TestMethod]
        public void Create_WhenInfoIsCorrect_ShouldReturnId()
        {
            var args = new CreateMovieArguments("test");
            
            var userLoggedId = "test2";
            var movie = new Movie(args.Name, userLoggedId);

            var movieId = _service.Create(args, userLoggedId);
            _movieRepositoryMock.Setup(x => x.Add(It.IsAny<Movie>()));
            _movieRepositoryMock.Setup(x => x.GetOrDefault(It.IsAny<Expression<Func<Movie, bool>>>())).Returns(movie);

            _movieRepositoryMock.VerifyAll();
            movieId.Should().NotBeNull();
        }
        #endregion
        #endregion

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_WhenNameIsEmpty_ShouldThrowException()
        {
            var args = new CreateMovieArguments("");
            
            var userLoggedId = "test2";

            try
            {
                _service.Create(args, userLoggedId);
            }
            catch (Exception ex)
            {
                ex.Message.Should().Be("Name cannot be empty or null");
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_WhenMovieIsDuplicated_ShouldThrowException()
        {
            var args = new CreateMovieArguments("duplicated");
            var userLoggedId = "test2";

            _movieRepositoryMock
                .Setup(x => x.GetOrDefault(It.IsAny<Expression<Func<Movie, bool>>>()))
                .Returns(new Movie());

            try
            {
                _service.Create(args, userLoggedId);
            }
            catch (Exception ex)
            {
                ex.Message.Should().Be("Movie is duplicated");
                throw;
            }
        }
    }
}
