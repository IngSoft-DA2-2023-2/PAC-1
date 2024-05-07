using FluentAssertions;
using Moq;
using PAC.Vidly.WebApi.DataAccess;
using PAC.Vidly.WebApi.Services.Movies;
using PAC.Vidly.WebApi.Services.Movies.Entities;
using PAC.Vidly.WebApi.Controllers.Movies;
using PAC.Vidly.WebApi.Controllers.Movies.Models;
using PAC.Vidly.WebApi.Controllers.Users;
using PAC.Vidly.WebApi.Controllers.Users.Models;
using PAC.Vidly.WebApi.Services.Users.Entities;

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
            var userRequest = new CreateUserRequest
            {
                Name = "test"
            };
            var userDto = new UserDto(userRequest);
            var user = new User();
            var movieRequest = new CreateMovieRequest
            {
                Name = null,
            };
            var movieDto = new MovieDto(movieRequest, new User());
            var userLoggedId = "test2";
            Action act = () => _service.Create(movieDto, userLoggedId);

            act.Should().Throw<ArgumentNullException>()
               .WithMessage("Name cannot be empty or null");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Create_WhenMovieIsDuplicated_ShouldThrowException()
        {
            var user = new User
            {
                Id = "test",
                Name = "test",
                Email = "test",
                Password = "test"
            };

            var movieRepositoryMock = new Mock<IRepository<Movie>>();

            movieRepositoryMock.Setup(m => m.GetAll()).Returns(new List<Movie> { new Movie { Name = "duplicated" } });

            var service = new MovieService(movieRepositoryMock.Object);

            var movieRequest = new CreateMovieRequest
            {
                Name = "duplicated"
            };
            var movieDto = new MovieDto(movieRequest, user);
            var userLoggedId = "test2";

            // Act & Assert
            service.Invoking(s => s.Create(movieDto, userLoggedId))
                   .Should().Throw<InvalidOperationException>()
                   .WithMessage("Movie is duplicated");
        }
        #endregion

        #region Success
        [TestMethod]
        public void Create_WhenInfoIsCorrect_ShouldReturnId()
        {
            var user = new User
            {
                Id = "test",
                Name = "test",
                Email = "test",
                Password = "test"
            };
            var movieRequest = new CreateMovieRequest
            {
                Name = "test"
            };
            var movieDto = new MovieDto(movieRequest, user);
            var movie = new Movie
            {
                Id = "test",
                Name = "test",
                CreatorId = "test"
            };
            var userLoggedId = "test2";
            _movieRepositoryMock.Setup(m => m.Add(movie));
            var movieId = _service.Create(movieDto, userLoggedId);
            _movieRepositoryMock.VerifyAll();
            movieId.Should().NotBeNull();
            movieId.Should().Be(movieId.Id);
        }
        #endregion
        #endregion
    }
}
