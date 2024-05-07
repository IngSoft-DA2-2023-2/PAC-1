using FluentAssertions;
using Moq;
using PAC.Vidly.WebApi.DataAccess;
using PAC.Vidly.WebApi.Exceptions;
using PAC.Vidly.WebApi.Services.Movies;
using PAC.Vidly.WebApi.Services.Movies.Entities;
using PAC.Vidly.WebApi.Services.Users.Entities;

namespace PAC.Vidly.WebApi.UnitTests.Services
{
    [TestClass]
    public sealed class MovieServiceTests
    {
        private Mock<IRepository<Movie>> _movieRepositoryMock;

        private MovieService _service;
        private User userTest;

        [TestInitialize]
        public void Initialize()
        {
            _movieRepositoryMock = new Mock<IRepository<Movie>>(MockBehavior.Strict);
            _service = new MovieService(_movieRepositoryMock.Object);

            userTest = new User
            {
                Name = "test",
                Email = "test@gmail.com",
                Password = "pass"
            };
        }

        #region Create
        #region Error
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_WhenNameIsNull_ShouldThrowException()
        {
            var args = new CreateMovieArgs
            (
                null
            );

            try
            {
                _service.Create(args, userTest);
            }
            catch (ArgsNullException ex)
            {
                ex.Message.Should().Be("Name cannot be empty or null");
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Create_WhenMovieIsDuplicated_ShouldThrowException()
        {
            var args = new CreateMovieArgs
            (
                "test"
            );

            _service.Create(args, userTest);

            try
            {
                _service.Create(args, userTest);
            }
            catch (DuplicatedException ex)
            {
                ex.Message.Should().Be("Movie is duplicated");
            }
        }
        #endregion

        #region Success
        [TestMethod]
        public void Create_WhenInfoIsCorrect_ShouldAnIdBeSetted()
        {
            var args = new CreateMovieArgs
            (
                "test"
            );

            var movieId = _service.Create(args, userTest);

            _movieRepositoryMock.VerifyAll();
            movieId.Should().NotBeNull();
        }
        #endregion
        #endregion
    }
}
