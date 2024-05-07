using FluentAssertions;
using Moq;
using PAC.Vidly.WebApi.DataAccess;
using PAC.Vidly.WebApi.Services.Movies;
using PAC.Vidly.WebApi.Services.Movies.Entities;

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
            // Arrange
            var args = new Movie
            {
                Id = "test",
                Name = null,
                CreatorId = "test"
            };
            var userLoggedId = "test2";

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => _service.Create(args, userLoggedId))
                .Message.Should().Be("Name cannot be empty or null");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Create_WhenMovieIsDuplicated_ShouldThrowException()
        {
            // Arrange
            var args = new Movie
            {
                Id = "test",
                Name = "duplicated",
                CreatorId = "test"
            };
            var userLoggedId = "test2";

            
            _movieRepositoryMock.Setup(repo => repo.GetAll(m => m.Name == args.Name)).Returns(new List<Movie> { args });

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => _service.Create(args, userLoggedId))
                .Message.Should().Be("Movie is duplicated");
        }
        #endregion

        #region Success
        [TestMethod]
        public void Create_WhenInfoIsCorrect_ShouldReturnId()
        {
            var args = new Movie
            {
                Id = Guid.NewGuid().ToString(),
                Name = "duplicated",
                CreatorId = "test"
            };
            var userLoggedId = "test";

            // Act
            var movieId = Guid.NewGuid();

            // Assert
            _movieRepositoryMock.VerifyAll();
            movieId.Should().Be(args.Id);
        }
        #endregion
        #endregion
    }
}
