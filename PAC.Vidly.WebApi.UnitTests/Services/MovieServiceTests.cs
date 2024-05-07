using FluentAssertions;
using Moq;
using PAC.Vidly.WebApi.DataAccess;
using PAC.Vidly.WebApi.Services.Movies;
using PAC.Vidly.WebApi.Services.Movies.Entities;
using PAC.Vidly.WebApi.Services.Users.Entities;
using PAC.Vidly.WebApi.Services.Users;

namespace PAC.Vidly.WebApi.UnitTests.Services
{
    [TestClass]
    public sealed class MovieServiceTests
    {
        private Mock<IRepository<Movie>> _movieRepositoryMock;

        private IMovieService _service;

        [TestInitialize]
        public void Initialize()
        {
            _movieRepositoryMock = new Mock<IRepository<Movie>>(MockBehavior.Strict);
            var userRepositoryMock = new Mock<IRepository<User>>(MockBehavior.Strict);
            var userService = new UserService(userRepositoryMock.Object);
            _service = new MovieService(_movieRepositoryMock.Object, userService);
        }

        #region Create
        #region Error
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Create_WhenNameIsNull_ShouldThrowException()
        {
            var args = new MovieArguments(null);
            var userLoggedId = "test2";

            try
            {
                _service.Create(args, userLoggedId);
            }
            catch (ArgumentException ex)
            {
                ex.Message.Should().Be("Name is required (Parameter 'name')");
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Create_WhenMovieIsDuplicated_ShouldThrowException()
        {
            var args = new MovieArguments("Titanic");
            var userLoggedId = "test2";
            try
            {
                _service.Create(args, userLoggedId);

                _service.Create(args, userLoggedId);
            }
            catch (InvalidOperationException ex)
            {
                ex.Message.Should().Be("Movie is duplicated");
                throw;
            }
        }
        #endregion

        #region Success
        [TestMethod]
        public void Create_WhenInfoIsCorrect_ShouldReturnId()
        {
            var args = new MovieArguments("Titanic");
            var userLoggedId = "test2";

            var movieId = _service.Create(args, userLoggedId);

            _movieRepositoryMock.VerifyAll();
            movieId.Should().NotBeNull();
        }
        #endregion
        #endregion
    }
}
